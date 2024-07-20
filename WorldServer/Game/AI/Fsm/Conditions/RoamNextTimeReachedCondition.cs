using System;
using WorldServer.Game.Entity;

namespace WorldServer.Game.AI.Fsm.Conditions;

public class RoamNextTimeReachedCondition : Condition
{
    public override bool IsConditionMet(BNpc src)
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds() - src.LastRoamReachedTime > 20;
    }
}