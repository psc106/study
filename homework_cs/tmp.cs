using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs
{


    //게임 매니저
    public class TicTacGame1
    {

        private TicTacPlayer1[] player;//플레이어(사용자/컴퓨터)
        private TicTacMap1 map;     //맵
        private int startPlayer;    //플레이어부터 시작
        private int[] score;        //점수

        //생성자
        public TicTacGame1()
        {
            player = new TicTacPlayer1[2];
            Init();
            score = new int[3] { 0, 0, 0 };
            startPlayer = 0;
        }
        public TicTacGame1(int startPlayer)
        {
            player = new TicTacPlayer1[2]; 
            Init();
            score = new int[3] { 0, 0, 0 };
            this.startPlayer = startPlayer<2 && startPlayer >= 0 ? startPlayer:0;
        }

        //초기화
        public void Init()
        {
            map = new TicTacMap1();
            player[0] = new TicTacPlayer1(1); //플레이어
            player[1] = new TicTacPlayer1(2); //컴퓨터
            Console.CursorVisible = false;
        }


        //changemap/checkmap->savebuffer->copybuffer->printmap순으로 출력한다.
        public bool Start()
        {
            //현재 턴
            int turn = 1;

            //가독성을 위한 변수
            TicTacPlayer1 human = player[0];
            TicTacPlayer1 currPlayer;

            //현재 턴플레이어의 인덱스
            int turnPlayerIndex = startPlayer;

            //현재 시작 플레이어가 human이면 map 수정
            if (turnPlayerIndex == 0)
            {
                map.ChangeMap(human.GetX(), human.GetY());
            }

            //초기 맵 정보 -> 버퍼 저장
            map.SaveBackBuffer();

            //게임 시작
            while (true)
            {
                currPlayer = player[turnPlayerIndex];

                //승패정보 출력
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("승리[{0}] 패배[{1}] 무승부[{2}]",score[0], score[1], score[2]);

                //백버퍼->프론트버퍼 후 프론트 버퍼 출력
                //초기 정보, 이동, 돌두기 등의 행동 뒤 출력되는 맵 
                map.CopyBufferBacktoFront();
                map.PrintMap();

                //이벤트정보 변수
                //while 돌 때 마다 초기화됨
                int direction = 0;
                bool isClick = false;
                bool isMove = false;
                bool isCheck = false;
                bool isQuit = false;
                bool isWin = false;

                //사용자 턴
                if (turnPlayerIndex == 0)
                {
                    //가독성을 위한 변수 선언
                    int currX = currPlayer.GetX();
                    int currY = currPlayer.GetY();

                    //현재 위치 정보
                    Console.WriteLine();
                    Console.WriteLine(currPlayer.GetX() + " " + currPlayer.GetY() + " " + turn);

                    //플레이어 키 입력
                    //방향키+wasd : 이동
                    //z : 돌두기
                    //q : 종료

                    direction = Input(out isMove, out isClick, out isQuit);

                    if (isQuit)
                    {
                        Console.WriteLine("종료");
                        return false;
                    }

                    //이동키 입력시
                    if (isMove)
                    {
                        //단순 이동시에 맵 정보 변경하는 함수 호출
                        map.ChangeMap(currX, currY, currPlayer.GetX_Hold(direction), currPlayer.GetY_Hold(direction));
                        //플레이어의 실제적인 값 변경
                        currPlayer.Move(direction);
                        //백 버퍼 수정
                        map.SaveBackBuffer();
                    }

                    //돌 두기 입력시+상대와 같은 좌표에 중복입력 불가능
                    if (isClick && map.GetMapStatus(player[0].GetX(), player[0].GetY()) == 3)
                    {
                        //돌 두기시에 맵 정보 변경하는 함수 호출
                        map.CheckMap(currX, currY, turnPlayerIndex);
                        //백 버퍼 수정
                        map.SaveBackBuffer();

                        //돌 둘 때 마다 승리 판정
                        isCheck = true;

                        //돌 둘 경우 플레이어 턴 종료+다음 플레이어한테 넘김
                        turnPlayerIndex = 1;
                        turn += 1;
                    }

                    //승리 판정
                    if (isCheck)
                    {
                        for (int i = 0; i <= 6; i += 2)
                        {
                            if (CheckTriple(i, currX, currY, 2, 0))
                            {
                                isWin = true;
                            }
                        }
                    }

                    //승리 플래그 on 될시에 종료
                    if (isWin)
                    {
                        //바로 종료 되기 때문에 이곳에서 한번 더 맵을 출력해준다.
                        map.CopyBufferBacktoFront();
                        map.PrintMap();

                        //승리메세지
                        Console.WriteLine("??승리");
                        Console.ReadKey();

                        //승리시에 컴퓨터부터 시작
                        startPlayer = 1;

                        //점수 반영
                        this.score[0] += 1;

                        //게임 재시작
                        return true;
                    }


                }

                //컴퓨터
                else if (turnPlayerIndex == 1)
                {
                    int currX;
                    int currY;
                    
                    //컴퓨터는 랜덤으로 처리
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
                        currX = rand.Next(0, 3);
                        currY = rand.Next(0, 3);

                        //현재 좌표의 맵 정보를 가져와서 빈칸인지 비교
                        if (map.GetMapStatus(currX, currY) == 0)
                        {
                            //돌 두기
                            map.CheckMap(currX, currY, turnPlayerIndex);
                            
                            //컴퓨터는 입력하지않기 때문에 좌표 코드로 반영
                            currPlayer.SetAxis(currX, currY);

                            //맵 정보 저장
                            map.SaveBackBuffer();

                            //돌 둘경우 턴종료+다음 플레이어한테 넘김
                            turnPlayerIndex = 0;
                            turn += 1;

                            break;
                        }
                    }

                    //승리 판정
                    for (int i = 0; i <= 6; i += 2)
                    {
                        if (CheckTriple(i, currX, currY, 2, 1))
                        {
                            isWin = true;
                        }
                    }

                    //승리 플래그 on
                    if (isWin)
                    {
                        //함수가 바로 종료되기 때문에 한번 맵 정보 출력해줌
                        map.CopyBufferBacktoFront();
                        map.PrintMap();

                        //패배 메세지
                        Console.WriteLine("??패배");
                        Console.ReadKey();

                        //패배시 사용자 부터 시작
                        startPlayer = 0;

                        //점수 반영
                        this.score[1] += 1;
                        
                        //재시작
                        return true;
                    }

                    //플레이어의 다음 좌표값 설정
                    human.SetAxis(1, 1);

                    //이동했다 가정하고 맵정보 변경
                    map.ChangeMap(human.GetX(), human.GetY());

                    //버퍼에 변경된 정보 변경
                    map.SaveBackBuffer();

                }

                //아무도 승리하지못하고 10턴이 종료되는 순간
                //무승부
                if (turn == 10)
                {
                    //바로 종료되기 때문에 맵 한번 출력
                    map.CopyBufferBacktoFront();
                    map.PrintMap();
                    
                    //무승부 메세지
                    Console.WriteLine("??무승부");
                    Console.ReadKey();

                    //점수반영
                    this.score[2] += 1;

                    //재시작
                    return true;
                }


            }
        }

        //키 인풋
        private int Input(out bool isMove, out bool isClick, out bool isQuit)
        {
            isQuit = false;
            isClick = false;
            isMove = false;

            int direction = 5;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    direction = 3;
                    isMove = true;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    direction = 2;
                    isMove = true;
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    direction = 1;
                    isMove = true;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    direction = 0;
                    isMove = true;
                    break;
                case ConsoleKey.Z:
                    isClick = true;
                    break;
                case ConsoleKey.Q:
                    isQuit = true;
                    break;
                default:
                    direction = 5;
                    break;
            }


            return direction;
        }


        //승패 판정용 재귀 함수
        //입력한 방향으로 3칸(현재칸 포함해서) 이동하고 모두 제대로 이동하면 return true
        //외곽을 벗어날 경우 다음 외곽으로 이동
        public bool CheckTriple(int direction, int x, int y, int count, int playerIndex)
        {
         /*   Console.SetCursorPosition(10 + x, 10 + y);
            Console.WriteLine(count + ")" + x + " " + y + " " + map.GetMapStatus(x, y));*/

            //checknumber == 비교할 값
            //비교한 값이 다르면 바로 false
            if (map.GetMapStatus(x, y) != player[playerIndex].GetCheckNumber())
            {
                return false;
            }

            //모두 이동했을 경우
            if (count == 0)
            {
                //대각선경우1+대각선경우2+가로/세로
                if ((((x + y) == 2) && direction == 4) || (x == y && direction == 6) || direction < 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //외곽 처리
            int nextX = x + player[playerIndex].GetXDirection(direction);
            int nextY = y + player[playerIndex].GetYDirection(direction);
            //가로
            if (nextX >= 3)
            {
                nextX = 0;
            }
            else if (nextX < 0)
            {
                nextX = 2;
            }
            //세로
            if (nextY >= 3)
            {
                nextY = 0;
            }
            else if (nextY < 0)
            {
                nextY = 2;
            }

            //count에 값이 남아있으면 재귀함수 호출 
            return CheckTriple(direction, nextX, nextY, count - 1, playerIndex);
        }

    }//[TicTocGame class] end


    //맵과 관련된 정보를 저장하는 클래스
    public class TicTacMap1
    {
        //맵의 높이,너비
        //출력할 문자들
        private readonly int WIDTH, HEIGHT;
        private readonly string[] MAP_CHARACTER = { "?", "O", "X", "&", "@", "#" };

        //맵
        private int[,] map;

        //2중버퍼
        private string[] bufferFront;
        private string[] bufferBack;

        //생성자
        public TicTacMap1()
        {
            WIDTH = 3;
            HEIGHT = 3;
            Init();
        }

        //초기화
        public void Init()
        {
            //map 빈칸 채우기
            map = new int[HEIGHT, WIDTH];
            for (int x = 0; x < HEIGHT; x++)
            {
                for (int y = 0; y < WIDTH; y++)
                {
                    map[y, x] = 0;

                }
            }

            //버퍼 생성
            bufferFront = new string[HEIGHT];
            bufferBack = new string[HEIGHT];
        }

        //이동시 맵 정보 변경
        public void ChangeMap(int beforeX, int beforeY, int afterX, int afterY)
        {
            map[beforeY, beforeX] -= 3;
            map[afterY, afterX] += 3;
        }

        //세팅시 맵 정보 변경(이동했다 가정)
        public void ChangeMap(int currX, int currY)
        {
            map[currY, currX] += 3;
        }

        //돌 두기시 맵 정보 변경
        public void CheckMap(int currX, int currY, int turnPlayerIndex)
        {
            map[currY, currX] = turnPlayerIndex + 1;
        }

        //백버퍼에 저장하는 함수
        public void SaveBackBuffer()
        {
            //한 줄마다 저장한다.
            string line = "";

            //높이x너비만큼 반복
            for (int vertical = 0; vertical < 3; vertical++)
            {
                //행마다 초기화
                line = "";
                for (int horizen = 0; horizen < 3; horizen++)
                {
                    //맵 정보를 읽고 읽기 전용 문자열로 변경해서 한 줄 저장
                    line += (MAP_CHARACTER[map[vertical, horizen]] + "   ");
                }
                //행에 저장되있던 정보 저장
                bufferBack[vertical] = line;
            }
        }


        //백버퍼->프론트버퍼 + 백버퍼 비움
        public void CopyBufferBacktoFront()
        {
            Array.Copy(this.bufferBack, this.bufferFront, HEIGHT);
            bufferBack = new string[HEIGHT];
        }

        //프론트 버퍼에 있던 정보 프린트
        public void PrintMap()
        {
            for (int vertical = 0; vertical < bufferFront.GetLength(0); vertical++)
            {
                Console.SetCursorPosition(4, 3+ vertical*2);
                Console.WriteLine(bufferFront[vertical] + "\n\n");
            }

        }

        //현재 좌표의 맵 정보 getter
        public int GetMapStatus(int x, int y)
        {
            return map[y, x];
        }


    }


    //플레이어 클래스
    public class TicTacPlayer1
    {
        //읽기 전용 좌표 이동
        private readonly int[] AXIS_X = { 0, 0, 1, -1, -1, 0, 1 };
        private readonly int[] AXIS_Y = { 1, -1, 0, 0, 1, 0, 1 };

        //좌표
        private int positionX;
        private int positionY;

        //비교할 값
        private int checkNumber;

        //생성자
        public TicTacPlayer1(int checkNumber)
        {
            //초기 좌표 1,1
            positionX = 1;
            positionY = 1;
            this.checkNumber = checkNumber;
        }

        //비교할 값 getter
        public int GetCheckNumber()
        {
            return checkNumber;
        }

        //방향 정하면 읽기 전용 배열 이용해서 이동
        public void Move(int direction)
        {
            this.positionX += AXIS_X[direction];
            this.positionY += AXIS_Y[direction];

            //외곽 넘어가면 좌표 고정
            HoldEdge();
        }

        //외곽 고정 함수
        //클래스에 있는 멤버 변수(좌표)를 직접 수정 
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


        //좌표 지정 setter
        public void SetAxis(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
        }

        //현재 x getter
        public int GetX()
        {
            return positionX;
        }

        //현재 y getter
        public int GetY()
        {
            return positionY;
        }

        //외곽 고정된 좌표 반환X
        public int GetX_Hold(int direction)
        {
            int axisX = positionX + AXIS_X[direction];
            if (axisX == -1)
            {
                axisX = 0;
            }
            else if (axisX == 3)
            {
                axisX = 2;
            }
            return axisX;

        }

        //외곽 고정된 좌표 반환Y
        public int GetY_Hold(int direction)
        {
            int axisY = positionY + AXIS_Y[direction];
            if (axisY == -1)
            {
                axisY = 0;
            }
            else if (axisY == 3)
            {
                axisY = 2;
            }
            return axisY;
        }

        //방향에 맞는 X 변동값 GETTER
        public int GetXDirection(int direction)
        {
            return AXIS_X[direction];
        }
        //방향에 맞는 Y 변동값 GETTER
        public int GetYDirection(int direction)
        {
            return AXIS_Y[direction];
        }

    }



}

