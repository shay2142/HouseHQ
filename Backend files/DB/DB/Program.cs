using System;
using System.IO;
using System.Data.SQLite;

namespace DB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string path = @"C:\Users\shay5\source\repos\DB\DB\MyDatabase.sqlite";
            string cs = @"URI=file:" + path;

            using var con = new SQLiteConnection(cs);
            con.Open();

            createTables(con);
            deleteTable(con, "USERS");
            insertVluesToUsers(con, "shay", "12345", "shay@gmail.com", "1");
            printUsersTable(con);
            //deleteValueFromeTable(con, "USERS", "USERNAME", "shay");

            con.Dispose();
        }

        public static void createTables(SQLiteConnection con)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS USERS(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, USERNAME TEXT NOT NULL, PASSWORD TEXT NOT NULL, EMAIL TEXT NOT NULL, LEVEL_KEY TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS APPS(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, NAME TEXT NOT NULL, REMOTEAPP_LICATION_PROGRAM TEXT NOT NULL, PATH_IMG TEXT NOT NULL, LEVEL_LOCK TEXT, USER_ID INTERGER NOT NULL, FOREIGN KEY (USER_ID) REFERENCES USERS(ID))";
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
                Console.WriteLine("Username "+ userName  + " exists");
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


    }
}

