using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;


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
            var data = new StringContent("115&"/*+ json*/, Encoding.UTF8, "application/json");
            //var data2 = new StringContent("115&" /*+ json*/, Encoding.UTF8, "application/json");

            var url = "http://127.0.0.1:8080/";
            using var client = new HttpClient();

            //var content = await client.GetStringAsync("http://:8080/");
            //Console.WriteLine(content);

            var response = await client.PostAsync(url, data);
            Console.WriteLine(response.RequestMessage);
            //Console.WriteLine("test");
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            jsonSentLogs user = new jsonSentLogs();
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "215")
                {
                    user = JsonConvert.DeserializeObject<jsonSentLogs>(results[1]);
                }
            

            
                using (StreamWriter writer = new StreamWriter("test.txt"))
                {
                    int maxID = 0;
                    int maxDate = 0;
                    int maxType = 0;
                    int maxSource = 0;
                    foreach (var log in user.jsonLogs)
                    {
                        maxID = maxID < log.ID.ToString().Length ? log.ID.ToString().Length : maxID;
                        maxDate = maxDate < log.dateLogs.ToString().Length ? log.dateLogs.ToString().Length : maxDate;
                        maxType = maxType < log.typeLog.ToString().Length ? log.typeLog.ToString().Length : maxType;
                        maxSource = maxSource < log.source.ToString().Length ? log.source.ToString().Length : maxSource;
                    }
                    writer.WriteLine("ID" + new string(' ', maxID - "ID".Length) + "|Date logs" + new string(' ', maxDate - "Date logs".Length) + "|Type logs" + new string(' ', maxType - "Type logs".Length) + "|Source" + new string(' ', maxSource - "Source".Length) + "|log");
                    writer.WriteLine(new string('-', maxID) + "+" + new string('-', maxDate) +  "+" + new string('-', maxType) + "+" + new string('-', maxSource) + "+" + new string('-', 100));

                    foreach (var log in user.jsonLogs)
                    {
                        writer.WriteLine(log.ID + new string(' ', maxID - log.ID.ToString().Length) + "|" + log.dateLogs + new string(' ', maxDate - log.dateLogs.ToString().Length) + "|" + log.typeLog + new string(' ', maxType - log.typeLog.ToString().Length) + "|" + log.source + new string(' ', maxSource - log.source.ToString().Length) + "|" + log.log);
                        writer.WriteLine(new string('-', maxID) + "+" + new string('-', maxDate) + "+" + new string('-', maxType) + "+" + new string('-', maxSource) + "+" + new string('-', 100));

                    }
                }
                // Read a file  
                string readText = File.ReadAllText("test.txt");
                Console.WriteLine(readText);
            }

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

    public class sentLogs
    {
        public int ID { get; set; }
        public string dateLogs { get; set; }
        public string typeLog { get; set; }
        public string source { get; set; }
        public string log { get; set; }
    }

    public class jsonSentLogs
    {
        public List<sentLogs> jsonLogs { get; set; }
    }
}