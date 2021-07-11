using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;

namespace server
{
    class Server
    {
        TcpListener server = null;
        public Server(int port)
        {
            //IPAddress localAddr = IPAddress.Parse(ip);
            //IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            server = new TcpListener(ipAddr, port);
            server.Start();
            StartListener();
        }
        public void StartListener()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Stop();
            }
        }

        public byte[] sentImg()
        {
            //Use graphics to send the image not the file!
            // Send the data through the socket.    
            Image img = Image.FromFile(@"D:\HHQ.jpg");

            using (MemoryStream m = new MemoryStream())
            {
                img.Save(m, img.RawFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                byte[] base64Bytes = Encoding.ASCII.GetBytes(base64String);
                //Console.WriteLine(base64String);
                //Console.WriteLine("<EOF>");
                return base64Bytes;

            }
        }

        public void HandleDeivce(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string imei = String.Empty;
            string data = null;
            Byte[] bytes = new Byte[256];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);
                    var user = System.Text.Json.JsonSerializer.Deserialize<login>(data);
                    Console.WriteLine(user.name);

                    var imgg = sentImg();

                    img test = new img()
                    {
                        data = imgg,
                         nameFile = @"D:\HHQ.jpg"
                    };

                    string str = JsonConvert.SerializeObject(test);
                    Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);
                    stream.Write(reply, 0, reply.Length);
                    Console.WriteLine("{1}: Sent: {0}", str, Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }
    }
    class login
    {
        // Make sure all class attributes have relevant getter setter.
        public int code { get; set; }

        public string name { get; set; }

        public string password { get; set; }
    }
    class ok
    {
        // Make sure all class attributes have relevant getter setter.
        public int code { get; set; }

        public string name { get; set; }

        public List<string> appList { get; set; }

        public string key { get; set; }

        public string img { get; set; }
    }

    class img
    {
        public string nameFile { get; set; }
        public byte[] data { get; set; }
    }
}
