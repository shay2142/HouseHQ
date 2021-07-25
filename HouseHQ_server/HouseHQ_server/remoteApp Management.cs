using System;
using System.Collections.Generic;
using System.Text;
using Cassia;
using dataBase;
using System.Threading;

namespace HouseHQ_server
{
    class remoteApp_Management
    {
        public DB db = new DB();

        //לא עובד צריך לבדוק
        public ThreadStart remoteAppRunning(httpServer http)
        {
            createUser create = new createUser();
            List<int> listSessionId = new List<int>();
            while (true)
            {
                foreach (var user in getUser())
                {
                    //remote.disconnectInactiveUsers();
                    //if this user in DB
                    //Console.WriteLine(user);
                    //remote.sentMsg(remote.getSessionId(user), "test");
                    int sessionId = getSessionId(user, listSessionId);
                    if (user != "" && !userIsConnect(sessionId) && http.db.userNameIsExists(http.con, user))
                    {
                        logOff(sessionId);
                        //Console.WriteLine("logoff - " + user);
                    }
                    else if (user != "" && userIsConnect(sessionId) && http.db.getLevelKey(http.con, user) != "admin")
                    {
                        create.DiableADUserUsingUserPrincipal(user);
                    }
                }
                System.Threading.Thread.Sleep(60000);//דקה
            }

        }
        /*


        input: 

        output:
        */
        public void killProcess(List<string> usersName, List<string> processToKill)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                while (true)
                {
                    foreach (ITerminalServicesSession session in server.GetSessions())
                    {
                        if (usersName.Contains(session.UserName))
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
                                        killProcess(usersName, processToKill);
                                    }
                                }
                            }
                        }
                    }
                    //System.Threading.Thread.Sleep(10);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        /*


         input: 

         output:
         */
        public int getSessionId(string userName, List<int> listSessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.UserName == userName && session.UserName.ToString() != "" && !listSessionId.Contains(session.SessionId))
                    {
                        listSessionId.Add(session.SessionId);
                        return session.SessionId;
                    }
                }
            }
            return -1;
        }

        public int getSessionId(string userName)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    if (session.UserName == userName && session.UserName.ToString() != "")
                    {
                        return session.SessionId;
                    }
                }
            }
            return -1;
        }

        public List<string> getUser()
        {
            List<string> ans = new List<string>();

            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    ans.Add(session.UserName);
                    Console.WriteLine(session.UserName + " - " + session.ConnectionState);
                }
            }
            return ans;
        }

        /*


        input: 

        output:
        */
        public bool userIsConnect(int sessionId)
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetLocalServer())
            {
                server.Open();
                try
                {
                    if (server.GetSession(sessionId).ConnectionState.ToString() == "Active")
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        //public void disconnectInactiveUsers()
        //{
        //    string time;
        //    int sessionId;
        //    foreach (var user in getUser())
        //    {
        //        try
        //        {
        //            sessionId = getSessionId(user);
        //            time = getLastInputTime(sessionId);
        //            if (time != "")
        //            {
        //                if (userIsOff(DateTime.ParseExact(time, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture)))
        //                {
        //                    sentMsg(sessionId, "Inactivity you are disconnected");
        //                    System.Threading.Thread.Sleep(50);
        //                    logOff(sessionId);
        //                }
        //            }
        //        }
        //        catch
        //        { 
        //        }
                
        //    }
        //}

        /*


         input: 

         output:
         */
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
