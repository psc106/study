using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0612
{
    public struct Cat
    {
        public int leg;
        public string name;
        public string color;
    }

    public class Dog
    {
        private int leg = 4;
        private string name;
        private string color;

        public int Leg { get => leg; set => leg = value; }
        public string Name { get => name; set => name = value; }
        public string Color { get => color; set => color = value; }

        static public void noninstance()
        {
            Console.WriteLine("인스턴스 안해도 사용가능");
        }
    }

}
