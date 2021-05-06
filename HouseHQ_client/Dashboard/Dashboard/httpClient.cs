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
            var splitList = host.Split(':');
            string ip = splitList[0];
            IPAddress address;
            IPAddress[] ipaddress = Dns.GetHostAddresses(splitList[0]);
            foreach (IPAddress ipaddr in ipaddress)
            {
                if (IPAddress.TryParse(ipaddr.ToString(), out address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = ipaddr.ToString();
                }
            }
            if (splitList.Length > 1 && (splitList[1] == null || splitList[1] == ""))
            {
                return ip + ":" + splitList[1];
            }
            return ip;
        }

        public string sent(string json, string ip, string code)
        {
            string result = "";
            string port = "8080";
            var splitList = ip.Split(':');
            if (splitList.Length > 1 && (splitList[1] == null || splitList[1] == ""))
            {
                port = splitList[1];
            }
            try
            {
                Task<string> task = Task.Run(async () => await msg(json, ip, port, code));
                result = task.Result;
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Server not found can be written, IP is incorrect", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public async Task<string> msg(string json, string ip, string port, string code)
        {
            var data = new StringContent(code + "&" + json, Encoding.UTF8, "application/json");

            var url = "http://" + ip + ":" + port + "/";

            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
