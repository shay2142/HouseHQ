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

namespace Dashboard
{
    public partial class Form1 : Form
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
        public List<string> apps;
        public string userName;
        public string key;

        public Form1(string result, string ip)
        {
            InitializeComponent();
            this.IP_server = ip;
            var user = JsonConvert.DeserializeObject<okLogin>(result);
            apps = user.appList;
            userName = user.name;
            key = user.key;
            label1.Text = user.name;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.BackColor = Color.FromArgb(24, 30, 54);

            lbltitle.Text = "HouseHQ";

            if (user.key != "admin")
            {
                panel1.Controls.Remove(btnManager);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDashbord_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnApps.Height;
            pnlNav.Top = btnApps.Top;
            pnlNav.Left = btnApps.Left;
            btnApps.BackColor = Color.FromArgb(46, 51, 73);
            pnlNav.BackColor = Color.FromArgb(0, 126, 249);

            lbltitle.Text = "Apps";
            this.pnlFormLoader.Controls.Clear();
            frmApps frmDashboard_vrb = new frmApps(IP_server, userName, this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmDashboard_vrb.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmDashboard_vrb);
            frmDashboard_vrb.Show();

        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnManager.Height;
            pnlNav.Top = btnManager.Top;
            btnManager.BackColor = Color.FromArgb(46, 51, 73);
            pnlNav.BackColor = Color.FromArgb(0, 126, 249);

            lbltitle.Text = "Manager";
            this.pnlFormLoader.Controls.Clear();
            frmManager frmAnalytics_vrb = new frmManager(IP_server, userName) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmAnalytics_vrb.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmAnalytics_vrb);
            frmAnalytics_vrb.Show();
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnContactUs.Height;
            pnlNav.Top = btnContactUs.Top;
            btnContactUs.BackColor = Color.FromArgb(46, 51, 73);
            pnlNav.BackColor = Color.FromArgb(0, 126, 249);

            this.pnlFormLoader.Controls.Clear();
            frmContactUs frmContactUs_vrb = new frmContactUs() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmContactUs_vrb.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmContactUs_vrb);
            frmContactUs_vrb.Show();
            lbltitle.Text = "Contact Us";
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnsettings.Height;
            pnlNav.Top = btnsettings.Top;
            btnsettings.BackColor = Color.FromArgb(46, 51, 73);
            pnlNav.BackColor = Color.FromArgb(0, 126, 249);

            this.pnlFormLoader.Controls.Clear();
            frmSettings frmSettings_vrb = new frmSettings(IP_server, userName, key) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmSettings_vrb.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(frmSettings_vrb);
            frmSettings_vrb.Show();
            lbltitle.Text = "Settings";
        }

        private void btnDashbord_Leave(object sender, EventArgs e)
        {
            btnApps.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnAnalytics_Leave(object sender, EventArgs e)
        {
            btnManager.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnContactUs.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnsettings_Leave(object sender, EventArgs e)
        {
            btnsettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            logout();
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void lbltitle_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            File.Delete(@"reamoteapp.rdp");
            new frmLogin().Show();
            logout();
            this.Hide();
        }

        public void logout()
        {
            logoutUser test = new logoutUser()
            {
                userName = userName
            };
            string json = JsonConvert.SerializeObject(test);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, IP_server, "109");
        }
    }
}
