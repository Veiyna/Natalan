using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Lumina.Data.Parsing.Layer;
using Shared;
using Shared.Database;
using Shared.Database.Datacentre;
using Shared.Database.Datacentre.Models;
using Shared.Game;
using Shared.Game.Enum;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.Achievement;
using WorldServer.Game.Calc;
using WorldServer.Game.ChatChannel;
using WorldServer.Game.ContentFinder;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;
using WorldServer.Game.FreeCompany;
using WorldServer.Game.Housing;
using WorldServer.Game.Housing.Enums;
using WorldServer.Game.Map;
using WorldServer.Game.Social;
using WorldServer.Manager;
using WorldServer.Network;
using WorldServer.Network.Message;
using Aetheryte = Lumina.Excel.GeneratedSheets.Aetheryte;
using BaseParam = WorldServer.Game.Entity.Enums.BaseParam;
using ClassJob = Shared.Game.Enum.ClassJob;
using OnlineStatus = WorldServer.Game.Entity.Enums.OnlineStatus;
using Role = WorldServer.Game.Entity.Enums.Role;

namespace WorldServer.Game.Entity;

public class Player : Character
{
    public WorldSession Session { get; }
    public ChatSession ChatSession { get; set; }
    public CharacterInfo Character { get; }
    public Inventory Inventory { get; }
    public EventManager Event { get; }
    public AchievementManager Achievement { get; }
    public Party Party { get; private set; }

    private readonly List<SocialInviteRequest> socialInviteLookup = [];
    public bool IsLogin { get; set; } = true;
    public bool IsLoading { get; set; } = true;
    public LandFlagSet[] LandFlagSets = new LandFlagSet[6];

    public bool IsFalling { get; set; }
    public List<Player> PartyPlayers => Party?.Players ?? [this];

    private readonly QueuedCounter<byte> spawnIndex = new(0);
    private readonly Dictionary<uint, byte> spawnIndexLookup = new(byte.MaxValue);

    public BitArray OnlineMask = new(8 * 8, false);

    public BitArray StateMask = new(8 * 8, false);

    public byte CurrentPose;

    public LandIdent ActiveLand;

    public TeleportQuery CachedTeleportLocation;
    
    public byte[] JobGauge = new byte[15];

    public ZoneingType ZoneingType = ZoneingType.None;

    public Role Role => (Role)GameTableManager.ClassJobs.GetRow(Character.ClassJobId).Role;

    public new byte ClassJobId => Character.ClassJobId;
    public new ushort Level => Character.Class.Level;

    public Shared.Database.Datacentre.Models.FreeCompany FreeCompany => FreeCompanyManager.GetFreeCompanyForPlayer(Character.Id);

    public Dictionary<Character,uint> HateList = new();
    
    private WorldPosition pendingTeleportPosition;

    public Player(WorldSession session, CharacterInfo character)
        : base(character.ActorId, ActorType.Player)
    {
        Session = session;
        Character = character;
        Inventory = new Inventory(this, character.Appearance.Race, character.Appearance.Sex, (ClassJob)character.ClassJobId);
        Event = new EventManager(this);
        Achievement = new AchievementManager(this);
        Position = new WorldPosition(character.TerritoryId, new Vector3(character.LocationX, character.LocationY, character.LocationZ), 0f);
        Session.Version = DataManager.GetVersion(DatabaseManager.Authentication.GetAccountGameVersion(character.AccountId));
        State = (ActorStatus)this.Character.State;
        StateParam = this.Character.StateParam;
        for (int i = 0; i < this.LandFlagSets.Length; i++)
        {
            ref var flagSet = ref this.LandFlagSets[i];
            flagSet.landIdent.LandId = ushort.MaxValue;
            flagSet.landIdent.TerritoryId = ushort.MaxValue;
            flagSet.landIdent.WorldId = ushort.MaxValue;
            flagSet.landIdent.WardNumber = ushort.MaxValue;
        }
    }

    public override bool AddVisibleActor(Actor actor)
    {
        if (!base.AddVisibleActor(actor))
            return false;

        // inital territory spawns are sent in bulk from packet handler after territory loading
        if (!IsLoading)
            SendActor(actor);

        return true;
    }

    public override void RemoveVisibleActor(Actor actor)
    {
        Session.Send(new ServerActorDespawn
        {
            ActorId = actor.Id,
            SpawnId = GetSpawnIndex(actor.Id)
        });

        if (this.spawnIndexLookup.Remove(actor.Id, out byte index))
        {
            spawnIndex.EnqueueValue(index);
        }

        base.RemoveVisibleActor(actor);
    }

    private byte GetSpawnIndex(uint actorId)
    {
        // local actor always has an index of 0
        if (actorId == Id)
            return 0;

        if (!spawnIndexLookup.TryGetValue(actorId, out byte index))
        {
            index = spawnIndex.DequeueValue();
            spawnIndexLookup.Add(actorId, index);
        }

        return index;
    }

