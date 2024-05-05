using System;
using System.Net.Http;
using System.Text;

namespace HTTP_CLIENT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //HttpGet();
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
            for (int i = 0; i < 5; i++)
            {
                HttpPost();
            }
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
        }

        static void HttpGet()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://192.168.0.125:8085/requestDate");
            var res = client.SendAsync(httpRequest).Result;
            Console.WriteLine("Response :" + " - " + res.Content.ReadAsStringAsync().Result);
        }

        static void HttpPost()
        {
            HttpClient client = new HttpClient();

            string str = "{\"name\":\"Test1234\"}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:8085/sendTextFile");
            httpRequest.Content = new StringContent(str, Encoding.UTF8, "application/json");
            var res = client.SendAsync(httpRequest).Result;
            Console.WriteLine("Response :" + " - " + res.Content.ReadAsStringAsync().Result);

        }
    }
}
