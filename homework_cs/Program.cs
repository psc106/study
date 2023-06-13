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


            CoinTrackerGame game = new CoinTrackerGame();
            while (game.start())
            {
                if (!game.IsRestart())
                {
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            /*
            Console.Clear();
            TicTacGame1 hw1 = new TicTacGame1();
            while (hw1.Start())
            {
                Console.Clear();
                hw1.Init();
            }*/
        }
    }
}