    public void SendActor(Actor actor)
    {
        if (actor.IsPlayer)
        {
            Player player = actor.ToPlayer;
            var directorId = 0u;
            if(player.Map.Director != null)
                directorId = player.Map.Director.DirectorId;
            (ulong MainHand, ulong OffHand) weaponDisplayId = player.Inventory.GetWeaponDisplayIds();
            Session.Send(actor.Id, Id,
                new ServerPlayerSpawn
                {
                    SpawnIndex = GetSpawnIndex(actor.Id),
                    Position = player.Position,
                    Character = player.Character,
                    MainHandDisplayId = weaponDisplayId.MainHand,
                    OffHandDisplayId = weaponDisplayId.OffHand,
                    VisibleItemDisplayIds = player.Inventory.GetVisibleItemDisplayIds(),
                    VisibleSecondDyeIds = player.Inventory.GetSecondDyeIds(),
                    TargetId = player.targetId,
                    Stance = player.Stance,
                    OnlineStatus = player.GetApplicableStatus(),
                    CurrentPose = player.CurrentPose,
                    MaxHP = player.MaxHP,
                    MaxMP = player.MaxMP,
                    HP = player.HP,
                    MP = player.MP,
                    State = (byte)player.State,
                    StateParam = player.StateParam,
                    StatusEffects = player.StatusEffects,
                    FreeCompany = player.FreeCompany,
                    DirectorId = directorId
                });
        }
        else if (actor.IsNpc)
        {
            BNpc bNpc = actor.ToBNpc;
            Session.Send(actor.Id, Id,
                new ServerNpcSpawn
                {
                    SpawnIndex = GetSpawnIndex(actor.Id),
                    BNpc = bNpc
                });
        }
        else if (actor.IsEObj)
        {
            EventObject eObj = actor.ToEObj;
            Session.Send(actor.Id, Id,
                new ServerObjectSpawn
                {
                    SpawnIndex = GetSpawnIndex(actor.Id),
                    EventObject = eObj
                });
        }
    }

