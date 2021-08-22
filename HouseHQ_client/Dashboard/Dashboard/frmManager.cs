using System;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class frmManager : Form
    {
        public loginParameters USER = new loginParameters();
        public hash hashPass = new hash();
        public Form1 Form;

        public frmManager(loginParameters user, Form1 form)
        {
            InitializeComponent();
            USER = user;
            Form = form;
        }

        private void btnChangeDet_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form.pnlFormLoader.Controls.Clear();
            frmUserManagement form = new frmUserManagement(USER, Form) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form.FormBorderStyle = FormBorderStyle.None;
            Form.pnlFormLoader.Controls.Add(form);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmLogs_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form.pnlFormLoader.Controls.Clear();
            frmLogs form = new frmLogs(USER, Form) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form.FormBorderStyle = FormBorderStyle.None;
            Form.pnlFormLoader.Controls.Add(form);
            form.Show();
        }

        private void btnAddS_Click(object sender, EventArgs e)
        {
            frmAddAppToServer form = new frmAddAppToServer(USER);
            form.Show();
        }
    }
}
