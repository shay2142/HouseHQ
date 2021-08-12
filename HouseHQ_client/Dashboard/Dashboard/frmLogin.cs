using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.Net.Http;

using System.Net;

namespace Dashboard
{
    public partial class frmLogin : Form
    {
        public hash hashPass = new hash();

        public frmLogin()
        {
            InitializeComponent();

            //if (Properties.Settings.Default.userName != "" && Properties.Settings.Default.password != "" && Properties.Settings.Default.ipServer != "")
            //{
            //    login(Properties.Settings.Default.ipServer, Properties.Settings.Default.userName, Properties.Settings.Default.password);
            //}
        }

        public bool checkInput()
        {
            IPAddress ip;

            if (IP.Text != "" && txtpassword.Text != "" && txtUsername.Text != "")
            {
                return true;
            }
            return false;
        }
        private void  button1_Click(object sender, EventArgs e)
        {

            string ip = IP.Text;
            bool remember = false;

            if (!checkInput())
            {
                MessageBox.Show("Invalid username or password or IP", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (rememberBox.Checked)
            {
                Properties.Settings.Default.ipServer = ip;
                Properties.Settings.Default.userName = txtUsername.Text;
                Properties.Settings.Default.password = txtpassword.Text;
                Properties.Settings.Default.Save();
                remember = true;
            }

            login(ip, txtUsername.Text, txtpassword.Text, remember);


        }

        public void login(string ip, string userName, string password, bool remember)
        {
            loginParameters userPram = new loginParameters();

            login test = new login()
            {
                name = hashPass.ComputeSha256Hash(userName),
                password = hashPass.ComputeSha256Hash(password)
            };
            string json = JsonConvert.SerializeObject(test);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, ip, "101", userName, test.password);
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "201")
                {
                    var user = JsonConvert.DeserializeObject<okLogin>(results[1]);

                    userPram.ipServer = ip;
                    userPram.userName = user.name;
                    userPram.password = password;
                    userPram.mail = user.mail;
                    userPram.key = user.key;
                    userPram.img = user.img;
                    userPram.apps = user.appList;
                    userPram.remember = remember;

                    new Form1(userPram).Show();
                    this.Hide();
                }
                else if (results[0] == "400" || results[0] == "404")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    MessageBox.Show(user.msg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtpassword.Text = "";
            IP.Text = "";
            txtUsername.Focus();
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtpassword.PasswordChar = '\0';
                
            }
            else
            {
                txtpassword.PasswordChar = '•';
                
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
