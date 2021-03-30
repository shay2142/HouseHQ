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
        public List<getDB> DB { get; set; }

        public frmManager(string ip, string userName)
        {
            InitializeComponent();
            IP = ip;
            DB = GetDB();
            dataGridView1.DataSource = DB;
            //dataGridView1.add
        }

        public List<getDB> GetDB()
        {
            var list = new List<getDB>();
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, testLogin.hostToIp(IP), "113");
            if (result != null)
            {
                string[] results = result.Split('&');
                //MessageBox.Show(results[1], "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (results[0] == "213")
                {
                    var user = JsonConvert.DeserializeObject<jsonSentDB>(results[1]);
                    list = user.db;
                }
            }
            return list;
        }
        private void btnAddU_Click(object sender, EventArgs e)
        {
            frmRegister form = new frmRegister(IP);
            form.Show();
        }

        private void btnChangeDet_Click(object sender, EventArgs e)
        {
            new frmChangeAccount(IP, "", "admin", "manager").Show();
        }
    }
}
