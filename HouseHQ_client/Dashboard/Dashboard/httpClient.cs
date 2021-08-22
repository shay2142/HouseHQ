using Dashboard;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Dashboard;

namespace Dashboard
{
    class httpClient
    {
        public hash hashPass = new hash();

        public httpClient()
        {
        }

        public string sent(string json, string ip, string code, string userName, string password)
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
                Task<string> task = Task.Run(async () => await msg(json, splitList[0], port, code, userName, password));
                try
                {
                    task.Wait();
                    result = task.Result;
                }
                catch
                {
                    result = "404&{msg:\"Server not found can be written, IP is incorrect\"}";
                }
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Server not found can be written, IP is incorrect", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public async Task<string> msg(string json, string ip, string port, string code, string userName, string password)
        {
            bool remember = Properties.Settings.Default.remember;

            int len = (code + "&" + json).Length;

            var data = new StringContent(StringCipher.Encrypt(code + "&" + json + "&" + hashPass.ComputeSha256Hash(userName + password) + "&" + hashPass.ComputeSha256Hash(remember.ToString()), hashPass.ComputeSha256Hash((code + "&" + json).Length.ToString())), Encoding.UTF8, "application/json|" + len);

            var url = "http://" + ip + ":" + port + "/";

            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string req = StringCipher.Decrypt(response.Content.ReadAsStringAsync().Result, hashPass.ComputeSha256Hash(response.Content.Headers.GetValues("Content-Type").FirstOrDefault().Split('|')[1]));

            return req;
        }
    }
}
