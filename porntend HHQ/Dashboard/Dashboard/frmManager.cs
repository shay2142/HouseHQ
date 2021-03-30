using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class frmManager : Form
    {
        public string IP;
        public frmManager(string ip, string userName)
        {
            InitializeComponent();
            IP = ip;
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
