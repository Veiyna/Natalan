using WorldServer.Game.Action;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Script.Action.Common
{
    [ActionScript(5)]
    public class ActionTeleport : ActionScript
    {
        public override void OnExecute(Game.Action.Action action)
        {
            if (action.Source.IsPlayer)
            {
                var player = action.Source.ToPlayer;
                var cost = player.CachedTeleportLocation.Cost;
                if (player.GetCurrency(CurrencyType.Gil) < cost || player.CachedTeleportLocation.Target == 0 ||
                    (player.CachedTeleportLocation.UsingAetheryteTicket && !player.Inventory.RemoveItem(7569)))
                {
                    action.Interrupt();
                    return;
                }
                
                
                if (cost > 0)
                {
                    player.RemoveCurrency(CurrencyType.Gil,cost);
                    player.Session.Send(new ServerActorActionSelf
                    {
                        Action = ActorActionServer.LogMsg,
                        Parameter1 = 4590,
                        Parameter2 = cost
                    });
                }

                player.AetheryteTeleport(player.CachedTeleportLocation.Target);
            }

            
        }
    }
}