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

           /* TicTacGame hw = null;
            while (true)
            {
                Console.Clear();
                hw = new TicTacGame();
                hw.Start();
            }*/


            TicTacGame1 hw1 = new TicTacGame1();
            while (hw1.Start())
            {
                Console.Clear();
                hw1.Init();
            }
        }
    }
}
