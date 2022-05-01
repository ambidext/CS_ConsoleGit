using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("FileCut.exe [filename] [cut file count]");
            }
            string orgFilename = args[0];
            int cnt = int.Parse(args[1]);

            FileStream f_in = new FileStream(orgFilename, FileMode.Open, FileAccess.Read);
            int size = (int)f_in.Length / cnt;
            byte[] buffer = new byte[size];
            if ((int) f_in.Length % cnt > 0)
            {
                cnt++;
            }
            for (int i = 0; i < cnt; i++)
            {
                string fname = orgFilename + "." + string.Format("{0:D2}", i);
                FileStream f_out = new FileStream(fname, FileMode.Create, FileAccess.Write);
                int nReadLen = f_in.Read(buffer, 0, size);
                f_out.Write(buffer, 0, nReadLen);
                f_out.Close();
            }
            f_in.Close();

        }
    }
}
