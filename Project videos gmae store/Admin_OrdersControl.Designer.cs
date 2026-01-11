namespace Project_videos_gmae_store
{
    partial class Admin_OrdersControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.picSlipViewer = new System.Windows.Forms.PictureBox();
            this.btnApprove = new Guna.UI2.WinForms.Guna2Button();
            this.btnReject = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlipViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvOrders.Location = new System.Drawing.Point(0, 0);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(525, 431);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellClick);
            // 
            // picSlipViewer
            // 
            this.picSlipViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSlipViewer.Location = new System.Drawing.Point(631, 35);
            this.picSlipViewer.Name = "picSlipViewer";
            this.picSlipViewer.Size = new System.Drawing.Size(255, 306);
            this.picSlipViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSlipViewer.TabIndex = 1;
            this.picSlipViewer.TabStop = false;
            // 
            // btnApprove
            // 
            this.btnApprove.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnApprove.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnApprove.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnApprove.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnApprove.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnApprove.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnApprove.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.HoverState.FillColor = System.Drawing.Color.Lime;
            this.btnApprove.Location = new System.Drawing.Point(525, 389);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(515, 42);
            this.btnApprove.TabIndex = 2;
            this.btnApprove.Text = "อนุมัติ";
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnReject
            // 
            this.btnReject.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReject.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReject.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReject.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReject.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReject.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnReject.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReject.ForeColor = System.Drawing.Color.White;
            this.btnReject.HoverState.FillColor = System.Drawing.Color.Red;
            this.btnReject.Location = new System.Drawing.Point(525, 347);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(515, 42);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "ปฏิเสธ";
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // Admin_OrdersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.picSlipViewer);
            this.Controls.Add(this.dgvOrders);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Admin_OrdersControl";
            this.Size = new System.Drawing.Size(1040, 431);
            this.Load += new System.EventHandler(this.Admin_OrdersControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlipViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.PictureBox picSlipViewer;
        private Guna.UI2.WinForms.Guna2Button btnApprove;
        private Guna.UI2.WinForms.Guna2Button btnReject;
    }
}
