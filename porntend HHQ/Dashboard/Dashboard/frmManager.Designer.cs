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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRemS = new System.Windows.Forms.Button();
            this.btnAddS = new System.Windows.Forms.Button();
            this.btnChangeDet = new System.Windows.Forms.Button();
            this.btnRemU = new System.Windows.Forms.Button();
            this.btnAddU = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRemS
            // 
            this.btnRemS.FlatAppearance.BorderSize = 0;
            this.btnRemS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemS.Image = ((System.Drawing.Image)(resources.GetObject("btnRemS.Image")));
            this.btnRemS.Location = new System.Drawing.Point(140, 431);
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
            this.btnAddS.Location = new System.Drawing.Point(96, 431);
            this.btnAddS.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddS.Name = "btnAddS";
            this.btnAddS.Size = new System.Drawing.Size(39, 41);
            this.btnAddS.TabIndex = 3;
            this.btnAddS.UseVisualStyleBackColor = true;
            // 
            // btnChangeDet
            // 
            this.btnChangeDet.FlatAppearance.BorderSize = 2;
            this.btnChangeDet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeDet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnChangeDet.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeDet.Image")));
            this.btnChangeDet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChangeDet.Location = new System.Drawing.Point(9, 10);
            this.btnChangeDet.Margin = new System.Windows.Forms.Padding(2);
            this.btnChangeDet.Name = "btnChangeDet";
            this.btnChangeDet.Size = new System.Drawing.Size(148, 36);
            this.btnChangeDet.TabIndex = 2;
            this.btnChangeDet.Text = "Change User Details      ";
            this.btnChangeDet.UseVisualStyleBackColor = true;
            this.btnChangeDet.Click += new System.EventHandler(this.btnChangeDet_Click);
            // 
            // btnRemU
            // 
            this.btnRemU.FlatAppearance.BorderSize = 0;
            this.btnRemU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemU.Image = ((System.Drawing.Image)(resources.GetObject("btnRemU.Image")));
            this.btnRemU.Location = new System.Drawing.Point(52, 431);
            this.btnRemU.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemU.Name = "btnRemU";
            this.btnRemU.Size = new System.Drawing.Size(39, 41);
            this.btnRemU.TabIndex = 1;
            this.btnRemU.UseVisualStyleBackColor = true;
            // 
            // btnAddU
            // 
            this.btnAddU.FlatAppearance.BorderSize = 0;
            this.btnAddU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddU.Image = ((System.Drawing.Image)(resources.GetObject("btnAddU.Image")));
            this.btnAddU.Location = new System.Drawing.Point(9, 431);
            this.btnAddU.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddU.Name = "btnAddU";
            this.btnAddU.Size = new System.Drawing.Size(39, 41);
            this.btnAddU.TabIndex = 0;
            this.btnAddU.UseVisualStyleBackColor = true;
            this.btnAddU.Click += new System.EventHandler(this.btnAddU_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView1.Location = new System.Drawing.Point(9, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView1.Size = new System.Drawing.Size(712, 375);
            this.dataGridView1.TabIndex = 5;
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(733, 482);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRemS);
            this.Controls.Add(this.btnAddS);
            this.Controls.Add(this.btnChangeDet);
            this.Controls.Add(this.btnRemU);
            this.Controls.Add(this.btnAddU);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmManager";
            this.Text = "frmAnalytics";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddU;
        private System.Windows.Forms.Button btnRemU;
        private System.Windows.Forms.Button btnChangeDet;
        private System.Windows.Forms.Button btnAddS;
        private System.Windows.Forms.Button btnRemS;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}