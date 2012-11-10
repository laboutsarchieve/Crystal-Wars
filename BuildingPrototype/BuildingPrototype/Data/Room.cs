using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor.Data
{
    class Room
    {
        private static Random rng = new Random();
        private List<Point> pointsInRoom;
        private Map parentMap;

        public Room(List<Point> pointsInRoom, Map parentMap)
        {
            this.pointsInRoom = pointsInRoom;
            this.parentMap = parentMap;
        }
        public bool IsInRoom(Point position)
        {
            return pointsInRoom.Contains(position);
        }
        public Point GetRandomPointInRoom()
        {
            return pointsInRoom[rng.Next(pointsInRoom.Count)];
        }
        public List<Point> PointsInRoom
        {
            get { return pointsInRoom; }
            set { pointsInRoom = value; }
        }
        internal Map ParentMap
        {
            get { return parentMap; }
            set { parentMap = value; }
        }
    }
}
