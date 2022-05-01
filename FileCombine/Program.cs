using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCombine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("FileCombine.exe [filename without number] [last number]");
                return;
            }

            string orgFname = args[0];
            int lastNum = int.Parse(args[1]);
            FileStream f_out = new FileStream(orgFname, FileMode.Create, FileAccess.Write);
            for (int i = 0; i <= lastNum; i++)
            {
                string fname = orgFname + i;
                byte[] totalBytes = File.ReadAllBytes(fname);
                f_out.Write(totalBytes, 0, totalBytes.Length);
            }
            f_out.Close();
        }
    }
}
