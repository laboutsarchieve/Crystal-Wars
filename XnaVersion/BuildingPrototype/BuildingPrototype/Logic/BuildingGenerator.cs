using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Data;

namespace MapEditor.Logic
{
    // TODO: Complete overhaul that tracks which tiles are in which room
    class BuildingGenerator
    {
        private Random rng;
        private Point buildingMapSize;
        private Point minBuildingSize;
        private Point maxBuildingSize;
        private float maxRoomDimensionRatio;
        private int maxRoomPerBuilding;

        public BuildingGenerator()
        {
            rng = new Random();
        }
        public void PlaceBuildings(Map map)
        {

        }
        public void PlaceBuilding(Map buildingMap, Point upperLeft, Point minSize, Point maxSize, int maxRoomNum)
        {
            Point buildingSize = PlaceRoom(buildingMap, upperLeft, minSize, maxSize, true);
            Point roomUpperLeft = upperLeft;
            Point roomSize = PlaceRoom(buildingMap, roomUpperLeft, new Point(buildingSize.X / 2, buildingSize.Y / 2), new Point(2 * buildingSize.X / 3, 2 * buildingSize.Y / 3), false);

            if(rng.NextDouble() > 0.5)
            {
                roomUpperLeft.X += roomSize.X;
                PlaceRoom(buildingMap, roomUpperLeft, new Point(buildingSize.X - roomSize.X, buildingSize.Y / 4), new Point(buildingSize.X - roomSize.X, buildingSize.X - roomSize.X), false);
            }
            else
            {
                roomUpperLeft.Y += roomSize.Y;
                PlaceRoom(buildingMap, roomUpperLeft, new Point(buildingSize.X / 4, buildingSize.Y - roomSize.Y), new Point(buildingSize.X - roomSize.X, buildingSize.Y - roomSize.Y), false);
            }

            RoomFinder.AddRoomsToMap(buildingMap, new Rectangle(upperLeft.X, upperLeft.Y, buildingSize.X, buildingSize.Y));

            ConnectRooms(buildingMap);
            PlaceOutsideDoor(buildingMap, upperLeft, buildingSize);
        }
        private void PlaceOutsideDoor(Map buildingMap, Point upperLeft, Point buildingSize)
        {
            Side side = (Side)rng.Next(0, 4);
            PlaceOutsideDoor(buildingMap, upperLeft, buildingSize, side);
        }
        private void PlaceOutsideDoor(Map buildingMap, Point upperLeft, Point buildingSize, Side side)
        {
            switch(side)
            {
                case Side.Bottom:
                    {

                        int xOffset = rng.Next(upperLeft.X, upperLeft.X + buildingSize.X);
                        buildingMap[xOffset, upperLeft.Y + buildingSize.Y] = TileType.Door;
                        break;
                    }
                case Side.Left:
                    {
                        int yOffset = rng.Next(upperLeft.Y, upperLeft.Y + buildingSize.Y);
                        buildingMap[upperLeft.X, yOffset] = TileType.Door;
                        break;
                    }
                case Side.Right:
                    {
                        int yOffset = rng.Next(upperLeft.Y, upperLeft.Y + buildingSize.Y);
                        buildingMap[upperLeft.X + buildingSize.X, yOffset] = TileType.Door;
                        break;
                    }
                case Side.Top:
                    {
                        int xOffset = rng.Next(upperLeft.X, upperLeft.X + buildingSize.X);
                        buildingMap[xOffset, upperLeft.Y] = TileType.Door;
                        break;
                    }
            }
        }
        private void ConnectRooms(Map buildingMap)
        {
            Room centralRoom = buildingMap.Rooms[rng.Next(buildingMap.Rooms.Count)];

            foreach(Room room in buildingMap.Rooms)
            {
                if(room == centralRoom)
                    continue;

                Point start = centralRoom.PointsInRoom[centralRoom.PointsInRoom.Count / 2]; // This will tend to be a central point in the room
                Point end = room.PointsInRoom[room.PointsInRoom.Count / 2];

                if(start.X < end.Y)
                {
                    for(int x = start.X; x < end.X + 1; x++)
                        if(buildingMap[x, start.Y] == TileType.Wall)
                            buildingMap[x, start.Y] = TileType.Door;
                }
                else
                {
                    for(int x = start.X; x > end.X - 1; x--)
                        if(buildingMap[x, start.Y] == TileType.Wall)
                            buildingMap[x, start.Y] = TileType.Door;
                }

                if(start.Y < end.Y)
                {
                    for(int y = start.Y; y < end.Y + 1; y++)
                        if(buildingMap[end.X, y] == TileType.Wall)
                            buildingMap[end.X, y] = TileType.Door;
                }
                else
                {
                    for(int y = start.Y; y > end.Y - 1; y--)
                        if(buildingMap[end.X, y] == TileType.Wall)
                            buildingMap[end.X, y] = TileType.Door;
                }

            }
        }
        public Point PlaceRoom(Map buildingMap, Point upperLeft, Point minSize, Point maxSize, bool mustBeClear) // Returns the size of the generated room
        {
            int roomWidth = rng.Next(minSize.X, maxSize.X + 1);
            int roomHeight = roomWidth;

            // TODO: Refactor this logic into its own method
            if(rng.NextDouble() > 0.5)
            {
                roomWidth = (int)((1 + (rng.NextDouble() * (maxRoomDimensionRatio - 1))) * roomHeight); // Larger Width
                roomWidth = Math.Min(maxSize.X, roomWidth);
                roomHeight = Math.Min(maxSize.Y, roomHeight);
            }
            else
            {
                roomHeight = (int)((1 + (rng.NextDouble() * (maxRoomDimensionRatio - 1))) * roomWidth); // Larger Height
                roomWidth = Math.Min(maxSize.X, roomWidth);
                roomHeight = Math.Min(maxSize.Y, roomHeight);
            }

            Point roomSize = new Point(roomWidth, roomHeight);

            if(mustBeClear)
            {
                while(!IsAreaClear(buildingMap, upperLeft, roomSize))
                {
                    roomSize.X--;
                    roomSize.Y--;

                    if(roomSize.X < minSize.X || roomSize.Y < minSize.Y)
                        return Point.Zero;
                }
            }

            buildingMap.SetTileInAreaTo(TileType.Floor, upperLeft, new Point(upperLeft.X + roomSize.X, upperLeft.Y + roomSize.Y));

            buildingMap.SetTileInAreaTo(TileType.Wall, upperLeft, new Point(upperLeft.X + roomSize.X, upperLeft.Y)); // Top Wall
            buildingMap.SetTileInAreaTo(TileType.Wall, upperLeft, new Point(upperLeft.X, upperLeft.Y + roomSize.Y)); // Left Wall

            buildingMap.SetTileInAreaTo(TileType.Wall, new Point(upperLeft.X + roomSize.X, upperLeft.Y), new Point(upperLeft.X + roomSize.X, upperLeft.Y + roomSize.Y)); // Bottom Wall
            buildingMap.SetTileInAreaTo(TileType.Wall, new Point(upperLeft.X, upperLeft.Y + roomSize.Y), new Point(upperLeft.X + roomSize.X, upperLeft.Y + roomSize.Y)); // Right Wall

            return roomSize;
        }
        private bool IsAreaClear(Map buildingMap, Point upperLeft, Point size)
        {
            for(int x = 0; x < size.X; x++)
            {
                for(int y = 0; y < size.Y; y++)
                {
                    if(buildingMap[upperLeft.X + x, upperLeft.Y + y] != TileType.Grass)
                        return false;
                }
            }

            return true;
        }

        public Point BuildingMapSize
        {
            get { return buildingMapSize; }
            set { buildingMapSize = value; }
        }
        public Point MinBuildingSize
        {
            get { return minBuildingSize; }
            set { minBuildingSize = value; }
        }
        public Point MaxBuildingSize
        {
            get { return maxBuildingSize; }
            set { maxBuildingSize = value; }
        }
        public float MaxRoomDimensionRatio
        {
            get { return maxRoomDimensionRatio; }
            set { maxRoomDimensionRatio = value; }
        }
        public int MaxRoomPerBuilding
        {
            get { return maxRoomPerBuilding; }
            set { maxRoomPerBuilding = value; }
        }
    }
}
