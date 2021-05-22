using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace HHQ_web
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        public List<string> apps;
        public string IP;
        public remoteApp remoteApp1 = new remoteApp();

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.setPageName = "APPS";
            getData();
        }

        public void getData()
        {
            if (Session["json"] != null && Session["ip"] != null)
            {
                IP = Session["ip"].ToString();
                Master.getIp(IP);
                var user = JsonConvert.DeserializeObject<okLogin>(Session["json"].ToString());

                localhost.WebService1 getApps = new localhost.WebService1();
                var jsongetApps = getApps.getAllAppsForUser(IP, user.name);
                apps = new List<string>(jsongetApps.allAppList);

                if (user.key == "admin")
                {
                    manager();
                }
                ContactUs();
                //user1.InnerHtml = "Hi there, " + user.name + "!";
                Master.UserNamePropertyOnMasterPage = user.name;

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
            
            ImageButton image = new ImageButton();
            image.ID = "addApp";
            image.ImageUrl = @"~\img\add.png";
            image.AlternateText = "add apps";
            image.CssClass = "mybtn2";
            image.Click += addApp_Click;
            Master.test2 = image;

            Label label1 = new Label();
            label1.Text = "&nbsp&nbsp&nbsp&nbsp&nbsp";
            Master.test3 = label1;

            ImageButton image1 = new ImageButton();
            image1.ID = "delete app";
            image1.ImageUrl = @"~\img\delete.png";
            image1.AlternateText = "delete app";
            image1.CssClass = "mybtn2";
            image1.Click += deleteApp_Click;
            Master.test2 = image1;
        }
        public void addApp_Click(object sender, EventArgs e)
        { 
            
        }

        public void deleteApp_Click(object sender, EventArgs e)
        {

        }

        public void button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            remoteApp1.createRemoteAppFile(IP, button.Text, Master.UserNamePropertyOnMasterPage);
            Response.ContentType = "application/exe";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + button.Text + ".exe");
            Response.TransmitFile(Server.MapPath("~/remoteApp/" + Master.UserNamePropertyOnMasterPage + "_" + button.Text.Replace(" ", "") + ".exe"));
            Response.End();
        }

        protected void btnApps(object sender, EventArgs e)
        {
            getData();
        }

        protected void manager()
        {
            Button button1 = new Button();
            button1.Text = "Manager";
            button1.ID = "Manager";
            button1.CssClass = "mybtn";
            button1.Click += btnManger;
            Master.test1 = button1;
        }

        protected void btnManger(object sender, EventArgs e)
        {
            Response.Redirect("Manager.aspx");
        }

        protected void ContactUs()
        {
            Button button1 = new Button();
            button1.Text = "Contact Us";
            button1.ID = "ContactUs";
            button1.CssClass = "mybtn";
            button1.Click += btnContact;
            Master.test1 = button1;
        }

        protected void btnContact(object sender, EventArgs e)
        {
            
        }
    }
}