using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeBigNumber
{
    class Program
    {
        // https://programmers.co.kr/learn/courses/30/lessons/42883
        //어떤 숫자에서 k개의 수를 제거했을 때 얻을 수 있는 가장 큰 숫자를 구하려 합니다.
        //예를 들어, 숫자 1924에서 수 두 개를 제거하면[19, 12, 14, 92, 94, 24] 를 만들 수 있습니다.이 중 가장 큰 숫자는 94 입니다.
        //문자열 형식으로 숫자 number와 제거할 수의 개수 k가 solution 함수의 매개변수로 주어집니다. number에서 k 개의 수를 제거했을 때 만들 수 있는 수 중 가장 큰 숫자를 문자열 형태로 return 하도록 solution 함수를 완성하세요.
        //제한 조건
        //number는 1자리 이상, 1,000,000자리 이하인 숫자입니다.
        //k는 1 이상 number의 자릿수 미만인 자연수입니다.
        //입출력 예
        //number k	return
        //1924	2	94
        //1231234	3	3234
        //4177252841	4	775841

        public class Solution
        {
            public string solution2(string number, int k)
            {
                StringBuilder a = new StringBuilder(number);
                int i = 0;
                int j = 0;
                int idx;
                int r;

                for (i = 0; i < k; i++)
                {
                    r = a.Length; 
                    idx = r - 1;
                    for (j = 0; j < r - 1; j++)
                    {
                        if (a[j] < a[j + 1])
                        {
                            idx = j;
                            break;
                        }
                    }

                    a.Remove(idx, 1); 
                }

                return a.ToString();
            }

            public string solution(string number, int k)
            {
                char[] chNum = number.ToArray();
                List<char> numList = new List<char>(chNum);
                int len = numList.Count;
                int idx = 0;
                int rem = k;
                while (true)
                {
                    if (idx + 1 >= numList.Count)
                    {
                        for (int r = 0; r < rem; r++)
                            numList.RemoveAt(numList.Count-1);
                        break;
                    }
                    if (numList[idx] >= numList[idx+1])
                    {
                        idx++;
                    }
                    else
                    {
                        for (int r=idx; r>=0; r--)
                        {
                            if (numList[idx+1] > numList[r])
                            {
                                numList.RemoveAt(r);
                                idx--;
                                if (idx < 0)
                                    idx = 0;
                                rem--;
                                if (rem == 0)
                                    break;
                            }
                        }
                    }
                    if (idx >= numList.Count)
                        break;
                    if (rem == 0)
                        break;
                }

                StringBuilder sb = new StringBuilder();
                foreach(var item in numList)
                {
                    sb.Append(item);
                }
                string answer = sb.ToString();
                return answer;
            }
        }
        static void Main(string[] args)
        {
            Solution sol = new Solution();
            string num = "1924";
            int k = 2;
            string ret = sol.solution(num, k);
            Console.WriteLine(ret);

            string ret2 = sol.solution2(num, k);
            Console.WriteLine(ret2);

        }
    }
}
