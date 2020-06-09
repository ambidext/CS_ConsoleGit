using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordChange
{
    class Program
    {
        //두 개의 단어 begin, target과 단어의 집합 words가 있습니다.아래와 같은 규칙을 이용하여 begin에서 target으로 변환하는 가장 짧은 변환 과정을 찾으려고 합니다.

        //1. 한 번에 한 개의 알파벳만 바꿀 수 있습니다.
        //2. words에 있는 단어로만 변환할 수 있습니다.
        //예를 들어 begin이 hit, target가 cog, words가[hot, dot, dog, lot, log, cog] 라면 hit -> hot -> dot -> dog -> cog와 같이 4단계를 거쳐 변환할 수 있습니다.

        //두 개의 단어 begin, target과 단어의 집합 words가 매개변수로 주어질 때, 최소 몇 단계의 과정을 거쳐 begin을 target으로 변환할 수 있는지 return 하도록 solution 함수를 작성해주세요.

        //제한사항
        //각 단어는 알파벳 소문자로만 이루어져 있습니다.
        //각 단어의 길이는 3 이상 10 이하이며 모든 단어의 길이는 같습니다.
        //words에는 3개 이상 50개 이하의 단어가 있으며 중복되는 단어는 없습니다.
        //begin과 target은 같지 않습니다.
        //변환할 수 없는 경우에는 0를 return 합니다.
        //입출력 예
        //begin target  words   return
        //hit cog [hot, dot, dog, lot, log, cog]  4
        //hit cog [hot, dot, dog, lot, log]   0
        //입출력 예 설명
        //예제 #1
        //문제에 나온 예와 같습니다.

        //예제 #2
        //target인 cog는 words 안에 없기 때문에 변환할 수 없습니다.

        public class Solution
        {
            public string target;
            public int min;
            public bool Is1Change(string word1, string word2)
            {
                int change = 0;
                for (int i=0; i<word1.Length; i++)
                {
                    if (word1[i] != word2[i])
                        change++;
                }

                if (change == 1)
                    return true;
                else
                    return false;

            }
            public void changeWords(string curWord, List<string> wList, int cnt)
            {
                if (wList.Count == 0)
                {
                    return;
                }
                if (curWord == target)
                {
                    min = Math.Min(min, cnt);
                    return;
                }

                cnt++;
                for (int i=0; i<wList.Count; i++)
                {
                    if (!Is1Change(curWord, wList[i]))
                    {
                        continue;
                    }
                    else
                    {
                        List<string> newList = new List<string>(wList);
                        newList.RemoveAt(i);
                        changeWords(wList[i], newList, cnt);
                    }
                }
            }

            public int solution(string begin, string target, string[] words)
            {
                min = int.MaxValue;
                this.target = target;

                List<string> wordsList = new List<string>(words);
                string curWord = begin;
                int cnt = 0;
                changeWords(curWord, wordsList, cnt);

                if (min == int.MaxValue)
                    return 0;

                return min;
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();
            //string begin = "hit";
            //string target = "cog";
            //string[] words = { "hot", "dot", "dog", "lot", "log", "cog" };

            string begin = "aa";
            string target = "bb";
            string[] words = { "ac", "ab", "bb" };

            int min = sol.solution(begin, target, words);
            Console.WriteLine(min);
        }
    }
}
