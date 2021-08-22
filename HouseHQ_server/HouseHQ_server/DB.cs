using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using jsonSerializer;
using HouseHQ_server; 

namespace dataBase
{
    class DB
    {
        public Codes codes = new Codes();

        /*
         Create tables for DB if they do not exist.

         input: 
            - con: SQLiteConnection

         output: none
         */
        public void createTables(SQLiteConnection con)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS USERS(usersID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, USERNAME TEXT NOT NULL, PASSWORD TEXT NOT NULL, EMAIL TEXT NOT NULL, LEVEL_KEY TEXT, STATUS TEXT);";/* , IP_ADDRESS TEXT STATUS NOT NULL*/
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS APP(appID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, NAME TEXT NOT NULL, REMOTEAPP TEXT NOT NULL, PHAT_IMG TEXT);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS APPS(appsID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, usersID INTEGER, appID INTEGER, FOREIGN KEY(usersID) REFERENCES USERS(usersID), FOREIGN KEY(appID) REFERENCES APP(appID));";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS LOGS(logID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, DATE_LOGS TEXT NOT NULL, CODE TEXT NOT NULL, TYPE TEXT NOT NULL, J_LOG TEXT NOT NULL, usersID INTEGER REFERENCES USERS (usersID) );";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS LEVEL(levelID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name_level TEXT NOT NULL, admin BOOLEAN NOT NULL);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS LEVELS(levelsID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, levelID INTEGER, appID INTEGER, FOREIGN KEY(levelID) REFERENCES LEVEL(levelID), FOREIGN KEY(appID) REFERENCES APP(appID));";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS BLOCKS_IP(ipID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, ip TEXT NOT NULL);";
                cmd.ExecuteNonQuery();

            }
        }

        /*


         input: 

         output:
         */
        public List<string> getAllBlockIp(SQLiteConnection con)
        {
            List<string> ipBlock = new List<string>();
            using (SQLiteCommand cmd = new SQLiteCommand("select ip from BLOCKS_IP", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ipBlock.Add(rdr.GetString(0));
                }
            }
            return ipBlock;
        }

        /*


         input: 

         output:
         */
        public void deleteIpblock(SQLiteConnection con, string ip)
        {
            if (ipIsBlock(con, ip))
            {
                deleteValueFromeTable(con, "BLOCKS_IP", "ip", ip);
            }
        }

        /*


         input: 

         output:
         */
        public void insertVluesToBLOCKS_IP(SQLiteConnection con, string ip)
        {
            if (!ipIsBlock(con, ip))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "insert into BLOCKS_IP(ip) VALUES ('" + ip + "')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*


         input: 

         output:
         */
        public bool ipIsBlock(SQLiteConnection con, string ip)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT ip FROM BLOCKS_IP", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == ip)
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
        public void deleteLevel(SQLiteConnection con, string levelName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM LEVELS WHERE levelID = (SELECT levelID from LEVEL WHERE name_level='" + levelName + "')";
                cmd.ExecuteNonQuery();
            }
            deleteLevelFromUsers(con, levelName);
            deleteValueFromeTable(con, "LEVEL", "name_level", levelName);
        }

        /*


         input: 

         output:
         */
        public void deleteAppForLevel(SQLiteConnection con, string nameLevel, List<string> apps)
        {
            foreach (string app in apps)
            {
                if (appIsExists(con, app))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "DELETE FROM LEVELS WHERE levelsID = (SELECT levelID from LEVEL WHERE name_level='" + nameLevel + "') and  appID = (SELECT appID from APP WHERE NAME='" + app + "')";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /*


         input: 

         output:
         */
        public void deleteLevelFromUsers(SQLiteConnection con, string levelName)
        {
            List<string> users = getAllUsers(con);
            foreach (string user in users)
            {
                if (getLevelKey(con, user) == levelName)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "update USERS set LEVEL_KEY = '' where USERNAME = '" + user + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /*


         input: 

         output:
         */
        public List<string> getLevelApplications(SQLiteConnection con, string nameLevel)
        {
            List<string> appsList = new List<string>();
            using (SQLiteCommand cmd = new SQLiteCommand("select APP.NAME from LEVEL join LEVELS ON LEVEL.levelID = LEVELS.levelID JOIN APP ON LEVELS.appID = APP.appID WHERE LEVEL.name_level = '" + nameLevel + "'", con))
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
        public void createLevel(SQLiteConnection con, string nameLevel, bool isAdmin)
        {
            if (!levelIsExists(con, nameLevel))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO LEVEL(name_level, admin) VALUES('" + nameLevel + "', '" + isAdmin.ToString() + "')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*


         input: 

         output:
         */
        public void insertAppToLevel(SQLiteConnection con, string nameLevel, List<string> apps)
        {
            if (levelIsExists(con, nameLevel))
            {
                foreach (string app in apps)
                {
                    if (appIsExists(con, app))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(con))
                        {
                            cmd.CommandText = "insert into LEVELS(levelID, appID) VALUES ((SELECT levelID from LEVEL WHERE name_level='" + nameLevel + "'), (SELECT appID from APP WHERE NAME='" + app + "'))";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /*


         input: 

         output:
         */
        public bool levelIsExists(SQLiteConnection con, string levelName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT name_level FROM LEVEL", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == levelName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*
         The function enters appropriate values in the "USERS" table and thus creates a new user.

         input:
            -con: SQLiteConnection
            -userName: string
            -password: string
            -email: string

         output: none
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
         The function adds appropriate values to the "APP" table and thus adds new applications to reamoteApp.

         input:
            -con: SQLiteConnection
            -nameApp: string
            -remoteApp: string

         output: none
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
        public void insertVluesToLOGS(SQLiteConnection con, string json, string type, string userName)
        {
            hash hash = new hash();
            DateTime aDate = DateTime.Now;
            string[] jsons = json.Split('&');
            string code = jsons[0];

            if (type == "client->server")
            {
                code = hash.getCodeHash(jsons[0]);
            }

            switch (code)
            {
                case "215":
                    jsons[1] = "sent logs";
                    break;
                case "233":
                    jsons[1] = "sent image";
                    break;
                case "222":
                    jsons[1] = "sent apps on server";
                    break;
                default:
                    break;
            }
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "INSERT INTO LOGS(DATE_LOGS, CODE, TYPE, J_LOG, usersID) VALUES('" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', '" + code + "', '" + type + "', '" + jsons[1] + "', '" + getIdForUser(con, userName) + "')";
                cmd.ExecuteNonQuery();
            }
        }
        /*
         The function checks if the user exists.

         input:
            -con: SQLiteConnection
            -userName: string

         output:
            -true or false type bool
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
         The function checks if an application exists.

         input:
            -con: SQLiteConnection
            -appName: string

         output:
            -true or false type bool
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
        public bool appIsExistsInLevel(SQLiteConnection con, string levelName, string appName)
        {
            if (levelIsExists(con, levelName))
            {
                foreach (string app in getLevelApplications(con, levelName))
                {
                    if (app == appName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*
         The function checks if an application exists in a user.

         input:
            -con: SQLiteConnection
            -userName: string
            -appName: string

         output:
            -true or false type boll
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
         The function updates the "LevelKey" in DB.

         input:
            -con: SQLiteConnection
            -userName: string
            -password: string
            -level: string

         output: none
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
         The function returns the "LevelKey" according to the user it receives.

         input:
            -con: SQLiteConnection
            -userName: string
         output:
            return string level key if there's something wrong the func retun ""
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
         The function checks if a username and password are correct.

         input:
            -con: SQLiteConnection
            -userName: string
            -password: string

         output:
            -return true when all is well
            -return false when there's something wrong
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
         The function connects the applications with the user.

         input:
            -con: SQLiteConnection
            -userName: string
            -nameApp: string

         output: none
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
         The function gives the user's email.

         input:
            -con: SQLiteConnection
            -userName: userName

         output:
             return the email
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
         The function sends the status of the user "online" or "offline".

         input:
            -con: SQLiteConnection
            -userName: string

         output:
            return the status of user
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
         The function returns the "userID" of the user

         input:
            -con: SQLiteConnection
            -userName: string

         output:
            return the userID if user name is not exists return 0
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
        public string getDateForLog(SQLiteConnection con, int logID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select DATE_LOGS from LOGS where  logID = '" + logID + "'", con))
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
            return "";
        }

        /*


         input: 

         output:
         */
        public string getNameForLevel(SQLiteConnection con, int levelID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select name_level from LEVEL where levelID = '" + levelID + "'", con))
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
            return "";
        }

        /*


         input: 

         output:
         */
        public string getAdminForLevel(SQLiteConnection con, int levelID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select admin from LEVEL where levelID = '" + levelID + "'", con))
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
            return "";
        }

        /*


         input: 

         output:
         */
        public string getCodeForLog(SQLiteConnection con, int logID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select CODE from LOGS where  logID = '" + logID + "'", con))
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
            return "";
        }

        /*


         input: 

         output:
         */
        public string getSourceForLog(SQLiteConnection con, int logID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select TYPE from LOGS where  logID = '" + logID + "'", con))
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
            return "";
        }

        /*


         input: 

         output:
         */
        public string getJ_LOGForLog(SQLiteConnection con, int logID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select J_LOG from LOGS where  logID = '" + logID + "'", con))
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
            return "";
        }
        /*
         The function returns the user's password.

         input:
            -con: SQLiteConnection
            -userName: string

         output:
            return the password if there's something wrong return ""
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
         The function returns all the applications that the user has

         input:
            -con: SQLiteConnection
            -userName: string

         output:
           return List<string> of apps
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
         The function returns all existing users in DB.

         input:
            -con: SQLiteConnection

         output:
            return the list users
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
        public List<int> getAllLogsID(SQLiteConnection con)
        {
            List<int> logsList = new List<int>();
            using (SQLiteCommand cmd = new SQLiteCommand("select logID from LOGS;", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    logsList.Add(rdr.GetInt32(0));
                }
            }
            return logsList;
        }

        /*


         input: 

         output:
         */
        public List<int> getAllLevelID(SQLiteConnection con)
        {
            List<int> levelsList = new List<int>();
            using (SQLiteCommand cmd = new SQLiteCommand("select levelID from LEVEL;", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    levelsList.Add(rdr.GetInt32(0));
                }
            }
            return levelsList;
        }

        /*
         The function returns all the applications that are on the server.

         input:
            -con: SQLiteConnection

         output:
            return the list apps
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
        public List<int> getAppID(SQLiteConnection con)
        {
            List<int> appsID = new List<int>();
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT appID FROM APP", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    appsID.Add(rdr.GetInt32(0));
                }
            }
            return appsID;
        }

        /*


         input: 

         output:
         */
        public string getNameForApp(SQLiteConnection con, int appID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select NAME from APP where appID = '" + appID + "'", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    return (rdr.GetString(0));
                }
            }
            return "";
        }

        /*


         input: 

         output:
         */
        public string getRemoteAppForApp(SQLiteConnection con, int appID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select REMOTEAPP from APP where appID = '" + appID + "'", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    return (rdr.GetString(0));
                }
            }
            return "";
        }

        /*
         The function updates the status status of the user.

         input:
            -con: SQLiteConnection
            -userName: string
            -status: string

         output: none
         */
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

        /*
         The function updates the user information according to the parameters it receives.

         input:
            -con: SQLiteConnection
            -userName: string
            -oldPassword: string
            -newPassword: string

         output:
            return error msg if necessary.
         */
        public string updateUser(SQLiteConnection con, string userName, string oldPassword, string newPassword, string mail, string level)
        {
            if (userNameIsExists(con, userName) && passwordIsCorrect(con, userName, oldPassword))
            {
                //update password
                if (newPassword != "" && newPassword != null)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "update USERS set  PASSWORD = '" + newPassword + "' where USERNAME = '" + userName + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                //update mail
                if (mail != "" && mail != null)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "update USERS set  EMAIL = '" + mail + "' where USERNAME = '" + userName + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                //update level key
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
         The function prints the user table.

         input:
            -con: SQLiteConnection

         output: none
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
         The function deletes values in tables according to the values that enter the function.

         input:
            -con: SQLiteConnection
            -table: string
            -column: string
            -value: string

         output: none
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
         The function deletes applications from users

         input:
            -con: SQLiteConnection
            -userName: string
            -appName: string

         output: none
         */
        public void deleteAppsFromUser(SQLiteConnection con, string userName, string appName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM apps WHERE usersID = (SELECT usersID from USERS WHERE USERNAME='" + userName + "') and  appID = (SELECT appID from APP WHERE NAME='" + appName + "')";
                cmd.ExecuteNonQuery();
            }
        }

        /*
         The function deletes all applications from users.
         input:
            -con: SQLiteConnection
            -userName: string

         output: none
         */
        public void deleteAppsUser(SQLiteConnection con, string userName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM apps WHERE usersID = (SELECT usersID from USERS WHERE USERNAME='" + userName + "')";
                cmd.ExecuteNonQuery();
            }
        }

        /*
         The function deletes tables and recreates them.

         input:
            -con: SQLiteConnection
            -table: string

         output: none
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

        /*


         input: 

         output:
         */
        public void createAdminDefault(SQLiteConnection con, string userName, string password, string mail)
        {
            insertVluesToUsers(con, userName, password, mail);
            updateUser(con, userName, password, null, null, "admin");
            updateStatus(con, userName, "offline");
        }

        /*


         input: 

         output:
         */
        public bool adminIsExist(SQLiteConnection con)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT LEVEL_KEY FROM USERS", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(0) == "admin")
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
        public void deleteApps(SQLiteConnection con, string appName)
        {
            if (appIsExists(con, appName))
            {
                foreach (string user in getAllUsers(con))
                {
                    deleteAppsFromUser(con, user, appName);
                }
                deleteValueFromeTable(con, "APP", "NAME", appName);
            }
        }

        /*


         input: 

         output:
         */
        public List<sentDB> getDB(SQLiteConnection con)
        {
            var list = new List<sentDB>();
            List<string> userList = getAllUsers(con);
            foreach (var user in userList)
            {
                list.Add(new sentDB()
                {
                    ID = getIdForUser(con, user),
                    userName = user,
                    password = getPassForUser(con, user),
                    mail = getMailForUser(con, user),
                    LEVEL_KEY = getLevelKey(con, user),
                    STATUS = getStatusForUser(con, user)
                });
            }
            return list;
        }

        public List<int> getUserLogsID(SQLiteConnection con, string userName)
        {
            List<int> logsList = new List<int>();
            using (SQLiteCommand cmd = new SQLiteCommand("select logID from LOGS where usersID = (SELECT usersID from USERS WHERE USERNAME='" + userName + "');", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    logsList.Add(rdr.GetInt32(0));
                }
            }
            return logsList;
        }

        /*


        input: 

        output:
        */
        public List<sentLogs> getLogsPerUser(SQLiteConnection con, string userName)
        {
            var list = new List<sentLogs>();
            List<int> logsID = getUserLogsID(con, userName);

            foreach (int logID in logsID)
            {
                list.Add(new sentLogs()
                {
                    ID = logID,
                    dateLogs = getDateForLog(con, logID),
                    typeLog = getCodeForLog(con, logID),
                    source = getSourceForLog(con, logID),
                    log = getJ_LOGForLog(con, logID)
                });
            }
            return list;
        }

        /*


         input: 

         output:
         */
        public List<sentLogs> sentLogsDB(SQLiteConnection con)
        {
            var list = new List<sentLogs>();
            List<int> logsID = getAllLogsID(con);
            string keyTypeLog = "Error";

            foreach (int logID in logsID)
            {
                if (codes.codes().ContainsKey(getCodeForLog(con, logID)))
                {
                    keyTypeLog = codes.codes()[getCodeForLog(con, logID)];
                }
                else
                {
                    keyTypeLog = "Error";
                }
                list.Add(new sentLogs()
                {
                    ID = logID,
                    dateLogs = getDateForLog(con, logID),
                    typeLog = keyTypeLog,
                    source = getSourceForLog(con, logID),
                    log = getJ_LOGForLog(con, logID)
                });
            }
            return list;
        }

        /*


         input: 

         output:
         */
        public List<sentLevels> sentLevelsInformation(SQLiteConnection con)
        {
            var list = new List<sentLevels>();
            List<int> levelsID = getAllLevelID(con);  

            foreach (int levelID in levelsID)
            {
                list.Add(new sentLevels()
                {
                    ID = levelID,
                    name = getNameForLevel(con, levelID),
                    admin = Convert.ToBoolean(getAdminForLevel(con, levelID)),
                    apps = getLevelApplications(con, getNameForLevel(con, levelID))
                });
            }
            return list;
        }

        /*


         input: 

         output:
         */
        public List<sentApp> sentApp(SQLiteConnection con)
        {
            var list = new List<sentApp>();
            List<int> appIDs = getAppID(con);
            foreach(int appID in appIDs)
            {
                list.Add(new sentApp() 
                { 
                    appID = appID,
                    name = getNameForApp(con, appID),
                    REMOTEAPP = getRemoteAppForApp(con, appID)
                });
            }
            return list;
        }

        /*


         input: 

         output:
         */
        public List<sentBLOCKS_IP> sentBLOCKS_IP(SQLiteConnection con)
        {
            var list = new List<sentBLOCKS_IP>();
            List<int> ipIDs = getIpID(con);
            foreach (int ipID in ipIDs)
            {
                list.Add(new sentBLOCKS_IP()
                {
                    ipID = ipID,
                    ip = getIp(con, ipID),
                });
            }
            return list;
        }

        /*


         input: 

         output:
         */
        public List<int> getIpID(SQLiteConnection con)
        {
            List<int> appsID = new List<int>();
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT ipID FROM BLOCKS_IP", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    appsID.Add(rdr.GetInt32(0));
                }
            }
            return appsID;
        }

        /*


         input: 

         output:
         */
        public string getIp(SQLiteConnection con, int ipID)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select ip from BLOCKS_IP where ipID = '" + ipID + "'", con))
            {
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    return (rdr.GetString(0));
                }
            }
            return "";
        }
    }
}

