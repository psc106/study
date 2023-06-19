using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619.Card
{
    public class MainSystem
    {
        private Card[] mainDeck;

        private Card[] playerDeck;
        private Card[] computerDeck;
        enum ranking
        {
            TOP = 0, ONE, TWO, TRI, STR, FLUSH, FULL, FOUR, STFL, ROYAL
        }

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

            for (int i = 0; i < mainDeck.Length; i++)
            {
                mainDeck[i] = new Card(i);
            }
        }

        private void Shuffle()
        {
            for (int i = 0; i < 300; i++)
            {
                mainDeck.Swap(Utility.random.Next(0, 52), Utility.random.Next(0, 52));
            }
        }

        public void start()
        {
            Print p = new Print();
            //덱 탑 위치
            int currPos = 0;

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

            int computerRank = CheckRank(ref computerDeck);

            p.PrintComDeck(computerDeck.SortNumber_2Start());

            p.PrintPlayerDeck(playerDeck);


            int changeIndex;
            int.TryParse(Console.ReadLine(), out changeIndex);

            if (changeIndex >= 1 && changeIndex <= 5)
            {
                playerDeck[changeIndex-1] = mainDeck[currPos];
                currPos += 1;
            }
            p.PrintPlayerDeck(playerDeck);


            int.TryParse(Console.ReadLine(), out changeIndex);

            if (changeIndex >= 1 && changeIndex <= 5)
            {
                playerDeck[changeIndex-1] = mainDeck[currPos];
                currPos += 1;
            }
            p.PrintPlayerDeck(playerDeck);

            int playerRank = CheckRank(ref playerDeck);


            if (playerRank > computerRank)
            {
                Console.WriteLine("승");
            }
            else if (playerRank < computerRank)
            {
                Console.WriteLine("패");
            }
            else if (playerRank == computerRank)
            {
                Console.WriteLine("무승부?");
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
                Console.WriteLine("로얄");
                return 10;
            }

            else if (IsStraightFlush(check2))
            {
                deck.SortNumber_2Start(null);
                IsStraightFlush(deck);
                Console.WriteLine("스플");
                return 9;
            }

            else if (IsFourCard(check2))
            {
                deck.SortNumber_2Start(null);
                IsFourCard(deck);
                Console.WriteLine("포");
                return 8;
            }
            else if (IsFullHouse(check2))
            {
                deck.SortNumber_2Start(null);
                IsFullHouse(deck);
                Console.WriteLine("풀");
                return 7;
            }
            else if (IsFlush(check1))
            {
                deck.SortPattern(null);
                IsFlush(deck);
                Console.WriteLine("플");
                return 6;
            }
            else if (IsStraight(check2))
            {
                deck.SortNumber_2Start(null);
                IsStraight(deck);
                Console.WriteLine("스트");
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
                Console.WriteLine("투페");
                return 3;
            }
            else if (IsOnePair(check2))
            {
                deck.SortNumber_2Start(null);
                IsOnePair(deck);
                Console.WriteLine("원페");
                return 2;
            }
            else
            {
                Console.WriteLine("탑?");
                return 1;
            }


        }

        public bool IsRoyal(Card[] deck)
        {
            int count = 0;
            int last = 0;
            Dictionary<int, int> arr;
            for (int i = 0; i < deck.Length - 4; i++)
            {
                last = 0;
                arr = new Dictionary<int, int>();
                count = 0;
                int num = deck[i].GetCardNumberNum();
                int pattern = deck[i].GetCardPatternNum();
                for (int j = i + 1; j <= i + 4 + i; j++)
                {

                    if (j >= deck.Length) { return false; }


                    if (num == deck[j].GetCardNumberNum())
                    {
                        i += 1;
                        num = deck[j].GetCardNumberNum();
                        if (arr.ContainsKey(deck[j].GetCardPatternNum()))
                        {
                            arr[deck[j].GetCardPatternNum()] += 1;
                        }
                        else
                        {
                            arr.Add(deck[j].GetCardPatternNum(), 1);
                        }
                        continue;
                    }

                    if (deck[j].GetCardNumberNum() - num == 1)
                    {
                        count += 1;
                        num = deck[j].GetCardNumberNum();
                        if (arr.ContainsKey(deck[j].GetCardPatternNum()))
                        {
                            arr[deck[j].GetCardPatternNum()] += 1;
                        }
                        else
                        {
                            arr.Add(deck[j].GetCardPatternNum(), 1);
                        }
                        last = deck[j].GetCardNumberNum();
                    }
                    else
                    {
                        break;
                    }

                }
                if (count == 4)
                {
                    foreach (var a in arr)
                    {
                        if (a.Value == 4)
                        {
                            if (last == 12)
                            {
                                return true;
                            }
                        }
                    }

                }

            }
            return false;
        }

        public bool IsStraightFlush(Card[] deck)
        {
            int count = 0;
            int start = 0;
            Dictionary<int, int> arr;
            for (int i = 0; i <= deck.Length - 4; i++)
            {
                start = i;
                arr = new Dictionary<int, int>();
                count = 0;
                int num = deck[i].GetCardNumberNum();
                for (int j = i + 1; j <= i + 4 + i; j++)
                {

                    if (j >= deck.Length) { return false; }

                    if (num == deck[j].GetCardNumberNum())
                    {
                        i += 1;
                        num = deck[j].GetCardNumberNum();
                        if (arr.ContainsKey(deck[j].GetCardPatternNum()))
                        {
                            arr[deck[j].GetCardPatternNum()] += 1;
                        }
                        else
                        {
                            arr.Add(deck[j].GetCardPatternNum(), 1);
                        }
                        continue;
                    }

                    if (deck[j].GetCardNumberNum() - num == 1)
                    {
                        count += 1;
                        num = deck[j].GetCardNumberNum();
                        if (arr.ContainsKey(deck[j].GetCardPatternNum()))
                        {
                            arr[deck[j].GetCardPatternNum()] += 1;
                        }
                        else
                        {
                            arr.Add(deck[j].GetCardPatternNum(), 1);
                        }
                    }

                }
                if (count == 3)
                {
                    if (deck[start].GetCardNumberNum() == 0)
                    {
                        foreach (var a in arr)
                        {
                            if (a.Value == 3)
                            {

                                for (int j = deck.Length - 1; deck[j].GetCardNumberNum() != 12; j--)
                                {
                                    if (a.Key == deck[j].GetCardNumberNum())
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }

                }
                if (count == 4)
                {
                    foreach (var a in arr)
                    {
                        if (a.Value == 4)
                        {
                            return true;
                        }
                    }

                }

            }
            return false;
        }

        public bool IsFourCard(Card[] deck)
        {
            for (int startIndex = 0; startIndex < deck.Length - 3; startIndex++)
            {
                int sameCount = 0;
                for (int i = startIndex; i < startIndex + 2; i++)
                {
                    if (deck[i].GetCardNumberNum() == deck[i + 1].GetCardNumberNum())
                    {
                        sameCount += 1;
                        continue;
                    }

                }
                if (sameCount == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse(Card[] deck)
        {
            Card[] arr = new Card[deck.Length];
            int number = 0;

            for (int startIndex = 0; startIndex < deck.Length - 2; startIndex++)
            {
                number = 0;
                int sameCount = 0;
                Array.Copy(deck, arr, arr.Length);

                for (int i = startIndex; i < startIndex + 2; i++)
                {
                    if (deck[i].GetCardNumberNum() == deck[i + 1].GetCardNumberNum())
                    {
                        sameCount += 1;
                        if (sameCount == 2)
                        {
                            number = deck[i].GetCardNumberNum();
                        }
                        continue;
                    }


                }
                if (sameCount == 2)
                {
                    for (int i = 0; i < deck.Length-1; i++)
                    {
                        if (number != deck[i].GetCardNumberNum() && deck[i].GetCardNumberNum() == deck[i + 1].GetCardNumberNum())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsFlush(Card[] deck)
        {
            bool isSameColor = true;

            for (int startIndex = 0; startIndex < deck.Length - 4; startIndex++)
            {
                for (int i = startIndex; i < startIndex + 4; i++)
                {
                    if (isSameColor)
                    {
                        if (deck[i].GetCardPatternNum() != deck[i + 1].GetCardPatternNum())
                        {
                            isSameColor = false;
                        }
                    }
                }
                if (isSameColor)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsStraight(Card[] deck)
        {
            int count = 0;
            int start = 0;
            for (int i = 0; i < deck.Length - 4; i++)
            {
                count = 0;
                start = i;
                int num = deck[i].GetCardNumberNum();
                for (int j = i + 1; j <= i + 4 + i; j++)
                {

                    if (j >= deck.Length) { return false; }

                    if (num == deck[j].GetCardNumberNum())
                    {
                        i += 1;
                        num = deck[j].GetCardNumberNum();
                        continue;
                    }

                    if (deck[j].GetCardNumberNum() - num == 1)
                    {
                        count += 1;
                        num = deck[j].GetCardNumberNum();
                    }
                }

                if (count == 3)
                {
                    if (deck[start].GetCardNumberNum() == 0 && deck[deck.Length - 1].GetCardNumberNum() == 12)
                    {
                        return true;

                    }

                }
                if (count == 4)
                {
                    return true;
                }

            }

            return false;
        }

        public bool IsTriple(Card[] deck)
        {
            for (int startIndex = 0; startIndex < deck.Length - 2; startIndex++)
            {
                int sameCount = 0;

                for (int i = startIndex; i < startIndex + 2; i++)
                {
                    if (deck[i].GetCardNumberNum() == deck[i + 1].GetCardNumberNum())
                    {
                        sameCount += 1;
                        continue;
                    }


                }
                if (sameCount == 2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsTwoPair(Card[] deck)
        {
            int sameCount = 0;

            for (int startIndex = 0; startIndex < deck.Length - 1; startIndex++)
            {

                if (deck[startIndex].GetCardNumberNum() == deck[startIndex + 1].GetCardNumberNum())
                {
                    sameCount += 1;
                    startIndex += 1;
                }

                if (sameCount == 2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsOnePair(Card[] deck)
        {
            int sameCount = 0;

            for (int startIndex = 0; startIndex < deck.Length - 1; startIndex++)
            {

                if (deck[startIndex].GetCardNumberNum() == deck[startIndex + 1].GetCardNumberNum())
                {
                    sameCount += 1;
                    startIndex += 1;
                }

                if (sameCount == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static class Utility
    {
        public static Random random = new Random();

        public static void Swap(this Card[] arr, int index1, int index2)
        {
            Card tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }
        public static Card[] SortPattern(this Card[] arr)
        {
            int min = 0;
            Card[] copyArr = new Card[arr.Length];

            Array.Copy(arr, copyArr, arr.Length);


            for (int i = 0; i < copyArr.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < copyArr.Length; j++)
                {
                    int currPattern = copyArr[j].cardNumber / 13;
                    int currNumber = copyArr[j].cardNumber % 13;

                    if (copyArr[min].GetCardPatternNum() > currPattern)
                    {
                        min = j;
                    }
                    else if (copyArr[min].GetCardPatternNum() == currPattern)
                    {
                        if (copyArr[min].GetCardNumberNum() > currNumber)
                        {
                            min = j;
                        }
                    }
                }
                if (i != min)
                {
                    Utility.Swap(copyArr, min, i);
                }
            }

            return copyArr;
        }

        public static Card[] SortNumber_2Start(this Card[] arr)
        {
            int min = 0;
            Card[] copyArr = new Card[arr.Length];

            Array.Copy(arr, copyArr, copyArr.Length);

            for (int i = 0; i < copyArr.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < copyArr.Length; j++)
                {
                    int currPattern = copyArr[j].cardNumber / 13;
                    int currNumber = copyArr[j].cardNumber % 13;

                    if (copyArr[min].GetCardNumberNum() > currNumber)
                    {
                        min = j;
                    }
                    else if (copyArr[min].GetCardNumberNum() == currNumber)
                    {
                        if (copyArr[min].GetCardPatternNum() > currPattern)
                        {
                            min = j;
                        }
                    }
                }
                if (i != min)
                {
                    Utility.Swap(copyArr, min, i);
                }
            }

            return copyArr;
        }
        public static void SortPattern(this Card[] copyArr, Object obj)
        {
            int min = 0;

            for (int i = 0; i < copyArr.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < copyArr.Length; j++)
                {
                    int currPattern = copyArr[j].cardNumber / 13;
                    int currNumber = copyArr[j].cardNumber % 13;

                    if (copyArr[min].GetCardPatternNum() > currPattern)
                    {
                        min = j;
                    }
                    else if (copyArr[min].GetCardPatternNum() == currPattern)
                    {
                        if (copyArr[min].GetCardNumberNum() > currNumber)
                        {
                            min = j;
                        }
                    }
                }
                if (i != min)
                {
                    Utility.Swap(copyArr, min, i);
                }
            }
        }

        public static void SortNumber_2Start(this Card[] copyArr, Object obj)
        {
            int min = 0;

            for (int i = 0; i < copyArr.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < copyArr.Length; j++)
                {
                    int currPattern = copyArr[j].cardNumber / 13;
                    int currNumber = copyArr[j].cardNumber % 13;

                    if (copyArr[min].GetCardNumberNum() > currNumber)
                    {
                        min = j;
                    }
                    else if (copyArr[min].GetCardNumberNum() == currNumber)
                    {
                        if (copyArr[min].GetCardPatternNum() > currPattern)
                        {
                            min = j;
                        }
                    }
                }
                if (i != min)
                {
                    Utility.Swap(copyArr, min, i);
                }
            }

        }

    }
}
