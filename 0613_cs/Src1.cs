using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0613_cs
{
    public class Src1
    {
    }

    public class CoinTrackerMap
    {
        private readonly string[] FIELD_STRING = { "　", "나", "ⓒ", "ㅁ" }; 

        private int[,] field;
        private int size;

        private string[] frontBuffer;
        private string[] backBuffer;

        public CoinTrackerMap(int size) 
        {
            this.size = size;
            field = new int[size+2,size+2];

            for (int i = 0; i < size+2; i++) {
                
                field[i, 0] = 0;
                field[i, (size+2)-1] = 3;

                field[0, i] = 0;
                field[(size+2)-1, i] = 3;
            }

            frontBuffer = new string[size+2];
            backBuffer = new string[size+2];
        }

        public void PrintMap()
        {
            ConsoleKeyInfo i = Console.ReadKey();
            
            for (int vertical = 0; vertical < size + 2; vertical++)
            {
                Console.WriteLine(frontBuffer[vertical]);
            }

        }
    }

    public class CoinTrackerPlayer
    {
        private int X, Y;
        public CoinTrackerPlayer(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
