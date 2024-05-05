using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllStatic;

namespace UseDLL
{
    class StaticClass : Class1
    {
        public override void MethodTest2(string input)
        {
            Console.WriteLine("MethodTest2 : " + input);
            //throw new NotImplementedException();
        }
    }
}
