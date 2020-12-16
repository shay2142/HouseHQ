
namespace HouseHQ_server
{
    partial class Form1
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
            this.path = new System.Windows.Forms.Button();
            this.namePath = new System.Windows.Forms.TextBox();
            this.createRemoteApp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(398, 29);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(75, 20);
            this.path.TabIndex = 0;
            this.path.Text = "path";
            this.path.UseVisualStyleBackColor = true;
            this.path.Click += new System.EventHandler(this.path_Click);
            // 
            // namePath
            // 
            this.namePath.Location = new System.Drawing.Point(12, 29);
            this.namePath.Name = "namePath";
            this.namePath.Size = new System.Drawing.Size(380, 20);
            this.namePath.TabIndex = 1;
            // 
            // createRemoteApp
            // 
            this.createRemoteApp.Location = new System.Drawing.Point(479, 29);
            this.createRemoteApp.Name = "createRemoteApp";
            this.createRemoteApp.Size = new System.Drawing.Size(75, 20);
            this.createRemoteApp.TabIndex = 2;
            this.createRemoteApp.Text = "create";
            this.createRemoteApp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.createRemoteApp.UseVisualStyleBackColor = true;
            this.createRemoteApp.Click += new System.EventHandler(this.createRemoteApp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.createRemoteApp);
            this.Controls.Add(this.namePath);
            this.Controls.Add(this.path);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button path;
        private System.Windows.Forms.TextBox namePath;
        private System.Windows.Forms.Button createRemoteApp;
    }
}

