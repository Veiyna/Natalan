using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using LobbyServer.Manager;
using LobbyServer.Network.Message;
using Lumina.Excel.GeneratedSheets;
using MongoDB.Driver;
using Shared.Database;
using Shared.Database.Datacentre;
using Shared.Game;
using Shared.Network;
using Shared.SqPack;

namespace LobbyServer.Network.Handler
{
    public class CharacterHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientCharacterList, SubPacketHandlerFlags.RequiresEncryption)]
        public static void HandleClientSessionRequest(LobbySession session, ClientCharacterList characterList)
        {
            session.Sequence = characterList.Sequence;

            if (characterList.ServiceAccount >= session.ServiceAccounts.Count)
                return;

            // must be sent before characrer list otherwise crash
            SendRealmList(session);

            session.ServiceAccount = session.ServiceAccounts[characterList.ServiceAccount];
            SendCharacterList(session);
            
            // SendRetainerList();
        }

        private static void SendRealmList(LobbySession session)
        {
            var realmInfo = AssetManager.RealmInfoStore;

            var realmList = new ServerRealmList { Sequence = session.Sequence };
            for (ushort i = 0; i < realmInfo.Count; i++)
            {
                // client expects no more than 6 realms per chunk
                if (i % ServerRealmList.MaxRealmsPerPacket == 0)
                {
                    // flush previous chunk
                    if (i != 0)
                    {
                        session.Send(realmList);
                        realmList.Realms.Clear();
                    }

                    realmList.Offset = i;
                    realmList.Final  = (ushort)(realmInfo.Count - i < ServerRealmList.MaxRealmsPerPacket ? 1 : 0);
                }

                var realm = realmInfo[i];
                realmList.Realms.Add(new ServerRealmList.RealmInfo
                {
                    Id       = realm.Id,
                    Position = i,
                    Name     = realm.Name,
                    Flags    = realm.Flags
                });

                // flush final chunk
                if (i == realmInfo.Count - 1)
                    session.Send(realmList);
            }
        }

        private static void SendCharacterList(LobbySession session)
        {
            session.NewEvent(new DatabaseGenericEvent<List<CharacterInfo>>(DatabaseManager.DataCentre.GetCharacters(session.ServiceAccount.Id), characters =>
            {
                session.Characters = characters;

                var characterList = new ServerCharacterList
                {
                    VeteranRank               = 0,
                    DaysTillNextVeteranRank   = 0u,
                    DaysSubscribed            = 0u,
                    SubscriptionDaysRemaining = 0u,
                    RealmCharacterLimit       = session.ServiceAccount.RealmCharacterLimit,
                    AccountCharacterLimit     = session.ServiceAccount.AccountCharacterLimit,
                    Expansion                 = session.ServiceAccount.Expansion,
                    Offset                    = 1
                };

                if (session.Characters.Count == 0)
                {
                    session.Send(characterList);
                    return;
                }

                for (var i = 0; i < session.Characters.Count; i++)
                {
                    // client expects no more than 2 characters per chunk
                    if (i % ServerCharacterList.MaxCharactersPerPacket == 0)
                    {
                        // flush previous chunk
                        if (i != 0)
                        {
                            session.Send(characterList);
                            session.FlushPacketQueue();
                            characterList.Characters.Clear();
                        }

                        // weird...
                        characterList.Offset = (byte)(session.Characters.Count - i <= ServerCharacterList.MaxCharactersPerPacket ? i * 2 + 1 : i * 2);
                    }

                    var realmInfo = AssetManager.GetRealmInfo(session.Characters[i].RealmId);
                    var realmInfo2 = AssetManager.GetRealmInfo(session.Characters[i].CurrentRealmId);
                    if (realmInfo == null || realmInfo2 == null)
                        continue;

                    characterList.Characters.Add(((byte)i, realmInfo.Name, realmInfo2.Name, session.Characters[i]));
                
                    // flush final chunk
                    if (i == session.Characters.Count - 1)
                    {
                        session.Send(characterList);
                        session.FlushPacketQueue();
                    }
                }
            }));
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientCharacterCreate, SubPacketHandlerFlags.RequiresEncryption | SubPacketHandlerFlags.RequiresAccount)]
        public static void HandleCharacterCreate(LobbySession session, ClientCharacterCreate characterCreate)
        {
            session.Sequence = characterCreate.Sequence;
            switch (characterCreate.Type)
            {
                // verify
                case 1:
                {
                    var realmInfo = AssetManager.GetRealmInfo(characterCreate.RealmId);
                    if (realmInfo == null)
                        return;

                    /*session.NewEvent(new DatabaseGenericEvent<bool>(DatabaseManager.DataCentre.IsCharacterNameAvailable(characterCreate.Name), available =>
                    {*/
                    var available = true;
                        if (!CharacterInfo.VerifyName(characterCreate.Name) || !available)
                        {
                            session.SendError(3035, 13004);
                            return;
                        }

                        if (session.Characters?.Count >= session.ServiceAccount.AccountCharacterLimit)
                        {
                            session.SendError(3035, 13203);
                            return;
                        }

                        if (session.Characters?.Count(c => c.RealmId == realmInfo.Id) >= session.ServiceAccount.RealmCharacterLimit)
                        {
                            session.SendError(3035, 13204);
                            return;
                        }

                        session.CharacterCreate = (realmInfo.Id, characterCreate.Name);
                        session.Send(new ServerCharacterCreate
                        {
                            Sequence = session.Sequence,
                            Type     = 1,
                            Name     = characterCreate.Name,
                            Realm    = realmInfo.Name
                        });
                    //}));
                    break;
                }
                // create
                case 2:
                {
                    if (session.CharacterCreate.Name == string.Empty)
                        return;

                    CharacterInfo characterInfo;
                    try
                    {
                        characterInfo = new CharacterInfo(session.ServiceAccount.Id, session.CharacterCreate.RealmId, session.CharacterCreate.Name, characterCreate.Json);
                    }
                    catch
                    {
                        // should only occur if JSON data is tampered with
                        return;
                    }

                    if (!characterInfo.Verify())
                        return;

                    var entry = GameTableManager.ClassJobs.GetRow(characterInfo.ClassJobId);
                    Debug.Assert(entry.RowId >= 0);


                    for (var i = 0; i < 30; i++)
                    {
                        characterInfo.AddClassInfo((byte)i);
                    }

                    characterInfo.Classes[entry.ExpArrayIndex].Level = 1;


                    var startTown = (byte)entry.StartingTown.Row;

                    characterInfo.StartTown = startTown;
                    //if (!AssetManager.GetCharacterSpawn(startTown, out WorldPosition spawnPosition))
                    //    return;

                    //characterInfo.Finalise(AssetManager.GetNewCharacterId(), spawnPosition); 
                    characterInfo.Finalise(AssetManager.GetNewCharacterId(), new WorldPosition(181, new Vector3(-53, 20 ,0), 0f)); 
                    session.NewEvent(new DatabaseEvent(DatabaseManager.DataCentre.Save(c =>
                    {
                        c.Characters.
                        InsertOne(characterInfo);

                    }), () =>
                    {
                        var realmInfo = AssetManager.GetRealmInfo(session.CharacterCreate.RealmId);
                        Debug.Assert(realmInfo != null);

                        session.Send(new ServerCharacterCreate
                        {
                            Sequence    = session.Sequence,
                            Type        = 2,
                            Name        = session.CharacterCreate.Name,
                            Realm       = realmInfo.Name,
                            CharacterId = characterInfo.Id
                        });

                        session.Characters.Add(characterInfo);
                        session.CharacterCreate = (0, string.Empty);
                    }, exception =>
                    {
                        Console.WriteLine(exception);
                        // should only occur if name was claimed in the time between verification and creation
                        session.SendError(3035, 13208);
                    }));
                    break;
                }
                case 4:
                {
                    var characterInfo = session.Characters.SingleOrDefault(c => c.Name == characterCreate.Name);

                    session.NewEvent(new DatabaseEvent(DatabaseManager.DataCentre.Save(c =>
                    {
                        var filter = Builders<CharacterInfo>.Filter
                            .Eq(r => r.Id, characterInfo.Id);
                        c.Characters.DeleteOne(filter);

                    }), () =>
                    {
                        var realmInfo = AssetManager.GetRealmInfo(characterInfo.RealmId);
                        Debug.Assert(realmInfo != null);

                        session.Send(new ServerCharacterCreate
                        {
                            Sequence    = session.Sequence,
                            Type        = 4,
                            Name        = characterCreate.Name,
                            Realm       = realmInfo.Name,
                            CharacterId = 0,
                        });

                        session.Characters.Remove(characterInfo);
                        session.CharacterCreate = (0, string.Empty);
                        // should only occur if name was claimed in the time between verification and creation
                        //session.SendError(3035, 13208);
                    }));
                    

                    break;
                }
            }
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientCharacterDelete, SubPacketHandlerFlags.RequiresEncryption | SubPacketHandlerFlags.RequiresAccount)]
        public static void HandleCharacterDelete(LobbySession session, SubPacket subPacket)
        {
            // TODO
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientEnterWorld, SubPacketHandlerFlags.RequiresEncryption | SubPacketHandlerFlags.RequiresAccount)]
        public static void HandleClientEnterWorld(LobbySession session, ClientEnterWorld enterWorld)
        {
            session.Sequence = enterWorld.Sequence;

            var character = session.Characters.SingleOrDefault(c => c.Id == enterWorld.CharacterId);
            if (character == null)
                return;

            var realmInfo = AssetManager.GetRealmInfo(character.CurrentRealmId);
            if (realmInfo == null)
                return;

            session.NewEvent(new DatabaseEvent(DatabaseManager.DataCentre.CreateCharacterSession(character.Id, session.Remote.ToString()), () =>
            {
                session.Send(new ServerEnterWorld
                {
                    Sequence    = session.Sequence,
                    ActorId     = character.ActorId,
                    CharacterId = enterWorld.CharacterId,
                    Token       = "",
                    Host        = realmInfo.Host,
                    Port        = realmInfo.Port
                });
            }));
        }
    }
}
