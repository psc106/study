using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0612
{
    internal class Program
    {












        static void Main(string[] args)
        {
            SecretCode sc = new SecretCode();
            while (true)
            {
                Console.Clear();
                sc.Start();
                Console.WriteLine("{0}번 만에 맞춤", sc.GetResult());
                Console.ReadKey();
            }
        }//[main] end





















        static void P()
        {

            Monster m = new Monster();
            Monster1 m1 = new Monster1();
            Monster2 m2 = new Monster2();
            Monster3 m3 = new Monster3();

            m.PrintStatus();
            Console.WriteLine();

            m1.PrintStatus();
            m2.PrintStatus();
            m3.PrintStatus();



            test monster = new test("디아블로", 100, 50, 150, 20, "악마");

            Dog dog = new Dog();

            Dog.noninstance();


            int a = 0; int b = 0;

            string[] str = new string[] { "1", "2" };


            Test(str);

            Test("1", "2");
            Console.WriteLine(a + " " + b);
            Test2(str);
            Test3(ref str);
            Test4(str, out str);

            int number = 0;
            number = ++number;
            Console.WriteLine("{0}", number);
        }

        static void Test(params string[] str)
        {
            foreach (string tmp in str)
            {
                Console.WriteLine("{0}", tmp);
                
            }
           
        }

        static void Test2(string[] str)
        {
            foreach (string tmp in str)
            {
                foreach (char c in tmp)
                {
                    Console.WriteLine("{0}", tmp);
                }
            }

        }
        static void Test3(ref string[] str)
        {

        }


        static void Test4(string[] str, out string[] outstr) 
        {
            string[] result = new string[str.Length + 1];
            for (int i = 0; i < str.Length; i++)
            {
                result[i] = str[i];
            }
            result[str.Length] = "!";
            outstr = result;

        }
    }//[program] end



    class Tests { }
}
