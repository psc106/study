using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace homework_cs.Hw0620
{
    public class MyObject
    {
        public int X; 
        public int Y;

        public MyObject() { }
        public MyObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }

    public class MyMoveObject : MyObject
    {
        public bool isLive;

        protected int[] AXIS_X = { 1, -1, 0, 0, 0,0 };
        protected int[] AXIS_Y = { 0, 0, -1, 1, 0,0 };

        public int hitPoint;

        public int GetNextX(int direction)
        {
            return X + AXIS_X[direction];
        }

        public int GetNextY(int direction)
        {
            return Y + AXIS_Y[direction];
        }

        public void MoveAndHold(int direction, int XSize, int YSize)
        {
            X += AXIS_X[direction];
            Y += AXIS_Y[direction];

            HoldX(XSize);
            HoldY(YSize);
        }


        public void HoldX(int XSize)
        {
            if (X < 0)
            {
                X = 0;
            }
            else if (X >= XSize)
            {
                X = XSize - 1;
            }

        }
        public void HoldY(int YSize)
        {
            if (Y < 0)
            {
                Y = 0;
            }
            else if (Y >= YSize)
            {
                Y = YSize - 1;
            }
        }

        public void HoldXY(ref int x, ref int y, int sizeX, int sizeY)
        {
            if (x < 0)
            {
                x = 0;
            }
            else if (x >= sizeX)
            {
                x = sizeX - 1;
            }
            if (y < 0)
            {
                y = 0;
            }
            else if (y >= sizeY)
            {
                y = sizeY - 1;
            }

        }
        public void Teleport(int XSize, int Ysize)
        {
            if (X == 0)
            {
                X = XSize - 1;
            }
            else if (X == XSize - 1)
            {
                X = 0;
            }
            else if (Y == 0)
            {
                Y = Ysize - 1;
            }
            else if (Y == Ysize - 1)
            {
                Y = 0;
            }
        }
    }

    public class Player : MyMoveObject
    {
        public Timer attackTimer;

        public int score;
        public bool isCoolTime;

        public Player() { }
        public Player(int x, int y)
        {
            isCoolTime = false;
            isLive = true;
            X = x;
            Y = y;
            score = 0;
            hitPoint = 3;

        }

        public void AttackTimer(object obj)
        {
            isCoolTime = false;
            attackTimer.Dispose();
        }

    }

    public class NonPlayableCharacter : MyMoveObject
    {
        public Timer walkTimer;
        public int npcID;

        public NonPlayableCharacter() { }
        public NonPlayableCharacter(int x, int y)
        {
            isLive = true;
            X = x;
            Y = y;
            hitPoint = 1;
            npcID = -1;
        }
        public NonPlayableCharacter(int x, int y, int id)
        {
            isLive = true;
            X = x;
            Y = y;
            hitPoint = 20;
            npcID = id;
        }

        public void WalkTimer(object obj)
        {
            int direction = Utility.random.Next(0, 5);

            int nextX = X + AXIS_X[direction];
            int nextY = Y + AXIS_Y[direction];

            HoldX(Room.ROOM_SIZE);
            HoldY(Room.ROOM_SIZE);
        }
    }

    public class Enemy : MyMoveObject
    {
        public Timer enemyMoveTimer;

        public int randMoveCount;

        public bool playerChange;

        List<Location> path;

        public Enemy(int x, int y)
        {
            hitPoint = 2;
            X = x;
            Y = y;
            isLive = false;
            randMoveCount = 0;
            playerChange = true;
            path = new List<Location>();

            StartTimer();
        }
        public void StartTimer()
        {
            enemyMoveTimer = new Timer(MoveTimer, null, 3000, 600);
        }

        public void MoveTimer(object obj)
        {
            if (!isLive)
            {
                isLive = true;
            }

            if (playerChange)
            {
                path = Algorithm.Go(this);
                playerChange = false;
            }


            if (path == null || path.Count == 0) return;
            Location next = path[0];

            if (Utility.currRoom.GetElementAt(next.X, next.Y) != 2 && Utility.currRoom.FindEnemiesAt(next.X, next.Y) == null)
            {
                this.X = next.X;
                this.Y = next.Y;
                path.RemoveAt(0);
            }
            else
            {
            }


            Player player = Utility.player;

            /*
                        int nextX = this.X;
                        int nextY = this.Y;

                        if (randMoveCount > 0)
                        {
                            nextX = this.X + AXIS_X[Utility.random.Next(4)];
                            nextY = this.Y + AXIS_Y[Utility.random.Next(4)];

                            nextX = HoldX(nextX, Room.ROOM_SIZE);
                            nextY = HoldY(nextY, Room.ROOM_SIZE);

                            if (Utility.currRoom.GetElementAt(nextX, nextY) != 2 && Utility.currRoom.FindEnemiesAt(nextX, nextY) == null)
                            {
                                this.X = nextX;
                                this.Y = nextY;
                            }

                            nextX = this.X + AXIS_X[Utility.random.Next(4)];
                            nextY = this.Y + AXIS_Y[Utility.random.Next(4)];

                            nextX = HoldX(nextX, Room.ROOM_SIZE);
                            nextY = HoldY(nextY, Room.ROOM_SIZE);

                            if (Utility.currRoom.GetElementAt(nextX, nextY) != 2 && Utility.currRoom.FindEnemiesAt(nextX, nextY) == null)
                            {
                                this.X = nextX;
                                this.Y = nextY;
                            }

                            randMoveCount -= 1;
                        }

                        if (Utility.random.Next(2) % 2 == 0)
                        {
                            if (player.X < this.X)
                            {
                                nextX = this.X - 1;
                            }
                            else if (player.X > this.X)
                            {
                                nextX = this.X + 1;
                            }
                            else
                            {
                                if (player.Y < this.Y)
                                {
                                    nextY = this.Y - 1;
                                }
                                else if (player.Y > this.Y)
                                {
                                    nextY = this.Y + 1;
                                }
                            }

                            nextX = HoldX(nextX, Room.ROOM_SIZE);
                            nextY = HoldY(nextY, Room.ROOM_SIZE);
                        }
                        else
                        {
                            if (player.Y < this.Y)
                            {
                                nextY = this.Y - 1;
                            }
                            else if (player.Y > this.Y)
                            {
                                nextY = this.Y + 1;
                            }
                            else
                            {
                                if (player.X < this.X)
                                {
                                    nextX = this.X - 1;
                                }
                                else if (player.X > this.X)
                                {
                                    nextX = this.X + 1;
                                }
                            }
                            nextX = HoldX(nextX, Room.ROOM_SIZE);
                            nextY = HoldY(nextY, Room.ROOM_SIZE);
                        }

                        if (Utility.currRoom.GetElementAt(nextX, nextY) != 2 && Utility.currRoom.FindEnemiesAt(nextX, nextY) == null)
                        {
                            this.X = nextX;
                            this.Y = nextY;
                        }
                        else
                        {
                            randMoveCount += 2;
                        }*/

            if (player.X == this.X && player.Y == this.Y)
            {
                player.hitPoint -= 1;
                if (player.hitPoint == 0)
                {
                    player.isLive = false;
                    enemyMoveTimer.Dispose();
                    return;
                }

            }
        }

        public int HoldX(int x, int XSize)
        {
            if (x < 0)
            {
                x = 0;
            }
            else if (x >= XSize)
            {
                x = XSize - 1;
            }
            return x;


        }
        public int HoldY(int y, int YSize)
        {
            if (y < 0)
            {
                y = 0;
            }
            else if (y >= YSize)
            {
                y = YSize - 1;
            }
            return y;
        }
      
    }
}
