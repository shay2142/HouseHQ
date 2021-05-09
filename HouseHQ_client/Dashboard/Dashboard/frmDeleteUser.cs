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

using HTTP_CLIENT;

namespace Dashboard
{
    public partial class frmDeleteUser : Form
    {
        public string IP;
        public string adminUserName;
        public frmManager manager;

        public frmDeleteUser(string ip, string admin, frmManager window)
        {
            InitializeComponent();

            IP = ip;
            manager = window;
            adminUserName = admin;

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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboUsers.Text != "" || comboUsers.Text != null)
            {
                deleteUser msg = new deleteUser()
                {
                    adminUserName = adminUserName,
                    userNameDelete = comboUsers.Text
                };
                string json = JsonConvert.SerializeObject(msg);
                httpClient testLogin = new httpClient();
                string result = testLogin.sent(json, IP, "110");
                if (result != null)
                {
                    string[] results = result.Split('&');
                    if (results[0] == "400")
                    {
                        var err = JsonConvert.DeserializeObject<error>(results[1]);
                        MessageBox.Show(err.msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if(results[0] == "210")
                    {
                        MessageBox.Show("The details have changed successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        manager.GetDB();
                        new frmDeleteUser(IP, adminUserName, manager).Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("the combo is empty, try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
