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
        public string title = "";
        public List<string> addApps = new List<string>();
        public List<string> deleteApps = new List<string>();
        public string userName;
        public bool test = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Controls.Clear();
            Panel2.Controls.Clear();
            title = "";
            Master.setPageName = "APPS";
            getData();
            if(test)
                edit_Click(sender, e);
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
                userName = user.name;
            }

            foreach (var app in apps)
            {
                Button button = new Button();
                button.ID = app + "1";
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
        public void edit_Click(object sender, EventArgs e)
        {
            test = true;
            title = "EDIT APPS";
            localhost.WebService1 allApps = new localhost.WebService1();
            var jsongetApps = allApps.allApps(IP);

            Label label1 = new Label();
            label1.Text = " \r\r\r";
            Panel2.Controls.Add(label1);
            foreach (var app in jsongetApps.allAppList)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.ID = app;
                checkBox.Text = app;
                checkBox.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
                checkBox.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
                checkBox.CssClass = "test1";
                checkBox.Font.Name = "Nirmala UI";
                checkBox.Font.Bold = true;
                checkBox.Font.Size = FontUnit.Point(10);
                checkBox.TabIndex = 10;
                checkBox.CheckedChanged += CheckedChanged;
                if (apps.Contains(app))
                {
                    //checkBox.Enabled = false;
                    checkBox.Checked = true;
                    //test.ForeColor = Color.FromArgb(46, 51, 73);
                }
                Panel2.Controls.Add(checkBox);
                Label label = new Label();
                label.Text = " ";
                Panel2.Controls.Add(label);
            }
            Button button = new Button();
            button.ID = "update";
            button.Text = "UPDATE";
            button.CssClass = "update";
            //button.Click += edit_Click;
            div.Controls.Add(button);
        }

        public void CheckedChanged(object sender, EventArgs e)
        {

            localhost.WebService1 update = new localhost.WebService1();
            CheckBox checkbox = (CheckBox)sender;

            if (checkbox.Checked == true)
            {
                update.addAppForUser(IP, userName, checkbox.Text);

            }
            else if (checkbox.Checked == false)
            {
                update.deleteAppsFromUser(IP, userName, checkbox.Text);
            }
            //getData();
            Page_Load(sender, e);
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