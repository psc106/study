using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619.Card
{
    public class Print
    {
        int Zone_Com = 4;
        int Zone_Player = 15;

        public void PrintComDeck(Card[] deck)
        {
            for (int i = 0; i < deck.Length; i++) {
                int x = 2 + i * 6;
                int y = Zone_Com;
                if (deck[i].isSelect)
                {
                    y -= 2;
                }

                //10칸(x2)
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("┌───────────┐");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│{0,3}       │",deck[i].GetCardString());
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│       {0,3}│", deck[i].GetCardString());
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("└───────────┘");
            }
        }


        public void PrintPlayerDeck(Card[] deck)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                int x = 8 + i * 6;
                int y = Zone_Player;

                if (deck[i].isSelect)
                {
                    if (i == 0)
                    {
                        Console.SetCursorPosition(x, y++);
                        Console.WriteLine("             ");
                        Console.SetCursorPosition(x, y++);
                        Console.WriteLine("             ");
                    }
                    else if ((i >0 && deck[i - 1].isSelect))
                    {
                        if (i >= 2 && !deck[i - 2].isSelect)
                        {
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("┐             ");
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("│             ");
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("             ");
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("             ");
                        }
                    }
                    else
                    {
                        y += 2;
                    }
                }

                //10칸(x2)
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("┌───────────┐          ");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│{0,3}       │          ", deck[i].GetCardString());
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│           │");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("│       {0,3}│          ", deck[i].GetCardString());
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("└───────────┘          ");

                if (!deck[i].isSelect)
                {


                    if (i == 0)
                    {
                        Console.SetCursorPosition(x, y++);
                        Console.WriteLine("             ");
                        Console.SetCursorPosition(x, y++);
                        Console.WriteLine("             ");
                    }
                    else if ((i > 0 && !deck[i-1].isSelect))
                    {

                        if (i >= 2 && deck[i - 2].isSelect)
                        {
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("│             ");
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("┘             ");
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("             ");
                            Console.SetCursorPosition(x, y++);
                            Console.WriteLine("             ");
                        }
                    }
                    else if (i==deck.Length- 1 && !deck[i - 1].isSelect)
                    {
                        Console.SetCursorPosition(x, y++);
                        Console.WriteLine("             ");
                        Console.SetCursorPosition(x, y++);
                        Console.WriteLine("             ");
                    }

                }

                
            }
        }

        public void printCursor(int position)
        {
            int x = 10 + position * 6;
            int y = Zone_Player-1;

            for (int i = 0; i <= 5; i++)
            {
                Console.SetCursorPosition(10 + i * 6, y);
                Console.WriteLine("  ");
            }
            
            Console.SetCursorPosition(x, y);
            Console.WriteLine("▼");

        }
    }
}
