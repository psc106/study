using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0612
{

    public class Monster
    {
        protected string name;
        protected string type;
        protected int hitPoint;
        protected int manaPoint;
        protected int attackPoint;
        protected int defencePoint;

        public Monster()
        {
            this.Init("이름", "속성", 0,0,0,0);
        }

        public void Init(string name, string type, int hp, int mp, int ap, int dp)
        {
            this.name = name;
            this.type = type;
            this.hitPoint = hp;
            this.manaPoint = mp;
            this.attackPoint = ap;
            this.defencePoint = dp;
        }

        public virtual void PrintStatus()
        {
            Console.WriteLine("이름 : {0}({1})", this.name, this.type);
            Console.WriteLine("HP [{0}] MP [{1}]", this.hitPoint, this.manaPoint);
            Console.WriteLine("공격력 [{0}] 방어력 [{1}]", this.attackPoint, this.defencePoint);
        }
    }

    public class Monster1 : Monster
    {
        public Monster1()
        {
            base.Init("슬라임", "물", 10, 0, 2, 5);
        }
        public override void PrintStatus()
        {
            base.PrintStatus();
            Console.WriteLine("특기 : 분열\n");
        }
        public void PrintStatus(int a)
        {
            base.PrintStatus();
            Console.WriteLine("특기 : {0}번 분열\n", a);
        }
    }

    public class Monster2 : Monster
    {
        public Monster2()
        {
            base.Init("오크", "땅", 100, 10, 19, 2);
        }

        public override void PrintStatus()
        {
            base.PrintStatus();
            Console.WriteLine("특기 : 힘\n");
        }


    }

    public class Monster3 : Monster
    {
        public Monster3()
        {
            base.Init("밴시", "어둠", 30, 100, 30, 100);
        }

        public override void PrintStatus()
        {
            base.PrintStatus();
            Console.WriteLine("특기 : 물리면역\n");
        }
    }

}
