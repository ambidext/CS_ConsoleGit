using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    class Program
    {
        static int[] c;
        static List<int>[] a;

        /// ////////////////////////////////////////////////////
        ///                  1
        ///                /   \
        ///               2 --- 3
        ///              / \   /  \
        ///             4 - 5 6 -  7
        /// 
        /// ////////////////////////////////////////////////////
        

        static void Main(string[] args)
        {
            c = new int[8];
            a = new List<int>[8];
            for (int i=0; i<8; i++)
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

            dfs(1);

            // 두더지 굴
            MoleCave mole = new MoleCave();
            mole.solution();
        }

        static void dfs(int x)
        {
            if (c[x] == 1)
                return;

            c[x] = 1;
            Console.Write(x + " ");
            for (int i = 0; i < a[x].Count; i++)
            {
                int y = a[x][i];
                dfs(y);
            }
        }
    }
}
