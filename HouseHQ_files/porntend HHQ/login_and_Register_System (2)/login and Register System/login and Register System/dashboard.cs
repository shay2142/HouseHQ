using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Newtonsoft.Json;
using System.Threading;
using System.IO;

using HTTP_CLIENT;

namespace login_and_Register_System
{
    public partial class dashboard : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
           int nLeftRect,
           int nTopRect,
           int nRightRect,
           int nBottomRect,
           int nWidthEllipse,
           int nHeightEllipse

        );

        public string IP_server;
        public dashboard(string result, string ip)
        {
            InitializeComponent();
            this.IP_server = ip;
            var user = JsonConvert.DeserializeObject<okLogin>(result);
            label1.Text = user.name;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            PnlNav.Height = btn1.Height;
            PnlNav.Top = btn1.Top;
            PnlNav.Left = btn1.Left;
            btn1.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void reaset(string btn)
        {
            Button[] strBtn = { btn1, btn2, /*btn5,*/ button3, btn4 };

            foreach (Button nameBtn in strBtn)
            {
                if (nameBtn.Name != btn)
                {
                    nameBtn.BackColor = Color.FromArgb(24, 30, 54);
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PnlNav.Height = btn2.Height;
            PnlNav.Top = btn2.Top;
            reaset(btn2.Name);
            btn2.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PnlNav.Height = btn1.Height;
            PnlNav.Top = btn1.Top;
            PnlNav.Left = btn1.Left;
            reaset(btn1.Name);
            btn1.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PnlNav.Height = btn2.Height;
            PnlNav.Top = btn2.Top;
            reaset(btn2.Name);
            btn2.BackColor = Color.FromArgb(46, 51, 73);
        }

        //private void btn5_Click(object sender, EventArgs e)
        //{
        //    PnlNav.Height = btn5.Height;
        //    PnlNav.Top = btn5.Top;
        //    reaset(btn5.Name);
        //    btn5.BackColor = Color.FromArgb(46, 51, 73);
        //}

        private void btn4_Click(object sender, EventArgs e)
        {
            PnlNav.Height = btn4.Height;
            PnlNav.Top = btn4.Top;
            reaset(btn4.Name);
            btn4.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            File.Delete(@"reamoteapp.rdp");
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PnlNav.Height = button3.Height;
            PnlNav.Top = button3.Top;
            reaset(button3.Name);
            button3.BackColor = Color.FromArgb(46, 51, 73);
        }
        private void createReamoteAppFile(string remoteAppName)
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
                    sw.WriteLine("alternate full address:s:" + IP_server);
                    sw.WriteLine("alternate shell:s:rdpinit.exe");
                    sw.WriteLine("devicestoredirect:s:*");
                    sw.WriteLine("full address:s:" + IP_server);
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
        private void button4_Click(object sender, EventArgs e)
        {
            createReamoteAppFile(button4.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            createReamoteAppFile(button5.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            createReamoteAppFile(button6.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            createReamoteAppFile(button7.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            File.Delete(@"reamoteapp.rdp");
            new frmLogin().Show();
            this.Hide();
        }
    }
}
