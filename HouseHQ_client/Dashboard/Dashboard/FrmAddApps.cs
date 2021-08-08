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

using Newtonsoft.Json;

using HTTP_CLIENT;

namespace Dashboard
{
    public partial class FrmAddApps : Form
    {
        public List<string> apps;

        public frmApps appsWindow { get; set; }
        public Form1 dashbord { get; set; }
        public loginParameters USER = new loginParameters();
        public hash Hash = new hash();

        public FrmAddApps(loginParameters users, frmApps window, Form1 window2)
        {
            InitializeComponent();

            USER = users;
            appsWindow = window;
            dashbord = window2;

            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, USER.ipServer, "105", USER.userName, Hash.ComputeSha256Hash(USER.password));
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "205")
                {
                    var user = JsonConvert.DeserializeObject<getAllApps>(results[1]);
                    apps = user.allAppList;
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

                getImg msg2 = new getImg()
                {
                    appName = app
                };
                string result1 = testLogin.sent(JsonConvert.SerializeObject(msg2), USER.ipServer, "133", USER.userName, Hash.ComputeSha256Hash(USER.password));

                if (result1 != null)
                {
                    //Console.WriteLine(result1);
                    string[] results1 = result1.Split('&');
                    if (results1[0] == "233")
                    {
                        var user = JsonConvert.DeserializeObject<img>(results1[1]);
                        var data = Encoding.ASCII.GetString(user.data, 0, user.data.Length);
                        byte[] bitmapData = Convert.FromBase64String(data.Substring(0, data.Length));
                        Image img = byteArrayToImage(bitmapData);// Construct a bitmap from the button image resource.
                        Bitmap bitmap = new Bitmap(img, new Size(48, 48));
                        test.Image = bitmap;
                    }
                    else if (results1[0] == "404")
                    {
                        new frmLogin().Show();
                        USER.dash.Hide();
                        this.Hide();
                    }
                }

                if (USER.apps.Contains(app))
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

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
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
                        userName = USER.userName,
                        appName = ((CheckBox)newApp).Text
                    };
                    string json = JsonConvert.SerializeObject(msg);
                    httpClient testLogin = new httpClient();
                    string result = testLogin.sent(json, USER.ipServer, "108", USER.userName, Hash.ComputeSha256Hash(USER.password));
                    if (result != null)
                    {
                        string[] results = result.Split('&');
                        if (results[0] == "400")
                        {
                            good = false;

                            MessageBox.Show("Something went wrong Try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (results[0] == "404")
                        {
                            new frmLogin().Show();
                            USER.dash.Hide();
                            this.Hide();
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
                frmApps apps = new frmApps(USER, dashbord) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                apps.FormBorderStyle = FormBorderStyle.None;
                dashbord.pnlFormLoader.Controls.Add(apps);
                apps.Show();
            }
        }
    }
}
