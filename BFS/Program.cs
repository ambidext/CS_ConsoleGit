using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    class Program
    {
        static int[] c;
        static List<int>[] a;
        static Queue<int> q2;
        /// ////////////////////////////////////////////////////
        ///                  1
        ///                /   \
        ///               2 --- 3
        ///              / \   /  \
        ///             4 - 5 6  - 7
        /// 
        /// ////////////////////////////////////////////////////
        static void Main(string[] args)
        {
            c = new int[8];
            a = new List<int>[8];
            for (int i = 0; i < 8; i++)
            {
                a[i] = new List<int>();
            }

            a[1].Add(2);
            a[2].Add(1);

            a[1].Add(3);
            a[3].Add(1);

            a[2].Add(3);
            a[3].Add(2);

            a[2].Add(4);
            a[2].Add(5);

            a[3].Add(6);
            a[6].Add(3);

            a[3].Add(7);
            a[7].Add(3);

            a[4].Add(5);
            a[5].Add(4);

            a[6].Add(7);
            a[7].Add(6);

            q2 = new Queue<int>();
            q2.Enqueue(1);
            bfs(1);
        }

        static void bfs(int start)
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            c[start] = 1;
            while (q.Count > 0)
            {
                int x = q.Dequeue();
                Console.WriteLine(x);
                for (int i=0; i<a[x].Count; i++)
                {
                    int y = a[x][i];
                    if (c[y] != 1)
                    {
                        q.Enqueue(y);
                        c[y] = 1;
                    }
                }
            }
        }

        static void bfs2(int x)
        {
            if (q2.Count == 0)
                return;
            if (c[x] == 1)
                return;
            int n = q2.Dequeue();
            c[x] = 1;
            Console.WriteLine(x);
            for (int i=0; i<a[n].Count; i++)
            {
                q2.Enqueue(a[n][i]);
                bfs2(a[n][i]);
            }
        }
    }
}
