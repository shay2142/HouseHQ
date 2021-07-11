using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HouseHQ_server
{
    class rdpwrap
    {
        public void update()
        {
            string path = @"C:\Program Files\RDP Wrapper\autoupdate.bat";
            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start(path).WaitForExit();
            }
            else
            {
                Console.WriteLine("the rdpwrap is not exist");
            }
        }
    }
}
