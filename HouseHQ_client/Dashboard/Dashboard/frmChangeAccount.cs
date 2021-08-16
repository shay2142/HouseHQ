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
    public partial class frmChangeAccount : Form
    {
        public hash hashPass = new hash();
        public loginParameters USER = new loginParameters();

        public ComboBox comboUsers;
        public string type;
        public string UserName;

        public frmChangeAccount(loginParameters userPram, string type)
        {
            InitializeComponent();
            USER = userPram;
            this.type = type;
            UserName = USER.userName;

            if (type == "user")
            {
                createTxtUsername();
                
                checkbxAdmin.Enabled = false;
                txtUsername.Enabled = false;
                txtUsername.Text = UserName;

                if (USER.key == "admin")
                {
                    checkbxAdmin.Checked = true;
                }

                sendGetUserInformation(UserName);
            }
            else if (type == "manager")
            {
                createComboText();

                httpClient testLogin = new httpClient();
                string result = testLogin.sent(null, USER.ipServer, "111", USER.userName, hashPass.ComputeSha256Hash(USER.password));
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
                    else if (results[0] == "404")
                    {
                        new frmLogin().Show();
                        USER.dash.Hide();
                        this.Hide();
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
            string result = testLogin.sent(json, USER.ipServer, "112", USER.userName, hashPass.ComputeSha256Hash(USER.password));
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
                else if (results[0] == "404")
                {
                    new frmLogin().Show();
                    USER.dash.Hide();
                    this.Hide();
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
            string oldPassword = hashPass.ComputeSha256Hash(oldPass.Text);
            if (type == "manager")
            {
                oldPassword = oldPass.Text;
            }
            if (txtPassword.Text == txtComPassword.Text)
            {
                if(checkbxAdmin.Checked)
                {
                    level = "admin";
                }
                changeAccount test = new changeAccount()
                {
                   userName = UserName,
                   oldPassword = oldPassword,
                   newPassword = hashPass.ComputeSha256Hash(txtPassword.Text),
                   mail = txtMail.Text,
                   level = level
                };

                string json = JsonConvert.SerializeObject(test);

                httpClient testLogin = new httpClient();
                string result = testLogin.sent(json, USER.ipServer, "103", USER.userName, hashPass.ComputeSha256Hash(USER.password));
                if (result != null)
                {
                    string[] results = result.Split('&');

                    if (results[0] == "203")
                    {
                        login login = new login()
                        {
                            name = UserName,
                            password = txtPassword.Text
                        };
                        string json2 = JsonConvert.SerializeObject(login);
                        string result2 = testLogin.sent(json2, USER.ipServer, "132", USER.userName, hashPass.ComputeSha256Hash(USER.password));
                        if (result2 != null)
                        {
                            string[] results2 = result2.Split('&');
                            if (results2[0] == "232")
                            {
                                MessageBox.Show(JsonConvert.DeserializeObject<msg>(results2[1]).message, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (results2[0] == "400")
                            {
                                MessageBox.Show(JsonConvert.DeserializeObject<error>(results2[1]).msg, "Create User Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        MessageBox.Show("The details have changed successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (results[0] == "400")
                    {
                        var user = JsonConvert.DeserializeObject<error>(results[1]);
                        MessageBox.Show(user.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
