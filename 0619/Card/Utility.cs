using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619.Card
{
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
