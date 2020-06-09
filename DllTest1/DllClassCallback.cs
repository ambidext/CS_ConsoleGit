using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DllTest1
{
    class DllClassCallback
    {
        public delegate void CallbackDelegate(int res);

        public void AddMethod(int a, int b, CallbackDelegate callback)
        {
            int res = a + b;

            Thread.Sleep(1000);

            callback(res);
        }
    }
}
