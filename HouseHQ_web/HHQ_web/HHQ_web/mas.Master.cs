using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHQ_web
{
    public partial class mas : System.Web.UI.MasterPage
    {
        public string IP;
        public string page = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            localhost.WebService1 logout = new localhost.WebService1();
            if (logout.logout(IP, userName.InnerHtml) == "209")
            {
                Response.Redirect("~/login.aspx");
            }
        }

        public void getIp(string ip)
        {
            IP = ip;
        }

        public string UserNamePropertyOnMasterPage
        {
            get
            {
                // Get value of control on master page  
                return userName.InnerHtml;
            }
            set
            {
                // Set new value for control on master page  
                userName.InnerHtml = value;
            }
        }

        public Button test1
        {
            set
            {
                // Set new value for control on master page  
                ContentPlaceHolder2.Controls.Add(value);

            }
        }

        public ImageButton test2
        {
            set
            {
                // Set new value for control on master page  
                ContentPlaceHolder1.Controls.Add(value);

            }
        }

        public Label test3
        {
            set
            {
                // Set new value for control on master page  
                ContentPlaceHolder1.Controls.Add(value);

            }
        }

        public string setPageName
        {
            set
            {
                page = value;
            }
        }
    }
}