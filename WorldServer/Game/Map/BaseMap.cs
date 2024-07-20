using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Shared;
using Shared.Game;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Game.Map
{
    public abstract class BaseMap
    {
        // arbitrary number of grids, this should be changed based on navmesh generated from the client when implemented
        public const int GridBreadth = 32;

        private readonly Dictionary<Vector2G, WorldGrid> grids = new();

        protected List<Actor> Actors = [];
        public List<Player> Players => this.Actors.Where(a => a.IsPlayer).Cast<Player>().ToList();
        protected List<EventObject> EventObjects => this.Actors.Where(a => a.IsEObj).Cast<EventObject>().ToList();

        private readonly ConcurrentQueue<Actor> pendingAdd = new();
        private readonly ConcurrentQueue<Actor> pendingRemove = new();
        private readonly ConcurrentQueue<(Actor Actor, WorldPosition Position)> pendingRelocate = new();
        public long LastUpdate;
        

        private WorldGrid GetGrid(Vector2G gridCoordinates)
        {
            WorldGrid grid;
            return !grids.TryGetValue(gridCoordinates, out grid) ? ActivateGrid(gridCoordinates) : grid;
        }

        private WorldGrid ActivateGrid(Vector2G gridCoordinates)
        {
            for (int x = (gridCoordinates.X - 1).Clamp(-GridBreadth, GridBreadth); x <= (gridCoordinates.X + 1).Clamp(-GridBreadth, GridBreadth); x++)
            {
                for (int y = (gridCoordinates.Y - 1).Clamp(-GridBreadth, GridBreadth); y <= (gridCoordinates.Y + 1).Clamp(-GridBreadth, GridBreadth); y++)
                {
                    var newGridCoordinates = new Vector2G(x, y);
                    if (grids.ContainsKey(newGridCoordinates))
                        continue;

                    grids.Add(newGridCoordinates, new WorldGrid(newGridCoordinates));
                }
            }

            return grids[gridCoordinates];
        }

        public void AddActor(Actor actor)
        {
            pendingAdd.Enqueue(actor);
        }

        private void _AddActor(Actor actor)
        {
            Debug.Assert(actor.Position != null);
            Debug.Assert(actor.Map == null);

            WorldGrid grid = GetGrid(WorldGrid.GetCoord(actor.Position.Offset));
            if (grid == null)
                return;

            grid.AddActor(actor);
            
            this.Actors.Add(actor);

            actor.Map = (Territory)this;
            actor.OnAddToMap();

            if (actor.IsPlayer)
                MapManager._AddPlayer(actor.ToPlayer);
            AfterAdd(actor);
        }

        public void RemoveActor(Actor actor)
        {
            pendingRemove.Enqueue(actor);
        }

        private void _RemoveActor(Actor actor)
        {
            Debug.Assert(actor.Map != null);

            WorldGrid grid = GetGrid(WorldGrid.GetCoord(actor.Position.Offset));
            if (grid == null)
                return;

            grid.RemoveActor(actor);

            this.Actors.Remove(actor);

            actor.OnRemoveFromMap();
            actor.Map = null;

            if (actor.IsPlayer)
                MapManager._RemovePlayer(actor.ToPlayer);
            AfterRemove(actor);
        }

        protected virtual void AfterRemove(Actor actor)
        {
            
        }

        protected virtual void AfterAdd(Actor actor)
        {
            
        }

        public void RelocateActor(Actor actor, WorldPosition newPosition)
        {
            pendingRelocate.Enqueue((actor, newPosition));
        }

        private void _RelocateActor(Actor actor, WorldPosition newPosition)
        {
            Debug.Assert(actor.Map != null);

            Vector2G curGridCoord = WorldGrid.GetCoord(actor.Position.Offset);
            Vector2G newGridCoord = WorldGrid.GetCoord(newPosition.Offset);

            WorldGrid curGrid = GetGrid(WorldGrid.GetCoord(actor.Position.Offset));
            if (curGrid == null)
                return;

            if (curGridCoord != newGridCoord)
            {
                WorldGrid newGrid = GetGrid(WorldGrid.GetCoord(actor.Position.Offset));
                if (newGrid == null)
                    return;

                curGrid.RemoveActor(actor);
                newGrid.AddActor(actor);
            }
            else
                curGrid.RelocateActor(actor, newPosition.Offset);

            actor.OnRelocate(newPosition);
        }

        public void Search(WorldPosition position, float radius, SearchCheck check, out List<Actor> intersectedActors)
        {
            intersectedActors = new List<Actor>();

            var traversedCells = new Dictionary<Vector2G /*grid*/, List<Vector2G /*cell*/>>();
            for (float x = position.Offset.X - radius; x <= position.Offset.X + radius; x += WorldCell.Size)
            {
                for (float y = position.Offset.Z - radius; y <= position.Offset.Z + radius; y += WorldCell.Size)
                {
                    Vector3 searchPosition = new Vector3(x, 0f, y);

                    Vector2G gridCoord = WorldGrid.GetCoord(searchPosition);
                    if (!traversedCells.ContainsKey(gridCoord))
                        traversedCells.Add(gridCoord, []);

                    Vector2G cellCoord = WorldCell.GetCoord(gridCoord, searchPosition);
                    traversedCells[gridCoord].Add(cellCoord);
                }
            }

            foreach (KeyValuePair<Vector2G, List<Vector2G>> pair in traversedCells)
                GetGrid(pair.Key)?.Search(check, pair.Value, intersectedActors);
        }

        public void SendToAll(SubPacket subPacket)
        {
            foreach (var player in this.Players)
            {
                player.Session.Send(subPacket);
            }
        }

        public virtual void Update(double lastTick, long tickCount)
        {
            while (pendingAdd.TryDequeue(out var actor))
                _AddActor(actor);

            // relocate must be before remove to prevent relocating actors no longer in the grid
            while (pendingRelocate.TryDequeue(out var actor))
            {
                (Actor Actor, WorldPosition Position) relocate = actor;
                _RelocateActor(relocate.Actor, relocate.Position);
            }

            while (pendingRemove.TryDequeue(out var actor))
                _RemoveActor(actor);

            if (tickCount - this.LastUpdate <= 250)
            {
                foreach (var pair in grids)
                    pair.Value.Update(lastTick);
            }

            this.LastUpdate = tickCount;

        }
    }
}
