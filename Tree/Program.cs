using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTree
{
    class Program
    {
        static int solution(int[] grade, int max_diff)
        {
            int answer = 0;
            Array.Sort(grade);
            for (int i=0; i<grade.Length; i++)
            {
                Console.WriteLine(grade[i]);
            }
            return answer;
        }
        static void Main(string[] args)
        {
            int[] g = { 5, 3, 1 };
            solution(g, 1);
        }
    }
}
