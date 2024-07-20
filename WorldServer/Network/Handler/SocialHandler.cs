using System;
using System.Collections;
using Shared.Network;
using WorldServer.Game.FreeCompany;
using WorldServer.Game.Map;
using WorldServer.Game.Social;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class SocialHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientPartyLeave, SubPacketHandlerFlags.RequiresWorld | SubPacketHandlerFlags.RequiresParty)]
        public static void HandleSocialLeave(WorldSession session, ClientPartyLeave partyLeave)
        {
            session.Player.Party.MemberLeave(session.Player);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientPartyDisband, SubPacketHandlerFlags.RequiresWorld | SubPacketHandlerFlags.RequiresParty)]
        public static void HandleSocialDisband(WorldSession session, ClientPartyDisband partyDisband)
        {
            session.Player.Party.MemberDisband(session.Player);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientPartyKick, SubPacketHandlerFlags.RequiresWorld | SubPacketHandlerFlags.RequiresParty)]
        public static void HandlePartyClientPartyKick(WorldSession session, ClientPartyKick partyKick)
        {
            session.Player.Party.MemberKick(session.Player, partyKick.Name);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientPartyPromote, SubPacketHandlerFlags.RequiresWorld | SubPacketHandlerFlags.RequiresParty)]
        public static void HandlePartyClientPartyPromote(WorldSession session, ClientPartyPromote partyPromote)
        {
            session.Player.Party.MemberPromote(session.Player, partyPromote.Name);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientSocialList, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleSocialList(WorldSession session, ClientSocialList socialList)
        {
            switch (socialList.ListType)
            {
                case SocialListType.Party:
                {
                    // party
                    if (session.Player.Party != null)
                        session.Player.Party.SendSocialList(session.Player, socialList.Sequence);
                    // solo
                    else
                    {
                        ServerSocialList serverSocialList = new ServerSocialList
                        {
                            ListType = SocialListType.Party,
                            Sequence = socialList.Sequence
                        };

                        serverSocialList.SocialMembers.Add(new ServerSocialList.Member(session.Player));
                        session.Send(serverSocialList);
                    }
                    break;
                }
                case SocialListType.Friend: // All players on the server on the friend list, why not
                {
                    ServerSocialList serverSocialList = new ServerSocialList
                    {
                        ListType = SocialListType.Friend,
                        Sequence = socialList.Sequence
                    };

                    foreach (var player in MapManager.GetPlayers())
                    {
                        if (player != session.Player)
                            serverSocialList.SocialMembers.Add(new ServerSocialList.Member(player));
                    }
                    
                    session.Send(serverSocialList);
                    break;
                }
                case SocialListType.FC:
                {
                    ServerSocialList serverSocialList = new ServerSocialList
                    {
                        ListType = SocialListType.FC,
                        Sequence = socialList.Sequence
                    };

                    foreach (var player in MapManager.GetFreeCompanyPlayers(session.Player.FreeCompany.Id))
                    {
                        serverSocialList.SocialMembers.Add(new ServerSocialList.Member(player));
                    }
                    
                    session.Send(serverSocialList);
                    break;
                }
                default:
                {
                    #if DEBUG
                        Console.WriteLine($"Unhandled SocialListType {socialList.ListType}");
                    #endif
                    break;
                }
            }
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientSocialInvite, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleSocialInvite(WorldSession session, ClientSocialInvite socialInvite)
        {
            switch (socialInvite.SocialType)
            {
                case SocialType.Party:
                    session.Player.PartyInvite(socialInvite.Invitee);
                    break;
                default:
                    throw new NotImplementedException($"Unhandled SocialType {socialInvite.SocialType}!");
            }
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientSocialInviteResponse, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleSocialInviteResponse(WorldSession session, ClientSocialInviteResponse socialInviteResponse)
        {
            SocialInviteRequest inviteRequest = session.Player.FindSocialInvite(socialInviteResponse.CharacterId, socialInviteResponse.SocialType);
            if (inviteRequest == null)
                throw new SocialInviteStateException($"Character {session.Player.Character.Id} doesn't have a pending {socialInviteResponse.SocialType} invite!");

            SocialBase socialEntity = SocialManager.FindSocialEntity<SocialBase>(socialInviteResponse.SocialType, inviteRequest.EntityId);
            socialEntity?.InviteResponse(session.Player, socialInviteResponse.Result);
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientSocialBlackList, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleBlackList(WorldSession session, ClientSocialBlackList blacklist)
        {
            session.Send(new ServerSocialBlacklist
            {
                Sequence = blacklist.Sequence,
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientExamineSearchComment, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleExamineSearchComment(WorldSession session, ClientExamineSearchComment searchComment)
        {
            var target = MapManager.FindPlayer(searchComment.ActorId);
            session.Send(new ServerExamineSearchComment
            {
                ActorId = searchComment.ActorId,
                SearchComment = target.Character.SocialInfo.SearchComment
            });
        }
        
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientExamineFreeCompanyInfo, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleExamineFreeCompanyInfo(WorldSession session, ClientExamineFreeCompanyInfo fcInfo)
        {
            var target = MapManager.FindPlayer(fcInfo.ActorId);
            if (target is null)
            {
                target = MapManager.FindPlayer(fcInfo.FreeCompanyOrPlayerId); 
            }

            var freeCompany = target?.FreeCompany ?? FreeCompanyManager.GetFreeCompanyByID(fcInfo.FreeCompanyOrPlayerId);

                

            session.Send(new ServerExamineFreeCompanyInfo
            {
                Player = target,
                FreeCompany = freeCompany,
            });

        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientInitSearchInfo, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleInitSearchInfo(WorldSession session, SubPacket searchinfo)
        {
            session.Send(new ServerInitSearchInfo
            {
                OnlineStatus = session.Player.OnlineMask,
                SocialInfo = session.Player.Character.SocialInfo
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientSetSearchInfo, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleSetSearchInfo(WorldSession session, ClientSetSearchInfo searchinfo)
        {
            session.Player.SetOnlineStatus(new BitArray(BitConverter.GetBytes(searchinfo.StatusMask)));
            session.Player.SetSearchInfo(searchinfo.Language, searchinfo.SearchComment);
            session.Send(new ServerUpdateSearchInfo
            {
                OnlineStatus = session.Player.OnlineMask,
                SocialInfo = session.Player.Character.SocialInfo,
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientViewSearchInfo, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleViewSearchInfo(WorldSession session, ClientViewSearchInfo searchinfo)
        {
            var TargetPlayer = MapManager.FindPlayer(searchinfo.CharacterId);
            session.Send(new ServerViewSearchInfo
            {
                Character = TargetPlayer.Character,
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientAdventurerPlateView, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleAdventurerPlateView(WorldSession session, ClientAdventurerPlateView plateView)
        {
            var TargetPlayer = MapManager.FindPlayer(plateView.Id);
            if(plateView.IsActorId == 1)
                TargetPlayer = MapManager.FindPlayer((uint)plateView.Id);

            if (TargetPlayer is null)
            {
                session.Send(new ServerErrorMessage
                {
                    Id = 5855
                });

                return;
            }
            
            if (TargetPlayer != session.Player && TargetPlayer.Character.AdventurerPlate.RawData is not null && TargetPlayer.Character.AdventurerPlate.RawData[66] == 2)
            {
                session.Send(new ServerErrorMessage
                {
                    Id = 5859
                });

                return;
            }
            session.Send(new ServerAdventurerPlateView
            {
                Character = TargetPlayer.Character,
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientAdventurerPlateEdit, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleAdventurerPlateEdit(WorldSession session, ClientAdventurerPlateEdit plateEdit)
        {
            session.Player.Character.AdventurerPlate.RawData = plateEdit.PlateData;
            session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.AdventurerPlateEditResponse,
                Parameter2 = plateEdit.Type,
            });
        }
        
    }
}
