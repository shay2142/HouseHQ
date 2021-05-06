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
    public partial class frmChangeAccount : Form
    {
        public hash hashPass = new hash();

        public string IP;
        public ComboBox comboUsers;
        public string UserName;
        public string type;

        public frmChangeAccount(string ip, string userName, string key, string type)
        {
            InitializeComponent();
            IP = ip;
            this.type = type;

            if (type == "user")
            {
                createTxtUsername();
                
                checkbxAdmin.Enabled = false;
                txtUsername.Enabled = false;
                txtUsername.Text = userName;

                if (key == "admin")
                {
                    checkbxAdmin.Checked = true;
                }

                UserName = txtUsername.Text;
                sendGetUserInformation(txtUsername.Text);
            }
            else if (type == "manager")
            {
                createComboText();

                httpClient testLogin = new httpClient();
                string result = testLogin.sent(null, IP, "111");
                if (result != null)
                {
                    string[] results = result.Split('&');
                    if (results[0] == "211")
                    {
                        var users = JsonConvert.DeserializeObject<getAllUsers>(results[1]);
                        foreach (var user in users.usersList)
                        {

                            comboUsers.Items.Add(user);
                        }
                    }
                }
            }
        }

        public void sendGetUserInformation(string userName)
        {
            getUserInformation msg = new getUserInformation()
            {
                userName = userName
            };
            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, IP, "112");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "212")
                {
                    var user = JsonConvert.DeserializeObject<userInformation>(results[1]);
                    if (user.key == "admin")
                    {
                        checkbxAdmin.Checked = true;
                    }
                    else
                    {
                        checkbxAdmin.Checked = false;
                    }
                    txtMail.Text = user.mail;
                    if (type == "manager")
                    {
                        oldPass.Text = user.password;
                    }
                }
            }
            UserName = userName;
        }

        private void createComboText()
        {
            comboUsers = new ComboBox();
            comboUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            comboUsers.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            comboUsers.Location = new System.Drawing.Point(38, 102);
            comboUsers.Name = "comboUsers";
            comboUsers.Size = new System.Drawing.Size(216, 28);
            comboUsers.TabIndex = 2;
            comboUsers.SelectedIndexChanged += new System.EventHandler(this.comboUsers_Click);
            Controls.Add(comboUsers);
        }

        private void createTxtUsername()
        {
            txtUsername = new TextBox();
            txtUsername.BackColor = Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("MS UI Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            txtUsername.Location = new Point(38, 102);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(216, 28);
            txtUsername.TabIndex = 2;
            Controls.Add(txtUsername);
        }

        private void comboUsers_Click(object sender, EventArgs e)
        {
            sendGetUserInformation(comboUsers.Text);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string level = null;
            if (txtPassword.Text == txtComPassword.Text)
            {
                if(checkbxAdmin.Checked)
                {
                    level = "admin";
                }
                changeAccount test = new changeAccount()
                {
                   userName = UserName,
                   oldPassword = hashPass.ComputeSha256Hash(oldPass.Text),
                   newPassword = hashPass.ComputeSha256Hash(txtPassword.Text),
                   mail = txtMail.Text,
                   level = level
                };

                string json = JsonConvert.SerializeObject(test);

                httpClient testLogin = new httpClient();
                string result = testLogin.sent(json, IP, "103");
                if (result != null)
                {
                    string[] results = result.Split('&');

                    if (results[0] == "203")
                    {
                        MessageBox.Show("The details have changed successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (results[0] == "400")
                    {
                        var user = JsonConvert.DeserializeObject<error>(results[1]);
                        MessageBox.Show(user.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtPassword.Text = "";
            txtComPassword.Text = "";
            txtMail.Text = "";
            oldPass.Text = "";

            if (type == "user")
            { 
                txtUsername.Text = "";
                txtUsername.Focus();
            }
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
