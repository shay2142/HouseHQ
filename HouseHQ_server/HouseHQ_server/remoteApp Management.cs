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
        public int getSession(string namePc)
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
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.SessionId == sessionId && session.ClientName.ToString() != "")
                    {
                        session.MessageBox(msg);
                    }
                }
            }
        }

        public void logOff(int sessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.SessionId == sessionId && session.ClientName.ToString() != "")
                    {
                        session.Logoff();
                    }
                }
            }
        }

        public bool theUserIsConnect(int sessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.SessionId == sessionId && session.ClientName.ToString() != "")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string getLastInputTime(int sessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.SessionId == sessionId && session.ClientName.ToString() != "")
                    {
                        return session.LastInputTime.ToString();
                    }
                }
            }
            return "";
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
    }
}
