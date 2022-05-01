using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedDictionaryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new SortedDictionary<int, string>();

            dic.Add(2, "hi");
            dic.Add(5, "hello");
            dic.Add(3, "good");
            dic.Add(1, "haha");
            dic.Add(4, "four");

            foreach (var item in dic)
                Console.WriteLine(item.Key + "," + item.Value);

            Console.WriteLine(dic[dic.First().Key]);
        }
    }
}
