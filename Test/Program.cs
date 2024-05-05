using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace JsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            JObject jObj1 = JObject.Parse(File.ReadAllText("test1.json"));
            JObject jObj2 = JObject.Parse(File.ReadAllText("test2.json"));

            Console.WriteLine(jObj1 == jObj2);
        }
    }
}
