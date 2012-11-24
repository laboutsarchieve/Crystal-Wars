using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Shared.Data
{
    public class ActorDatabase
    {
        private List<Actor> actors;

        public ActorDatabase()
        {
            actors = new List<Actor>();
            Clear();
        }
        public Actor GetActorAt(Vector2 location)
        {
            foreach(Actor actor in actors)
            {
                if(actor.Position == location)
                    return actor;
            }

            return Actor.DummyActor;
        }
        public void AddActor(Actor actor)
        {
            actors.Add(actor);
        }
        public void RemoveAllActorsOn(Point map)
        {
            actors = new List<Actor>();
        }
        public void RemoveActor(Actor actor, Point map)
        {
            actors.Remove(actor);
        }
        public void Clear()
        {
            actors = new List<Actor>();
        }
        public List<Actor> Actors
        {
            get { return actors; }
            set { actors = value; }
        }
    }
}
