using Shared.SqPack;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Common.Warp
{
    [EventScript(131079)]
    [EventScript(131080)]
    [EventScript(131081)]
    public class WarpInn : EventScript
    {
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                Scene00001();
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001()
        {
            var callback = (SceneResult result) =>
            {
                if (result.param2 == 1)
                {
                    var warp = GameTableManager.Warp.GetRow(Id);
                    if (warp != null)
                    {
                        this.owner.TeleportToPopRange(warp.PopRange.Row);
                    }
                }

            };
            owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00002()
        {
            owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR );
        }
        
        public override void OnGossip(ulong actorId)
        {
            var warp = GameTableManager.Warp.GetRow(Id);
            if (warp == null)
                return;
            
            if(player.GetMasterUnlock((ushort)UnlockEntry.InnRoom))
                Scene00000();
            else
                Scene00002();
            
            

        }
        
        
        
    }
}