using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs
{
    internal class Program
    {


        static void Main(string[] args)
        {


            /*CoinTrackerGame game = new CoinTrackerGame();
            while (game.start())
            {
                if (!game.IsRestart())
                {
                    Console.ReadKey();
                    Console.Clear();
                }
            }*/

            /* Console.Clear();
             TicTacGame1 hw1 = new TicTacGame1();
             while (hw1.Start())
             {
                 Console.Clear();
                 hw1.Init();
             }*/

            /*ShopSystem ss = new ShopSystem();
            ss.start();*/

            Sokoban skb = null;
            int size = 0;

            bool isStart = true;
            while (isStart)
            {
                if (skb == null || !skb.isRestart)
                {
                    while (size < 5 || size >= 30) { int.TryParse(Console.ReadLine(), out size); }
                    skb = new Sokoban(size);
                }
                isStart = skb.Start();
            }
        }
    }
}
