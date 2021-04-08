using server;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;


class Program
{
    static void Main(string[] args)
    {
        Thread t = new Thread(delegate ()
        {
            // replace the IP with your system IP Address...
            Server myserver = new Server(13000);
        });
        t.Start();

        Console.WriteLine("Server Started...!");
    }
}