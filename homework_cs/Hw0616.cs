//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace homework_cs
//{
//    //게임 시작, 전체적 자원 관리
//    public class MiniWorldGame
//    {
//        GameSystem system;

//        GameMap map;
//        Buffer buffer;
//        GamePlayer player;

//        int mode = 0;

//        public MiniWorldGame()
//        {

//            DataManager.ITEM_DATABASE = new Dictionary<int, GameItem>();
//            DataManager.ITEM_DATABASE.Add(1, new GameItem(1, "아이템1", "1번 아이템입니다.", 109));
//            DataManager.ITEM_DATABASE.Add(2, new GameItem(2, "아이템2", "2번 아이템입니다.", 5));
//            DataManager.ITEM_DATABASE.Add(3, new GameItem(3, "아이템3", "3번 아이템입니다.", 35));
//            DataManager.ITEM_DATABASE.Add(4, new GameItem(4, "아이템4", "4번 아이템입니다.", 71));
//            DataManager.ITEM_DATABASE.Add(5, new GameItem(5, "아이템5", "5번 아이템입니다.", 19));
//            DataManager.ITEM_DATABASE.Add(6, new GameItem(6, "아이템6", "6번 아이템입니다.", 88));
//            DataManager.ITEM_DATABASE.Add(7, new GameItem(7, "아이템7", "7번 아이템입니다.", 250));
//            DataManager.ITEM_DATABASE.Add(8, new GameItem(8, "아이템8", "8번 아이템입니다.", 20));
//            DataManager.ITEM_DATABASE.Add(9, new GameItem(9, "아이템9", "9번 아이템입니다.", 10));
//            DataManager.ITEM_DATABASE.Add(10, new GameItem(10, "아이템10", "10번 아이템입니다.", 0));

//            Console.WindowHeight = 60;
//            Console.WindowWidth = 60;

//            player = new GamePlayer(5, 5, 0);
//            buffer = new Buffer();

//            map = null;
//        }

//        public void Start()
//        {
//            bool isMove = false;
//            int direction = 0;
//            player = new GamePlayer(5, 5, 0);

//            while (true)
//            {
//                player.X = 5;
//                player.Y = 5;
//                player.direction = 0;

//                if (mode == 0)
//                {
//                    system = new PortalSystem(ref player);
//                }


//                else if (mode == 1)
//                {
//                    system = new CardSystem(ref player);
//                }


//                else if (mode == 2)
//                {
//                    system = new ShopSystem(ref player);
//                }


//                else if (mode == 3)
//                {
//                    system = new BattleSystem(ref player);
//                }

//                map = system.map;
//                while (system.printTimer != null) { Console.Clear(); }

//                system.printTimer = new Timer(PrintMap, map, 100, 100);
//                mode = system.startThisMode();
//            }
//        }

//        public void PrintMap(Object obj)
//        {
//            if (obj != null)
//            {
//                buffer.SetBuffer(((BufferHelp)obj).MapToStringArray(player.direction));
//                buffer.PrintBuffer();
//            }

//            else
//            {
//                Console.Clear();
//            }
//        }

//    }

//    public abstract class GameSystem
//    {
//        public GameMap map { get; protected set; }
//        public GamePlayer player;
//        public Timer printTimer;

//        public abstract int startThisMode();
//    }

//    public class PortalSystem : GameSystem
//    {
//        private Portal[] nextPortal;

//        public PortalSystem()
//        {
//        }

//        public PortalSystem(ref GamePlayer player)
//        {
//            this.player = player;

//            map = new PortalMap(player);

//            Random rand = new Random();

//            nextPortal = new Portal[2];

//            while (true)
//            {
//                for (int i = 0; i < nextPortal.Length; i++)
//                {
//                    int mode = rand.Next(1, 4);

//                    int x = rand.Next(0, 10);
//                    int y = rand.Next(0, 10);

//                    if ((x != player.X || y != player.Y))
//                    {
//                        nextPortal[i] = new Portal(x, y, mode);
//                    }
//                }

//                if (nextPortal[0].X != nextPortal[1].X || nextPortal[0].Y != nextPortal[1].Y)
//                {
//                    break;
//                }
//            }

//            map.SetFieldObject(player);
//            for (int i = 0; i < nextPortal.Length; i++)
//            {
//                map.SetFieldObject(nextPortal[i]);
//            }

