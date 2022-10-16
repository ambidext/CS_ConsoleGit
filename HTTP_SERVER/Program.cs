using System;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace HTTP_SERVER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://192.168.0.101:8085/");
            listener.Start();
            while (true)
            {
                var context = listener.GetContext();
                Console.WriteLine(string.Format("Request({0}) : {1}", context.Request.HttpMethod, context.Request.Url));

                DateTime dt = DateTime.Now;
                String strRes = "";
                if (context.Request.HttpMethod == "GET")
                {
                    
                    strRes = dt.ToString();
                }
                else if (context.Request.HttpMethod == "POST")
                {
                    strRes = "(POST) " + dt.ToString();
                    System.IO.Stream body = context.Request.InputStream;
                    System.Text.Encoding encoding = context.Request.ContentEncoding;
                    System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

                    string strReceive = reader.ReadToEnd();
                    JObject jsonObj = new JObject();
                    jsonObj = JObject.Parse(strReceive);
                    string name = jsonObj["name"].ToString();
                    Console.WriteLine("body : " + body.ToString());
                    Console.WriteLine("strReceive : " + strReceive);

                    body.Close();
                    reader.Close();
                }
                byte[] data = Encoding.UTF8.GetBytes(strRes);
                context.Response.OutputStream.Write(data, 0, data.Length);
                context.Response.StatusCode = 200;
                context.Response.Close();
            }
        }
    }
}
