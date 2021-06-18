﻿using System;
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
        The function opens a form window on a new process

         input: none

         output: none
         */
        public void frmNewFormThread()
        {
            Application.Run(new Form2(this));
        }

        /*
        The function activates the server and connects the DB

         input: none

         output: none
         */
        public void runServer()
        {
            //connect DB
            string path = @"HHQ_DB.sqlite";
            string cs = @"URI=file:" + path;

            con = new SQLiteConnection(cs);
            con.Open();

            db.createTables(con);

            //new form window 
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
        The function finds all the IP addresses of the server and returns them accordingly

         input: none

         output:
            - ip address
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
        The actual function communicates with the customer and returns information according to what he sent

         input: none

         output: Task results
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
                        if (IsValidJson(json[1]) && codes.codes().ContainsKey(json[0]) || codes.codes().ContainsKey(json[0]))
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
                                case "130"://run app - Verification with the DB that the user has access to the software and a login confirmation
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
        The function receives a login request if the user exists The function returns information accordingly if it does not exist it returns an error message.

         input: 
            -string login json

         output:
            - ok login json if all good
            - if not eror json msg
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
                db.updateStatus(con, user.name, "online");//update status to online

                return "201&" + JsonConvert.SerializeObject(test);
            }
            else
            {
                return error("Username or password incorrect");
            }
        }

        /*
        The function receives a singup request does not exist The function returns information depending on whether it exists it returns an error message

         input: 
            - singup json msg

         output:
            - 202 if all good
            - if not error msg
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
        The function receives a changeAccount request

         input: 
            - json msg

         output:
            - 203 if all good
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
        The function receives a request to add applications to the server

         input: 
            - json msg

         output:
            - json msg
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
        The function returns all applications

         input: none

         output:
            - json msg
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
        The function deletes applications from the server

         input: 
            - json msg

         output:
            -json msg
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
        The function deletes applications from the user

         input: 
            - json msg

         output:
            - json msg
         */
        public string deleteAppsFromUser(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteAppFromUser>(json);

            db.deleteAppsFromUser(con, user.userName, user.appName);

            return "207&";
        }

        /*
        The function adds apps to the user

         input: 
            - json msg

         output:
            - json msg
         */
        public string addAppForUser(string json)
        {
            var user = JsonConvert.DeserializeObject<addAppForUser>(json);
            db.addAppForUser(con, user.userName, user.appName);
            //check if app or user name exist mybe and password
            return "208&";
        }

        /*
              The function receives a login request if the user exists The function returns information accordingly if it does not exist it returns an error message.

               input: 
                  -string login json

               output:
                  - ok login json if all good
                  - if not eror json msg
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
                db.updateStatus(con, user.name, "online");//update status to online

                return "201&" + JsonConvert.SerializeObject(test);
            }
            else
            {
                return error("Username or password incorrect");
            }
        }

        /*
        The function receives a singup request does not exist The function returns information depending on whether it exists it returns an error message

         input: 
            - singup json msg

         output:
            - 202 if all good
            - if not error msg
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
        The function receives a changeAccount request

         input: 
            - json msg

         output:
            - 203 if all good
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
        The function receives a request to add applications to the server

         input: 
            - json msg

         output:
            - json msg
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
        The function returns all applications

         input: none

         output:
            - json msg
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
        The function deletes applications from the server

         input: 
            - json msg

         output:
            -json msg
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
        The function deletes applications from the user

         input: 
            - json msg

         output:
            - json msg
         */
        public string deleteAppsFromUser(string json)
        {
            var user = JsonConvert.DeserializeObject<deleteAppFromUser>(json);

            db.deleteAppsFromUser(con, user.userName, user.appName);

            return "207&";
        }

        /*
        The function adds apps to the user

         input: 
            - json msg

         output:
            - json msg
         */
        public string addAppForUser(string json)
        {
            var user = JsonConvert.DeserializeObject<addAppForUser>(json);
            db.addAppForUser(con, user.userName, user.appName);
            //check if app or user name exist mybe and password
            return "208&";
        }

        /*
        The function adds level key applications

         input: 
            - json msg

         output:
            - json msg
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
        The function deletes the logs

         input: none

         output:
            - json msg
         */
        public string deleteLogs()
        {
            db.deleteTable(con, "LOGS");
            return "221&";
        }

        /*
        The function sends all computers connected to the remoteApp

         input: none

         output:
            - json msg
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
        The function sends a message to a specific user

         input: 
            - json msg

         output:
            - json msg
         */
        public string sentMsg(string json)
        {
            var user = JsonConvert.DeserializeObject<sentMsg>(json);

            remoteApp_Management remote = new remoteApp_Management();
            remote.sentMsg(remote.getSessionId(user.namePc), user.msg);

            return "223&";
        }

        /*
        The function disconnects all users from the remoteApp

         input: none
            
         output:
            - json msg
         */
        public string logoffAllUsers()
        {
            remoteApp_Management remote = new remoteApp_Management();
            remote.logOffAllUsers();
            return "224&";
        }

        /*


         input: none

         output:
            - json msg
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
        The function returns all users who are connected to the remoteApp

         input: none

         output:
            - json msg
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
        The function returns all blocked IP addresses

         input: none

         output:
            - json msg
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
        The function returns an error message

         input: 
            - string msg

         output:
            - json msg
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
        The function updates the user status

         input: 
            - string hashUser

         output: none
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
        The function actually returns a response to the customer

         input: 
            - HttpListenerResponse resp
            - string msg
            - string ContentType

         output: none
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
        Analyzes the jsons she receives and checks if the message from the user she received is correct

         input: 
            - string strInput
         output:
            - bool ture or false
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
