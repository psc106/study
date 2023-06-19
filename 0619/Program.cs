using _0619.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*src2 s = new src2();
            s.another1();
            s.another2();*/
            MainSystem mainSystem = new MainSystem();
            mainSystem.start();

        }
    }

    public class src
    {
        void ssrc()
        {
            int a = 10;
            char b = '1';
            string c = "asd";

            //박싱
            object d = a;
            object f = b;
            object g = c;

            //리플렉션
            //컴파일 타임에 타입을 추론
            var h = d;  //Object형
            var j = a;  //int형

            Console.WriteLine(d.GetType());
            Console.WriteLine(f);
            Console.WriteLine(g);

            Child cc = new Child();
            cc.PrintSomething();


            Parent pp = new Parent();
            pp.PrintSomething();

            pp = cc;                //업 캐스팅
            pp.PrintSomething();

            if (pp.GetType().Equals(cc.GetType()))
            {
                Child dd = (Child)pp;       //다운 캐스팅
                Console.WriteLine(dd.char_);
                //                Console.WriteLine(pp.char_); //오류 발생
                dd.PrintSomething();
            }
            Parent ppp = new Parent();
            Child2 ddd = (Child2)ppp;
            ddd.PrintSomething();
        }
    }

    public class Parent
    {
        protected int number = default;
        protected string strValue = default;

        public virtual void PrintSomething()
        {
            number = 1;
            strValue = "부모";
            Console.WriteLine("부모 클래스"+number+"/"+strValue);
        }
    }

    public class Child : Parent
    {
        public char char_ = 'a';

        public override void PrintSomething()
        {
            number = 2;
            strValue = "자식";
            Console.WriteLine("자식 2클래스" + number + "/" + strValue);
        }
    }

    public class Child2 : Parent
    {
        public override void PrintSomething()
        {
            number = 2;
            strValue = "자식";
            Console.WriteLine("자식 클래스" + number + "/" + strValue);
        }
    }
}
