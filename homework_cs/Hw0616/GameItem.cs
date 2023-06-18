using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs.Hw0616
{
    public class GameItem
    {
        private int itemNumber;
        private string itemName;
        private string itemTip;
        private int itemPrice;
        private int itemCount;

        public GameItem(int number, string name, string tip, int price)
        {
            this.itemNumber = number;
            this.itemName = name;
            this.itemTip = tip;
            this.itemPrice = price;
            this.itemCount = 0;
        }

        public GameItem(GameItem item)
        {
            this.itemNumber = item.itemNumber;
            this.itemName = item.itemName;
            this.itemTip = item.itemTip;
            this.itemPrice = item.itemPrice;
            this.itemCount = 0;
        }

        public int GetNumber()
        {
            return this.itemNumber;
        }
        public string GetName()
        {
            return this.itemName;
        }
        public string GetTip()
        {
            return this.itemTip;
        }
        public int GetPrice()
        {
            return this.itemPrice;
        }
        public int GetCount()
        {
            return this.itemCount;
        }
        public void AddCount(int count)
        {
            this.itemCount += count;
        }

        public void SetCount(int count)
        {
            this.itemCount = count;
        }

        public int CompareTo(GameItem other)
        {
            if (this.itemNumber == other.itemNumber)
            {
                return 0;
            }
            else if (this.itemNumber > other.itemNumber)
            {
                return -1;
            }
            return 1;
        }
    }

}
