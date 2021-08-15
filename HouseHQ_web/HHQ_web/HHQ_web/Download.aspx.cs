using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHQ_web
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string namefile = Request.QueryString["namefile"].ToString();

            Response.ContentType = "application/exe";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + namefile + ".exe");
            Response.TransmitFile(Server.MapPath("~/remoteApp/" + namefile.Replace(" ", "") + ".exe"));
            Response.End();
        }
    }
}