using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0612
{
    public class SecretCode
    {
        private char code;
        private int count;

        private char hintUp;
        private char hintDown;

        //생성자
        public SecretCode() 
        {
            Init();
        }

        //초기화 코드
        private void Init()
        {
            this.code = (char)new Random().Next(33, 126);
            this.count = -1;

            this.hintUp = '!';
            this.hintDown = '~';
        }

        //도움 코드 출력
        private void PrintHelpCode()
        {
            for (int i = '!'; i <= '~'; i++)
            {
                //화살표 -> 범위
                if (i == hintUp)
                {
                    Console.Write("▶");
                }
                Console.Write(" {0} ", (char)i);
                if (i == hintDown)
                {
                    Console.Write("◀");
                }

                //10칸마다 띄어쓰기
                if ((i - '!') % 10 == 9)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("\n");

        }

        //게임 시작
        public void Start() 
        {
            //게임 초기화
            Init();
            char answer;

            while (true)
            {
                //입력
                char.TryParse(Console.ReadLine(), out answer);

                //카운트+1
                this.count += 1;

                //도움코드
                if (answer < '!' || answer > '~')
                {
                    this.PrintHelpCode();
                    continue;
                }
                 
                //up,down
                if (answer < code)
                {
                    Console.WriteLine("UP");
                    hintUp = answer;
                }
                else if (answer > code)
                {
                    Console.WriteLine("DOWN");
                    hintDown = answer;
                }
                else
                {
                    Console.WriteLine("RIGHT!");
                    break;
                }
            }

        }

        //결과값 반환
        public int GetResult()
        {
            return count;
        }


    }
}
