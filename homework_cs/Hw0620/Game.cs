using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace homework_cs.Hw0620
{

   
    public class Game
    {
        List<MyEffect> AttackEffect;
        string[] line;
        MyBuffer buffer;
        Map map;

        int currFieldX;
        int currFieldY;
        int direction;

        int[] axisX = { 1, -1, 0, 0, 0 };
        int[] axisY = { 0, 0, -1, 1, 0 };

        public Game()
        {
            line = new string[50];
            buffer = new MyBuffer();
            map = new Map(5);
            AttackEffect = new List<MyEffect>();
        }

        public void Start()
        {
            Console.CursorVisible = false;


            currFieldX = map.size / 2;
            currFieldY = map.size / 2;

            Utility.currRoom = map.field[currFieldY, currFieldX];
            Utility.currRoom.BuildTown();
            
            Utility.currRoom.isFog = false;
            Utility.currRoom.isCurrRoom = true;

            Utility.player = new Player(5, 5);

            direction = 5;
            bool isMove = false;
            bool isAttack = false;
            bool isStun = false;
            bool isTalk = false;



            Timer printTimer = new Timer(PrintMap, line, 100, 100);


            while (true)
            {
                Console.SetCursorPosition(0, 21);

                isMove = false;
                isAttack = false;
                isTalk = false;


                if (!isStun)
                {

                    switch (Console.ReadKey(false).Key)
                    {
                        case ConsoleKey.RightArrow:
                            isMove = true;
                            direction = 0;
                            break;
                        case ConsoleKey.LeftArrow:
                            isMove = true;
                            direction = 1;
                            break;
                        case ConsoleKey.UpArrow:
                            isMove = true;
                            direction = 2;
                            break;
                        case ConsoleKey.DownArrow:
                            isMove = true;
                            direction = 3;
                            break;
                        case ConsoleKey.Z:
                            isAttack = true;
                            break;
                        case ConsoleKey.X:
                            isTalk = true;
                            break;
                        default:
                            direction = 4;
                            break;
                    }

                }
                else
                {
                    Thread.Sleep(1000);
                    while (Console.KeyAvailable) Console.ReadKey(true);

                    isStun = false;
                    direction = 5;
                }

                //패배
                if (!Utility.player.isLive)
                {
                    PrintMap(line);
                    Console.SetCursorPosition(0, 24);
                    Console.WriteLine("패배");

                    printTimer.Dispose();
                    return;
                }

                //이동
                if (isMove)
                {
                    int beforeX = Utility.player.X;
                    int beforeY = Utility.player.Y;
                    bool isWall = Utility.player.MoveAndHold(direction, Room.ROOM_SIZE, Room.ROOM_SIZE);

                    int currX = Utility.player.X;
                    int currY = Utility.player.Y;


                    //빈칸으로 이동
                    if (Utility.currRoom.roomInfomation[currY, currX] == 0)
                    {
                        if (!isWall)
                        {
                            Utility.player.score += 1;

                        }
                    }

                    //수풀로 이동
                    if (Utility.currRoom.roomInfomation[currY, currX] == 1)
                    {
                        if (!isWall)
                        {
                            Utility.player.score += 2;
                            if (Utility.random.Next(1, 101) <= 20)
                            {
                                direction = 4;
                                isStun = true;
                            }
                        }
                    }
                    //벽으로 이동
                    else if (Utility.currRoom.roomInfomation[currY, currX] == 2)
                    {
                        Utility.player.X = beforeX;
                        Utility.player.Y = beforeY;
                    }
                    //텔레포트로 이동
                    else if (Utility.currRoom.roomInfomation[currY, currX] == 3)
                    {
                        if (Utility.player.score >= 10)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (Utility.currRoom.portal[i] == null) { continue; }


                                if (currX == Utility.currRoom.portal[i].X && currY == Utility.currRoom.portal[i].Y)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            currFieldX += 1;
                                            break;
                                        case 1:
                                            currFieldX -= 1;

                                            break;
                                        case 2:
                                            currFieldY -= 1;

                                            break;
                                        case 3:
                                            currFieldY += 1;

                                            break;
                                    }
                                    Utility.player.Teleport(Room.ROOM_SIZE, Room.ROOM_SIZE);
                                    Utility.player.score -= 10;

                                    Utility.currRoom.isCurrRoom = false;
                                    Utility.currRoom.StopEnemies();
                                    Utility.currRoom.StopNPC();
                                    if (Utility.currRoom.enemyTimer != null)
                                    {
                                        Utility.currRoom.enemyTimer.Dispose();
                                    }

                                    //Utility.currRoom.enemyTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

                                    Utility.currRoom = map.field[currFieldY, currFieldX];
                                    if (Utility.currRoom.type == 0)
                                    {
                                        Utility.currRoom.PlayEnemies();
                                        Utility.currRoom.PlayNPC();
                                        Utility.currRoom.enemyTimer = new Timer(Utility.currRoom.CreateEnemy, null, 100, 10000);
                                    }
                                    Utility.currRoom.isFog = false;
                                    Utility.currRoom.isCurrRoom = true;

                                    break;
                                }
                            }
                        }
                    }

                    //적에게 이동
                    Enemy enemy = Utility.currRoom.FindEnemiesAt(currX, currY);
                    for (int i = 0; i < Utility.currRoom.enemies.Count; i++)
                    {
                        Utility.currRoom.enemies[i].playerChange = true;
                    }
                    if (enemy != null && enemy.isLive)
                    {
                        Utility.player.hitPoint -= 1;
                        enemy.enemyMoveTimer.Dispose();
                        if (Utility.player.hitPoint == 0)
                        {
                            Utility.player.isLive = false;
                            Utility.currRoom.enemies.RemoveAll(x => x.X == enemy.X && x.Y == enemy.Y);
                        }
                        Utility.currRoom.enemies.RemoveAll(x => x.X == enemy.X && x.Y == enemy.Y);
                    }

                    //npc에게 이동
                    NonPlayableCharacter npc = Utility.currRoom.FindNPCAt(currX, currY);                    
                    if (npc != null && npc.isLive)
                    {
                        Utility.player.X = beforeX;
                        Utility.player.Y = beforeY;
                    }

                }//[if(isMove)] 종료

                //공격
                if (isAttack)
                {

                    if (Utility.player.score >= 5 && !Utility.player.isCoolTime)
                    {
                        Utility.player.isCoolTime = true;
                        Utility.player.attackTimer = new Timer(Utility.player.AttackTimer, null, 1000, 0);

                        int nextX = Utility.player.GetNextX(direction);
                        int nextY = Utility.player.GetNextY(direction);

                        Utility.player.score -= 5;
                        
                        AttackEffect.Add(new MyEffect(nextX, nextY, 0));

                        if (Utility.player.weapon == 1)
                        {
                            if (direction / 2 == 0)//양쪽 보는중
                            {
                                AttackEffect.Add(new MyEffect(nextX, nextY + 1, 0));
                                AttackEffect.Add(new MyEffect(nextX, nextY - 1, 0));
                            }
                            else if (direction / 2 == 1)//위쪽 보는중
                            {
                                AttackEffect.Add(new MyEffect(nextX+1, nextY, 0));
                                AttackEffect.Add(new MyEffect(nextX-1, nextY, 0));
                            }
                        } 
                        else if (Utility.player.weapon == 2)
                        {
                            if (direction == 0)//오른쪽 보는중
                            {
                                AttackEffect.Add(new MyEffect(nextX + 1, nextY, 0));
                                AttackEffect.Add(new MyEffect(nextX + 2, nextY, 0));
                            }
                            else if (direction == 1)//왼쪽 보는중
                            {
                                AttackEffect.Add(new MyEffect(nextX - 1, nextY, 0));
                                AttackEffect.Add(new MyEffect(nextX - 2, nextY, 0));
                            }
                            else if (direction == 2)//위쪽 보는중
                            {
                                AttackEffect.Add(new MyEffect(nextX, nextY-1, 0));
                                AttackEffect.Add(new MyEffect(nextX, nextY-2, 0));
                            }
                            else if (direction == 3)//아래쪽 보는중
                            {
                                AttackEffect.Add(new MyEffect(nextX, nextY + 1, 0));
                                AttackEffect.Add(new MyEffect(nextX, nextY + 2, 0));
                            }
                        }
                        
                    }

                    for (int i = 0; i < AttackEffect.Count; i++)
                    {
                        if (AttackEffect[i].type == -1) continue;

                        int nextX = AttackEffect[i].X;
                        int nextY = AttackEffect[i].Y;

                        //적 공격
                        Enemy currEnemy = Utility.currRoom.FindEnemiesAt(nextX, nextY);
                        if (currEnemy != null)
                        {
                            if (currEnemy.isLive)
                            {
                                currEnemy.hitPoint -= 1;
                                currEnemy.enemyMoveTimer.Change(400, 400);
                                if (currEnemy.hitPoint == 0)
                                {
                                    Utility.currRoom.RemoveEnemy(nextX, nextY);
                                    Utility.player.score += 15;
                                }
                            }
                            continue;
                        }

                        //npc 공격
                        NonPlayableCharacter currNPC = Utility.currRoom.FindNPCAt(nextX, nextY);
                        if (currNPC != null)
                        {
                            if (currNPC.isLive)
                            {
                                currNPC.hitPoint -= 1;
                                if (currNPC.hitPoint == 0)
                                {
                                    Utility.currRoom.RemoveNPC(nextX, nextY);
                                    Utility.player.score -= 10;
                                }
                            }
                            continue;
                        }

                        //벽 공격
                        if (Utility.currRoom.roomInfomation[nextY, nextX] == 2)
                        {
                            Utility.player.HoldXY(ref nextX, ref nextY, Room.ROOM_SIZE, Room.ROOM_SIZE);
                            Utility.currRoom.roomInfomation[nextY, nextX] = 0;

                            continue;
                        }

                        //함정 공격
                        if (Utility.currRoom.roomInfomation[nextY, nextX] == 1)
                        {
                            Utility.player.HoldXY(ref nextX, ref nextY, Room.ROOM_SIZE, Room.ROOM_SIZE);
                            Utility.currRoom.roomInfomation[nextY, nextX] = 0;

                            continue;
                        }
                    }
                    
                }


                //대화
                if (isTalk)
                {
                    int nextX = Utility.player.GetNextX(direction);
                    int nextY = Utility.player.GetNextY(direction);

                    NonPlayableCharacter currNPC = Utility.currRoom.FindNPCAt(nextX, nextY);

                    if (currNPC != null)
                    {
                        if (currNPC.isLive)
                        {

                        }
                        continue;
                    }

                }

            }

        }

        public void MapToStringArray(ref string[] line)
        {
            for (int y = 0; y < Room.ROOM_SIZE; y++)
            {
                line[y] = "";
                for (int x = 0; x < Room.ROOM_SIZE; x++)
                {
                    //이펙트 1순위
                    if (AttackEffect!=null && AttackEffect.Count>0)
                    {
                        MyEffect currEffect = AttackEffect.FindAll(effect => (effect.X == x && effect.Y == y)).FirstOrDefault();
                        if (currEffect != null)
                        {
                            line[y] += ".8."+ MyEffect.ATTACK_STRING[currEffect.type]+ ".";
                            AttackEffect.Remove(currEffect);
                            continue;
                        }                        
                    }

                    //적 2순위
                    Enemy tmp = Utility.currRoom.FindEnemiesAt(x, y);
                    if (tmp != null)
                    {
                        if (tmp.isLive)
                        {
                            switch (tmp.hitPoint)
                            {
                                case 1:
                                    line[y] += ".7.";
                                    break;
                                case 2:
                                    line[y] += ".1.";
                                    break;
                                default:
                                    line[y] += ".0.";
                                    break;
                            }
                            if (tmp.randMoveCount > 0)
                            {
                                line[y] += "？.";
                            }
                            else
                            {
                                line[y] += "적.";
                            }
                        }
                        else if (!tmp.isLive)
                        {
                            line[y] += ".3.봉.";
                        }
                        continue;
                    }

                    //NPC 3순위
                    NonPlayableCharacter npc = Utility.currRoom.FindNPCAt(x, y);
                    if (npc != null)
                    {
                        if (npc.isLive)
                        {
                            if (0 <= npc.hitPoint && npc.hitPoint < 10)
                            {
                                line[y] += ".9.";
                            }
                            else if (10 <= npc.hitPoint && npc.hitPoint <= 20)
                            {
                                line[y] += ".10.";
                            }
                            else{
                                line[y] += ".11.";
                            }
                            line[y] += "★.";                            
                        }
                        else if (!tmp.isLive)
                        {
                            line[y] += ".3.봉.";
                        }
                        continue;
                    }

                    //플레이어 4순위
                    if (Utility.player.X == x && Utility.player.Y == y)
                    {
                        if (Utility.player.isLive)
                        {
                            switch (Utility.player.hitPoint)
                            {
                                case 1:
                                    line[y] += ".6.";
                                    break;
                                case 2:
                                    line[y] += ".5.";
                                    break;
                                case 3:
                                    line[y] += ".4.";
                                    break;
                                default:
                                    line[y] += ".0.";
                                    break;
                            }
                            switch (direction)
                            {
                                case 0:
                                    line[y] += "▶.";
                                    break;
                                case 1:
                                    line[y] += "◀.";
                                    break;
                                case 2:
                                    line[y] += "▲.";
                                    break;
                                case 3:
                                    line[y] += "▼.";
                                    break;
                                case 4:
                                    line[y] += "！.";
                                    break;
                                default:
                                    line[y] += "나.";
                                    break;
                            }

                        }
                        else
                        {
                            line[y] += ".3.Π.";
                        }
                        continue;
                    }

                    //맵정보 마지막
                    switch (Utility.currRoom.roomInfomation[y, x])
                    {
                        case 0:
                            line[y] += ".0.　.";
                            break;
                        case 1:
                            line[y] += ".0.＊.";
                            break;
                        case 2:
                            line[y] += ".0.〓.";
                            break;
                        case 3:
                            line[y] += ".2.문.";
                            break;
                    }
                    
                }
            }


            for (int y = 0; y < map.field.GetLength(0); y++)
            {
                for (int x = 0; x < map.field.GetLength(1); x++)
                {
                    string[] tmp = map.field[y, x].MakeRoomToString();

                    line[y * 3] += tmp[0];
                    line[y * 3 + 1] += tmp[1];
                    line[y * 3 + 2] += tmp[2];
                }
            }

        }

        public void PrintMap(Object obj)
        {
            MapToStringArray(ref line);
            if (!buffer.isWork)
            {
                buffer.SetBuffer((string[])obj);
                buffer.PrintBuffer();
            }
            else
            {
                Console.Clear();
            }
        }

    }


}
