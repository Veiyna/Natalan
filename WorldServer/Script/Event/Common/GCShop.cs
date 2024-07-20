using WorldServer.Game.Event;

namespace WorldServer.Script.Common;

[EventScript(1441792)] 
public class GCShop : EventScript
{
    private void Scene00000()
    {
        var callback = (SceneResult result) =>
        {
            Scene00010();
        };
        this.owner.Event.NewScene(Id, 0, SceneFlags.HIDE_HOTBAR | SceneFlags.NO_DEFAULT_CAMERA, Callback: callback);
        
    }
    
    private void Scene00010()
    {
        var callback = (SceneResult result) =>
        {
            if (result.GetResult(0) == 0)
            {
                var itemId = result.GetResult(2);
                var amount = result.GetResult(5);
                player.Inventory.NewItem(itemId, amount);
                Scene00010();
            }
        };

        this.owner.Event.NewScene(Id, 10, SceneFlags.HIDE_HOTBAR | SceneFlags.NO_DEFAULT_CAMERA, Callback: callback);

    }
    public override void OnGossip(ulong actorId)
    {
        Scene00000();
    }
}