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

namespace Dashboard
{
    public partial class frmAddAppToServer : Form
    {
        public List<string> apps = new List<string>();
        public loginParameters USER = new loginParameters();
        public hash Hash = new hash();
        public Dictionary<string, app> allApps = new Dictionary<string, app>();

        public frmAddAppToServer(loginParameters user)
        {
            InitializeComponent();
            USER = user;

            getData();
        }

        public void getData()
        {
            httpClient http = new httpClient();
            string result = http.sent(null, USER.ipServer, "122", USER.userName, Hash.ComputeSha256Hash(USER.password));

            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "222")
                {
                    var user = JsonConvert.DeserializeObject<getAllAppsOnPC>(results[1]);
                    allApps = user.getApps;
                    apps = new List<string>(user.getApps.Keys);
                }
                else if (results[0] == "400")
                {
                    MessageBox.Show(JsonConvert.DeserializeObject<error>(results[1]).msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Hide();
                }
                else if (results[0] == "404")
                {
                    new frmLogin().Show();
                    USER.dash.Hide();
                    this.Hide();
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

                if (USER.apps.Contains(app))
                {
                    test.Enabled = false;
                    test.Checked = true;
                    test.ForeColor = Color.FromArgb(46, 51, 73);
                }
                flowLayoutPanel1.Controls.Add(test);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool good = false;
            List<addApps> newApps = new List<addApps>();

            foreach (var newApp in flowLayoutPanel1.Controls)
            {
                if ((newApp is CheckBox) && ((CheckBox)newApp).Checked && (((CheckBox)newApp).Enabled == true))
                {
                    good = true;
                    newApps.Add(new addApps()
                    { 
                        nameApp = ((CheckBox)newApp).Text,
                        pathExeFile = allApps[((CheckBox)newApp).Text].EXE_File
                    });
                }
            }
            addAppsOnServer msg = new addAppsOnServer()
            {
                userName = USER.userName,
                password = Hash.ComputeSha256Hash(USER.password),
                listApps = newApps
            };
            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, USER.ipServer, "104", USER.userName, Hash.ComputeSha256Hash(USER.password));
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "400")
                {
                    good = false;

                    MessageBox.Show(JsonConvert.DeserializeObject<error>(results[1]).msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (results[0] == "404")
                {
                    new frmLogin().Show();
                    USER.dash.Hide();
                    this.Hide();
                }
            }
            if (good)
            {
                MessageBox.Show("The details have changed successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Hide();
            //if (appsWindow != null)
            //{
            //    appsWindow.Close();
            //    frmApps apps = new frmApps(USER, dashbord) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            //    apps.FormBorderStyle = FormBorderStyle.None;
            //    dashbord.pnlFormLoader.Controls.Add(apps);
            //    apps.Show();
            //}
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
    }
}
