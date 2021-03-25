using System;
using System.Collections.Generic;
using System.Text;

namespace jsonSerializer
{
    class okLogin
    {
        // Make sure all class attributes have relevant getter setter.

        public string name { get; set; }

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
}
