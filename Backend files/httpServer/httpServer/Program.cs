// Filename:  HttpServer.cs        
// Author:    Benjamin N. Summerton <define-private-public>        
// License:   Unlicense (http://unlicense.org/)

using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Data.SQLite;

using jsonDeserialize;
using jsonSerializer;
using dataBase;

namespace HttpListenerExample
{
    class HttpServer
    {
        public static SQLiteConnection con;
        public static HttpListener listener;
        public static DB db = new DB();
        public static string url = "http://192.168.0.131:8080/";
        public static int requestCount = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HouseHQ</title>" +
            "  </head>" +
            "  <body>" +
            "     <h1>HouseHQ</h1>" +
            "    <p>Hi! You go to a place you are not supposed to go to.</p>" +
            "  </body>" +
            "</html>";

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
               
                Console.WriteLine(ctx.Request.HttpMethod);

                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") /*&& (req.Url.AbsolutePath == "/shutdown")*/)
                {
                    Console.WriteLine("test");
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        string s = reader.ReadToEnd();
                        Console.WriteLine("InputStream: {0}", s);

                        string[] json = s.Split('&');
                        Console.WriteLine(json[0]);
                        Console.WriteLine(json[1]);
                        //Console.WriteLine();
                        string msg = "";
                        if (IsValidJson(json[1]))
                        {
                            switch (json[0])
                            {
                                case "101"://login
                                    msg = login(json[1]);
                                    break;
                                case "102": //singup
                                    msg = singup(json[1]);
                                    break;
                                case "103"://change account
                                    break;
                                case "109"://logout
                                    break;
                                case "104"://add apps?
                                    break;
                                case "105"://all apps
                                    break;
                                case "106"://delete apps?
                                    break;
                                default://400 error
                                    break;
                            }
                            response(resp, msg, "json");
                        }
                        else
                        {
                            response(resp, "the msg is not json", "text");
                        }
                    }
                }
                else
                {
                    response(resp, String.Format(pageData), "text/html");
                }
                
            }
        }
        public static string login(string json)
        {
            var user = System.Text.Json.JsonSerializer.Deserialize<login>(json);
            Console.WriteLine(user.name);
            if (db.userNameIsExists(con, user.name) && db.passwordIsCorrect(con, user.name, user.password))
            {
                okLogin test = new okLogin()
                {
                    name = user.name,
                    appList = new List<string>
                    {
                        "app1",
                        "app2",
                        "app3"
                    }
                };
                return "201&" + JsonConvert.SerializeObject(test);
            }
            else
            {
                return error("Username or password incorrect");
            }
        }
        public static string singup(string json)
        {
            var user = System.Text.Json.JsonSerializer.Deserialize<singup>(json);
            //DB
            if (!db.userNameIsExists(con, user.name))
            {
                db.insertVluesToUsers(con, user.name, user.password, user.mail, user.key);
                return "202&";
            }
            else
            {
                return error("username is exist");
            }
            
        }
        public static string changeAccount()
        {
            return "203&";
        }
        public static string logout()//return???
        {
            return "209&";
        }
        public static string allApps()
        {
            return "205&";
        }
        public static string error(string msg)
        {
            error err = new error()
            {
                msg = msg
            };
            return "400&" + JsonConvert.SerializeObject(err);
        }
        public static void response(HttpListenerResponse resp, string msg, string ContentType)
        {
            // Write the response info
            byte[] data = Encoding.UTF8.GetBytes(msg);
            resp.ContentType = ContentType;
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            // Write out to the response stream (asynchronously), then close it
            resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
        }
        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void Main(string[] args)
        {
            //string path = @"MyDatabase.sqlite";
            string path = @"C:\Users\shay5\Documents\HHQ\Backend files\httpServer\httpServer\MyDatabase.sqlite";
            string cs = @"URI=file:" + path;
            
            con = new SQLiteConnection(cs);
            //using var con = new SQLiteConnection(cs);
            con.Open();
            db.createTables(con);
            
            // Create a Http server and start listening for incoming connections
            //url = "http://" + GetLocalIPAddress() + ":8080/";
            url = "http://+:8080/";
            //String[] prefixes = { "http://+:8080/"/*, "https://+:8443/"*/ };
            String[] prefixes = { "http://+:8080/"/*, "https://+:8443/"*/ };
            listener = new HttpListener();
            //listener.Prefixes.Add(url);
            foreach (string s in prefixes)
                listener.Prefixes.Add(s);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            //close DB
            con.Dispose();
            // Close the listener
            listener.Close();
        }
    }
    class login
    {
        // Make sure all class attributes have relevant getter setter.
        public string name { get; set; }

        public string password { get; set; }
    }
    class ok
    {
        // Make sure all class attributes have relevant getter setter.

        public string name { get; set; }

        public List<string> appList { get; set; }

        public string key { get; set; }

        public string img { get; set; }
    }
}