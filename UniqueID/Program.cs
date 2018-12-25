using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.NetworkInformation;

namespace UniqueID
{
    class Program
    {
        static void Main(string[] args)
        {
            UniqueIDs uids = new UniqueIDs();

            uids.GetBaseboardID();
            uids.RetrieveProcessorInfo();
            uids.GetMAC();
            uids.GetHarddiskSerial();
        }
    }

    class UniqueIDs
    {
        public void RetrieveProcessorInfo()
        {
            //initialize the select query with command text
            SelectQuery query = new SelectQuery(@"Select * from Win32_Processor");

            //initialize the searcher with the query it is supposed to execute
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                //execute the query
                foreach (ManagementObject process in searcher.Get())
                {
                    //print process properties
                    Console.WriteLine("/*********Processor Information ***************/");
                    Console.WriteLine("{0}{1}", "Manufacturer:", process["Manufacturer"]);
                    Console.WriteLine("{0}{1}", "Name:", process["Name"]);
                    Console.WriteLine("{0}{1}", "MaxClockSpeed:", process["MaxClockSpeed"]);
                    Console.WriteLine("{0}{1}", "ProcessorID:", process["ProcessorID"]);
                    Console.WriteLine("{0}{1}", "Revision:", process["Revision"]);
                }
            }
        }

        public void GetBaseboardID()
        {
            // Win32_CPU will work too
            var search = new ManagementObjectSearcher("SELECT * FROM Win32_baseboard");
            var mobos = search.Get();

            foreach (var m in mobos)
            {
                var serial = m["SerialNumber"]; // ProcessorID if you use Win32_CPU
                Console.WriteLine("Baseboard ID : "+serial);
            }
        }

        public void GetMAC()
        {
            Console.WriteLine("MAC : "+NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress());
        }

        public void GetHarddiskSerial()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            string serial_number = "";

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                serial_number = wmi_HD["SerialNumber"].ToString();
                Console.WriteLine("Harddisk Serial : " + serial_number);
            }
        }
    }

}
