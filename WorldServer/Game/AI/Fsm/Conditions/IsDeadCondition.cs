using System;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Game.AI.Fsm.Conditions;

public class IsDeadCondition : Condition
{
    public override bool IsConditionMet(BNpc src)
    {
        return src.State.HasFlag(ActorStatus.Dead);
    }
}