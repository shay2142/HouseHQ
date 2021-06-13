using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace HHQ_web
{
    public partial class logs1 : System.Web.UI.Page
    {
        public string IP;
        public localhost.jsonSentLogs LOGS;
        public string userName;
        public DateTime dateTimeNow;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.page = "LOGS";
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

                userName = JsonConvert.DeserializeObject<okLogin>(Session["json"].ToString()).name;

                localhost.WebService1 getLogs = new localhost.WebService1();
                localhost.jsonSentLogs logs = getLogs.getLogs(IP);

                GridView1.DataSource = logs.jsonLogs;
                GridView1.DataBind();

                LOGS = logs;
            }
        }
        public void logToTxtFile(localhost.jsonSentLogs user)
        {
            dateTimeNow = DateTime.Now;

            using (StreamWriter writer = new StreamWriter(@"C:\Users\shay5\Documents\househq\HouseHQ_web\HHQ_web\HHQ_web\remoteApp\logs_" + userName + ".txt"))
            {
                int maxID = 0;
                int maxDate = 0;
                int maxType = 0;
                int maxSource = 0;
                int maxLog = 0;
                foreach (var log in user.jsonLogs)
                {
                    maxID = maxID < log.ID.ToString().Length ? log.ID.ToString().Length : maxID;
                    maxDate = maxDate < log.dateLogs.ToString().Length ? log.dateLogs.ToString().Length : maxDate;
                    maxType = maxType < log.typeLog.ToString().Length ? log.typeLog.ToString().Length : maxType;
                    maxSource = maxSource < log.source.ToString().Length ? log.source.ToString().Length : maxSource;
                    maxLog = maxLog < log.log.ToString().Length ? log.log.ToString().Length : maxLog;
                }
                writer.WriteLine("ID" + new string(' ', maxID - "ID".Length) + "|Date logs" + new string(' ', maxDate - "Date logs".Length) + "|Type logs" + new string(' ', maxType - "Type logs".Length) + "|Source" + new string(' ', maxSource - "Source".Length) + "|log");
                writer.WriteLine(new string('-', maxID) + "+" + new string('-', maxDate) + "+" + new string('-', maxType) + "+" + new string('-', maxSource) + "+" + new string('-', maxLog));

                foreach (var log in user.jsonLogs)
                {
                    writer.WriteLine(log.ID + new string(' ', maxID - log.ID.ToString().Length) + "|" + log.dateLogs + new string(' ', maxDate - log.dateLogs.ToString().Length) + "|" + log.typeLog + new string(' ', maxType - log.typeLog.ToString().Length) + "|" + log.source + new string(' ', maxSource - log.source.ToString().Length) + "|" + log.log);
                    writer.WriteLine(new string('-', maxID) + "+" + new string('-', maxDate) + "+" + new string('-', maxType) + "+" + new string('-', maxSource) + "+" + new string('-', maxLog));
                }
            }
        }
        protected void btnSaveLogs(object sender, EventArgs e)
        {
            logToTxtFile(LOGS);
            Response.ContentType = "txt";
            Response.AppendHeader("Content-Disposition", "attachment; filename=logs_" + userName + "_" + dateTimeNow.ToString("MM/dd/yyyy-HH:mm") + ".txt");
            Response.TransmitFile(Server.MapPath("~/remoteApp/logs_" + userName + ".txt"));
            Response.End();
        }
        protected void btnDeleteLogs(object sender, EventArgs e)
        {
            logToTxtFile(LOGS);//?
            localhost.WebService1 deleteLogs = new localhost.WebService1();
            deleteLogs.deleteLogs(IP);
            Page_Load(sender, e);
        }
        protected void btnApps(object sender, EventArgs e)
        {
            Response.Redirect("~/Apps.aspx");
        }

        protected void btnContact(object sender, EventArgs e)
        {

        }

        protected void btnMangar(object sender, EventArgs e)
        {
            Response.Redirect("~/Manager.aspx");
        }

        protected void backPage(object sender, EventArgs e)
        {
            Response.Redirect("~/Manager.aspx");
        }
    }
}