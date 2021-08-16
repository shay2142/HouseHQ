
namespace WindowsFormsApp1
{
    partial class login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ipServer = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.remoteapp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ipServer
            // 
            this.ipServer.Location = new System.Drawing.Point(99, 20);
            this.ipServer.Name = "ipServer";
            this.ipServer.Size = new System.Drawing.Size(100, 20);
            this.ipServer.TabIndex = 1;
            this.ipServer.Text = "USER2";
            this.ipServer.TextChanged += new System.EventHandler(this.ipServer_TextChanged);
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(99, 89);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(100, 20);
            this.pass.TabIndex = 3;
            this.pass.TextChanged += new System.EventHandler(this.pass_TextChanged);
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(99, 53);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(100, 20);
            this.user.TabIndex = 2;
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(47, 23);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(17, 13);
            this.IP.TabIndex = 5;
            this.IP.Text = "IP";
            this.IP.Click += new System.EventHandler(this.label1_Click);
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Location = new System.Drawing.Point(12, 53);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(56, 13);
            this.userName.TabIndex = 6;
            this.userName.Text = "user name";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(12, 92);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(52, 13);
            this.password.TabIndex = 7;
            this.password.Text = "password";
            // 
            // remoteapp
            // 
            this.remoteapp.Location = new System.Drawing.Point(12, 150);
            this.remoteapp.Name = "remoteapp";
            this.remoteapp.Size = new System.Drawing.Size(187, 23);
            this.remoteapp.TabIndex = 8;
            this.remoteapp.Text = "connect";
            this.remoteapp.UseVisualStyleBackColor = true;
            this.remoteapp.Click += new System.EventHandler(this.remoteapp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 185);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remoteapp);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.user);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.ipServer);
            this.Name = "login";
            this.Text = "login";
            this.Load += new System.EventHandler(this.login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ipServer;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Button remoteapp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

