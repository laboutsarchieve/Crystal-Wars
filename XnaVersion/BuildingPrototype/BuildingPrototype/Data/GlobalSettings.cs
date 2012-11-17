using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor.Data
{
    static class GlobalSettings
    {
        private static Vector2 resolution;
        private static int tileSize;
        private static float scale = 1.0f;

        public static Vector2 Resolution
        {
            get { return GlobalSettings.resolution; }
            set { GlobalSettings.resolution = value; }
        }
        public static float Scale
        {
            get { return GlobalSettings.scale; }
            set { GlobalSettings.scale = Math.Max(0.1f, value); }
        }

        public static int ScaledTileSize
        {
            get { return Math.Max(1, (int)(scale * GlobalSettings.tileSize)); }
        }
        public static int TileSize
        {
            get { return GlobalSettings.tileSize; }
            set { GlobalSettings.tileSize = value; }
        }
        public static Vector2 TileResolution
        {
            get { return resolution / ScaledTileSize; }
        }
    }
}
