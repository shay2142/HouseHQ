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

namespace Dashboard
{
    public partial class frmUserManagement : Form
    {
        public loginParameters USER = new loginParameters();
        public hash hashPass = new hash();
        public Form1 Form;

        public frmUserManagement(loginParameters user, Form1 form)
        {
            InitializeComponent();
            USER = user;
            Form = form;
            GetDB();
        }

        public void GetDB()
        {
            var list = new List<getDB>();
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, USER.ipServer, "113", USER.userName, hashPass.ComputeSha256Hash(USER.password));
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "213")
                {
                    var user = JsonConvert.DeserializeObject<jsonSentDB>(results[1]);
                    list = user.db;
                }
                else if (results[0] == "404")
                {
                    new frmLogin().Show();
                    USER.dash.Hide();
                    this.Hide();
                }
            }
            dataGridView1.DataSource = list;
        }
        private void btnAddU_Click(object sender, EventArgs e)
        {
            frmRegister form = new frmRegister(USER, this);
            form.Show();
        }

        private void btnChangeDet_Click(object sender, EventArgs e)
        {
            new frmChangeAccount(USER, "manager").Show();
        }

        private void btnRemU_Click(object sender, EventArgs e)
        {
            frmDeleteUser form = new frmDeleteUser(USER, this);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form.pnlFormLoader.Controls.Clear();
            frmManager form = new frmManager(USER, Form) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form.FormBorderStyle = FormBorderStyle.None;
            Form.pnlFormLoader.Controls.Add(form);
            form.Show();
        }
    }
}
