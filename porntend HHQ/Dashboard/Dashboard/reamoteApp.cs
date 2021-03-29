using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Dashboard
{
    class reamoteApp
    {
        public void createReamoteAppFile(string remoteAppName , string ipServer)
        {
            string path = @"reamoteapp.rdp";
            File.Delete(path);

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("allow desktop composition:i:1");
                    sw.WriteLine("allow font smoothing:i:1");
                    sw.WriteLine("alternate full address:s:" + ipServer);
                    sw.WriteLine("alternate shell:s:rdpinit.exe");
                    sw.WriteLine("devicestoredirect:s:*");
                    sw.WriteLine("full address:s:" + ipServer);
                    sw.WriteLine("prompt for credentials on client:i:1");
                    sw.WriteLine("promptcredentialonce:i:0");
                    sw.WriteLine("redirectcomports:i:1");
                    sw.WriteLine("redirectdrives:i:1");
                    sw.WriteLine("remoteapplicationmode:i:1");
                    sw.WriteLine("RemoteProgram:s:" + remoteAppName);
                    sw.WriteLine("remoteapplicationprogram:s:||" + remoteAppName);
                    sw.WriteLine("span monitors:i:1");
                    sw.WriteLine("use multimon:i:1");
                }
            }
            System.Diagnostics.Process.Start(path);
            //File.Delete(path);
        }
    }
}
