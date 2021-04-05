using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;

namespace dataBase
{
    class DB
    {
        /*
         
         input:
         output:
         */
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
        }
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
        public void insertVluesToAPP(SQLiteConnection con, string nameApp, string remoteApp /*string pathImg,*/)
        {
            if (!appIsExists(con, nameApp))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO APP(NAME, REMOTEAPP) VALUES('" + nameApp + "', '" + remoteApp + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine(nameApp + "is exists");
            }
        }
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
        public void updateStatus(SQLiteConnection con, string userName, string status)
        {
            if (userNameIsExists(con, userName) )
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "update USERS set  STATUS = '" + status + "' where USERNAME = '" + userName + "'";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /*
         
         input:
         output:
         */
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
                        cmd.CommandText = "update USERS set LEVEL_KEY = '" + level + "' where USERNAME = '" + userName + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (level == null)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "update USERS set LEVEL_KEY = '' where USERNAME = '" + userName + "'";
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
        /*
         
         input:
         output:
         */
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
        /*
         
         input:
         output:
         */
        public void deleteValueFromeTable(SQLiteConnection con, string table, string column, string value) //value = just string value
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM " + table + " WHERE " + column + " = '" + value + "'";
                cmd.ExecuteNonQuery();
            }
        }
        /*
         
         input:
         output:
         */
        public void deleteAppsFromUser(SQLiteConnection con, string userName, string appName)
        {
            //check input??
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM apps WHERE usersID = (SELECT usersID from USERS WHERE USERNAME='" + userName + "') and  appID = (SELECT appID from APP WHERE NAME='" + appName + "')";
                cmd.ExecuteNonQuery();
            }
        }
        /*
         
         input:
         output:
         */
        public void deleteTable(SQLiteConnection con, string table) //value = just string value
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DROP TABLE IF EXISTS " + table;
                cmd.ExecuteNonQuery();
            }
            createTables(con);
        }
    }
}

