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

using HTTP_CLIENT;
using System.Net;

namespace login_and_Register_System
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        public bool checkInput()
        {
            IPAddress ip;
            httpClient http = new httpClient();//Test

            if (IP.Text != "" && txtpassword.Text != "" && txtUsername.Text != "")
            {
                return true;
            }
            return false;
        }
        private void  button1_Click(object sender, EventArgs e)
        {
            string ip = IP.Text;
            var splitList = IP.Text.Split(':');
            if (splitList.Length == 2)
            {
                ip = splitList[0];
            }
            if (!checkInput())
            {
                MessageBox.Show("Invalid username or password or IP", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            login test = new login()
            {
                name = txtUsername.Text,
                password = txtpassword.Text
            };
            string json = JsonConvert.SerializeObject(test);
            //httpClient shay = new httpClient(json);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, testLogin.hostToIp(ip), "101");
            //string result = await msg(json, IP.Text, "101");
            if (result != null)
            {
                string[] results = result.Split('&');
                //MessageBox.Show(user.name, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                //MessageBox.Show(results[1], "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (results[0] == "201")
                {
                    var user = JsonConvert.DeserializeObject<okLogin>(results[1]);
                    new dashboard(results[1], IP.Text).Show();
                    this.Hide();
                }
                else if (results[0] == "400")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    MessageBox.Show(user.msg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            //var data = new StringContent("101&" + json, Encoding.UTF8, "application/json");
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

        private void label6_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
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
