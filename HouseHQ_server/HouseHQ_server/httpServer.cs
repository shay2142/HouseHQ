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
        public Dictionary<string, string> CODE;
        public SQLiteConnection con;
        public HttpListener listener;
        internal DB db = new DB();
        public string url = "http://+:8080/";
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
            string path = @"HHQ_DB.sqlite";
            string cs = @"URI=file:" + path;

            con = new SQLiteConnection(cs);
            con.Open();

            db.createTables(con);

            CODE = codes();

            var newThread = new System.Threading.Thread(frmNewFormThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();

            remoteApp app = new remoteApp();
            app.laodApp(this);

            url = "http://+:8080/";
            String[] prefixes = { url };
            listener = new HttpListener();
            foreach (string s in prefixes)
            { 
                listener.Prefixes.Add(s);
            }
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
                Console.WriteLine(req.RemoteEndPoint);
                Console.WriteLine();
                
                if ((req.HttpMethod == "POST") && !db.ipIsBlock(con, req.RemoteEndPoint.Address.ToString()))
                {
                    Console.WriteLine("test");
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        string s = reader.ReadToEnd();
                        Console.WriteLine("InputStream: {0}", s);

                        db.insertVluesToLOGS(con, s, "client->server");

                        string[] json = s.Split('&');
                        Console.WriteLine(json[0]);
                        Console.WriteLine(json[1]);

                        string msg = "";
                        if (IsValidJson(json[1]) || (Int32.Parse(json[0]) > 100 && Int32.Parse(json[0]) < 200))
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
                                    msg = changeAccount(json[1]);
                                    break;
                                case "104"://add apps
                                    msg = addAppsToServer(json[1]);
                                    break;
                                case "105"://all apps
                                    msg = allApps();
                                    break;
                                case "106"://delete apps
                                    msg = deleteAppsForServer(json[1]);
                                    break;
                                case "107"://delete apps from user
                                    msg = deleteAppsFromUser(json[1]);
                                    break;
                                case "108"://add apps for user
                                    msg = addAppForUser(json[1]);
                                    break;
                                case "109"://logout
                                    msg = logout(json[1]);
                                    break;
                                case "110"://delete user
                                    msg = deleteUser(json[1]);
                                    break;
                                case "111"://getAllUsers
                                    msg = getAllUsers();
                                    break;
                                case "112"://get user information 
                                    msg = getUserInformation(json[1]);
                                    break;
                                case "113"://sent DB
                                    msg = sentDB();
                                    break;
                                case "114":
                                    msg = getUserApps(json[1]);
                                    break;
                                case "115":
                                    msg = sentLogs();
                                    break;
                                case "116":
                                    msg = addLevelKey(json[1]);
                                    break;
                                case "117":
                                    msg = getLevelKey();
                                    break;
                                case "118":
                                    msg = deleteAppForLevel(json[1]);
                                    break;
                                case "119":
                                    msg = deleteLevel(json[1]);
                                    break;
                                case "120":
                                    msg = updateAppsForLevel(json[1]);
                                    break;
                                case "121":
                                    msg = deleteLogs();
                                    break;
                                case "122":
                                    msg = getAllAppsOnPC();
                                    break;
                                default://400 error
                                    msg = error("code is incorrect");
                                    break;
                            }
                            response(resp, msg, "json");
                            db.insertVluesToLOGS(con, msg, "server->client");
                            Console.WriteLine(msg);
                        }
                        else
                        {
                            response(resp, "the msg is not json", "text");
                        }
                    }
                }
                else if(!db.ipIsBlock(con, req.RemoteEndPoint.Address.ToString()))
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

        public string addAppsToServer(string json)
        {
            var user = JsonConvert.DeserializeObject<addAppsOnServer>(json);

            remoteApp remote = new remoteApp();

            if (db.passwordIsCorrect(con, user.userName, user.password) && db.userNameIsExists(con, user.userName) && db.getLevelKey(con, user.userName) == "admin")
            {
                foreach (var app in user.listApps)
                {
                    remote.createRemoteApp(this, app.pathExeFile, app.nameApp);
                }
                return "204&";
            }
            return error("access is denied");
        }

        public string allApps()
        {
            getAllApps msg = new getAllApps()
            {
                allAppList = db.getAllApplications(con)
            };
            return "205&" + JsonConvert.SerializeObject(msg);
        }

        public string deleteAppsForServer(string json)
        { 
            var user = JsonConvert.DeserializeObject<deleteAppsForServer>(json);

            remoteApp remote = new remoteApp();

            if (db.passwordIsCorrect(con, user.userName, user.password) && db.userNameIsExists(con, user.userName) && db.getLevelKey(con, user.userName) == "admin")
            {
                foreach (string app in user.appsList)
                {
                    if (db.appIsExists(con, app))
                    {
                        remote.deleteRemoteApp(this, app);
                        return "206&";
                    }
                }
            }
            return error("access is denied");
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

        public string sentLogs()
        {
            jsonSentLogs msg = new jsonSentLogs()
            {
                jsonLogs = sentLogsDB()
            };

            return "215&" + JsonConvert.SerializeObject(msg);
        }

        public string addLevelKey(string json)
        {
            var user = JsonConvert.DeserializeObject<addLevelKey>(json);
            if (!db.levelIsExists(con, user.nameLevel))
            {
                db.createLevel(con, user.nameLevel, user.admin);
            }
            db.insertAppToLevel(con, user.nameLevel, user.apps);

            return "216&";
        }

        public string getLevelKey()
        {
            jsonSentLevels msg = new jsonSentLevels()
            {
                jsonLevels = sentLevelsInformation()
            };

            return "217&" + JsonConvert.SerializeObject(msg);
        }

        public string deleteAppForLevel(string json)
        { 
            var user = JsonConvert.DeserializeObject<deleteAppForLevel>(json);
            db.deleteAppForLevel(con, user.nameLevel, user.apps);
            return "218&";
        }

        public string deleteLevel(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteLevel>(json);
            db.deleteLevel(con, user.nameLevel);
            return "219&";
        }

        public string updateAppsForLevel(string json)
        {
            var user = JsonConvert.DeserializeObject<updateAppsForLevel>(json);

            var appList = new List<string>();

            foreach (string app in user.apps)
            {
                if (!db.appIsExistsInLevel(con, user.nameLevel, app))
                {
                    appList.Add(app);
                }
            }
            db.insertAppToLevel(con, user.nameLevel, appList);

            return "220&";
        }

        public string deleteLogs()
        {
            db.deleteTable(con, "LOGS");
            return "221&";
        }

        public string getAllAppsOnPC()
        {
            Apps getApps = new Apps();
            getAllAppsOnPC msg = new getAllAppsOnPC()
            {
                getApps = getApps.getAppsOnPC()
            };
            return "222&" + JsonConvert.SerializeObject(msg);
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

        internal List<sentLogs> sentLogsDB()
        {
            var list = new List<sentLogs>();
            List<int> logsID = db.getAllLogsID(con);
            foreach (int logID in logsID)
            {
                list.Add(new sentLogs()
                {
                    ID = logID,
                    dateLogs = db.getDateForLog(con, logID),
                    typeLog = CODE[db.getCodeForLog(con, logID)],
                    source = db.getSourceForLog(con, logID),
                    log = db.getJ_LOGForLog(con, logID)
                });
            }
            return list;
        }

        internal List<sentLevels> sentLevelsInformation()
        { 
            var list = new List<sentLevels>();
            List<int> levelsID = db.getAllLevelID(con);
            foreach(int levelID in levelsID)
            {
                list.Add(new sentLevels()
                {
                    ID = levelID,
                    name = db.getNameForLevel(con, levelID),
                    admin = Convert.ToBoolean(db.getAdminForLevel(con, levelID)),
                    apps = db.getLevelApplications(con, db.getNameForLevel(con, levelID))
                });
            }
            return list;
        }

        public Dictionary<string, string> codes() 
        {
            return new Dictionary<string, string>()
            {
                {"101", "login"},
                {"102", "singup"},
                {"103", "change account"},
                {"104", "add apps"},
                {"105", "all apps"},
                {"106", "delete apps"},
                {"107", "delete apps from user"},
                {"108", "add apps for user"},
                {"109", "logout"},
                {"110", "delete user"},
                {"111", "getAllUsers"},
                {"112", "getAllUsers"},
                {"113", "sent DB"},
                {"114", "get user apps"},
                {"115", "sent logs"},
                {"116", "add level"},
                {"117", "get level key"},
                {"118", "delete app for level"},
                {"119", "delete level"},
                {"120", "update apps for level"},
                {"121", "delete logs"},
                {"122", "getAllAppsOnPC" },
                {"400", "error"},
                {"201", "ok login"},
                {"202", "ok singup"},
                {"203", "ok change account"},
                {"204", "ok add apps"},
                {"205", "ok all apps"},
                {"206", "ok delete apps"},
                {"207", "ok delete apps from user"},
                {"208", "ok add apps for user"},
                {"209", "ok logout"},
                {"210", "ok delete user"},
                {"211", "ok getAllUsers"},
                {"212", "ok getAllUsers"},
                {"213", "ok sent DB"},
                {"214", "ok get user apps"},
                {"215", "ok sent logs"},
                {"216", "ok add level"},
                {"217", "ok get level key"},
                {"218", "ok delete app for level"},
                {"219", "ok delete level"},
                {"220", "ok update apps for level"},
                {"221", "ok delete logs"},
                {"222", "ok getAllAppsOnPC" }
            };
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
