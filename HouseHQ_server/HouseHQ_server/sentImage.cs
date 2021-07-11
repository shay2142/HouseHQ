using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Windows.Forms;
using TsudaKageyu;
using System.Drawing.Imaging;



namespace HouseHQ_server
{
    public static class sentImage
    {
        public static byte[] sentImg(string pathImg)
        {
            //Use graphics to send the image not the file!
            // Send the data through the socket.    
            
            //Image img = Image.FromFile(@"D:\test.jpg");
            Image img = Image.FromFile(@".\appImg\" + Path.GetFileNameWithoutExtension(pathImg) + ".png");



            using (MemoryStream m = new MemoryStream())
            {
                img.Save(m, ImageFormat.Png);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                byte[] base64Bytes = Encoding.ASCII.GetBytes(base64String);

                return base64Bytes;
            }
        }

        public static void getIconFromExe(string path, string nameApp)
        {
            String fileName = path;
            Int32 index = 0;

            Icon icon = null;
            Icon[] splitIcons = null;
            if(!File.Exists(@".\appImg\" + nameApp + ".png"))
            { 
                try
                {
                    if (Path.GetExtension(fileName).ToLower() == ".ico")
                    {
                        icon = new Icon(fileName);
                    }
                    else
                    {
                        var extractor = new IconExtractor(fileName);
                        icon = extractor.GetIcon(index);
                    }

                    splitIcons = IconUtil.Split(icon);

                    using (FileStream stream = new FileStream(@".\appImg\" + nameApp + ".png", FileMode.CreateNew))
                    {
                        splitIcons[getIndexForMaxSizeFile(splitIcons)].Save(stream);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex);
                    return;
                }
            }
        }

        public static int getIndexForMaxSizeFile(Icon[] splitIcons)
        {

            int max = 0;
            for (int i = 0; i < splitIcons.Length; i++)
            {
                //if (splitIcons[i].Size.Width > splitIcons[max].Size.Width && splitIcons[i].Size.Height > splitIcons[max].Size.Height)
                //{
                //    max = i;
                //}

                if (splitIcons[i].Size.Width >= 48 && splitIcons[i].Size.Height >= 48)
                {
                    return i;
                }
            }
            return max;
        }
    }
}
