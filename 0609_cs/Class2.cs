using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0609_cs
{
    internal class Class2
    {

        static void study()
        {
            Random random = new Random();
            int[] lotto = new int[6];
            lotto.Reverse();


            for (int i = 0; i < lotto.Length; i++)
            {
                lotto[i] = random.Next(1, 45);
            }

            foreach (int num in lotto)
            {
                Console.Write(" {0}", num);
                //Task.Delay(1000).Wait();
                //Thread.Sleep(1000); 
            }

            lotto.Reverse();
            foreach (int num in lotto)
            {
                Console.Write(" {0}", num);
                //Task.Delay(1000).Wait();
                // Thread.Sleep(1000);
            }
            string[,] board = new string[6, 3];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "*";
                }
            }


            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.WriteLine("{0} {1} {2}", i, j, board[i, j]);
                }
            }
            Class1 asdfsadf = new Class1();

            int[] arr = new int[5];
            int[,] arr2 = new int[5, 5];

            Console.WriteLine("{0} ", arr2.Length);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arr2[i, j] = i * 5 + j + 1;
                }
            }
            getArr(arr2);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    board[i, j] = "*";
                }
            }


            string str = default;
            int str2 = int.Parse(Console.ReadLine());

            Console.WriteLine("반갑습니다. {0}\n\n", str2 + str2);

            Console.WriteLine("헬로월드\n");

            str = Console.ReadLine();

            Console.WriteLine("반갑습니다. {1} {0} {0} {1}\n\n", str, "asdzz");

            int a;
            str = Console.ReadLine();
            Console.WriteLine("{0}\n", str);

            /*a = System.Convert.ToInt32(str);
            Console.WriteLine("{0}\n", a);

            a = int.Parse(str);
            Console.WriteLine("{0}\n", a);*/

            int.TryParse(str, out a);
            Console.WriteLine("{0}\n", a);

            Func1();

            My my = new My();
            my.Func();
        }

        static void getArr(int[,] arr)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write("{0,2} ", arr[i, j]);
                    arr[i, j] = i * 5 + j + 1;
                }
                Console.WriteLine();
            }
            Console.WriteLine(arr.Length);
        }

        static void Func1()
        {
            Console.WriteLine("함수");
        }

    }

    public class My

    {
        int a;
        int b;

        public My()
        {
            a = 1;
            b = 1;
        }

        public My(int aa, int bb)
        {
            a = aa;
            b = bb;
        }
        public int Func()
        {
            return 0;
        }
    }
}


