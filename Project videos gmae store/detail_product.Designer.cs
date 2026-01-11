namespace Project_videos_gmae_store
{
    partial class detail_product
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
            this.name = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.Label();
            this.lblprice = new System.Windows.Forms.Label();
            this.picproduct = new System.Windows.Forms.PictureBox();
            this.panelQuantity = new System.Windows.Forms.Panel();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnIncrease = new Guna.UI2.WinForms.Guna2Button();
            this.btnDecrease = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddToCart = new Guna.UI2.WinForms.Guna2Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picproduct)).BeginInit();
            this.panelQuantity.SuspendLayout();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.name.Location = new System.Drawing.Point(40, 9);
            this.name.MaximumSize = new System.Drawing.Size(300, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(65, 22);
            this.name.TabIndex = 1;
            this.name.Text = "label1";
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.description.Location = new System.Drawing.Point(40, 366);
            this.description.MaximumSize = new System.Drawing.Size(300, 0);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(47, 17);
            this.description.TabIndex = 2;
            this.description.Text = "label2";
            // 
            // lblprice
            // 
            this.lblprice.AutoSize = true;
            this.lblprice.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblprice.Location = new System.Drawing.Point(40, 524);
            this.lblprice.Name = "lblprice";
            this.lblprice.Size = new System.Drawing.Size(65, 22);
            this.lblprice.TabIndex = 3;
            this.lblprice.Text = "label3";
            // 
            // picproduct
            // 
            this.picproduct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picproduct.Location = new System.Drawing.Point(43, 63);
            this.picproduct.Name = "picproduct";
            this.picproduct.Size = new System.Drawing.Size(300, 300);
            this.picproduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picproduct.TabIndex = 0;
            this.picproduct.TabStop = false;
            // 
            // panelQuantity
            // 
            this.panelQuantity.Controls.Add(this.lblQuantity);
            this.panelQuantity.Controls.Add(this.btnIncrease);
            this.panelQuantity.Controls.Add(this.btnDecrease);
            this.panelQuantity.Location = new System.Drawing.Point(214, 514);
            this.panelQuantity.Name = "panelQuantity";
            this.panelQuantity.Size = new System.Drawing.Size(93, 46);
            this.panelQuantity.TabIndex = 6;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(34, 11);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(19, 21);
            this.lblQuantity.TabIndex = 7;
            this.lblQuantity.Text = "0";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnIncrease
            // 
            this.btnIncrease.BorderRadius = 10;
            this.btnIncrease.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnIncrease.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnIncrease.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnIncrease.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnIncrease.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnIncrease.ForeColor = System.Drawing.Color.White;
            this.btnIncrease.Location = new System.Drawing.Point(65, 3);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(28, 37);
            this.btnIncrease.TabIndex = 6;
            this.btnIncrease.Text = "+";
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // btnDecrease
            // 
            this.btnDecrease.BorderRadius = 10;
            this.btnDecrease.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDecrease.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDecrease.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDecrease.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDecrease.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDecrease.ForeColor = System.Drawing.Color.White;
            this.btnDecrease.Location = new System.Drawing.Point(0, 3);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(28, 37);
            this.btnDecrease.TabIndex = 5;
            this.btnDecrease.Text = "-";
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BorderRadius = 10;
            this.btnAddToCart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddToCart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddToCart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddToCart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(313, 527);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(85, 33);
            this.btnAddToCart.TabIndex = 5;
            this.btnAddToCart.Text = "หยิบใส่ตะกร้า";
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(10, 560);
            this.panel6.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 10);
            this.panel1.TabIndex = 8;
            // 
            // detail_product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 560);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panelQuantity);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.lblprice);
            this.Controls.Add(this.description);
            this.Controls.Add(this.name);
            this.Controls.Add(this.picproduct);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "detail_product";
            this.Text = "detail_product";
            ((System.ComponentModel.ISupportInitialize)(this.picproduct)).EndInit();
            this.panelQuantity.ResumeLayout(false);
            this.panelQuantity.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picproduct;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Label lblprice;
        private System.Windows.Forms.Panel panelQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private Guna.UI2.WinForms.Guna2Button btnIncrease;
        private Guna.UI2.WinForms.Guna2Button btnDecrease;
        private Guna.UI2.WinForms.Guna2Button btnAddToCart;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel1;
    }
}