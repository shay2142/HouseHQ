using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MSTSCLib;

namespace WindowsFormsApp1
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

       
        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            /*string txtServer = "192.168.0.134";
            try
            {
                // Check if connected before disconnecting
                if (axMsRdpClient81.Connected.ToString() == "1")
                    axMsRdpClient81.FullScreen = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error Disconnecting", "Error disconnecting from remote desktop " + txtServer + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
            
            Form2 form = new Form2(ipServer.Text, user.Text, pass.Text);
            this.Hide();
            form.Show();

        }

        private void axMsRdpClient81_OnConnecting(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
        {
            pass.PasswordChar = '*';
        }
    }
}
