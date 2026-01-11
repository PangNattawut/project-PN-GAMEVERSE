namespace Project_videos_gmae_store
{
    partial class admin_userform
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(admin_userform));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.datauser = new System.Windows.Forms.DataGridView();
            this.btndelete = new Guna.UI2.WinForms.Guna2Button();
            this.textsearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datauser)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 11);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btndelete);
            this.panel2.Controls.Add(this.textsearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 44);
            this.panel2.TabIndex = 1;
            // 
            // datauser
            // 
            this.datauser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datauser.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datauser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.datauser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datauser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datauser.EnableHeadersVisualStyles = false;
            this.datauser.Location = new System.Drawing.Point(0, 55);
            this.datauser.Name = "datauser";
            this.datauser.RowHeadersVisible = false;
            this.datauser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datauser.Size = new System.Drawing.Size(1000, 245);
            this.datauser.TabIndex = 2;
            // 
            // btndelete
            // 
            this.btndelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btndelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btndelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btndelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btndelete.FillColor = System.Drawing.Color.Transparent;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btndelete.Image = ((System.Drawing.Image)(resources.GetObject("btndelete.Image")));
            this.btndelete.Location = new System.Drawing.Point(4, 8);
            this.btndelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(52, 30);
            this.btndelete.TabIndex = 35;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // textsearch
            // 
            this.textsearch.BackColor = System.Drawing.Color.Transparent;
            this.textsearch.BorderRadius = 20;
            this.textsearch.BorderThickness = 0;
            this.textsearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textsearch.DefaultText = "";
            this.textsearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textsearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textsearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textsearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textsearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textsearch.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.textsearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textsearch.IconLeft = ((System.Drawing.Image)(resources.GetObject("textsearch.IconLeft")));
            this.textsearch.Location = new System.Drawing.Point(644, 4);
            this.textsearch.Margin = new System.Windows.Forms.Padding(4);
            this.textsearch.Name = "textsearch";
            this.textsearch.PlaceholderText = "Search here";
            this.textsearch.SelectedText = "";
            this.textsearch.Size = new System.Drawing.Size(352, 34);
            this.textsearch.TabIndex = 0;
            this.textsearch.TextChanged += new System.EventHandler(this.textsearch_TextChanged);
            // 
            // admin_userform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 300);
            this.Controls.Add(this.datauser);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "admin_userform";
            this.Text = "admin_userform";
            this.Load += new System.EventHandler(this.admin_userform_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datauser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2TextBox textsearch;
        private System.Windows.Forms.DataGridView datauser;
        private Guna.UI2.WinForms.Guna2Button btndelete;
    }
}