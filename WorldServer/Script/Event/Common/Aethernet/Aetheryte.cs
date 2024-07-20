using Shared.SqPack;
using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(0x00050000)]
    public class Aetheryte : EventScript
    {
        private const ushort  ACTION_ATTUNE = 0x13;
        private const ushort  ACTION_TELEPORT = 0x4;

        private const uint  AETHERYTE_MENU_AETHERNET = 1;
        private const uint  AETHERYTE_MENU_HOUSING = 2;
        private const uint  AETHERYTE_MENU_HOME_POINT = 3;
        private const uint  AETHERYTE_MENU_FAVORITE_POINT = 4;
        private const uint  AETHERYTE_MENU_FAVORITE_POINT_SECURITY_TOKEN = 5;
        private void aetheryte()
        {
            var aetheryteId = (byte)(Id & 0xFFFF);
            if (this.owner.IsAetheryteRegistered(aetheryteId))
            {
                owner.Event.NewScene(this.Id,0, SceneFlags.NO_DEFAULT_CAMERA, result =>
                {
                    
                    if (result.param1 == 256)
                    {
                        var cmd = result.GetResult(0);
                        if (cmd == 1)
                        {
                            player.SetHomepoint(aetheryteId);
                            player.SendQuestMessage(Id, 2, 0xEA);
                        }

                        if (cmd == 5)
                        {
                            
                        }

                    }
                    else if (result.param1 == 512)
                    {
                        if (result.param2 == 4 && result.param3 != 0)
                        {
                            player.AetheryteTeleport(result.param3);
                        }
                    }
                }, 1,1);
            }
            else
            {
                player.SetAetheryte(aetheryteId, true);
                if (player.GetMasterUnlock(ACTION_TELEPORT))
                {
                    player.SendQuestMessage(Id, 0, 2);
                }
                else
                {
                    player.SendQuestMessage( Id, 0, 1, 1);
                    player.SetMasterUnlock( ACTION_TELEPORT);
                }
                player.Event.StopEvent(Id);
            }
        }
        
        private void aethernet()
        {
            var aetheryteId = (byte)(Id & 0xFFFF);
            if (this.owner.IsAetheryteRegistered(aetheryteId))
            {
                owner.Event.NewScene(this.Id,2, SceneFlags.None, Callback: result =>
                {

                    if (result.param1 == 256 && result.param2 != 0)
                    {
                        player.AetheryteTeleport(result.param2);
                    }
                } );
            }
            else
            {
                player.SetAetheryte(aetheryteId, true);
                player.Event.NewScene(Id, 3, SceneFlags.None);
            }
        }
        
        public override void OnGossip(ulong actorId)
        {
            var aetheryteId = (byte)(Id & 0xFFFF);
            var aetheryte = GameTableManager.Aetheryte.GetRow(aetheryteId);
            
            if( aetheryte == null)
                return;

            if (aetheryte.IsAetheryte)
            {
                this.aetheryte();
            }
            else
            {
                aethernet();
            }

            
        }
        

    }
}