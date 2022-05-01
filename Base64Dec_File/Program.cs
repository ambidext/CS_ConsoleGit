using System;
using System.IO;

namespace Base64Dec_File
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Base64Enc.exe [src file] [des file]");
                return;
            }

            string str = File.ReadAllText(args[0]);
            byte[] bytes = Convert.FromBase64String(str);
            File.WriteAllBytes(args[1], bytes);
        }
    }
}
