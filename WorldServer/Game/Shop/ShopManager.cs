using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network.Message;

namespace WorldServer.Game.Shop;

public static class ShopManager
{
    public static bool GilShopPurchase(Player player, uint shopId, ushort itemId, uint quantity)
    {
        var gilShopItem = GameTableManager.GilShopItem.GetRow(shopId, itemId);

        var item = gilShopItem?.Item.Value;
        if (item == null)
            return false;

        var price = item.PriceMid * quantity;
        if (player.GetCurrency(CurrencyType.Gil) < price)
            return false;
        
        player.Inventory.NewItem(gilShopItem.Item.Row, quantity, false);
        player.RemoveCurrency(CurrencyType.Gil, price);
        player.Session.Send(new ServerShopMessage
        {
            ShopId = shopId,
            MsgType = 1687,
            ItemId = gilShopItem.Item.Row,
            Amount = quantity,
            Price = price
        });
        return true;
    }
}