using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0614
{
    public class Program
    {
        static void Main(string[] args)
        {
           // TimeTest t = new TimeTest();
            //t.start();

            List<int> iList = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                iList.Add(i);
                Console.WriteLine(iList.ElementAt(i));
            }

            Console.WriteLine("=================");
            for (int i = 0; i < iList.Count; i++)
            {
                /*if (i % 2 == 0)
                {
                    iList.RemoveAt(i);
                }*/
                /*if (iList.ElementAt(i) % 2 == 0)
                {
                    iList.RemoveAt(i);
                }*/
            }
            Console.WriteLine("=================");

            for (int i = 0; i < iList.Count; i++)
            {
                Console.WriteLine(iList.ElementAt(i));
            }

            List<data> list = new List<data>();
            list.Add(new data());
            list.Add(new data());
            list.Add(new data(4,5,"a"));
            list.Add(null);
            list.Add(new data());
            list.RemoveAt(4);
            Stack<int> st = new Stack<int>();
            st.Push(1);
            st.Pop();

            SortedSet<int> set = new SortedSet<int>();
            set.Add(1);
            set.Add(2);
            set.Remove(3);

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("1", 1);
            dic.Add("2", 1);
            dic.Add("3", 3);
            dic.Add("12342342345233aefwsawsefefawefqa23wqvb5 aw4", 1);
            dic.Add("4", 4);
            dic.Add("5", 1);

            for (int i = 0; i < dic.Count; i++)
            {
                Console.WriteLine(dic.ElementAt(i));
            }

            foreach (KeyValuePair<string, int> pair in dic) 
            {
                Console.WriteLine(pair.Key+"/"+pair.Value);
            }
        }

    }

    class data
    {
        int x;
        int y;
        string a;

        public data()
        {
            this.x = 1;
            this.y = 2;
            this.a = "zz";

        }
        public data(int x, int y, string a) 
        {
            this.x = x;
            this.y = y;
            this.a = a;
            
        }
    }

    public class TimeTest
    {
        int a = default;
        public void start()
        {
            Timer t = new Timer(time, a, 100, 1000);

            while (true)
            {
                int.TryParse(Console.ReadLine(), out a);
                Console.WriteLine(a);
            }
        }

        void time(object a)
        {
            Console.WriteLine(a);
        }
    }
}
