using System;
using System.Collections.Generic;
using System.Linq;

namespace permutation
{
    class Program
    {
        static void swap(ref char a, ref char b)
        {
            char temp;

            temp = a;
            a = b;
            b = temp;
        }
        static void permutation(int n, char[] szStr)
        {
            int i;
            if (n == szStr.Length)
            {
                Console.WriteLine(szStr);
            }

            //for (i = n; i < szStr.Length; i++)
            for (i = 0; i < szStr.Length; i++)
            {
                swap(ref szStr[n], ref szStr[i]);
                permutation(n + 1, szStr);
                swap(ref szStr[n], ref szStr[i]);
            }
        }

        static string inputStr;
        static void permutation2(LinkedList<int> list, int[] check, int n, int r)
        {
            if (list.Count() == r)
            {
                foreach (int i in list)
                {
                    //Console.Write(i + " ");
                    Console.Write(inputStr[i]);
                }
                Console.WriteLine(); 
                return;
            }
            for (int i = 0; i < n; i++)
            {//**중복 순열과 다른 점
                if (check[i] == 0)
                {//자기자신을 못뽑게 해야지 중복이 안됨(이미 뽑은 것은 뽑지 않도록 체크)
                    check[i] = 1;//자기자신을 못뽑게 해야지 중복이 안됨
                    list.AddLast(i);
                    permutation2(list, check, n, r);
                    list.RemoveLast();//해당 넘버를 다시 제거 (즉,뽑지 않고 다음 번호 뽑기위함)
                    check[i] = 0;
                }
            }
        }

        // 중복순열
        static void rePermutation(LinkedList<int> list, int n, int r)
        {
            if (list.Count() == r)
            {
                foreach (int i in list)
                {
                    //Console.Write(i + " ");
                    Console.Write(inputStr[i]);
                }

                Console.WriteLine();
                return;
            }
            for (int i = 0; i < n; i++)
            {
                list.AddLast(i);
                rePermutation(list, n, r);
                list.RemoveLast();
            }

        }
        static void Main(string[] args)
        {
            Console.Write("Input String : ");
            inputStr = Console.ReadLine();
            Console.Write("Input n r : ");
            string line = Console.ReadLine();

            int n = int.Parse(line.Split(' ')[0]);
            int r = int.Parse(line.Split(' ')[1]);
            int [] arr = new int[n];
            for (int i = 1; i <= n; i++) arr[i - 1] = i;

            LinkedList<int> list = new LinkedList<int>();
            int [] check = new int[n];
            Console.WriteLine("****순  열****");
            permutation2(list, check, n, r);
            list.Clear();

            Console.WriteLine("****중복순열****");
            rePermutation(list, n, r);
            list.Clear();
        }

    }
}
