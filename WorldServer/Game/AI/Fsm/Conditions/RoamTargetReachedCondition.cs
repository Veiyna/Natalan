using WorldServer.Game.Entity;

namespace WorldServer.Game.AI.Fsm.Conditions;

public class RoamTargetReachedCondition : Condition
{
    public override bool IsConditionMet(BNpc src)
    {
        return src.RoamTargetReached;
    }
}