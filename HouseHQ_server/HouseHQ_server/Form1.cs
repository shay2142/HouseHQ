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
        }

        private void createRemoteApp_Click(object sender, EventArgs e)
        {
            //create RemoteApp not workin with Program Files check it!
            ProcessStartInfo startInfo = new ProcessStartInfo("reg.exe", "Add " + '"' + @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + Path.GetFileNameWithoutExtension(namePath.Text) + '"' + @" /v Path /t REG_SZ /d " + namePath.Text);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            System.Diagnostics.Process.Start(startInfo);
        }
    }
}
