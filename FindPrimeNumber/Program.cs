using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace FindPrimeNumber
{
    class Program
    {
        //한자리 숫자가 적힌 종이 조각이 흩어져있습니다.흩어진 종이 조각을 붙여 소수를 몇 개 만들 수 있는지 알아내려 합니다.

        //각 종이 조각에 적힌 숫자가 적힌 문자열 numbers가 주어졌을 때, 종이 조각으로 만들 수 있는 소수가 몇 개인지 return 하도록 solution 함수를 완성해주세요.


        //제한사항
        //numbers는 길이 1 이상 7 이하인 문자열입니다.
        //numbers는 0~9까지 숫자만으로 이루어져 있습니다.
        //013은 0, 1, 3 숫자가 적힌 종이 조각이 흩어져있다는 의미입니다.
        //입출력 예
        //numbers return
        //17	3
        //011	2
        //입출력 예 설명
        //예제 #1
        //[1, 7]으로는 소수 [7, 17, 71]를 만들 수 있습니다.

        //예제 #2
        //[0, 1, 1] 으로는 소수[11, 101] 를 만들 수 있습니다.

        //11과 011은 같은 숫자로 취급합니다.
        class Solution
        {
            char[] arr;
            List<int> numList;
            public int solution(string numbers)
            {
                int answer = 0;
                arr = numbers.ToArray();
                numList = new List<int>();
                for (int i = 1; i <= arr.Length; i++)
                    Combination2(arr.Length, i);

                foreach (var item in numList)
                {
                    if (IsPrime(item))
                        answer++;

                    //Console.WriteLine(item);
                }
                return answer;
            }

            void printPicked(List<int> picked)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in picked)
                {
                    //Console.Write(arr[item] + " ");
                    sb.Append(arr[item]);
                }

                string str = sb.ToString();
                permutation(0, str.ToArray());
            }

            void pick(int n, List<int> picked, int toPick)
            {
                if (toPick == 0)
                {
                    printPicked(picked); return;
                }

                int smallest = (picked.Count == 0 ? 0 : picked.Last() + 1);

                for (int next = smallest; next < n; ++next)
                {
                    picked.Add(next);
                    pick(n, picked, toPick - 1);
                    picked.Remove(picked.Last());
                }
            }

            void Combination2(int n, int r)
            {
                List<int> picked = new List<int>();
                pick(n, picked, r);
            }

            bool IsPrime(int n)
            {
                if (n <= 1) return false;
                if (n == 2) return true;

                // 2를 제외한 모든 짝수는 소수가 아님.
                if (n % 2 == 0) return false;

                for (int i = 3; i * i <= n; i += 2)
                {
                    if (n % i == 0)
                        return false;
                }
                return true;
            }

            void swap(ref char a, ref char b)
            {
                char temp;

                temp = a;
                a = b;
                b = temp;
            }
            void permutation(int n, char[] szStr)
            {
                int i;
                if (n == szStr.Length)
                {
                    //Console.WriteLine(szStr);
                    int num = int.Parse(new string(szStr));
                    //Console.WriteLine(num);
                    if (!numList.Contains(num))
                        numList.Add(num);
                }

                for (i = n; i < szStr.Length; i++)
                {
                    swap(ref szStr[n], ref szStr[i]);
                    permutation(n + 1, szStr);
                    swap(ref szStr[n], ref szStr[i]);
                }
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();

            int ret = sol.solution("17");

            Console.WriteLine(ret);
        }
    }
}
