using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Microsoft.Win32;
using System.Diagnostics;
using System.Data.SQLite;
using dataBase;

namespace HouseHQ_server
{
    public partial class AddApps : Form
    {
        public httpServer Http;
        public AddApps(httpServer http)
        {
            InitializeComponent();
            Http = http;
        }

        private void createRemoteApp_Click(object sender, EventArgs e)
        {

            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + Path.GetFileNameWithoutExtension(namePath.Text)) == null)
            {
                RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + Path.GetFileNameWithoutExtension(namePath.Text));
                key.SetValue("Path", namePath.Text);
                key.Close();

                remoteApp app = new remoteApp();
                app.laodApp(Http);
            }


        }

        private void namePath_TextChanged(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var filename = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Programs (*.exe, *.com, *.cmd, *.bat)| *.exe;*.com;*.cmd;*.bat|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                namePath.Text = filePath;
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            namePath.Text = "";
            nameProg.Text = "";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //new Form2().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
