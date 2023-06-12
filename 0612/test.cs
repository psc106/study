using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0612
{
    internal class test
    {

        private string monsterName = "디아블로";
        private int Hp = 100;
        private int Mp = 20;
        private int damage = 150;
        private int defence = 50;
        private string monsterType = "Demon";



        public test(string monsterName_, int Hp_,int Mp_,int damage_,int defence_,string monsterType_)
        {
            monsterName = monsterName_;
            Hp = Hp_;
            Mp = Mp_;
            damage = damage_;
            defence = defence_;
            monsterType = monsterType_;
        }
        public void print_test()
        {
            Console.Write("이름은 = {0} 체력= {1}, 마나 = {2}, 공격력 = {3}, 방어력 = {4}, 종족명 = {5} ",
                monsterName, Hp, Mp, damage, defence, monsterType);
        }



    }




}
