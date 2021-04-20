using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace HHQ_web
{
    public partial class Manager : System.Web.UI.Page
    {
        public List<string> apps;
        public string IP;

        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            GetDB();
        }

        public void getData()
        {
            if (Session["json"] != null && Session["ip"] != null)
            {

                var user = JsonConvert.DeserializeObject<okLogin>(Session["json"].ToString());
                if (user.key != "admin")
                {
                    Response.Redirect("~/login.aspx");
                }
                userName.InnerHtml = user.name;
                IP = Session["ip"].ToString();
            }
        }

        public void GetDB()
        {
            var list = new List<getDB>();
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(null, testLogin.hostToIp(IP), "113");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "213")
                {
                    var user = JsonConvert.DeserializeObject<jsonSentDB>(results[1]);
                    list = user.db;
                }
            }
            GridView1.DataSource = list;
            GridView1.DataBind();
        }

        protected void btnMangar(object sender, EventArgs e)
        {

        }

        protected void btnContact(object sender, EventArgs e)
        {

        }

        protected void btnApps(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            logoutUser msg = new logoutUser()
            {
                userName = userName.InnerHtml
            };

            string json = JsonConvert.SerializeObject(msg);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, testLogin.hostToIp("shaypc"), "109");//
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "209")
                {
                    Response.Redirect("~/login.aspx");
                }
            }
        }

        protected void createUsers_Click(object sender, EventArgs e)
        {

        }

        protected void deleteUsers_Click(object sender, EventArgs e)
        {

        }

        protected void changeUser_Click(object sender, EventArgs e)
        {

        }
    }
}