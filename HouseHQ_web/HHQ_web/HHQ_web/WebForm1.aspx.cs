using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;

namespace HHQ_web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public List<string> apps;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserName"] != null)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ Session["UserName"].ToString() +"')", true);
            //}
            //if (Session["Pass"] != null)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ Session["Pass"].ToString() +"')", true);
            //    //tbpwd.Text = Session["Pwd"].ToString();
            //}
            getData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            getData();
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
                string result = testLogin.sent(json, Session["ip"].ToString(), "114");
                if (result != null)
                {
                    string[] results = result.Split('&');
                    if (results[0] == "214")
                    {
                        var application = JsonConvert.DeserializeObject<getAllApps>(results[1]);
                        apps = application.allAppList;
                    }
                }
            }
            foreach (var app in apps)
            {
                Panel3.Controls.Add(new Button()
                {
                    ID = "buttonId1",
                    Text = app
                });

                //this.pnlFrame.Controls.Add(new Button()
                //{
                //    ID = "buttonId1",
                //    Text = "Text for new button"
                //});
            }

            //foreach (var app in apps)
            //{
            //    //Button test = new Button();
            //    //test.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            //    //test.Cursor = System.Windows.Forms.Cursors.Hand;
            //    //test.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            //    //test.FlatAppearance.BorderSize = 0;
            //    //test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //    //test.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    //test.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            //    ////test.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image"))); /*test.Image = Image.FromFile(@"C:\Images\Dock.jpg");*/
            //    ////test.Location = new System.Drawing.Point(290, 9);
            //    //test.Margin = new System.Windows.Forms.Padding(2);
            //    //test.Name = app;
            //    //test.Size = new System.Drawing.Size(101, 91);
            //    ////test.TabIndex = 27;
            //    //test.Text = app;
            //    //test.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            //    //test.UseVisualStyleBackColor = true;
            //    //test.UseVisualStyleBackColor = true;
            //    //test.Click += new System.EventHandler(reamoteApp);
            //    //flowLayoutPanel1.Controls.Add(test);
            //}
        }
    }
}