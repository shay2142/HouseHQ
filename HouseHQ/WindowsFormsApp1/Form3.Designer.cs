
namespace WindowsFormsApp1
{
    partial class Form3
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
            this.appList = new System.Windows.Forms.ListView();
            this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IconPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // appList
            // 
            this.appList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Title,
            this.Path,
            this.IconPath});
            this.appList.HideSelection = false;
            this.appList.Location = new System.Drawing.Point(0, 0);
            this.appList.Name = "appList";
            this.appList.Size = new System.Drawing.Size(800, 451);
            this.appList.TabIndex = 0;
            this.appList.UseCompatibleStateImageBehavior = false;
            this.appList.View = System.Windows.Forms.View.Tile;
            this.appList.SelectedIndexChanged += new System.EventHandler(this.appList_SelectedIndexChanged);
            // 
            // Title
            // 
            this.Title.Text = "Name";
            // 
            // Path
            // 
            this.Path.Text = "Path";
            // 
            // IconPath
            // 
            this.IconPath.Text = "IconPath";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "kill";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.appList);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView appList;
        public System.Windows.Forms.ColumnHeader Title;
        public System.Windows.Forms.ColumnHeader Path;
        public System.Windows.Forms.ColumnHeader IconPath;
        private System.Windows.Forms.Button button1;
    }
}