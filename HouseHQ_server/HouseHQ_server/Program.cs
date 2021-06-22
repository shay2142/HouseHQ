using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Data.SQLite;
using System.Threading;

using jsonDeserialize;
using jsonSerializer;
using dataBase;
using NATUPNPLib;

namespace HouseHQ_server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            upnp upnp = new upnp();
            upnp.createUpnp();

            httpServer server2 = new httpServer();
            server2.runServer();
        }
    }
}
