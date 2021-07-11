using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHQ_server
{
    class createUser
    {
        /*


         input: 

         output:
         */
        public string createUserOnWin(string Name, string Pass)
        {
            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" +
                                    Environment.MachineName + ",computer");
                DirectoryEntry NewUser = AD.Children.Add(Name, "user");
                NewUser.Invoke("SetPassword", new object[] { Pass });
                NewUser.Invoke("Put", new object[] { "Description", "Test User from .NET" });
                NewUser.CommitChanges();
                DirectoryEntry grp;

                grp = AD.Children.Find("Remote Desktop Users", "group");
                if (grp != null) 
                {
                    grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); 
                }

                return "Account Created Successfully";

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        /*


         input: 

         output:
         */
        public string deleteUserOnWin(string userName)
        {
            try
            {
                using (var root = new DirectoryEntry($"WinNT://{Environment.MachineName}"))
                using (var userAccount = root.Children.Find(userName))
                {
                    root.Children.Remove(userAccount);
                }
                return "Account Delete Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /*


         input: 

         output:
         */
        public string changePassword(string userName, string newPassword)
        {
            try
            {
                DirectoryEntry localDirectory =
                    new DirectoryEntry("WinNT://" + Environment.MachineName.ToString());
                DirectoryEntries users = localDirectory.Children;
                DirectoryEntry user = users.Find(userName);
                user.Invoke("SetPassword", newPassword);

                return "Success!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
