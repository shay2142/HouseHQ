using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace HouseHQ_server
{
    public class remoteApp
    {
        public void laodApp(httpServer Http)
        {
            string path = @"readApp.bat";

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
                        if (!Http.db.appIsExists(Http.con, Path.GetFileNameWithoutExtension(s)))
                        {
                            Http.db.insertVluesToAPP(Http.con, Path.GetFileNameWithoutExtension(s), Path.GetFileNameWithoutExtension(s));
                        }
                    }
                }
            }
            File.Delete("app.txt");
        }
    }
}
