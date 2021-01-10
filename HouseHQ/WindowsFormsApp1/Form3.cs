using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using System;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            //Bitmap bm = new Bitmap("Picture.jpg");
            //IntPtr hBitmap = bm.GetHbitmap();
            //appList.Items.Add("calculator");
            //appList.SmallImageList.Images.Add(Bitmap.FromFile(@"C:\Users\shay5\anaconda3\pkgs\pywin32-227-py38he774522_1\Lib\site-packages\win32\Demos\images\frowny.bmp"));
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void appList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //appList.Items.Add("List item text", 3);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //foreach (var process in Process.GetProcessesByName("mstsc"))
            //{
            //    process.Kill();
            //}
            File.Delete(@"test.rdp");
            foreach (var process in Process.GetProcessesByName("mstsc"))
            {
                process.Kill();
            }
            //this.close();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\remoteapp\‏‏notepad.rdp");
            createReamoteAppFile("USER2", label1.Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\remoteapp\Visual Studio 2019.rdp");
            createReamoteAppFile("USER2", label4.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\remoteapp\notepadpp.rdp");
            createReamoteAppFile("USER2", label3.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\remoteapp\Calculator.rdp");
            createReamoteAppFile("USER2", label2.Text);
        }
        private void createReamoteAppFile(string server, string remoteAppName)
        {
            string path = @"test.rdp";
            File.Delete(path);

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("allow desktop composition:i:1");
                    sw.WriteLine("allow font smoothing:i:1");
                    sw.WriteLine("alternate full address:s:" + server);
                    sw.WriteLine("alternate shell:s:rdpinit.exe");
                    sw.WriteLine("devicestoredirect:s:*");
                    sw.WriteLine("full address:s:" + server);
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
