using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHQ_web
{
    public partial class CreateUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            localhost.WebService1 create = new localhost.WebService1();
            string level = "";

            if (userName.Value == "" || pass.Value == "" || conPassword.Value == "" || mail.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields are empty')", true);
            }
            else if (pass.Value == conPassword.Value)
            {
                if (Checkbox1.Checked)
                {
                    level = "admin";
                }
                string msg = create.createUsers(Session["ip"].ToString(), userName.Value, pass.Value, mail.Value, level);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Passwords does not match, Please Re-enter')", true);
                pass.Value = "";
                conPassword.Value = "";
            }
            //Response.Redirect("~/Manager.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Manager.aspx");
        }
    }
}