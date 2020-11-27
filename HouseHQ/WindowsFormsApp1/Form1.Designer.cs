
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
            this.components = new System.ComponentModel.Container();
            this.ipServer = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // ipServer
            // 
            this.ipServer.Location = new System.Drawing.Point(99, 20);
            this.ipServer.Name = "ipServer";
            this.ipServer.Size = new System.Drawing.Size(100, 20);
            this.ipServer.TabIndex = 1;
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 120);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(187, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "connect";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 161);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.user);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.ipServer);
            this.Name = "login";
            this.Text = "login";
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

