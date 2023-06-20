using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619.Card
{
    public partial class MainSystem
    {

        //로얄 스트레이트 플러시
        //패턴 정렬된 덱을 받아야한다.
        public bool IsRoyal(Card[] deck_Pattern)
        {
            //플래그 배열 : 선택된 족보 카드의 인덱스 저장
            int[] flag;
            int flagCount = 0;

            //조건 체크용(다음 숫자와의 차이가 1일 경우)
            int count = 0;

            //모든 조건이 맞을 경우 마지막 숫자(12='A'일 경우에 성공)
            int last = 0;

            //알고리즘 진행 방식
            //5 -> 1
            //7 -> 3
            //0 1 2 3 4 5 6
            //1 2 3 4 5
            //  1 2 3 4 5
            //    1 2 3 4 5
            //   [   ]
            //     [   ]
            //       [   ]
            //         [   ]

            for (int startIndex = 0; startIndex < deck_Pattern.Length - 4; startIndex++)
            {
                //변수 초기화
                flagCount = 0;
                flag = new int[5];
                last = 0;
                count = 0;

                //처음 값을 저장한다.
                int beforeNum = deck_Pattern[startIndex].GetCardNumberNum();
                flag[flagCount++] = startIndex;

                //before <-> curr
                for (int i = startIndex + 1; i <= startIndex + 4; i++)
                {
                    if (i >= deck_Pattern.Length) { return false; }

                    int currNum = deck_Pattern[i].GetCardNumberNum();

                    //이전숫자랑 비교
                    if (currNum - beforeNum == 1)
                    {
                        //조건 맞을 경우
                        count += 1;

                        flag[flagCount++] = i;
                        beforeNum = deck_Pattern[i].GetCardNumberNum();

                        //조건맞을때마다 값 넣음
                        last = beforeNum;
                    }
                    else
                    {
                        break;
                    }

                }

                //4번다 맞을 경우
                if (count == 4)
                {
                    if (last == 12)
                    {
                        for (int j = 0; j < flag.Length; j++)
                        {
                            deck_Pattern[flag[j]].isSelect = true;
                        }
                        return true;
                    }
                }

            }
            return false;
        }


        //스트레이트 플러시
        public bool IsStraightFlush2(Card[] deck_Pattern)
        {
            int[] flag;
            int flagCount = 0;
            
            //플러시 조건용 bool변수
            bool isSameColor = true;
            
            //sequanceEqual용 상수 배열.
            //카드의 번호가 들어간다.
            List<int> back = new List<int>();
            back.Add(0);
            back.Add(1);
            back.Add(2);
            back.Add(3);

            //플러시 조건이 맞을 카드 패턴
            int cardPattern = -1;

            //마지막 카드(백스트레이트(A,2,3,4,5) 체크)
            int cardNumber = -1;

            //5장씩 검사
            for (int startIndex = 0; startIndex < deck_Pattern.Length - 4; startIndex++)
            {
                flagCount = 0;
                flag = new int[5];
                isSameColor = true;
                
                //sequanceEqual용 배열
                List<int> tmp = new List<int>();
                
                for (int i = startIndex; i < startIndex + 4; i++)
                {
                    //플러시
                    if (isSameColor)
                    {
                        if (deck_Pattern[i].GetCardPatternNum() != deck_Pattern[i + 1].GetCardPatternNum())
                        {
                            isSameColor = false;
                        }
                        else
                        {
                            //조건 맞을 경우 해당 카드를 선택한다.
                            flag[flagCount++] = i;
                            tmp.Add(deck_Pattern[i].GetCardNumberNum());
                            //카드 패턴이 맞으면 저장한다.
                            cardPattern = deck_Pattern[i].GetCardPatternNum();
                        }
                    }
                }
                //tmp 마지막 값 입력 
                tmp.Add(deck_Pattern[startIndex + 4].GetCardNumberNum());

                //해당 문양의 마지막 카드의 번호를 찾는다.
                for (int i = startIndex; i < deck_Pattern.Length - 1; i++)
                {
                    if (deck_Pattern[i].GetCardPatternNum() == cardPattern)
                    {
                        cardNumber = deck_Pattern[i].GetCardNumberNum();
                    }
                    else
                    {
                        break;
                    }
                }

                //현재 시작부터 5번째 값의 인덱스를 구하는 반복문
                int count = 0;
                for (int i = startIndex; i < deck_Pattern.Length - 1; i++)
                {
                    count++;
                    flag[flagCount] = i + 1;
                    if (count == 4)
                    {
                        break;
                    }                   
                }

                //플러시 일경우
                if (isSameColor)
                {
                    //연속적인지 검사
                    bool isContinue = true;
                    for (int i = 1; i < 5; i++)
                    {
                        if (isContinue)
                        {
                            isContinue = (tmp[i] - tmp[i - 1] == 1);
                        }
                    }

                    //연속적일 경우
                    if (isContinue)
                    {
                        //return;
                        for (int j = 0; j < flag.Length; j++)
                        {
                            deck_Pattern[flag[j]].isSelect = true;
                        }
                        return true;
                    }

                    //백스트레이트일 경우
                    tmp.RemoveAt(4);
                    if (tmp.SequenceEqual(back) && cardNumber == 12)
                    {
                        //return;
                        for (int j = 0; j < flag.Length; j++)
                        {
                            deck_Pattern[flag[j]].isSelect = true;
                        }
                        return true;
                    }
                }
            }
            return false;
        }



        //포카드
        public bool IsFourCard(Card[] deck_Number)
        {
            int[] flag;
            int flagCount = 0;

            //4칸을 검사해서 모두 같으면 true 반환
            for (int startIndex = 0; startIndex < deck_Number.Length - 3; startIndex++)
            {
                flagCount = 0;
                flag = new int[4];

                int sameCount = 0;

                for (int i = startIndex; i < startIndex + 2; i++)
                {
                    if (deck_Number[i].GetCardNumberNum() == deck_Number[i + 1].GetCardNumberNum())
                    {
                        flag[flagCount++] = i;
                        sameCount += 1;
                        continue;
                    }

                }
                if (sameCount == 3)
                {
                    flag[flag.Length - 1] = startIndex + 2;

                    for (int j = 0; j < flag.Length; j++)
                    {
                        deck_Number[flag[j]].isSelect = true;
                    }
                    return true;
                }
            }
            return false;
        }

        //풀하우스
        public bool IsFullHouse(Card[] deck_Number)
        {
            int number = 0;

            int[] flag;
            int flagCount = 0;

            //먼저 3칸인 카드를 찾는다.
            for (int startIndex = 0; startIndex < deck_Number.Length - 2; startIndex++)
            {
                flagCount = 0;
                flag = new int[5];

                number = 0;

                int sameCount = 0;

                for (int i = startIndex; i < startIndex + 2; i++)
                {
                    if (deck_Number[i].GetCardNumberNum() == deck_Number[i + 1].GetCardNumberNum())
                    {
                        sameCount += 1;
                        flag[flagCount++] = i;
                        if (sameCount == 2)
                        {
                            number = deck_Number[i].GetCardNumberNum();
                            flag[flagCount++] = i + 1;
                        }
                        continue;
                    }
                }

                //3칸 찾을 경우
                if (sameCount == 2)
                {
                    //2칸 바로 찾으면 종료
                    for (int i = 0; i < deck_Number.Length - 1; i++)
                    {
                        if (number != deck_Number[i].GetCardNumberNum() && 
                            deck_Number[i].GetCardNumberNum() == deck_Number[i + 1].GetCardNumberNum())
                        {
                            flag[flagCount++] = i;
                            flag[flagCount++] = i + 1;

                            for (int j = 0; j < flag.Length; j++)
                            {
                                deck_Number[flag[j]].isSelect = true;
                            }

                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //플러시
        public bool IsFlush(Card[] deck_Pattern)
        {
            int[] flag;
            int flagCount = 0;
            
            bool isSameColor = true;

            //모든 문양이 같을경우 true
            for (int startIndex = 0; startIndex < deck_Pattern.Length - 4; startIndex++)
            {
                flagCount = 0;
                flag = new int[5];
                isSameColor = true;
                for (int i = startIndex; i < startIndex + 4; i++)
                {
                    if (isSameColor)
                    {
                        if (deck_Pattern[i].GetCardPatternNum() != deck_Pattern[i + 1].GetCardPatternNum())
                        {
                            isSameColor = false;
                        }
                        else
                        {
                            flag[flagCount++] = i;
                        }
                    }
                }
                if (isSameColor)
                {
                    flag[flag.Length - 1] = startIndex + 4;

                    for (int j = 0; j < flag.Length; j++)
                    {
                        deck_Pattern[flag[j]].isSelect = true;
                    }
                    return true;
                }
            }
            return false;
        }


        //스트레이트
        public bool IsStraight(Card[] deck_Number)
        {
            int[] flag;
            int flagCount = 0;

            int count = 0;
            int beforeNumber = -1;

            List<int> back = new List<int>();
            back.Add(0);
            back.Add(1);
            back.Add(2);
            back.Add(3);


            for (int startIndex = 0; startIndex < deck_Number.Length - 4; startIndex++)
            {
                flagCount = 0;
                flag = new int[5];
                count = 0;
                List<int> tmp = new List<int>();
                beforeNumber = deck_Number[startIndex].GetCardNumberNum();
                tmp.Add(beforeNumber);

                flag[flagCount++] = startIndex;

                for (int i = startIndex + 1; i <= startIndex + 4; i++)
                {

                    if (i >= deck_Number.Length) { return false; }

                    if (beforeNumber == deck_Number[i].GetCardNumberNum())
                    {
                        startIndex += 1;
                        beforeNumber = deck_Number[i].GetCardNumberNum();
                        continue;
                    }

                    if (deck_Number[i].GetCardNumberNum() - beforeNumber == 1)
                    {
                        count += 1;
                        tmp.Add(deck_Number[i].GetCardNumberNum());
                        beforeNumber = deck_Number[i].GetCardNumberNum();
                        flag[flagCount++] = i;
                    }
                }

                if (count == 4)
                {
                    bool isContinue = true;
                    for (int i = 1; i < 5; i++)
                    {
                        if (isContinue)
                        {
                            isContinue = (tmp[i] - tmp[i - 1] == 1);
                        }
                    }

                    if (isContinue)
                    {
                        for (int j = 0; j < flag.Length; j++)
                        {
                            deck_Number[flag[j]].isSelect = true;
                        }
                        Console.Write("!!");
                        return true;
                    }
                }
                else if (count == 3)
                {
                    if (tmp.SequenceEqual(back) && deck_Number[deck_Number.Length - 1].GetCardNumberNum() == 12)
                    {
                        flag[flagCount++] = deck_Number.Length - 1;
                        for (int j = 0; j < flag.Length; j++)
                        {
                            deck_Number[flag[j]].isSelect = true;
                        }
                        return true;
                    }
                }


            }
            return false;
        }


        //트리플
        // 0 1 2 3 4
        // 0 1 2 3 4 5 6
        //[     ]
        //  [     ]
        //    [     ]
        //      [     ]
        //        [     ]
        public bool IsTriple(Card[] deck_Number)
        {
            int[] flag;
            int flagCount = 0;
            for (int startIndex = 0; startIndex < deck_Number.Length - 2; startIndex++)
            {
                flagCount = 0;
                flag = new int[3];
                int sameCount = 0;

                for (int i = startIndex; i < startIndex + 2; i++)
                {
                    if (deck_Number[i].GetCardNumberNum() == deck_Number[i + 1].GetCardNumberNum())
                    {
                        sameCount += 1;
                        flag[flagCount++] = i;
                        continue;
                    }


                }

                flag[flag.Length - 1] = startIndex + 2;
                if (sameCount == 2)
                {
                    for (int j = 0; j < flag.Length; j++)
                    {
                        deck_Number[flag[j]].isSelect = true;
                    }
                    return true;
                }
            }
            return false;
        }


        //투페어
        public bool IsTwoPair(Card[] deck_Number)
        {
            int[] flag;
            flag = new int[4];
            int flagCount = 0;
            int sameCount = 0;

            for (int startIndex = 0; startIndex < deck_Number.Length - 1; startIndex++)
            {
                if (deck_Number[startIndex].GetCardNumberNum() == deck_Number[startIndex + 1].GetCardNumberNum())
                {
                    flag[flagCount++] = startIndex;
                    flag[flagCount++] = startIndex + 1;

                    sameCount += 1;
                    startIndex += 1;
                }

                if (sameCount == 2)
                {
                    for (int j = 0; j < flag.Length; j++)
                    {
                        deck_Number[flag[j]].isSelect = true;
                    }
                    return true;
                }
            }
            return false;
        }


        //원페어
        public bool IsOnePair(Card[] deck_Number)
        {
            int[] flag;
            int flagCount = 0;
            int sameCount = 0;

            for (int startIndex = 0; startIndex < deck_Number.Length - 1; startIndex++)
            {

                flagCount = 0;
                flag = new int[2];
                if (deck_Number[startIndex].GetCardNumberNum() == deck_Number[startIndex + 1].GetCardNumberNum())
                {
                    flag[flagCount++] = startIndex;
                    flag[flagCount++] = startIndex + 1;
                    sameCount += 1;
                    startIndex += 1;
                }

                if (sameCount == 1)
                {
                    for (int j = 0; j < flag.Length; j++)
                    {
                        deck_Number[flag[j]].isSelect = true;
                    }
                    return true;
                }
            }
            return false;
        }




    }
}
