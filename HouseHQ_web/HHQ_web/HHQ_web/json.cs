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

    class getAllUsers
    {
        public List<string> usersList { get; set; }
    }

    class userInformation
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
}