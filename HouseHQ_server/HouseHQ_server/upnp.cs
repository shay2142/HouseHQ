﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Management;
using NATUPNPLib;

namespace HouseHQ_server
{
    class upnp
    {
        /*


         input: 

         output:
         */
        public void createUpnp()
        {

            UPnPNAT NatMgr = new UPnPNAT();

            if (NatMgr == null)
            {
                Console.WriteLine("Initialization failed creating Windows UPnPNAT interface.");
                return;
            }

            IStaticPortMappingCollection mappings = NatMgr.StaticPortMappingCollection;
            if (mappings == null)
            {
                Console.WriteLine("No mappings found. Do you have a uPnP enabled router as your gateway ? ");
                return;
            }

            foreach (IStaticPortMapping pm in mappings)
            {
                if (pm.InternalClient == GetLocalIPAddress() && pm.Description == "HHQ")
                {
                    mappings.Remove(pm.ExternalPort, pm.Protocol);
                }
            }
            addUpnp(mappings);
        }

        /*


         input: 

         output:
         */
        public void addUpnp(IStaticPortMappingCollection mappings)
        { 
            Random rnd = new Random();
            try
            {
                mappings.Add(rnd.Next(1000, 65535), "TCP", 3389, GetLocalIPAddress(), true, "HHQ");
            }
            catch
            {
                addUpnp(mappings);
            }
        }

        /*


         input: 

         output:
         */
        public int getRdpPort()
        {
            UPnPNAT NatMgr = new UPnPNAT();
            IStaticPortMappingCollection mappings = NatMgr.StaticPortMappingCollection;
            foreach (IStaticPortMapping pm in mappings)
            {
                if (pm.InternalClient == GetLocalIPAddress() && pm.Description == "HHQ")
                {
                    return pm.ExternalPort;
                }
            }
            return -1;
        }

        /*


         input: 

         output:
         */
        public string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        /*


         input: 

         output:
         */
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection nics = mc.GetInstances();
            foreach (ManagementObject nic in nics)
            {
                if (Convert.ToBoolean(nic["ipEnabled"]) == true && nic["DefaultIPGateway"] != null)
                {
                    string mac = nic["MacAddress"].ToString(); // Mac address
                    string ip = (nic["IPAddress"] as String[])[0]; // IP address
                    string ipsubnet = (nic["IPSubnet"] as String[])[0]; // Subnet mask
                    string ipgateway = (nic["DefaultIPGateway"] as String[])[0]; // Default gateway

                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
