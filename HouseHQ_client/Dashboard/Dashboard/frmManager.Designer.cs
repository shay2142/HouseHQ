namespace Dashboard
{
    partial class frmManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.frmLogs = new System.Windows.Forms.Button();
            this.btnRemS = new System.Windows.Forms.Button();
            this.btnAddS = new System.Windows.Forms.Button();
            this.btnChangeDet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // frmLogs
            // 
            this.frmLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.frmLogs.FlatAppearance.BorderSize = 0;
            this.frmLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.frmLogs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.frmLogs.Image = ((System.Drawing.Image)(resources.GetObject("frmLogs.Image")));
            this.frmLogs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.frmLogs.Location = new System.Drawing.Point(9, 50);
            this.frmLogs.Margin = new System.Windows.Forms.Padding(2);
            this.frmLogs.Name = "frmLogs";
            this.frmLogs.Size = new System.Drawing.Size(64, 36);
            this.frmLogs.TabIndex = 5;
            this.frmLogs.Text = "Logs";
            this.frmLogs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.frmLogs.UseVisualStyleBackColor = true;
            this.frmLogs.Click += new System.EventHandler(this.frmLogs_Click);
            // 
            // btnRemS
            // 
            this.btnRemS.FlatAppearance.BorderSize = 0;
            this.btnRemS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemS.Image = ((System.Drawing.Image)(resources.GetObject("btnRemS.Image")));
            this.btnRemS.Location = new System.Drawing.Point(54, 431);
            this.btnRemS.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemS.Name = "btnRemS";
            this.btnRemS.Size = new System.Drawing.Size(39, 41);
            this.btnRemS.TabIndex = 4;
            this.btnRemS.UseVisualStyleBackColor = true;
            // 
            // btnAddS
            // 
            this.btnAddS.FlatAppearance.BorderSize = 0;
            this.btnAddS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddS.Image = ((System.Drawing.Image)(resources.GetObject("btnAddS.Image")));
            this.btnAddS.Location = new System.Drawing.Point(11, 431);
            this.btnAddS.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddS.Name = "btnAddS";
            this.btnAddS.Size = new System.Drawing.Size(39, 41);
            this.btnAddS.TabIndex = 3;
            this.btnAddS.UseVisualStyleBackColor = true;
            // 
            // btnChangeDet
            // 
            this.btnChangeDet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeDet.FlatAppearance.BorderSize = 0;
            this.btnChangeDet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeDet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnChangeDet.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeDet.Image")));
            this.btnChangeDet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangeDet.Location = new System.Drawing.Point(9, 10);
            this.btnChangeDet.Margin = new System.Windows.Forms.Padding(2);
            this.btnChangeDet.Name = "btnChangeDet";
            this.btnChangeDet.Size = new System.Drawing.Size(130, 36);
            this.btnChangeDet.TabIndex = 2;
            this.btnChangeDet.Text = "User Management ";
            this.btnChangeDet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChangeDet.UseVisualStyleBackColor = true;
            this.btnChangeDet.Click += new System.EventHandler(this.btnChangeDet_Click);
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(733, 482);
            this.Controls.Add(this.frmLogs);
            this.Controls.Add(this.btnRemS);
            this.Controls.Add(this.btnAddS);
            this.Controls.Add(this.btnChangeDet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmManager";
            this.Text = "frmAnalytics";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnChangeDet;
        private System.Windows.Forms.Button btnAddS;
        private System.Windows.Forms.Button btnRemS;
        private System.Windows.Forms.Button frmLogs;
    }
}