using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Shared;
using WorldServer.Game.Entity;

namespace WorldServer.Game.Map
{
    public class WorldCell
    {
        public const int Size = 32;

        private readonly HashSet<Actor> actors = new();

        public static Vector2G GetCoord(Vector2G gridCoord, Vector3 position)
        {
            int cx = (int)Math.Floor(position.X / Size) - (gridCoord.X * WorldGrid.CellBreadth);
            int cy = (int)Math.Floor(position.Z / Size) - (gridCoord.Y * WorldGrid.CellBreadth);
            Debug.Assert(cx.InRange(-WorldGrid.CellBreadth, WorldGrid.CellBreadth) && cy.InRange(-WorldGrid.CellBreadth, WorldGrid.CellBreadth));

            return new Vector2G(cx, cy);
        }

        public void AddActor(Actor actor)
        {
            actors.Add(actor);
        }

        public void RemoveActor(Actor actor)
        {
            actors.Remove(actor);
        }

        public void Search(SearchCheck check, List<Actor> intersectedActors)
        {
            intersectedActors.AddRange(check != null ? actors.Where(check.CheckActor) : actors);
        }

        public void Update(double lastTick)
        {
            foreach (Actor actor in actors)
            {
                // TODO: Somehow we end up being in multiple world cells at the same time, this shouldn't be possible
                if (!actor.InWorld)
                {
                    Console.WriteLine("Workaround: Actor removed from WorldCell");
                    RemoveActor(actor);
                }
                    
                if(actor.IsCharacter)
                    actor.ToChara.Update(lastTick);
            }
        }
    }
}
