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

namespace HouseHQ_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void path_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var filename = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Programs (*.exe, *.com, *.cmd, *.bat)| *.exe;*.com;*.cmd;*.bat|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                namePath.Text = filePath;
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();
 
            }

            filename = Path.GetFileNameWithoutExtension(filePath);
            MessageBox.Show(fileContent, "File Content at path: " + filename, MessageBoxButtons.OK);
        }

        private void createRemoteApp_Click(object sender, EventArgs e)
        {
            //var i;
            //System.Diagnostics.Process.Start("reg.exe", @"ADD \\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + Path.GetFileNameWithoutExtension(namePath.Text));
            //MessageBox.Show(fileContent, "File Content at path: " + filename, MessageBoxButtons.OK);
        }
    }
}
