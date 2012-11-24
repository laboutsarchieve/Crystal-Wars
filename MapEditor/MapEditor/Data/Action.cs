using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Shared.Data;

namespace MapEditor.Data
{
    class MapEditAction
    {
        private Point position;
        private TileType oldTileType;

        public MapEditAction(Point position, TileType tileType)
        {
            this.position = position;
            this.oldTileType = tileType;
        }
        public Point Position
        {
            get { return position; }
        }
        internal TileType OldTileType
        {
            get { return oldTileType; }
        }
    }
}
