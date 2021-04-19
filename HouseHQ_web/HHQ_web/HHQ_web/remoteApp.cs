using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;

namespace HHQ_web
{
    public class remoteApp
    {
        public void createRemoteAppFile(string ip, string remoteAppName)
        {
            string path = @"D:\HHQ_web\HHQ_web\remoteApp\reamoteapp.c";
            string path2 = @"D:\HHQ_web\HHQ_web\remoteApp\gcc.bat";

            File.Delete(path);
            File.Delete(path2);

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("#include <stdio.h>");
                    sw.WriteLine("#include <string.h>");
                    sw.WriteLine("#include <windows.h>");
                    sw.WriteLine("#include <unistd.h>");
                    sw.WriteLine("\n");
                    sw.WriteLine("int main ()");
                    sw.WriteLine("{");
                    sw.WriteLine("  char IP_server[] = " + '"' + ip + '"' + ";");
                    sw.WriteLine("  char remoteAppName[] = " + '"' + remoteAppName + '"' + ";");
                    sw.WriteLine("  char fileName[] = " + '"' + "remoteApp.rdp" + '"' + ";");
                    sw.WriteLine("  FILE * fp;");
                    sw.WriteLine("\n");
                    sw.WriteLine("  fp = fopen (fileName," + '"' + "w" + '"' + ");");
                    sw.WriteLine("\n");
                    sw.WriteLine("  fprintf (fp, " + '"' + "allow desktop composition:i:1%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "allow font smoothing: i:1%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "alternate full address: s:%s%c" + '"' + ", IP_server, 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "alternate shell: s:rdpinit.exe%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "devicestoredirect: s:*%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "full address: s:%s%c" + '"' + ", IP_server, 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "prompt for credentials on client:i:1%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "promptcredentialonce: i:0%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "redirectcomports: i:1%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "redirectdrives: i:1%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "remoteapplicationmode: i:1%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "RemoteProgram: s:%s%c" + '"' + ", remoteAppName, 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "remoteapplicationprogram: s:||%s%c" + '"' + ", remoteAppName, 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "span monitors: i:1%c" + '"' + ", 10);");
                    sw.WriteLine("  fprintf (fp, " + '"' + "use multimon: i:1%c" + '"' + ", 10);");
                    sw.WriteLine("\n");
                    sw.WriteLine("  fclose (fp);");
                    sw.WriteLine("\n");
                    sw.WriteLine("  system(fileName);");
                    sw.WriteLine("  Sleep(10);");
                    sw.WriteLine("  do{remove(fileName);}while(access( fileName, F_OK ) == 0);");
                    sw.WriteLine("\n");
                    sw.WriteLine("  return 0;");
                    sw.WriteLine("}");
                    sw.WriteLine(); 
                }
            }
            var process = System.Diagnostics.Process.Start(@"D:\HHQ_web\HHQ_web\remoteApp\gcc.exe", @"-o D:\HHQ_web\HHQ_web\remoteApp\" + remoteAppName.Replace(" ", "") + @".exe D:\HHQ_web\HHQ_web\remoteApp\reamoteapp.c");
            process.WaitForExit();
        }
    }
}