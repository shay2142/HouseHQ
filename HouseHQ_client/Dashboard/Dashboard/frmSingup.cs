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
    public partial class frmRegister : Form
    {
        public loginParameters USER = new loginParameters();
        public hash hashPass = new hash();

        public frmUserManagement manager;

        public frmRegister(loginParameters user, frmUserManagement window)
        {
            InitializeComponent();
            USER = user;
            manager = window;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string level = "";
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtComPassword.Text == "" || txtMail.Text == "")
            {
                MessageBox.Show("Fields are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtPassword.Text == txtComPassword.Text)
            {
                if(checkbxAdmin.Checked)
                {
                    level = "admin";
                }
                singup test = new singup()
                {
                    name = txtUsername.Text,
                    password = hashPass.ComputeSha256Hash(txtPassword.Text),
                    mail = txtMail.Text,
                    key = level
                };
                string json = JsonConvert.SerializeObject(test);
                httpClient testLogin = new httpClient();
                string result = testLogin.sent(json, USER.ipServer, "102", USER.userName, hashPass.ComputeSha256Hash(USER.password));
                if (result != null)
                {
                    string[] results = result.Split('&');

                    if (results[0] == "202")
                    {
                        login login = new login()
                        {
                            name = txtUsername.Text,
                            password = txtPassword.Text
                        };
                        string json2 = JsonConvert.SerializeObject(login);
                        string result2 = testLogin.sent(json2, USER.ipServer, "131", USER.userName, hashPass.ComputeSha256Hash(USER.password));
                        if (result2 != null)
                        {
                            string[] results2 = result2.Split('&');
                            if (results2[0] == "231")
                            {
                                MessageBox.Show(JsonConvert.DeserializeObject<msg>(results2[1]).message, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (results2[0] == "400")
                            { 
                                MessageBox.Show(JsonConvert.DeserializeObject<error>(results2[1]).msg, "Create User Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (results2[0] == "404")
                            {
                                new frmLogin().Show();
                                USER.dash.Hide();
                                this.Hide();
                            }
                        }
                        MessageBox.Show("The details have changed successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        manager.GetDB();
                    }
                    else if (results[0] == "400")
                    {
                        var user = JsonConvert.DeserializeObject<error>(results[1]);
                        MessageBox.Show(user.msg, "Singup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (results[0] == "404")
                    {
                        new frmLogin().Show();
                        USER.dash.Hide();
                        this.Hide();
                    }
                }

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
            txtMail.Text = "";
            txtUsername.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
