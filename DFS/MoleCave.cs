using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 두더지 굴
// https://blog.naver.com/ambidext/221258275510
namespace DFS
{
    class MoleCave
    {
        public int[,] c;
        public int count;
        public int n;
        int[,] area;
        public void solution()
        {
            n = 7;
            area = new int [,]{ { 0, 1, 1, 0, 1, 0, 0 },
                                { 0, 1, 1, 0, 1, 0, 1 },
                                { 1, 1, 1, 0, 1, 0, 1 },
                                { 0, 0, 0, 0, 1, 1, 1 },
                                { 0, 1, 0, 0, 0, 0, 0 },
                                { 0, 1, 1, 1, 1, 1, 0 },
                                { 0, 1, 1, 1, 0, 0, 0 }};
            Console.WriteLine("\n==================== MOLE SOLUTION =======================");
            // 
            List<int> caves = new List<int>();

            c = new int[n, n];
            for (int y=0; y<n; y++)
            {
                for (int x = 0; x<n; x++)
                {
                    if (c[y, x] == 1)
                        continue;
                    count = 0;
                    c[y, x] = 1;
                    if (area[y, x] != 0)                       
                    {
                        count++;
                        checkAround(y, x);
                    }
                    if (count != 0)
                        caves.Add(count);
                }
            }
            
            Console.WriteLine(caves.Count);
            foreach(var item in caves.OrderByDescending(x => x))
            {
                Console.WriteLine(item);
            }
        }

        private bool checkLimit(int y, int x)
        {
            if (y < 0 || x < 0)
                return false;
            if (y >= n || x >= n)
                return false;

            return true;
        }
        private void checkAround(int y, int x)
        {
            int [] dx = { 0, -1, 1, 0 };
            int [] dy = { -1, 0, 0, 1 };

            for (int k=0; k<4; k++)
            {
                int ny = y + dy[k];
                int nx = x + dx[k];

                if (!checkLimit(ny, nx))
                    continue;
                if (c[ny, nx] == 1)
                    continue;
                c[ny, nx] = 1;
                if (area[ny,nx] > 0)
                {
                    count++;
                    checkAround(ny, nx);
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
