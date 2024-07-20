using WorldServer.Game.Event;
using WorldServer.Game.Shop;

namespace WorldServer.Script.Common;

[EventScript(0x00040000)]
public class GilShop : EventScript
{
    private void Scene00000()
    {
        var callback = (SceneResult result) =>
        {
            StartShop();
        };
        this.owner.Event.NewScene(Id, 0, SceneFlags.HIDE_HOTBAR | SceneFlags.NO_DEFAULT_CAMERA, Callback: callback, 2);
        
    }

    private void StartShop()
    {
        this.owner.Event.NewScene(Id, 10, SceneFlags.HIDE_HOTBAR | SceneFlags.NO_DEFAULT_CAMERA, Callback: ShopInteractionCallback);
    }
    
    private void ShopInteractionCallback(SceneResult result)
    {
        if (result.param1 == 768 || result.param1 == 512)
        {
            if (result.param2 == 1)
            {
                ShopManager.GilShopPurchase(player, Id, result.param3, result.param4);
            }
            StartShop();
            return;
        }
        
        this.owner.Event.NewScene(Id, 255, SceneFlags.HIDE_HOTBAR | SceneFlags.NO_DEFAULT_CAMERA);
    }
    
    public override void OnGossip(ulong actorId)
    {
        Scene00000();
    }
}