    public void OnLogin()
    {
        Debug.Assert(IsLogin);

        Achievement.CheckAchievements();

        Character.Progression.EnsureSize();
        AddOnlineStatus(OnlineStatus.Online);



        switch (this.Character.StateParam)
        {
            case 1:
                this.CurrentPose = (byte)(this.Character.Pose[3] + 48);
                break;
            default:
                this.CurrentPose = this.Character.Pose[0];
                break;
        }
        
        ChatSession.Version = DataManager.GetVersion(DatabaseManager.Authentication.GetAccountGameVersion(Character.AccountId));
        
        Session.Send(new ServerContentFinderList
        {
            // Unlock all contents for now
            Contents = new BitArray(0x48 * 8, true)
        });

        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.SetCharaGearParamUI,
            Parameter1 = Character.EquipDisplayFlags,
            Parameter2 = 1,
        });

        Session.Send(new ServerPlayerSetup
        {
            Character = Character
        });

        SendStats();
        this.HP = MaxHP;
        this.MP = MaxMP;
        SendClassInfo();


        // MOTD
        Session.Send(new ServerMessage
        {
            Message = "Welcome to hell v2 :)"
        });

        Session.Send(new ServerMessage
        {
            Message = $"Current client version: {Session.Version.Version}"
        });

        // TODO: client hangs without these sent
        Session.Send(new ServerUnknown01FB());
        Session.Send(new ServerUnknown01FD());

        Session.Send(new ServerQuestJournalActiveList
        {
            Quests = Character.Quests
        });

        Session.Send(new ServerQuestJournalCompleteList
        {
            QuestMask = Character.Progression.Quest
        });

        SendQuestTracker();

        SendMSQTracker();

        SendJobGauge();
        
        SendGCData();
        
        
        
        
        
    }

    public void SavePlayerData()
    {
        Character.State = (byte)State;
        Character.StateParam = StateParam;
        Character.TerritoryId = Position.TerritoryId;
        Character.LocationX = Position.Offset.X;
        Character.LocationY = Position.Offset.Y;
        Character.LocationZ = Position.Offset.Z;

        Character.FlagsCu &= ~PlayerFlagsCu.FirstLogin;

        Session.NewEvent(new DatabaseEvent(DatabaseManager.DataCentre.Save(c =>
        {
            var filter = MongoDB.Driver.Builders<CharacterInfo>.Filter
                .Eq(r => r.Id, Character.Id);

            Inventory.Save(c);
            c.Characters.ReplaceOne(filter, Character);

        }), () => { }));

        Console.WriteLine($"Saving {Character.Id}:{Character.Name} successful.");
    }
    public void OnLogout()
    {
        RemoveFromMap();
        SavePlayerData();
        DeclineSocialInvites();
        ChatChannelManager.RemoveFromChannel(AssetManager.NoviceNetworkChannel, this);
        foreach (var VARIABLE in PartyPlayers)
        {
            
        }
        if (Party is not null)
        {
            Party.MemberLeave(this);
            foreach (var player in Party.Players)
            {
                ContentFinderManager.Withdraw(player);
            }
        }
        else
        {
            ContentFinderManager.Withdraw(this);
        }
    }



    public override void OnAddToMap()
    {
        // opening maps are unique phased
        if (Map.Entry.TerritoryIntendedUse == 6)
            SetPhase(Character.ActorId);

        base.OnAddToMap();
        pendingTeleportPosition = null;

        Session.Send(new ServerNewWorld
        {
            ActorId = Character.ActorId
        });
        
        Inventory.Send();
        
        SendHousingAccess();
        
        Session.Send(new ServerTerritorySetup
        {
            WeatherId = 1,
            WorldPosition = Position,
            Territory = Map
        });
        
        Map.OnZoneIn(this);
    }

    public void SendHousingAccess()
    {
        Session.Send(new ServerHousingLandFlags
        {
            LandFlagSets = this.LandFlagSets
        });
    }

    public override void OnRemoveFromMap()
    {
        base.OnRemoveFromMap();

        if (pendingTeleportPosition != null)
        {
            // remove phasing when leaving an opening map
            if (Map.Entry.TerritoryIntendedUse == 6)
                SetPhase(0u);

            Position = pendingTeleportPosition;
            IsLoading = true;
            AddToMap();
        }
    }

    public void SendVisible()
    {
        foreach (Actor actor in visibleActors)
            SendActor(actor);
    }

    public void SendClassInfo()
    {
        Session.Send(new ServerClassSetup
        {
            ClassJobId = Character.ClassJobId,
            Level = Character.Class.Level
        });
    }

    public void ChangePos(WorldPosition position)
    {
        Relocate(position);
        Session.Send(new ServerActorSetPos
        {
            Position = position
        });
    }
    /// <summary>
    /// Teleport player to a new world position.
    /// </summary>
    public void TeleportTo(WorldPosition newPosition)
    {
        if (pendingTeleportPosition != null)
            return;

        if (!MapManager.CheckInstance(newPosition.TerritoryId, newPosition.InstanceId))
        {
            newPosition.TerritoryId = 132;
            newPosition.InstanceId = 0;
            newPosition.Offset = new Vector3(0, 0, 0);
            Console.WriteLine("Teleporting to fallback location, territory does not have an instance");
            sendDebug("Something went wrong while teleporting, you have been placed in a fallback location");
        }
#if DEBUG
        Console.WriteLine($"Teleporting player '{Character.Name}' to Territory: {newPosition.TerritoryId}, "
                          + $"X: {newPosition.Offset.X}, Y: {newPosition.Offset.Y}, Z: {newPosition.Offset.Z} I: {newPosition.InstanceId}");
#endif
        // TODO: validate position
        pendingTeleportPosition = newPosition;

        if (Position.TerritoryId != newPosition.TerritoryId)
        {
            // shows black loading screen with territory name
            /*Session.Send(new ServerTerritoryPending
            {
                TerritoryId  = newPosition.TerritoryId,
                LogMessageId = 0u
            });*/
        }

        Map.RemoveActor(this);
    }

    public void TeleportToPopRange(uint pop)
    {

        var poprange = GameTableManager.GetLGBEntry(LayerEntryType.PopRange, pop);
        if (poprange.Item1.HasValue && poprange.Item2.HasValue)
            TeleportTo(new WorldPosition(poprange.Item1.Value,
                new Vector3(poprange.Item2.Value.Transform.Translation.X, poprange.Item2.Value.Transform.Translation.Y,
                    poprange.Item2.Value.Transform.Translation.Z),
                Utilities.EulerToDirection(new Vector3(poprange.Item2.Value.Transform.Rotation.X, poprange.Item2.Value.Transform.Rotation.Y,
                    poprange.Item2.Value.Transform.Rotation.Z))));
        else
        {
            var levelEntry = GameTableManager.Level.GetRow(pop);
            TeleportTo(new WorldPosition((ushort)levelEntry.Territory.Row, new Vector3(levelEntry.X, levelEntry.Y, levelEntry.Z), levelEntry.Yaw));
        }
    }
    public void AetheryteTeleport(ushort aetheryteId)
    {
        if (this.ZoneingType is not ZoneingType.ReturnDead)
            ZoneingType = ZoneingType.Teleport;

        Aetheryte aetheryte = GameTableManager.Aetheryte.GetRow(aetheryteId);
        TeleportToPopRange(aetheryte.Level[0].Row);
    }

    /// <summary>
    /// Invite player to your party, if no party exists one will be created.
    /// </summary>
    public void PartyInvite(string inviteeName)
    {
        Player invitee = MapManager.FindPlayer(inviteeName);
        if (invitee == null)
            throw new InvalidPlayerException(inviteeName);

        this.Party ??= SocialManager.NewParty(this);

        Party.Invite(this, invitee);
    }

    public void SetParty(Party party)
    {
        if (party != null && Party != null)
            throw new PartyStateException($"Can't assign {Character.Name} to a new party, they haven't left their current one!");

        Party = party;
    }

    public void PartyTeleport(ushort aetheryteId, byte partyIndex)
    {
        this.CachedTeleportLocation.Target = aetheryteId;
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.PartyTeleport,
            Parameter2 = aetheryteId,
            Parameter3 = partyIndex,
        });
    }

    public SocialInviteRequest FindSocialInvite(ulong hostId, SocialType type)
    {
        return socialInviteLookup.SingleOrDefault(s => s.HostId == hostId && s.Type == type);
    }

    public void AddSocialInvite(SocialInviteRequest inviteRequest)
    {
        socialInviteLookup.Add(inviteRequest);
    }

    public void RemoveSocialInvite(ulong hostId, SocialType type)
    {
        SocialInviteRequest inviteRequest = FindSocialInvite(hostId, type);
        if (inviteRequest != null)
            socialInviteLookup.Remove(inviteRequest);
    }

    /// <summary>
    /// Decline all pending social invites of a type, if no type is specified all invites will be declined.
    /// </summary>
    public void DeclineSocialInvites(SocialType type = SocialType.None)
    {
        IEnumerable<SocialInviteRequest> invites = type != SocialType.None ? socialInviteLookup.Where(s => s.Type == type) : socialInviteLookup;
        foreach (SocialInviteRequest inviteRequest in invites)
        {
            SocialBase socialEntity = SocialManager.FindSocialEntity<SocialBase>(inviteRequest.Type, inviteRequest.EntityId);
            socialEntity?.InviteResponse(this, 0);
        }
    }

    public void SetTitle(ushort titleId)
    {
        Character.CurrentTitle = titleId;
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.SetTitle,
            Parameter1 = titleId,
        }, true);
    }


    public bool HasOnlineStatus(OnlineStatus status)
    {
        return this.OnlineMask.Get((byte)status);

    }

    public void AddOnlineStatus(OnlineStatus status)
    {
        this.OnlineMask.Set((byte)status, true);
        SendOnlineStatusUpdate();

    }

    public void RemoveOnlineStatus(OnlineStatus status)
    {
        this.OnlineMask.Set((byte)status, false);
        SendOnlineStatusUpdate();

    }

    public void SetOnlineStatus(BitArray statusMask)
    {
        this.OnlineMask = statusMask;
        SendOnlineStatusUpdate();
    }

    public void SendOnlineStatusUpdate()
    {
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.SetStatusIcon,
            Parameter1 = (uint)GetApplicableStatus(),
        }, true);

        Session.Send(new ServerSetOnlineStatus
        {
            onlineStatus = this.OnlineMask
        });
    }


    public OnlineStatus GetApplicableStatus()
    {
        var statusMask = this.OnlineMask;
        uint statusDisplayOrder = 0xFF14;
        uint applicableStatus = (uint)OnlineStatus.Online;
        for (uint i = 0; i < statusMask.Count; i++)
        {
            if (!statusMask.Get((int)i))
                continue;

            var onlinestatusentry = GameTableManager.OnlineStatus.GetRow(i);
            if (onlinestatusentry == null)
                continue;

            if (onlinestatusentry.Priority < statusDisplayOrder)
            {
                statusDisplayOrder = onlinestatusentry.Priority;
                applicableStatus = i;
            }

        }

        return (OnlineStatus)applicableStatus;
    }



    public void SetPose(byte type, byte pose, bool permanent)
    {
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.SetPose,
            Parameter1 = type,
            Parameter2 = pose,
        });

        if (permanent)
            Character.Pose[type] = pose;

        CurrentPose = pose;
    }

    public void SetLevel(byte level)
    {
        Character.Class.Level = level;
        Character.Class.Experience = 0;
        UpdateClassInfo();
        SendStats();

        this.HP = MaxHP;
        this.MP = MaxMP;
        SendHPUpdate();

    }

    public void SetLevelForClass(ClassJob classJob, byte level)
    {
        var classEntry = GameTableManager.ClassJobs.GetRow((uint)classJob);
        if (classEntry == null) return;
        Character.Classes[classEntry.ExpArrayIndex].Level = level;
        
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.ClassJobUpdate,
            Parameter1 = (uint)classJob,
            Parameter2 = level
        });
    }

    public void SetCompanion(ushort id)
    {
        Character.Companion = id;

        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.ToggleCompanion,
            Parameter1 = id
        }, true);

    }

    public void SeenHowTo(ushort id)
    {
        Character.Progression.HowTo.Set(id, true);
    }

    public void SetAetheryte(byte id, bool state)
    {
        Character.Progression.Aetheryte.Set(id, state);
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.AetheryteUnlock,
            Parameter1 = id,
            Parameter2 = Convert.ToUInt32(state)
        });
    }

    public bool IsAetheryteRegistered(byte id)
    {
        return Character.Progression.Aetheryte[id];
    }

    public bool GetMasterUnlock(ushort id)
    {
        return Character.Progression.MasterMask[id];
    }

    public void SetMasterUnlock(ushort id, bool state = true)
    {
        Character.Progression.MasterMask.Set(id, state);
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.ToggleMasterUnlock,
            Parameter1 = id,
            Parameter2 = Convert.ToUInt32(state)
        });
    }


    public void UpdateClassInfo()
    {
        Session.Send(new ServerUpdateClassInfo
        {
            Player = this,
        });
    }

    public void SetSearchInfo(byte region, string comment)
    {
        Character.SocialInfo.SelectRegion = region;
        Character.SocialInfo.SearchComment = comment;
    }

    public QuestModel GetQuest(ushort questId)
    {
        var quest = this.Character.Quests.FirstOrDefault(quest => quest.QuestId == questId);
        if (quest == null)
        {
            quest = new QuestModel
            {
                QuestId = questId,
                Slot = (ulong)this.Character.Quests.Count,
                Sequence = 0,
            };

            Character.Quests.Add(quest);
            SendMSQTracker();
        }
        
        

        return quest;

    }

    public QuestModel GetQuest(uint questId)
    {
        var actualQuest = Convert.ToUInt16(questId & 0xffff);
        return GetQuest(actualQuest);
    }

    public void UpdateQuest(ushort questId, byte sequence)
    {
        var quest = GetQuest(questId);
        quest.Sequence = sequence;
        SendQuestTracker();
        SendQuestUpdate(quest);
    }

    public void UpdateQuest(ushort questId)
    {
        var quest = GetQuest(questId);
        if (quest.Sequence == 0)
        {
            RemoveQuest(quest);
            return;
        }

        SendQuestTracker();
        SendQuestUpdate(quest);
    }

    public void UpdateQuest(uint questId, byte sequence)
    {
        var actualQuest = Convert.ToUInt16(questId & 0xffff);
        UpdateQuest(actualQuest, sequence);
    }


    public void UpdateQuest(uint questId)
    {
        var actualQuest = Convert.ToUInt16(questId & 0xffff);
        UpdateQuest(actualQuest);
    }

    public void UpdateQuests()
    {
        foreach (var quest in Character.Quests.ToList())
        {
            UpdateQuest(quest.QuestId);
        }
    }

    public void FinishQuest(ushort questId, uint optionalChoice = 0)
    {
        var quest = GetQuest(questId);

        UpdateQuestProgression(questId, true);
        GiveQuestRewards(questId, optionalChoice);
        

        RemoveQuest(quest);
        
        SendMSQTracker();

    }

    public void GiveQuestRewards(ushort questId, uint optionalChoice = 0)
    {
        var questEntry = GameTableManager.Quests.GetRow((uint)(questId + 65536));
        if (questEntry is null)
            return;


        for (int i = 0; i < questEntry.ItemReward.Length; i++)
        {
            var itemId = questEntry.ItemReward[i];
            if (itemId > 0)
                Inventory.NewItem(itemId, questEntry.ItemCountReward[i]);
        }

        for (int i = 0; i < questEntry.OptionalItemReward.Length; i++)
        {
            var itemId = questEntry.OptionalItemReward[i].Row;
            if (itemId > 0 && itemId == optionalChoice)
                Inventory.NewItem(itemId, questEntry.OptionalItemCountReward[i]);
        }

        var paramGrowth = GameTableManager.ParamGrow.GetRow((uint)(questEntry.ClassJobLevel0 + questEntry.QuestLevelOffset));
        var exp = (uint)(questEntry.ExpFactor * paramGrowth.ScaledQuestXP * paramGrowth.QuestExpModifier / 100);

        if (exp > 0)
            GainExp(exp);

        uint gil = questEntry.GilReward;
        AddCurrency(CurrencyType.Gil, gil);

        var currencyItemId = questEntry.CurrencyReward.Row;
        var currencyAmount = questEntry.CurrencyRewardCount;

        if (currencyItemId != 0)
        {
            AssetManager.ItemToCurrency.TryGetValue(currencyItemId, out var currencyType);
            if(currencyType != 0)
                AddCurrency(currencyType, currencyAmount);
        }



    }

    public void FinishQuest(uint questId, uint optionalChoice = 0)
    {
        var actualQuest = Convert.ToUInt16(questId & 0xffff);
        FinishQuest(actualQuest, optionalChoice);

    }

    public void UpdateQuestProgression(ushort questId, bool completed)
    {
        Session.Send(new ServerQuestFinish
        {
            QuestId = questId,
            Flag1 = Convert.ToByte(completed),
            Flag2 = 1,
        });

        Character.Progression.Quest.Set(questId / 8 * 8 + (7 - questId % 8), completed);
    }


    public void RemoveQuest(QuestModel quest)
    {
        SendQuestUpdate(new QuestModel
        {
            Slot = quest.Slot
        });

        Character.Quests.Remove(quest);

        for (int i = 0; i < Character.Quests.Count; i++)
        {
            var someQuest = Character.Quests[i];
            someQuest.Slot = (ulong)i;
        }

    }

    public void RemoveQuest(ushort questId)
    {
        var quest = GetQuest(questId);
        SendQuestUpdate(new QuestModel
        {
            Slot = quest.Slot
        });

        Character.Quests.Remove(quest);

    }

    public void SendMSQTracker()
    {
        foreach (var quest in Character.Quests)
        {
            var scenarioTree = GameTableManager.ScenarioTree.GetRow((uint)(quest.QuestId + 65536));
            if (scenarioTree is not null)
            {
                Session.Send(new ServerMSQTrackerProgress
                {
                    Id = quest.QuestId
                });

                return;
            }
        }
    }

    public void createAndJoinQuestBattle(uint Id)
    {
        var questbattle = GameTableManager.QuestBattle.GetRow(Id);
        var questId = questbattle.Quest;
        if (questId == 854246)
        {
            questId = 69398;
        }
        if(questId > 66216)
            GetQuest((uint)questId).Sequence = 255;
        else
            GetQuest((uint)questId).Sequence += 1;
        TeleportTo(Position);
        sendDebug("Quest battles not implemented :(");
    }



    public void SendQuestUpdate(QuestModel quest)
    {
        Session.Send(new ServerQuestUpdate
        {
            Quest = quest
        });


    }


    public void SendQuestTracker()
    {
        Session.Send(new ServerQuestTracker
        {
            Quests = Character.Quests
        });


    }

    public void SetNewAdventurer(bool state)
    {
        if (state)
        {
            Character.IsNewAdventurer = true;
            AddOnlineStatus(OnlineStatus.NewAdventurer);
            Session.Send(new ServerActorAction
            {
                Action = ActorActionServer.SetNewAdventurer,
            });

            Session.Send(new ServerSystemLogMessage
            {
                Id = 3817,
                Parameter1 = 1,
                Parameter5 = 1
            });
        }
        else
        {
            Character.IsNewAdventurer = false;
            RemoveOnlineStatus(OnlineStatus.NewAdventurer);
            Session.Send(new ServerActorAction
            {
                Action = ActorActionServer.SetNewAdventurer,
                Parameter2 = 1
            });

            Session.Send(new ServerSystemLogMessage
            {
                Id = 3817,
                Parameter1 = 1
            });
        }
    }

    public void SendStats()
    {
        CalculateBaseStats();
        Session.Send(new ServerPlayerStats
        {
            Stats = this.Stats
        });

        Session.Send(new ServerActorAction
        {
            Action = ActorActionServer.SetItemLevel,
            Parameter1 = 999
        });
    }
    public void CalculateBaseStats()
    {
        var level = Character.Class.Level;
        var classInfo = GameTableManager.ClassJobs.GetRow(Character.ClassJobId);
        var tribeInfo = GameTableManager.Tribe.GetRow(Character.Appearance.Clan);
        var paramGrowth = GameTableManager.ParamGrow.GetRow(level);


        var mainStat = CalcManager.levelTable[level, (uint)LevelTableEntry.MAIN];
        var mainHP = CalcManager.levelTable[level, (uint)LevelTableEntry.HP];

        SetBaseStat(BaseParam.Strength, (uint)(mainStat * ((float)classInfo.ModifierStrength / 100) + tribeInfo.STR));
        SetBaseStat(BaseParam.Dexterity, (uint)(mainStat * ((float)classInfo.ModifierDexterity / 100) + tribeInfo.DEX));
        SetBaseStat(BaseParam.Vitality, (uint)(mainStat * ((float)classInfo.ModifierVitality / 100) + tribeInfo.VIT));
        SetBaseStat(BaseParam.Intelligence, (uint)(mainStat * ((float)classInfo.ModifierIntelligence / 100) + tribeInfo.INT));
        SetBaseStat(BaseParam.Mind, (uint)(mainStat * ((float)classInfo.ModifierMind / 100) + tribeInfo.MND));

        SetBaseStat(BaseParam.Determination, mainStat);
        SetBaseStat(BaseParam.Piety, mainStat);

        SetBaseStat(BaseParam.SkillSpeed, (uint)paramGrowth.BaseSpeed);
        SetBaseStat(BaseParam.SpellSpeed, (uint)paramGrowth.BaseSpeed);

        SetBaseStat(BaseParam.Haste, 100);


        SetBaseStat(BaseParam.DirectHitRate, (uint)paramGrowth.BaseSpeed);
        SetBaseStat(BaseParam.CriticalHit, (uint)paramGrowth.BaseSpeed);
        SetBaseStat(BaseParam.AttackMagicPotency, (uint)paramGrowth.BaseSpeed);
        SetBaseStat(BaseParam.HealingMagicPotency, (uint)paramGrowth.BaseSpeed);

        SetBaseStat(BaseParam.Tenacity, (uint)paramGrowth.BaseSpeed);

        SetBaseStat(BaseParam.AttackPower, GetBaseStat(BaseParam.Strength));
        SetBaseStat(BaseParam.AttackMagicPotency, GetBaseStat(BaseParam.Intelligence));
        SetBaseStat(BaseParam.HealingMagicPotency, GetBaseStat(BaseParam.Mind));

        var hpMod = paramGrowth.HpModifier;

        SetBaseStat(BaseParam.HP,
            (uint)(classInfo.ModifierHitPoints * (hpMod / (float)100) + (hpMod / (float)100 * GetBaseStat(BaseParam.Vitality) - mainStat)));

        SetBaseStat(BaseParam.MP, 10000);

        if (HP > MaxHP)
            HP = MaxHP;

        if (MP > MaxMP)
            MP = MaxMP;


    }

    public void SendStateFlags()
    {
        Session.Send(new ServerPlayerStateFlags
        {
            StateMask = this.StateMask
        });
    }
    public void SetStateFlag(PlayerStateFlag flag)
    {
        this.StateMask.Set((byte)flag, true);
        SendStateFlags();
    }

    public void UnsetStateFlag(PlayerStateFlag flag)
    {
        this.StateMask.Set((byte)flag, false);
        SendStateFlags();
    }


    public void ReturnHomepoint()
    {
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.ActorDespawnEffect,
            Parameter1 = 3,
        });

        if (Character.Homepoint == 0) Character.Homepoint = 8;
        AetheryteTeleport(Character.Homepoint);
    }

    public void Mount(uint id)
    {
        if (id > 0)
        {
            SetState(ActorStatus.Mounted);
            SendMessageToVisible(new ServerActorActionSelf
            {
                Action = ActorActionServer.SetMountSpeed,
                Parameter1 = 20,
            }, true);

            SendMessageToVisible(new ServerMount
            {
                Id = id,
                Color = (ushort)(id == 1 ? this.Character.CompanionInfo.Color : 0)
            }, true);

            SendMessageToVisible(new ServerActorActionSelf
            {
                Action = ActorActionServer.MountEnable1,
                Parameter1 = 1,
            }, true);

            Character.Mount = id;
        }
    }

    public void Dismount()
    {
        SetState(ActorStatus.Idle);

        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.Dismount,
            Parameter1 = 1,
        }, true);

        SendMessageToVisible(new ServerActorActionSelf
        {
            Action = ActorActionServer.MountEnable2,
            Parameter1 = 1,
        }, true);

        SendMessageToVisible(new ServerActorActionSelf
        {
            Action = ActorActionServer.MountEnable1,
        }, true);

        Character.Mount = 0;

    }
    
    public void ClearJobGauge()
    {
        Array.Clear(JobGauge);
        SendJobGauge();
    }
    
    public void SendJobGauge()
    {
        Session.Send(new ServerJobGauge
        {
            Player = this,
        });
    }
    
    public void SetHomepoint(byte aetheryteId)
    {
        Character.Homepoint = aetheryteId;

        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.SetHomepoint,
            Parameter1 = aetheryteId,
        });
    }

    public void SendQuestMessage(uint eventId, byte msgId, byte type = 0, uint var1 = 0, uint var2 = 0)
    {
        Session.Send(new ServerQuestMessage
        {
            QuestId = eventId,
            MsgId = msgId,
            Type = type,
            Var1 = var1,
            Var2 = var2
        });
    }

    public void ChangeClass(byte classJob)
    {
        Character.ClassJobId = classJob;

        SendClassInfo();
        SendJobGauge();

        SendStats();
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.ClassJobChange,
            Parameter1 = classJob
        }, true);

        SendHPUpdate();
    }


    public void sendDebug(string message)
    {
        Session.Send(new ServerMessage
        {
            Message = message
        });
    }

    public void sendUrgent(string message)
    {
        Session.Send(new ServerMessage
        {
            Message = message
        });
    }

    public void enterPredefinedPrivateInstance(uint zoneId)
    {
        TeleportTo(new WorldPosition((ushort)zoneId, Vector3.Zero, 0f));
    }


    public void GainExp(uint amount)
    {
        var level = Character.Class.Level;

        if (level >= 90)
        {
            Character.Class.Experience = 0;
            return;
        }

        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.GainExpMsg,
            Parameter1 = ClassJobId,
            Parameter2 = amount,
        });

        CheckLevelUp(amount);

    }


    private void CheckLevelUp(uint amount)
    {
        int requiredExp = GameTableManager.ParamGrow.GetRow(Level).ExpToNext;
        if (Character.Class.Experience + amount >= requiredExp)
        {
            var amountTakenToLevelUp = (uint)(requiredExp - this.Character.Class.Experience);
            amount -= amountTakenToLevelUp;
            GainLevel();
            CheckLevelUp(amount);
        }
        else
        {
            if (Level != 90)
            {
                Character.Class.Experience += amount;
                Session.Send(new ServerActorActionSelf
                {
                    Action = ActorActionServer.UpdateUiExp,
                    Parameter1 = ClassJobId,
                    Parameter2 = Character.Class.Experience
                });
            }
        }
    }

    public void GainLevel()
    {
        if (Level == 90)
            return;

        SetLevel((byte)(this.Level + 1));

        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.LevelUpEffect,
            Parameter1 = ClassJobId,
            Parameter2 = Level,
            Parameter3 = (uint)(this.Level - 1),
        }, true);

    }

    public void SendFreeCompany()
    {
        Session.Send(new ServerFreeCompanyInfo
        {
            FreeCompany = this.FreeCompany
        });
    }

    public uint GetCurrency(CurrencyType currencyType)
    {
        var slot = (byte)(currencyType - 1);
        var item = Inventory.GetItemAtSlot(ContainerType.Currency, slot);
        if (item == null)
            return 0;

        return item.StackSize;
    }

    public void AddCurrency(CurrencyType currencyType, uint amount, bool sendLootMsg = true)
    {
        if (amount == 0)
            return;
        var slot = (byte)(currencyType - 1);
        var item = Inventory.GetItemAtSlot(ContainerType.Currency, slot);

        if (item == null)
        {
            Inventory.NewItem(ContainerType.Currency, AssetManager.CurrencyToItem[currencyType], slot, amount);
        }
        else
        {
            item.UpdateStackSize(amount);
        }


        if (!sendLootMsg)
        {
            return;
        }

        switch (currencyType)
        {
            case CurrencyType.Gil:
            {
                this.Session.Send(new ServerLootMessage
                {
                    MessageType = LootMessageType.GetGil,
                    Var1 = amount,
                });

                break;
            }

            case CurrencyType.StormSeal:
            case CurrencyType.SerpentSeal:
            case CurrencyType.FlameSeal:
            {
                this.Session.Send(new ServerActorActionSelf
                {
                    Action = ActorActionServer.LogMsg,
                    Parameter1 = 1300,
                    Parameter2 = (uint)(currencyType - 1),
                    Parameter3 = amount
                });
                break;
            }
        }
    }

    public void RemoveCurrency(CurrencyType currencyType, uint amount)
    {
        if (amount == 0)
            return;
        var slot = (byte)(currencyType - 1);
        var item = Inventory.GetItemAtSlot(ContainerType.Currency, slot);
        if (item == null)
            return;
        var currentAmount = item.StackSize;
        if (amount > currentAmount)
        {
            item.UpdateStackSize(0);
        }
        else
        {
            item.UpdateStackSize(amount, false);  
        }
    }

    public void OnMobAggro(BNpc bnpc)
    {
        HateListAdd(bnpc);
        SetStateFlag(PlayerStateFlag.InCombat);
    }
    
    public void OnMobDeAggro(BNpc bnpc)
    {
        HateListRemove(bnpc);
        UnsetStateFlag(PlayerStateFlag.InCombat);
    }

    public void HateListAdd(BNpc bnpc)
    {
        this.HateList.TryAdd(bnpc, 1);
        SendHateList();
    }
    
    public void HateListRemove(BNpc bnpc)
    {
        this.HateList.Remove(bnpc);
        SendHateList();
    }
    public void SendHateList()
    {
        Session.Send(new ServerHateList
        {
            HateList = this.HateList
        });
        Session.Send(new ServerHateRank
        {
            HateList = this.HateList
        });
    }

    public void OnMobKill(BNpc bnpc)
    {
        Event.OnBNpcKill(bnpc);
    }

    public void SetMountUnlock(uint mountId, bool unlocked = true)
    {
        var mount = GameTableManager.Mount.GetRow(mountId);
        if (mount == null) return;
        if (mount.Order == -1 || mount.ModelChara.Row == 0) return;
        var mountOrderId = mount.Order;
        Character.Progression.Mount.Set(mount.Order, unlocked);
        
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.SetMountBitmask,
            Parameter1 = (uint)mountOrderId,
            Parameter2 = Convert.ToUInt32(unlocked)
            
        });
    }

    public void SetDutyUnlock(uint id, bool unlocked = true)
    {
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.DutyUnlock,
            Parameter1 = id,
            Parameter2 = Convert.ToUInt32(unlocked),
        });
    }

    public void SetGC(byte grandCompanyId)
    {
        Character.GrandCompany = grandCompanyId;
        if (grandCompanyId != 0)
        {
            ref var gcRank = ref Character.GrandCompanyRanks[grandCompanyId - 1];
            if (gcRank == 0)
                gcRank = 1;
            
        }
        SendGCData();
    }

    public void SetGCRank(byte rank)
    {
        Character.GrandCompanyRanks[Character.GrandCompany - 1] = rank;
        SendGCData();
    }

    public void SendGCData()
    {
        Session.Send(new ServerGCAffiliation
        {
            GCId = Character.GrandCompany,
            GCRanks = Character.GrandCompanyRanks
        });
    }
    
    public void SetCompanionName(string name)
    {
        Character.CompanionInfo.Name = name;
        Session.Send(new ServerCompanionName
        {
            Name = name
        });
    }

    public void SendLogMessage(uint messageId, uint param2 = 0 ,uint param3 = 0 ,uint param4 = 0,uint param5 = 0,uint param6 =0 )
    {
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.LogMsg,
            Parameter1 = messageId,
            Parameter2 = param2,
            Parameter3 = param3,
            Parameter4 = param4,
            Parameter5 = param5,
            Parameter6 = param6
        });
    }

    public void SetEurekaStep(uint value)
    {
        Character.EurekaInfo.EurekaStep = value;
        Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.EurekaStep,
            Parameter1 = value,
            Parameter2 = 0x3C,
            Parameter3 = 0xFFFFE3,
        });
        sendDebug($"Eureka Step set to {value}");
    }


    public void SetHousingAccess(LandFlagsSlot slot, LandFlags flags, LandIdent landIdent)
    {
        ref var flagSet = ref this.LandFlagSets[(int)slot];
        flagSet.landFlags = flags;
        flagSet.landIdent = landIdent;

        SendHousingAccess();
    }
    

}