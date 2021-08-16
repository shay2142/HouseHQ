using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using dataBase;

namespace HouseHQ_server
{
    public partial class Form1 : Form
    {
        public hash hashPass = new hash();
        public httpServer Http;

        public Form1(httpServer http, string type)
        {
            InitializeComponent();

            Http = http;
            label1.Text = type;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createUser create = new createUser();

            if (txtUsername.Text == "" || txtPassword.Text == "" || txtComPassword.Text == "" || txtMail.Text == "")
            {
                MessageBox.Show("Fields are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtPassword.Text == txtComPassword.Text)
            {
                if (label1.Text == "create admin")
                {
                    Http.db.createAdminDefault(Http.con, txtUsername.Text, hashPass.ComputeSha256Hash(txtPassword.Text), txtMail.Text);
                    create.createUserOnWin(txtUsername.Text, txtPassword.Text);
                }
                else
                {
                    if (!Http.db.userNameIsExists(Http.con, txtUsername.Text))
                    {
                        Http.db.insertVluesToUsers(Http.con, txtUsername.Text, hashPass.ComputeSha256Hash(txtPassword.Text), txtMail.Text);

                        if (checkbxAdmin.Checked)
                        {
                            Http.db.updateUser(Http.con, txtUsername.Text, hashPass.ComputeSha256Hash(txtPassword.Text), null, null, "admin");
                        }

                        create.createUserOnWin(txtUsername.Text, txtPassword.Text);

                        Http.db.updateStatus(Http.con, txtUsername.Text, "offline");
                    }
                }
                MessageBox.Show("The details have changed successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //new Form2(Http).Show();
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

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
