using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs.Hw0620
{
    public class MyBuffer
    {
        string[] buffer;

        public static int _BUFFER_SIZE = 50;

        public bool isWork { private set; get; }

        private enum color
        {
            WHITE = 0, RED, GREEN, BLUE, YELLOW, CYAN, DARK_GRAY, DARK_RED, DARK_MAGENTA, DARK_BLUE, DARK_CYAN, DARK_GREEN
        }
        
        public MyBuffer()
        {
            isWork = false;
            buffer = new string[_BUFFER_SIZE];
        }

        public void SetBuffer(string[] map)
        {
            buffer = map;
        }

        public void PrintBuffer()
        {
            if (isWork) { return; }
            if (buffer == null) { return; }

            isWork = true;

            for (int y = 0; y < buffer.Length; y++)
            {
                if (buffer[y] == null) { break; }

                Console.SetCursorPosition(0, y);
                string[] splitString = buffer[y].Split('.');

                for (int i = 0; i < splitString.Length; i++)
                {

                    if (i % 3 == 0)
                    {
                        Console.Write(splitString[i]);
                    }
                    else if (i % 3 == 1)
                    {
                        switch (int.Parse(splitString[i]))
                        {
                            case (int)color.WHITE:
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case (int)color.RED:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case (int)color.GREEN:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case (int)color.BLUE:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case (int)color.YELLOW:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case (int)color.CYAN:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case (int)color.DARK_GRAY:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                break;
                            case (int)color.DARK_RED:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case (int)color.DARK_MAGENTA:
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                break;
                            case (int)color.DARK_BLUE:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            case (int)color.DARK_CYAN:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                break;
                            case (int)color.DARK_GREEN:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            default:
                                break;
                        }   //[switch] end 컬러체크
                    }
                    else if (i % 3 == 2)
                    {
                        Console.Write(splitString[i]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }


                }// 1행 출력 종료

            }//모든 행 출력 종료
            Console.SetCursorPosition(0, 17);
            Console.Write("{0,5}\t", Utility.player.score);
            if (Utility.player.score < 5)
            {
                Console.Write("공격 불가능\t\t");
            }
            else
            {

                if (Utility.player.isCoolTime)
                {
                    Console.Write("쿨타임\t\t");
                }
                else
                {
                    Console.Write("공격 가능\t");
                }
            }

            Console.SetCursorPosition(0, 18);
            if (Utility.player.score < 10)
            { 
                Console.Write("포탈 입장 불가능\t\t");            
            }
            else 
            {
                Console.Write("포탈 입장 가능\t\t");
            }
            Console.SetCursorPosition(0, 19);

            isWork = false;
        }
    }

    public static class Utility
    {
        public static Random random = new Random();
        public static Room currRoom = default;
        public static Player player = default;
    }

}
