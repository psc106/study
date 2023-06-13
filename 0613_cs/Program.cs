using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0613_cs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CoinTrackerGame game = new CoinTrackerGame();

            while (game.start())
            {
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
