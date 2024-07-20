using WorldServer.Game.Entity;

namespace WorldServer.Game.AI.Fsm;

public class Transition
{
    public Transition(State targetState, Condition condition)
    {
        this.TargetState = targetState;
        this.Condition = condition;
    }
    public State TargetState;
    public Condition Condition;
    
    public bool HasTriggered(BNpc bnpc)
    {
        return this.Condition.IsConditionMet(bnpc);
    }
}