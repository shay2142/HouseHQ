using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
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

        //disable - אל תאפשר
        public void DiableADUserUsingUserPrincipal(string username)
        {
            try
            {
                PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity
                        (principalContext, username);
                userPrincipal.Enabled = false;
                userPrincipal.Save();

                //MessageBox.Show("AD Account disabled for {0}", username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //enable - אפשר
        public void EnableADUserUsingUserPrincipal(string username)
        {
            try
            {
                PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity
                (principalContext, username);
                userPrincipal.Enabled = true;
                userPrincipal.Save();

                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //return false;
        }

        public void deleteUser(string username)
        {
            try
            {
                PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity
                (principalContext, username);
                userPrincipal.Delete();
                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //return false;
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
