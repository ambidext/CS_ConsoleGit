using System;
using System.IO;

namespace Base64Enc_File
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
            string encodedStr = Convert.ToBase64String(File.ReadAllBytes(args[0]));
            File.WriteAllText(args[1], encodedStr);
        }
    }
}
