using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Shared.Game;
using Shared.Game.Enum;
using Shared.Network;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Map;
using WorldServer.Network.Message;

namespace WorldServer.Game.Entity;

public abstract class Actor
{
    public uint Id { get; }
    public ActorType Type { get; }

    public AnimationData AnimationData { get; set; } = new()
    {
        HeadPosition = 0x3a,
        AnimationType = MoveType.Walking,
        AnimationState = 0,
        AnimationSpeed = (MoveSpeed)90
    };
    public WorldPosition Position { get; protected set; }
    public Territory Map { get; set; }

    public bool InWorld { get; private set; }

    public bool IsCharacter => this.Type is ActorType.Player or ActorType.Npc;
    public bool IsPlayer => Type == ActorType.Player;
    public bool IsNpc => Type == ActorType.Npc;
        
    public bool IsEObj => Type == ActorType.EObj;
        
    public Character ToChara => IsCharacter ? (Character)this : null;
    public Player ToPlayer => IsPlayer ? (Player)this : null;
    public BNpc ToBNpc => IsNpc ? (BNpc)this : null;
    public EventObject ToEObj => IsEObj ? (EventObject)this : null;

    public uint PhaseId { get; private set; }

    protected readonly HashSet<Actor> visibleActors = [];

    protected Actor(uint id, ActorType type)
    {
        Id   = id;
        Type = type;
    }

    public void AddToMap()
    {
        Debug.Assert(Position != null);
        MapManager.AddToMap(this);
    }

    public virtual void OnAddToMap()
    {
        InWorld = true;
        UpdateVision();
    }

    public void RemoveFromMap()
    {
        this.Map?.RemoveActor(this);
    }

    public virtual void OnRemoveFromMap()
    {
        // TODO: broadcast removal
        /*
        SendMessageToVisible(new ServerActorDespawn
        {
            SpawnId = 0,
            ActorId = Id
        });
        */
            
        ClearVision();
        InWorld = false;
    }

    public void Relocate(WorldPosition worldPosition)
    {
        // TODO: validate position
        Map.RelocateActor(this, worldPosition);
    }

    public virtual void OnRelocate(WorldPosition newPosition)
    {
        if (IsPlayer && newPosition.Offset != this.Position.Offset)
            this.ToPlayer.CurrentAction?.Interrupt();
            
        Position.Relocate(newPosition);
        UpdateVision();
        SendPositionUpdate();
            
            
    }

    public void SendPositionUpdate()
    {
        SendMessageToVisible(new ServerActorMove
        {
            Position = this.Position,
            AnimationData = this.AnimationData
        });
    }

    private bool CanSeeActor(Actor other)
    {
        if (PhaseId != other.PhaseId)
            return false;

        if (other.IsPlayer && (other.ToPlayer.Character.FlagsCu & PlayerFlagsCu.Invisible) != 0)
            return false;

        return true;
    }

    /// <summary>
    /// Update all visible actors in vision range.
    /// </summary>
    public void UpdateVision()
    {
        List<Actor> intersectedActors;
        Map.Search(Position, 64f, new SearchCheckRange(Position, 64f), out intersectedActors);

        var actorsToRemove = new List<Actor>(visibleActors);
        foreach (Actor actor in intersectedActors)
        {
            if (!visibleActors.Contains(actor))
            {
                AddVisibleActor(actor);
                if (actor != this)
                    actor.AddVisibleActor(this);
            }
            else
                actorsToRemove.Remove(actor);
        }

        foreach (Actor actor in actorsToRemove)
        {
                
            if (actor != this)
            {
                RemoveVisibleActor(actor);
                actor.RemoveVisibleActor(this);
            }
                    
        }
    }

    protected void ClearVision()
    {
        foreach (Actor actor in visibleActors)
            if (actor != this)
                actor.RemoveVisibleActor(this);
            
        visibleActors.Clear();
    }

    public virtual bool AddVisibleActor(Actor actor)
    {
        if (!CanSeeActor(actor))
            return false;

        visibleActors.Add(actor);
        return true;
    }

    public virtual void RemoveVisibleActor(Actor actor)
    {
        visibleActors.Remove(actor);
    }

    public void SendMessageToVisible(SubPacket subPacket, bool self = false)
    {
        if (self)
        {
            foreach (Actor actor in visibleActors.Where(a => a.IsPlayer))
                actor.ToPlayer.Session.Send(Id, actor.Id, subPacket);
        }
        else
        {
            foreach (Actor actor in visibleActors.Where(a => a.IsPlayer && a.Id != Id))
                actor.ToPlayer.Session.Send(Id, actor.Id, subPacket);
        }

    }
        
        
    public void SendMessageToRange(SubPacket subPacket, uint range = 50)
    {
        var allVisible = visibleActors.Where(a => a.IsPlayer && a.Id != Id).ToList();
        Map.Search(Position, range, new SearchCheckRange(Position, range), out var intersectedActors);
        var final = allVisible.Intersect(intersectedActors);
        foreach (Actor actor in final)
            actor.ToPlayer.Session.Send(Id, actor.Id, subPacket);

    }

    public List<Actor> GetActorsInRange()
    {
        return visibleActors.ToList();
    }

    /// <summary>
    /// Set actor phase, actor can only see and interact with other actors in the same phase.
    /// </summary>
    public void SetPhase(uint phaseId)
    {
        PhaseId = phaseId;
        if (InWorld)
            UpdateVision();
    }
}