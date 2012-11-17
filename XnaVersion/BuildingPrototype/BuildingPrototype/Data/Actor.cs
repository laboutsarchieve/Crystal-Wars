using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor.Data
{
    class Actor
    {
        private Vector2 position;
        private Vector2 size;
        private int maxMove;
        private Dictionary<TileType, int> TileCost;
        private ActorType type;

        private static Actor dummyActor = new Actor(-Vector2.One, Vector2.Zero, 0, ActorType.Dummy);

        public Actor(Vector2 position, Vector2 size, int maxMove, ActorType actorType)
        {
            this.position = position;
            this.size = size;
            this.type = actorType;
            this.maxMove = maxMove;

            TileCost = ActorInfo.GetTileCosts(actorType);
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }
        public int MaxMove
        {
            get { return maxMove; }
        }
        internal ActorType Type
        {
            get { return type; }
            set { type = value; }
        }
        public static Actor DummyActor
        {
            get
            {
                return dummyActor;
            }
        }

        internal int MovementOn(TileType tile)
        {
            if(!TileCost.ContainsKey(tile))
                return 1000;
            else
                return TileCost[tile];
        }
    }
}
