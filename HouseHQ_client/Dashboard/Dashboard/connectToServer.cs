using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HTTP_CLIENT;

namespace Dashboard
{
    public class connectToServer
    {
        public hash hashPass = new hash();

        public WS_Login login(string ip, string userName, string password)//101
        {
            hash hashPass = new hash();
            var splitList = ip.Split(':');
            if (splitList.Length == 2)
            {
                ip = splitList[0];
            }
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

        public string deleteUser(string ip, string userNameDelete, string adminUserName)//110
        {
            deleteUser delete = new deleteUser
            {
                userNameDelete = userNameDelete,
                adminUserName = adminUserName
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
    }

    //public jsonSentLogs getLogs(string ip)//115
    //{
    //    httpClient testLogin = new httpClient();
    //    string result = testLogin.sent(null, ip, "115");
    //    if (result != null)
    //    {
    //        string[] results = result.Split('&');
    //        if (results[0] == "215")
    //        {
    //            var user = JsonConvert.DeserializeObject<jsonSentLogs>(results[1]);
    //            return user;
    //        }
    //    }
    //    //return new jsonSentLogs() { jsonLogs = new List<sentLogs>()};
    //    return new jsonSentLogs();
    //}
}
