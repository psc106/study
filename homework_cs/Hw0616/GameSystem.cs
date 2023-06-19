using homework_0616;
using homework_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace homework_cs.Hw0616
{
    //추상 클래스
    public abstract class GameSystem
    {
        public GameMap map { get; protected set; }
        public GamePlayer player;

        public abstract int StartThisMode();
    }


    //포탈 시스템
    public class PortalSystem : GameSystem
    {
        private Portal[] nextPortal;
        public Timer printTimer;

        public PortalSystem()
        {
        }

        public PortalSystem(ref GamePlayer player)
        {
            this.player = player;

            map = new PortalMap(player);

            Random rand = new Random();

            nextPortal = new Portal[2];

            while (true)
            {
                for (int i = 0; i < nextPortal.Length; i++)
                {
                    int mode = rand.Next(1, 4);

                    int x = rand.Next(0, 10);
                    int y = rand.Next(0, 10);

                    if ((x != player.X || y != player.Y))
                    {
                        nextPortal[i] = new Portal(x, y, mode);
                    }
                }

                if (nextPortal[0].X != nextPortal[1].X || nextPortal[0].Y != nextPortal[1].Y)
                {
                    break;
                }
            }

            map.SetFieldObject(player);
            for (int i = 0; i < nextPortal.Length; i++)
            {
                map.SetFieldObject(nextPortal[i]);
            }

        }

        //시스템 내용 구현
        public override int StartThisMode()
        {

            Console.Clear();

            bool isMove = false;
            bool isNo = false;
            int direction = 0;

            while (true)
            {
                Console.CursorVisible = false;
                isMove = false;
                isNo = false;


                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction = 1;
                        isMove = true;
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        direction = 2;
                        isMove = true;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction = 3;
                        isMove = true;
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        direction = 4;
                        isMove = true;
                        break;

                    case ConsoleKey.X:
                        isNo = true;
                        break;

                    default:
                        direction = 0;
                        break;
                }

                if (isMove)
                {
                    int beforeX = player.X;
                    int beforeY = player.Y;

                    player.direction = direction;
                    player.Move();
                    player.Hold(map.field.GetLength(0));

                    int currX = player.X;
                    int currY = player.Y;

                    for (int i = 0; i < nextPortal.Length; i++)
                    {

                        if (nextPortal[i].X == currX && nextPortal[i].Y == currY)
                        {
                            map = null;
                            printTimer.Dispose();

                            return nextPortal[i].objectID;
                        }
                    }

                    map.SetFieldAt(beforeX, beforeY, 0);
                    map.SetFieldObject(player);
                }

                if (isNo)
                {
                    this.printTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                    Thread.Sleep(500);
                    InventorySystem inventorySystem = new InventorySystem();
                    inventorySystem.Open(ref player);
                    this.printTimer.Change(100, 100);
                }



            }
        }

    }


    //인벤토리 시스템
    public class InventorySystem
    {
        public void Open(ref GamePlayer player)
        {
            Console.Clear();

            int cursor = 0;
            int direction = 0;
            bool isNo = false;
            bool isMove = false;
            bool isYes = false;

            while (true)
            {

                direction = 0;
                isNo = false;
                isMove = false;
                isYes = false;

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction = -1;
                        isMove = true;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction = 1;
                        isMove = true;
                        break;

                    case ConsoleKey.Z:
                        isYes = true;
                        break;

                    case ConsoleKey.X:
                        isNo = true;
                        break;

                    default:
                        direction = 0;
                        break;
                }


                //취소
                if (isNo)
                {
                    Console.Clear();
                    return;
                }

                //확인(정보 확인)
                else if (isYes)
                {
                    if (player.inventory.Count > 0)
                    {
                        Console.SetCursorPosition(0, (int)GameMap.InfomationLine.status2);
                        Console.Write("{0} 사용",
                             player.inventory.ElementAt(cursor).GetName());
                        for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
                        {
                            Console.Write(" ");
                        }

                        for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
                        {
                            Console.Write(" ");
                        }

                        player.inventory.ElementAt(cursor).AddCount(-1);

                        if (player.inventory.ElementAt(cursor).GetCount() == 0)
                        {
                            player.inventory.RemoveAt(cursor);
                            cursor -= 1;
                        }

                        if (cursor < 0)
                        {
                            cursor += 1;
                        }
                    }
                }

                //이동키
                else if (isMove)
                {
                    if (player.inventory.Count == 0)
                    {
                        continue;
                    }

                    cursor += direction;
                    if (cursor < 0)
                    {
                        cursor += player.inventory.Count;
                    }
                    else if (cursor >= player.inventory.Count)
                    {
                        cursor = 0;
                    }
                }

            }

        }
    }


    public class BattleSystem : GameSystem
    {
        public BattleSystem() { }

        public BattleSystem(ref GamePlayer player)
        {
        }

        public override int StartThisMode()
        {
            Console.WriteLine("배틀시스템 입장");

            return 0;
        }
    }

    public class CardSystem : GameSystem
    {
        public CardSystem(ref GamePlayer player)
        {
        }

        public override int StartThisMode()
        {
            Console.WriteLine("카드시스템 입장");

            return 0;
        }
    }



    public class ShopSystem : GameSystem
    {
        private List<GameItem> currList;
        private ShopSystemShop shop;
        public ShopSystem(ref GamePlayer player)
        {
            this.player = player;
            shop = new ShopSystemShop();
            ChangeItemitem(DataManager.ITEM_DATABASE);
            //Timer timer_ChangeItem = new Timer(ChangeItemitem, DataManager.ITEM_DATABASE, 0, 5000);
        }

        void ChangeItemitem(object obj)
        {
            shop.ChargeNewItem(obj);
            currList = shop.GetShopList();
        }

        public override int StartThisMode()
        {
            bool isNo = false;
            bool isYes = false;
            bool isQuit = false;
            bool isVerticalMove = false;
            bool isHorizenMove = false;

            int direction = 0;

            ShopMap sMap = ((ShopMap)map);

            Console.Clear();

            while (true)
            {

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("[상점]  돈:{0}                      ", player.gold);
                for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
                {
                    Console.Write("─");
                }

                //커서 출력
                for (int i = 0; i < currList.Count; i++)
                {
                    Console.SetCursorPosition(0, 2 + i);
                    Console.Write("　");
                }
                if (shop.GetShopList().Count != 0)
                {
                    Console.SetCursorPosition(0, 2 + sMap.cursorY);
                    Console.WriteLine("▶");
                }
                else
                {
                    Console.SetCursorPosition(0, 2 + sMap.cursorY);
                    Console.WriteLine("아이템이 없습니다.");
                }

                //상품정보 출력
                int listIndex = 0;
                foreach (GameItem currItem in currList)
                {
                    Console.SetCursorPosition(2, 2 + listIndex);
                    Console.WriteLine("{0}) {1}\t{2}\t\t{3}\t{4}"
                        , ++listIndex, currItem.GetName(), currItem.GetPrice(), currItem.GetCount(), currItem.GetTip());
                }
                Console.Write("이동[↑,↓] 확인/취소(z/x)");

                direction = 0;
                isNo = false;
                isYes = false;
                isVerticalMove = false;
                isHorizenMove = false;

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction = -1;
                        isVerticalMove = true;
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        direction = -1;
                        isHorizenMove = true;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction = 1;
                        isVerticalMove = true;
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        direction = 1;
                        isHorizenMove = true;
                        break;

                    case ConsoleKey.Z:
                        isYes = true;
                        break;

                    case ConsoleKey.X:
                        isNo = true;
                        break;

                    case ConsoleKey.Q:
                        isQuit = true;
                        break;

                    default:
                        direction = 0;
                        break;
                }

                if (isQuit)
                {
                    return 0;
                }

                //이동키
                if (isVerticalMove)
                {
                    if (shop.GetShopList().Count == 0)
                    {
                        continue;
                    }

                    sMap.cursorY += direction;
                    if (sMap.cursorY < 0)
                    {
                        sMap.cursorY += currList.Count;
                    }
                    else if (sMap.cursorY >= currList.Count)
                    {
                        sMap.cursorY = 0;
                    }
                }

                //확인(상품 구매)
                else if (isYes)
                {
                    Console.SetCursorPosition(0, (int)GameMap.InfomationLine.debug);
                    int flag = player.BuyItem(currList[sMap.cursorY]);
                    if (flag >= 0)
                    {
                        Console.WriteLine("구매    ");
                        currList[sMap.cursorY].AddCount(-1);

                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("[상점]  돈:{0}                      ", player.gold);
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
            }

            return 0;

        }
    }


    public class ShopSystemShop
    {
        private List<GameItem> shopList;

        public ShopSystemShop()
        {
            shopList = new List<GameItem>();
        }


        public List<GameItem> GetShopList()
        {
            return shopList;
        }

        public void ChargeNewItem(Object obj)
        {
            Random random = new Random();

            Dictionary<int, GameItem> db = (Dictionary<int, GameItem>)obj;

            shopList = new List<GameItem>();
            int count = random.Next(1, 10);

            for (int i = 0; i < count; i++)
            {
                int itemNum = random.Next(1, 11);
                int itemCount = random.Next(1, 3);
                int itemIndex = -1;

                GameItem tempItem = new GameItem(db[itemNum]);
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
}
