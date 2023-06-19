using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619
{
    public partial class src2 : src
    {
        public int v = 1;
        public void another1()
        {
            Console.WriteLine("어나더1");

        }

    }

    public partial class src2
    {
        public void another2()
        {
            Console.WriteLine("어나더2");

            int a = 4;
            Console.WriteLine( a.Adddd(this));
        }

        

    }

    public partial class src3 : src2
    {
        public void another3()
        {
            Console.WriteLine("어나더2");

            int a = 4;
        }



    }
    public static class ExtendedMethod
    {
        public static void ExtandPrint(this src2 s)
        {
            s.another1();
        }
        public static int Adddd(this int s, src2 a)
        {
            return s + a.v;
        }

    }
}
