using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Shared.Database.Datacentre.Models;
using Shared.Game;
using Shared.Network;
using Shared.SqPack;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Housing;
using WorldServer.Game.Map;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class ActorActionHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientActorAction, SubPacketHandlerFlags.RequiresWorld)]
        [SubPacketHandler(SubPacketClientHandlerId.ClientActorActionEnvironment, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleActorAction(WorldSession session, ClientActorAction actorAction)
        {
            Console.WriteLine($"Got Actor Action: {actorAction.Action}(0x{actorAction.Action:X}), {actorAction.Parameters[0]}, "
                              + $"{actorAction.Parameters[1]}, {actorAction.Parameters[2]}, {actorAction.Parameters[3]}, {actorAction.Position}");

            ActorActionManager.Invoke(session, actorAction);
        }

        [ActorActionHandler(ActorActionClient.Action00C9)]
        public static void HandleActorAction00C9(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.IsLoading = false;
            session.Player.IsLogin = false;
            switch (session.Player.ZoneingType)
            {
                case ZoneingType.None:
                    session.Player.SendMessageToVisible(new ServerActorActionSelf
                    {
                        Action = ActorActionServer.ZoneIn,
                        Parameter1 = 1,
                    }, true);

                    break;
                case ZoneingType.Teleport:
                    session.Player.SendMessageToVisible(new ServerActorActionSelf
                    {
                        Action = ActorActionServer.ZoneIn,
                        Parameter4 = 110
                    }, true);

                    break;
                case ZoneingType.Return:
                    session.Player.SendMessageToVisible(new ServerActorActionSelf
                    {
                        Action = ActorActionServer.ZoneIn,
                        Parameter4 = 111
                    }, true);

                    break;
                case ZoneingType.ReturnDead:
                    session.Player.Raise();
                    break;
            }

            session.Player.ZoneingType = ZoneingType.None;

        }

        [ActorActionHandler(ActorActionClient.AchievementCriteriaRequest)]
        public static void HandleActorActionAchievementCriteriaRequest(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.Achievement.SendAchievementCriteria(actorAction.Parameters[0]);
        }

        [ActorActionHandler(ActorActionClient.AchievementList)]
        public static void HandleActorActionAchievementList(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.Achievement.SendAchievementList();
        }


        [ActorActionHandler(ActorActionClient.Teleport)]
        public static void HandleTeleport(WorldSession session, ClientActorAction actorAction)
        {
            var aetheryteId = (ushort)actorAction.Parameters[0];
            bool usingTicket = actorAction.Parameters[1] == 1;
            var targetAetheryte = GameTableManager.Aetheryte.GetRow(aetheryteId);

            if (targetAetheryte is not null)
            {
                var currentAetheryte = session.Player.Map.Entry.Aetheryte.Value;
                var cost = (ushort)(Math.Sqrt(Math.Pow(currentAetheryte.AetherstreamX - targetAetheryte.AetherstreamX, 2) +
                                              Math.Pow(currentAetheryte.AetherstreamY - targetAetheryte.AetherstreamY, 2)) / 2 + 100);


                if (usingTicket) cost = 0;
                bool hasGil = session.Player.GetCurrency(CurrencyType.Gil) > cost;

                session.Send(new ServerActorActionSelf
                {
                    Action = ActorActionServer.TeleportStart,
                    Parameter1 = (uint)(hasGil ? 0 : 2),
                    Parameter2 = aetheryteId,
                });

                if (hasGil)
                {
                    session.Player.CachedTeleportLocation.Target = aetheryteId;
                    session.Player.CachedTeleportLocation.Cost = cost;
                    session.Player.CachedTeleportLocation.UsingAetheryteTicket = usingTicket;
                }
                else
                {
                    session.Player.CachedTeleportLocation.Target = 0;
                    session.Player.CachedTeleportLocation.Cost = 0;
                }
            }


        }

        [ActorActionHandler(ActorActionClient.Dye)]
        public static void HandleDye(WorldSession session, ClientActorAction actorAction)
        {
            var itemContainer = actorAction.Parameters[0];
            var itemSlot = actorAction.Parameters[1];
            var dyeContainer = actorAction.Parameters[2];
            var dyeSlot = actorAction.Parameters[3];
            session.Player.SendStateFlags();
            session.Player.Inventory.DyeItem(itemContainer, (ushort)itemSlot, dyeContainer, (ushort)dyeSlot);

        }

        [ActorActionHandler(ActorActionClient.Glam)]
        public static void HandleGlam(WorldSession session, ClientActorAction actorAction)
        {
            var itemContainer = actorAction.Parameters[0];
            var itemSlot = actorAction.Parameters[1];
            var glamContainer = actorAction.Parameters[2];
            var glamSlot = actorAction.Parameters[3];
            session.Player.SendStateFlags();

            session.Player.Inventory.GlamourItem(itemContainer, (ushort)itemSlot, glamContainer, (ushort)glamSlot);

        }

        [ActorActionHandler(ActorActionClient.RemoveGlam)]
        public static void HandleRemoveGlam(WorldSession session, ClientActorAction actorAction)
        {
            var itemContainer = actorAction.Parameters[0];
            var itemSlot = actorAction.Parameters[1];
            session.Player.SendStateFlags();

            session.Player.Inventory.RemoveGlamour(itemContainer, (ushort)itemSlot);

        }

        [ActorActionHandler(ActorActionClient.EmoteReq)]
        [ActorActionHandler(ActorActionClient.EmoteWithWarp)]
        public static void HandleEmoteReq(WorldSession session, ClientActorAction actorAction)
        {
            var emoteId = actorAction.Parameters[0];
            var emote = GameTableManager.Emote.GetRow(emoteId);
            if (emote.EmoteMode.Row != 0)
            {
                session.Player.SetStance(Stance.Passive);
                session.Player.SetState(ActorStatus.EmoteMode, (byte)emote.EmoteMode.Value.RowId);
            }

            if (emote.DrawsWeapon)
            {
                session.Player.SetStance(Stance.Active);
            }

            session.Player.SendMessageToVisible(new ServerActorActionTarget
            {
                Action = ActorActionServer.Emote,
                Parameter1 = emoteId,
                Parameter3 = actorAction.Parameters[2],
                Parameter6 = session.Player.targetId,
            }, true);


        }

        [ActorActionHandler(ActorActionClient.EmoteCancel)]
        [ActorActionHandler(ActorActionClient.PersistentEmoteCancel)]
        [ActorActionHandler(ActorActionClient.EmoteCancelWithWarp)]
        public static void HandleEmoteCancel(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetState(ActorStatus.Idle);
            session.Player.SendMessageToVisible(new ServerActorActionTarget
            {
                Action = ActorActionServer.EmoteInterrupt,
            }, true);
        }

        [ActorActionHandler(ActorActionClient.SetTarget)]
        public static void HandleSetTarget(WorldSession session, ClientActorAction actorAction)
        {
            var actorId = actorAction.Parameters[0];
            session.Player.SetTarget(actorId);

        }

        [ActorActionHandler(ActorActionClient.DrawSheathe)]
        public static void HandleDrawSheathe(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetStance((Stance)actorAction.Parameters[0]);

        }

        [ActorActionHandler(ActorActionClient.PoseChange)]
        [ActorActionHandler(ActorActionClient.PoseReapply)]
        public static void HandlePoseChange(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetPose((byte)actorAction.Parameters[0], (byte)actorAction.Parameters[1], true);
        }

        [ActorActionHandler(ActorActionClient.PoseCancel)]
        public static void HandlePoseCancel(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetPose((byte)actorAction.Parameters[0], (byte)actorAction.Parameters[1], false);
        }

        [ActorActionHandler(ActorActionClient.WorldVisit)]
        public static void HandleWorldVisit(WorldSession session, ClientActorAction actorAction)
        {
            session.Send(new ServerWorldVisit());

        }


        [ActorActionHandler(ActorActionClient.Titles)]
        public static void HandleTitleList(WorldSession session, ClientActorAction actorAction)
        {
            session.Send(new ServerTitleList());

        }

        [ActorActionHandler(ActorActionClient.SetTitleReq)]
        public static void HandleSetTitle(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetTitle((ushort)actorAction.Parameters[0]);

        }

        [ActorActionHandler(ActorActionClient.Examine)]
        public static void HandleExamine(WorldSession session, ClientActorAction actorAction)
        {
            var player = MapManager.FindPlayer(actorAction.Parameters[0]);
            session.Send(player.Character.ActorId, session.Player.Character.ActorId, new ServerExamine
            {
                Player = player
            });

        }

        [ActorActionHandler(ActorActionClient.RequestChocoboInventory)]
        public static void HandleRequestChocoboInventory(WorldSession session, ClientActorAction actorAction)
        {

        }

        [ActorActionHandler(ActorActionClient.SetNewAdventurerOff)]
        public static void HandleSetNewAdventurerOff(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetNewAdventurer(false);
        }

        [ActorActionHandler(ActorActionClient.SetNewAdventurerOn)]
        public static void HandleSetNewAdventurerOn(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetNewAdventurer(true);
        }


        [ActorActionHandler(ActorActionClient.CameraMode)]
        public static void HandleCameraMode(WorldSession session, ClientActorAction actorAction)
        {
            if (actorAction.Parameters[0] == 1)
                session.Player.AddOnlineStatus(OnlineStatus.CameraMode);
            else
            {
                session.Player.RemoveOnlineStatus(OnlineStatus.CameraMode);
            }
        }

        [ActorActionHandler(ActorActionClient.CastCancel)]
        public static void HandleCastCancel(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.CurrentAction?.Interrupt();
        }

        public enum ReturnType : byte
        {
            None = 0,
            RaiseSpell = 5,
            Return = 8
        };

        [ActorActionHandler(ActorActionClient.Return)]
        public static void HandleReturn(WorldSession session, ClientActorAction actorAction)
        {
            switch ((ReturnType)actorAction.Parameters[0])
            {
                case ReturnType.RaiseSpell:
                    session.Player.Raise();
                    break;
                case ReturnType.Return:
                    session.Player.ZoneingType = ZoneingType.ReturnDead;
                    session.Player.ReturnHomepoint();
                    break;
            }


        }

        [ActorActionHandler(ActorActionClient.DismountReq)]
        public static void HandleDismount(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.Dismount();
        }

        [ActorActionHandler(ActorActionClient.RemoveStatusEffect)]
        public static void HandleRemoveStatusEffect(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.RemoveStatusEffect(actorAction.Parameters[0]);
        }

        [ActorActionHandler(ActorActionClient.TeleportReq)]
        public static void HandleDeclineRaise(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.RemoveStatusEffect(148);
        }


        [ActorActionHandler(ActorActionClient.SpawnCompanionReq)]
        [ActorActionHandler(ActorActionClient.DespawnCompanionReq)]
        public static void HandleSpawnCompanion(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SetCompanion((ushort)actorAction.Parameters[0]);
        }


        [ActorActionHandler(ActorActionClient.SeenHowTo)]
        public static void HandleSeenHowTo(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SeenHowTo((ushort)actorAction.Parameters[0]);
        }

        [ActorActionHandler(ActorActionClient.RequestEventBattle)]
        public static void HandleEventBattle(WorldSession session, ClientActorAction actorAction)
        {
            session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.EventBattleDialog,
                Parameter1 = 0,
                Parameter2 = actorAction.Parameters[1],
                Parameter3 = actorAction.Parameters[2]

            });
        }


        [ActorActionHandler(ActorActionClient.AbandonQuest)]
        public static void HandleAbandonQuest(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.RemoveQuest((ushort)actorAction.Parameters[0]);
        }

        [ActorActionHandler(ActorActionClient.TeleportAccept)]
        public static void HandleTeleportAccept(WorldSession session, ClientActorAction actorAction)
        {
            if (actorAction.Parameters[0] == 0)
            {
                if (session.Player.CachedTeleportLocation.Target != 0)
                {
                    session.Player.AetheryteTeleport(session.Player.CachedTeleportLocation.Target);
                }
            }
        }


        [ActorActionHandler(ActorActionClient.OpenPerformInstrumentUI)]
        public static void HandleOpenPerformInstrumentUI(WorldSession session, ClientActorAction actorAction)
        {
            if (actorAction.Parameters[0] == 0)
            {
                session.Player.SetState(ActorStatus.Idle);
                session.Player.UnsetStateFlag(PlayerStateFlag.Performing);
            }
            else
            {
                session.Player.SetState(ActorStatus.Performing, (byte)actorAction.Parameters[0]);
                session.Player.UnsetStateFlag(PlayerStateFlag.Performing);
            }
        }

        [ActorActionHandler(ActorActionClient.ResetEnmity)]
        public static void HandleResetEnmity(WorldSession session, ClientActorAction actorAction)
        {
            foreach (var actor in session.Player.GetActorsInRange())
            {
                if (actor.IsNpc && actor.Id == actorAction.Parameters[0])
                {
                    actor.ToBNpc.DeAggro(session.Player);
                }
            }
        }

        [ActorActionHandler(ActorActionClient.DirectorInitFinish)]
        public static void HandleDirectorInitFinish(WorldSession session, ClientActorAction actorAction)
        {

            session.Player.Map?.Director.SendDirectorVars(session.Player);

        }

        [ActorActionHandler(ActorActionClient.DirectorSync)]
        public static void HandleDirectorSync(WorldSession session, ClientActorAction actorAction)
        {

            session.Player.Map?.Director.OnDirectorSync(session.Player);

        }


        [ActorActionHandler(ActorActionClient.RequestInstanceLeave)]
        public static void HandleRequestInstanceLeave(WorldSession session, ClientActorAction actorAction)
        {

            session.Player.TeleportTo(new WorldPosition(132, Vector3.Zero, 0));

        }

        [ActorActionHandler(ActorActionClient.RequestLandSignFree)]
        public static void HandleRequestLandSignFree(WorldSession session, ClientActorAction actorAction)
        {
            var ident = HousingManager.ParamsToLandIdent(actorAction.Parameters[0], actorAction.Parameters[1]);

            HousingManager.SendLandSignFree(session.Player, ident);
        }

        [ActorActionHandler(ActorActionClient.RequestLandSignOwned)]
        public static void HandleRequestLandSignOwned(WorldSession session, ClientActorAction actorAction)
        {
            var ident = HousingManager.ParamsToLandIdent(actorAction.Parameters[0], actorAction.Parameters[1], false);

            HousingManager.SendLandSignOwned(session.Player, ident);
        }

        [ActorActionHandler(ActorActionClient.RequestEstateHallRemoval)]
        public static void HandleRequestEstateHallRemoval(WorldSession session, ClientActorAction actorAction)
        {
            HousingManager.RemoveEstate(session.Player, (byte)actorAction.Parameters[0]);
        }


        [ActorActionHandler(ActorActionClient.RequestHousingBuildPreset)]
        public static void HandleRequestHousingBuildPreset(WorldSession session, ClientActorAction actorAction)
        {
            session.Send(new ServerActorAction
            {
                Action = ActorActionServer.ShowBuildPresetUI,
                Parameter1 = actorAction.Parameters[0]
            });
        }


        [ActorActionHandler(ActorActionClient.GlamourDresserOpen)]
        public static void HandleGlamourDresserOpen(WorldSession session, ClientActorAction actorAction)
        {
            session.Send(new ServerGlamourDresser
            {
                Sequence = 0,
                Entries = session.Player.Character.GlamourInfo.DresserEntries
            });

            session.Send(new ServerGlamourDresser
            {
                Sequence = 1,
                Entries = new List<GlamourDresserEntry>()
            });
        }

        [ActorActionHandler(ActorActionClient.GlamourDresserAdd)]
        public static void HandleGlamourDresserAdd(WorldSession session, ClientActorAction actorAction)
        {
            var containerType = actorAction.Parameters[0];
            var slot = actorAction.Parameters[1];
            var item = session.Player.Inventory.GetItemAtSlot((ContainerType)containerType, (ushort)slot);
            var glamourInfo = session.Player.Character.GlamourInfo;
            glamourInfo.DresserEntries.Add(new GlamourDresserEntry(item.Entry.RowId, item.Color));
            session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.GlamourDresserUpdate,
                Parameter1 = (uint)glamourInfo.DresserEntries.Count - 1,
                Parameter2 = item.Entry.RowId,
                Parameter3 = item.Color
            });
        }

        [ActorActionHandler(ActorActionClient.GlamourDresserRestore)]
        public static void HandleGlamourDresserRestore(WorldSession session, ClientActorAction actorAction)
        {
            var slot = actorAction.Parameters[0];
            var glamourInfo = session.Player.Character.GlamourInfo;
            glamourInfo.DresserEntries.RemoveAt((int)slot);
            session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.GlamourDresserUpdate,
                Parameter1 = slot,
                Parameter2 = 0,
                Parameter3 = 0
            });
        }

        [ActorActionHandler(ActorActionClient.GlamourDresserApply)]
        public static void HandleGlamourDresserApply(WorldSession session, ClientActorAction actorAction)
        {
            var index = actorAction.Parameters[0];
            var container = actorAction.Parameters[1];
            var slot = (ushort)actorAction.Parameters[2];
            var glamourEntry = session.Player.Character.GlamourInfo.DresserEntries.ElementAtOrDefault((int)index);
            if (glamourEntry == null) return;
            session.Player.Inventory.GlamourItem(container, slot, glamourEntry);
            session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.GlamourDresserApplied,
                Parameter1 = 1,
            });
        }

        [ActorActionHandler(ActorActionClient.GlamourDresserDye)]
        public static void HandleGlamourDresserDye(WorldSession session, ClientActorAction actorAction)
        {
            var index = actorAction.Parameters[0];
            var container = actorAction.Parameters[1];
            var slot = actorAction.Parameters[2];
            var dyeItem = session.Player.Inventory.GetItemAtSlot((ContainerType)container, (ushort)slot);
            if (dyeItem == null) return;
            var color = (ushort)dyeItem.Entry.AdditionalData;
            var glamourEntry = session.Player.Character.GlamourInfo.DresserEntries.ElementAtOrDefault((int)index);
            if (glamourEntry == null) return;
            glamourEntry.Color = color;
            session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.GlamourDresserUpdate,
                Parameter1 = index,
                Parameter2 = glamourEntry.ItemId,
                Parameter3 = glamourEntry.Color,

            });
        }


        [ActorActionHandler(ActorActionClient.GlamourPlateOpen)]
        public static void HandleGlamourPlateOpen(WorldSession session, ClientActorAction actorAction)
        {
            session.Send(new ServerGlamourPlate());
        }

        [ActorActionHandler(ActorActionClient.NoviceNetworkJoin)]
        public static void HandleNoviceNetworkJoin(WorldSession session, ClientActorAction actorAction)
        {
            switch (actorAction.Parameters[0])
            {
                case 0:
                {
                    session.Send(new ServerActorActionSelf
                    {
                        Action = ActorActionServer.SetNewAdventurer,
                        Parameter1 = 1,
                        Parameter2 = 1
                    });

                    session.Send(new ServerSystemLogMessage
                    {
                        Id = 3820,
                        Parameter1 = 1,
                        Parameter5 = 0,
                    });

                    break;
                }
            }

        }

        [ActorActionHandler(ActorActionClient.ChangeGlasses)]
        public static void HandleChangeGlasses(WorldSession session, ClientActorAction actorAction)
        {
            session.Player.SendMessageToVisible(new ServerActorAction
            {
                Action = ActorActionServer.GlassesChange,
                Parameter2 = actorAction.Parameters[1]
            }, true);

            if (actorAction.Parameters[1] != 0)
            {
                session.Player.SendLogMessage(10569, actorAction.Parameters[1]);
            }
            else
            {
                session.Player.SendLogMessage(10570, session.Player.Character.Glasses);
            }

            session.Player.Character.Glasses = actorAction.Parameters[1];


        }


    }
}