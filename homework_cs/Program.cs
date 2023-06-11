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
            Console.SetWindowSize(40, 30);

            Deck deck = new Deck(52);
            Deck dummy = new Deck();
            
            int chip = 0;
            int round = 0;
            int target = 0;
            int difficult = 1;

            int chipSum = 0;
            int incomeSum = 0;

            deck.Swap();

            while (difficult < 2 || difficult > 4)
            {
                Console.Clear();
                Console.Write("입력(2~4) : ");
                int.TryParse(Console.ReadLine(), out difficult);
            }

            

        }
    }
}
