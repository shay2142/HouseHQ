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

namespace Dashboard
{
    public partial class frmLogin : Form
    {
        public hash hashPass = new hash();

        public frmLogin()
        {
            InitializeComponent();
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

            if (!checkInput())
            {
                MessageBox.Show("Invalid username or password or IP", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            login test = new login()
            {
                name = txtUsername.Text,
                password = hashPass.ComputeSha256Hash(txtpassword.Text)
            };
            string json = JsonConvert.SerializeObject(test);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, ip, "101");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "201")
                {
                    var user = JsonConvert.DeserializeObject<okLogin>(results[1]);
                    new Form1(results[1], IP.Text).Show();
                    this.Hide();
                }
                else if (results[0] == "400")
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
