using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllStatic
{
    public abstract class Class1
    {

        public string MethodTest(string input)
        {
            string strOut = "Output : " + input;
            MethodTest2("hahaha");
            return strOut;
        }

        public abstract void MethodTest2(string input);
    }
}
