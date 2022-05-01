using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryKeyClass
{
    class Program
    {
        static void Main(string[] args)
        {
            CServiceList sList1 = new CServiceList();
            CServiceList sList2 = new CServiceList();
            CServiceList sList3 = new CServiceList();

            sList1.serviceList.Add("service1");
            sList1.serviceList.Add("service1_1");


            sList2.serviceList.Add("service2");

            sList3.serviceList.Add("service1");
            sList3.serviceList.Add("service1_1");

            Dictionary<CServiceList, int> dicServiceList = new Dictionary<CServiceList, int>();
            if (!dicServiceList.ContainsKey(sList1))
            {
                dicServiceList.Add(sList1, 1);
            }
            else
            {
                dicServiceList[sList1]++;
            }

            if (!dicServiceList.ContainsKey(sList2))
            {
                dicServiceList.Add(sList2, 1);
            }
            else
            {
                dicServiceList[sList2]++;
            }

            if (!dicServiceList.ContainsKey(sList3))
            {
                dicServiceList.Add(sList3, 1);
            }
            else
            {
                dicServiceList[sList3]++;
            }

            Console.WriteLine();
        }
    }

    public class CServiceList
    {
        public List<String> serviceList;
        public int classId;

        public CServiceList()
        {
            serviceList = new List<String>();
            classId = 999; 
        }

        public override bool Equals(object obj)
        {
            CServiceList o = obj as CServiceList;

            return o != null && serviceList.SequenceEqual(o.serviceList);
        }

        public override int GetHashCode()
        {
            return classId; 
        }
    }
}
