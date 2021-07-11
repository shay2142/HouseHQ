using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace newHttpServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, "127.0.0.1", "133");
            if (result != null)
            {
                Console.WriteLine(result);
                string[] results = result.Split('&');
                if (results[0] == "233")
                {
                    var user = JsonConvert.DeserializeObject<img>(results[1]);
                    var data = Encoding.ASCII.GetString(user.data, 0, user.data.Length);
                    byte[] bitmapData = Convert.FromBase64String(data.Substring(0, data.Length));
                    Image img = byteArrayToImage(bitmapData);// Construct a bitmap from the button image resource.
                    Bitmap bitmap = new Bitmap(img);
                    bitmap.Save(@"D:\test\" + user.nameFile);
                }     
            }

            


        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }

    class img
    {
        public string nameFile { get; set; }
        public byte[] data { get; set; }
    }
}
