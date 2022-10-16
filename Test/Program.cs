using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace JsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // git test comment merge
            Console.WriteLine("Branch Master Test");
            Console.WriteLine("Master branch");
            if (args.Length < 2)
            {
                Console.WriteLine("Base64Enc.exe [src file] [des file]");
                return;
            }
            string encodedStr = Convert.ToBase64String(File.ReadAllBytes(args[0]));
            File.WriteAllText(args[1], encodedStr);

        }
        static void Base64Sample(string str)
        {
            //string str = "This is a Base64 test.";
            byte[] byteStr = System.Text.Encoding.UTF8.GetBytes(str);
            string encodedStr;
            byte[] decodedBytes;

            Console.WriteLine(str);

            encodedStr = Convert.ToBase64String(byteStr);
            Console.WriteLine(encodedStr);

            decodedBytes = Convert.FromBase64String(encodedStr);
            Console.WriteLine(Encoding.Default.GetString(decodedBytes));
        }
    }
}
