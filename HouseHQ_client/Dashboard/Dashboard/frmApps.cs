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

using System.IO;
using HTTP_CLIENT;

namespace Dashboard
{
    public partial class frmApps : Form
    {
        public Form1 dashbord;

        public loginParameters USER = new loginParameters();
        public hash hashPass = new hash();

        public frmApps(loginParameters user, Form1 form)
        {
            InitializeComponent();

            USER = user;
            dashbord = form;

            getData();
        }

        public void getData()
        {
            getUserInformation msg = new getUserInformation()
            {
                userName = USER.userName
            };
            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, USER.ipServer, "114", USER.userName, hashPass.ComputeSha256Hash(USER.password));
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "214")
                {
                    var user = JsonConvert.DeserializeObject<getAllApps>(results[1]);
                    USER.apps = user.allAppList;
                }
                else if (results[0] == "404")
                {
                    new frmLogin().Show();
                    USER.dash.Hide();
                    this.Hide();
                }
            }

            foreach (var app in USER.apps)
            {
                Button test = new Button();
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
                //test.Image = Image.FromFile(@"C:\Users\shay5\Documents\househq\HouseHQ_files\porntend HHQ\login_and_Register_System (2)\login and Register System\login and Register System\img\icons8-visual-studio-2019-48.png");
                getImg msg2 = new getImg()
                {
                    appName = app
                };
                string result1 = testLogin.sent(JsonConvert.SerializeObject(msg2), USER.ipServer, "133", USER.userName, hashPass.ComputeSha256Hash(USER.password));

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
                
                test.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                test.UseVisualStyleBackColor = true;
                test.UseVisualStyleBackColor = true;
                test.Click += new System.EventHandler(reamoteApp);
                flowLayoutPanel1.Controls.Add(test);
            }
        }

        public void createReamoteAppFile(string remoteAppName, string ip, string port)
        {
            string path = @"C:\Users\Public\reamoteapp.rdp";
            File.Delete(path);

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("allow desktop composition:i:1");
                    sw.WriteLine("allow font smoothing:i:1");
                    sw.WriteLine("alternate full address:s:" + ip + ":" + port);
                    sw.WriteLine("alternate shell:s:rdpinit.exe");
                    sw.WriteLine("devicestoredirect:s:*");
                    sw.WriteLine("full address:s:" + ip + ":" + port);
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

            runApp msg = new runApp()
            {
                userName = USER.userName,
                password = hashPass.ComputeSha256Hash(USER.password),
                app = button.Text
            };
            string json = JsonConvert.SerializeObject(msg);

            httpClient runApp = new httpClient();
            string result = runApp.sent(json, USER.ipServer, "130", USER.userName, hashPass.ComputeSha256Hash(USER.password));
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "230")
                {
                    var user = JsonConvert.DeserializeObject<okRunApp>(results[1]);
                    createReamoteAppFile(user.app, user.ip, user.port);
                }
                else if (results[0] == "400")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    MessageBox.Show(user.msg, "Run app Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (results[0] == "404")
                {
                    new frmLogin().Show();
                    USER.dash.Hide();
                    this.Hide();
                }
            }
            
        }
        private void frmApps_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FrmAddApps(USER, this, dashbord).Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FrmDeleteApps(USER, this, dashbord).Show();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
