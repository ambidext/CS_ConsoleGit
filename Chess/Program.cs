using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        public class Solution
        {
            public int solution(int n, int[,] blocks)
            {
                int answer = -1;

                return answer;
            }
        }

        static void Main(string[] args)
        {
            int n = 3;
            int[,] blocks = new int[,] { {1,2},{3,2 },{2,3} };
            Solution sol = new Solution();
            sol.solution(n, blocks);
        }
    }
}
