using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boxingRank
{
    class Program
    {
        public class Solution
        {
            public int[,] floyd(int[,] d, int V)
            {
                for (int k = 0; k < V; k++)
                    for (int i = 0; i < V; i++)
                        for (int j = 0; j < V; j++)
                        {
                            if (d[i, k] == int.MaxValue || d[k, j] == int.MaxValue) continue;
                            if (d[i, j] > d[i, k] + d[k, j])
                            {
                                if (i == j)
                                {
                                    d[i, j] = 0;
                                }
                                else
                                {
                                    //if (d[i, k] + d[k, j] > 0)
                                    //    d[i, j] = 1;
                                    //else if (d[i, k] + d[k, j] < 0)
                                    //    d[i, j] = -1;
                                    //else
                                    //    d[i, j] = 0;
                                    d[i, j] = d[i, k] + d[k, j];
                                }
                            }
                        }
                return d;
            }

            public int solution(int n, int[,] edge)
            {
                int[,] dd = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                            dd[i, j] = 0;
                        else
                            dd[i, j] = int.MaxValue;
                    }
                }

                int hLen = edge.GetLength(0);
                for (int h = 0; h < hLen; h++)
                {
                    int node1 = edge[h, 0] - 1;
                    int node2 = edge[h, 1] - 1;
                    dd[node1, node2] = 2;
                    dd[node2, node1] = 1;
                }

                dd = floyd(dd, n);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("{0,15}", dd[i, j]);
                    }
                    Console.WriteLine();
                }

                return 0;
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();
            int n = 5;
            int[,] edge = { { 4, 3 }, { 4, 2 }, { 3, 2 }, { 1, 2 }, { 2, 5 } };
            int ret = sol.solution(n, edge);
            Console.WriteLine(ret);
        }
    }
}
