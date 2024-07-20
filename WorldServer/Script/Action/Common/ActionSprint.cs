using WorldServer.Game.Action;
using WorldServer.Game.Action.Effects;

namespace WorldServer.Script.Action.Common
{
    [ActionScript(3)]
    public class ActionSprint : ActionScript
    {
        public override void OnExecute(Game.Action.Action action)
        {
            action.AddEffect(action.Target, EffectBuilder.StatusEffectTarget(action, 50, 20, 30, action.Target));
        }
    }
}