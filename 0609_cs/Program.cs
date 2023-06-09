using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0609_cs
{
    internal class Program
    {
        static readonly string[] CARD_PATTERNS = { "♠", "◆", "♥", "♣" };
        static readonly string[] CARD_NUMBERS = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        static readonly string[] TURN_PLAYER_NAME = { "  당신", "컴퓨터" };
        static void Main(string[] args)
        {
            Random rand = new Random();
            int[] deck = new int[52];
            int[] dummy = new int[52];

            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = i;
                dummy[i] = -1;
            }

            for (int i = 0; i < 300; i++)
            {
                Swap(deck, rand.Next() % 52, rand.Next() % 52);
            }


            int[,] selectCard = new int[2, 2];

            while (true)
            {
                if (deck[51] == -1)
                {
                    Console.WriteLine("모든 카드를 뽑았습니다.");
                    Console.ReadKey();
                    Console.Clear();

                    Array.Copy(dummy, deck, deck.Length);
                    dummy = Enumerable.Repeat(-1, dummy.Length).ToArray();

                    for (int i = 0; i < 300; i++)
                    {
                        Swap(deck, rand.Next() % 52, rand.Next() % 52);
                    }
                }

                int turn = 0;

                turn = PlayTurn(selectCard, turn, GetCard(deck, dummy));
                Console.ReadKey();

                turn = PlayTurn(selectCard, turn, GetCard(deck, dummy));
                Console.ReadKey();

                turn = PlayTurn(selectCard, turn, GetCard(deck, dummy));
                Console.ReadKey();

                turn = PlayTurn(selectCard, turn, GetCard(deck, dummy));
                Console.ReadKey();

                int mySum = (selectCard[0, 0] % 13+1) + (selectCard[0, 1] % 13+1);
                int comSum = (selectCard[1, 0] % 13+1) + (selectCard[1, 1] % 13+1);

                if (mySum > comSum)
                {
                    Console.WriteLine("{2}의 승리({0} : {1})", mySum, comSum, TURN_PLAYER_NAME[0]);
                }
                else if (comSum > mySum)
                {
                    Console.WriteLine("{2}의 승리({0} : {1})", mySum, comSum, TURN_PLAYER_NAME[1]);
                }
                else
                {
                    int myMax, comMax;

                    //내 가장 큰값 인덱스 구함
                    //컴퓨터 가장 큰값 인덱스 구함
                    myMax = selectIndex(selectCard, 0);
                    comMax = selectIndex(selectCard, 1);

                    //승리 구분
                    if (selectCard[0, myMax] / 13 > selectCard[1, comMax] / 13)
                    {
                        Console.WriteLine("내 승리");
                    }
                    else if (selectCard[0, myMax] / 13 < selectCard[1, comMax] / 13)
                    {
                        Console.WriteLine("컴퓨터 승리");
                    }
                    else
                    {
                        if (selectCard[0, myMax] < selectCard[1, comMax])
                        {
                            Console.WriteLine("내 승리");
                        }
                        else
                        {
                            Console.WriteLine("컴퓨터 승리");
                        }
                    }

                }
                Console.ReadKey();
                Console.Clear();
            }

        }

        static int selectIndex(int[,] arr, int index)
        {
            if (arr[index, 0] % 13 > arr[index, 1] % 13)
            {
                return 0;
            }
            else if (arr[index, 0] % 13 < arr[index, 1] % 13)
            {
                return 1;
            }
            else
            {
                return arr[index, 0] < arr[index, 1] ? 0 : 1;
            }

        }

        static int PlayTurn(int[,] cardDeck, int turn, int currCard)
        {

            int currIndex = ((int)(turn * 0.5));
            int currPlayer = turn % 2;

            int currPattern = ((int)(currCard / 13));
            int currNumber = currCard % 13;

            cardDeck[currPlayer, currIndex] = currCard;
            PrintCard(turn, currCard);

            return turn + 1;
        }

        static void PrintCard(int turn, int currCard)
        {

            int currIndex = ((int)(turn * 0.5));
            int currPlayer = turn % 2;

            int currPattern = ((int)(currCard / 13));
            int currNumber = currCard % 13;
            int cursor = currIndex * 15 + currPlayer * 35;
            Console.SetCursorPosition(cursor, 0);
            Console.WriteLine("┌────────┐");
            Console.SetCursorPosition(cursor, 1);
            Console.WriteLine("│{0}{1,2}    │", CARD_PATTERNS[currPattern], CARD_NUMBERS[currNumber]);
            Console.SetCursorPosition(cursor, 2);
            Console.WriteLine("│        │");
            Console.SetCursorPosition(cursor, 3);
            Console.WriteLine("│        │");
            Console.SetCursorPosition(cursor, 4);
            Console.WriteLine("│        │");
            Console.SetCursorPosition(cursor, 5);
            Console.WriteLine("│        │");
            Console.SetCursorPosition(cursor, 6);
            Console.WriteLine("│    {0}{1,2}│", CARD_PATTERNS[currPattern], CARD_NUMBERS[currNumber]);
            Console.SetCursorPosition(cursor, 7);
            Console.WriteLine("└────────┘");
            Console.SetCursorPosition(currPlayer * 35, 8+ currIndex);
            Console.WriteLine("{2}의 {1}번째 카드는 {0}입니다.",
                CARD_PATTERNS[currPattern] + CARD_NUMBERS[currNumber], currIndex + 1, TURN_PLAYER_NAME[currPlayer]);

        }

        static void Swap(int[] arr, int num1, int num2)
        {
            int tmp = arr[num1];
            arr[num1] = arr[num2];
            arr[num2] = tmp;
        }
        static int GetCard(int[] arr1, int[] arr2)
        {
            int card = -1;
            for (int top = 0; top < arr1.Length; top++)
            {
                if (arr1[top] != -1)
                {
                    card = arr1[top];
                    arr1[top] = -1;
                    break;
                }
            }

            for (int top = 0; top < arr2.Length; top++)
            {
                if (arr2[top] == -1)
                {
                    arr2[top] = card;
                    break;
                }
            }

            return card;
        }


    }

}
