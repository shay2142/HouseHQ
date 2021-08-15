using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Dashboard
{
    static class Program
    {
        public static hash hashPass = new hash();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.userName == "" && Properties.Settings.Default.password == "")
            {
                Application.Run(new frmLogin());
            }
            else
            {
                loginParameters userPram = new loginParameters();
                login test = new login()
                {
                    name = hashPass.ComputeSha256Hash(Properties.Settings.Default.userName),
                    password = hashPass.ComputeSha256Hash(Properties.Settings.Default.password)
                };
                string json = JsonConvert.SerializeObject(test);
                httpClient testLogin = new httpClient();
                string result = testLogin.sent(json, Properties.Settings.Default.ipServer, "101", Properties.Settings.Default.userName, test.password);
                if (result != null)
                {
                    string[] results = result.Split('&');
                    if (results[0] == "201")
                    {
                        var user = JsonConvert.DeserializeObject<okLogin>(results[1]);

                        userPram.ipServer = Properties.Settings.Default.ipServer;
                        userPram.userName = user.name;
                        userPram.password = Properties.Settings.Default.password;
                        userPram.mail = user.mail;
                        userPram.key = user.key;
                        userPram.img = user.img;
                        userPram.apps = user.appList;
                        userPram.remember = true;

                        Application.Run(new Form1(userPram));
                        //this.Hide();
                    }
                    else
                    {
                        Application.Run(new frmLogin());
                    }
                }
            }
        }
    }
}
