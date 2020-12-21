using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            //Bitmap bm = new Bitmap("Picture.jpg");
            //IntPtr hBitmap = bm.GetHbitmap();
            appList.Items.Add("calculator");
            //appList.SmallImageList.Images.Add(Bitmap.FromFile(@"C:\Users\shay5\anaconda3\pkgs\pywin32-227-py38he774522_1\Lib\site-packages\win32\Demos\images\frowny.bmp"));
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void appList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //appList.Items.Add("List item text", 3);
            System.Diagnostics.Process.Start(@"C:\Users\shay5\Desktop\Calculator.rdp");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //foreach (var process in Process.GetProcessesByName("mstsc"))
            //{
            //    process.Kill();
            //}
            foreach (var process in Process.GetProcessesByName("mstsc"))
            {
                process.Kill();
            }
        }
    }
}
