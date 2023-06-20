using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619.Card
{
    public partial class MainSystem
    {
        private Card[] mainDeck;

        private Card[] playerDeck;
        private Card[] computerDeck;
        

        public MainSystem()
        {
            this.Init();
            this.Shuffle();
        }

        private void Init()
        {
            mainDeck = new Card[52];
            playerDeck = new Card[5];
            computerDeck = new Card[7];
            
            //Test();
            for (int i = 0; i < mainDeck.Length; i++)
            {
                mainDeck[i] = new Card(i);
            }
        }       

        private void Shuffle()
        {
            for (int i = 0; i < mainDeck.Length; i++)
            {
                mainDeck[i].isSelect = false;
            }

            for (int i = 0; i < 300; i++)
            {
                mainDeck.Swap(Utility.random.Next(0, 52), Utility.random.Next(0, 52));
            }
        }

        public void start()
        {
            int gold = 100;
            Print p = new Print();
            Console.CursorVisible = false;

            //덱 탑 위치
            int currPos = 0;

            while (true)
            {
                int batting = -1;

                if (currPos >= 38)
                {
                    currPos = 0;
                    Shuffle();
                }

                for (int i = 0; i < 7; i++)
                {
                    computerDeck[i] = mainDeck[currPos];
                    currPos += 1;
                }

                for (int i = 0; i < 5; i++)
                {
                    playerDeck[i] = mainDeck[currPos];
                    currPos += 1;
                }

                Console.SetCursorPosition(0, 0);
                int computerRank = CheckRank(ref computerDeck);

                playerDeck.SortNumber_2Start(null);
                p.PrintComDeck(computerDeck.SortNumber_2Start());
                p.PrintPlayerDeck(playerDeck);


                while (batting < 0 || batting > gold)
                {
                    Console.SetCursorPosition(0, 28);
                    Console.Write("배팅(0 ~ {0}) :                             ", gold);
                    Console.SetCursorPosition(16, 28);
                    int.TryParse(Console.ReadLine(), out batting);
                }

                gold -= batting;

                int position = 0;
                int boolCount = 0;
                bool isSelect = false;
                bool isQuit = false;
                bool isYes = false;
                bool isNo = false;
                bool isMove = false;

                while (true)
                {
                    p.PrintPlayerDeck(playerDeck.SortNumber_2Start());
                    p.printCursor(position);

                    isYes = false;
                    isNo = false;
                    isMove = false;
                    isSelect = false;

                    int direction = 0;

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            direction = -1;
                            isMove = true;
                            break;

                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            direction = 1;
                            isMove = true;
                            break;

                        case ConsoleKey.Z:
                            isYes = true;
                            break;

                        case ConsoleKey.X:
                            isNo = true;
                            break;

                        case ConsoleKey.Q:
                            isQuit = true;
                            break;

                        case ConsoleKey.Enter:
                            isSelect = true;
                            break;
                    }

                    if (isQuit)
                    {
                        return;
                    }

                    if (isSelect)
                    {
                        if (boolCount >= 0 && boolCount <= 2)
                        {
                            break;
                        }
                    }

                    if (isMove)
                    {
                        position += direction;
                        if (position < 0)
                        {
                            position = playerDeck.Length - 1;
                        }

                        else if (position >= playerDeck.Length)
                        {
                            position = 0;
                        }
                    }

                    if (isYes && !playerDeck[position].isSelect)
                    {
                        boolCount += 1;
                        playerDeck[position].isSelect = true;
                    }
                    if (isNo && playerDeck[position].isSelect)
                    {
                        boolCount -= 1;
                        playerDeck[position].isSelect = false;
                    }

                }

                for (int i = 0; i < playerDeck.Length; i++)
                {
                    if (playerDeck[i].isSelect)
                    {
                        playerDeck[i] = mainDeck[currPos];
                        currPos += 1;
                    }
                }

                p.PrintPlayerDeck(playerDeck.SortNumber_2Start());
                Console.SetCursorPosition(0, 25);
                Console.Write("패 교환   ");
                Console.ReadKey(true);

                Console.SetCursorPosition(0, 26);
                int playerRank = CheckRank(ref playerDeck);
                p.PrintPlayerDeck(playerDeck.SortNumber_2Start());
                Console.ReadKey(true);

                if (playerRank > computerRank)
                {
                    Console.WriteLine("승      ");
                    gold += batting * 2;
                }
                else if (playerRank < computerRank)
                {
                    Console.WriteLine("패      ");
                }
                else if (playerRank == computerRank)
                {
                    if (playerRank == 1)
                    {
                        if (playerDeck[playerDeck.Length - 1].GetCardNumberNum() >
                            computerDeck[computerDeck.Length - 1].GetCardNumberNum())
                        {
                            Console.WriteLine("승      ");
                            gold += batting * 2;
                        }
                        else if (playerDeck[playerDeck.Length - 1].GetCardNumberNum() <
                            computerDeck[computerDeck.Length - 1].GetCardNumberNum())
                        {
                            Console.WriteLine("패      ");
                        }
                        else
                        {
                            Console.WriteLine("무승부?  ");
                            gold += batting;
                        }
                    }
                    else
                    {
                        Console.WriteLine("무승부?  ");
                        gold += batting;
                    }
                }

                if (gold == 0)
                {
                    Console.WriteLine("파산?    ");
                    return;
                }
                else if (gold == 500)
                {
                    Console.WriteLine("부자?    ");
                    return;
                }

                Console.ReadKey(true);
                Console.Clear();
            }
        }

        public int CheckRank(ref Card[] deck)
        {
            Card[] check1 = deck.SortPattern();
            Card[] check2 = deck.SortNumber_2Start();

            if (IsRoyal(check1))
            {
                deck.SortPattern(null);
                IsRoyal(deck);
                Console.WriteLine("로얄 스트레이트 플러시");
                return 10;
            }

            else if (IsStraightFlush2(check1))
            {
                deck.SortPattern(null);
                IsStraightFlush2(deck);
                Console.WriteLine("스트레이트 플러시");
                return 9;
            }

            else if (IsFourCard(check2))
            {
                deck.SortNumber_2Start(null);
                IsFourCard(deck);
                Console.WriteLine("포카드   ");
                return 8;
            }
            else if (IsFullHouse(check2))
            {
                deck.SortNumber_2Start(null);
                IsFullHouse(deck);
                Console.WriteLine("풀 하우스");
                return 7;
            }
            else if (IsFlush(check1))
            {
                deck.SortPattern(null);
                IsFlush(deck);
                Console.WriteLine("플러시");
                return 6;
            }
            else if (IsStraight(check2))
            {
                deck.SortNumber_2Start(null);
                IsStraight(deck);
                Console.WriteLine("스트레이트");
                return 5;
            }
            else if (IsTriple(check2))
            {
                deck.SortNumber_2Start(null);
                IsTriple(deck);
                Console.WriteLine("트리플");
                return 4;
            }
            else if (IsTwoPair(check2))
            {
                deck.SortNumber_2Start(null);
                IsTwoPair(deck);
                Console.WriteLine("투페어");
                return 3;
            }
            else if (IsOnePair(check2))
            {
                deck.SortNumber_2Start(null);
                IsOnePair(deck);
                Console.WriteLine("원페어");
                return 2;
            }
            else
            {
                deck.SortNumber_2Start(null);
                deck[deck.Length - 1].isSelect = true;
                Console.WriteLine("탑");
                return 1;
            }
        }

        private void Test()
        {
            mainDeck[0] = new Card(1);
            mainDeck[1] = new Card(2);
            mainDeck[2] = new Card(3);
            mainDeck[3] = new Card(4);
            mainDeck[4] = new Card(5);
            mainDeck[5] = new Card(6);
            mainDeck[6] = new Card(7);
            mainDeck[7] = new Card(50);
            mainDeck[8] = new Card(51);
            mainDeck[9] = new Card(10);
            mainDeck[10] = new Card(20);
            mainDeck[11] = new Card(9);
            mainDeck[12] = new Card(28);
            mainDeck[13] = new Card(32);
            for (int i = 14; i < mainDeck.Length; i++)
            {
                mainDeck[i] = new Card(i);
            }
        }
    }


}
