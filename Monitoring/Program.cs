﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            MainLogic logic = new MainLogic();
            logic.mList = new List<MonitorInfo>();
            logic.loadConfig();

            // Create Thread
            ThreadLogic th = new ThreadLogic();
            Thread workerThread = new Thread(th.DoWork);
            workerThread.Start(logic.mList);    // start함수 안에 parameter를 넣는다. 

            // Tasklist work
            while (true)
            {
                Console.Write("Memory Size : ");
                int size = int.Parse(Console.ReadLine());
                logic.TaskList(size);
            }


            //workerThread.Join();
        }
    }

    class Logic
    {
        protected string resultFile;
        protected string processOutFile;
        public Logic()
        {
            processOutFile = "../../OUTFILE/PROCESS.TXT";
            resultFile = "../../OUTFILE/RESULT.TXT";
        }
    }

    class ThreadLogic : Logic
    {
        private FileInfo getFileInfo(string fname)
        {
            string path = "../../MONFILE/" + fname;
            FileInfo fi = new FileInfo(path);
            return fi;
        }
       
        private void WriteFileResult(string line)
        {
            StreamWriter sw = new StreamWriter(resultFile, true);
            sw.WriteLine(line);
            sw.Close();
        }

        private List<Tuple<string, int>> getTaskList()
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
                char[] delimiter = { ' ' };
                string[] strWords = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (strWords.Length == 6)
                {
                    if (strWords[4].Contains(","))
                    {
                        string strSize = strWords[4].Replace(",", "");
                        int memSize = int.Parse(strSize);
                        tList.Add(new Tuple<string,int>(strWords[0], memSize));
                    }
                }
            }
            return tList;
        }

        public void DoWork(Object obj)
        {
            List<MonitorInfo> miList = (List<MonitorInfo>)obj;

            while (true)
            {
                foreach (var item in miList)
                {
                    if (item.Type == (int)MonitorInfo.EnumType.File)
                    {
                        FileInfo fi = getFileInfo(item.FileName);
                        if (fi.Exists)
                        {
                            item.WriteExist = false;
                            if ((item.Condition == '>' && fi.Length > item.CondValue * 1024) || (item.Condition == '<' && fi.Length < item.CondValue * 1024))
                            {
                                if (item.WriteCondition == false)
                                {
                                    WriteFileResult(item.Line);
                                    item.WriteCondition = true;
                                }
                            }
                            else
                            {
                                item.WriteCondition = false;
                            }
                        }
                        else
                        {
                            item.WriteCondition = false;
                            if (item.WriteExist == false)
                            {
                                WriteFileResult(item.Line);
                                item.WriteExist = true;
                            }
                        }
                    }
                    else // Proc
                    {
                        List<Tuple<string, int>> procList = getTaskList();
                        List<int> findMems = new List<int>();
                        bool bExistInTaskList = false;
                        foreach(var v in procList)
                        {
                            if (v.Item1 == item.FileName)
                            {
                                findMems.Add(v.Item2);
                                bExistInTaskList = true;
                            }
                        }

                        if (bExistInTaskList == false)
                        {
                            item.WriteCondition = false;
                            if (item.WriteExist == false)
                            {
                                WriteFileResult(item.Line);
                                item.WriteExist = true;
                            }
                        }
                        else
                        {
                            item.WriteExist = false;
                            foreach (var v in findMems)
                            {
                                if ((item.Condition == '>' && v > item.CondValue) || (item.Condition == '<' && v < item.CondValue))
                                {
                                    if (item.WriteCondition == false)
                                    {
                                        WriteFileResult(item.Line);
                                        item.WriteCondition = true;
                                    }
                                }
                                else
                                {
                                    item.WriteCondition = false;
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }

    class MainLogic : Logic
    {
        public List<MonitorInfo> mList;
        private string configFile;

        public MainLogic()
        {
            configFile = "../../CONFILE/CONFIG.TXT";
        }

        public void loadConfig()
        {
            // Load Config File
            StreamReader sr = new StreamReader(configFile);
            while (true)
            {
                string line = sr.ReadLine();
                if (line == "" || line == null)
                    break;
                MonitorInfo mInfo = new MonitorInfo(line);
                mList.Add(mInfo);
            }
            sr.Close();
        }

        public void TaskList(int size)
        {
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
                char[] delimiter = { ' ' };
                string[] strWords = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (strWords.Length == 6)
                {
                    if (strWords[4].Contains(","))
                    {
                        string strSize = strWords[4].Replace(",", ""); 
                        int memSize = int.Parse(strSize);
                        if (memSize > size)
                        {
                            WriteProcessMemResult(strWords[0], memSize);
                        }
                    }
                    Console.WriteLine(line);
                }
            }
        }

        private void WriteProcessMemResult(string procName, int memSize)
        {
            StreamWriter sw = new StreamWriter(processOutFile, true);
            sw.WriteLine(procName + " " + memSize);
            sw.Close();
        }
    }

    class MonitorInfo
    {
        public enum EnumType { File, Proc, Error };

        public string Line {get; set;}
        public int Type { get; set; } // File or Proc
        public string FileName { get; set;  }
        public char Condition { get; set;  } // "<" or ">"
        public int CondValue { get; set;  } // File or Mem Size Value
        public bool WriteExist { get; set;  }
        public bool WriteCondition { get; set; }

        public MonitorInfo() { }
        public MonitorInfo(string input)
        {
            Line = input;
            string[] words = input.Split('#');
            if (words[0] == "FILE")
            {
                Type = (int)EnumType.File;
            }
            else if (words[1] == "PROC")
            {
                Type = (int)EnumType.Proc;
            }
            else
            {
                Type = (int)EnumType.Error;
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
        }
    }
}
