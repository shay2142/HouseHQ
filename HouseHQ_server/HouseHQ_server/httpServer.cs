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
        public Codes codes = new Codes();
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

        /*


         input: 

         output:
         */
        public void frmNewFormThread()
        {
            Application.Run(new Form2(this));
        }

        /*


         input: 

         output:
         */
        public void runServer()
        {
            string path = @"HHQ_DB.sqlite";
            string cs = @"URI=file:" + path;

            con = new SQLiteConnection(cs);
            con.Open();

            db.createTables(con);

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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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
                                case "114"://get user apps
                                    msg = getUserApps(json[1]);
                                    break;
                                case "115"://sent logs db
                                    msg = sentLogs();
                                    break;
                                case "116"://add level key
                                    msg = addLevelKey(json[1]);
                                    break;
                                case "117":// get all levels key
                                    msg = getLevelKey();
                                    break;
                                case "118"://delete app for level key
                                    msg = deleteAppForLevel(json[1]);
                                    break;
                                case "119":// delete level key
                                    msg = deleteLevel(json[1]);
                                    break;
                                case "120"://update app level key
                                    msg = updateAppsForLevel(json[1]);
                                    break;
                                case "121"://delete logs
                                    msg = deleteLogs();
                                    break;
                                case "122"://get all app in server 
                                    msg = getAllAppsOnPC();
                                    break;
                                case "123"://sent msg
                                    msg = sentMsg(json[1]);
                                    break;
                                case "124"://logoff all users
                                    msg = logoffAllUsers();
                                    break;
                                case "125"://logoff user
                                    break;
                                case "126"://get all users conncet to remoteApp
                                    msg = getAllUsersRemoteApp();
                                    break;
                                case "127"://sent app db
                                    msg = sentApp();
                                    break;
                                case "128"://sent blockIp db
                                    msg = sentBLOCKS_IP();
                                    break;
                                case "129"://connect to remote app ? now just app no hhq_web
                                    //sent computer name and open thred remote app manager
                                    break;
                                default://400 error
                                    msg = error("code is incorrect");
                                    break;
                            }
                            response(resp, msg, "json");
                            db.insertVluesToLOGS(con, msg, "server->client");
                            if (json.Length == 3)
                            {
                                updateStatus(json[2]);
                            }
                            Console.WriteLine(msg);
                        }
                        else
                        {
                            response(resp, "the msg is not json", "text");
                        }
                    }
                }
                else if (!db.ipIsBlock(con, req.RemoteEndPoint.Address.ToString()))
                {
                    response(resp, String.Format(pageData), "text/html");
                }

            }
        }

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        public string allApps()
        {
            getAllApps msg = new getAllApps()
            {
                allAppList = db.getAllApplications(con)
            };
            return "205&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        public string deleteAppsFromUser(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteAppFromUser>(json);

            db.deleteAppsFromUser(con, user.userName, user.appName);

            return "207&";
        }

        /*


         input: 

         output:
         */
        public string addAppForUser(string json)
        {
            var user = JsonConvert.DeserializeObject<addAppForUser>(json);
            db.addAppForUser(con, user.userName, user.appName);
            //check if app or user name exist mybe and password
            return "208&";
        }

        /*


         input: 

         output:
         */
        public string logout(string json)//return???
        {
            var user = JsonConvert.DeserializeObject<logoutUser>(json);
            db.updateStatus(con, user.userName, "offline");

            return "209&";
        }

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        public string getAllUsers()
        {
            getAllUsers msg = new getAllUsers()
            {
                usersList = db.getAllUsers(con)
            };
            return "211&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        public string sentDB()
        {
            jsonSentDB msg = new jsonSentDB
            {
                db = db.getDB(con)
            };

            return "213&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
        public string getUserApps(string json)
        {
            var user = JsonConvert.DeserializeObject<getUserInformation>(json);
            getAllApps msg = new getAllApps()
            {
                allAppList = db.getUserApplications(con, user.userName)
            };

            return "214&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
        public string sentLogs()
        {
            jsonSentLogs msg = new jsonSentLogs()
            {
                jsonLogs = db.sentLogsDB(con)
            };

            return "215&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        public string getLevelKey()
        {
            jsonSentLevels msg = new jsonSentLevels()
            {
                jsonLevels = db.sentLevelsInformation(con)
            };

            return "217&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
        public string deleteAppForLevel(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteAppForLevel>(json);
            db.deleteAppForLevel(con, user.nameLevel, user.apps);
            return "218&";
        }

        /*


         input: 

         output:
         */
        public string deleteLevel(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteLevel>(json);
            db.deleteLevel(con, user.nameLevel);
            return "219&";
        }

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        public string deleteLogs()
        {
            db.deleteTable(con, "LOGS");
            return "221&";
        }

        /*


         input: 

         output:
         */
        public string getAllAppsOnPC()
        {
            Apps getApps = new Apps();
            getAllAppsOnPC msg = new getAllAppsOnPC()
            {
                getApps = getApps.getAppsOnPC()
            };
            return "222&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
        public string sentMsg(string json)
        {
            var user = JsonConvert.DeserializeObject<sentMsg>(json);

            remoteApp_Management remote = new remoteApp_Management();
            remote.sentMsg(remote.getSessionId(user.namePc), user.msg);

            return "223&";
        }

        /*


         input: 

         output:
         */
        public string logoffAllUsers()
        {
            remoteApp_Management remote = new remoteApp_Management();
            remote.logOffAllUsers();
            return "224&";
        }

        /*


         input: 

         output:
         */
        public string getAllUsersRemoteApp()
        {
            remoteApp_Management remote = new remoteApp_Management();

            getAllUsersRemoteApp msg = new getAllUsersRemoteApp()
            {
                users = remote.getAllClientName()
            };
            return "226&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
        public string sentApp()
        {
            jsonSentApp msg = new jsonSentApp()
            {
                jsonApp = db.sentApp(con)
            };

            return "227&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
        public string sentBLOCKS_IP()
        {
            jsonSentBLOCKS_IP msg = new jsonSentBLOCKS_IP()
            {
                jsonBLOCKS_IP = db.sentBLOCKS_IP(con)
            };

            return "228&" + JsonConvert.SerializeObject(msg);
        }

        /*


         input: 

         output:
         */
        public string error(string msg)
        {
            error err = new error()
            {
                msg = msg
            };
            return "400&" + JsonConvert.SerializeObject(err);
        }

        /*


         input: 

         output:
         */
        public void updateStatus(string hashUser)
        {
            hash hashUsers = new hash();
            string userName = hashUsers.getUserNameHash(this, hashUser);
            if (userName != "")
            {
                db.updateStatus(con, userName, "online");
            }
        }

        /*


         input: 

         output:
         */
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

        /*


         input: 

         output:
         */
        private bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) 
            { 
                return false; 
            }

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
