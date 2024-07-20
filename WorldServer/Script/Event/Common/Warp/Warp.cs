using Shared.SqPack;
using WorldServer.Game.Event;

namespace WorldServer.Script.Common.Warp
{
    [EventScript(0x00020000)]
    public class Warp : EventScript
    {
        public override void OnGossip(ulong actorId)
        {
            var warp = GameTableManager.Warp.GetRow(Id);
            if (warp == null)
                return;
            
            owner.Event.NewScene(Id,0, SceneFlags.HIDE_HOTBAR, Callback: result =>
            {
                if (result.GetResult(0) == 1 && result.errorCode != 2)
                {
                    this.owner.TeleportToPopRange(warp.PopRange.Row);
                }
            });
            
            

        }
        
    }
}