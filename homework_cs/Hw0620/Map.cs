using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace homework_cs.Hw0620
{
    /// <summary>
    /// 맵
    /// </summary>
    /// <return>함수가 정상작동시 true</return>
    public class Map
    {
        int[] axisX = { 1, -1, 0, 0 };
        int[] axisY = { 0, 0, -1, 1 };
        public Room[,] field;
        public int size;


        public Map() { }
        public Map(int size)
        {
            this.size = size;
            field = new Room[size, size];
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {

                    //홀수 행, 짝수 열
                    if (y % 2 == 0 && x % 2 == 1)
                    {
                        field[y, x] = new Room(Utility.random.Next(0, 16));

                        if (x == 0 && field[y, x].portal[1] != null)
                        {
                            Door currPortal = field[y, x].portal[1];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[1] = null;
                        }
                        if (x == size - 1 && field[y, x].portal[0] != null)
                        {
                            Door currPortal = field[y, x].portal[0];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[0] = null;
                        }
                        if (y == size - 1 && field[y, x].portal[3] != null)
                        {
                            Door currPortal = field[y, x].portal[3];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[3] = null;
                        }
                        if (y == 0 && field[y, x].portal[2] != null)
                        {
                            Door currPortal = field[y, x].portal[2];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[2] = null;
                        }
                    }
                    else if (y % 2 == 1 && x % 2 == 0)
                    {

                        field[y, x] = new Room(Utility.random.Next(0, 16));
                        if (x == 0 && field[y, x].portal[1] != null)
                        {
                            Door currPortal = field[y, x].portal[1];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[1] = null;
                        }
                        if (x == size - 1 && field[y, x].portal[0] != null)
                        {
                            Door currPortal = field[y, x].portal[0];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[0] = null;
                        }
                        if (y == size - 1 && field[y, x].portal[3] != null)
                        {
                            Door currPortal = field[y, x].portal[3];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[3] = null;
                        }
                        if (y == 0 && field[y, x].portal[2] != null)
                        {
                            Door currPortal = field[y, x].portal[2];
                            field[y, x].roomInfomation[currPortal.Y, currPortal.X] = 0;
                            field[y, x].portal[2] = null;
                        }
                    }

                }

            }

            int cnt = 0;
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {

                    //홀수 행, 짝수 열
                    if (y % 2 == 0 && x % 2 == 1)
                    {
                        if (x == size - 1 && cnt == 0) continue;
                        if (x == 0 && cnt == 1) continue;
                        if (y == size - 1 && cnt == 3) continue;
                        if (y == 0 && cnt == 2) continue;

                        if (field[y, x].portal[cnt] == null)
                        {
                            field[y, x].CreateDoor(cnt);
                            cnt += 1;
                            if (cnt == 4)
                            {
                                cnt = 0;
                            }
                        }
                    }
                    else if (y % 2 == 1 && x % 2 == 0)
                    {
                        if (x == size - 1 && cnt == 0) continue;
                        if (x == 0 && cnt == 1) continue;
                        if (y == size - 1 && cnt == 3) continue;
                        if (y == 0 && cnt == 2) continue;

                        if (field[y, x].portal[cnt] == null)
                        {
                            field[y, x].CreateDoor(cnt);
                            cnt += 1;
                            if (cnt == 4)
                            {
                                cnt = 0;
                            }
                        }
                    }

                }

            }

            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    //홀수 행, 짝수 열
                    if ((y % 2 == 0 && x % 2 == 0) || (y % 2 == 1 && x % 2 == 1))
                    {
                        field[y, x] = new Room();
                        if (x != 0 && field[y, x - 1].portal[0] != null)
                        {
                            Door nextDoor = field[y, x - 1].portal[0];

                            field[y, x].portal[1] = new Door(0, nextDoor.Y);
                            Door currDoor = field[y, x].portal[1];


                            field[y, x].roomInfomation[currDoor.Y, currDoor.X] = 3;

                        }

                        if (x != size - 1 && field[y, x + 1].portal[1] != null)
                        {
                            Door nextDoor = field[y, x + 1].portal[1];

                            field[y, x].portal[0] = new Door(Room.ROOM_SIZE - 1, nextDoor.Y);
                            Door currDoor = field[y, x].portal[0];

                            field[y, x].roomInfomation[currDoor.Y, currDoor.X] = 3;


                        }

                        if (y != size - 1 && field[y + 1, x].portal[2] != null)
                        {
                            Door nextDoor = field[y + 1, x].portal[2];

                            field[y, x].portal[3] = new Door(nextDoor.X, Room.ROOM_SIZE - 1);
                            Door currDoor = field[y, x].portal[3];

                            field[y, x].roomInfomation[currDoor.Y, currDoor.X] = 3;

                        }

                        if (y != 0 && field[y - 1, x].portal[3] != null)
                        {
                            Door nextDoor = field[y - 1, x].portal[3];

                            field[y, x].portal[2] = new Door(nextDoor.X, 0);
                            Door currDoor = field[y, x].portal[2];

                            field[y, x].roomInfomation[currDoor.Y, currDoor.X] = 3;

                        }
                    }

                }

            }
        }

    }

    public class Room
    {
        int[] axisX = { 1, -1, 0, 0 };
        int[] axisY = { 0, 0, -1, 1 };

        int x; int y;

        public int type;

        public List<Enemy> enemies;
        public List<NonPlayableCharacter> NPC;
        public Timer enemyTimer;

        public int[,] roomInfomation;

        public bool isFog;
        public bool isCurrRoom;

        public static int ROOM_SIZE = 15;

        //{동,서,북,남}
        public Door[] portal;

        public Room()
        {
            type = 0;
            isCurrRoom = false;
            isFog = true;
            int wallCount = 60;
            int wallSleep = 2;
            roomInfomation = new int[ROOM_SIZE, ROOM_SIZE];
            for (int y = 0; y < ROOM_SIZE; y++)
            {
                for (int x = 0; x < ROOM_SIZE; x++)
                {
                    if (wallSleep == 0 && wallCount > 0)
                    {
                        roomInfomation[y, x] = Utility.random.Next(3);
                        if (roomInfomation[y, x] == 2)
                        {
                            wallCount -= 1;
                            wallSleep = 3;
                        }
                    }
                    else if (wallSleep > 0 || wallCount == 0)
                    {
                        roomInfomation[y, x] = Utility.random.Next(2);
                        wallSleep -= 1;
                    }
                }
            }
            enemies = new List<Enemy>();
            portal = new Door[4];
        }
        public Room(int flag)
        {
            type = 0;
            isFog = true;
            int wallCount = 60;
            int wallSleep = 2;
            roomInfomation = new int[ROOM_SIZE, ROOM_SIZE];
            for (int y = 0; y < ROOM_SIZE; y++)
            {
                for (int x = 0; x < ROOM_SIZE; x++)
                {
                    if (wallSleep == 0 && wallCount > 0)
                    {
                        roomInfomation[y, x] = Utility.random.Next(3);
                        if (roomInfomation[y, x] == 2)
                        {
                            wallCount -= 1;
                            wallSleep = 3;
                        }
                    }
                    else if (wallSleep > 0 || wallCount == 0)
                    {
                        roomInfomation[y, x] = Utility.random.Next(2);
                        wallSleep -= 1;
                    }
                }
            }

            enemies = new List<Enemy>();
            portal = new Door[4];
            int bit = 1;

            for (int i = 0; i < portal.Length; i++)
            {
                if ((flag & bit) == bit)
                {
                    CreateDoor(i);
                }
                else
                {
                    portal[i] = null;
                }
                bit = bit << 1;
            }

            //portal[0].asd();
        }

        public void BuildTown()
        {
            type = 1;

            StopEnemies();

            if (enemyTimer != null)
            {
                enemyTimer.Dispose();
            }
            for (int y = 0; y < ROOM_SIZE; y++)
            {
                for (int x = 0; x < ROOM_SIZE; x++)
                {

                    if (roomInfomation[y, x] == 1)
                    {
                        roomInfomation[y, x] = 0;
                    }
                }
            }

            NPC = new List<NonPlayableCharacter>();
            NonPlayableCharacter questNPC = new NonPlayableCharacter(ROOM_SIZE / 2 - 1, ROOM_SIZE / 2 - 1, 1);
            NPC.Add(questNPC);
            PlayNPC();

        }

        public void CreateDoor(int index)
        {
            switch (index)
            {
                case 0:
                    portal[index] = new Door(ROOM_SIZE - 1, Utility.random.Next(1, ROOM_SIZE - 1));
                    break;
                case 1:
                    portal[index] = new Door(0, Utility.random.Next(1, ROOM_SIZE - 1));
                    break;
                case 2:
                    portal[index] = new Door(Utility.random.Next(1, ROOM_SIZE - 1), 0);
                    break;
                case 3:
                    portal[index] = new Door(Utility.random.Next(1, ROOM_SIZE - 1), ROOM_SIZE - 1);
                    break;
            }
            roomInfomation[portal[index].Y, portal[index].X] = 3;
        }

        public string[] MakeRoomToString()
        {
            string[] line = new string[3];

            for (int y = 0; y < line.Length; y++)
            {
                line[y] = "";

            }

            if (!this.isFog && this.portal[2] != null)
            {
                line[0] += "　↕　";
            }
            else
            {
                line[0] += "　　　";
            }

            if (!this.isFog && this.portal[1] != null)
            {
                line[1] += "↔";
            }
            else
            {
                line[1] += "　";
            }

            if (!this.isFog)
            {
                if (!this.isCurrRoom)
                {
                    line[1] += "□";
                }
                else
                {
                    line[1] += "■";
                }
            }
            else
            {
                line[1] += "　";
            }

            if (!this.isFog && this.portal[0] != null)
            {
                line[1] += "↔";
            }
            else
            {
                line[1] += "　";
            }
            if (!this.isFog && this.portal[3] != null)
            {
                line[2] += "　↕　";
            }
            else
            {
                line[2] += "　　　";
            }

            return line;
        }

        public void CreateEnemy(object obj)
        {
            if (enemies.Count > 6) return;
            while (true)
            {
                int enemyX = Utility.random.Next(ROOM_SIZE);
                int enemyY = Utility.random.Next(ROOM_SIZE);
                if (roomInfomation[enemyY, enemyX] != 2)
                {
                    Enemy e = new Enemy(enemyX, enemyY);
                    enemies.Add(e);
                    break;
                }
            }
        }
        public void RemoveEnemy(int index)
        {
            if (enemies.Count <= 0) return;
            if (enemies[index].enemyMoveTimer != null)
            {
                enemies[index].enemyMoveTimer.Dispose();
            }
            enemies.RemoveAt(index);
        }
        public void RemoveEnemy(int x, int y)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null) continue;
                if (enemies[i].X == x && enemies[i].Y == y)
                {
                    if (enemies[i].enemyMoveTimer != null)
                    {
                        enemies[i].enemyMoveTimer.Dispose();
                    }
                    enemies.RemoveAt(i);
                }
            }
        }
        public void RemoveNPC(int x, int y)
        {
            for (int i = 0; i < NPC.Count; i++)
            {
                if (NPC[i] == null) continue;
                if (NPC[i].X == x && NPC[i].Y == y)
                {
                    if (NPC[i].walkTimer != null)
                    {
                        NPC[i].walkTimer.Dispose();
                    }
                    NPC.RemoveAt(i);
                }
            }
        }
        public int GetElementAt(int x, int y)
        {
            return roomInfomation[y, x];
        }
        public Enemy FindEnemiesAt(int x, int y)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null) continue;
                if (enemies[i].X == x && enemies[i].Y == y) return enemies[i];
            }
            return null;
        }
        public void StopEnemies()
        {
            if (enemies == null) return;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].enemyMoveTimer != null)
                {
                    enemies[i].enemyMoveTimer.Dispose();
                }
                //enemies[i].enemyTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            }
            return;
        }
        public void PlayEnemies()
        {
            if (enemies == null) return;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].enemyMoveTimer != null)
                {
                    enemies[i].StartTimer();
                }
                //enemies[i].enemyTimer.Change(500, 500);
            }
        }

        public NonPlayableCharacter FindNPCAt(int x, int y)
        {
            for (int i = 0; i < NPC.Count; i++)
            {
                if (NPC[i] == null) continue;
                if (NPC[i].X == x && NPC[i].Y == y) return NPC[i];
            }
            return null;
        }
        public void StopNPC()
        {
            if (NPC == null) return;
            for (int i = 0; i < NPC.Count; i++)
            {
                if (NPC[i].walkTimer != null)
                {
                    NPC[i].walkTimer.Dispose();
                }
            }
            return;
        }
        public void PlayNPC()
        {
            if (NPC == null) return;
            for (int i = 0; i < NPC.Count; i++)
            {
                if (NPC[i].walkTimer != null)
                {
                    NPC[i].walkTimer = new Timer(NPC[i].WalkTimer, null, 100, 3000);
                }
            }
        }
    }

}
public class Door
{
    public int X;
    public int Y;

    public Door()
    {
    }

    public Door(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

}
