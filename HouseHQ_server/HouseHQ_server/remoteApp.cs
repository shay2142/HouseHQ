using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using Cassia;

namespace HouseHQ_server
{
    public class remoteApp
    {

        /*


         input: 

         output:
         */
        public void laodApp(httpServer Http)
        {
            string path = @"readApp.bat";
            List<string> appsInServer = new List<string>();

            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("REG QUERY " + '"' + @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications" + '"' + " > app.txt");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            System.Diagnostics.Process.Start(path).WaitForExit();

            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText("app.txt"))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (Path.GetFileNameWithoutExtension(s) != "")
                    {
                        appsInServer.Add(Path.GetFileNameWithoutExtension(s));
                        if (!Http.db.appIsExists(Http.con, Path.GetFileNameWithoutExtension(s)))
                        {
                            Http.db.insertVluesToAPP(Http.con, Path.GetFileNameWithoutExtension(s), Path.GetFileNameWithoutExtension(s));
                        }
                    }
                }
            }
            File.Delete("app.txt");

            foreach (string app in Http.db.getAllApplications(Http.con))
            {
                if (!appsInServer.Contains(app))
                {
                    Http.db.deleteApps(Http.con, app);
                }
            }
        }

        /*


         input: 

         output:
         */
        public void createRemoteApp(httpServer Http, string pathApp, string nameApp)
        {
            if (nameApp == null)
            {
                nameApp = Path.GetFileNameWithoutExtension(pathApp);
            }

            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + nameApp) == null)
            {
                RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + nameApp);
                key.SetValue("Path", pathApp);
                key.Close();

                laodApp(Http);
            }
        }

        /*


         input: 

         output:
         */
        public void deleteRemoteApp(httpServer Http, string nameApp)
        {
            nameApp = Path.GetFileNameWithoutExtension(nameApp);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + nameApp, true))
            {
                if (key != null)
                {
                    key.DeleteSubKey(nameApp);
                }
            }
            laodApp(Http);
        }
    }
}
