﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs.Hw0620
{
    public class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;

        public Location()
        {
        }
        public Location(MyObject obj)
        {
            X = obj.X;
            Y = obj.Y;
        }
    }

    public static class Algorithm
    {
        public static List<Location> Go(Enemy enemy)
        {
            Location start = new Location(enemy);
            Location target = new Location(Utility.player);

            // algorithm  
            Location current = null;
            List<Location> openList = new List<Location>();
            List<Location> closedList = new List<Location>();
            List<Location> returnList = new List<Location>();
            int g = 0;

            // start by adding the original position to the open list  
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score  
                int lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // add the current square to the closed list  
                closedList.Add(current);

                // remove it from the open list  
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path  
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;

                List<Location> adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, openList);
                g = current.G + 1;

                foreach (Location adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it  
                    if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                        continue;

                    // if it's not in the open list...  
                    if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                    {
                        // compute its score, set the parent  
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;

                        // and add it to the open list  
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score  
                        // lower, if yes update the parent because it means it's a better path  
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            Location end = current;

            while (current != null)
            {
                returnList.Add(current);
                current = current.Parent;
            }
            returnList.Reverse();
            returnList.RemoveAt(0);

            return returnList;
        }

        private static List<Location> GetWalkableAdjacentSquares(int x, int y, List<Location> openList)
        {
            List<Location> list = new List<Location>();

            if (y!=0 &&( Utility.currRoom.roomInfomation[y - 1,x] == 0 || Utility.currRoom.roomInfomation[y - 1, x] == 1))
            {
                Location node = openList.Find(l => l.X == x && l.Y == y - 1);
                if (node == null) list.Add(new Location() { X = x, Y = y - 1 });
                else list.Add(node);
            }

            if (y != Room.ROOM_SIZE-1 && (Utility.currRoom.roomInfomation[y + 1, x] == 0 || Utility.currRoom.roomInfomation[y + 1, x] == 1))

            {
                Location node = openList.Find(l => l.X == x && l.Y == y + 1);
                if (node == null) list.Add(new Location() { X = x, Y = y + 1 });
                else list.Add(node);
            }

            if (x != 0 && (Utility.currRoom.roomInfomation[y, x-1] == 0 || Utility.currRoom.roomInfomation[y, x-1] == 1))
            {
                Location node = openList.Find(l => l.X == x - 1 && l.Y == y);
                if (node == null) list.Add(new Location() { X = x - 1, Y = y });
                else list.Add(node);
            }
            if (x != Room.ROOM_SIZE - 1 && (Utility.currRoom.roomInfomation[y, x + 1] == 0 || Utility.currRoom.roomInfomation[y, x + 1] == 1))
            {
                Location node = openList.Find(l => l.X == x + 1 && l.Y == y);
                if (node == null) list.Add(new Location() { X = x + 1, Y = y });
                else list.Add(node);
            }

            return list;
        }

        private static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}
