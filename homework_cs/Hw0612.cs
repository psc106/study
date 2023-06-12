using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs
{

    public class TicTacGame
    {
        TicTacPlayer[] player;
        TicTacMap map;
        int turn = 1;   //플레이어부터 시작

        public TicTacGame()
        {
            map = new TicTacMap();
            player = new TicTacPlayer[2];
            player[0] = new TicTacPlayer(2); //플레이어
            player[1] = new TicTacPlayer(0); //컴퓨터
        }


        public void Start()
        {
            Console.CursorVisible = false;
            map.ChangeMap(player[0].GetX(), player[0].GetY());
            map.SaveBackBuffer();
            while (true)
            {
                map.CopyBufferBacktoFront();
                map.PrintMap();
                Console.WriteLine();

                Console.WriteLine(player[0].GetX()+" "+player[0].GetY());

                int direction = 0;
                bool isClick = false;
                bool isMove = false;
                bool isCheck = false;
                bool isWin = false;

                //플레이어 키 입력
                if (turn == 1)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            direction = 4;
                            isMove = true;
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            direction = 3;
                            isMove = true;
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            direction = 2;
                            isMove = true;
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            direction = 1;
                            isMove = true;
                            break;
                        case ConsoleKey.Z:
                            isClick = true;
                            break;
                        default:
                            direction = 0;
                            break;
                    }

                    if (isMove)
                    {
                        map.ChangeMap(player[0].GetX(), player[0].GetY(), player[0].GetX_Hold(direction), player[0].GetY_Hold(direction));
                        player[0].Move(direction);
                        map.SaveBackBuffer();
                    }

                    if (isClick && map.GetMapStatus(player[0].GetX(), player[0].GetY()) == 4)
                    {
                        map.CheckMap(player[0].GetX(), player[0].GetY(), turn);
                        map.SaveBackBuffer();
                        turn *= -1;
                        isCheck = true;

                    }

                    if (isCheck)
                    {
                        int GetX = player[0].GetX();
                        int GetY = player[0].GetY();

                        if (checkTriple(5, GetX, GetY, 2, 0))
                        {
                            isWin = true;
                        }
                        else if (checkTriple(6, GetX, GetY, 2, 0))
                        {
                            isWin = true;
                        }
                        else if(checkTriple(3, GetX, GetY, 2, 0))
                        {
                            isWin = true;
                        }
                        else if(checkTriple(1, GetX, GetY, 2, 0))
                        {
                            isWin = true;
                        }
                    }
                    if (isWin)
                    {
                        map.CopyBufferBacktoFront();
                        map.PrintMap();
                        Console.WriteLine("??승리");
                        Console.ReadKey();
                        return;
                    }

                }
                //컴퓨터
                else if (turn == -1)
                {
                    Random rand = new Random();
                    /*while(map.GetMapStatus(player[1].GetX(), player[1].GetY()) != 2)
                    {
                        direction = rand.Next(1, 4);
                        map.ChangeMap(player[1].GetX(), player[1].GetY(), player[1].GetX(direction), player[1].GetY(direction));
                        player[1].Move(direction);
                        map.SaveBackBuffer();
                    }*/
                    while (true)
                    {
                        int num1 = rand.Next(0, 3);
                        int num2 = rand.Next(0, 3);

                        if (map.GetMapStatus(num1, num2) == 1)
                        {
                            map.CheckMap(num1, num2, turn);
                            turn *= -1;
                            player[0].setAxis(1, 1);
                            player[1].setAxis(num1, num2);
                            map.ChangeMap(player[0].GetX(), player[0].GetY());
                            map.SaveBackBuffer();
                            break;
                        }
                    }

                    int GetX = player[1].GetX();
                    int GetY = player[1].GetY();

                    if (checkTriple(5, GetX, GetY, 2, 1))
                    {
                        isWin = true;
                    }
                    else if (checkTriple(6, GetX, GetY, 2, 1))
                    {
                        isWin = true;
                    }
                    else if (checkTriple(3, GetX, GetY, 2, 1))
                    {
                        isWin = true;
                    }
                    else if (checkTriple(1, GetX, GetY, 2, 1))
                    {
                        isWin = true;
                    }

                    if (isWin)
                    {
                        map.CopyBufferBacktoFront();
                        map.PrintMap();
                        Console.WriteLine("??패배");
                        Console.ReadKey();
                        return;
                    }
                }




                //플레이어 변경
                //turn *= -1;

            }
        }
        public bool checkTriple(int direction, int x, int y, int count, int playerIndex)
        {
            if (map.GetMapStatus(x, y) != player[playerIndex].GetCheckNumber())
            {
                return false;
            }

            if (count == 0)
            {
                if ( ( ((x + y) == 2) && direction == 6) || (x == y && direction == 5) || direction < 5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            int nextX = x + player[playerIndex].GetXDirection(direction);
            int nextY = y + player[playerIndex].GetYDirection(direction);
            if (nextX >= 3)
            {
                nextX = 0;
            }
            
            else if (nextX < 0)
            {
                nextX = 2;
            }
            if (nextY >= 3)
            {
                nextY = 0;
            }
            else if (nextY < 0)
            {
                nextY = 2;
            }
            return checkTriple(direction, nextX, nextY, count - 1, playerIndex);
        }

    }
    public class TicTacMap
    {
        private readonly int WIDTH, HEIGHT;
        private readonly string[] MAP_CHARACTER = { "X", "?", "O", "#", "&", "@" };

        private int[,] map;
        private string[] bufferFront;
        private string[] bufferBack;


        public TicTacMap()
        {
            WIDTH = 3;
            HEIGHT = 3;
            Init();
        }

        public void Init()
        {

            map = new int[HEIGHT, WIDTH];
            for (int x = 0; x < HEIGHT; x++)
            {
                for (int y = 0; y < WIDTH; y++)
                {
                    map[y, x] = 1;

                }
            }

            bufferFront = new string[HEIGHT];
            bufferBack = new string[HEIGHT];
        }

        public void ChangeMap(int beforeX, int beforeY, int afterX, int afterY)
        {
            map[beforeY, beforeX] -= 3;
            map[afterY, afterX] += 3;
        }
        public void ChangeMap(int currX, int currY)
        {
            map[currY, currX] += 3;
        }

        public void CheckMap(int currX, int currY, int turnPlayer)
        {
            if (turnPlayer == 1)
            {
                map[currY, currX] = 2;
            }
            else if (turnPlayer == -1)
            {
                map[currY, currX] = 0;
            }
        }

        public void SaveBackBuffer()
        {
            string line = "";

            for (int vertical = 0; vertical < 3; vertical++)
            {
                line = "";
                for (int horizen = 0; horizen < 3; horizen++)
                {
                    Console.WriteLine(" ");
                    line += (MAP_CHARACTER[map[vertical, horizen]] + "  ");
                }
                bufferBack[vertical] = line;
            }
        }

        public void CopyBufferBacktoFront()
        {
            Array.Copy(this.bufferBack, this.bufferFront, HEIGHT);
            bufferBack = new string[HEIGHT];
        }

        public void PrintMap()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine();
            for (int vertical = 0; vertical < bufferFront.GetLength(0); vertical++)
            {
                Console.WriteLine(" " + bufferFront[vertical] + "\n");
            }

        }

        public int GetMapStatus(int x, int y)
        {
            return map[y, x];
        }


    }

    public class TicTacPlayer
    {
        private readonly int[] AXIS_X = { 0, 0, 0, 1, -1, 1, -1};
        private readonly int[] AXIS_Y = { 0, 1, -1, 0, 0, 1, 1};

        private int positionX;
        private int positionY;

        private int checkNumber;

        public TicTacPlayer(int checkNumber)
        {
            positionX = 1;
            positionY = 1;
            this.checkNumber = checkNumber;
        }

        public int GetCheckNumber()
        {
            return checkNumber;
        }
        public void Move(int direction)
        {
            this.positionX += AXIS_X[direction];
            this.positionY += AXIS_Y[direction];

            HoldEdge();
        }
        private void HoldEdge()
        {

            if (this.positionX == -1)
            {
                this.positionX = 0;
            }
            else if (this.positionX == 3)
            {
                this.positionX = 2;
            }

            if (this.positionY == -1)
            {
                this.positionY = 0;
            }
            else if (this.positionY == 3)
            {
                this.positionY = 2;
            }
        }
        private int HoldEdge(int currAxis)
        {

            if (currAxis == -1)
            {
                currAxis = 0;
            }
            else if (currAxis == 3)
            {
                currAxis = 2;
            }
            return currAxis;

        }
        public void setAxis(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
        }

        public int GetX()
        {
            return positionX;
        }
        public int GetY()
        {
            return positionY;
        }
        public int GetX_Hold(int direction)
        {
            int axis = positionX + AXIS_X[direction];
            return HoldEdge(axis);

        }
        public int GetY_Hold(int direction)
        {
            int axis = positionY + AXIS_Y[direction];
            return HoldEdge(axis);
        }

        public int GetXDirection(int direction)
        {
            return AXIS_X[direction];
        }
        public int GetYDirection(int direction)
        {
            return AXIS_Y[direction];
        }

    }



}
