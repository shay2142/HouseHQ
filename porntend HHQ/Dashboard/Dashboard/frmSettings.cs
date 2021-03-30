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
        public string ip;
        public string userName;
        public string key;

        public frmSettings(string ip, string userName, string key)
        {
            InitializeComponent();

            this.ip = ip;
            this.userName = userName;
            this.key = key;
        }

        private void btnChangeDet_Click(object sender, EventArgs e)
        {
            new frmChangeAccount(ip, userName, key, "user").Show();
        }
    }
}
