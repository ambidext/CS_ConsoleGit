using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Test
{
    class Program
    {
        static void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        static void PrintNumberWithStatus()
        {
            Console.WriteLine("Starting...");
            Console.WriteLine("Current - " + Thread.CurrentThread.ThreadState.ToString());
            for (int i=0; i<10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }

        static bool bEnd = false;
        static List<int> twos = new List<int> { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536 };
        //static int[] arr = { 2, 2, 4 };
        static bool printPicked(int [] arr, List<int> picked)
        {
            int nTotal = 0;
            int nCnt = 0;
            int nPrev = 0;
            int nSameNumCnt = 1;
            foreach (var item in picked)
            {
                if (nCnt == 0)
                {
                    nTotal = arr[item];
                    nCnt++;
                    nPrev = arr[item];
                    continue;
                }

                if (nPrev != arr[item])
                {
                    if (nTotal != arr[item])
                        return false;
                    if (!twos.Contains(nSameNumCnt))
                        return false;
                    nTotal += arr[item];
                    nSameNumCnt = 1;
                }
                else
                {
                    nTotal += arr[item];
                    nSameNumCnt++;
                }
            }
            if (nSameNumCnt != 1 && !twos.Contains(nSameNumCnt))
                return false;

            bEnd = true;
            return true;
        }

        static bool pick(int [] arr, int n, List<int> picked, int toPick)
        {
            if (toPick == 0)
            {
                return printPicked(arr, picked);
            }

            int smallest = (picked.Count == 0 ? 0 : picked.Last() + 1);

            for (int next = smallest; next < n; ++next)
            {
                picked.Add(next);
                pick(arr, n, picked, toPick - 1);
                if (bEnd)
                    return true;
                picked.Remove(picked.Last());
            }

            return false;
        }

        static bool Combination2(int [] arr, int n, int r)
        {
            List<int> picked = new List<int>();
            return pick(arr, n, picked, r);
        }

        static bool CheckNum(int [] nums)
        {
            int nTotal = nums[0];
            int nSameNumCnt = 1;
            for (int i=1; i<nums.Length; i++)
            {
                if (nums[i - 1] != nums[i])
                {
                    if (nTotal != nums[i])
                        return false;
                    if (!twos.Contains(nSameNumCnt))
                        return false;
                    nTotal += nums[i];
                    nSameNumCnt = 1;
                }
                else
                {
                    nTotal += nums[i];
                    nSameNumCnt++;
                }
                    
            }
            if (!twos.Contains(nSameNumCnt))
                return false;

            return true;
        }

        static int N, M;
        static int sy, sx, gy, gx; // start좌표, goal좌표

        // 상하좌우 탐색용
        static int dx = 1;

        static bool bSafe(int x)
        {
            if (x < 0)
                return false;
            if (x >= M)
                return false;

            return true;
        }

        static bool Verify(int nTotal, int[] data, idx)
        {
            for (int i=0; i<idx; i++)
            {
                if (CheckTwo(data[i]))
            }
        }

        static void Main(string[] args)
        {
            int[] weights = { 1, 3, 3, 3, 3, 3, 3, 3, 5, 12, 15, 15, 15};
            int[] data = new int[weights.Length];
            int dataCnt = 0;
            Array.Sort(weights);

            int i;
            int nSameNum = 0;
            for (i=0; i<weights.Length-1; i++)
            {
                if (i == 0)
                {
                    if ((weights[i] % 2 == 1) && weights[i] != weights[i + 1])
                    {
                        nSameNum = 0;
                    }
                    else
                    {
                        data[dataCnt++] = weights[i];
                        nSameNum++;
                    }
                }
                else
                {
                    if ((weights[i] % 2 == 1) && (weights[i] != weights[i + 1] && weights[i] != weights[i - 1]))
                    {
                        if (nSameNum %2 == 1)
                        {
                            dataCnt--;
                        }
                        nSameNum = 0;
                    }
                    else
                    {
                        data[dataCnt++] = weights[i];
                        nSameNum++;
                    }
                }
            }

            if (nSameNum % 2 == 1)
            {
                //dataCnt--;
            }
            else
            {
                data[dataCnt++] = weights[i];
            }
            Console.WriteLine(data);

            int nMax = 0;
            int nTotal = 0;
            for (i=0; i<data.Length; i++)
            {
                nTotal += data[i];
                if (Verify(nTotal, data, i))
                    nMax = i;
            }

            /*
            int[] visit = new int[data.Length];

            Queue<int> queue = new Queue<int>();
            sx = 0;
            gx = weights.Length - 1;
            queue.Enqueue(sx);
            visit[sx] = 1;
            visit[gx] = -1;

            while (queue.Count != 0)
            {
                int cur = queue.Dequeue();
                if (cur == gx)
                {
                    break;
                }

                int nextX = cur + dx;

                // 방문 전이고 
                if (bSafe(nextX) && visit[nextX] <= 0)
                {
                    queue.Enqueue(nextX);
                    visit[nextX] = visit[cur] + 1; // 방문 지점까지 경로 길이 
                }
            }
            */

            /*
            Console.WriteLine("Starting program...");
            Thread t = new Thread(PrintNumberWithStatus);
            Thread t2 = new Thread(DoNothing);
            Console.WriteLine(t.ThreadState.ToString());
            t2.Start();
            t.Start();
            for (int i=0; i<30; i++)
            {
                Console.WriteLine("main - " + t.ThreadState.ToString());
            }
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            Console.WriteLine("A thread has been aborted");
            Console.WriteLine(t.ThreadState.ToString());
            Console.WriteLine(t2.ThreadState.ToString());
            */
        }
    }
}
