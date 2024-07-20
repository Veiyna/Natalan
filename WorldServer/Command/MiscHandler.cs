using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Shared.Command;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.FreeCompany;
using WorldServer.Game.Housing;
using WorldServer.Game.Map;
using WorldServer.Network;
using WorldServer.Network.Message;
using WorldServer.Script;

namespace WorldServer.Command
{
    public static class MiscHandler
    {
        [CommandHandler("grid_vision_test", SecurityLevel.Developer, 0)]
        public static void HandleGridVisionTest(WorldSession session, params string[] parameters)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (uint i = 0u; i < 1_000_000u; i++)
                session.Player.UpdateVision();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        [CommandHandler("unstuck", SecurityLevel.Developer, 0)]
        public static void HandleUnstuck(WorldSession session, params string[] parameters)
        {
            session.Player.Event.StopAllEvents();
            session.Player.StateMask = new BitArray(8 * 8, false);
            session.Player.SendStateFlags();
        }

        [CommandHandler("unstuck2", SecurityLevel.Developer, 0)]
        public static void HandleUnstuck2(WorldSession session, params string[] parameters)
        {
            session.Player.StateMask = new BitArray(8 * 8, false);
            session.Player.SendStateFlags();
            session.Player.TeleportTo(session.Player.Position);

        }


        [CommandHandler("housing_teleport", SecurityLevel.Developer, 2)]
        public static void HandleHousingTeleport(WorldSession session, params string[] parameters)
        {
            if (!byte.TryParse(parameters[0], out var index))
                return;

            if (!byte.TryParse(parameters[1], out var ward))
                return;

            HousingManager.TeleportToWard(session.Player, index, ward);

        }


        [CommandHandler("resend_setup", SecurityLevel.Developer, 0)]
        public static void HandleResendSetup(WorldSession session, params string[] parameters)
        {

            session.Send(new ServerPlayerSetup
            {
                Character = session.Player.Character
            });


        }

        [CommandHandler("reload", SecurityLevel.Developer, 0)]
        public static void HandleReload(WorldSession session, params string[] parameters)
        {
            ScriptManager.Initialise();
        }

        [CommandHandler("dmg_test", SecurityLevel.Developer, 1)]
        public static void HandleDmgTest(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out uint type))
                return;

