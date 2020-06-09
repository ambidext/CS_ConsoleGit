using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Test
{
    class Program
    {
        class Solution
        {
            public void solution1(String str)
            {
                String res = "";
                char prev = (char)0;
                for (int i=0; i<str.Length; i++)
                {
                    if (str[i] >= '2' && str[i]<='9')
                    {
                        for (int k=0; k<(str[i] - '0')-1; k++)
                        {
                            res += prev;
                        }
                    }
                    else
                    {
                        res += str[i];
                        prev = str[i];
                    }
                }

                Console.WriteLine(res);
            }

            public void solution2(List<String> inputData)
            {
                List<String> dicList = new List<String>();
                int fileCnt = 0;
                for (int i=0; i<inputData.Count; i++)
                {
                    String str = inputData[i];
                    int idx = str.LastIndexOf('/');
                    if (idx != str.Length-1)
                    {
                        fileCnt++;
                        inputData[i] = str.Substring(0, idx + 1);
                    }

                    String[] words = inputData[i].Split('/');
                    String strDic = "";
                    foreach(var item in words)
                    {
                        if (item == "")
                            break;
                        strDic += (item + "/");
                        if (!dicList.Contains(strDic))
                            dicList.Add(strDic);
                    }
                }
                Console.WriteLine("File Count : " + fileCnt);
                Console.WriteLine("Dic Count : " + dicList.Count);
            }

            public void solution3()
            {
                
            }

        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();

            //List<String> input1 = new List<String>() {"A/B/C/A.TXT","A/CC/D/","A/DD/E.TXT","A/CC/README","A/DD/LL/" };
            //List<String> input2 = new List<String>() {"BB/HELP/DIC/","BB/CC/D/","BB/DD/T.PNG","BB/CC/O.PNG","BB/DD/BB/HELP","BB/DD/BB/CC.EXE","BB/BB/BB/" };            
            //sol.solution2(input2);

            sol.solution3();
        }
    }
}
