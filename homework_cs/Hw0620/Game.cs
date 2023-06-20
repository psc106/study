
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace homework_cs.Hw0620
{
    public class Game
    {
        public void Start()
        {
            


        }
    }


    public class Map 
    {
        Room[,] field;
        int currFieldX;
        int currFieldY;
    }

    public class Room
    {
        Door[] portal = new Door[4];

    }

    public class Door
    {
        public int position;
    }

    //포탈 시스템
    public class MoveSystem
    {
        public MoveSystem()
        {
        }

        public MoveSystem(Player player)
        {
            this.player = player;

            map = new PortalMap(player);

            Random rand = new Random();

            nextPortal = new Portal[2];

            while (true)
            {
                for (int i = 0; i < nextPortal.Length; i++)
                {
                    int mode = rand.Next(1, 4);

                    int x = rand.Next(0, 10);
                    int y = rand.Next(0, 10);

                    if ((x != player.X || y != player.Y))
                    {
                        nextPortal[i] = new Portal(x, y, mode);
                    }
                }

                if (nextPortal[0].X != nextPortal[1].X || nextPortal[0].Y != nextPortal[1].Y)
                {
                    break;
                }
            }

            map.SetFieldObject(player);
            for (int i = 0; i < nextPortal.Length; i++)
            {
                map.SetFieldObject(nextPortal[i]);
            }

        }

        //시스템 내용 구현
        public override int StartThisMode()
        {

            Console.Clear();

            bool isMove = false;
            bool isNo = false;
            int direction = 0;

            while (true)
            {
                Console.CursorVisible = false;
                isMove = false;
                isNo = false;


                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction = 1;
                        isMove = true;
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        direction = 2;
                        isMove = true;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction = 3;
                        isMove = true;
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        direction = 4;
                        isMove = true;
                        break;

                    case ConsoleKey.X:
                        isNo = true;
                        break;

                    default:
                        direction = 0;
                        break;
                }

                if (isMove)
                {
                    int beforeX = player.X;
                    int beforeY = player.Y;

                    player.direction = direction;
                    player.Move();
                    player.Hold(map.field.GetLength(0));

                    int currX = player.X;
                    int currY = player.Y;

                    for (int i = 0; i < nextPortal.Length; i++)
                    {

                        if (nextPortal[i].X == currX && nextPortal[i].Y == currY)
                        {
                            map = null;
                            printTimer.Dispose();

                            return nextPortal[i].objectID;
                        }
                    }

                    map.SetFieldAt(beforeX, beforeY, 0);
                    map.SetFieldObject(player);
                }

                if (isNo)
                {
                    this.printTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                    Thread.Sleep(500);
                    InventorySystem inventorySystem = new InventorySystem();
                    inventorySystem.Open(ref player);
                    this.printTimer.Change(100, 100);
                }



            }
        }

    }
}
