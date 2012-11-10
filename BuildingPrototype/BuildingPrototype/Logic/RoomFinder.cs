using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Data;

namespace MapEditor.Logic
{
    static class RoomFinder
    {
        private static Map map;

        public static void AddRoomsToMap(Map buildingMap, Rectangle searchArea)
        {
            map = buildingMap;

            for(int x = searchArea.X; x < searchArea.X + searchArea.Width; x++)
            {
                for(int y = searchArea.Y; y < searchArea.Y + searchArea.Height; y++)
                {
                    Point currentPosition = new Point(x, y);

                    if(!map.isOnMap(currentPosition) || IsAlreadyInRoom(currentPosition))
                        continue;

                    TileType currentTile = map[x, y];

                    if(IsInside(currentTile))
                        AddRoomsToMap(currentPosition);
                }
            }
        }
        private static void AddRoomsToMap(Point startPosition)
        {
            // This methods uses a goaless Breadth First Search to find every tile in the room
            HashSet<Point> explored = new HashSet<Point>();
            Queue<Point> toExplore = new Queue<Point>();

            toExplore.Enqueue(startPosition);
            explored.Add(startPosition);

            Point upperLeft = new Point(int.MaxValue, int.MaxValue);
            Point lowerRight = Point.Zero;

            while(toExplore.Count > 0)
            {
                Point newPoint = toExplore.Dequeue();

                if(newPoint.X < upperLeft.X)
                    upperLeft.X = newPoint.X;
                if(newPoint.Y < upperLeft.Y)
                    upperLeft.Y = newPoint.Y;

                if(newPoint.X > lowerRight.X)
                    lowerRight.X = newPoint.X;
                if(newPoint.Y > lowerRight.Y)
                    lowerRight.Y = newPoint.Y;

                AddAdjacentTiles(toExplore, explored, newPoint);
            }

            Rectangle newRoomArea = new Rectangle(upperLeft.X, upperLeft.Y, lowerRight.X - upperLeft.X + 1, lowerRight.Y - upperLeft.Y + 1);
            List<Point> pointsInRoom = explored.ToList<Point>();
            map.AddRoom(new Room(pointsInRoom, map));
        }
        private static void AddAdjacentTiles(Queue<Point> toExplore, HashSet<Point> explored, Point newPoint)
        {
            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    if(x == 0 && y == 0)
                        continue;

                    Point pointToAdd = new Point(newPoint.X + x, newPoint.Y + y);
                    if(map.isOnMap(pointToAdd) && IsInside(map[pointToAdd.X, pointToAdd.Y]) && !explored.Contains(pointToAdd))
                    {
                        toExplore.Enqueue(pointToAdd);
                        explored.Add(pointToAdd);
                    }
                }
            }
        }
        private static bool IsInside(TileType tile)
        {
            return tile == TileType.Floor;
        }
        private static bool IsAlreadyInRoom(Point point)
        {
            foreach(Room room in map.Rooms)
            {
                if(room.IsInRoom(point))
                    return true;
            }

            return false;
        }
    }
}
