using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace HttpClientEx
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //using var client = new HttpClient();
            //var content = await client.GetStringAsync("http://192.168.0.194:8080/");

            //Console.WriteLine(content);
            login test = new login()
            {
                name = "shay",
                password = "12345"
            };
            string json = JsonConvert.SerializeObject(test);
            var data = new StringContent("101&" + json, Encoding.UTF8, "application/json");
            var url = "http://localhost:8080/";
            using var client = new HttpClient();

            //var content = await client.GetStringAsync("http://192.168.0.194:8080/");
            //Console.WriteLine(content);

            var response = await client.PostAsync(url, data);
            Console.WriteLine(response.RequestMessage);
            //Console.WriteLine("test");
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

        }
    }
    class ok
    {
        // Make sure all class attributes have relevant getter setter.

        public string name { get; set; }

        public List<string> appList { get; set; }

        public string key { get; set; }

        public string img { get; set; }
    }
    class login
    {
        // Make sure all class attributes have relevant getter setter.
        public string name { get; set; }

        public string password { get; set; }
    }
}