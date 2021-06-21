using System;
using System.Collections.Generic;
using System.Text;

namespace jsonDeserialize
{
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

    class addAppForUser
    {
        public string userName { get; set; }
        public string appName { get; set; }
    }

    class deleteUser
    {
        public string userNameDelete { get; set; }
        public string adminUserName { get; set; }
    }

    class changeAccount
    {
        public string userName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string mail { get; set; }
        public string level { get; set; }
    }

    class deleteAppFromUser
    {
        public string userName { get; set; }
        public string appName { get; set; }
    }

    class getUserInformation
    {
        public string userName { get; set; }
    }

    class logoutUser
    {
        public string userName { get; set; }
    }

    class addLevelKey
    {
        public string nameLevel { get; set; }
        public List<string> apps { get; set; }
        public bool admin { get; set; }
    }

    class deleteAppForLevel
    { 
        public string nameLevel { get; set; }
        public List<string> apps { get; set; }
    }

    class deleteLevel
    { 
        public string nameLevel { get; set; }
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

    class addApps
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

    class sentMsg
    { 
        public string namePc { get; set; }
        public string msg { get; set; }
    } 
}
