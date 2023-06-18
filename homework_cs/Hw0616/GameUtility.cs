using homework_cs.Hw0616;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace homework_0616
{
    //출력 담당
    public class GameBuffer
    {
        string[] buffer;

        public static int _BUFFER_SIZE = 50;

        public bool isWork { private set; get; }

        private enum color
        {
            WHITE = 0, RED = 1, GREEN = 2, BLUE = 3, YELLOW = 4
        }

        public GameBuffer()
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
            isWork = true;

            for (int y = 0; y < _BUFFER_SIZE; y++)
            {
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
            isWork = false;
        }


        public void PrintBuffer2()
        {
            if (isWork) { return; }
            isWork = true;

            for (int y = 0; y < _BUFFER_SIZE; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(buffer[y]);
            }
            isWork = false;
        }
    }



    public static class DataManager
    {
        public static Dictionary<int, GameItem> ITEM_DATABASE;
        public static string[] _PLAYER_STRING = { ".4.♣.", ".4.▲.", ".4.◀.", ".4.▼.", ".4.▶." };
        public static string[] _PORTAL_STRING = { "□", ".1.◎.", ".2.◎.", ".3.◎." };

    }

    public interface BufferHelp
    {
        string[] MapToStringArray(Object obj);
    }
}
