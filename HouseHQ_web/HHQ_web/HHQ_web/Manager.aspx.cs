using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace HHQ_web
{
    public partial class Manager1 : System.Web.UI.Page
    {
        public List<string> apps;
        public string IP;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.setPageName = "MANAGER";
            getData();
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
                Master.UserNamePropertyOnMasterPage = user.name;
                IP = Session["ip"].ToString();
                Master.getIp(IP);
            }
        }

        protected void btnMangar1(object sender, EventArgs e)
        {

        }

        protected void btnContact(object sender, EventArgs e)
        {

        }

        protected void btnApps1(object sender, EventArgs e)
        {
            Response.Redirect("~/Apps.aspx");
        }

        protected void usersManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/usersManager.aspx");
        }

        protected void remoteAppManagement_Click(object sender, EventArgs e)
        {

        }

        protected void levelKeyManagement_Click(object sender, EventArgs e)
        { 
        
        }

        protected void viewLogs_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/logs.aspx");
        }
    }
}