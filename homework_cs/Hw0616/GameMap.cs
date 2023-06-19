
using homework_0616;
using homework_cs.Hw0616;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs.Hw0616
{
    //게임 맵 
    public abstract class GameMap
    {
        public int[,] field { get; protected set; }
        public enum InfomationLine
        {
            status1 = 2, game = 40, status2 = 50, debug = 60
        }

        protected void Init(int empty)
        {

            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1) - 1; x++)
                {
                    field[y, x] = empty;
                }
            }
        }

        public virtual void SetFieldObject(GameObject obj)
        {
            field[obj.Y, obj.X] = obj.objectID;
        }


        public void SetFieldAt(int x, int y, int info)
        {
            field[y, x] = info;
        }
        public int GetFieldAt(int x, int y)
        {
            return field[y, x];
        }

        protected string GetEmptyLine()
        {
            string emptyLine = "";
            for (int i = Console.WindowLeft; i < Console.WindowWidth; i++)
            {
                emptyLine += " ";
            }
            return emptyLine;
        }
        protected string GetEmptyLine(int cursor)
        {
            string emptyLine = "";
            for (int i = cursor; i < Console.WindowWidth; i++)
            {
                emptyLine += " ";
            }
            return emptyLine;
        }

    }



    public class PortalMap : GameMap, BufferHelp
    {
        public static int _PORTAL_MAP_SIZE = 10;


        public PortalMap()
        {
            this.field = new int[_PORTAL_MAP_SIZE, _PORTAL_MAP_SIZE];
            this.Init(0);
        }

        public PortalMap(GamePlayer player)
        {
            this.field = new int[_PORTAL_MAP_SIZE, _PORTAL_MAP_SIZE];
            this.Init(0);
            this.field[player.Y, player.X] = 5;
        }


        public override void SetFieldObject(GameObject obj)
        {
            field[obj.Y, obj.X] = obj.objectID;
        }


        public string[] MapToStringArray(Object direction)
        {
            string[] map = new string[GameBuffer._BUFFER_SIZE];
            string line;
            int cursorPosition = 0;
            int yPosition = 0;

            for (int bufferY = 0; bufferY < GameBuffer._BUFFER_SIZE; bufferY++)
            {
                line = "";
                cursorPosition = 0;

                //맵 정보 출력
                if (bufferY < (int)GameMap.InfomationLine.status1)
                {
                    line = this.GetEmptyLine();
                    map[bufferY] = line;
                    continue;
                }

                //맵 출력
                else if (bufferY < (int)GameMap.InfomationLine.game)
                {
                    if (yPosition >= field.GetLength(1))
                    {
                        line = this.GetEmptyLine();
                        map[bufferY] = line;
                        continue;
                    }
                    for (int x = 0; x < field.GetLength(1); x++)
                    {
                        if (field[yPosition, x] < DataManager._PORTAL_STRING.Length)
                        {
                            line += (" " + DataManager._PORTAL_STRING[field[yPosition, x]]);
                        }
                        else
                        {
                            line += (" " + DataManager._PLAYER_STRING[(int)direction]);
                        }
                        cursorPosition += 3;
                    }
                    yPosition += 1;
                    line = line + this.GetEmptyLine(cursorPosition);
                    map[bufferY] = line;
                    continue;
                }

                //상태 출력
                else if (bufferY < (int)GameMap.InfomationLine.status2)
                {
                    line = this.GetEmptyLine();
                    map[bufferY] = line;
                    continue;
                }

                //디버그 출력
                else if (bufferY < (int)GameMap.InfomationLine.debug)
                {
                    line = this.GetEmptyLine();
                    map[bufferY] = line;
                    continue;
                }
            }
            return map;
        }
    }

    public class ShopMap : GameMap, BufferHelp
    {
        public static int _SHOP_MAP_SIZE = 3;
        public int[][] itemSlot;

        public ShopMap(List<GameItem> shopList)
        {
            itemSlot = new int[_SHOP_MAP_SIZE][];
        }

        public string[] MapToStringArray(object obj)
        {

            string[] map = new string[GameBuffer._BUFFER_SIZE];
            string line;
            int cursorPosition = 0;
            int yPosition = 0;

            for (int bufferY = 0; bufferY < GameBuffer._BUFFER_SIZE; bufferY++)
            {
                line = "";
                cursorPosition = 0;

                //맵 정보 출력
                if (bufferY < (int)GameMap.InfomationLine.status1)
                {
                    line = this.GetEmptyLine();
                    map[bufferY] = line;
                    continue;
                }

                //맵 출력
                else if (bufferY < (int)GameMap.InfomationLine.game)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if()
                    }
                    map[bufferY] = line;
                    continue;
                }

                //상태 출력
                else if (bufferY < (int)GameMap.InfomationLine.status2)
                {
                    line = this.GetEmptyLine();
                    map[bufferY] = line;
                    continue;
                }

                //디버그 출력
                else if (bufferY < (int)GameMap.InfomationLine.debug)
                {
                    line = this.GetEmptyLine();
                    map[bufferY] = line;
                    continue;
                }
            }
            return map;
        }
    }

    public class BattleMap : GameMap
    {
    }


    public class CardMap : GameMap
    {
    }

}
