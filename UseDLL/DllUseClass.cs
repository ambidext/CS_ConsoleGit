using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UseDLL
{
    class DllUseClass
    {
        public int res;
        private int inputA, inputB;
        DllLoadClass dlc;
        Delegate callbackMethod;
        public void Callback(int res)
        {
            this.res = res;
            Console.WriteLine(inputA + "+" + inputB + "="+res);
        }

        public void ExecuteCallbackMethod(int a, int b)
        {
            inputA = a;
            inputB = b;
            dlc = new DllLoadClass(@"C:\VSProjects\CS_ConsoleGit\DllTest1\bin\Debug\DllTest1.dll",
                "DllTest1.DllClassCallback", "CallbackDelegate", "AddMethod");

            MethodInfo callBackMethodInfo = this.GetType().GetMethod("Callback", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(int) }, null);
            callbackMethod = Delegate.CreateDelegate(dlc.deleType, this, callBackMethodInfo);

            dlc.minfo.Invoke(dlc.c, new object[] { inputA, inputB, callbackMethod });
        }

        public int ExecuteMethod(int a, int b)
        {
            dlc = new DllLoadClass(@"C:\VSProjects\CS_ConsoleGit\DllTest1\bin\Debug\DllTest1.dll",
                "DllTest1.DllClass", "", "AddMethod");

            res = (int)dlc.minfo.Invoke(dlc.c, new object[] { a, b });

            return res;
        }
    }
}
