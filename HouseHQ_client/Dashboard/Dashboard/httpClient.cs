using Dashboard;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HTTP_CLIENT
{
    class httpClient
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
