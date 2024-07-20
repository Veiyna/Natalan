using WorldServer.Game.Action;
using WorldServer.Game.Action.Effects;

namespace WorldServer.Script.Action.Common
{
    [ActionScript(4)]
    public class ActionMount : ActionScript
    {
        public override void OnExecute(Game.Action.Action action)
        {
            action.AddEffect(action.Target, EffectBuilder.Mount(action, action.AdditionalId, action.Target));
        }
    }
}