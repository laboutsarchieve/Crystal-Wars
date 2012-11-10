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
        private HashSet<TileType> slowTiles;
        private ActorType type;

        public Actor(Vector2 position, Vector2 size, int maxMove, ActorType actorType)
        {
            this.position = position;
            this.size = size;
            this.type = actorType;
            this.maxMove = maxMove;
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
        internal HashSet<TileType> SlowTiles
        {
            get { return slowTiles; }
            set { slowTiles = value; }
        }
        internal ActorType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
