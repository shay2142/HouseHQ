using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace HHQ_web
{

    public partial class login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            localhost.WebService1 login = new localhost.WebService1();
            var jsonLogin = login.login(ipServer.Value, userName.Value, pass.Value);

            if (jsonLogin.error == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Session["json"] = JsonConvert.SerializeObject(jsonLogin.okLogin);
                Session["ip"] = ipServer.Value;
                Response.Redirect("Apps.aspx");

            }
            else if (jsonLogin.okLogin == null)
            { 
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + jsonLogin.error.msg + "')", true);

            }
        }
    }
}