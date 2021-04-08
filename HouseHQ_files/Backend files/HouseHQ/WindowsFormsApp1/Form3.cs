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
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        string IP = "";
        bool connect = false;
        DateTime now;

        public Form3(string ip)
        {
            InitializeComponent();

            IP = ip;
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
            File.Delete(@"reamoteapp.rdp");
            connect = false;
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
            createReamoteAppFile(IP, label1.Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\remoteapp\Visual Studio 2019.rdp");
            createReamoteAppFile(IP, label4.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\remoteapp\notepadpp.rdp");
            createReamoteAppFile(IP, label3.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //Dictionary<string, string>[] oldSession = getSession();
            //System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\remoteapp\Calculator.rdp");
            createReamoteAppFile(IP, label2.Text);
            //Dictionary<string, string>[] newSession = getSession();
        }

        private void createReamoteAppFile(string server, string remoteAppName)
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
            if (!connect)
            {
                connect = true;
                //now = DateTime.Now;
                //MessageBox.Show(now.Day + "/" + now.Month + "/" + now.Year + " " + now.Hour + ":" + now.Minute, "", MessageBoxButtons.OK);
                //Console.WriteLine("{0}/{1}/{2} {3}:{4}", now.Day, now.Month, now.Year, now.Hour, now.Minute);
            } 
            System.Diagnostics.Process.Start(path);
            //File.Delete(path);
        }
        private Dictionary<string, string>[] getSession()
        {
            run();
            string text = System.IO.File.ReadAllText(@"test1.txt");

            System.Console.WriteLine("Contents of WriteText.txt = {0}", text.Replace(" ", "|").Replace("||", "|"));
            //string[] subs = text.Replace(" ", "|")//.Split("|");
            string[] subs = text.Replace(" ", "|").Split('|');
            subs = subs.Where(o => o != "").ToArray();
            subs = subs.Where(o => o != "\r\n").ToArray();
            subs = subs.Where(o => o != "TYPE").ToArray();
            subs = subs.Where(o => o != "DEVICE").ToArray();

            Dictionary<string, string>[] dict = new Dictionary<string, string>[(subs.Length / 4) - 1];

            for (int i = 1; i <= ((subs.Length / 4) - 1); i++)
            {
                dict[i - 1] = new Dictionary<string, string>();
                dict[i - 1].Add(subs[0], subs[(4 * i)]);
                dict[i - 1].Add(subs[1], subs[(4 * i) + 1]);
                dict[i - 1].Add(subs[2], subs[(4 * i) + 2]);
                dict[i - 1].Add(subs[3], subs[(4 * i) + 3]);
            }

            return dict;
        }
        public void run()
        {
            string path = @"test.bat";

            File.Delete("test1.txt");

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("query user /server:USER2 > test1.txt");
            }
            System.Diagnostics.Process.Start("test.bat");
        }

    }
}
