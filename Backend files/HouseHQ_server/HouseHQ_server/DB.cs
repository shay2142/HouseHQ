using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;

namespace dataBase
{
    class DB
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");

        //    string path = @"MyDatabase.sqlite";
        //    string cs = @"URI=file:" + path;

        //    using var con = new SQLiteConnection(cs);
        //    con.Open();

        //    createTables(con);
        //    //deleteTable(con, "USERS");
        //    insertVluesToUsers(con, "shay", "12345", "shay@gmail.com", "1");
        //    //insertVluesToSingupKey(con, "12345");
        //    printUsersTable(con);
        //    Console.WriteLine(passwordIsCorrect(con, "shay", "1234"));
        //    Console.WriteLine(passwordIsCorrect(con, "shay", "12345"));
        //    printUsersTable(con);
        //    //deleteValueFromeTable(con, "USERS", "USERNAME", "shay");

        //    con.Dispose();
        //}
        //public DB()
        //{ 

        //}
        public void createTables(SQLiteConnection con)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS USERS(usersID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, USERNAME TEXT NOT NULL, PASSWORD TEXT NOT NULL, EMAIL TEXT NOT NULL, LEVEL_KEY TEXT, STATUS TEXT);";/*STATUS NOT NULL*/
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS APP(appID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, NAME TEXT NOT NULL, REMOTEAPP TEXT NOT NULL, PHAT_IMG TEXT);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS APPS(appsID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, usersID INTEGER, appID INTEGER, FOREIGN KEY(usersID) REFERENCES USERS(usersID), FOREIGN KEY(appID) REFERENCES APP(appID));";
                cmd.ExecuteNonQuery();
            }
            //Console.WriteLine("This table created");
        }

        public void insertVluesToUsers(SQLiteConnection con, string userName, string password, string email)
        {
            if (!userNameIsExists(con, userName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO USERS(USERNAME, PASSWORD, EMAIL) VALUES('" + userName + "', '" + password + "', '" + email + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("Username " + userName + " exists");
            }

        }

        public void insertVluesToAPP(SQLiteConnection con, string nameApp, string remoteApp /*string pathImg,*/)
        {
            if (!appIsExists(con, nameApp))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO APP(NAME, REMOTEAPP) VALUES('" + nameApp + "', '" + remoteApp + "')";
                    //cmd.CommandText = "INSERT INTO APP(NAME, REMOTEAPP) VALUES('" + nameApp + "', '" + remoteApp + "', '" + pathImg + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine(nameApp + "is exists");
            }
        }

        public bool userNameIsExists(SQLiteConnection con, string userName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT USERNAME FROM USERS", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == userName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool appIsExists(SQLiteConnection con, string appName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT NAME FROM APP", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == appName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool appIsExistsInUser(SQLiteConnection con, string userName, string appName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select APP.NAME from USERS join APPS ON  USERS.usersID = APPS.usersID JOIN APP ON APPS.appID = APP.appID WHERE USERS.USERNAME = '" + userName + "'", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == appName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //???
        public void updateLevelKey(SQLiteConnection con, string userName, string password, string level)
        {
            if (userNameIsExists(con, userName) && passwordIsCorrect(con, userName, password))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "update USERS set LEVEL_KEY = " + level + " where USERNAME = '" + userName + "'";
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("Username or password is incorrect");
            }
        }

        public string getLevelKey(SQLiteConnection con, string userName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select LEVEL_KEY from  USERS where USERNAME = '" + userName + "'", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    try
                    {
                        return rdr.GetString(0);
                    }
                    catch (InvalidCastException e)
                    {
                        return "";
                    }
                }
            }
            return "";
        }

        public bool passwordIsCorrect(SQLiteConnection con, string userName, string password)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT USERNAME, PASSWORD FROM USERS", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == userName && rdr.GetString(1) == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void addAppForUser(SQLiteConnection con, string userName, string nameApp)
        {
            if (appIsExists(con, nameApp) || userNameIsExists(con, userName))
            {
                if (!appIsExistsInUser(con, userName, nameApp))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "insert into apps(usersID, appID) VALUES ((SELECT usersID from USERS WHERE USERNAME='" + userName + "'), (SELECT appID from APP WHERE NAME='" + nameApp + "'))";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                Console.WriteLine("Username or app is not exists");
            }
        }

        public string getMailForUser(SQLiteConnection con, string userName)
        {
            if (userNameIsExists(con, userName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("select  EMAIL from  USERS where  USERNAME = '" + userName + "'", con))
                {
                    using SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        return (rdr.GetString(0));
                    }
                }
            }
            return "";
        }

        public string getStatusForUser(SQLiteConnection con, string userName)
        {
            if (userNameIsExists(con, userName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("select STATUS from  USERS where  USERNAME = '" + userName + "'", con))
                {
                    using SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        try
                        {
                            return (rdr.GetString(0));
                        }
                        catch (InvalidCastException e)
                        {
                            return "";
                        }
                    }
                }
            }
            return "";
        }

        public int getIdForUser(SQLiteConnection con, string userName)
        {
            if (userNameIsExists(con, userName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("select usersID from  USERS where  USERNAME = '" + userName + "'", con))
                {
                    using SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        try
                        {
                            return (rdr.GetInt32(0));
                        }
                        catch (InvalidCastException e)
                        {
                            return 0;
                        }
                    }
                }
            }
            return 0;
        }

        public string getPassForUser(SQLiteConnection con, string userName)
        {
            if (userNameIsExists(con, userName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("select  PASSWORD from  USERS where  USERNAME = '" + userName + "'", con))
                {
                    using SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        try
                        {
                            return (rdr.GetString(0));
                        }  
                        catch (InvalidCastException e)
                        {
                            return "";
                        }
                    }
                }
            }
            return "";
        }

        public List<string> getUserApplications(SQLiteConnection con, string userName)
        {
            List<string> appsList = new List<string>();
            using (SQLiteCommand cmd = new SQLiteCommand("select APP.NAME from USERS join APPS ON  USERS.usersID = APPS.usersID JOIN APP ON APPS.appID = APP.appID WHERE USERS.USERNAME = '" + userName + "'", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    appsList.Add(rdr.GetString(0));
                }
            }
            return appsList;
        }

        public List<string> getAllUsers(SQLiteConnection con)
        {
            List<string> usersList = new List<string>();
            using (SQLiteCommand cmd = new SQLiteCommand("select USERNAME from USERS;", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    usersList.Add(rdr.GetString(0));
                }
            }
            return usersList;
        }
        public List<string> getAllApplications(SQLiteConnection con)
        {
            List<string> appsList = new List<string>();
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT NAME FROM APP", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    appsList.Add(rdr.GetString(0));
                }
            }
            return appsList;
        }

        public void updateStatus(SQLiteConnection con, string userName, string status)
        {
            if (userNameIsExists(con, userName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "update USERS set  STATUS = '" + status + "' where USERNAME = '" + userName + "'";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string updateUser(SQLiteConnection con, string userName, string oldPassword, string newPassword, string mail, string level)
        {
            if (userNameIsExists(con, userName) && passwordIsCorrect(con, userName, oldPassword))
            {
                if (newPassword != "" && newPassword != null)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "update USERS set  PASSWORD = '" + newPassword + "' where USERNAME = '" + userName + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                if (mail != "" && mail != null)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "update USERS set  EMAIL = '" + mail + "' where USERNAME = '" + userName + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                if (level != null)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "update USERS set LEVEL_KEY = '"+ level + "' where USERNAME = '" + userName + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                return "";
            }
            else
            {
                return "Username or password is incorrect";
            }
        }

        public void printUsersTable(SQLiteConnection con)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM USERS", con))
            {
                /* Read data from DB*/
                using SQLiteDataReader rdr = cmd.ExecuteReader();
                Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-8} {rdr.GetName(2),8} {rdr.GetName(3),8} {rdr.GetName(4),8}");

                while (rdr.Read())
                {
                    Console.WriteLine($"{rdr.GetInt32(0),-3} {rdr.GetString(1),-8} {rdr.GetString(2),8} {rdr.GetString(3),-3} {rdr.GetString(4),-3}");
                }
            }
        }

        public void deleteValueFromeTable(SQLiteConnection con, string table, string column, string value) //value = just string value
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM " + table + " WHERE " + column + " = '" + value + "'";
                cmd.ExecuteNonQuery();
            }
        }
        public void deleteAppsFromUser(SQLiteConnection con, string userName, string appName)
        {
            //check input??
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM apps WHERE usersID = (SELECT usersID from USERS WHERE USERNAME='" + userName + "') and  appID = (SELECT appID from APP WHERE NAME='" + appName + "')";
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteTable(SQLiteConnection con, string table) //value = just string value
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DROP TABLE IF EXISTS " + table;
                cmd.ExecuteNonQuery();
            }
            createTables(con);
        }

        //public void insertVluesToSingupKey(SQLiteConnection con, string password)
        //{
        //    if (!singupKeyIsExists(con, password))
        //    {
        //        using (SQLiteCommand cmd = new SQLiteCommand(con))
        //        {
        //            cmd.CommandText = "INSERT INTO SINGUP_KEY(PASSWORD) VALUES('" + password + "')";
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("SingupKey " + password + " exists");
        //    }

        //}

        //public bool singupKeyIsExists(SQLiteConnection con, string singupKey)
        //{
        //    using (SQLiteCommand cmd = new SQLiteCommand("SELECT PASSWORD FROM SINGUP_KEY", con))
        //    {
        //        using SQLiteDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            if (rdr.GetString(0) == singupKey)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}

