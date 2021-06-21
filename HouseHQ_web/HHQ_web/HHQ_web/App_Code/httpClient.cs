using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HHQ_web
{
    public class httpClient
    {
        public httpClient()
        {
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
                //MessageBox.Show("Server not found can be written, IP is incorrect", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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