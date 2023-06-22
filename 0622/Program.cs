using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0622
{

    // F = 최종 점수 (작을 수록 좋음, 경로에 따라 달라짐)
    // G = 시작점에서 해당 좌표까지 이동하는 데 드는 비용 (작을 수록 좋음, 경로에 따라 달라짐)
    // H = 목적지에서 얼마나 가까운지 (작을 수록 좋음, 고정된 값)
    public class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }

    public class Test
    {
        protected int num1;
        protected int num2;
        public void Init(int num1, int num2)
        {
            this.num1 = num1;
            this.num2 = num2;
        }

        public virtual void Print()
        {
            Console.WriteLine(position.x+" "+position);
        }

        protected (int x, int y) position = (1, 1);

    }
    public class TestChild : Test
    {
        public override void Print()
        {
            base.Print();
            position.x = 10;
            base.Print();
        }
    }

    public class Program
    {
        /*static public void PrintValue(int value)
        {
            Console.Write("int {0} 입력", value);
        }
        static public void PrintValue(float value)
        {
            Console.Write("float {0} 입력", value);
        }
        static public void PrintValue(char value)
        {
            Console.Write("char {0} 입력", value);
        }
        static public void PrintValue(string value)
        {
            Console.Write("string {0} 입력", value);
        }*/

        //제네릭=템플릿
        //where 제한 가능/제약 조건/
        //컴파일 타임에 타입 추론 <-> var는 런타임에 타입 추론
        static public void PrintValue<T>(T genericValue) where T : Test
        {
            Console.Write("T {0} 입력", genericValue);
        }



        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            Test t1 = new Test();
            TestChild t2 = new TestChild();
            PrintValue(t1);
            PrintValue(t2);

            //msdn
            //nullable
            Console.WriteLine();
            string str = "asd asd gfd";
            string[] s;
            s = str.Split(' ');
            Console.WriteLine(s.Length);
            Console.WriteLine();


            t2?.Print();
            Console.WriteLine();
            Console.WriteLine();

            int? number = null;
            Console.ReadKey(true) ;
            Console.Clear();

            Console.Title = "A* Pathfinding";

            // draw map  
            string[] map = new string[]
            {
                "+------------+",
                "|     X      |",
                "|  X     X  B|",
                "|  X     X   |",
                "|  X     X   |",
                "|A X     X   |",
                "|            |",
                "+------------+",
            };
            var start = new Location { X = 1, Y = 5 };
            var target = new Location { X = 12, Y = 2 };

            int SLEEP_TIME = 100;

            foreach (var line in map)
                Console.WriteLine(line);

            // algorithm  
            Location current = null;
            var openList = new List<Location>();
            var closedList = new List<Location>();
            int g = 0;

            // start by adding the original position to the open list  
            openList.Add(start);

            //열린 필드(시작 이동 위치? 아직 안 간곳)의 크기가 1이상일 경우
            while (openList.Count > 0)
            {
                //현재 열린 필드에서 제일 낮은 값을 가져온다.
                // get the square with the lowest F score  
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                //닫힌필드에 뽑아낸 값(로케이션 클래스) 추가
                // add the current square to the closed list  
                closedList.Add(current);
                                
                // show current square on the map  
                Console.SetCursorPosition(current.X, current.Y);
                Console.Write(g);
                Console.SetCursorPosition(current.X, current.Y);
                System.Threading.Thread.Sleep(SLEEP_TIME);

                //열린필드에 뽑아낸 값 제거
                // remove it from the open list  
                openList.Remove(current);

                //출구/목적지와 같은 값이 저장되어 있을 경우 종료
                // if we added the destination to the closed list, we've found a path  
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;

                //다음 이동 가능한 필드를 저장한다.(상하좌우)
                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map, openList);
                //이동가중치 + 1
                g = current.G + 1;

                //모든 접근 가능한 곳으로 이동해본다.
                foreach (var adjacentSquare in adjacentSquares)
                {
                    //현재 이동한 위치가 닫힌 필드에 있을 경우 다음 값 체크(continue)
                    // if this adjacent square is already in the closed list, ignore it  
                    if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                        continue;

                    //현재 이동한 위치가 열린 필드에 없을때
                    // if it's not in the open list...  
                    if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                    {
                        //현재 이동한 가중치만큼 증가
                        // compute its score, set the parent
                        adjacentSquare.G = g;
                        //현재 이동한 위치와 출구까지의 절대값 저장
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        //가중치 합(이동한 위치+출구까지의 절대값)
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        //이전 위치 저장
                        adjacentSquare.Parent = current;

                        //새로운 경로 생성(해당 위치에서 시작하는 값들 추가)
                        // and add it to the open list  
                        openList.Insert(0, adjacentSquare);
                    }
                    //현재 이동한 위치가 열린 필드에 있을때(이전에 한번 접근해봣을경우)
                    else
                    {
                        //이전 가중치의 합(F)가 현재 가중치의 합(g+adjacentSquare.H)이 더 클경우 새로 변경한다.
                        // test if using the current G score makes the adjacent square's F score  
                        // lower, if yes update the parent because it means it's a better path  
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            //가중치 값 변경
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            //현재위치에서 이동하는 것이 더 좋기 때문에 이전 필드 위치를 현재 위치로 변경
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            //최종 위치를 현재 위치로 변경
            Location end = current;

            // 결과값이 제대로 찾아졌을 경우
            // assume path was found; let's show it  
            while (current != null)
            {
                Console.SetCursorPosition(current.X, current.Y);
                Console.Write('_');
                Console.SetCursorPosition(current.X, current.Y);
                current = current.Parent;
                System.Threading.Thread.Sleep(SLEEP_TIME);
            }

            if (end != null)
            {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Path : {0}", end.G);
            }

            // end  
            Console.ReadLine();
        }

        static public List<Location> GetWalkableAdjacentSquares(int x, int y, string[] map, List<Location> openList)
        {
            List<Location> list = new List<Location>();

            if (map[y - 1][x] == ' ' || map[y - 1][x] == 'B')
            {
                Location node = openList.Find(l => l.X == x && l.Y == y - 1);
                if (node == null) list.Add(new Location() { X = x, Y = y - 1 });
                else list.Add(node);
            }

            if (map[y + 1][x] == ' ' || map[y + 1][x] == 'B')
            {
                Location node = openList.Find(l => l.X == x && l.Y == y + 1);
                if (node == null) list.Add(new Location() { X = x, Y = y + 1 });
                else list.Add(node);
            }

            if (map[y][x - 1] == ' ' || map[y][x - 1] == 'B')
            {
                Location node = openList.Find(l => l.X == x - 1 && l.Y == y);
                if (node == null) list.Add(new Location() { X = x - 1, Y = y });
                else list.Add(node);
            }

            if (map[y][x + 1] == ' ' || map[y][x + 1] == 'B')
            {
                Location node = openList.Find(l => l.X == x + 1 && l.Y == y);
                if (node == null) list.Add(new Location() { X = x + 1, Y = y });
                else list.Add(node);
            }

            return list;
        }

        static public int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}

