using System;

namespace user
{
    class Program
    {
        public static string Name;
        public static string Pass;


        static void Main(string[] args)
        {
            Console.WriteLine("Windows Account Creator");
            Console.WriteLine("Enter User Name");
            Name = Console.ReadLine();

            Pass = Console.ReadLine();
            Console.WriteLine("Enter User Password");

            createUser(Name, Pass);

        }

        public static void createUser(string Name, string Pass)
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

                grp = AD.Children.Find("Administrators", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
                Console.WriteLine("Account Created Successfully");
                Console.WriteLine("Press Enter to continue....");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();

            }

        }
    }
    }
}
