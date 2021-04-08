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
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (txtUsername.Text == "" || txtSecurity.Text == "" || txtAuthorization.Text == "" || txtPassword.Text == "" || txtComPassword.Text == "" || txtIp.Text == "" || txtMail.Text == "")
            {
                MessageBox.Show("Fields are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtPassword.Text == txtComPassword.Text)
            {
                singup test = new singup()
                {
                    name = txtUsername.Text,
                    password = txtPassword.Text,
                    mail = txtMail.Text,
                    key = txtAuthorization.Text
                };
                string json = JsonConvert.SerializeObject(test);
                //httpClient shay = new httpClient(json);
                httpClient testLogin = new httpClient();
                string result = testLogin.sent(json, testLogin.hostToIp(txtIp.Text), "102");
                if (result != null)
                {
                    string[] results = result.Split('&');
                    //MessageBox.Show(results[1], "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (results[0] == "202")
                    {
                        var user = JsonConvert.DeserializeObject<okSingup>(results[1]);
                        new frmLogin().Show();
                        this.Hide();
                    }
                    else if (results[0] == "400")
                    {
                        var user = JsonConvert.DeserializeObject<error>(results[1]);
                        MessageBox.Show(user.msg, "Singup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //txtUsername.Text = "";
                //txtPassword.Text = "";
                //txtComPassword.Text = "";

                //MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Passwords does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtComPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtComPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtComPassword.PasswordChar = '•';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtComPassword.Text = "";
            txtAuthorization.Text = "";
            txtMail.Text = "";
            txtSecurity.Text = "";
            txtIp.Text = "";
            txtUsername.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            //label2.Text = kryptonDateTimePicker1.Value.ToShortDateString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
