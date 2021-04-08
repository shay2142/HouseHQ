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
}
