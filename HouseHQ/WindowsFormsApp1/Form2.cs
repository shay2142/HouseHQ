using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {

        public Form2(string server, string userName, string password)
        {
            InitializeComponent();
            this.axMsRdpClient81.Size = new System.Drawing.Size(Form2.ActiveForm.ClientSize.Width, Form2.ActiveForm.ClientSize.Height - 25);
            try
            {


                axMsRdpClient81.Server = server; //IP address of remote machine
                axMsRdpClient81.UserName = userName;
                axMsRdpClient81.AdvancedSettings9.ClearTextPassword = password;
                axMsRdpClient81.AdvancedSettings9.AuthenticationLevel = 2;
                axMsRdpClient81.AdvancedSettings9.EnableCredSspSupport = true;
                axMsRdpClient81.AdvancedSettings9.NegotiateSecurityLayer = false;

                axMsRdpClient81.AdvancedSettings8.RelativeMouseMode = true;
                axMsRdpClient81.AdvancedSettings.BitmapPeristence = 1;
                axMsRdpClient81.AdvancedSettings.Compress = 1;
                axMsRdpClient81.AdvancedSettings8.SmartSizing = true;
                axMsRdpClient81.DesktopHeight = Screen.PrimaryScreen.Bounds.Height;
                axMsRdpClient81.DesktopWidth = Screen.PrimaryScreen.Bounds.Width;
                axMsRdpClient81.FullScreen = false;
                axMsRdpClient81.ColorDepth = 15; //or 32 bit color

                axMsRdpClient81.AdvancedSettings8.RedirectDrives = false;
                axMsRdpClient81.AdvancedSettings8.RedirectPrinters = true;
                axMsRdpClient81.AdvancedSettings8.RedirectClipboard = true;
                axMsRdpClient81.AdvancedSettings8.RedirectSmartCards = false;

                //axMsRdpClient81.AdvancedSettings8.ConnectToServerConsole //admin 
                //button
                axMsRdpClient81.AdvancedSettings8.ConnectionBarShowMinimizeButton = true;
                axMsRdpClient81.AdvancedSettings8.ConnectionBarShowPinButton = true;
                axMsRdpClient81.AdvancedSettings8.ConnectionBarShowRestoreButton = true;

                axMsRdpClient81.AdvancedSettings8.DisableCtrlAltDel = 0;

                //axMsRdpClient81.AdvancedSettings8.DisplayConnectionBar = false; //מבטל את הפס הכחול

                //axMsRdpClient81.AdvancedSettings8.EnableWindowsKey = 1;//Enable winkey

                /****************
                 *****hotkey*****
                 ****************/

                //axMsRdpClient81.AdvancedSettings8.KeyboardFunctionKey

                //axMsRdpClient81.AdvancedSettings8.MaximizeShell = 1;
                //axMsRdpClient81.SecuredSettings.StartProgram = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"; //StartProgram

                //axMsRdpClient81.AdvancedSettings8.MaxReconnectAttempts // מספר הפעמים לניסיון ההתחברות אוטומטי מחדש

                axMsRdpClient81.AdvancedSettings8.minInputSendInterval = 1; //מציין את המרווח המינימלי, באלפיות השנייה, בין שליחת אירועי עכבר.

                //axMsRdpClient81.AdvancedSettings8.NetworkConnectionType = 

                //axMsRdpClient81.RemoteProgram.RemoteProgramMode = true;

                /*****remote app no working!!!*****/
                axMsRdpClient81.OnConnected += axMsRdpClient81_OnConnecting;
                axMsRdpClient81.RemoteProgram.RemoteProgramMode = true;
                axMsRdpClient81.RemoteProgram2.RemoteApplicationName = "Calculator";
                axMsRdpClient81.RemoteProgram2.RemoteApplicationProgram = @"C:\Windows\system32\calc.exe";


                axMsRdpClient81.Connect();

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error Connecting", "Error connecting to remote desktop " + server + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void axMsRdpClient81_OnConnecting(object sender, EventArgs e)
        {
            this.axMsRdpClient81.Size = new System.Drawing.Size(Form2.ActiveForm.ClientSize.Width, Form2.ActiveForm.ClientSize.Height - 25);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            axMsRdpClient81.FullScreen = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string txtServer = "192.168.0.134";
            try
            {
                // Check if connected before disconnecting
                if (axMsRdpClient81.Connected.ToString() == "1")
                {
                    axMsRdpClient81.Disconnect();
                    this.Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error Disconnecting", "Error disconnecting from remote desktop " + txtServer + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
