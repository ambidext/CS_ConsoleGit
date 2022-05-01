using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Parsing
{
    class CSV_Parsing
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<List<string>>> dicService = ServiceParsing();
            foreach (var item in dicService)
            {
                StreamWriter sw = new StreamWriter("c:\\Temp\\" + item.Key + ".txt");
                Console.Write(item.Key + ":");
                foreach (var item2 in item.Value)
                {
                    Console.Write("--->");
                    bool bFirst = true;
                    foreach (var item3 in item2)
                    {
                        if (bFirst)
                        {
                            bFirst = false;
                        }
                        else
                        {
                            sw.Write(" ");
                        }
                        Console.WriteLine(item3);
                        sw.Write(item3);
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }
            Console.WriteLine("==================");
            AllItemParsing();

            ServiceReport(@"C:\Temp\service.csv");
        }

        static void ServiceReport(String inPath)
        {
            Dictionary<string, List<List<string>>> dicService = new Dictionary<string, List<List<string>>>();
            Dictionary<string, int> dicServiceCount = new Dictionary<string, int>();
            Dictionary<string, List<CItem>> dicItemCount = new Dictionary<string, List<CItem>>();
            Dictionary<string, Dictionary<CServiceList, int>> dicListCount = new Dictionary<string, Dictionary<CServiceList, int>>();

            int mode = 0; // 0:start ", 1:end "
            StreamReader sr = new StreamReader(inPath);
            String serviceKey = "";
            List<List<String>> valList = null;
            List<String> serviceList = null;
            while (true)
            {
                String line = sr.ReadLine();
                if (line == null || line == "")
                    break;

                if (mode == 0)
                {
                    // find serviceKey, serviceValue //////////////////
                    String[] words = line.Split(',');
                    serviceKey = words[0];
                    String serviceValue = words[1].Replace("\r", "").Replace("\n", ""); ;
                    if (serviceValue[0] == '"')
                    {
                        serviceValue = serviceValue.Substring(1);
                    }
                    if (serviceValue[0] == '[')
                    {
                        serviceValue = serviceValue.Substring(1);
                    }
                    if (serviceValue[serviceValue.Length - 1] == '"')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }
                    if (serviceValue[serviceValue.Length - 1] == ']')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }
                    ////////////////////////////////////////////////////

                    if (serviceList == null)
                    {
                        serviceList = new List<string>();
                        serviceList.Add(serviceValue);

                        /////////////////////////////////////////////////////
                        if (!dicItemCount.ContainsKey(serviceKey))
                        {
                            CItem objItem = new CItem();
                            objItem.item = serviceValue;
                            objItem.count = 1;
                            List<CItem> itemList = new List<CItem>();
                            itemList.Add(objItem);
                            dicItemCount.Add(serviceKey, itemList);
                        }
                        else
                        {
                            CItem objItem = new CItem();
                            objItem.item = serviceValue;
                            if (!dicItemCount[serviceKey].Contains(objItem))
                            {
                                objItem.count = 1;
                                dicItemCount[serviceKey].Add(objItem);
                            }
                            else
                            {
                                dicItemCount[serviceKey].Find(x => x.item == serviceValue).count++;
                            }
                        }
                        ///////////////////////////////////////////////////////
                    }

                    int count = line.Count(f => f == '"');
                    if (count == 0 || count == 2)
                    {
                        mode = 2;
                    }
                    else if (count == 1)
                    {
                        mode = 1;
                    }
                    else
                    {
                        // error 
                        Console.WriteLine("error");
                    }
                }
                else if (mode == 1)
                {
                    String serviceValue = line.Replace("\r", "").Replace("\n", "");
                    if (serviceValue[serviceValue.Length - 1] == '"')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }
                    if (serviceValue[serviceValue.Length - 1] == ']')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }

                    serviceList.Add(serviceValue);
                    /////////////////////////////////////////////////////
                    if (!dicItemCount.ContainsKey(serviceKey))
                    {
                        CItem objItem = new CItem();
                        objItem.item = serviceValue;
                        objItem.count = 1;
                        List<CItem> itemList = new List<CItem>();
                        itemList.Add(objItem);
                        dicItemCount.Add(serviceKey, itemList);
                    }
                    else
                    {
                        CItem objItem = new CItem();
                        objItem.item = serviceValue;
                        if (!dicItemCount[serviceKey].Contains(objItem))
                        {
                            objItem.count = 1;
                            dicItemCount[serviceKey].Add(objItem);
                        }
                        else
                        {
                            dicItemCount[serviceKey].Find(x => x.item == serviceValue).count++;
                        }
                    }
                    ///////////////////////////////////////////////////////                    

                    int count = line.Count(f => f == '"');
                    if (count == 1)
                    {
                        mode = 2;
                    }
                }

                if (mode == 2)
                {
                    if (!dicServiceCount.ContainsKey(serviceKey))
                    {
                        dicServiceCount.Add(serviceKey, 1);
                    }
                    else
                    {
                        dicServiceCount[serviceKey]++;
                    }

                    if (valList == null)
                    {
                        valList = new List<List<string>>();
                        valList.Add(serviceList);
                        if (!dicService.ContainsKey(serviceKey))
                        {
                            dicService.Add(serviceKey, valList);
                        }
                        else
                        {
                            dicService[serviceKey].Add(serviceList);
                        }

                        ////////////////////////////////////////
                        if (!dicListCount.ContainsKey(serviceKey))
                        {
                            Dictionary<CServiceList, int> sCount = new Dictionary<CServiceList, int>();
                            CServiceList cList = new CServiceList();
                            cList.serviceList = serviceList;
                            sCount.Add(cList, 1);
                            dicListCount.Add(serviceKey, sCount);
                        }
                        else
                        {
                            CServiceList cList = new CServiceList();
                            cList.serviceList = serviceList;
                            if (!dicListCount[serviceKey].ContainsKey(cList))
                            {
                                dicListCount[serviceKey].Add(cList, 1);
                            }
                            else
                            {
                                dicListCount[serviceKey][cList]++;
                            }
                        }
                        ////////////////////////////////////////
                    }

                    valList = null;
                    serviceList = null;
                    mode = 0;
                }
            }
            sr.Close();

            String outPath = Path.GetDirectoryName(inPath) + "\\" + Path.GetFileNameWithoutExtension(inPath) + "_Report.csv";
            StreamWriter sw = new StreamWriter(outPath);
            foreach (var service in dicServiceCount)
            {
                sw.Write(service.Key);
                sw.Write(",");
                sw.Write(service.Value);
                sw.Write(",");

                // item list /////////////////////////////////////////////////////////////////////////////////////////
                sw.Write("\"");
                bool bFirst = true;
                foreach (var sItem in dicItemCount[service.Key])
                {
                    if (bFirst)
                        bFirst = false;
                    else
                        sw.Write("\n");

                    String strItemList = string.Format("({0:D5}) {1}", sItem.count, sItem.item);
                    sw.Write(strItemList);
                }
                sw.Write("\",");
                //////////////////////////////////////////////////////////////////////////////////////////////////////

                // serviceList ///////////////////////////////////////////////////////////////////////////////////////
                sw.Write("\"");
                bFirst = true;
                foreach (var sList in dicListCount[serviceKey].OrderByDescending(x => x.Value))
                {
                    if (bFirst)
                        bFirst = false;
                    else
                        sw.Write("\n");

                    String strCnt = string.Format("({0:D5})", sList.Value);
                    sw.Write(strCnt);

                    bool bFirst2 = true;
                    StringBuilder sb = new StringBuilder();
                    foreach (var oneItem in sList.Key.serviceList)
                    {

                        if (bFirst2)
                        {
                            sb.Append(" [");
                            bFirst2 = false;
                        }
                        else
                        {
                            sb.Append("\n         ");
                        }
                        sb.Append(oneItem);
                    }
                    sb.Append("]");
                    sw.Write(sb);
                }
                sw.Write("\"");
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                sw.WriteLine();
            }

            sw.Close();
        }

        static Dictionary<string, List<List<string>>> ServiceParsing()
        {
            Dictionary<string, List<List<string>>> dicService = new Dictionary<string, List<List<string>>>();

            int mode = 0; // 0:start ", 1:end "
            StreamReader sr = new StreamReader(@"C:\Temp\service.csv");
            String serviceKey = "";
            List<List<String>> valList = null;
            List<String> serviceList = null;
            while (true)
            {
                String line = sr.ReadLine();
                if (line == null || line == "")
                    break;

                if (mode == 0)
                {
                    // find serviceKey, serviceValue //////////////////
                    String[] words = line.Split(',');
                    serviceKey = words[0];
                    String serviceValue = words[1].Replace("\r", "").Replace("\n", ""); ;
                    if (serviceValue[0] == '"')
                    {
                        serviceValue = serviceValue.Substring(1);
                    }
                    if (serviceValue[0] == '[')
                    {
                        serviceValue = serviceValue.Substring(1);
                    }
                    if (serviceValue[serviceValue.Length - 1] == '"')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }
                    if (serviceValue[serviceValue.Length - 1] == ']')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }
                    ////////////////////////////////////////////////////
                    
                    if (serviceList == null)
                    {
                        serviceList = new List<string>();
                        serviceList.Add(serviceValue);
                    }

                    int count = line.Count(f => f == '"');
                    if (count == 0 || count == 2)
                    {
                        mode = 2;
                    }
                    else if (count == 1)
                    {
                        mode = 1;
                    }
                    else
                    {
                        // error 
                        Console.WriteLine("error");
                    }
                }
                else if (mode == 1)
                {
                    String serviceValue = line.Replace("\r", "").Replace("\n", "");
                    if (serviceValue[serviceValue.Length-1] == '"')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }
                    if (serviceValue[serviceValue.Length - 1] == ']')
                    {
                        serviceValue = serviceValue.Substring(0, serviceValue.Length - 1);
                    }

                    serviceList.Add(serviceValue);                

                    int count = line.Count(f => f == '"');
                    if (count == 1)
                    {
                        mode = 2;
                    }
                }

                if (mode == 2)
                {
                    if (valList == null)
                    {
                        valList = new List<List<string>>();
                        valList.Add(serviceList);
                        if (!dicService.ContainsKey(serviceKey))
                        {
                            dicService.Add(serviceKey, valList);
                        }
                        else
                        {
                            dicService[serviceKey].Add(serviceList);
                        }
                    }

                    valList = null;
                    serviceList = null;
                    mode = 0;
                }
            }
            sr.Close();

            return dicService;
        }

        static List<String> itemParsing(String items)
        {
            List<String> itemList = new List<String>();

            bool bFirst = true;
            while (true)
            {
                int idx = items.IndexOf(" | ");
                if (idx == -1)
                {
                    String item = items;
                    if (bFirst)
                    {
                        if (item[0] == '[')
                            item = item.Substring(1);
                        bFirst = false;
                    }
                    if (item[item.Length - 1] == ']')
                        item = item.Substring(0, item.Length - 1);
                    itemList.Add(item);
                    break;
                }
                else
                {
                    String item = items.Substring(0, idx);
                    if (bFirst)
                    {
                        bFirst = false;
                        if (item[0] == '[')
                            item = item.Substring(1);
                    }

                    itemList.Add(item);

                    items = items.Substring(idx + 3);                    
                }
            }

            return itemList;
        }

        static void AllItemParsing()
        {
            StreamReader sr = new StreamReader(@"C:\Temp\item.csv");
            String serviceKey = "";
            while (true)
            {
                String line = sr.ReadLine();
                if (line == null || line == "")
                    break;
                String[] words = line.Split(',');
                serviceKey = words[0];
                String items = words[1];
                List<String> itemList = itemParsing(items);

                Console.WriteLine(serviceKey + ":");
                foreach(var item in itemList)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }
    }
    public class CServiceList
    {
        public List<String> serviceList;
        public int classId;

        public CServiceList()
        {
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

    public class CItem
    {
        public String item;
        public int count;
        public int classId;

        public CItem()
        {
            classId = 998;
        }
        public override bool Equals(object obj)
        {
            CItem o = obj as CItem;

            return o != null && item.Equals(o.item);
        }

        public override int GetHashCode()
        {
            return classId;
        }

    }
}
