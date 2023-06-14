using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace homework_cs
{

    public class CoinTrackerGame
    {
        private CoinTrackerMap map;
        private CoinTrackerMover player;
        private CoinTrackerMover[] stone;

        private int score;
        private int target;
        private int itemCount;
        private int size;

        private bool isWork;
        private bool isRestart = false;

        public CoinTrackerGame()
        {
            size = 0;
            isRestart = false;
            isWork = false;
        }

        public void Init(int size)
        {
            score = 0;
            target = 10;
            map = new CoinTrackerMap(size);
            player = new CoinTrackerMover(size);
            stone = new CoinTrackerMover[size];
        }

        public bool start()
        {
            if (!isRestart)
            {
                size = 1;
                while (size <= 4 || size > 40)
                {
                    Console.Write("사이즈 입력(5~40) : ");
                    int.TryParse(Console.ReadLine(), out size);
                }
            }
            Console.CursorVisible = false;

            Init(this.size);
            Timer timer = new Timer(MakeCoin, null, 500, 800);
            isWork = false;
            isRestart = false;

            map.SetFieldStatus(player.GetX(), player.GetY(), 1);
            for (int i = 0; i < stone.Length; i++)
            {
                stone[i] = new CoinTrackerMover(0, 0);
                while (map.getFieldStatus(stone[i].GetX(), stone[i].GetY()) != 0)
                {
                    stone[i].SetRandomXY(size);
                }
                map.SetFieldStatus(stone[i].GetX(), stone[i].GetY(), 4);
            }

            while (true)
            {

                CoinTrackerMover currStone = null;

                bool isQuit = false;
                bool isMove = false;
                bool isStone = false;
                bool isAllStone = false;
                int direction = 0;

                int beforeX = player.GetX();
                int beforeY = player.GetY();
                int currX = player.GetX();
                int currY = player.GetY();

                if (!isWork)
                {
                    isWork = true;
                    map.SaveBackBuffer();
                    map.CopyBufferBacktoFront();
                    map.PrintMap(!isWork);

                    Console.SetCursorPosition(0, size + 2);
                    Console.WriteLine("점수 : {0}                        ", this.score);
                    isWork = false;
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        direction = 3;
                        isMove = true;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        direction = 2;
                        isMove = true;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        direction = 1;
                        isMove = true;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        direction = 0;
                        isMove = true;
                        break;
                    case ConsoleKey.Q:
                        isQuit = true;
                        break;
                    case ConsoleKey.R:
                        isRestart = true;
                        break;
                    default:
                        direction = 4;
                        break;
                }

                if (isQuit)
                {
                    isWork = true;
                    timer.Dispose();

                    map.SaveBackBuffer();
                    map.CopyBufferBacktoFront();
                    map.PrintMap(false);

                    Console.Write("종료                                 ");
                    return false;
                }

                if (isRestart)
                {
                    isWork = true;
                    timer.Dispose();
                    isWork = false;


                    Console.Write("재시작중                             ");
                    return true;
                }

                if (isMove)
                {
                    isWork = true;
                    player.MoveXY_Hold(size, direction);
                    currX = player.GetX();
                    currY = player.GetY();

                    //빈공간으로 이동시
                    if (map.getFieldStatus(currX, currY) == 0)
                    {
                        Console.Write("이동                         ");
                        if (player.IsHole())
                        {
                            map.SetFieldStatus(beforeX, beforeY, 5);
                            player.SetHole(false);
                        }
                        else
                        {
                            map.SetFieldStatus(beforeX, beforeY, 0);
                        }
                        map.SetFieldStatus(currX, currY, 1);
                    }

                    //아이템 먹기
                    else if (map.getFieldStatus(currX, currY) == 2)
                    {
                        Console.Write("코인                          ");
                        score += 1;
                        itemCount -= 1;
                        if (player.IsHole())
                        {
                            map.SetFieldStatus(beforeX, beforeY, 5);
                            player.SetHole(false);
                        }
                        else
                        {
                            map.SetFieldStatus(beforeX, beforeY, 0);
                        }
                        map.SetFieldStatus(currX, currY, 1);
                    }

                    //벽 부딫침
                    else if (map.getFieldStatus(currX, currY) == 3)
                    {
                        Console.Write("벽                             ");
                        player.SetPosition(beforeX, beforeY);
                    }

                    //돌 부딫침
                    else if (map.getFieldStatus(currX, currY) == 4)
                    {
                        Console.Write("돌");
                        for (int i = 0; i < stone.Length; i++)
                        {
                            if (stone[i].GetX() == currX && stone[i].GetY() == currY)
                            {
                                currStone = stone[i];
                                break;
                            }
                        }

                        isStone = true;
                    }

                    //구멍 부딫침
                    else if (map.getFieldStatus(currX, currY) == 5)
                    {
                        Console.Write("구멍                           ");
                        if (player.IsHole())
                        {
                            map.SetFieldStatus(beforeX, beforeY, 5);
                            player.SetHole(false);
                        }
                        else
                        {
                            map.SetFieldStatus(beforeX, beforeY, 0);
                        }
                        map.SetFieldStatus(currX, currY, 1);
                        player.SetHole(true);

                    }

                    //구멍+돌 부딫침
                    else if (map.getFieldStatus(currX, currY) == 6)
                    {
                        Console.Write("구멍+돌 ");
                        for (int i = 0; i < stone.Length; i++)
                        {
                            if (stone[i].GetX() == currX && stone[i].GetY() == currY)
                            {
                                currStone = stone[i];
                                break;
                            }
                        }
                        isStone = true;

                    }

                    if (isStone)
                    {

                        int nextStoneX = currStone.GetNextX(currX, direction, size);
                        int nextStoneY = currStone.GetNextY(currY, direction, size);

                        if (map.getFieldStatus(nextStoneX, nextStoneY) == 0 || map.getFieldStatus(nextStoneX, nextStoneY) == 2)
                        {
                            Console.WriteLine(" 이동                           ");
                            currStone.SetPosition(nextStoneX, nextStoneY);
                            if (player.IsHole())
                            {
                                map.SetFieldStatus(beforeX, beforeY, 5);
                                player.SetHole(false);
                            }
                            else
                            {
                                map.SetFieldStatus(beforeX, beforeY, 0);
                            }

                            if (currStone.IsHole())
                            {
                                player.SetHole(true);
                                currStone.SetHole(false);
                            }
                            map.SetFieldStatus(currX, currY, 1);
                            map.SetFieldStatus(nextStoneX, nextStoneY, 4);
                        }
                        else if (map.getFieldStatus(nextStoneX, nextStoneY) == 5)
                        {
                            Console.WriteLine(" 넣음                           ");
                            currStone.SetPosition(nextStoneX, nextStoneY);
                            if (player.IsHole())
                            {
                                map.SetFieldStatus(beforeX, beforeY, 5);
                                player.SetHole(false);
                            }
                            else
                            {
                                map.SetFieldStatus(beforeX, beforeY, 0);
                            }


                            if (currStone.IsHole())
                            {
                                player.SetHole(true);
                            }
                            map.SetFieldStatus(currX, currY, 1);
                            map.SetFieldStatus(nextStoneX, nextStoneY, 6);
                            currStone.SetHole(true);
                            isAllStone = true;
                        }
                        else
                        {
                            Console.WriteLine(" 막힘                            ");
                            player.SetPosition(beforeX, beforeY);
                        }

                    }

                    isWork = false;
                }

                if (isAllStone)
                {
                    for (int i = 0; i < stone.Length; i++)
                    {
                        if (map.getFieldStatus(stone[i].GetX(), stone[i].GetY()) != 6)
                        {
                            isAllStone = false;
                            break;
                        }
                    }
                    if (isAllStone)
                    {
                        isWork = true;
                        timer.Dispose();

                        map.SaveBackBuffer();
                        map.CopyBufferBacktoFront();
                        map.PrintMap(false);

                        Console.WriteLine("돌승리                                  ");

                        return true;
                    }
                }

                if (score >= target)
                {
                    isWork = true;
                    timer.Dispose();

                    map.SaveBackBuffer();
                    map.CopyBufferBacktoFront();
                    map.PrintMap(false);

                    Console.WriteLine("패배?                                        ");

                    return true;
                }

            }
        }

        public void MakeCoin(object obj)
        {
            Random r = new Random();
            int X;
            int Y;
            if (itemCount < size * (size - 1))
            {
                while (!isWork)
                {
                    X = r.Next(0, this.size + 2);
                    Y = r.Next(0, this.size + 2);

                    if (map.getFieldStatus(X, Y) == 0)
                    {
                        map.SetFieldStatus(X, Y, 2);
                        itemCount += 1;

                        map.SaveBackBuffer();
                        map.CopyBufferBacktoFront();
                        map.PrintMap(isWork);
                        Console.SetCursorPosition(0, size + 2);
                        Console.WriteLine("점수 : {0}                         ", this.score);

                        return;
                    }
                }
            }
        }

        public bool IsRestart()
        {
            return isRestart;
        }
    }

    public class CoinTrackerMap
    {
        private readonly string[] FIELD_STRING = { "　", "나", "ⓒ", "ㅁ", "돌", "홀", "꽉" };

        private int[,] field;
        private int size;

        private CoinTrackerObject[] hole;

        private string[] frontBuffer;
        private string[] backBuffer;

        public CoinTrackerMap(int size_)
        {
            this.size = size_;
            field = new int[this.size + 2, this.size + 2];

            for (int i = 0; i < size + 2; i++)
            {

                field[i, 0] = 3;
                field[i, (size + 2) - 1] = 3;

                field[0, i] = 3;
                field[(size + 2) - 1, i] = 3;
            }
            Random r = new Random();
            hole = new CoinTrackerObject[size];

            for (int i = 0; i < size; i++)
            {
                hole[i] = new CoinTrackerObject();
                while (field[hole[i].GetX(), hole[i].GetY()] != 0 ||
                        (hole[i].GetX() == (size + 2) / 2 && hole[i].GetY() == (size + 2) / 2))
                {
                    hole[i].SetX(r.Next(1, size + 1));
                    hole[i].SetY(r.Next(1, size + 1));
                }
                field[hole[i].GetX(), hole[i].GetY()] = 5;
            }

            this.frontBuffer = new string[size + 2];
            this.backBuffer = new string[size + 2];
        }

        public void SetFieldStatus(int x, int y, int status)
        {
            this.field[x, y] = status;
        }
        public void ModifyFieldStatus(int x, int y, int status)
        {
            this.field[x, y] = this.field[x, y] + status;
        }

        public int getFieldStatus(int x, int y)
        {
            return this.field[x, y];
        }

        //백버퍼에 저장하는 함수
        public void SaveBackBuffer()
        {
            //한 줄마다 저장한다.
            string line = "";

            //높이x너비만큼 반복
            for (int vertical = 0; vertical < size + 2; vertical++)
            {
                //행마다 초기화
                line = "";
                for (int horizen = 0; horizen < size + 2; horizen++)
                {
                    //맵 정보를 읽고 읽기 전용 문자열로 변경해서 한 줄 저장
                    line += (FIELD_STRING[field[vertical, horizen]]);
                }
                for (int i = line.Length / 2; i < Console.WindowWidth; i++)
                {
                    line += " ";
                }

                //행에 저장되있던 정보 저장
                backBuffer[vertical] = line;
            }
        }

        //백버퍼->프론트버퍼 + 백버퍼 비움
        public void CopyBufferBacktoFront()
        {
            Array.Copy(this.backBuffer, this.frontBuffer, size + 2);
            backBuffer = new string[size + 2];
        }


        public void PrintMap(bool isBusy)
        {
            if (isBusy)
            {
                return;
            }

            for (int vertical = 0; vertical < size + 2; vertical++)
            {
                Console.SetCursorPosition(0, vertical);
                Console.WriteLine(frontBuffer[vertical]);
            }

        }

    }

    public class CoinTrackerMover : CoinTrackerObject
    {
        private readonly int[] AXIS_X = { 0, 0, 1, -1, 0 };
        private readonly int[] AXIS_Y = { 1, -1, 0, 0, 0 };
        private bool isHole;

        public CoinTrackerMover(int size)
        {
            this.X = (size + 2) / 2;
            this.Y = (size + 2) / 2;
            isHole = false;
        }
        public CoinTrackerMover(int x, int y)
        {
            this.X = x;
            this.Y = y;
            isHole = false;
        }
        public bool IsHole()
        {
            return isHole;
        }
        public void SetHole(bool isHole)
        {
            this.isHole = isHole;
        }

        public int GetNextX(int x, int direction, int size)
        {
            return HoldPosition(this.X + AXIS_X[direction], size);
        }
        public int GetNextY(int x, int direction, int size)
        {
            return HoldPosition(this.Y + AXIS_Y[direction], size);
        }

        public void SetRandomXY(int size)
        {
            Random r = new Random();
            this.X = r.Next(2, size);
            this.Y = r.Next(2, size);
        }
        public void MoveXY_Hold(int size, int direction)
        {
            this.X = HoldPosition(this.X + AXIS_X[direction], size);
            this.Y = HoldPosition(this.Y + AXIS_Y[direction], size);
        }
        private int HoldPosition(int axis, int size)
        {
            if (axis == size + 2)
            {
                return axis - 1;
            }
            else if (axis == -1)
            {
                return 0;
            }
            return axis;
        }

        public void SetPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }


    public class CoinTrackerObject
    {
        protected int X, Y;
        public CoinTrackerObject()
        {
            this.X = 0;
            this.Y = 0;
        }
        public int GetX()
        {
            return X;
        }
        public int GetY()
        {
            return Y;
        }
        public void SetX(int x)
        {
            X = x;
        }
        public void SetY(int y)
        {
            Y = y;
        }
    }
}
