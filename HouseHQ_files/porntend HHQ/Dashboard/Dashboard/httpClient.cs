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


namespace HTTP_CLIENT
{
    class httpClient
    {
        public httpClient()
        {
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
                Task<string> task = Task.Run(async () => await msg(json, ip, code));
                result = task.Result;
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Server not found can be written, IP is incorrect", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public async Task<string> msg(string json, string ip, string code)
        {
            var data = new StringContent(code + "&" + json, Encoding.UTF8, "application/json");

            var url = "http://" + ip + ":8080/";

            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
