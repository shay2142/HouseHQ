﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace HHQ_web
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        public List<string> apps;
        public string IP;
        public remoteApp remoteApp1 = new remoteApp();

        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
        }
        public void button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            remoteApp1.createRemoteAppFile(IP, button.Text);
            Response.ContentType = "application/exe";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + button.Text + ".exe");
            Response.TransmitFile(Server.MapPath("~/remoteApp/" + button.Text.Replace(" ", "") + ".exe"));
            Response.End();
        }
        public void getData()
        {
            if (Session["json"] != null && Session["ip"] != null)
            {

                var user = JsonConvert.DeserializeObject<okLogin>(Session["json"].ToString());

                getUserInformation msg = new getUserInformation()
                {
                    userName = user.name
                };

                string json = JsonConvert.SerializeObject(msg);
                httpClient testLogin = new httpClient();
                string result = testLogin.sent(json, testLogin.hostToIp(Session["ip"].ToString()), "114");
                if (result != null)
                {
                    string[] results = result.Split('&');
                    if (results[0] == "214")
                    {
                        var application = JsonConvert.DeserializeObject<getAllApps>(results[1]);
                        apps = application.allAppList;
                    }
                }
                if (user.key == "admin")
                {
                    manager();
                }
                ContactUs();
                //user1.InnerHtml = "Hi there, " + user.name + "!";
                userName.InnerHtml = user.name;
                IP = Session["ip"].ToString();
            }
            foreach (var app in apps)
            {
                Button button = new Button();
                button.ID = app;
                button.Text = app;
                button.Click += button_Click; //adding the event
                button.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
                button.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
                button.CssClass = "test1";
                button.Font.Name = "Nirmala UI";
                button.Font.Bold = true;
                button.Font.Size = FontUnit.Point(10);
                button.TabIndex = 10;
                Panel1.Controls.Add(button);
                Label label = new Label();
                label.Text = " ";
                Panel1.Controls.Add(label);

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

        }
        protected void lbl_Click(object sender, EventArgs e)
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
            string result = testLogin.sent(json, testLogin.hostToIp(IP), "109");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "209")
                {
                    Response.Redirect("~/login.aspx");
                }
            }
        }

        protected void btnApps(object sender, EventArgs e)
        {
            getData();
        }

        protected void btnManger(object sender, EventArgs e)
        {
            Response.Redirect("Manager.aspx");
        }

        protected void btnContact(object sender, EventArgs e)
        {

        }

        protected void manager()
        {
            Button button1 = new Button();
            button1.Text = "Manager";
            button1.ID = "Manager";
            button1.CssClass = "mybtn";
            button1.Click += btnManger;
            mySidenav.Controls.Add(button1);
        }
        protected void ContactUs()
        {
            Button button1 = new Button();
            button1.Text = "Contact Us";
            button1.ID = "ContactUs";
            button1.CssClass = "mybtn";
            button1.Click += btnContact;
            mySidenav.Controls.Add(button1);
        }
    }
}