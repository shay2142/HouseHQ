using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Newtonsoft.Json;

namespace HHQ_web
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        public hash hashPass = new hash();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

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

            string result = testLogin.sent(json, testLogin.hostToIp(ipServer), "101");
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

            string result = testLogin.sent(json, testLogin.hostToIp(ipServer), "101");
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

        [WebMethod]
        public WS_Login login(string ip, string userName, string password)
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
            string result = testLogin.sent(json, testLogin.hostToIp(ip), "101");
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

        [WebMethod]
        public getAllApps getAllAppsForUser(string ip, string userName)
        {
            getUserInformation msg = new getUserInformation()
            {
                userName = userName
            };
            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, testLogin.hostToIp(ip), "114");

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

        [WebMethod]
        public string logout(string ip, string userName)
        {
            logoutUser msg = new logoutUser()
            {
                userName = userName
            };

            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, testLogin.hostToIp(ip), "109");
            if (result != null)
            {
                string[] results = result.Split('&');
                return results[0];
            }
            return "400";
        }
        [WebMethod]
        public jsonSentDB getDB(string ip)
        {
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, testLogin.hostToIp(ip), "113");
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
    }
}
