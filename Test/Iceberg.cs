using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Iceberg
    {
        public bool IsInnerWater(int[][] map, int y, int x)
        {
            int hLen = map.Length;
            int wLen = map[0].Length;

            if (map[y][x] != 0)
                return false;

            if (x == 0 || y == 0 || x == wLen - 1 || y == hLen - 1)
                return false;

            int cnt = 0;
            for (int i=x-1; i>=0; i--)
            {
                cnt += map[y][i];
            }
            if (cnt == 0)
                return false;

            cnt = 0;
            for (int i = x + 1; i < wLen; i++)
            {
                cnt += map[y][i];
            }
            if (cnt == 0)
                return false;

            cnt = 0;
            for (int i = y - 1; i >= 0; i--)
            {
                cnt += map[i][x];
            }
            if (cnt == 0)
                return false;

            cnt = 0;
            for (int i = y + 1; i < hLen; i++)
            {
                cnt += map[i][x];
            }
            if (cnt == 0)
                return false;

            return true;
        }

        public int[][] ConvertInnerWater(int[][] icebergMap)
        {
            int hLen = icebergMap.Length;
            int wLen = icebergMap[0].Length;

            int[][] ret = new int[hLen][];
            for (int h = 0; h<hLen; h++ )
            {
                ret[h] = new int[wLen];
                for (int w = 0; w < wLen; w++)
                {
                    if (IsInnerWater(icebergMap, h, w))
                    {
                        ret[h][w] = 9;
                    }
                    else
                    {
                        ret[h][w] = icebergMap[h][w];
                    }
                }
            }

            return ret;
        }

        public bool IsEmpty(int[][] map)
        {
            int hLen = map.Length;
            int wLen = map[0].Length;

            for (int h=0; h<hLen; h++)
            {
                for (int w=0; w<wLen; w++)
                {
                    if (map[h][w] > 0)
                        return false;
                }
            }

            return true;
        }

        public int CheckSurround(int[][] map, int y, int x)
        {
            int cnt = 0;
            int hLen = map.Length;
            int wLen = map[0].Length;

            if (x > 0)
            {
                if (map[y][x - 1] == 0)
                    cnt++;
            }

            if (x < wLen-1)
            {
                if (map[y][x + 1] == 0)
                    cnt++;
            }

            if (y>0)
            {
                if (map[y - 1][x] == 0)
                    cnt++;
            }

            if (y<hLen-1)
            {
                if (map[y + 1][x] == 0)
                    cnt++;
            }

            return cnt;
        }

        public int GetCollapseYear(int[][] icebergMap)
        {
            int year = 0;

            int hLen = icebergMap.Length;
            int wLen = icebergMap[0].Length;

            while (true)
            {
                if (IsEmpty(icebergMap))
                    break;
                int[][] newMap = ConvertInnerWater(icebergMap);

                for (int h = 0; h < hLen; h++)
                {
                    for (int w = 0; w < wLen; w++)
                    {
                        if (newMap[h][w] >= 1 && newMap[h][w] <= 4)
                        {
                            int cnt = CheckSurround(newMap, h, w);
                            icebergMap[h][w] = newMap[h][w] - cnt;
                        }
                    }
                }

                year++;
            }
            return year;
        }
    }
}
