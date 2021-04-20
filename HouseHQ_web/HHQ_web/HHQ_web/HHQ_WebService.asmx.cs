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
    }
}
