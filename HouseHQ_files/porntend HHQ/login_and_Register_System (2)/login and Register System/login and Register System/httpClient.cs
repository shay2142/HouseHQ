using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Net;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace HTTP_CLIENT
{
    class httpClient
    {
        //string json;
        //string result;
        //public string result;
        public httpClient(/*string json*/)
        {
            //this.json = json;
            //setLogin();
        }
        public string hostToIp(string host)
        {
            string ip = host;
            IPAddress address;
            
            IPAddress[] ipaddress = Dns.GetHostAddresses(host);
            foreach (IPAddress ipaddr in ipaddress)
            {
                if (IPAddress.TryParse(ipaddr.ToString(), out address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = ipaddr.ToString();
                }
            }
            return ip;
        }
        public string sent(string json, string ip, string code)
        {
            string result = "";
            try
            {
                //Task listenTask = msg<string>(json, ip, code);
                //listenTask.GetAwaiter().GetResult();
                Task<string> task = Task.Run(async () => await msg(json, ip, code));
                result = task.Result;
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Server not found can be written, IP is incorrect", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
        //error
        public async Task<string> msg(string json, string ip, string code)
        {
            var data = new StringContent(code + "&" + json, Encoding.UTF8, "application/json");

            var url = "http://" + ip + ":8080/";
            /*using*/
            var client = new HttpClient();
            //var content = await client.GetStringAsync("http://192.168.0.194:8080/");
            //Console.WriteLine(content);

            var response = await client.PostAsync(url, data);
            //sConsole.WriteLine(response.RequestMessage);
            //Console.WriteLine("test");
            return response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(result);
            //MessageBox.Show(result, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //var user = System.Text.Json.JsonSerializer.Deserialize<ok>(json);
        }
    }
    class okLogin
    {
        // Make sure all class attributes have relevant getter setter.

        public string name { get; set; }

        public List<string> appList { get; set; }

        public string key { get; set; }

        public string img { get; set; }
    }
    class okSingup
    {
        // Make sure all class attributes have relevant getter setter.

        public bool ok { get; set; }
    }
    class login
    {
        // Make sure all class attributes have relevant getter setter.
        public string name { get; set; }

        public string password { get; set; }
    }
    class singup
    {
        public string name { get; set; }

        public string password { get; set; }

        public string mail { get; set; }

        public string key { get; set; }
    }
    class error
    {
        public string msg { get; set; }
    }
}
