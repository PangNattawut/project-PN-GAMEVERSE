namespace Project_videos_gmae_store
{
    partial class PaymentForm
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
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnPaid = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.picSlip = new System.Windows.Forms.PictureBox();
            this.btnAttachSlip = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlip)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 274);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(57, 21);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "label1";
            // 
            // btnPaid
            // 
            this.btnPaid.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPaid.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPaid.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPaid.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPaid.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnPaid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPaid.ForeColor = System.Drawing.Color.White;
            this.btnPaid.HoverState.FillColor = System.Drawing.Color.Lime;
            this.btnPaid.Location = new System.Drawing.Point(16, 575);
            this.btnPaid.Name = "btnPaid";
            this.btnPaid.Size = new System.Drawing.Size(141, 45);
            this.btnPaid.TabIndex = 2;
            this.btnPaid.Text = "ฉันชำระเงินแล้ว";
            this.btnPaid.Click += new System.EventHandler(this.btnPaid_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.HoverState.FillColor = System.Drawing.Color.Red;
            this.btnCancel.Location = new System.Drawing.Point(210, 575);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 45);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // picQRCode
            // 
            this.picQRCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.picQRCode.Location = new System.Drawing.Point(0, 0);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(379, 259);
            this.picQRCode.TabIndex = 0;
            this.picQRCode.TabStop = false;
            // 
            // picSlip
            // 
            this.picSlip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSlip.Location = new System.Drawing.Point(16, 317);
            this.picSlip.Name = "picSlip";
            this.picSlip.Size = new System.Drawing.Size(165, 205);
            this.picSlip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSlip.TabIndex = 3;
            this.picSlip.TabStop = false;
            // 
            // btnAttachSlip
            // 
            this.btnAttachSlip.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAttachSlip.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAttachSlip.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAttachSlip.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAttachSlip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAttachSlip.ForeColor = System.Drawing.Color.White;
            this.btnAttachSlip.Location = new System.Drawing.Point(210, 403);
            this.btnAttachSlip.Name = "btnAttachSlip";
            this.btnAttachSlip.Size = new System.Drawing.Size(141, 45);
            this.btnAttachSlip.TabIndex = 4;
            this.btnAttachSlip.Text = "แนบสลิป";
            this.btnAttachSlip.Click += new System.EventHandler(this.btnAttachSlip_Click);
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 643);
            this.Controls.Add(this.btnAttachSlip);
            this.Controls.Add(this.picSlip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPaid);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.picQRCode);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PaymentForm";
            this.Text = "PaymentForm";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.Label lblTotal;
        private Guna.UI2.WinForms.Guna2Button btnPaid;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private System.Windows.Forms.PictureBox picSlip;
        private Guna.UI2.WinForms.Guna2Button btnAttachSlip;
    }
}