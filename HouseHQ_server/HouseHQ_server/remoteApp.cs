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
            List<string> appsInServer = new List<string>();

            foreach (string app in getAppsRemoteApp())
            {
                appsInServer.Add(Path.GetFileNameWithoutExtension(app));
                if (!Http.db.appIsExists(Http.con, Path.GetFileNameWithoutExtension(app)))
                {
                    Http.db.insertVluesToAPP(Http.con, Path.GetFileNameWithoutExtension(app), Path.GetFileNameWithoutExtension(app));
                }

                if (!File.Exists(@".\appImg\" + Path.GetFileNameWithoutExtension(app) + ".png"))
                {
                    sentImage.getIconFromExe(getPathOfApp(Path.GetFileNameWithoutExtension(app)), Path.GetFileNameWithoutExtension(app));
                }
            }

            foreach (string app in Http.db.getAllApplications(Http.con))
            {
                if (!appsInServer.Contains(app))
                {
                    Http.db.deleteApps(Http.con, app);

                    //צריך למחוק את כל התמונות שלא צריך מהתיקיה
                    if (File.Exists(@".\appImg\" + Path.GetFileNameWithoutExtension(app) + ".png"))
                    {
                        File.Delete(@".\appImg\" + Path.GetFileNameWithoutExtension(app) + ".png");
                    }
                }
            }
        }

        /*


         input: 

         output:
         */
        public string getPathOfApp(string appName)
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + appName + "\\";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string value_name in key.GetValueNames())
                {
                    if (value_name == "Path")
                    {
                        return key.GetValue(value_name).ToString();
                    }
                }
            }
            return "";
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

        /*


         input: 

         output:
         */
        public List<string> getAppsRemoteApp()
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications";
            List<string> apps = new List<string>();

            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    apps.Add(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + subkey_name);
                }
            }
            return apps;
        }
    }
}
