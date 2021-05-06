using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassia;

namespace HouseHQ_server
{
    class remoteApp_Management
    {
        public void killProcess(List<string> namePc, List<string> processToKill)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                while (true)
                {
                    foreach (ITerminalServicesSession session in server.GetSessions())
                    {
                        if (namePc.Contains(session.ClientName))
                        {
                            foreach (var proc in session.GetProcesses())
                            {
                                if (processToKill.Contains(proc.ProcessName))
                                {
                                    try
                                    {
                                        proc.Kill();
                                    }
                                    catch
                                    {
                                        killProcess(namePc, processToKill);
                                    }
                                }
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
        public int getSessionId(string namePc)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.ClientName == namePc && session.ClientName.ToString() != "")
                    {
                        return session.SessionId;
                    }
                }
            }
            return 0;
        }

        public void sentMsg(int sessionId, string msg)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                try
                {
                    server.GetSession(sessionId).MessageBox(msg);
                }
                catch
                { 
                }
            }
        }

        public void logOff(int sessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                try
                {
                    server.GetSession(sessionId).Logoff();
                }
                catch
                { 
                }
            }
        }

        public bool theUserIsConnect(int sessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                try
                {
                    if (server.GetSession(sessionId).ToString() != "")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public string getLastInputTime(int sessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                try
                {
                    return server.GetSession(sessionId).LastInputTime.ToString();
                }
                catch
                {
                    return "";
                }
                
            }  
        }

        public void logOffAllUsers()
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.ClientName.ToString() != "")
                    {
                        session.Logoff();
                    }
                }
            }
        }

        public List<string> getAllClientName()
        {
            List<string> ans = new List<string>();
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.ClientName.ToString() != "")
                    {
                        ans.Add(session.ClientDisplay.ToString());
                    }
                }
            }
            return ans;
        }

        public void disconnectInactiveUsers()
        {
            string time;
            foreach (var user in getAllClientName())
            {
                time = getLastInputTime(getSessionId(user));
                if (time != "")
                {
                    if (userIsOff(DateTime.ParseExact(time, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture)))
                    {
                        sentMsg(getSessionId(user), "Inactivity you are disconnected");
                        System.Threading.Thread.Sleep(50);
                        logOff(getSessionId(user));
                    }
                }
            }

        }

        public bool userIsOff(DateTime userLastInputTime)
        {
            DateTime now = DateTime.Now;

            if ((now - userLastInputTime).Minutes > 5 || (now - userLastInputTime).Hours >= 1 || (now - userLastInputTime).Days >= 1)
            {
                return true;
            }
            return false;
        }
    }
}
