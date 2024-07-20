using System.Collections.Generic;
using WorldServer.Game.Entity;

namespace WorldServer.Game.AI.Fsm;

public class StateMachine
{
    public StateMachine(BNpc owner)
    {
        this.Owner = owner;
    }

    private BNpc Owner;

    private State State;

    private List<State> States = [];
    public void Update(double lastTick)
    {
        if (this.State is null)
            return;

        Transition transition = this.State.GetTriggeredTransition(this.Owner);
        if (transition != null)
        {
            this.State.OnExit(this.Owner);
            this.State = transition.TargetState;
            this.State.OnEnter(this.Owner);
        }
        this.State.OnUpdate(this.Owner, (ulong)lastTick);


    }

    public void AddState(State state)
    {
        this.States.Add(state);
    }

    public void SetCurrentState(State state)
    {
        State = state;
    }
}