//        }

//        //시스템 내용 구현
//        public override int startThisMode()
//        {

//            bool isMove = false;
//            bool isNo = false;
//            int direction = 0;

//            while (true)
//            {
//                Console.CursorVisible = false;
//                isMove = false;
//                isNo = false;


//                switch (Console.ReadKey(true).Key)
//                {
//                    case ConsoleKey.UpArrow:
//                    case ConsoleKey.W:
//                        direction = 1;
//                        isMove = true;
//                        break;

//                    case ConsoleKey.LeftArrow:
//                    case ConsoleKey.A:
//                        direction = 2;
//                        isMove = true;
//                        break;

//                    case ConsoleKey.DownArrow:
//                    case ConsoleKey.S:
//                        direction = 3;
//                        isMove = true;
//                        break;

//                    case ConsoleKey.RightArrow:
//                    case ConsoleKey.D:
//                        direction = 4;
//                        isMove = true;
//                        break;

//                    case ConsoleKey.X:
//                        isNo = true;
//                        break;

//                    default:
//                        direction = 0;
//                        break;
//                }

//                if (isMove)
//                {
//                    int beforeX = player.X;
//                    int beforeY = player.Y;

//                    player.direction = direction;
//                    player.Move();
//                    player.Hold(map.field.GetLength(0));

//                    int currX = player.X;
//                    int currY = player.Y;

//                    for (int i = 0; i < nextPortal.Length; i++)
//                    {

//                        if (nextPortal[i].X == currX && nextPortal[i].Y == currY)
//                        {
//                            map = null;
//                            printTimer.Dispose();

//                            return nextPortal[i].objectID;
//                        }
//                    }

//                    map.SetFieldAt(beforeX, beforeY, 0);
//                    map.SetFieldObject(player);
//                }

//                if (isNo)
//                {
//                    this.printTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
//                    Thread.Sleep(500);
//                    InventorySystem inventorySystem = new InventorySystem();
//                    inventorySystem.Open(ref player);
//                    this.printTimer.Change(100, 100);
//                }



//            }
//        }

//    }

//    public class InventorySystem
//    {
//        public void Open(ref GamePlayer player)
//        {
//            Console.Clear();

//            int cursor = 0;
//            int direction = 0;
//            bool isNo = false;
//            bool isMove = false;
//            bool isYes = false;

//            while (true)
//            {
//                //취소
//                if (isNo)
//                {                    
//                    Console.Clear();
//                    return;
//                }

//                //확인(정보 확인)
//                else if (isYes)
//                {
//                    if (player.inventory.Count > 0)
//                    {
//                        Console.SetCursorPosition(0, (int)GameMap.InfomationLine.status2);
//                        Console.Write("{0} 사용",
//                             player.inventory.ElementAt(cursor).GetName());
//                        for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
//                        {
//                            Console.Write(" ");
//                        }

//                        for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
//                        {
//                            Console.Write(" ");
//                        }

//                        player.inventory.ElementAt(cursor).AddCount(-1);

//                        if (player.inventory.ElementAt(cursor).GetCount() == 0)
//                        {
//                            player.inventory.RemoveAt(cursor);
//                            cursor -= 1;
//                        }

//                        if (cursor < 0)
//                        {
//                            cursor += 1;
//                        }
//                    }
//                }

//                //이동키
//                else if (isMove)
//                {
//                    if (player.inventory.Count == 0)
//                    {
//                        continue;
//                    }

//                    cursor += direction;
//                    if (cursor < 0)
//                    {
//                        cursor += player.inventory.Count;
//                    }
//                    else if (cursor >= player.inventory.Count)
//                    {
//                        cursor = 0;
//                    }
//                }

//                //기본정보
//                Console.SetCursorPosition(0, 0);
//                Console.WriteLine("[인벤토리]  돈:{0}                      ");
//                for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
//                {
//                    Console.Write("─");
//                }

//                //커서 출력
//                if (player.inventory.Count >= 0)
//                {
//                    for (int i = 0; i < player.inventory.Count; i++)
//                    {
//                        Console.SetCursorPosition(0, (int)GameMap.InfomationLine.game + i);
//                        Console.WriteLine("　");
//                    }
//                    if (player.inventory.Count != 0)
//                    {
//                        Console.SetCursorPosition(0, (int)GameMap.InfomationLine.game + cursor);
//                        Console.WriteLine("▶");
//                    }
//                    else
//                    {
//                        Console.SetCursorPosition(0, (int)GameMap.InfomationLine.game + cursor);
//                        Console.WriteLine("아이템이 없습니다.");
//                    }
//                }

