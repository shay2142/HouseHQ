using System;
using System.Collections.Generic;
using System.Text;

namespace jsonSerializer
{
    class okLogin
    {
        // Make sure all class attributes have relevant getter setter.

        public string name { get; set; }

        public string mail { get; set; }

        public List<string> appList { get; set; }

        public string key { get; set; }

        public string img { get; set; }
    }

    class okSingup
    {
        // Make sure all class attributes have relevant getter setter.

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
}
