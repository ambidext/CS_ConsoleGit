using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandConnect
{
    class Program
    {
        // https://programmers.co.kr/learn/courses/30/lessons/42861
        //섬 연결하기
        //문제 설명
        //n개의 섬 사이에 다리를 건설하는 비용(costs)이 주어질 때, 최소의 비용으로 모든 섬이 서로 통행 가능하도록 만들 때 필요한 최소 비용을 return 하도록 solution을 완성하세요.
        //다리를 여러 번 건너더라도, 도달할 수만 있으면 통행 가능하다고 봅니다.예를 들어 A 섬과 B 섬 사이에 다리가 있고, B 섬과 C 섬 사이에 다리가 있으면 A 섬과 C 섬은 서로 통행 가능합니다.
        //제한사항
        //섬의 개수 n은 1 이상 100 이하입니다.
        //costs의 길이는 ((n-1) * n) / 2이하입니다.
        //임의의 i에 대해, costs[i][0] 와 costs[i][1] 에는 다리가 연결되는 두 섬의 번호가 들어있고, costs[i] [2] 에는 이 두 섬을 연결하는 다리를 건설할 때 드는 비용입니다.
        //같은 연결은 두 번 주어지지 않습니다.또한 순서가 바뀌더라도 같은 연결로 봅니다. 즉 0과 1 사이를 연결하는 비용이 주어졌을 때, 1과 0의 비용이 주어지지 않습니다.
        //모든 섬 사이의 다리 건설 비용이 주어지지 않습니다. 이 경우, 두 섬 사이의 건설이 불가능한 것으로 봅니다.
        //연결할 수 없는 섬은 주어지지 않습니다.
        //입출력 예
        //n costs	return
        //4	[[0,1,1], [0,2,2], [1,2,5], [1,3,1], [2,3,8]]	4
        //입출력 예 설명
        //costs를 그림으로 표현하면 다음과 같으며, 이때 초록색 경로로 연결하는 것이 가장 적은 비용으로 모두를 통행할 수 있도록 만드는 방법입니다.
        // 알고리즘 : 크루스칼 알고리즘 (https://m.blog.naver.com/PostView.nhn?blogId=ndb796&logNo=221230994142&proxyReferer=https:%2F%2Fwww.google.com%2F)

        public class Solution
        {
            public class Edge : IComparable<Edge>
            {
                public int[] node;
                public int distance;
                public Edge(int a, int b, int distance)
                {
                    node = new int[2];
                    node[0] = a;
                    node[1] = b;
                    this.distance = distance;
                }
                public int CompareTo(Edge other)
                {
                    if (distance < other.distance)
                        return -1;
                    else if (distance > other.distance)
                        return 1;
                    else return 0;
                }
            }

            int getParent(int[] set, int x)
            {
                if (set[x] == x) return x;
                return set[x] = getParent(set, set[x]);
            }

            void unionParent(int[] set, int a, int b)
            {
                a = getParent(set, a);
                b = getParent(set, b);

                if (a < b) set[b] = a;
                else set[a] = b;
            }

            int find(int[] set, int a, int b)
            {
                a = getParent(set, a);
                b = getParent(set, b);
                if (a == b) return 1;
                else return 0;
            }

            public int solution(int n, int[,] costs)
            {
                int m = costs.GetLength(0);
                List<Edge> eList = new List<Edge>();
                for (int i=0; i<m; i++)
                {
                    eList.Add(new Edge(costs[i,0],costs[i,1], costs[i,2]));
                }
                eList.Sort();

                int[] set = new int[n];
                for (int i = 0; i < n; i++)
                    set[i] = i;

                int sum = 0;
                for (int i = 0; i < eList.Count; i++)
                {
                    if (find(set, eList[i].node[0], eList[i].node[1]) == 0)
                    {
                        sum += eList[i].distance;
                        unionParent(set, eList[i].node[0], eList[i].node[1]);
                    }
                }

                return sum; 
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();

            int n = 4;
            int[,] costs = { { 0, 1, 1 },{ 0, 2, 2 },{ 1, 2, 5 },{ 1, 3, 1 },{ 2, 3, 8 } };
            int ret = sol.solution(n, costs);
            Console.WriteLine(ret);


            //List<Edge> eList = new List<Edge>();
            //int n = 7;
            //int m = 11;
            //eList.Add(new Edge(1, 7, 12));
            //eList.Add(new Edge(1, 4, 28));
            //eList.Add(new Edge(1, 2, 67));
            //eList.Add(new Edge(1, 5, 17));
            //eList.Add(new Edge(2, 4, 24));
            //eList.Add(new Edge(2, 5, 62));
            //eList.Add(new Edge(3, 5, 20));
            //eList.Add(new Edge(3, 6, 37));
            //eList.Add(new Edge(4, 7, 13));
            //eList.Add(new Edge(5, 6, 45));
            //eList.Add(new Edge(5, 7, 73));

            //int[] set = new int[n];
            //for (int i = 0; i < n; i++)
            //    set[i] = i;

            //int sum = 0;
            //for (int i=0; i<eList.Count; i++)
            //{
            //    if (find(set, eList[i].node[0] - 1, eList[i].node[1] - 1) == 0)
            //    {
            //        sum += eList[i].distance;
            //        unionParent(set, eList[i].node[0] - 1, eList[i].node[1] - 1);
            //    }
            //}
            //Console.WriteLine(sum);
        }
    }
}
