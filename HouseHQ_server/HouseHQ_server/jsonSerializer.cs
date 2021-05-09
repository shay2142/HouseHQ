using System;
using System.Collections.Generic;
using System.Text;

namespace jsonSerializer
{
    class okLogin
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

    class error
    {
        public string msg { get; set; }
    }

    class getAllApps
    {
        public List<string> allAppList { get; set; }
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

    class sentDB
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public string LEVEL_KEY { get; set; }
        public string STATUS { get; set; }
    }

    class jsonSentDB
    {
        public List<sentDB> db { get; set; }
    }

    class sentLogs
    {
        public int ID { get; set; }
        public string dateLogs { get; set; }
        public string typeLog { get; set; }
        public string source { get; set; }
        public string log { get; set; }
    }

    class jsonSentLogs
    {
        public List<sentLogs> jsonLogs { get; set; }
    }

    class sentLevels
    {
        public int ID { get; set; }
        public string name { get; set; }
        public List<string> apps { get; set; }
        public bool admin { get; set; }
    }

    class jsonSentLevels
    { 
        public List<sentLevels> jsonLevels { get; set; }
    }

    class getAllAppsOnPC
    { 
        public Dictionary<string, HouseHQ_server.app> getApps { get; set; }
    }

    class sentApp
    {
        public int appID { get; set; }
        public string name { get; set; }
        public string REMOTEAPP { get; set; }
    }

    class sentBLOCKS_IP
    { 
        public int ipID { get; set; }
        public string ip { get; set; }
    }

    class jsonSentApp
    {
        public List<sentApp> jsonApp { get; set; }
    }

    class jsonSentBLOCKS_IP
    {
        public List<sentBLOCKS_IP> jsonBLOCKS_IP { get; set; }
    }

    class getAllUsersRemoteApp
    { 
        public List<string> users { get; set; }
    }
}
