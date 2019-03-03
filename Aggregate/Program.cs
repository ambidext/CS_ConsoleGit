using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] inputData = LoadData();

            int numberOfSubArrays = GetNumberOfSubArrays(inputData.GetLength(0));

            int maximumValue = GetMaximumValue(inputData);

            Console.WriteLine(numberOfSubArrays);
            Console.WriteLine(maximumValue);
        }

        static int GetNumberOfSubArrays(int n)
        {
            int total = 0;
            for (int i = 1; i <= n; i++)
                total += (i * i);

            return total;
        }

        static int[,] LoadData()
        {
            //int[,] inputData =
            //{
            //    {-1, 0, 7, 9 },
            //    {-6, 2, -3, 5 },
            //    {3, -6, 0, -5 },
            //    {7, 8, -7, 2 }
            //};

            int[,] inputData =
{
                {1, -3, 0, 2, 5 },
                {-3, 0, 8, -3, 7 },
                {9, -1, -2 , 6, 0 },
                {-2, -5, 9, 7, 6 },
                {3, 2, 4, 7, -5 }
            };

            return inputData;
        }

        static int GetMaximumValue(int [,] input)
        {
            int n = input.GetLength(0);
            int max = 0;
            for (int i=1; i<=n; i++)
            {
                for (int h=0; h<n; h++)
                {
                    for (int w=0; w<n; w++)
                    {
                        int total = GetTotal(input, w, h, i);
                        max = Math.Max(max, total);
                    }
                }                
            }
     
            return max;
        }

        static int GetTotal(int[,] input, int w, int h, int i)
        {
            int total = 0;
            int n = input.GetLength(0);

            if (w + i > n)
                return 0;

            if (h + i > n)
                return 0;

            for (int y = h; y<h+i; y++)
            {
                for (int x=w; x<w+i; x++)
                {
                    total += input[y, x];
                }
            }

            return total;
        }
    }
}
