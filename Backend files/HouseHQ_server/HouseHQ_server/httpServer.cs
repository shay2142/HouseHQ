using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Data.SQLite;

using jsonDeserialize;
using jsonSerializer;
using dataBase;
using System.Windows.Forms;
using System.Threading;


namespace HouseHQ_server
{
    public class httpServer
    {
        public SQLiteConnection con;
        public HttpListener listener;
        internal DB db = new DB();
        public string url = "http://192.168.0.131:8080/";
        public int requestCount = 0;
        public string pageData =
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

        public httpServer()
        {
            
        }

        public void frmNewFormThread()
        {
            Application.Run(new Form2(this));
        }

        public void runServer()
        {
            //string path = @"MyDatabase.sqlite";
            string path = @"C:\Users\shay5\Documents\househq\Backend files\HouseHQ_server\HouseHQ_server\MyDatabase.sqlite";
            string cs = @"URI=file:" + path;

            con = new SQLiteConnection(cs);
            //using var con = new SQLiteConnection(cs);
            con.Open();
            db.createTables(con);

            var newThread = new System.Threading.Thread(frmNewFormThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();

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

        public string GetLocalIPAddress()
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

        public async Task HandleIncomingConnections()
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
                        if (IsValidJson(json[1]) || (Int32.Parse(json[0]) > 100 && Int32.Parse(json[0]) < 200))
                        {
                            switch (json[0])
                            {
                                case "101"://login
                                    msg = login(json[1]);//
                                    break;
                                case "102": //singup
                                    msg = singup(json[1]);//
                                    break;
                                case "103"://change account
                                    msg = changeAccount(json[1]);//
                                    break;
                                case "104"://add apps?
                                    break;
                                case "105"://all apps
                                    msg = allApps();//
                                    break;
                                case "106"://delete apps?
                                    break;
                                case "107"://delete apps from user
                                    msg = deleteAppsFromUser(json[1]);//
                                    break;
                                case "108"://add apps for user
                                    msg = addAppForUser(json[1]);//
                                    break;
                                case "109"://logout
                                    msg = logout(json[1]);
                                    break;
                                case "110"://delete user
                                    msg = deleteUser(json[1]);//
                                    break;
                                case "111"://getAllUsers
                                    msg = getAllUsers();//
                                    break;
                                case "112"://get user information 
                                    msg = getUserInformation(json[1]);//
                                    break;
                                case "113"://sent DB
                                    msg = sentDB();//
                                    break;
                                case "114":
                                    msg = getUserApps(json[1]);//
                                    break;
                                default://400 error
                                    msg = error("code is incorrect");
                                    break;
                            }
                            response(resp, msg, "json");
                            Console.WriteLine(msg);
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

        public string login(string json)
        {
            var user = JsonConvert.DeserializeObject<login>(json);
            Console.WriteLine(user.name);
            if (db.userNameIsExists(con, user.name) && db.passwordIsCorrect(con, user.name, user.password))
            {
                okLogin test = new okLogin()
                {
                    name = user.name,
                    mail = db.getMailForUser(con, user.name),
                    appList = db.getUserApplications(con, user.name),
                    key = db.getLevelKey(con, user.name)
                }; 
                db.updateStatus(con, user.name, "online");

                return "201&" + JsonConvert.SerializeObject(test);
            }
            else
            {
                return error("Username or password incorrect");
            }
        }

        public string singup(string json)
        {
            var user = JsonConvert.DeserializeObject<singup>(json);
            //DB
            if (!db.userNameIsExists(con, user.name))
            {
                db.insertVluesToUsers(con, user.name, user.password, user.mail);

                if (user.key == "admin")
                {
                    db.updateUser(con, user.name, user.password, null, null, user.key);
                }
                db.updateStatus(con, user.name, "offline");

                return "202&";
            }
            else
            {
                return error("username is exist");
            }

        }

        public string changeAccount(string json)
        {
            var user = JsonConvert.DeserializeObject<changeAccount>(json);

            string answer = db.updateUser(con, user.userName, user.oldPassword, user.newPassword, user.mail, user.level);

            if (answer != "")
            {
                return error(answer);
            }
            return "203&";
        }

        public string allApps()
        {
            getAllApps msg = new getAllApps()
            {
                allAppList = db.getAllApplications(con)
            };
            return "205&" + JsonConvert.SerializeObject(msg);
        }

        public string deleteAppsFromUser(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteAppFromUser>(json);

            db.deleteAppsFromUser(con, user.userName, user.appName);

            return "207&";
        }

        public string addAppForUser(string json)
        {
            var user = JsonConvert.DeserializeObject<addAppForUser>(json);
            db.addAppForUser(con, user.userName, user.appName);
            //check if app or user name exist mybe and password
            return "208&";
        }

        public string logout(string json)//return???
        {
            var user = JsonConvert.DeserializeObject<logoutUser>(json);
            db.updateStatus(con, user.userName, "offline");

            return "209&";
        }

        public string deleteUser(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteUser>(json);
            if (db.userNameIsExists(con, user.adminUserName) && db.userNameIsExists(con, user.userNameDelete) && (db.getLevelKey(con, user.adminUserName) == "admin"))
            {
                db.deleteAppsUser(con, user.userNameDelete);
                db.deleteValueFromeTable(con, "USERS", "USERNAME", user.userNameDelete);

                return "210&";
            }
            else
            {
                return error("username is incorrect");
            }

        }

        public string getAllUsers()
        {
            getAllUsers msg = new getAllUsers()
            {
                usersList = db.getAllUsers(con)
            };
            return "211&" + JsonConvert.SerializeObject(msg);
        }

        public string getUserInformation(string json)
        {
            var user = JsonConvert.DeserializeObject<getUserInformation>(json);
            userInformation msg = new userInformation()
            {
                password = db.getPassForUser(con, user.userName),
                mail = db.getMailForUser(con, user.userName),
                key = db.getLevelKey(con, user.userName)
            };
            return "212&" + JsonConvert.SerializeObject(msg);
        }

        public string sentDB()
        {
            jsonSentDB msg = new jsonSentDB
            {
                db = getDB()
            };

            return "213&" + JsonConvert.SerializeObject(msg);
        }

        public string getUserApps(string json)
        {
            var user = JsonConvert.DeserializeObject<getUserInformation>(json);
            getAllApps msg = new getAllApps()
            {
                allAppList = db.getUserApplications(con, user.userName)
            };

            return "214&" + JsonConvert.SerializeObject(msg);

        }

        public string error(string msg)
        {
            error err = new error()
            {
                msg = msg
            };
            return "400&" + JsonConvert.SerializeObject(err);
        }

        internal List<sentDB> getDB()
        {
            var list = new List<sentDB>();
            List<string> userList = db.getAllUsers(con);
            foreach (var user in userList)
            {
                list.Add(new sentDB()
                {
                    ID = db.getIdForUser(con, user),
                    userName = user,
                    password = db.getPassForUser(con, user),
                    mail = db.getMailForUser(con, user),
                    LEVEL_KEY = db.getLevelKey(con, user),
                    STATUS = db.getStatusForUser(con, user)
                });
            }
            return list; 
        }

        public void response(HttpListenerResponse resp, string msg, string ContentType)
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

        private bool IsValidJson(string strInput)
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
    }
}
