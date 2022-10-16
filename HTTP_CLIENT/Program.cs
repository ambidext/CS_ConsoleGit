using System;
using System.Net.Http;
using System.Text;

namespace HTTP_CLIENT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://192.168.0.125:8085/requestDate");
            var res = client.SendAsync(httpRequest).Result;
            Console.WriteLine("Response :" + " - " + res.Content.ReadAsStringAsync().Result);

            string str = "{\"name\":\"Test1234\"}";
            httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:8080/sendTextFile");
            httpRequest.Content = new StringContent(str, Encoding.UTF8, "application/json");
            res = client.SendAsync(httpRequest).Result;
            Console.WriteLine("Response :" + " - " + res.Content.ReadAsStringAsync().Result);
        }
    }
}
