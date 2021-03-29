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
            this.btnAddU = new System.Windows.Forms.Button();
            this.btnRemU = new System.Windows.Forms.Button();
            this.btnChangeDet = new System.Windows.Forms.Button();
            this.btnAddS = new System.Windows.Forms.Button();
            this.btnRemS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddU
            // 
            this.btnAddU.FlatAppearance.BorderSize = 0;
            this.btnAddU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddU.Image = ((System.Drawing.Image)(resources.GetObject("btnAddU.Image")));
            this.btnAddU.Location = new System.Drawing.Point(12, 531);
            this.btnAddU.Name = "btnAddU";
            this.btnAddU.Size = new System.Drawing.Size(52, 50);
            this.btnAddU.TabIndex = 0;
            this.btnAddU.UseVisualStyleBackColor = true;
            // 
            // btnRemU
            // 
            this.btnRemU.FlatAppearance.BorderSize = 0;
            this.btnRemU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemU.Image = ((System.Drawing.Image)(resources.GetObject("btnRemU.Image")));
            this.btnRemU.Location = new System.Drawing.Point(70, 531);
            this.btnRemU.Name = "btnRemU";
            this.btnRemU.Size = new System.Drawing.Size(52, 50);
            this.btnRemU.TabIndex = 1;
            this.btnRemU.UseVisualStyleBackColor = true;
            // 
            // btnChangeDet
            // 
            this.btnChangeDet.FlatAppearance.BorderSize = 2;
            this.btnChangeDet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeDet.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeDet.Image")));
            this.btnChangeDet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChangeDet.Location = new System.Drawing.Point(12, 12);
            this.btnChangeDet.Name = "btnChangeDet";
            this.btnChangeDet.Size = new System.Drawing.Size(197, 44);
            this.btnChangeDet.TabIndex = 2;
            this.btnChangeDet.Text = "Change User Details      ";
            this.btnChangeDet.UseVisualStyleBackColor = true;
            // 
            // btnAddS
            // 
            this.btnAddS.FlatAppearance.BorderSize = 0;
            this.btnAddS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddS.Image = ((System.Drawing.Image)(resources.GetObject("btnAddS.Image")));
            this.btnAddS.Location = new System.Drawing.Point(128, 531);
            this.btnAddS.Name = "btnAddS";
            this.btnAddS.Size = new System.Drawing.Size(52, 50);
            this.btnAddS.TabIndex = 3;
            this.btnAddS.UseVisualStyleBackColor = true;
            // 
            // btnRemS
            // 
            this.btnRemS.FlatAppearance.BorderSize = 0;
            this.btnRemS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemS.Image = ((System.Drawing.Image)(resources.GetObject("btnRemS.Image")));
            this.btnRemS.Location = new System.Drawing.Point(186, 531);
            this.btnRemS.Name = "btnRemS";
            this.btnRemS.Size = new System.Drawing.Size(52, 50);
            this.btnRemS.TabIndex = 4;
            this.btnRemS.UseVisualStyleBackColor = true;
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(977, 593);
            this.Controls.Add(this.btnRemS);
            this.Controls.Add(this.btnAddS);
            this.Controls.Add(this.btnChangeDet);
            this.Controls.Add(this.btnRemU);
            this.Controls.Add(this.btnAddU);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmManager";
            this.Text = "frmAnalytics";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddU;
        private System.Windows.Forms.Button btnRemU;
        private System.Windows.Forms.Button btnChangeDet;
        private System.Windows.Forms.Button btnAddS;
        private System.Windows.Forms.Button btnRemS;
    }
}