//                //인벤 정보 출력
//                int listIndex = 0;
//                foreach (GameItem currItem in player.inventory)
//                {
//                    Console.SetCursorPosition(2, (int)GameMap.InfomationLine.status2);
//                    Console.WriteLine("{0}) {1}\t{2}\t\t{3}\t{4}"
//                        , ++listIndex, currItem.GetName(), currItem.GetPrice() / 2, currItem.GetCount(), currItem.GetTip());
//                }
//                Console.Write("이동[↑,↓] 확인/취소(z/x)");

//                for (int i = Console.CursorLeft; i < Console.WindowWidth; i++)
//                {
//                    Console.Write("─");
//                }


//                isNo = false;
//                isMove = false;
//                isYes = false;

//                switch (Console.ReadKey(true).Key)
//                {
//                    case ConsoleKey.UpArrow:
//                    case ConsoleKey.W:
//                        direction = -1;
//                        break;

//                    case ConsoleKey.DownArrow:
//                    case ConsoleKey.S:
//                        direction = 1;
//                        break;

//                    case ConsoleKey.Z:
//                        isYes = true;
//                        break;

//                    case ConsoleKey.X:
//                        isNo = true;
//                        break;

//                    default:
//                        direction = 0;
//                        break;
//                }
//            }

//        }
//    }


//    public class BattleSystem : GameSystem
//    {
//        public BattleSystem() { }

//        public BattleSystem(ref GamePlayer player)
//        {
//            printTimer = null;
//        }

//        public override int startThisMode()
//        {
//            Console.WriteLine("배틀시스템 입장");

//            if (printTimer != null) printTimer.Dispose();
//            return 0;
//        }
//    }

//    public class CardSystem : GameSystem
//    {
//        public CardSystem(ref GamePlayer player)
//        {
//            printTimer = null;
//        }

//        public override int startThisMode()
//        {
//            Console.WriteLine("카드시스템 입장");

//            if (printTimer != null) printTimer.Dispose();
//            return 0;
//        }
//    }

//    public class ShopSystem : GameSystem
//    {
//        public ShopSystem(ref GamePlayer player)
//        {
//            printTimer = null;
//        }

//        public override int startThisMode()
//        {
//            Console.WriteLine("샵시스템 입장");

//            if (printTimer != null) printTimer.Dispose();
//            return 0;
//        }
//    }

//    //오브젝트 담당(부모 클래스)
//    public abstract class GameMap
//    {
//        public int[,] field { get; protected set; }
//        public enum InfomationLine
//        {
//            status1 = 2, game = 40, status2 = 50, debug = 60
//        }

//        protected void Init(int empty)
//        {

//            for (int y = 0; y < field.GetLength(0); y++)
//            {
//                for (int x = 0; x < field.GetLength(1) - 1; x++)
//                {
//                    field[y, x] = empty;
//                }
//            }
//        }

//        public virtual void SetFieldObject(GameObject obj)
//        {
//            field[obj.Y, obj.X] = obj.objectID;
//        }


//        public void SetFieldAt(int x, int y, int info)
//        {
//            field[y, x] = info;
//        }
//        public int GetFieldAt(int x, int y)
//        {
//            return field[y, x];
//        }

//        protected string GetEmptyLine()
//        {
//            string emptyLine = "";
//            for (int i = Console.WindowLeft; i < Console.WindowWidth; i++)
//            {
//                emptyLine += " ";
//            }
//            return emptyLine;
//        }
//        protected string GetEmptyLine(int cursor)
//        {
//            string emptyLine = "";
//            for (int i = cursor; i < Console.WindowWidth; i++)
//            {
//                emptyLine += " ";
//            }
//            return emptyLine;
//        }

//    }


//    public class PortalMap : GameMap, BufferHelp
//    {
//        public static int _PORTAL_MAP_SIZE = 10;


//        public PortalMap()
//        {
//            this.field = new int[_PORTAL_MAP_SIZE, _PORTAL_MAP_SIZE];
//            this.Init(0);
//        }

