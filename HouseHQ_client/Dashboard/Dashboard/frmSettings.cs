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
    public partial class frmSettings : Form
    {
        public loginParameters USER = new loginParameters();

        public frmSettings(loginParameters user)
        {
            InitializeComponent();
            USER = user;
        }

        private void btnChangeDet_Click(object sender, EventArgs e)
        {
            new frmChangeAccount(USER, "user").Show();
        }
    }
}
