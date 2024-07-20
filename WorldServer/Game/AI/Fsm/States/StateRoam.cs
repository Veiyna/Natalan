using System;
using WorldServer.Game.Entity;

namespace WorldServer.Game.AI.Fsm.States;

public class StateRoam : State
{
    public override void OnUpdate(BNpc bnpc, ulong tickCount)
    {
        var navigationProvider = bnpc.Map.NavigationProvider;
        
        navigationProvider?.SetMoveTarget(bnpc, bnpc.RoamPosition );

        if (bnpc.MoveTo(bnpc.RoamPosition))
        {
            bnpc.RoamTargetReached = true;
            bnpc.LastRoamReachedTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }

    public override void OnEnter(BNpc bnpc)
    {
        var navigationProvider = bnpc.Map.NavigationProvider;
        if (navigationProvider is null) bnpc.RoamTargetReached = true;
        bnpc.RoamPosition = navigationProvider.FindRandomPositionInCircle(bnpc.SpawnPosition, 5);

    }

    public override void OnExit(BNpc bnpc)
    {
        bnpc.RoamTargetReached = false;
    }
}