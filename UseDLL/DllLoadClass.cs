using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace UseDLL
{
    class DllLoadClass
    {
        Assembly u;
        Type t;
        public Type deleType;
        public MethodInfo minfo;
        public object c;
        public DllLoadClass() { }
        public DllLoadClass(String strPath, String strType, String strDeleType, String strMethodName)
        {
            u = Assembly.LoadFile(strPath);
            t = u.GetType(strType);
            if (strDeleType != "")
                deleType = u.GetTypes().Single(a => a.Name == strDeleType);
            minfo = t.GetMethod(strMethodName);
            c = Activator.CreateInstance(t);
        }
    }
}
