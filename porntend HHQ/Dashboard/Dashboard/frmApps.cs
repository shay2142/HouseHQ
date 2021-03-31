using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Dashboard
{
    public partial class frmApps : Form
    {
        public List<string> apps;
        public string IP_server;
        public string userName;

        public frmApps(List<string> apps, string ip, string userName)
        {
            InitializeComponent();
            this.apps = apps;
            this.userName = userName;
            IP_server = ip;

            foreach (var app in apps)
            {
                Button test = new Button();
                test.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                test.Cursor = System.Windows.Forms.Cursors.Hand;
                test.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                test.FlatAppearance.BorderSize = 0;
                test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                test.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                test.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
                //test.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image"))); /*test.Image = Image.FromFile(@"C:\Images\Dock.jpg");*/
                //test.Location = new System.Drawing.Point(290, 9);
                test.Margin = new System.Windows.Forms.Padding(2);
                test.Name = app;
                test.Size = new System.Drawing.Size(101, 91);
                //test.TabIndex = 27;
                test.Text = app;
                test.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                test.UseVisualStyleBackColor = true;
                test.UseVisualStyleBackColor = true;
                test.Click += new System.EventHandler(reamoteApp);
                flowLayoutPanel1.Controls.Add(test);
            }
        }

        public void createReamoteAppFile(string remoteAppName)
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

        private void reamoteApp(object sender, EventArgs e)
        {
            var button = (Button)sender;
            createReamoteAppFile(button.Text);
        }
        private void frmApps_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FrmAddApps(IP_server, apps, userName).Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
