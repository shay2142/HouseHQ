﻿using System;
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
                Master.UserNamePropertyOnMasterPage = user.name;
                IP = Session["ip"].ToString();
                Master.getIp(IP);
            }
        }

        public void GetDB()
        {
            localhost.WebService1 getDB = new localhost.WebService1();

            GridView1.DataSource = getDB.getDB(IP).db;
            GridView1.DataBind();

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

        protected void createUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateUsers.aspx");
        }

        protected void deleteUsers_Click(object sender, EventArgs e)
        {

        }

        protected void changeUser_Click(object sender, EventArgs e)
        {

        }
        protected void viewLogs_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/logs.aspx");
        }
    }
}