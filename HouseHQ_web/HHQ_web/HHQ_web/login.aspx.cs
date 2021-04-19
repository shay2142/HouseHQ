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
            hash hashPass = new hash();
            string ip = ipServer.Value;
            var splitList = ipServer.Value.Split(':');
            if (splitList.Length == 2)
            {
                ip = splitList[0];
            }
            login test = new login()
            {
                name = userName.Value,
                password = hashPass.ComputeSha256Hash(pass.Value)
            };
            string json = JsonConvert.SerializeObject(test);
            httpClient testLogin = new httpClient();
            string result = testLogin.sent(json, testLogin.hostToIp(ip), "101");
            if (result != null)
            {
                string[] results = result.Split('&');
                if (results[0] == "201")
                {
                    var user = JsonConvert.DeserializeObject<okLogin>(results[1]);
                    //new Form1(results[1], IP.Text).Show();
                    //this.Hide();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    //textbox value is stored in Session  
                    Session["json"] = results[1];
                    Session["ip"] = ipServer.Value;
                    Response.Redirect("Apps.aspx");

                }
                else if (results[0] == "400")
                {
                    var user = JsonConvert.DeserializeObject<error>(results[1]);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('error')", true);
                }
            }
            
        }
    }
}