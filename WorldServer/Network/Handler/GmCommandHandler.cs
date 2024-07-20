using System.Numerics;
using Shared.Game;
using Shared.Network;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class GmCommandHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientGmCommandInt)]
        public static void HandleGmCommandInt(WorldSession session, ClientGmCommandInt gmCommand)
        {
            GmCommandManager.Invoke(session, gmCommand);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientGmCommandString)]
        public static void HandleGmCommandString(WorldSession session, ClientGmCommandString gmCommand)
        {
            GmCommandManager.Invoke(session, gmCommand);
        }

        [GmCommandHandler(GmCommand.Inspect)]
        public static void HandleGmCommandInspect(WorldSession session, GmCommandParameters parameters)
        {
        }

        [GmCommandHandler(GmCommand.Item)]
        public static void HandleGmCommandItem(WorldSession session, GmCommandParameters parameters)
        {
            var count = parameters.Parameters[1];
            if (count < 1 || count > 999)
                count = 1;
            
            parameters.Target.Inventory.NewItem(parameters.Parameters[0], count);
        }
        
        [GmCommandHandler(GmCommand.Gil)]
        public static void HandleGmCommandGil(WorldSession session, GmCommandParameters parameters)
        {
            var count = parameters.Parameters[0];
            
            parameters.Target.AddCurrency(CurrencyType.Gil, count);
        }
        
        [GmCommandHandler(GmCommand.Call)]
        public static void HandleGmCommandCall(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.TeleportTo(session.Player.Position);
        }
        
        [GmCommandHandler(GmCommand.Jump)]
        public static void HandleGmCommandJump(WorldSession session, GmCommandParameters parameters)
        {
            session.Player.TeleportTo(parameters.Target.Position);
        }
        
        
        [GmCommandHandler(GmCommand.Icon)]
        public static void HandleGmCommandIcon(WorldSession session, GmCommandParameters parameters)
        {
            var target = parameters.Target;
            var status = (OnlineStatus)parameters.Parameters[0];
            if (target.HasOnlineStatus(status)) 
                target.RemoveOnlineStatus(status);
            else
                target.AddOnlineStatus(status);

        }
        
        [GmCommandHandler(GmCommand.Wireframe)]
        public static void HandleGmCommandWireframe(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.Session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.ToggleWireframeRendering
            });

        }
        
        [GmCommandHandler(GmCommand.ImmediatelyAction)]
        public static void HandleGmCommandImmediatelyAction(WorldSession session, GmCommandParameters parameters)
        {
        }
        
        [GmCommandHandler(GmCommand.Speed)]
        public static void HandleGmCommandSpeed(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.Session.Send(new ServerActorActionSelf
                {
                    Action = ActorActionServer.Flee,
                    Parameter1 = parameters.Parameters[0],
                });
        }
        
        [GmCommandHandler(GmCommand.Raise)]
        public static void HandleGmCommandRaise(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.Raise();
        }
        
        [GmCommandHandler(GmCommand.Kill)]
        public static void HandleGmCommandKill(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.Die();
        }
        
        [GmCommandHandler(GmCommand.Lv)]
        public static void HandleGmCommandLevel(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.SetLevel((byte)parameters.Parameters[0]);
        }
        
        [GmCommandHandler(GmCommand.Hp)]
        public static void HandleGmCommandHp(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.HP = parameters.Parameters[0];
            parameters.Target.SendHPUpdate();
        }
        [GmCommandHandler(GmCommand.Mp)]
        public static void HandleGmCommandMp(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.SetMP((ushort)parameters.Parameters[0]);
        }
        
        [GmCommandHandler(GmCommand.FCPoint)]
        public static void HandleGmCommandFCPoint(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.FreeCompany.Points = parameters.Parameters[0];
        }
        
        [GmCommandHandler(GmCommand.FCCredit)]
        public static void HandleGmCommandFCCredit(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.FreeCompany.Credit = parameters.Parameters[0];
        }
        
        [GmCommandHandler(GmCommand.FCRank)]
        public static void HandleGmCommandFCRank(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.FreeCompany.Rank = (byte)parameters.Parameters[0];
        }
        
        [GmCommandHandler(GmCommand.Aetheryte)]
        public static void HandleGmCommandAetheryte(WorldSession session, GmCommandParameters parameters)
        {
            var aetheryteId = (byte)parameters.Parameters[1];
            var state = parameters.Parameters[0] == 0;
            if (aetheryteId == 0)
            {
                for (byte i = 0; i < 255; i++)
                {
                    parameters.Target.SetAetheryte(i, state);
                }
            }
            else
            {
                parameters.Target.SetAetheryte((byte)parameters.Parameters[1], (byte)parameters.Parameters[0] == 0);
            }
            
        }
        
        [GmCommandHandler(GmCommand.Teri)]
        public static void HandleGmCommandTerritory(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.TeleportTo(new WorldPosition((ushort)parameters.Parameters[0], Vector3.Zero, 0));
        }
        
        [GmCommandHandler(GmCommand.QuestSequence)]
        public static void HandleGmCommandQuestSequence(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.UpdateQuest((ushort)parameters.Parameters[0], (byte)parameters.Parameters[1]);
        }
        
        [GmCommandHandler(GmCommand.QuestCancel)]
        public static void HandleGmCommandQuestCancel(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.RemoveQuest((ushort)parameters.Parameters[0]);
        }
        
        [GmCommandHandler(GmCommand.QuestAccept)]
        public static void HandleGmCommandQuestAccept(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.UpdateQuest(parameters.Parameters[0], 1);
        }
        
        [GmCommandHandler(GmCommand.QuestIncomplete)]
        public static void HandleGmCommandQuestIncomplete(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.UpdateQuestProgression((ushort)parameters.Parameters[0], false);
        }
        
        [GmCommandHandler(GmCommand.QuestComplete)]
        public static void HandleGmCommandQuestComplete(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.UpdateQuestProgression((ushort)parameters.Parameters[0], true);
        }
        
        [GmCommandHandler(GmCommand.Exp)]
        public static void HandleExp(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.GainExp(parameters.Parameters[0]);
        }
        
        [GmCommandHandler(GmCommand.GC)]
        public static void HandleGC(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.SetGC((byte)parameters.Parameters[0]);
        }
        
        [GmCommandHandler(GmCommand.GCRank)]
        public static void HandleGCRank(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.SetGCRank((byte)parameters.Parameters[0]);
        }
        
        [GmCommandHandler(GmCommand.Kick)]
        public static void HandleKick(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.Session.Disconnect();
        }
        
        [GmCommandHandler(GmCommand.TeriInfo)]
        public static void HandleTeriInfo(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.sendUrgent($"{parameters.Target.Map.Entry.Name.RawString} ({parameters.Target.Map.Entry.PlaceName.Value.Name.RawString})\nTerritory Id: {parameters.Target.Position.TerritoryId}\nInstance Id: {parameters.Target.Position.InstanceId}\nPosition:\n {parameters.Target.Position.Offset}\n Rotation: {parameters.Target.Position.Orientation}");
        }
        [GmCommandHandler(GmCommand.Jail)]
        public static void HandleJail(WorldSession session, GmCommandParameters parameters)
        {
            parameters.Target.TeleportTo(new WorldPosition(176, Vector3.Zero, 0f));
        }
        
        
        [GmCommandHandler(GmCommand.Pos)]
        public static void HandlePos(WorldSession session, GmCommandParameters parameters)
        {
            var x = (float)parameters.Parameters[0] / 100;
            var y = (float)parameters.Parameters[1] / 100;
            var z = (float)parameters.Parameters[2] / 100;
            var position = new Vector3(x, y, z);
            var worldPosition = new WorldPosition(parameters.Target.Position.TerritoryId, position, parameters.Target.Position.Orientation);
            parameters.Target.ChangePos(worldPosition);
        }
        
        
        [GmCommandHandler(GmCommand.EurekaStep)]
        public static void HandleEurekaStep(WorldSession session, GmCommandParameters parameters)
        {
            session.Player.SetEurekaStep(parameters.Parameters[0]);
        }
    }
}
