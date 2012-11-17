using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Data;

namespace MapEditor.Logic
{
    class Pathfinder
    {
        class Node
        {
            Node parent;
            Point location;
            float costSoFar;



            public Node(Point start, float stepCost)
            {
                parent = null;
                location = start;
            }
            public Node(Node previousNode, Point newStep, float stepCost)
            {
                parent = previousNode;
                location = newStep;

                if(parent == null)
                    costSoFar = stepCost;
                else
                    costSoFar = stepCost + parent.costSoFar;
            }

            public float CostSoFar
            {
                get { return costSoFar; }
            }
            public Point Location
            {
                get { return location; }
            }
            public Node Parent
            {
                get { return parent; }
            }
        }

        public static List<Point> GetPointsInRange(Map map, Actor actor)
        {
            Node start = new Node(new Point((int)actor.Position.X, (int)actor.Position.Y), 0);

            List<Point> inRange = new List<Point>();
            Stack<Node> toExplore = new Stack<Node>();

            toExplore.Push(start);

            while(toExplore.Count > 0)
            {
                Node currentNode = toExplore.Pop();
                Point currentPoint = currentNode.Location;

                for(int x = -1; x < 2; x++)
                {
                    for(int y = -1; y < 2; y++)
                    {
                        Point pointToExamine = new Point(currentPoint.X + x, currentPoint.Y + y);
                        if((x == 0 && y == 0) || (x != 0 && y != 0) || !map.isOnMap(pointToExamine))
                            continue;


                        TileType tile = map[pointToExamine.X, pointToExamine.Y];
                        int stepCost = actor.MovementOn(tile);

                        if(currentNode.CostSoFar + stepCost < actor.MaxMove + 1)
                        {
                            if(!inRange.Contains(pointToExamine))
                                inRange.Add(pointToExamine);
                            toExplore.Push(new Node(currentNode, pointToExamine,stepCost));                            
                        }
                    }
                }
            }

            return inRange;
        }
    }
}
