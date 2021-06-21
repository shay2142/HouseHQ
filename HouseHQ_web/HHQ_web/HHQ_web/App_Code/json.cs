using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHQ_web
{
    public class json
    {
    }

    public class okLogin
    {
        public string name { get; set; }
        public string mail { get; set; }
        public List<string> appList { get; set; }
        public string key { get; set; }
        public string img { get; set; }
    }

    class okSingup
    {
        public bool ok { get; set; }
    }

    class login
    {
        public string name { get; set; }
        public string password { get; set; }
    }

    class singup
    {
        public string name { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public string key { get; set; }
    }

    public class error
    {
        public string msg { get; set; }
    }

    public class getAllUsers
    {
        public List<string> usersList { get; set; }
    }

    public class userInformation
    {
        public string password { get; set; }
        public string mail { get; set; }
        public string key { get; set; }
    }

    class getUserInformation    
    {
        public string userName { get; set; }
    }

    class changeAccount
    {
        public string userName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string mail { get; set; }
        public string level { get; set; }
    }

    public class getDB
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public string LEVEL_KEY { get; set; }
        public string STATUS { get; set; }
    }

    public class jsonSentDB
    {
        public List<getDB> db { get; set; }
    }

    public class getAllApps
    {
        public List<string> allAppList { get; set; }
    }

    class addAppForUser
    {
        public string userName { get; set; }
        public string appName { get; set; }
    }

    class deleteAppFromUser
    {
        public string userName { get; set; }
        public string appName { get; set; }
    }

    class deleteUser
    {
        public string userNameDelete { get; set; }
        public string adminUserName { get; set; }
    }

    class logoutUser
    {
        public string userName { get; set; }
    }

    public class sentLogs
    {
        public int ID { get; set; }
        public string dateLogs { get; set; }
        public string typeLog { get; set; }
        public string source { get; set; }
        public string log { get; set; }
    }

    public class jsonSentLogs
    {
        public List<sentLogs> jsonLogs { get; set; }
    }

    class updateAppsForLevel
    {
        public string nameLevel { get; set; }
        public List<string> apps { get; set; }
    }

    class addAppsOnServer
    {
        public string userName { get; set; }
        public string password { get; set; }
        public List<addApps> listApps { get; set; }
    }

    public class addApps
    {
        public string pathExeFile { get; set; }
        public string nameApp { get; set; }
    }

    class deleteAppsForServer
    {
        public string userName { get; set; }
        public string password { get; set; }
        public List<string> appsList { get; set; }
    }

    public class app
    {
        public string appName { get; set; }
        public string folder { get; set; }
        public string EXE_File { get; set; }
    }

    public class getAllAppsOnPC
    {
        public Dictionary<string, app> getApps { get; set; }
    }

    class sentMsg
    {
        public string namePc { get; set; }
        public string msg { get; set; }
    }

    public class sentApp
    {
        public int appID { get; set; }
        public string name { get; set; }
        public string REMOTEAPP { get; set; }
    }

    public class sentBLOCKS_IP
    {
        public int ipID { get; set; }
        public string ip { get; set; }
    }

    public class jsonSentApp
    {
        public List<sentApp> jsonApp { get; set; }
    }

    public class jsonSentBLOCKS_IP
    {
        public List<sentBLOCKS_IP> jsonBLOCKS_IP { get; set; }
    }

    public class getAllUsersRemoteApp
    {
        public List<string> users { get; set; }
    }
}