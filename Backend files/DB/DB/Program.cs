using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;

namespace DB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string path = @"C:\Users\shay5\Documents\househq\Backend files\DB\DB\MyDatabase.sqlite";
            string cs = @"URI=file:" + path;

            using var con = new SQLiteConnection(cs);
            con.Open();

            createTables(con);
            //deleteTable(con, "USERS");
            insertVluesToUsers(con, "shay", "12345", "shay@gmail.com", "1");
            insertVluesToUsers(con, "shay", "12345", "shay@gmail.com", "1");
            insertVluesToUsers(con, "shay1", "12345", "shay@gmail.com", "1");
            insertVluesToAPP(con, "vs2019", "c:\\vs2019");
            insertVluesToAPP(con, "vs2019", "c:\\vs2019");
            insertVluesToAPP(con, "vscode", "c:\\vscode");
            insertVluesToAPP(con, "notepad", "c:\\notepad");
            addAppForUser(con, "shay", "vs2019");
            addAppForUser(con, "shay", "vscode");
            addAppForUser(con, "shay1", "notepad");

            getUserApplications(con, "shay").ForEach(Console.WriteLine);
            Console.WriteLine("\n\nshay1\n");
            
            getUserApplications(con, "shay1").ForEach(Console.WriteLine);
            //insertVluesToSingupKey(con, "12345");
            printUsersTable(con);
            //Console.WriteLine(passwordIsCorrect(con, "shay", "1234"));
            //Console.WriteLine(passwordIsCorrect(con, "shay", "12345"));
            printUsersTable(con);
            //deleteValueFromeTable(con, "USERS", "USERNAME", "shay");

            con.Dispose();
        }

        public static void createTables(SQLiteConnection con)
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

        public static void insertVluesToUsers(SQLiteConnection con, string userName, string password, string email, string levelKey)
        {
            if (!userNameIsExists(con, userName))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO USERS(USERNAME, PASSWORD, EMAIL, LEVEL_KEY) VALUES('" + userName + "', '" + password + "', '" + email + "', '" + levelKey + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("Username " + userName + " exists");
            }

        }

        public static void insertVluesToAPP(SQLiteConnection con, string nameApp, string remoteApp /*string pathImg,*/)
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
        
        public static bool userNameIsExists(SQLiteConnection con, string userName)
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

        public static bool appIsExists(SQLiteConnection con, string appName)
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

        public static void addAppForUser(SQLiteConnection con, string userName, string nameApp)
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

        public static bool appIsExistsInUser(SQLiteConnection con, string userName, string appName)
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

        public static List<string> getUserApplications(SQLiteConnection con, string userName)
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
        //אולי לשנות תפונק' שיבדוק בצורה קצת שונה
        public static bool passwordIsCorrect(SQLiteConnection con, string userName, string password)
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

        public static void printUsersTable(SQLiteConnection con)
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

        public static void deleteValueFromeTable(SQLiteConnection con, string table, string column, string value) //value = just string value
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DELETE FROM " + table + " WHERE " + column + " = '" + value + "'";
                cmd.ExecuteNonQuery();
            }
        }

        public static void deleteTable(SQLiteConnection con, string table) //value = just string value
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DROP TABLE IF EXISTS " + table;
                cmd.ExecuteNonQuery();
            }
            createTables(con);
        }

        //public static void insertVluesToSingupKey(SQLiteConnection con, string password)
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

        //public static bool singupKeyIsExists(SQLiteConnection con, string singupKey)
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

