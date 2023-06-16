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
            PLAYER = 0, BLANK, WALL, STONE, hole, full
        }

        #region 클래스 멤버 변수
        //객체
        SokobanMap map;
        SokobanBuffer buffer;
        SokobanPlayer player;
        List<SokobanStone> stones;

        //일반
        public bool isRestart { get; private set; }
        private int score;
        private int size;
        #endregion 

        //생성자
        //소코반 객체가 생성할때 size를 받아야한다.
        public Sokoban(int size)
        {
            this.size = size + 2;
            this.Init();
        }

        //초기화
        public void Init()
        {
            this.score = 0;
            map = new SokobanMap(size);
            buffer = new SokobanBuffer(size);

            player = new SokobanPlayer(size);
            stones = new List<SokobanStone>();
        }

        //게임 시작
        public bool Start()
        {
            //사용자가 게임중에 r 눌렀을 경우에
            if (isRestart)
            {
                //초기화
                this.Init();
                isRestart = false;
            }

            //지역 변수
            bool isQuit = false;
            bool isMove = false;
            bool isStone = false;
            int currDirection = 0;

            //돌생성, 화면출력 자동
            Timer stoneTimer = new Timer(this.MakeStone, this.size, 500, 1500);
            Timer printTimer = new Timer(this.PrintMap, this.size, 500, 100);

            //플레이어 위치 반영
            map.SetFieldAt(player.X, player.Y, (int)map_str.PLAYER);
            
            //커서깜빡임 비활성화
            Console.CursorVisible = false;

            //게임 시작
            while (true)
            {
                SokobanStone currStone = null;

                int beforeX = player.X;
                int beforeY = player.Y;
                int currX = player.X;
                int currY = player.Y;

                //키입력
                switch (Console.ReadKey(true).Key)
                {
                    //이동
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        currDirection = 3;
                        isMove = true;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        currDirection = 2;
                        isMove = true;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        currDirection = 1;
                        isMove = true;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        currDirection = 0;
                        isMove = true;
                        break;

                    //종료
                    case ConsoleKey.Q:
                        isQuit = true;
                        break;

                    //재시작
                    case ConsoleKey.R:
                        isRestart = true;
                        break;

                    //예외
                    default:
                        currDirection = 4;
                        break;
                }//키 입력 종료

                //게임 종료시
                //Q일 경우 게임을 다시 시작하지않음
                //R일 경우 게임을 다시 시작함
                if (isQuit || isRestart)
                {
                    stoneTimer.Dispose();
                    printTimer.Dispose();
                    for (int i = 0; i < stones.Count; i++)
                    {
                        if (stones[i].pushTimer != null)
                        {
                            stones[i].pushTimer.Dispose();
                        }
                    }
                    this.PrintMap(size);

                    if      (isQuit)    { return false; }
                    else if (isRestart) { return true;  }
                }//게임 종료 end

                //이동시
                if (isMove)
                {
                    player.MoveXY_Hold(size, currDirection);
                    currX = player.X;
                    currY = player.Y;

                    //빈공간으로 이동시
                    if (map.GetFieldAt(currX, currY) == (int)map_str.BLANK)
                    {
                        map.SetFieldAt(beforeX, beforeY, (int)map_str.BLANK);
                        map.SetFieldAt(currX, currY, (int)map_str.PLAYER);
                    }

                    //벽 부딫침
                    else if (map.GetFieldAt(currX, currY) == (int)map_str.WALL)
                    {
                        player.SetPosition(beforeX, beforeY);
                    }

                    //돌 부딫침
                    else if (map.GetFieldAt(currX, currY) == (int)map_str.STONE)
                    {
                        //돌 검색
                        currStone = stones.Find(x => x.X == currX && x.Y == currY);
                        
                        //제대로 된 돌일경우 처리
                        if (currStone != null)
                        {
                            currStone.Direction = currDirection;

                            isStone = true;
                        }

                        //맵상에만 돌이 존재할경우(버그)
                        //걍 밀어버린다.
                        else
                        {
                            map.SetFieldAt(beforeX, beforeY, (int)map_str.BLANK);
                            map.SetFieldAt(currX, currY, (int)map_str.PLAYER);
                        }
                    }
                }//이동 end

                //돌 옮길시
                if (isStone && currStone != null)
                {
                    //다음 돌의 좌표 저장
                    int nextStoneX = currStone.GetNextX(currX, size);
                    int nextStoneY = currStone.GetNextY(currY, size);

                    //다음이 빈칸이면 플레이어 이동
                    if (map.GetFieldAt(nextStoneX, nextStoneY) == (int)map_str.BLANK)
                    {
                        //플레이어의 좌표 변경
                        currStone.SetPosition(nextStoneX, nextStoneY);
                        
                        //맵상에 반영
                        map.SetFieldAt(beforeX, beforeY, (int)map_str.BLANK);
                        map.SetFieldAt(currX, currY, (int)map_str.PLAYER);
                                                
                        //돌 좌표 1번은 무조건 이동
                        map.SetFieldAt(nextStoneX, nextStoneY, (int)map_str.STONE);
                        
                        //고정되어있던 돌은 자동 이동
                        if (currStone.pushTimer == null)
                        {
                            currStone.isStop = false;
                            currStone.pushTimer = new Timer(PushStone, currStone, 500, 500);
                        }

                    }
                    //아닐 경우 플레이어 고정
                    else
                    {
                        player.SetPosition(beforeX, beforeY);
                    }

                }//돌옮기기 end

            }//[Start-while] end

        }//[Start] end


        //맵은 매초 그려진다.
        public void PrintMap(Object size)
        {
            //버퍼저장->프론트로 변경(안해도될듯?)->버퍼 출력
            buffer.SaveBackBuffer(map.field);
            buffer.CopyBufferBacktoFront((int)size);
            buffer.PrintMap();

            //추가 정보 출력
            Console.SetCursorPosition(0, (int)size + 1);
            Console.WriteLine("점수 {0}", score);
        }

        //돌은 매초 생성된다
        public void MakeStone(Object size)
        {
            //돌 최대 10개 생성
            if (stones.Count >= 10)
            {
                return;
            }

            //랜덤
            Random rand = new Random();

            while (true)
            {
                int x = rand.Next(0, (int)size);
                int y = rand.Next(0, (int)size);

                //설정한 위치가 빈칸일 경우 통과
                if (map.GetFieldAt(x, y) == (int)map_str.BLANK)
                {
                    //객체생성->리스트추가->맵에반영
                    SokobanStone stone = new SokobanStone(x, y);
                    stones.Add(stone);

                    map.SetFieldAt(x, y, (int)map_str.STONE);

                    return;
                }
            }
        }

        //돌 밀리는 함수
        public void PushStone(Object stone)
        {
            //검사용 배열 변수
            int[,] check = { { 1, -1 }, { 1, 2 }, { -1, -2 } };

            SokobanStone currStone = (SokobanStone)stone;
            SokobanStone checkStone1 = null;
            SokobanStone checkStone2 = null;

            int currX = currStone.X;
            int currY = currStone.Y;
            int nextX = currStone.GetNextX(currX, size);
            int nextY = currStone.GetNextY(currY, size);

            //외곽에 닿을 경우
            //빈칸이 아닌곳에 닿을 경우
            //즉 다음에 이동이 안될 경우
            if ((nextX == 0 || nextX == size - 1 || nextY == 0 || nextY == size - 1) ||
                map.GetFieldAt(nextX, nextY) != (int)map_str.BLANK)
            {

                //경우1 x축 +1,-1/+1+2/-1-2
                for (int i = 0; i < 3; i++)
                {
                    //제거용 인덱스 저장
                    int check1 = stones.FindIndex(x => (x.Y == currY) && (x.X == currX + check[i, 0]));
                    int check2 = stones.FindIndex(x => (x.Y == currY) && (x.X == currX + check[i, 1]));

                    //두개다 선택되야 통과
                    if (check1 == -1 || check2 == -1)
                    {
                        continue;
                    }
                    //맞추기 처리
                    else
                    {
                        //코드용 객체 저장
                        checkStone1 = stones[check1];
                        checkStone2 = stones[check2];

                        //둘다 멈춰진 상태일 경우만 처리
                        if ((checkStone1.isStop) && (checkStone2.isStop))
                        {
                            //위에 있는 인덱스부터 지움
                            if (check1 > check2)
                            {
                                //돌 이동 함수 멈춤
                                currStone.pushTimer.Dispose();

                                //맵에 반영 후 리스트 제거
                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check1);
                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check2);

                                //현재 돌의 인덱스 저장
                                int checkCurr = stones.FindIndex(x => (x.Y == currY) && (x.X == currX));

                                //맵에 반영 후 리스트 제거
                                map.SetFieldAt(currX, currY, (int)map_str.BLANK);
                                stones.RemoveAt(checkCurr);

                                //스코어 + 1
                                score += 1;

                                return;
                            }//[PushStone-for-if] end x축 3매치 처리 완료


                            //위와 동일한 로직
                            else
                            {
                                currStone.pushTimer.Dispose();

                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check2);
                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check1);

                                int checkCurr = stones.FindIndex(x => (x.Y == currY) && (x.X == currX));
                                map.SetFieldAt(currX, currY, (int)map_str.BLANK);
                                stones.RemoveAt(checkCurr);
                                score += 1;

                                return;
                            }//[PushStone-for-else] end x축 3매치 처리 완료
                        }

                    }//[PushStone-for-else] end x축 3매치 처리 시작

                }//[PushStone-for] end x축


                //경우2 y축 +1,-1/+1+2/-1-2
                //위의 for문과 동일
                for (int i = 0; i < 3; i++)
                {
                    int check1 = stones.FindIndex(x => (x.X == currX) && (x.Y == currY + check[i, 0]));
                    int check2 = stones.FindIndex(x => (x.X == currX) && (x.Y == currY + check[i, 1]));

                    if (check1 == -1 || check2 == -1)
                    {
                        continue;
                    }

                    //[PushStone-for-else] Y축 3매치 처리 시작
                    else
                    {
                        checkStone1 = stones[check1];
                        checkStone2 = stones[check2];

                        if ((checkStone1.isStop) && (checkStone2.isStop))
                        {
                            if (check1 > check2)
                            {
                                currStone.pushTimer.Dispose();

                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check1);
                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check2);

                                int checkCurr = stones.FindIndex(x => (x.Y == currY) && (x.X == currX));
                                map.SetFieldAt(currX, currY, (int)map_str.BLANK);
                                stones.RemoveAt(checkCurr);
                                score += 1;

                                return;
                            }//[PushStone-for-if] end Y축 3매치 처리 완료

                            else
                            {
                                currStone.pushTimer.Dispose();

                                map.SetFieldAt(checkStone2.X, checkStone2.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check2);
                                map.SetFieldAt(checkStone1.X, checkStone1.Y, (int)map_str.BLANK);
                                stones.RemoveAt(check1);

                                int checkCurr = stones.FindIndex(x => (x.Y == currY) && (x.X == currX));
                                map.SetFieldAt(currX, currY, (int)map_str.BLANK);
                                stones.RemoveAt(checkCurr);
                                score += 1;

                                return;
                            }//[PushStone-for-else] end Y축 3매치 처리 완료
                        }

                    }//[PushStone-for-else] end Y축 3매치 처리 시작

                }//[PushStone-for] end Y축

                //3매칭이 안될 경우 그 자리에서 멈춘다.
                currStone.pushTimer.Dispose();
                currStone.pushTimer = null;
                currStone.isStop = true;

                return;

            }

            //다음 위치로 이동
            map.SetFieldAt(currX, currY, (int)map_str.BLANK);
            currStone.SetPosition(currStone.GetNextX(currX, size), currStone.GetNextY(currY, size));
            map.SetFieldAt(currStone.X, currStone.Y, (int)map_str.STONE);
        }

    }



    //맵 클래스
    public class SokobanMap
    {
        public int[,] field { get; private set; }
        private int size;

        //생성자
        public SokobanMap(int size)
        {
            //사이즈 입력
            this.size = size;
            this.field = new int[this.size, this.size];
            
            //생성시 외곽에 벽을 두른다.
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                    {
                        field[i, j] = 2;
                        continue;
                    }
                    field[i, j] = 1;
                }

            }
        }

        //좌표에 있는 값 변경
        public void SetFieldAt(int x, int y, int number)
        {
            this.field[x, y] = number;
        }

        //좌표에 있는 값 반환
        public int GetFieldAt(int x, int y)
        {
            return this.field[x, y];
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
        //(없어도 될거같음)
        public void CopyBufferBacktoFront(int size)
        {
            Array.Copy(this.backBuffer, this.frontBuffer, size);
            backBuffer = new string[size];
        }

        //해당 버퍼 클래스가 출력중일 경우 맵 그리기 안함
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


    //게임에서 쓰이는 객체
    //부모클래스
    public class SokobanObject
    {
        //이동 좌표
        //(X,y) 기준
        //오른쪽/왼쪽/아래/위/정지
        protected readonly int[] AXIS_X = { 0, 0, 1, -1, 0 };
        protected readonly int[] AXIS_Y = { 1, -1, 0, 0, 0 };

        public int Direction { set; protected get; }
        public int X { get; protected set; }
        public int Y { get; protected set; }

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

        protected int TeleportPosition(int axis, int size)
        {
            if (axis == size)
            {
                return 0;
            }
            else if (axis == -1)
            {
                return axis + size;
            }

            return axis;
        }

        public void SetPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    //플레이어 객체
    //정지하는 기능이 있다.
    public class SokobanPlayer : SokobanObject
    {
        //private readonly string[] PLAYER_DIRECTION = { "◀", "▶", "▼", "▲", "나" };
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

    //돌 객체
    //다음 좌표를 계산하는 기능이 있다.
    //timer를 저장해서 돌마다 자동으로 이동하고 벽에 부딫치면 정지할수있게 한다.
    public class SokobanStone : SokobanObject
    {
        
        public bool isStop { set; get; }
        public Timer pushTimer;

        //생성자
        public SokobanStone() { }
        public SokobanStone(int x, int y)
        {
            isStop = true;
            this.X = x;
            this.Y = y;
        }

        //소멸자
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