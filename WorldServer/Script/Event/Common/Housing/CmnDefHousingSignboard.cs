using WorldServer.Game.Event;
using WorldServer.Game.Housing;
using WorldServer.Game.Housing.Enums;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Script.Common
{
    [EventScript(721031)]
    public class CmnDefHousingSignboard : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                if (result.GetResult(0) == 2)
                {
                    var purchaseResult = HousingManager.PurchaseLand(player, HousingManager.LandIdentToLand(player.ActiveLand));
                    switch (purchaseResult)
                    {
                        case LandPurchaseResult.ERR_INTERNAL:
                        {
                            player.SendLogMessage(1684);
                            break;
                        }
                        case LandPurchaseResult.ERR_NOT_ENOUGH_GIL:
                        {
                            player.SendLogMessage(3314);
                            break;
                        }
                        case LandPurchaseResult.ERR_NOT_AVAILABLE:
                        {
                            player.SendLogMessage(3312);
                            break;
                        }
                        case LandPurchaseResult.ERR_NO_MORE_LANDS_FOR_CHAR:
                        {
                            player.SendLogMessage(3313);
                            break;
                        }
                        case LandPurchaseResult.SUCCESS:
                        {
                            player.Session.Send(new ServerActorActionSelf
                            {
                                Action = ActorActionServer.DutyQuestScreenMsg,
                                Parameter1 = this.Id,
                                Parameter2 = 0x98
                            });
                            player.SendLogMessage(0x0D16, player.Map.Entry.PlaceName.Row, (uint)(this.player.ActiveLand.WardNumber + 1), (uint)(this.player.ActiveLand.LandId + 1));
                            break;
                        }
                    }
                }
            };
            owner.Event.NewScene(this.Id,0, SceneFlags.HIDE_HOTBAR, Callback:callback);
        }
        public override void OnGossip(ulong actorId)
        {
            this.owner.Session.Send(new ServerLandAvailability
            {
                SellMode = LandSellMode.FirstComeFirstServed,
                AvailableTo = LandAvailableTo.Private,
                LotteryStatus = LandLotteryStatus.FirstComeFirstServed
            });
            Scene00000();
        }
    }
}