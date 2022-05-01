using Cotur.DataMining.Association;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriPrj
{
    class Program
    {
        static void Main(string[] args)
        {
            //Apriori_One();
            Apriori_Market_Basket_One();
        }
        public static void Apriori_One()
        {
            var transaction1 = new List<int>() { 0, 1, 2, 3 };
            var transaction2 = new List<int>() { 0, 2, 3, 4, 5, 6 };
            var transaction3 = new List<int>() { 1, 4, 5, 7 };
            var transaction4 = new List<int>() { 0, 1, 4, 5 };
            var transaction5 = new List<int>() { 0, 1, 2, 5, 7 };
            var transaction6 = new List<int>() { 0, 1, 3, 4, 5 };
            var transaction7 = new List<int>() { 0, 1, 5, 7 };

            var transactions = new List<List<int>>()
            {
                transaction1,
                transaction2,
                transaction3,
                transaction4,
                transaction5,
                transaction6,
                transaction7
            };

            var maxCol = transactions.Max(x => x.Max());

            var dataFields = new DataFields(maxCol, transactions);

            var myApriori = new Apriori(dataFields);
            var minimumSupport = 0.0001f; // %40 Minimum Support

            myApriori.CalculateCNodes(minimumSupport);

            //myApriori.Rules.ShouldNotBeNull();
            //myApriori.Rules.Count.ShouldNotBe(0);
            //myApriori.Rules.Count(x => x.Confidence >= .7f).ShouldBe(12);

            int table = 1;
            foreach(var levels in myApriori.EachLevelOfNodes)
            {
                Console.WriteLine(" Table{0} ", table++);
                foreach(var node in levels)
                {
                    Console.WriteLine(node.ToDetailedString(myApriori.Data));
                }
            }

            Console.WriteLine("Top rules ordered by Confidence (Up to 10)");
            foreach (var associationRule in myApriori.Rules.OrderByDescending(x => x.Confidence).Take(100))
            {
                Console.WriteLine(associationRule.ToDetailedString(dataFields));
            }
        }

        public static void Apriori_Market_Basket_One()
        {
            var dataFields = DataFields.ReadFromFile(@".\small-market-basket.csv");

            var myApriori = new Apriori(dataFields);

            //myApriori.CalculateCNodes(.4f);
            myApriori.CalculateCNodes(0.000001f);

            //myApriori.Rules.ShouldNotBeNull();
            //myApriori.Rules.Count.ShouldNotBe(0);
            //myApriori.Rules.Count(x => x.Confidence >= .7f).ShouldBe(12);

            int table = 1;
            foreach (var levels in myApriori.EachLevelOfNodes)
            {
                Console.WriteLine(" Table{0} ", table++);
                foreach (var node in levels)
                {
                    Console.WriteLine(node.ToDetailedString(myApriori.Data));                    
                }
            }
            Console.WriteLine();

            Console.WriteLine("Top rules ordered by Confidence (Up to 10)");
            foreach (var associationRule in myApriori.Rules.OrderByDescending(x => x.Confidence))
            {
                Console.WriteLine(associationRule.ToDetailedString(dataFields));
            }


        }

        public static void Apriori_Big()
        {
            var csvFilePath = @".\big-text-1.csv";

            var dataFields = DataFields.ReadFromFile(csvFilePath);

            var myApriori = new Apriori(dataFields);

            myApriori.CalculateCNodes(0.003f);
            //myApriori.Rules.Count.ShouldBeGreaterThan(0);

            Console.WriteLine("Top rules ordered by Confidence (Up to 10)");
            foreach (var associationRule in myApriori.Rules.OrderByDescending(x => x.Confidence).Take(10))
            {
                Console.WriteLine(associationRule.ToDetailedString(dataFields));
            }
        }
    }
}
