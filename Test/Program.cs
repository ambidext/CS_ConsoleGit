using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        private static int[][] loadData()
        {

            //////////////////////////////////
            // 제공 데이터 1
            /////////////////////////////////
            //int[][] icebergMap = new int[][] {
            //            new int[] { 0, 0, 0, 0 },
            //            new int[] { 0, 0, 4, 0 },
            //            new int[] { 0, 4, 0, 4 },
            //            new int[] { 0, 4, 0, 4 },
            //            new int[] { 0, 4, 4, 4 },
            //            new int[] { 0, 0, 0, 0 }
            //};

            //////////////////////////////////
            // 제공 데이터 2
            /////////////////////////////////
            int[][] icebergMap = new int[][] {
               new int[] { 0, 0, 0, 0, 4, 4 },
               new int[] { 0, 0, 0, 4, 0, 4 },
               new int[] { 0, 4, 4, 0, 4, 4 },
               new int[] { 0, 4, 0, 0, 4, 0 },
               new int[] { 4, 4, 4, 4, 4, 0 },
               new int[] { 0, 4, 0, 0, 0, 0 }
            };

            return icebergMap;
        }

        static void printMap(int[][] data)
        {
            foreach (int[] line in data)
            {
                foreach (var item in line)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[][] icebergMap = loadData();
            printMap(icebergMap);

            Iceberg iceberg = new Iceberg();

            int[][] innerMap = iceberg.ConvertInnerWater(icebergMap);
            printMap(innerMap);

            int year = iceberg.GetCollapseYear(icebergMap);
            Console.WriteLine(year);
        }
    }
}
