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
            //addLevelKey test = new addLevelKey()
            //{
            //    nameLevel = "test",
            //    admin = true,
            //    apps = new List<string>()
            //        {
            //            "notepad"
            //        }
            //   };
            hash hashUser = new hash();

            deleteLevel test = new deleteLevel()
            { 
                nameLevel = "test"
            };
            string json = JsonConvert.SerializeObject(test);
            var data = new StringContent("105&"/*+ json*/, Encoding.UTF8, "application/json");
            //var data2 = new StringContent("115&" /*+ json*/, Encoding.UTF8, "application/json");

            var url = "http://192.168.0.128:8080/";
            using var client = new HttpClient();

            //var content = await client.GetStringAsync("http://192.168.0.194:8080/");
            //Console.WriteLine(content);

            var response = await client.PostAsync(url, data);
            Console.WriteLine(response.RequestMessage);
            //Console.WriteLine("test");
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            //var response2 = await client.PostAsync(url, data2);
            //Console.WriteLine(response2.RequestMessage);
            ////Console.WriteLine("test");
            //string result2 = response2.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(result2);

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

    class changeAccount
    {
        public string userName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string mail { get; set; }
        public string level { get; set; }
    }

    class addLevelKey
    {
        public string nameLevel { get; set; }
        public List<string> apps { get; set; }
        public bool admin { get; set; }
    }

    class deleteAppForLevel
    {
        public string nameLevel { get; set; }
        public List<string> apps { get; set; }
    }

    class deleteLevel
    {
        public string nameLevel { get; set; }
    }
}