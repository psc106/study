using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace homework_cs
{

    public class ShopSystem
    {
        private Dictionary<int, ShopSystemItem> ITEM_DATABASE;
        private List<ShopSystemItem> currList;
        private ShopSystemShop shop;
        private ShopSystemPlayer player;

        private bool inShop = false;
        private bool inInventory = false;

        public ShopSystem()
        {
            ITEM_DATABASE = new Dictionary<int, ShopSystemItem>();
            ITEM_DATABASE.Add(1, new ShopSystemItem(1, "아이템1", "1번 아이템입니다.", 109));
            ITEM_DATABASE.Add(2, new ShopSystemItem(2, "아이템2", "2번 아이템입니다.", 5));
            ITEM_DATABASE.Add(3, new ShopSystemItem(3, "아이템3", "3번 아이템입니다.", 35));
            ITEM_DATABASE.Add(4, new ShopSystemItem(4, "아이템4", "4번 아이템입니다.", 71));
            ITEM_DATABASE.Add(5, new ShopSystemItem(5, "아이템5", "5번 아이템입니다.", 19));
            ITEM_DATABASE.Add(6, new ShopSystemItem(6, "아이템6", "6번 아이템입니다.", 88));
            ITEM_DATABASE.Add(7, new ShopSystemItem(7, "아이템7", "7번 아이템입니다.", 250));
            ITEM_DATABASE.Add(8, new ShopSystemItem(8, "아이템8", "8번 아이템입니다.", 20));
            ITEM_DATABASE.Add(9, new ShopSystemItem(9, "아이템9", "9번 아이템입니다.", 10));
            ITEM_DATABASE.Add(10, new ShopSystemItem(10, "아이템10", "10번 아이템입니다.", 0));

            shop = new ShopSystemShop();
            player = new ShopSystemPlayer();
        }


        void ChangeItemitem(object obj)
        {
            shop.ChargeNewItem(obj);
            if (!inShop)
            {
                currList = shop.GetShopList();
            }
        }

        public void start()
        {
            bool isFirst = false;
            bool isCursorMove = false;
            bool yes = false;
            bool no = false;
            int cursor = 0;

            Timer timer_ChangeItem = new Timer(ChangeItemitem, ITEM_DATABASE, 0, 1000);
            Console.CursorVisible = false;
            


            while (true)
            {
                int cursorDirection = 0;

                isCursorMove = false;
                yes = false;
                no = false;

                if (!inShop)
                {
                    currList = shop.GetShopList();
                }


                //키 입력
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        break;

                    //커서 이동
                    case ConsoleKey.DownArrow:
                        isCursorMove = true;
                        cursorDirection = 1;
                        break;
                    case ConsoleKey.UpArrow:
                        isCursorMove = true;
                        cursorDirection = -1;
                        break;

                    //상점 들어옴
                    case ConsoleKey.S:
                        if (!inShop)
                        {
                            isFirst = true;
                            cursor = 0;
                        }
                        inShop = true;
                        break;
                    //인벤토리 들어옴
                    case ConsoleKey.I:
                        if (!inInventory)
                        {
                            isFirst = true;
                            cursor = 0;
                        }
                        inInventory = true;
                        break;

                    //확인
                    case ConsoleKey.Z:
                        yes = true;
                        break;
                    //취소
                    case ConsoleKey.X:
                        no = true;
                        break;

                    //예외사항
                    default:
                        break;
                }

                //상점 입장
                if (inShop)
                {

                    //첫입장
                    if (isFirst)
                    {
                        Console.Clear();
                        isFirst = false;
                    }

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("[상점]  돈:{0}                      ", player.GeyMoney());
                    for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
                    {
                        Console.Write("─");
                    }

                    //취소
                    if (no)
                    {
                        inShop = false;
                        Console.Clear();
                        continue;
                    }

                    //확인(상품 구매)
                    else if (yes)
                    {
                        Console.SetCursorPosition(0, currList.Count+3);
                        int flag = player.BuyItem(currList[cursor]);
                        if (flag >= 0)
                        {
                            Console.WriteLine("구매    ");
                            currList[cursor].AddCount(-1);

                            Console.SetCursorPosition(0, 0);
                            Console.WriteLine("[상점]  돈:{0}                      ", player.GeyMoney());
                        }
                        else if (flag == -1) 
                        {
                            Console.WriteLine("돈없다  ");
                        }
                        else if (flag == -2)
                        {
                            Console.WriteLine("재고없다");
                        }
                    }

                    //이동키
                    else if (isCursorMove)
                    {
                        if (shop.GetShopList().Count == 0)
                        {
                            continue;
                        }

                        cursor += cursorDirection;
                        if (cursor < 0)
                        {
                            cursor += currList.Count;
                        } 
                        else if (cursor >= currList.Count) 
                        {
                            cursor = 0;
                        }
                    }

                    //커서 출력
                    for (int i = 0; i < currList.Count; i++)
                    {
                        Console.SetCursorPosition(0, 2+i);
                        Console.Write("　");
                    }
                    if (shop.GetShopList().Count != 0)
                    {
                        Console.SetCursorPosition(0, 2 + cursor);
                        Console.WriteLine("▶");
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 2 + cursor);
                        Console.WriteLine("아이템이 없습니다.");
                    }

                    //상품정보 출력
                    int listIndex = 0;
                    foreach (ShopSystemItem currItem in currList)
                    {
                        Console.SetCursorPosition(2, 2+listIndex);
                        Console.WriteLine("{0}) {1}\t{2}\t\t{3}\t{4}"
                            , ++listIndex, currItem.GetName(), currItem.GetPrice(), currItem.GetCount(), currItem.GetTip());
                    }
                    for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
                    {
                        Console.Write("─");
                    }

                }

                //인벤토리 입장
                else if (inInventory)
                {
                    //첫입장
                    if (isFirst)
                    {
                        Console.Clear();
                        isFirst = false;
                    }

                    //기본정보
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("[인벤토리]  돈:{0}                      ", player.GeyMoney());
                    for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
                    {
                        Console.Write("─");
                    }

                    //취소
                    if (no)
                    {
                        inInventory = false;
                        Console.Clear();
                        continue;
                    }

                    //확인(정보 확인)
                    else if (yes)
                    {
                        if (player.GetInventoryList().Count > 0)
                        {
                            Console.SetCursorPosition(0, player.GetInventoryList().Count + 3);
                            Console.WriteLine("{0} x{1} {2}", 
                                player.GetInventoryListAt(cursor).GetName(), 
                                player.GetInventoryListAt(cursor).GetCount(),
                                player.GetInventoryListAt(cursor).GetTip());
                        }
                    }

                    //이동키
                    else if (isCursorMove)
                    {
                        if (player.GetInventoryList().Count == 0)
                        {
                            continue;
                        }

                        cursor += cursorDirection;
                        if (cursor < 0)
                        {
                            cursor += player.GetInventoryList().Count;
                        }
                        else if (cursor >= player.GetInventoryList().Count)
                        {
                            cursor = 0;
                        }
                    }

                    //커서 출력
                    for (int i = 0; i < player.GetInventoryList().Count; i++)
                    {
                        Console.SetCursorPosition(0, 2 + i);
                        Console.WriteLine("　");
                    }
                    if (player.GetInventoryList().Count != 0)
                    {
                        Console.SetCursorPosition(0, 2 + cursor);
                        Console.WriteLine("▶");
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 2 + cursor);
                        Console.WriteLine("아이템이 없습니다.");
                    }

                    //인벤 정보 출력
                    int listIndex = 0;
                    foreach (ShopSystemItem currItem in player.GetInventoryList())
                    {
                        Console.SetCursorPosition(2, 2 + listIndex);
                        Console.WriteLine("{0}) {1}\t{2}\t\t{3}\t{4}"
                            , ++listIndex, currItem.GetName(), currItem.GetPrice()/2, currItem.GetCount(), currItem.GetTip());
                    }
                    for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
                    {
                        Console.Write("─");
                    }
                }
            }
        }


    }

    public class ShopSystemShop
    {
        private List<ShopSystemItem> shopList;

        public ShopSystemShop()
        {
            shopList = new List<ShopSystemItem>();
        }


        public List<ShopSystemItem> GetShopList()
        {
            return shopList;
        }

        public void ChargeNewItem(Object obj)
        {
            Random random = new Random();

            Dictionary<int, ShopSystemItem> db = (Dictionary<int, ShopSystemItem>)obj;

            shopList = new List<ShopSystemItem>();
            int count = random.Next(1, 10);

            for (int i = 0; i < count; i++)
            {
                int itemNum = random.Next(1, 11);
                int itemCount = random.Next(1, 3);
                int itemIndex = -1;

                ShopSystemItem tempItem = new ShopSystemItem(db[itemNum]);
                tempItem.AddCount(itemCount);

                itemIndex = shopList.FindIndex(x => x.GetNumber() == itemNum);


                if (itemIndex < 0)
                {
                    shopList.Add(tempItem);
                }
                else
                {
                    shopList[itemIndex].AddCount(itemCount);
                }
            }

        }
    }

    public class ShopSystemPlayer
    {
        private List<ShopSystemItem> inventory;
        private int money;

        public ShopSystemPlayer()
        {
            inventory = new List<ShopSystemItem>();
            money = 100;
        }

        public ShopSystemItem GetInventoryListAt(int i)
        {
            return inventory[i];
        }
        public List<ShopSystemItem> GetInventoryList()
        {
            return inventory;
        }

        public int GeyMoney()
        {
            return money;
        }
        public int BuyItem(ShopSystemItem item)
        {            
            if (item.GetCount() <= 0)
            {
                return -2;
            }

            if (item.GetPrice() > money)
            {
                return -1;
            }

            ShopSystemItem tmp = new ShopSystemItem(item);
            tmp.SetCount(1);

            int itemIndex = inventory.FindIndex(x => x.GetNumber() == tmp.GetNumber());


            if (itemIndex < 0)
            {
                inventory.Add(tmp);
            }
            else
            {
                inventory[itemIndex].AddCount(1);
            }

            money -= item.GetPrice();
            return 0;
        }
    }

    public class ShopSystemItem : IComparable<ShopSystemItem>
    {
        private int itemNumber;
        private string itemName;
        private string itemTip;
        private int itemPrice;
        private int itemCount;

        public ShopSystemItem(int number, string name, string tip, int price)
        {
            this.itemNumber = number;
            this.itemName = name;
            this.itemTip = tip;
            this.itemPrice = price;
            this.itemCount = 0;
        }

        public ShopSystemItem(ShopSystemItem item)
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

        public int CompareTo(ShopSystemItem other)
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
