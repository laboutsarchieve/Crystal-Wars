using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapFileHandler
{
    public class Map
    {
        private TileType[,] tiles;

        public Map(Point size)
        {
            tiles = new TileType[size.X, size.Y];
        }
        public Map(int x, int y)
        {
            tiles = new TileType[x, y];
        }
        public void SetAllTilesTo(TileType tile)
        {
            SetTileInAreaTo(tile, new Point(0, 0), new Point(WidthInTiles - 1, HeightInTiles - 1));
        }
        public void SetTileInAreaTo(TileType tile, Point upperLeft, Point lowerRight)
        {
            Point size = new Point(lowerRight.X - upperLeft.X, lowerRight.Y - upperLeft.Y);

            for(int x = 0; x < size.X + 1; x++)
            {
                for(int y = 0; y < size.Y + 1; y++)
                {
                    tiles[upperLeft.X + x, upperLeft.Y + y] = tile;
                }
            }
        }
        public bool isOnMap(Point position)
        {
            return (position.X > -1 && position.Y > -1 &&
                    position.X < WidthInTiles && position.Y < HeightInTiles);
        }
        public TileType this[int x, int y]
        {
            get { return tiles[x, y]; }
            set { tiles[x, y] = value; }
        }
        public int GetWidthInPixels(int tileSize)
        {
            return tileSize * tiles.GetLength(0);
        }
        public int GetHeightInPixels(int tileSize)
        {
            return tileSize * tiles.GetLength(1);
        }
        public int WidthInTiles
        {
            get { return tiles.GetLength(0); }
        }
        public int HeightInTiles
        {
            get { return tiles.GetLength(1); }
        }
    }
}