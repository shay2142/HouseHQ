using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace HHQ_web
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [XmlRoot("dictionary")]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        public hash hashPass = new hash();

        /*
        API gat all apps for users

         input: 
            - string ip - ip address to server
            - string userName - user name to login
            - string password - to login
         output:
            - list<string> all apps for users
         */
        [WebMethod]
        public List<string> getAllAppsForUsers(string ipServer, string userName, string password)
        {
            List<string> appsList = new List<string>();

            login login = new login()
            {
                name = userName,
                password = hashPass.ComputeSha256Hash(password)
            };

            string json = JsonConvert.SerializeObject(login);
            httpClient testLogin = new httpClient();

            string result = testLogin.sent(json, ipServer, "101");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "201")
                {
                    var user = JsonConvert.DeserializeObject<okLogin>(results[1]);
                    appsList = user.appList;
                }
                else if (results[0] == "400")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    appsList.Add(user.msg);
                }
            }
            else
            {
                appsList.Add("not found server");
            }
            return appsList;
        }

        /*
        run app get url to Download app and connect in remoteApp

         input: 
            - string ip - ip address to server
            - string userName - to login
            - string password - to login
            - string appName - to run app

         output:
            - string url to Download app to connect
         */
        [WebMethod]
        public string runApp(string ipServer, string userName, string password, string appName)
        {
            remoteApp remoteApp1 = new remoteApp();
            string host = HttpContext.Current.Request.Url.Authority;

            List<string> appsList = new List<string>();

            login login = new login()
            {
                name = userName,
                password = hashPass.ComputeSha256Hash(password)
            };

            string json = JsonConvert.SerializeObject(login);
            httpClient testLogin = new httpClient();

            string result = testLogin.sent(json, ipServer, "101");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "201")
                {
                    var user = JsonConvert.DeserializeObject<okLogin>(results[1]);
                    appsList = user.appList;
                    foreach (var app in appsList)
                    {
                        if (app == appName)
                        {
                            remoteApp1.createRemoteAppFile(ipServer, appName, userName);
                            return "https://" + host + "/Download.aspx?namefile=" + userName + "_" + appName.Replace(" ", "");
                        }
                    }
                }
                else if (results[0] == "400")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    return user.msg;
                }
            }
            else
            {
                return "not found server";
            }
            return "";
        }

        /*
        code: 101 - login

         input:
            - string ip - ip address to server
            - string userName - to login
            - string password - to login
         output:
            - object type WS_Login - ok login or error
         */
        [WebMethod]
        public WS_Login login(string ip, string userName, string password)//101
        {
            hash hashPass = new hash();
            login test = new login()
            {
                name = userName,
                password = hashPass.ComputeSha256Hash(password)
            };
            string json = JsonConvert.SerializeObject(test);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, ip, "101");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "201")
                {
                    var user = JsonConvert.DeserializeObject<okLogin>(results[1]);
                    WS_Login WS_okLogin1 = new WS_Login()
                    {
                        okLogin = user
                    };
                    return WS_okLogin1;
                }
                else if (results[0] == "400")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    WS_Login WS_okLogin2 = new WS_Login()
                    {
                        error = user
                    };
                    return WS_okLogin2;
                }
            }
            error err = new error
            {
                msg = "login failed"
            };

            WS_Login WS_okLogin3 = new WS_Login()
            {
                error = err
            };
            return WS_okLogin3;
        }

        /*
        code: 102 - create users

         input: 
            - string ip - ip address to server
            - string userName
            - string password
            - string mail
            - string level key

         output:
            - string msg to user
         */
        [WebMethod]
        public string createUsers(string ip, string userName, string password, string mail, string levelKey)//102
        {
            singup create = new singup()
            {
                name = userName,
                password = hashPass.ComputeSha256Hash(password),
                mail = mail,
                key = levelKey
            };
            string json = JsonConvert.SerializeObject(create);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, ip, "102");

            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "202")
                {
                    return "The details have changed successfully";
                }
                else if (results[0] == "400")
                {
                    return JsonConvert.DeserializeObject<error>(results[1]).msg;
                }
            }
            return "Singup Failed";
        }

        /*
        code: 103 - change account

         input: 
            - string ip - ip address to server   
            - string userName
            - string oldPassword
            - string newPassword
            - string mail
            - string level

         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string changeAccount(string ip, string userName, string oldPassword, string newPassword, string mail, string level)//103
        {
            changeAccount change = new changeAccount()
            {
                userName = userName,
                oldPassword = hashPass.ComputeSha256Hash(oldPassword),
                newPassword = hashPass.ComputeSha256Hash(newPassword),
                mail = mail,
                level = level
            };
            string json = JsonConvert.SerializeObject(change);

            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, ip, "103");
            if (result != null)
            {
                string[] results = result.Split('&');

              
                if (results[0] == "203")
                {
                    return results[0];
                }
                else if (results[0] == "400")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    return user.msg;
                }
            }
            return "";
        }

        /*
        code: 104 add apps to server

         input:
            - string ip - ip address to server
            - string user name
            - string password
            - list<addApps> listApps - list to add apps on server
         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string addAppsToServer(string ip, string userName, string password, List<addApps> listApps)//104
        {
            addAppsOnServer addApps = new addAppsOnServer
            {
                userName = userName,
                password = password,
                listApps = listApps
            };
            string json = JsonConvert.SerializeObject(addApps);
            httpClient connect = new httpClient();
            string result = connect.sent(json, ip, "104");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "204")
                {
                    return results[0];
                }
                else
                {
                    return JsonConvert.DeserializeObject<error>(results[1]).msg;
                }
            }
            return "error";
        }

        /*
        code: 105 - get all apps

         input: 
            - string ip - ip address to server

         output:
            - object type getAllApps - to get all apps
         */
        [WebMethod]
        public getAllApps allApps(string ip)//105
        {
            httpClient connect = new httpClient();
            string result = connect.sent(null, ip, "105");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "205")
                {
                    var user = JsonConvert.DeserializeObject<getAllApps>(results[1]);
                    return user;
                }
            }
            return new getAllApps();
        }

        /*
        code: 106 - delete app for server

         input: 
            - string ip - ip address to server    
            - string userName
            - string password
            - List<string> listApps - list apps to delete RemoteApp

         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string deleteAppsForServer(string ip, string userName, string password, List<string> listApps)//106
        {
            deleteAppsForServer delete = new deleteAppsForServer
            {
                userName = userName,
                password = password,
                appsList = listApps
            };
            string json = JsonConvert.SerializeObject(delete);
            httpClient connect = new httpClient();
            string result = connect.sent(json, ip, "106");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "206")
                {
                    return results[0];
                }
                else
                {
                    return JsonConvert.DeserializeObject<error>(results[1]).msg;
                }
            }
            return "error";
        }

        /*
        code: 107 - delete app from user

         input: 
            - string ip - ip address to server
            - string userName
            --> string password <--
            - string appName - to delete

         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string deleteAppsFromUser(string ip, string userName, string appName)//107
        {
            deleteAppFromUser deleteApp = new deleteAppFromUser
            { 
                userName = userName,
                appName = appName
            };
            string json = JsonConvert.SerializeObject(deleteApp);
            httpClient connect = new httpClient();
            string result = connect.sent(json, ip, "107");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "207")
                {
                    return results[0];
                }
            }
            return "error";
        }

        /*
        code: 108 - add app for user

         input: 
            - string ip - ip address to server
            - string userName
            --> string password <--
            - string appName - to add

        output:
            - string - Message accordingly
         */
        [WebMethod]
        public string addAppForUser(string ip, string userName, string appName)//108
        {
            addAppForUser addApps = new addAppForUser
            { 
                userName = userName,
                appName = appName
            };

            string json = JsonConvert.SerializeObject(addApps);
            httpClient connect = new httpClient();
            string result = connect.sent(json, ip, "108");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "208")
                {
                    return results[0];
                }
            }
            return "error";
        }

        /*
        code: 109 - logout to server

         input:
            - string ip - ip address to server
            - string userName
            --> string password?? <--

         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string logout(string ip, string userName)//109
        {
            logoutUser msg = new logoutUser()
            {
                userName = userName
            };

            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, ip, "109");
            if (result != null)
            {
                string[] results = result.Split('&');
                return results[0];
            }
            return "400";
        }

        /*
        code: 110 - deleteUser

         input: 
            - string ip - ip address to server
            - string userNameDelete
            - string adminUserName
            --> string adminPassword

         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string deleteUser(string ip, string userNameDelete, string adminUserName)//110
        {
            deleteUser delete = new deleteUser
            { 
              userNameDelete = userNameDelete,
              adminUserName =adminUserName
            };
            string json = JsonConvert.SerializeObject(delete);
            httpClient connect = new httpClient();
            string result = connect.sent(json, ip, "110");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "210")
                {
                    return results[0];
                }
            }
            return "error";
        }

        /*
        code: 111 - get all users

         input: 
            - string ip - ip address to server
            --> user & pass - admin?? <--

         output:
            - object type getAllUsers
         */
        [WebMethod]
        public getAllUsers allUsers(string ip)//111
        {
            httpClient connect = new httpClient();
            string result = connect.sent(null, ip, "111");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "211")
                {
                    return JsonConvert.DeserializeObject<getAllUsers>(results[1]);
                }
            }
            return new getAllUsers();
        }

        /*
        code: 112 - get user infornattion

         input: 
            - string ip - ip address to server
            - string userName
            --> user & pass - admin || this user ?? <--

         output:
            - object type userInformation
         */
        [WebMethod]
        public userInformation getUserInformation(string ip, string userName)//112
        {
            getUserInformation userInf = new getUserInformation
            {
                userName = userName
            };
            string json = JsonConvert.SerializeObject(userInf);
            httpClient connect = new httpClient();
            string result = connect.sent(json, ip, "112");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "212")
                {
                    return JsonConvert.DeserializeObject<userInformation>(results[1]);
                }
            }
            return new userInformation();
        }

        /*
        code: 113 - get DB users

         input: 
            - string ip - ip address to server
            --> admin?? <--

         output:
            - object type jsonSentDB
         */
        [WebMethod]
        public jsonSentDB getDB(string ip)//113
        {
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, ip, "113");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "213")
                {
                    var user = JsonConvert.DeserializeObject<jsonSentDB>(results[1]);
                    return user;
                }
            }
            return new jsonSentDB();
        }

        /*
        code: 114

         input: 
            - string ip - ip address to server
            - string userName
            --> pass <--

         output:
            - object type getAllApps
         */
        [WebMethod]
        public getAllApps getAllAppsForUser(string ip, string userName)//114
        {
            getUserInformation msg = new getUserInformation()
            {
                userName = userName
            };
            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, ip, "114");

            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "214")
                {
                    var application = JsonConvert.DeserializeObject<getAllApps>(results[1]);
                    return application;
                }
            }
            getAllApps empty = new getAllApps()
            {
                allAppList = new List<string> { }
            };
            return empty;
        }

        /*
        code: 115 - get logs

         input: 
            - string ip - ip address to server
            --> admin <--

         output:
            - object type jsonSentLogs
         */
        [WebMethod]
        public jsonSentLogs getLogs(string ip)//115
        {
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, ip, "115");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "215")
                {
                    var user = JsonConvert.DeserializeObject<jsonSentLogs>(results[1]);
                    return user;
                }
            }
            return new jsonSentLogs();
        }

        //116 add level key

        //117 get all levels key

        //118 delete app for level key

        //119 update app level key

        /*


         input: 
            - string ip - ip address to server

         output:
         */
        [WebMethod]
        public void deleteLogs(string ip)//121
        {
            httpClient connect = new httpClient();
            connect.sent(null, ip, "121");//check??
        }


        /*
        code: 122 - get all apps on pc

         input: 
            - string ip - ip address to server
            --> admin <--

         output:
            - object type getAllAppsOnPC
         */
        //[WebMethod]
        //public getAllAppsOnPC getAllAppsOnPC(string ip)//122
        //{
        //    httpClient connect = new httpClient();
        //    string result = connect.sent(null, ip, "122");
        //    if (result != null)
        //    {
        //        string[] results = result.Split('&');

        //        if (results[0] == "222")
        //        {
        //            return JsonConvert.DeserializeObject<getAllAppsOnPC>(results[1]);
        //        }
        //    }
        //    return new getAllAppsOnPC();
        //}

        /*
        code: 123 - sent msg to users

         input: 
            - string ip - ip address to server
            - string namePc
            - string msg

         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string sentMsg(string ip, string namePc, string msg)
        {
            sentMsg json = new sentMsg()
            {
                namePc = namePc,
                msg = msg
            };

            httpClient connect = new httpClient();
            string result = connect.sent(JsonConvert.SerializeObject(json), ip, "123");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "223")
                {
                    return results[1];
                }
            }
            return "";
        }

        /*
        code: 124 - log Off All Users

         input: 
            - string ip - ip address to server
            --> admin <--

         output:
            - string - Message accordingly
         */
        [WebMethod]
        public string logOffAllUsers(string ip)
        {
            httpClient connect = new httpClient();
            string result = connect.sent(null, ip, "124");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "224")
                {
                    return results[1];
                }
            }
            return "";
        }

        //125 logoff user

        /*
        code: 126 - get All Users Remote App

         input: 
            - string ip - ip address to server
            --> admin <--

         output:
            - object type getAllUsersRemoteApp
         */
        [WebMethod]
        public getAllUsersRemoteApp getAllUsersRemoteApps(string ip)
        {
            httpClient connect = new httpClient();
            string result = connect.sent(null, ip, "126");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "226")
                {
                    return JsonConvert.DeserializeObject<getAllUsersRemoteApp>(results[1]);
                }
            }
            return new getAllUsersRemoteApp();
        }

        /*
        code: 127 - get App DB

         input: 
            - string ip - ip address to server
            --> admin <--

         output:
            - object type jsonSentApp
         */
        [WebMethod]
        public jsonSentApp getAppDB(string ip)
        {
            httpClient connect = new httpClient();
            string result = connect.sent(null, ip, "127");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "227")
                {
                    return JsonConvert.DeserializeObject<jsonSentApp>(results[1]);
                }
            }
            return new jsonSentApp();
        }

        /*
        code: 128 - get BLOCKS_IP DB

         input: 
            - string ip - ip address to server
            --> admin <--

         output:
            - object type jsonSentBLOCKS_IP
         */
        [WebMethod]
        public jsonSentBLOCKS_IP getBLOCKS_IP_DB(string ip)
        {
            httpClient connect = new httpClient();
            string result = connect.sent(null, ip, "128");
            if (result != null)
            {
                string[] results = result.Split('&');

                if (results[0] == "228")
                {
                    return JsonConvert.DeserializeObject<jsonSentBLOCKS_IP>(results[1]);
                }
            }
            return new jsonSentBLOCKS_IP();
        }
    }
}
