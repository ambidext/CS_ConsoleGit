using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listMain = new List<string>() { "A", "B", "C" };
            Console.WriteLine("Main List");
            foreach (var item in listMain)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Sub List");
            List<string> listSub = new List<string>() { "1", "2", "3" };
            foreach (var item in listMain)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Add SubList to Main List");
            listMain.AddRange(listSub);
            foreach (var item in listMain)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}
