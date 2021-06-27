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
        public hash hashPass = new hash();

        public httpClient()
        {
        }

        public string sent(string json, string ip, string code)
        {
            string result = "";
            string port = "8080";
            var splitList = ip.Split(':');

            code = hashPass.ComputeSha256Hash(code);

            if (splitList.Length > 1 && !(splitList[1] == null || splitList[1] == ""))
            {
                port = splitList[1];
            }
            try
            {
                Task<string> task = Task.Run(async () => await msg(json, splitList[0], port, code));
                result = task.Result;
            }
            catch (InvalidCastException e)
            {
                //MessageBox.Show("Server not found can be written, IP is incorrect", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public async Task<string> msg(string json, string ip, string port, string code)
        {
            int len = (code + "&" + json).Length;

            var data = new StringContent(hashPass.Encrypt(code + "&" + json, hashPass.ComputeSha256Hash((code + "&" + json).Length.ToString())), Encoding.UTF8, "application/json|" + len);

            var url = "http://" + ip + ":" + port + "/";

            var client = new HttpClient();

             var response = await client.PostAsync(url, data);

            string req = hashPass.Decrypt(response.Content.ReadAsStringAsync().Result, hashPass.ComputeSha256Hash(response.Content.Headers.GetValues("Content-Type").FirstOrDefault().Split('|')[1]));


            return req;
        }
    }
}