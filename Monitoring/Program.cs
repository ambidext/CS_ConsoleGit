using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitoring
{
    static class Paths
    {
        public const string resultFile = "../../OUTFILE/RESULT.TXT";
        public const string processOutFile = "../../OUTFILE/PROCESS.TXT";
        public const string configFile = "../../CONFILE/CONFIG.TXT";
    }

    class Program
    {
        static void Main(string[] args)
        {
            MainLogic mLogic = new MainLogic();
            // Create Thread
            ThreadLogic th = new ThreadLogic();
            Thread workerThread = new Thread(th.DoMonitoring);
            workerThread.Start();    // start함수 안에 parameter를 넣는다. 

            // Tasklist work
            while (true)
            {
                Console.Write("Memory Size : ");
                int size = int.Parse(Console.ReadLine());
                mLogic.WriteLargerMemsProc(size);
            }

            //workerThread.Join();
        }
    }

    class Logic
    {
        protected FileInfo getFileInfo(string fname)
        {
            string path = "../../MONFILE/" + fname;
            FileInfo fi = new FileInfo(path);
            return fi;
        }

        protected void WriteResultAppend(string path, string line)
        {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(line);
            sw.Close();

            //FileStream fs = new FileStream(path, FileMode.Append);
            //byte[] byteArray = { 0x01, 0x02, 0x03 };
            //fs.Write(byteArray, 0, byteArray.Length);
            //fs.Close();
        }  

        protected List<Tuple<string, int>> getTaskList()
        {
            List<Tuple<string, int>> tList = new List<Tuple<string, int>>();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "tasklist.exe";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;

            Process process = Process.Start(start);         // exe 실행시
            StreamReader reader = process.StandardOutput;   // 출력되는 값을 가져오기 위해 StreamReader에 연결  
            while (true)
            {
                string line = reader.ReadLine();            // 출력값의 한 라인을 읽는다 
                if (line == null)
                    break;
                if (line == "")
                    continue;
                char[] delimiter = { ' ' };
                string[] strWords = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (strWords.Length < 6)
                    continue;
                int lastIdx = strWords.Length - 1;
                if (strWords[lastIdx] == "K")
                {
                    string procName = "";
                    int memSize = int.Parse(strWords[lastIdx - 1].Replace(",", ""));

                    for (int i = 0; i < strWords.Length - 5; i++) // 뒤에서 5개까지는 이름이 아니므로, 5개를 제외한 나머지가 모두 Process이름
                    {
                        if (i != 0)
                            procName += " ";
                        procName += strWords[i];
                    }
                    //Console.WriteLine(procName + ": " + memSize);
                    tList.Add(new Tuple<string, int>(procName, memSize));
                }
            }
            return tList;
        }
    }

    class ThreadLogic : Logic
    {
        public List<ConfigInfo> monitorList;

        private List<ConfigInfo> loadConfig()
        {
            List<ConfigInfo> confList = new List<ConfigInfo>();

            // Load Config File
            StreamReader sr = new StreamReader(Paths.configFile);
            while (true)
            {
                string line = sr.ReadLine();
                if (line == "" || line == null)
                    break;
                ConfigInfo mInfo = new ConfigInfo(line);
                confList.Add(mInfo);
            }
            sr.Close();

            return confList;
        }

        private void FileAlarmCheck(ConfigInfo item)
        {
            FileInfo fi = getFileInfo(item.FileName);
            if (fi.Exists)
            {
                //item.WroteExist = false;
                if ((item.Condition == '>' && fi.Length > item.CondValue * 1024) || (item.Condition == '<' && fi.Length < item.CondValue * 1024))
                {
                    if (item.LastAlarmAttr != ConfigInfo.AlarmAttr.Size)
                    {
                        WriteResultAppend(Paths.resultFile, item.Line);
                        item.LastAlarmAttr = ConfigInfo.AlarmAttr.Size;
                    }
                }
                else
                {
                    item.LastAlarmAttr = ConfigInfo.AlarmAttr.None;
                }
            }
            else
            {
                //item.WroteCondition = false;
                if (item.LastAlarmAttr != ConfigInfo.AlarmAttr.Exist)
                {
                    WriteResultAppend(Paths.resultFile, "FILE#" + item.FileName);
                    item.LastAlarmAttr = ConfigInfo.AlarmAttr.Exist;
                }
            }
        }

        private void ProcAlarmCheck(ConfigInfo item, List<Tuple<string, int>> procList)
        {
            List<int> findMems = new List<int>();
            bool bExistInTaskList = false;
            foreach (var v in procList)
            {
                if (v.Item1 == item.FileName)
                {
                    findMems.Add(v.Item2);
                    bExistInTaskList = true;
                }
            }

            if (bExistInTaskList == false)
            {
                //item.WroteCondition = false;
                if (item.LastAlarmAttr != ConfigInfo.AlarmAttr.Exist)
                {
                    WriteResultAppend(Paths.resultFile, "PROC#" + item.FileName);
                    item.LastAlarmAttr = ConfigInfo.AlarmAttr.Exist;
                }
            }
            else
            {
                //item.WroteExist = false;
                foreach (var v in findMems)
                {
                    if ((item.Condition == '>' && v > item.CondValue) || (item.Condition == '<' && v < item.CondValue))
                    {
                        if (item.LastAlarmAttr != ConfigInfo.AlarmAttr.Memory)
                        {
                            WriteResultAppend(Paths.resultFile, item.Line);
                            item.LastAlarmAttr = ConfigInfo.AlarmAttr.Memory;
                            break;
                        }
                    }
                    else
                    {
                        item.LastAlarmAttr = ConfigInfo.AlarmAttr.None;
                    }
                }
            }
        }

        public void DoMonitoring()
        {
            // load config file
            monitorList = loadConfig(); 

            while (true)
            {
                List<Tuple<string, int>> procList = getTaskList();

                foreach (var item in monitorList)
                {
                    if (item.Type == ConfigInfo.MonitoringType.File)
                    {
                        FileAlarmCheck(item);
                    }
                    else // Proc
                    {
                        //..List<Tuple<string, int>> procList = getTaskList();
                        ProcAlarmCheck(item, procList);
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }

    class MainLogic : Logic
    {
        public void WriteLargerMemsProc(int size)
        {
            List<Tuple<string, int>> tList = getTaskList();

            foreach(var v in tList)
            {
                if (v.Item2 > size)
                {
                    WriteResultAppend(Paths.processOutFile, v.Item1 + " " + v.Item2);
                }
            }
        }
    }
    
    class ConfigInfo
    {
        public enum MonitoringType { File, Proc, Error };
        public enum AlarmAttr { None, Exist, Size, Memory };

        public string Line {get; set;}
        public MonitoringType Type { get; set; } // File or Proc
        public string FileName { get; set;  }
        public char Condition { get; set;  } // "<" or ">"
        public int CondValue { get; set;  } // File or Mem Size Value
        public bool WroteExist { get; set;  }
        public bool WroteCondition { get; set; }
        public AlarmAttr LastAlarmAttr { get; set; }
        private void parsing(string input)
        {
            Line = input;
            string[] words = input.Split('#');
            if (words[0] == "FILE")
            {
                Type = MonitoringType.File;
            }
            else if (words[1] == "PROC")
            {
                Type = MonitoringType.Proc;
            }
            else
            {
                Type = MonitoringType.Error;
            }

            FileName = words[1];

            if (words.Length < 3)
            {
                Condition = ' ';
                CondValue = -1;
            }
            else
            {
                if (words[2].Contains("<"))
                {
                    Condition = '<';
                    string[] condWords = words[2].Split('<');
                    CondValue = int.Parse(condWords[1]);
                }
                else if (words[2].Contains(">"))
                {
                    Condition = '>';
                    string[] condWords = words[2].Split('>');
                    CondValue = int.Parse(condWords[1]);
                }
            }
            LastAlarmAttr = AlarmAttr.None;
        }

        public ConfigInfo() { }
        public ConfigInfo(string input)
        {
            parsing(input);
        }
    }
}
