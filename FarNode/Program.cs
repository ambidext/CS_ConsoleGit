using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarNode
{
    class Program
    {
        // https://programmers.co.kr/learn/courses/30/lessons/49189
        //가장 먼 노드
        //문제 설명
        //n개의 노드가 있는 그래프가 있습니다.각 노드는 1부터 n까지 번호가 적혀있습니다. 1번 노드에서 가장 멀리 떨어진 노드의 갯수를 구하려고 합니다.가장 멀리 떨어진 노드란 최단경로로 이동했을 때 간선의 개수가 가장 많은 노드들을 의미합니다.
        //노드의 개수 n, 간선에 대한 정보가 담긴 2차원 배열 vertex가 매개변수로 주어질 때, 1번 노드로부터 가장 멀리 떨어진 노드가 몇 개인지를 return 하도록 solution 함수를 작성해주세요.
        //제한사항
        //노드의 개수 n은 2 이상 20,000 이하입니다.
        //간선은 양방향이며 총 1개 이상 50,000개 이하의 간선이 있습니다.
        //vertex 배열 각 행 [a, b]는 a번 노드와 b번 노드 사이에 간선이 있다는 의미입니다.
        //입출력 예
        //n vertex	return
        //6	[[3, 6], [4, 3], [3, 2], [1, 3], [1, 2], [2, 4], [5, 2]]	3
        //입출력 예 설명
        //예제의 그래프를 표현하면 아래 그림과 같고, 1번 노드에서 가장 멀리 떨어진 노드는 4,5,6번 노드입니다.

        // Floyd-Warshall로 풀었는데 시간초과... (Solution_old)
        // dijkstra로 풀어서 성공.
        public class Solution
        {
            class Graph
            {
                private int n;          //노드들의 수
                private int[,] maps;    //노드들간의 가중치 저장할 변수

                public Graph(int n)
                {
                    this.n = n;
                    maps = new int[n + 1, n + 1];
                }
                public void input(int i, int j, int w)
                {
                    maps[i, j] = w;
                    maps[j,i] = w;    // 양방향으로 하려면 주석 제거
                }

                public int dijkstra(int v)
                {
                    int[] distance = new int[n + 1];   // 최단 거리를 저장할 변수
                    bool[] check = new bool[n + 1];     //해당 노드를 방문했는지 체크할 변수

                    //distance값 초기화.
                    for (int i = 1; i < n + 1; i++)
                    {
                        distance[i] = int.MaxValue;
                    }

                    //시작노드값 초기화.
                    distance[v] = 0;
                    check[v] = true;

                    //연결노드 distance갱신
                    for (int i = 1; i < n + 1; i++)
                    {
                        if (!check[i] && maps[v, i] != 0)
                        {
                            distance[i] = maps[v, i];
                        }
                    }


                    for (int a = 0; a < n - 1; a++)
                    {
                        //원래는 모든 노드가 true될때까지 인데
                        //노드가 n개 있을 때 다익스트라를 위해서 반복수는 n-1번이면 된다.
                        //원하지 않으면 각각의 노드가 모두 true인지 확인하는 식으로 구현해도 된다.
                        int min = int.MaxValue;
                        int min_index = -1;

                        //최소값 찾기
                        for (int i = 1; i < n + 1; i++)
                        {
                            if (!check[i] && distance[i] != int.MaxValue)
                            {
                                if (distance[i] < min)
                                {
                                    min = distance[i];
                                    min_index = i;
                                }
                            }
                        }

                        if (min_index != -1)
                        {
                            check[min_index] = true;
                            for (int i = 1; i < n + 1; i++)
                            {
                                if (!check[i] && maps[min_index, i] != 0)
                                {
                                    if (distance[i] > distance[min_index] + maps[min_index, i])
                                    {
                                        distance[i] = distance[min_index] + maps[min_index, i];
                                    }
                                }
                            }
                        }
                    }

                    //결과값 출력
                    //for (int i = 1; i < n + 1; i++)
                    //{
                    //    if (distance[i] == int.MaxValue)
                    //        Console.WriteLine("INF");
                    //    else
                    //        Console.WriteLine(distance[i]);
                    //}

                    int max = 0;
                    int maxCnt = 1;
                    for (int i = 1; i < n+1; i++)
                    {
                        if (distance[i] == int.MaxValue)
                            continue;
                        else
                        {
                            if (distance[i] > max)
                            {
                                max = distance[i];
                                maxCnt = 1;
                            }
                            else if (distance[i] == max)
                            {
                                maxCnt++;
                            }
                        }
                    }
                    return maxCnt;
                }
            }
            public int solution(int n, int[,] edge)
            {
                Graph g = new Graph(n);

                int hLen = edge.GetLength(0);
                for (int h = 0; h < hLen; h++)
                {
                    int node1 = edge[h, 0];
                    int node2 = edge[h, 1];
                    g.input(node1, node2, 1);
                }

                return g.dijkstra(1);
            }
        }
        public class Solution_old
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
                for (int h=0; h<hLen; h++)
                {
                    int node1 = edge[h, 0]-1;
                    int node2 = edge[h, 1]-1;
                    dd[node1, node2] = 1;
                    dd[node2, node1] = 1;
                }

                dd = floyd(dd, n);

                int max = 0;
                int maxCnt = 1;
                for (int i = 0; i < n; i++)
                {
                    if (dd[0,i] > max)
                    {
                        max = dd[0, i];
                        maxCnt = 1;
                    }
                    else if (dd[0,i] == max)
                    {
                        maxCnt++;
                    }
                    //Console.WriteLine(dd[0, i]);
                }

                return maxCnt;
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();
            int n = 6;
            int[,] edge = { { 3, 6}, {4, 3}, {3, 2}, {1, 3}, {1, 2}, {2, 4}, {5, 2} };
            int ret = sol.solution(n, edge);
            Console.WriteLine(ret);
        }
    }
}
