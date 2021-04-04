using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace HouseHQ_server
{
    public partial class Form2 : Form
    {
        public httpServer Http;
        public Form2(httpServer http)
        {
            InitializeComponent();

            Http = http;

            GetLocalIPAddress();
        }

        public void GetLocalIPAddress()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                richTextBox1.AppendText(adapter.Description + ":\n\n");
                foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        richTextBox1.AppendText("  IPv4 Address .............................. : " + ip.Address.ToString() + "\n");
                        richTextBox1.AppendText("  Listening for connections on .. : http://" + ip.Address.ToString() + ":8080/" + "\n\n");
                    }
                }
            }
            Console.WriteLine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AddApps(Http).Show();
        }
    }
}
