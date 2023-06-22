using homework_cs.Hw0620;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace homework_cs
{
    internal class Program
    {

        static void Main(string[] args)
        {
            /*GameManager game = new GameManager();
            game.Start();*/

            Game game = new Game();
            game.Start();
            while (true)
            {
            }
        }


        //static void Main(string[] args)
        //{
        /* ConsoleKeyInfo cki;

         do
         {
             Console.WriteLine("\nPress a key to display; press the 'x' key to quit.");

             // Your code could perform some useful task in the following loop. However,
             // for the sake of this example we'll merely pause for a quarter second.

             while (Console.KeyAvailable == false)
             {
                 Console.WriteLine("TEST");

                 Thread.Sleep(1050); // Loop until input is entered.
             }
             cki = Console.ReadKey(true);
             Console.WriteLine("You pressed the '{0}' key.", cki.Key);
         } while (cki.Key != ConsoleKey.X);*/

        //MiniWorldGame game = new MiniWorldGame();
        //game.Start();

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
        /*
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
                    }*/
        //}
    }
}
