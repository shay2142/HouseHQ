using System;
using System.Collections.Generic;
using System.Text;

namespace jsonDeserialize
{
    class login
    {
        // Make sure all class attributes have relevant getter setter.
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
    class error
    {
        public string msg { get; set; }
    }
}
