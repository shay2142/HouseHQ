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
using System.Net.Http;

using HTTP_CLIENT;

namespace Dashboard
{
    public partial class frmManager : Form
    {
        public string IP;
        public string UserName;

        public frmManager(string ip, string userName)
        {
            InitializeComponent();
            IP = ip;
            UserName = userName;
            GetDB();
        }

        public void GetDB()
        {
            var list = new List<getDB>();
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, IP, "113");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "213")
                {
                    var user = JsonConvert.DeserializeObject<jsonSentDB>(results[1]);
                    list = user.db;
                }
            }
            dataGridView1.DataSource = list;
        }
        private void btnAddU_Click(object sender, EventArgs e)
        {
            frmRegister form = new frmRegister(IP, this);
            form.Show();
        }

        private void btnChangeDet_Click(object sender, EventArgs e)
        {
            new frmChangeAccount(IP, "", "admin", "manager").Show();
        }

        private void btnRemU_Click(object sender, EventArgs e)
        {
            frmDeleteUser form = new frmDeleteUser(IP, UserName, this);
            form.Show();
        }
    }
}
