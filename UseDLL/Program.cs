using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using DllStatic;

namespace UseDLL
{
    class Program
    {
        static void CallDLLTest()
        {
            Assembly u = Assembly.LoadFile(@"C:\VSProjects\CS_ConsoleGit\DllTest1\bin\Debug\DllTest1.dll");
            //Assembly u = Assembly.LoadFile(@".\DllTest1.dll");

            //Module[] modules = u.GetModules();
            Type t = u.GetType("DllTest1.DllClass");
            var c = Activator.CreateInstance(t);
            MethodInfo minfo = t.GetMethod("AddMethod");

            //object[] parameter = { 1, 2 };

            int res = (int)minfo.Invoke(c, new object[] { 1, 2 });

            Console.WriteLine(res);
        }

        static void CallDll1()
        {
            DllUseClass duc = new DllUseClass();
            int a = 2, b = 3;
            int res = duc.ExecuteMethod(a, b);
            Console.WriteLine(a + "+" + b + "=" + res);
        }

        static void CallStaticDll()
        {
            StaticClass staticClass = new StaticClass();
            staticClass.MethodTest("input1");
        }

        static void Main(string[] args)
        {
            // static dll
            CallStaticDll();

            // Dynamic loading
            CallDll1();

            for (int i=0; i<10; i++)
            {
                Thread th = new Thread(DoWork);
                th.Start(i); 
            }
            Console.ReadLine();
        }

        static void DoWork(object param)
        {
            int i = (int)param;
            Console.WriteLine(i);
            DllUseClass duc = new DllUseClass();
            duc.ExecuteCallbackMethod(i, i + 1);
        }
    }
}