//        public PortalMap(GamePlayer player)
//        {
//            this.field = new int[_PORTAL_MAP_SIZE, _PORTAL_MAP_SIZE];
//            this.Init(0);
//            this.field[player.Y, player.X] = 5;
//        }


//        public override void SetFieldObject(GameObject obj)
//        {
//            field[obj.Y, obj.X] = obj.objectID;
//        }


//        public string[] MapToStringArray(Object direction)
//        {
//            string[] map = new string[Buffer._BUFFER_SIZE];
//            string line;
//            int cursorPosition = 0;
//            int yPosition = 0;

//            for (int bufferY = 0; bufferY < Buffer._BUFFER_SIZE; bufferY++)
//            {
//                line = "";
//                cursorPosition = 0;

//                //맵 정보 출력
//                if (bufferY < (int)GameMap.InfomationLine.status1)
//                {
//                    line = this.GetEmptyLine();
//                    map[bufferY] = line;
//                    continue;
//                }

//                //맵 출력
//                else if (bufferY < (int)GameMap.InfomationLine.game)
//                {
//                    if (yPosition >= field.GetLength(1))
//                    {
//                        line = this.GetEmptyLine();
//                        map[bufferY] = line;
//                        continue;
//                    }
//                    for (int x = 0; x < field.GetLength(1); x++)
//                    {
//                        if (field[yPosition, x] < DataManager._PORTAL_STRING.Length)
//                        {
//                            line += (" " + DataManager._PORTAL_STRING[field[yPosition, x]]);
//                        }
//                        else
//                        {
//                            line += (" " + DataManager._PLAYER_STRING[(int)direction]);
//                        }
//                        cursorPosition += 3;
//                    }
//                    yPosition += 1;
//                    line = line + this.GetEmptyLine(cursorPosition);
//                    map[bufferY] = line;
//                    continue;
//                }

//                //상태 출력
//                else if (bufferY < (int)GameMap.InfomationLine.status2)
//                {
//                    line = this.GetEmptyLine();
//                    map[bufferY] = line;
//                    continue;
//                }

//                //디버그 출력
//                else if (bufferY < (int)GameMap.InfomationLine.debug)
//                {
//                    line = this.GetEmptyLine();
//                    map[bufferY] = line;
//                    continue;
//                }
//            }
//            return map;
//        }
//    }

//    public class BattleMap : GameMap
//    {
//    }

//    public class ShopMap : GameMap
//    {
//    }

//    public class CardMap : GameMap
//    {
//    }


//    //오브젝트 담당(부모 클래스)
//    public class GameObject
//    {
//        public int X;
//        public int Y;
//        public int objectID;
//    }

//    public abstract class GameMoveObject : GameObject
//    {
//        public int direction;
//        protected int[] AXIS_X = { 0, 0, -1, 0, 1 };
//        protected int[] AXIS_Y = { 0, -1, 0, 1, 0 };

//        public abstract void Move();

//        public void Hold(int size)
//        {
//            if (X < 0)
//            {
//                X = 0;
//            }
//            else if (X >= size)
//            {
//                X = size - 1;
//            }
//            else if (Y < 0)
//            {
//                Y = 0;
//            }
//            else if (Y >= size)
//            {
//                Y = size - 1;
//            }
//        }
//    }

//    public class GamePlayer : GameMoveObject
//    {
//        public List<GameItem> inventory { get; private set; }
//        public int gold;

//        public GamePlayer(int x, int y, int dir)
//        {
//            this.X = x;
//            this.Y = y;
//            this.direction = dir;
//            this.objectID = 5;
//            this.gold = 0;
//            this.inventory = new List<GameItem>();
//        }

//        public override void Move()
//        {
//            this.X += AXIS_X[this.direction];
//            this.Y += AXIS_Y[this.direction];
//        }

//    }

//    public class GameEnemy : GameMoveObject
//    {
//        public GameEnemy(int x, int y, int dir)
//        {
//            this.X = x;
//            this.Y = y;
//            this.direction = dir;
//            this.objectID = 5;
//        }

//        public override void Move()
//        {
//            this.X += AXIS_X[this.direction];
//            this.Y += AXIS_Y[this.direction];
//        }

//    }

//    public class Portal : GameObject
//    {
//        public Portal(int x, int y, int nextMode)
//        {
//            this.X = x;
//            this.Y = y;
//            this.objectID = nextMode;
//        }

//    }


