using System;
using System.Collections.Generic;
using System.Numerics;
using DotRecast.Core;
using DotRecast.Core.Numerics;
using DotRecast.Detour;
using DotRecast.Detour.Crowd;
using WorldServer.Game.Entity;

namespace WorldServer.Game.Navigation;

public class NavigationProvider
{
    private DtNavMeshQuery query;
    private DtNavMesh navmesh;
    private DtCrowd crowd;
    private List<DtCrowdAgent> agents = [];
    public NavigationProvider(DtNavMesh navMesh)
    {
        navmesh = navMesh;
        query = new DtNavMeshQuery(navmesh);
        DtCrowdConfig config = new DtCrowdConfig(0.6f);
        crowd = new DtCrowd(config, navmesh);
        DtObstacleAvoidanceParams option = new DtObstacleAvoidanceParams();
        option.velBias = 0.5f;
        option.adaptiveDivs = 5;
        option.adaptiveRings = 2;
        option.adaptiveDepth = 1;
        crowd.SetObstacleAvoidanceParams(0, option);
        option = new DtObstacleAvoidanceParams
        {
            velBias = 0.5f,
            adaptiveDivs = 5,
            adaptiveRings = 2,
            adaptiveDepth = 2
        };

        crowd.SetObstacleAvoidanceParams(1, option);
        option = new DtObstacleAvoidanceParams
        {
            velBias = 0.5f,
            adaptiveDivs = 7,
            adaptiveRings = 2,
            adaptiveDepth = 3
        };

        crowd.SetObstacleAvoidanceParams(2, option);
        option = new DtObstacleAvoidanceParams
        {
            velBias = 0.5f,
            adaptiveDivs = 7,
            adaptiveRings = 3,
            adaptiveDepth = 3
        };

        crowd.SetObstacleAvoidanceParams(3, option);
    }

    public void AddAgent(Character character)
    {
        DtCrowdAgentParams ap = new DtCrowdAgentParams();
        ap.height = 3;
        ap.maxAcceleration = 25;
        ap.maxSpeed = (float)(Math.Pow(2, character.Radius * 0.35) + 1);
        ap.radius = (float)(character.Radius * 0.75);
        ap.collisionQueryRange = ap.radius * 12;
        ap.pathOptimizationRange = ap.radius * 20; 
        ap.updateFlags = DtCrowdAgentUpdateFlags.DT_CROWD_ANTICIPATE_TURNS;
        var position = new RcVec3f(character.Position.Offset.X, character.Position.Offset.Y, character.Position.Offset.Z);
        var agent = this.crowd.AddAgent(position, ap);
        character.CrowdAgent = agent;
        this.agents.Add(agent);
    }

    public void RemoveAgent(Character character)
    {
        this.crowd.RemoveAgent(character.CrowdAgent);
    }

    public void SetMoveTarget(Character character, Vector3 pos)
    {
        RcVec3f ext = crowd.GetQueryExtents();
        IDtQueryFilter filter = crowd.GetFilter(0);
        query.FindNearestPoly(new RcVec3f(pos.X,pos.Y, pos.Z), ext, filter, out var nearestRef, out var nearestPt, out _);
        crowd.RequestMoveTarget(character.CrowdAgent, nearestRef, nearestPt);
    }

    public Vector3 FindRandomPositionInCircle(Vector3 pos, float radius)
    {
        RcRand f = new RcRand();
        var pos1 = new RcVec3f(pos.X, pos.Y, pos.Z);
        RcVec3f ext = new RcVec3f(30, 60, 30);
        IDtQueryFilter filter = new DtQueryDefaultFilter();
        query.FindNearestPoly(pos1, ext, filter, out var nearestRef, out var nearestPt, out _);
        this.query.FindRandomPointAroundCircle(nearestRef, pos1, radius, filter, f, out var nextRandomRef, out var nextRandomPt);
        return new Vector3(nextRandomPt.X, nextRandomPt.Y, nextRandomPt.Z);
    }
    
    public void Update(float dt)
    {
        this.crowd.Update(dt, null);
    }

    public void UpdateAgentPosition(Character character)
    {
        RemoveAgent(character);
        AddAgent(character);
    }
}