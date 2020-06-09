using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baseball
{
    class Program
    {
    //숫자 야구 게임이란 2명이 서로가 생각한 숫자를 맞추는 게임입니다.게임해보기        
    //각자 서로 다른 1~9까지 3자리 임의의 숫자를 정한 뒤 서로에게 3자리의 숫자를 불러서 결과를 확인합니다.그리고 그 결과를 토대로 상대가 정한 숫자를 예상한 뒤 맞힙니다.
       
    //* 숫자는 맞지만, 위치가 틀렸을 때는 볼
    //* 숫자와 위치가 모두 맞을 때는 스트라이크
    //* 숫자와 위치가 모두 틀렸을 때는 아웃
    //예를 들어, 아래의 경우가 있으면

    //A : 123
    //B : 1스트라이크 1볼.
    //A : 356
    //B : 1스트라이크 0볼.
    //A : 327
    //B : 2스트라이크 0볼.
    //A : 489
    //B : 0스트라이크 1볼.
    //이때 가능한 답은 324와 328 두 가지입니다.


    //질문한 세 자리의 수, 스트라이크의 수, 볼의 수를 담은 2차원 배열 baseball이 매개변수로 주어질 때, 가능한 답의 개수를 return 하도록 solution 함수를 작성해주세요.


    //제한사항
    //질문의 수는 1 이상 100 이하의 자연수입니다.
    //baseball의 각 행은[세 자리의 수, 스트라이크의 수, 볼의 수] 를 담고 있습니다.

        public class Solution
        {
            string inputStr;
            int count;
            int[,] inputNum;
            public int solution(int[,] baseball)
            {
                inputNum = baseball;

                int answer = 0;
                inputStr = "123456789";

                LinkedList<int> list = new LinkedList<int>();
                int n = 9;
                int r = 3;
                int[] check = new int[n];
                int[] arr = new int[n];
                for (int i = 1; i <= n; i++) arr[i - 1] = i;

                permutation2(list, check, n, r);

                answer = count;
                return answer;
            }

            bool CheckSB(string str1, string str2, int s, int b)
            {
                int strike = 0;
                int ball = 0;

                for (int i = 0; i < 3; i++)
                {
                    if (str1[i] == str2[i])
                        strike++;

                    for (int k = 0; k < 3; k++)
                    {
                        if (i != k)
                        {
                            if (str1[i] == str2[k])
                                ball++;
                        }
                    }
                }

                if (strike == s && ball == b)
                    return true;
                else
                    return false;
            }

            bool CheckAll(int[,] inputNum, String str)
            {
                for (int i = 0; i < inputNum.GetLength(0); i++)
                {
                    if (!CheckSB(inputNum[i, 0] + "", str, inputNum[i, 1], inputNum[i, 2]))
                        return false;
                }

                return true;
            }

            void permutation2(LinkedList<int> list, int[] check, int n, int r)
            {
                if (list.Count() == r)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (int i in list)
                    {
                        //Console.Write(i + " ");
                        //Console.Write(inputStr[i]);
                        sb.Append(inputStr[i]);
                    }

                    if (CheckAll(inputNum, sb.ToString()))
                    {
                        count++;
                    }
                    //Console.WriteLine();
                    return;
                }
                for (int i = 0; i < n; i++)
                {
                    if (check[i] == 0)
                    {
                        check[i] = 1;
                        list.AddLast(i);
                        permutation2(list, check, n, r);
                        list.RemoveLast();
                        check[i] = 0;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();

            int[,] baseball = new int[,] { { 123, 1, 1 }, { 356, 1, 0 }, { 327, 2, 0 }, { 489, 0, 1 } };
            int ret = sol.solution(baseball);

            Console.WriteLine(ret);
        }
    }
}