//    //출력 담당
//    public class Buffer
//    {
//        string[] buffer;

//        public static int _BUFFER_SIZE = 50;

//        public bool isWork { private set; get; }

//        private enum color
//        {
//            WHITE = 0, RED = 1, GREEN = 2, BLUE = 3, YELLOW = 4
//        }

//        public Buffer()
//        {
//            isWork = false;
//            buffer = new string[_BUFFER_SIZE];
//        }

//        public void SetBuffer(string[] map)
//        {
//            buffer = map;
//        }

//        public void PrintBuffer()
//        {
//            if (isWork) { return; }
//            isWork = true;

//            for (int y = 0; y < _BUFFER_SIZE; y++)
//            {
//                Console.SetCursorPosition(0, y);
//                string[] splitString = buffer[y].Split('.');

//                for (int i = 0; i < splitString.Length; i++)
//                {

//                    if (i % 3 == 0)
//                    {
//                        Console.Write(splitString[i]);
//                    }
//                    else if (i % 3 == 1)
//                    {
//                        switch (int.Parse(splitString[i]))
//                        {
//                            case (int)color.WHITE:
//                                Console.ForegroundColor = ConsoleColor.White;
//                                break;
//                            case (int)color.RED:
//                                Console.ForegroundColor = ConsoleColor.Red;
//                                break;
//                            case (int)color.GREEN:
//                                Console.ForegroundColor = ConsoleColor.Green;
//                                break;
//                            case (int)color.BLUE:
//                                Console.ForegroundColor = ConsoleColor.Blue;
//                                break;
//                            case (int)color.YELLOW:
//                                Console.ForegroundColor = ConsoleColor.Yellow;
//                                break;
//                            default:
//                                break;
//                        }   //[switch] end 컬러체크
//                    }
//                    else if (i % 3 == 2)
//                    {
//                        Console.Write(splitString[i]);
//                        Console.ForegroundColor = ConsoleColor.White;
//                    }


//                }// 1행 출력 종료

//            }//모든 행 출력 종료
//            isWork = false;
//        }


//        public void PrintBuffer2()
//        {
//            if (isWork) { return; }
//            isWork = true;

//            for (int y = 0; y < _BUFFER_SIZE; y++)
//            {
//                Console.SetCursorPosition(0, y);
//                Console.Write(buffer[y]);
//            }
//            isWork = false;
//        }
//    }


//    public class GameItem
//    {
//        private int itemNumber;
//        private string itemName;
//        private string itemTip;
//        private int itemPrice;
//        private int itemCount;

//        public GameItem(int number, string name, string tip, int price)
//        {
//            this.itemNumber = number;
//            this.itemName = name;
//            this.itemTip = tip;
//            this.itemPrice = price;
//            this.itemCount = 0;
//        }

//        public GameItem(GameItem item)
//        {
//            this.itemNumber = item.itemNumber;
//            this.itemName = item.itemName;
//            this.itemTip = item.itemTip;
//            this.itemPrice = item.itemPrice;
//            this.itemCount = 0;
//        }

//        public int GetNumber()
//        {
//            return this.itemNumber;
//        }
//        public string GetName()
//        {
//            return this.itemName;
//        }
//        public string GetTip()
//        {
//            return this.itemTip;
//        }
//        public int GetPrice()
//        {
//            return this.itemPrice;
//        }
//        public int GetCount()
//        {
//            return this.itemCount;
//        }
//        public void AddCount(int count)
//        {
//            this.itemCount += count;
//        }

//        public void SetCount(int count)
//        {
//            this.itemCount = count;
//        }

//        public int CompareTo(GameItem other)
//        {
//            if (this.itemNumber == other.itemNumber)
//            {
//                return 0;
//            }
//            else if (this.itemNumber > other.itemNumber)
//            {
//                return -1;
//            }
//            return 1;
//        }
//    }


//    public static class DataManager
//    {
//        public static Dictionary<int, GameItem> ITEM_DATABASE;
//        public static string[] _PLAYER_STRING = { ".4.♣.", ".4.▲.", ".4.◀.", ".4.▼.", ".4.▶." };
//        public static string[] _PORTAL_STRING = { "□", ".1.◎.", ".2.◎.", ".3.◎." };

//    }

//    public interface BufferHelp
//    {
//        string[] MapToStringArray(Object obj);
//    }
//}
