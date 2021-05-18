using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    class json
    {
    }

    public class okLogin
    {
        // Make sure all class attributes have relevant getter setter.

        public string name { get; set; }

        public string mail { get; set; }

        public List<string> appList { get; set; }

        public string key { get; set; }

        public string img { get; set; }
    }

    public class okSingup
    {
        // Make sure all class attributes have relevant getter setter.

        public bool ok { get; set; }
    }

    public class login
    {
        // Make sure all class attributes have relevant getter setter.
        public string name { get; set; }

        public string password { get; set; }
    }

    public class singup
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

    public class getUserInformation
    {
        public string userName { get; set; }
    }

    public class changeAccount
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

    public class addAppForUser
    {
        public string userName { get; set; }
        public string appName { get; set; }
    }

    public class deleteAppFromUser
    {
        public string userName { get; set; }
        public string appName { get; set; }
    }

    public class deleteUser
    {
        public string userNameDelete { get; set; }
        public string adminUserName { get; set; }
    }

    public class logoutUser
    {
        public string userName { get; set; }
    }

    public class WS_Login
    {
        public okLogin okLogin { get; set; }
        public error error { get; set; }
    }

    public class deleteAppsForServer
    {
        public string userName { get; set; }
        public string password { get; set; }
        public List<string> appsList { get; set; }
    }

    public class jsonSentLogs
    {
        public List<sentLogs> jsonLogs { get; set; }
    }

    public class sentLogs
    {
        public int ID { get; set; }
        public string dateLogs { get; set; }
        public string typeLog { get; set; }
        public string source { get; set; }
        public string log { get; set; }
    }
}
