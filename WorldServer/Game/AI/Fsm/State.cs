using System.Collections.Generic;
using WorldServer.Game.Entity;

namespace WorldServer.Game.AI.Fsm;

public abstract class State
{
    private List<Transition> Transitions = new();

    public abstract void OnUpdate(BNpc bnpc, ulong tickCount);
    public abstract void OnEnter(BNpc bnpc);
    public abstract void OnExit(BNpc bnpc);
    
    public void AddTransition(Transition transition)
    {
        this.Transitions.Add(transition);
    }
    
    public void AddTransition(State state, Condition condition)
    {
        this.Transitions.Add(new Transition(state, condition));
    }

    public Transition GetTriggeredTransition(BNpc bnpc)
    {
        foreach (var transition in this.Transitions)
        {
            if (transition.HasTriggered(bnpc))
                return transition;
        }

        return null;
    }
}