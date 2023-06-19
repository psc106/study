using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _0616
{
    public class Thousand
    {
        int[,] map;
        private ThousandBuffer buffer = default;

        private int size = default;
        int count;

        public Thousand()
        {
        }

        public Thousand(int size_)
        {
            this.size = size_;
            count = 0;
            map = new int[size, size];
        }

        private void Init()
        {
            buffer = new ThousandBuffer();
        }

        private void Merge(ref int[,] map, int currY, int currX, int nextY, int nextX, int direction)
        {
            int start = 0;
            int end = 0;

            //size-1 ~ 0
            if (direction == 0)
            {
                if (map[currY, currX] == map[nextY, nextX])
                {
                    map[currY, currX] += map[nextY, nextX];
                    map[nextY, nextX] = 0;
                }

                if (map[nextY, nextX] == 0)
                {
                    start = nextY;
                    for (int i = start; i < map.GetLength(0); i+=Background_Data.axisy[direction])
                    {
                        end = i;
                        if (map[end, i] != 0)
                        {
                            break;
                        }
                    }
                }

                if ((map[nextY, nextX] == 0 || map[nextY, nextX] == map[vertical, horizen]))
                {
                    map[nextY, nextX] += map[vertical, horizen];
                    map[vertical, horizen] = 0;
                    Merge(ref map, vertical + 1, horizen, currDirection);
                }
                else return;
            }
            //size-1 ~ 0
            if (currDirection == 1)
            {
                if (map[vertical, horizen] == 0 || horizen == size - 1)
                {
                    return;
                }

                if ((map[nextY, nextX] == 0 || map[nextY, nextX] == map[vertical, horizen]))
                {
                    map[nextY, nextX] += map[vertical, horizen];
                    map[vertical, horizen] = 0;
                    Merge(ref map, vertical, horizen, currDirection);
                }
                else
                {
                    return;
                }
            }
        }

        public bool Start()
        {

            bool isQuit = false;
            bool isMove = false;
            int currDirection = 0;

            Console.CursorVisible = false;

            this.Init();

            Timer stoneTimer = new Timer(this.MakeNumber_1, this.size, 500, 10000);
            Timer printTimer = new Timer(this.PrintMap, map, 500, 100);

            while (true)
            {

                isQuit = false;
                isMove = false;
                currDirection = 4;

                //키입력
                switch (Console.ReadKey(true).Key)
                {
                    //이동
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        currDirection = 0;
                        isMove = true;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        currDirection = 1;
                        isMove = true;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        currDirection = 2;
                        isMove = true;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        currDirection = 3;
                        isMove = true;
                        break;

                    //종료
                    case ConsoleKey.Q:
                        isQuit = true;
                        break;

                    //재시작
                    case ConsoleKey.R:
                        //isRestart = true;
                        break;

                    //예외
                    default:
                        currDirection = 4;
                        break;
                }//키 입력 종료

                if (isMove)
                {
                    int nextX ;
                    int nextY ;

                    for (int horizen = size-1; horizen >= 0; horizen--)
                    {
                        Merge(ref map, size-1, horizen, size - 2, horizen, currDirection);
                    }

                    for (int horizen = size - 1; horizen >= 0; horizen--)
                    {
                    }

                    for (int vertical = 0; vertical < size; vertical++)
                    {
                        for (int horizen = 0; horizen < size; horizen++)
                        {
                            nextX = horizen + Background_Data.axisx[currDirection];
                            nextY = vertical + Background_Data.axisy[currDirection];


                            if (currDirection == 2)
                            {
                                if (vertical == 0) { continue; }
                                if ((map[nextY, nextX] == 0 || map[vertical, horizen] == 0) ||
                                    (map[nextY, nextX] == map[vertical, horizen]))
                                {
                                    map[nextY, nextX] += map[vertical, horizen];
                                    map[vertical, horizen] = 0;
                                }
                            }
                            if (currDirection == 3)
                            {
                                if (horizen == 0) { continue; }
                                if ((map[nextY, nextX] == 0 || map[vertical, horizen] == 0) ||
                                    (map[nextY, nextX] == map[vertical, horizen]))
                                {
                                    map[nextY, nextX] += map[vertical, horizen];
                                    map[vertical, horizen] = 0;
                                }

                            }
                        }
                    }
                }

            }

            return false;


        }

        public void MakeNumber_1(Object size)
        {
            Random rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    int x = rand.Next(0, (int)size);
                    int y = rand.Next(0, (int)size);

                    //설정한 위치가 빈칸일 경우 통과
                    if (map[y, x] == 0)
                    {
                        //객체생성->리스트추가->맵에반영
                        map[y, x] = 1;
                        count += 1;
                        break;
                    }
                }
            }
        }

        public void PrintMap(Object numbers_)
        {
            //버퍼저장->프론트로 변경(안해도될듯?)->버퍼 출력
            //buffer.SaveBackBuffer(map.field);
            //.CopyBufferBacktoFront((int)size);
            buffer.PrintMap((int[,])numbers_);
            Console.SetCursorPosition(0, 10);
        }

    }


    public class ThousandBuffer
    {

        private bool isBusy;
        //해당 버퍼 클래스가 출력중일 경우 맵 그리기 안함
        public void PrintMap(int[,] number)
        {
            if (isBusy)
            {
                return;
            }

            isBusy = true;
            for (int vertical = 0; vertical < number.GetLength(0); vertical++)
            {
                for (int horizen = 0; horizen < number.GetLength(1); horizen++)
                {
                    if(number[vertical, horizen]==1) Console.ForegroundColor = ConsoleColor.DarkBlue;
                    else if(number[vertical, horizen]==2) Console.ForegroundColor = ConsoleColor.DarkBlue+1;
                    else if (number[vertical, horizen] == 4) Console.ForegroundColor = ConsoleColor.DarkBlue+2;
                    else if (number[vertical, horizen] == 8) Console.ForegroundColor = ConsoleColor.DarkBlue+3;
                    else if (number[vertical, horizen] == 16) Console.ForegroundColor = ConsoleColor.DarkBlue+4;
                    else if (number[vertical, horizen] == 32) Console.ForegroundColor = ConsoleColor.DarkBlue+5;
                    else if (number[vertical, horizen] == 64) Console.ForegroundColor = ConsoleColor.DarkBlue+6;
                    else if (number[vertical, horizen] == 128) Console.ForegroundColor = ConsoleColor.DarkBlue+7;
                    else if (number[vertical, horizen] == 256) Console.ForegroundColor = ConsoleColor.DarkBlue+8;
                    else if (number[vertical, horizen] == 512) Console.ForegroundColor = ConsoleColor.DarkBlue+9;
                    else if (number[vertical, horizen] == 1024) Console.ForegroundColor = ConsoleColor.DarkBlue+10;

                    Console.SetCursorPosition(horizen * 6, vertical*2);
                    Console.WriteLine("{0,5} ", number[vertical, horizen]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            isBusy = false;

        }
    }

    public static class Background_Data
    {
        public static readonly int[] axisx = { 0, 1, 0, -1, 0 };
        public static readonly int[] axisy = { 1, 0, -1, 0, 0 };
    }
}
