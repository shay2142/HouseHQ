using System;
using System.Net.Sockets;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace client
{
    // Define a class to store values to be converted to JSON  
    class login
    {
        // Make sure all class attributes have relevant getter setter.
        public int code { get; set; }

        public string name { get; set; }

        public string password { get; set; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            login testLogin = new login()
            {
                code = 101,
                name = "shay",
                password ="12345"
            };

            string stringjson = JsonConvert.SerializeObject(testLogin);

            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Connect("localhost", stringjson);
            //}).Start();
            Connect("localhost", stringjson);

            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Connect("192.168.0.194", "Hello I'm Device 2...");
            //}).Start();
            Console.ReadLine();
        }   
        static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                int count = 0;

                // Translate the Message into ASCII.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);
                // Bytes Array to receive Server Response.
                data = new Byte[256];
                String response = String.Empty;
                // Read the Tcp Server Response Bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", response);
                //Thread.Sleep(2000);

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }
    }
}