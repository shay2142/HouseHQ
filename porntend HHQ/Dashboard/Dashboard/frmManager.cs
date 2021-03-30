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
        public frmManager(string ip)
        {
            InitializeComponent();
            IP = ip;
        }

        private void btnAddU_Click(object sender, EventArgs e)
        {
            frmRegister form = new frmRegister(IP);
            form.Show();
        }
    }
}
