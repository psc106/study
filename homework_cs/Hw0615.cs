using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace homework_cs
{
    public class Sokoban
    {
        enum map_str
        {
            player = 0, blank, wall, ball, hole, full
        }

        SokobanMap map;
        SokobanBuffer buffer;

        SokobanPlayer player;
        List<SokobanStone> stones;

        private int score;
        private int size;
        private bool isRestart;

        public Sokoban(int size)
        {
            this.score = 0;
            this.size = size + 2;
            this.Init();
        }

        public void Init()
        {
            map = new SokobanMap(size);
            buffer = new SokobanBuffer(size);

            player = new SokobanPlayer(size);
            stones = new List<SokobanStone>();
        }

        public bool Start()
        {
            Console.CursorVisible = false;

            bool isQuit = false;
            bool isMove = false;
            bool isStone = false;
            int direction = 0;

            Timer stoneTimer = new Timer(this.MakeStone, this.size, 500, 1500);
            Timer printTimer = new Timer(this.PrintMap, this.size, 500, 100);

            map.SetFieldAt(player.X, player.Y, (int)map_str.player);

            while (true)
            {
                SokobanStone currStone = null;

                int beforeX = player.X;
                int beforeY = player.Y;
                int currX = player.X;
                int currY = player.Y;

                //this.PrintMap(size);

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

                //종료시
                if (isQuit)
                {
                    stoneTimer.Dispose();
                    printTimer.Dispose();
                    this.PrintMap(size);

                    return false;
                }

                //재시작시
                if (isRestart)
                {
                    stoneTimer.Dispose();
                    printTimer.Dispose();
                    this.PrintMap(size);

                    return true;
                }

                //이동시
                if (isMove)
                {
                    player.MoveXY_Hold(size, direction);
                    currX = player.X;
                    currY = player.Y;

                    //빈공간으로 이동시
                    if (map.GetFieldAt(currX, currY) == (int)map_str.blank)
                    {
                        map.SetFieldAt(beforeX, beforeY, (int)map_str.blank);
                        map.SetFieldAt(currX, currY, (int)map_str.player);
                    }

                    //벽 부딫침
                    else if (map.GetFieldAt(currX, currY) == (int)map_str.wall)
                    {
                        player.SetPosition(beforeX, beforeY);
                    }

                    //돌 부딫침
                    else if (map.GetFieldAt(currX, currY) == (int)map_str.ball)
                    {
                        for (int i = 0; i < stones.Count; i++)
                        {
                            if (stones[i].X == currX && stones[i].Y == currY)
                            {
                                currStone = stones[i];
                                currStone.Direction = direction;
                                break;
                            }

                            if (i == stones.Count)
                            {
                                for (int j = 1; j < map.Field.GetLength(0) - 1; j++)
                                {
                                    for (int k = 1; k < map.Field.GetLength(1) - 1; k++)
                                    {
                                        map.Field[j, k] = (int)map_str.blank;
                                    }
                                }

                                map.Field[player.X, player.Y] = (int)map_str.player;

                                for (int j = 0; j < stones.Count; j++)
                                {
                                    map.Field[stones[i].X, stones[i].Y] = (int)map_str.ball;

                                }
                            }
                        }

                        isStone = true;
                    }

                }

                //돌 옮길시
                if (isStone && currStone != null)
                {

                    int nextStoneX = currStone.GetNextX(currX, size);
                    int nextStoneY = currStone.GetNextY(currY, size);

                    if (map.GetFieldAt(nextStoneX, nextStoneY) == (int)map_str.blank)
                    {
                        currStone.SetPosition(nextStoneX, nextStoneY);
                        map.SetFieldAt(beforeX, beforeY, (int)map_str.blank);
                        map.SetFieldAt(currX, currY, (int)map_str.player);
                        if (currStone.pushTimer == null)
                        {
                            currStone.pushTimer = new Timer(PushStone, currStone, 100, 500);
                        }
                        map.SetFieldAt(nextStoneX, nextStoneY, (int)map_str.ball);
                    }
                    else
                    {
                        player.SetPosition(beforeX, beforeY);
                    }

                }


            }

            return false;
        }

        public void PrintMap(Object size)
        {
            buffer.SaveBackBuffer(map.Field);
            buffer.CopyBufferBacktoFront((int)size);
            buffer.PrintMap();
            Console.SetCursorPosition(0, (int)size+1);
            Console.WriteLine("점수 {0}", score);
        }

        public void MakeStone(Object size)
        {
            if (stones.Count >= 10)
            {
                return;
            }

            Random rand = new Random();

            while (true)
            {
                int x = rand.Next(0, (int)size);
                int y = rand.Next(0, (int)size);

                if (map.GetFieldAt(x, y) == (int)map_str.blank)
                {
                    SokobanStone stone = new SokobanStone(x, y);
                    stones.Add(stone);
                    map.SetFieldAt(x, y, (int)map_str.ball);
                    return;
                }
            }
        }


        public void PushStone(Object stone)
        {
            int[,] check = { { 1, -1 }, { 1, 2 }, { -1, -2 } };
            SokobanStone currStone = (SokobanStone)stone;
            SokobanStone checkStone1 = null;
            SokobanStone checkStone2 = null;



            int nextX = currStone.GetNextX(currStone.X, size);
            int nextY = currStone.GetNextY(currStone.Y, size);
            if ((nextX == 0 || nextX == size - 1 || nextY == 0 || nextY == size - 1) ||
                map.GetFieldAt(nextX, nextY) != (int)map_str.blank)
            {
                for (int i = 0; i < 3; i++)
                {
                    int check1 = stones.FindIndex(x => (x.Y == currStone.Y) && (x.X == currStone.X + check[i, 0]));
                    int check2 = stones.FindIndex(x => (x.Y == currStone.Y) && (x.X == currStone.X + check[i, 1]));

                    if (check1 == -1 || check2 == -1)
                    {
                        continue;
                    }
                    else
                    {
                        checkStone1 = stones[check1];
                        checkStone2 = stones[check2];
                        if ((checkStone1.isStop) && (checkStone2.isStop))
                        {
                            if (check1 > check2)
                            {
                                currStone.pushTimer.Dispose();

                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.blank);
                                stones.RemoveAt(check1);
                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.blank);
                                stones.RemoveAt(check2);

                                int checkCurr = stones.FindIndex(x => (x.Y == currStone.Y) && (x.X == currStone.X));
                                map.SetFieldAt(currStone.X, currStone.Y, (int)map_str.blank);
                                stones.RemoveAt(checkCurr);
                                score += 1;

                                return;
                            }
                            else
                            {
                                currStone.pushTimer.Dispose();

                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.blank);
                                stones.RemoveAt(check2);
                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.blank);
                                stones.RemoveAt(check1);

                                int checkCurr = stones.FindIndex(x => (x.Y == currStone.Y) && (x.X == currStone.X));
                                map.SetFieldAt(currStone.X, currStone.Y, (int)map_str.blank);
                                stones.RemoveAt(checkCurr);
                                score += 1;

                                return;
                            }
                        }

                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    int check1 = stones.FindIndex(x => (x.X == currStone.X) && (x.Y == currStone.Y + check[i, 0]));
                    int check2 = stones.FindIndex(x => (x.X == currStone.X) && (x.Y == currStone.Y + check[i, 1]));

                    if (check1 == -1 || check2 == -1)
                    {
                        continue;
                    }
                    else
                    {
                        checkStone1 = stones[check1];
                        checkStone2 = stones[check2];
                        if ((checkStone1.isStop) && (checkStone2.isStop))
                        {
                            if (check1 > check2)
                            {
                                currStone.pushTimer.Dispose();

                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.blank);
                                stones.RemoveAt(check1);
                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.blank);
                                stones.RemoveAt(check2);

                                int checkCurr = stones.FindIndex(x => (x.Y == currStone.Y) && (x.X == currStone.X));
                                map.SetFieldAt(currStone.X, currStone.Y, (int)map_str.blank);
                                stones.RemoveAt(checkCurr);
                                score += 1;

                                return;
                            }
                            else
                            {
                                currStone.pushTimer.Dispose();

                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.blank);
                                stones.RemoveAt(check2);
                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.blank);
                                stones.RemoveAt(check1);

                                int checkCurr = stones.FindIndex(x => (x.Y == currStone.Y) && (x.X == currStone.X));
                                map.SetFieldAt(currStone.X, currStone.Y, (int)map_str.blank);
                                stones.RemoveAt(checkCurr);
                                score += 1;

                                return;
                            }
                        }

                    }
                }
                currStone.pushTimer.Dispose();
                currStone.pushTimer = null;
                currStone.isStop = true;
                return;
            }

            currStone.isStop = false;
            map.SetFieldAt(currStone.X, currStone.Y, (int)map_str.blank);
            currStone.SetPosition(currStone.GetNextX(currStone.X, size), currStone.GetNextY(currStone.Y, size));
            map.SetFieldAt(currStone.X, currStone.Y, (int)map_str.ball);
        }

    }

    public class SokobanMap
    {
        public int[,] Field { get; private set; }
        private int size;

        public SokobanMap(int size)
        {
            this.size = size;
            this.Field = new int[this.size, this.size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                    {
                        Field[i, j] = 2;
                        continue;
                    }
                    Field[i, j] = 1;
                }

            }
        }

        public void SetFieldAt(int x, int y, int number)
        {
            this.Field[x, y] = number;
        }

        public int GetFieldAt(int x, int y)
        {
            return this.Field[x, y];
        }

    }

    public class SokobanBuffer
    {
        private readonly string[] SOKOBAN_FIELD_STRING = { "나", "　", "ㅁ", "돌", "홀", "꽉" };

        private string[] backBuffer;
        private string[] frontBuffer;

        private bool isBusy;

        public SokobanBuffer(int size)
        {
            backBuffer = new string[size];
            frontBuffer = new string[size];
        }

        //백버퍼에 저장하는 함수
        public void SaveBackBuffer(int[,] map)
        {
            //한 줄마다 저장한다.
            string line = "";

            //높이x너비만큼 반복
            for (int vertical = 0; vertical < backBuffer.Length; vertical++)
            {
                //행마다 초기화
                line = "";
                for (int horizen = 0; horizen < map.GetLength(1); horizen++)
                {
                    //맵 정보를 읽고 읽기 전용 문자열로 변경해서 한 줄 저장
                    line += (SOKOBAN_FIELD_STRING[map[vertical, horizen]]);
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
        public void CopyBufferBacktoFront(int size)
        {
            Array.Copy(this.backBuffer, this.frontBuffer, size);
            backBuffer = new string[size];
        }


        public void PrintMap()
        {
            if (isBusy)
            {
                return;
            }

            isBusy = true;
            for (int vertical = 0; vertical < frontBuffer.Length; vertical++)
            {
                Console.SetCursorPosition(0, vertical);
                Console.WriteLine(frontBuffer[vertical]);
            }
            isBusy = false;

        }
    }

    public class SokobanObject
    {
        protected readonly int[] AXIS_X = { 0, 0, 1, -1, 0 };
        protected readonly int[] AXIS_Y = { 1, -1, 0, 0, 0 };

        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int ObjectId { get; set; }

        protected int HoldPosition(int axis, int size)
        {
            if (axis == size)
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

    public class SokobanPlayer : SokobanObject
    {
        public SokobanPlayer(int size)
        {
            this.X = size / 2;
            this.Y = size / 2;
        }

        public void MoveXY_Hold(int size, int direction)
        {
            this.X = HoldPosition(this.X + AXIS_X[direction], size);
            this.Y = HoldPosition(this.Y + AXIS_Y[direction], size);
        }

    }

    public class SokobanStone : SokobanObject
    {
        public int Direction { set; private get; }
        public bool isStop { set; get; }
        public Timer pushTimer;
        public SokobanStone(int x, int y)
        {
            isStop = true;
            this.X = x;
            this.Y = y;
        }
        ~SokobanStone()
        {
            if (pushTimer != null)
            {
                pushTimer.Dispose();
            }
        }



        public int GetNextX(int x, int size)
        {
            return HoldPosition(this.X + AXIS_X[Direction], size);
        }
        public int GetNextY(int y, int size)
        {
            return HoldPosition(this.Y + AXIS_Y[Direction], size);
        }

    }


}