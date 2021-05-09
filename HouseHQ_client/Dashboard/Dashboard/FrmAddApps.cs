using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

using HTTP_CLIENT;

namespace Dashboard
{
    public partial class FrmAddApps : Form
    {
        public List<string> apps;
        public List<string> userApps;
        public string IP;
        public string userName;

        public frmApps appsWindow { get; set; }
        public Form1 dashbord { get; set; }

        public FrmAddApps(string ip, List<string> userApps, string userName, frmApps window, Form1 window2)
        {
            InitializeComponent();

            this.userApps = userApps;
            this.userName = userName;
            IP = ip;
            appsWindow = window;
            dashbord = window2;

            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, ip, "105");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "205")
                {
                    var user = JsonConvert.DeserializeObject<getAllApps>(results[1]);
                    apps = user.allAppList;
                }
            } 
            foreach (var app in apps)
            {
                CheckBox test = new CheckBox();
                test.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                test.Cursor = System.Windows.Forms.Cursors.Hand;
                test.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                test.FlatAppearance.BorderSize = 0;
                test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                test.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                test.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);

                test.Margin = new System.Windows.Forms.Padding(2);
                test.Name = app;
                test.Size = new System.Drawing.Size(101, 91);
                test.Text = app;
                test.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                test.UseVisualStyleBackColor = true;
                test.UseVisualStyleBackColor = true;
                test.Click += new System.EventHandler(test_click);

                if (userApps.Contains(app))
                {
                    test.Enabled = false;
                    test.Checked = true;
                    test.ForeColor = Color.FromArgb(46, 51, 73);
                }
                flowLayoutPanel1.Controls.Add(test);
            }
        }

        private void test_click(object sender, EventArgs e)
        {
            var app = (CheckBox)sender;
            if (app.Checked)
            {
                app.ForeColor = Color.FromArgb(46, 51, 73);
            }
            else 
            {
                app.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool good = false;
            foreach (var newApp in flowLayoutPanel1.Controls)
            { 
                if ((newApp is CheckBox) && ((CheckBox)newApp).Checked && (((CheckBox)newApp).Enabled == true))
                {
                    good = true;
                    addAppForUser msg = new addAppForUser()
                    {
                        userName = userName,
                        appName = ((CheckBox)newApp).Text
                    };
                    string json = JsonConvert.SerializeObject(msg);
                    httpClient testLogin = new httpClient();
                    string result = testLogin.sent(json, IP, "108");
                    if (result != null)
                    {
                        string[] results = result.Split('&');
                        if (results[0] == "400")
                        {
                            good = false;

                            MessageBox.Show("Something went wrong Try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            if(good)
            {
                MessageBox.Show("The details have changed successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Hide();
            if (appsWindow != null)
            {
                appsWindow.Close();
                frmApps apps = new frmApps(IP, userName, dashbord) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                apps.FormBorderStyle = FormBorderStyle.None;
                dashbord.pnlFormLoader.Controls.Add(apps);
                apps.Show();
            }
        }
    }
}
