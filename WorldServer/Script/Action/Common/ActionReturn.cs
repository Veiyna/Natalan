using WorldServer.Game.Action;

namespace WorldServer.Script.Action.Common
{
    [ActionScript(6)]
    public class ActionReturn : ActionScript
    {
        public override void OnExecute(Game.Action.Action action)
        {
            if (action.Source.IsPlayer)
                action.Source.ToPlayer.ReturnHomepoint();
        }
    }
}