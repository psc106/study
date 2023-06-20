using homework_0616;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace homework_cs.Hw0616
{
    public class GameManager
    {
        GameSystem system;

        GameBuffer buffer;
        GamePlayer player;

        int mode = 0;

        public GameManager()
        {

            DataManager.ITEM_DATABASE = new Dictionary<int, GameItem>();
            DataManager.ITEM_DATABASE.Add(1, new GameItem(1, "아이템1", "1번 아이템입니다.", 109));
            DataManager.ITEM_DATABASE.Add(2, new GameItem(2, "아이템2", "2번 아이템입니다.", 5));
            DataManager.ITEM_DATABASE.Add(3, new GameItem(3, "아이템3", "3번 아이템입니다.", 35));
            DataManager.ITEM_DATABASE.Add(4, new GameItem(4, "아이템4", "4번 아이템입니다.", 71));
            DataManager.ITEM_DATABASE.Add(5, new GameItem(5, "아이템5", "5번 아이템입니다.", 19));
            DataManager.ITEM_DATABASE.Add(6, new GameItem(6, "아이템6", "6번 아이템입니다.", 88));
            DataManager.ITEM_DATABASE.Add(7, new GameItem(7, "아이템7", "7번 아이템입니다.", 250));
            DataManager.ITEM_DATABASE.Add(8, new GameItem(8, "아이템8", "8번 아이템입니다.", 20));
            DataManager.ITEM_DATABASE.Add(9, new GameItem(9, "아이템9", "9번 아이템입니다.", 10));
            DataManager.ITEM_DATABASE.Add(10, new GameItem(10, "아이템10", "10번 아이템입니다.", 0));

            Console.WindowHeight = 60;
            Console.WindowWidth = 60;

            player = new GamePlayer(5, 5, 0);
            buffer = new GameBuffer();

        }

        public void Start()
        {
            player = new GamePlayer(5, 5, 0);
            GameMap map;

            while (true)
            {
                player.X = 5;
                player.Y = 5;
                player.direction = 0;

                if (mode == 0)
                {
                    system = new PortalSystem(ref player);
                    map = system.map;
                    Console.Clear();
                    ((PortalSystem)system).printTimer = new Timer(PrintMap, map, 100, 100);
                }


                else if (mode == 1)
                {
                    system = new CardSystem(ref player);
                }


                else if (mode == 2)
                {
                    system = new ShopSystem(ref player);
                }


                else if (mode == 3)
                {
                    system = new BattleSystem(ref player);
                }



                mode = system.StartThisMode();
                Thread.Sleep(100);
            }
        }

        public void PrintMap(Object obj)
        {
            if (obj != null)
            {
                buffer.SetBuffer(((BufferHelp)obj).MapToStringArray(player.direction));
                buffer.PrintBuffer();
            }

            else
            {
                Console.Clear();
            }
        }

    }
}