            session.Player.TakeDamage(type);
        }

        [CommandHandler("add_status", SecurityLevel.Developer, 2)]
        public static void HandleAddStatus(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out uint type))
                return;

            if (!ushort.TryParse(parameters[1], out ushort param))
                return;

            session.Player.AddStatusEffect(type, session.Player, param);
        }

        [CommandHandler("clear_status", SecurityLevel.Developer, 0)]
        public static void HandleClearEffects(WorldSession session, params string[] parameters)
        {
            session.Player.ClearStatusEffects();
        }

        [CommandHandler("mount", SecurityLevel.Developer, 1)]
        public static void HandleMount(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out uint type))
                return;

            session.Player.Mount(type);
        }

        [CommandHandler("visual", SecurityLevel.Developer, 0)]
        public static void HandleSetVisualEffect(WorldSession session, params string[] parameters)
        {
            if (!ushort.TryParse(parameters[0], out ushort effect))
                return;

            session.Player.SetVisualEffect(effect);
        }

        [CommandHandler("bnpcspawn", SecurityLevel.Developer, 1)]
        public static void HandleBnpcSpawn(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out var instanceId))
                return;

            var position = session.Player.Position;
            var bnpc = session.Player.Map.CreateBNpcFromLayoutId(instanceId, session.Player.Id);
        }
        [CommandHandler("mobaggro", SecurityLevel.Developer, 0)]
        public static void HandleMobAggro(WorldSession session, params string[] parameters)
        {
            var actors = session.Player.GetActorsInRange();
            foreach (var actor in actors.Where(actor => actor.IsNpc))
            {
                actor.ToBNpc.Aggro(session.Player);
            }
        }

        [CommandHandler("resetgil", SecurityLevel.Developer, 0)]
        public static void HandleResetGil(WorldSession session, params string[] parameters)
        {
            session.Player.RemoveCurrency(CurrencyType.Gil, session.Player.GetCurrency(CurrencyType.Gil));


        }

        [CommandHandler("addcurrency", SecurityLevel.Developer, 2)]
        public static void HandleAddCurrency(WorldSession session, params string[] parameters)
        {
            if (!Enum.TryParse(parameters[0], out CurrencyType type))
                return;

            if (!uint.TryParse(parameters[1], out var amount))
                return;

            session.Player.AddCurrency(type, amount);


        }

        [CommandHandler("classjob", SecurityLevel.Developer, 1)]
        public static void HandleClassjob(WorldSession session, params string[] parameters)
        {
            if (!byte.TryParse(parameters[0], out var classjob))
                return;

            session.Player.ChangeClass(classjob);

        }

        [CommandHandler("setmasterunlock", SecurityLevel.Developer, 2)]
        public static void HandleSetMasterUnlock(WorldSession session, params string[] parameters)
        {
            if (!ushort.TryParse(parameters[0], out var master))
                return;

            if (!bool.TryParse(parameters[1], out var enable))
                return;

            session.Player.SetMasterUnlock(master, enable);

        }

        [CommandHandler("setquest", SecurityLevel.Developer, 2)]
        public static void HandleSetQuest(WorldSession session, params string[] parameters)
        {
            if (!ushort.TryParse(parameters[0], out var master))
                return;

            if (!bool.TryParse(parameters[1], out var enable))
                return;

            session.Player.UpdateQuestProgression(master, enable);

        }

        [CommandHandler("pos", SecurityLevel.Developer, 3)]
        public static void HandleSetPos(WorldSession session, params string[] parameters)
        {
            if (!float.TryParse(parameters[0], out var x))
                return;

            if (!float.TryParse(parameters[1], out var y))
                return;

            if (!float.TryParse(parameters[2], out var z))
                return;

            var position = new Vector3(x, y, z);
            var worldPosition = new WorldPosition(session.Player.Position.TerritoryId, position, session.Player.Position.Orientation);
            session.Player.ChangePos(worldPosition);

        }

        [CommandHandler("msqtracker", SecurityLevel.Developer, 2)]
        public static void HandleMsqTracker(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out var master))
                return;

            if (!uint.TryParse(parameters[1], out var master2))
                return;

            session.Send(new ServerMSQTrackerComplete
            {
                Id = master,
                Id2 = master2
            });

            session.Player.SendMSQTracker();

        }

        [CommandHandler("msqtracker2", SecurityLevel.Developer, 0)]
        public static void HandleMsqTracker2(WorldSession session, params string[] parameters)
        {
            session.Player.SendMSQTracker();
        }

        [CommandHandler("testcommand1", SecurityLevel.Developer, 0)]
        public static void HandleTestCommand1(WorldSession session, params string[] parameters)
        {
            session.Player.PartyTeleport(13, 1);
        }

        [CommandHandler("save", SecurityLevel.Developer, 0)]
        public static void HandleSave(WorldSession session, params string[] parameters)
        {
            session.Player.SavePlayerData();
        }

        [CommandHandler("fake_quest_progress", SecurityLevel.Developer, 0)]
        public static void HandleFakeQuest(WorldSession session, params string[] parameters)
        {
            session.Send(new ServerQuestJournalCompleteList
            {
                QuestMask = new BitArray(727 * 8, true)
            });
        }

        [CommandHandler("create_instance", SecurityLevel.Developer, 1)]
        public static void HandleCreateInstance(WorldSession session, params string[] parameters)
        {
            if (!ushort.TryParse(parameters[0], out var master))
                return;

            var territory = MapManager.CreateInstance(master);

            session.Player.sendDebug($"Created instance {territory.InstanceId}");

        }

        [CommandHandler("dungeon_test", SecurityLevel.Developer, 0)]
        public static void HandleDungeonTest(WorldSession session, params string[] parameters)
        {
            session.Player.TeleportTo(new WorldPosition(1036, Vector3.Zero, 0));

        }

        [CommandHandler("eureka_test", SecurityLevel.Developer, 0)]
        public static void HandleEurekaTest(WorldSession session, params string[] parameters)
        {
            session.Player.TeleportTo(new WorldPosition(732, Vector3.Zero, 0));

        }

        [CommandHandler("begin_duty", SecurityLevel.Developer, 0)]
        public static void HandleBeginDuty(WorldSession session, params string[] parameters)
        {
            var instance = (InstanceContent)session.Player.Map;
            instance.BeginDuty();

        }

        [CommandHandler("enter_content", SecurityLevel.Developer, 1)]
        public static void HandleEnterContent(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out var contentId))
                return;

            session.Player.TeleportTo(new WorldPosition((ushort)contentId, Vector3.Zero, 0));
        }


        [CommandHandler("setstate", SecurityLevel.Developer, 2)]
        public static void HandleSetState(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out var objectId))
                return;

            if (!byte.TryParse(parameters[1], out var state))
                return;

            session.Player.Map.GetFirstEObjByObjectId(objectId)?.UpdateState(state);
        }

        [CommandHandler("teleport_instance", SecurityLevel.Developer, 2)]
        public static void HandleTeleportInstance(WorldSession session, params string[] parameters)
        {
            if (!ushort.TryParse(parameters[0], out var territoryId))
                return;

            if (!uint.TryParse(parameters[1], out var instanceId))
                return;

            session.Player.TeleportTo(new WorldPosition(territoryId, new Vector3(0, 0, 0), 2f, instanceId));
        }

        [CommandHandler("trade", SecurityLevel.Developer, 1)]
        public static void HandleTrade(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out var master))
                return;

            session.Send(new ServerTrade
            {
                Type = master
            });

        }

        [CommandHandler("itemset", SecurityLevel.Developer, 1)]
        public static void HandleItemSet(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out var master))
                return;

            master += 1000000;
            var itemset = GameTableManager.FittingShopItemSet.GetRow(master);

            if (itemset == null)
                return;

            session.Player.Inventory.NewItem((uint)itemset.Unknown0);
            session.Player.Inventory.NewItem((uint)itemset.Unknown1);
            session.Player.Inventory.NewItem((uint)itemset.Unknown2);
            session.Player.Inventory.NewItem((uint)itemset.Unknown3);
            session.Player.Inventory.NewItem((uint)itemset.Unknown4);
            session.Player.Inventory.NewItem((uint)itemset.Unknown5);

        }


        [CommandHandler("gauge_fill", SecurityLevel.Developer, 0)]
        public static void HandleGaugeFill(WorldSession session, params string[] parameters)
        {
            Array.Fill(session.Player.JobGauge, (byte)255);
            session.Player.SendJobGauge();

        }

        [CommandHandler("gauge_empty", SecurityLevel.Developer, 0)]
        public static void HandleGaugeEmpty(WorldSession session, params string[] parameters)
        {
            Array.Fill(session.Player.JobGauge, (byte)0);
            session.Player.SendJobGauge();

        }

        [CommandHandler("server_save", SecurityLevel.Developer, 0)]
        public static void HandleServerSave(WorldSession session, params string[] parameters)
        {
            foreach (var player in MapManager.GetPlayers())
            {
                player.SavePlayerData();
            }

            FreeCompanyManager.Save();
        }

        [CommandHandler("fc_create", SecurityLevel.Developer, 0)]
        public static void HandleFcCreate(WorldSession session, params string[] parameters)
        {
            FreeCompanyManager.CreateFreeCompany(session.Player, "Testing Company", "TEST");

        }

        [CommandHandler("unlock_emotes", SecurityLevel.Developer, 0)]
        public static void HandleEmoteUnlock(WorldSession session, params string[] parameters)
        {
            foreach (var emote in GameTableManager.Emote)
            {
                if (emote.UnlockLink != 0 && emote.UnlockLink < ushort.MaxValue)
                    session.Player.SetMasterUnlock((ushort)emote.UnlockLink, true);
            }

        }


        [CommandHandler("aetheryte_teleport", SecurityLevel.Developer, 1)]
        public static void HandleAetheryteTeleport(WorldSession session, params string[] parameters)
        {
            if (!ushort.TryParse(parameters[0], out var id))
                return;

            session.Player.AetheryteTeleport(id);

        }
    }
}