namespace Project_videos_gmae_store
{
    partial class ProductCard
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
            this.name = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.btnAddToCart = new Guna.UI2.WinForms.Guna2Button();
            this.panelQuantity = new System.Windows.Forms.Panel();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnIncrease = new Guna.UI2.WinForms.Guna2Button();
            this.btnDecrease = new Guna.UI2.WinForms.Guna2Button();
            this.picproduct = new System.Windows.Forms.PictureBox();
            this.panelQuantity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picproduct)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(3, 290);
            this.name.MaximumSize = new System.Drawing.Size(180, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(57, 21);
            this.name.TabIndex = 1;
            this.name.Text = "label1";
            this.name.Click += new System.EventHandler(this.OnCardClick);
            // 
            // price
            // 
            this.price.AutoSize = true;
            this.price.Location = new System.Drawing.Point(3, 356);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(57, 21);
            this.price.TabIndex = 2;
            this.price.Text = "label2";
            this.price.Click += new System.EventHandler(this.OnCardClick);
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
            this.btnAddToCart.Location = new System.Drawing.Point(187, 384);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(92, 33);
            this.btnAddToCart.TabIndex = 3;
            this.btnAddToCart.Text = "หยิบใส่ตะกร้า";
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // panelQuantity
            // 
            this.panelQuantity.Controls.Add(this.lblQuantity);
            this.panelQuantity.Controls.Add(this.btnIncrease);
            this.panelQuantity.Controls.Add(this.btnDecrease);
            this.panelQuantity.Location = new System.Drawing.Point(90, 372);
            this.panelQuantity.Name = "panelQuantity";
            this.panelQuantity.Size = new System.Drawing.Size(91, 49);
            this.panelQuantity.TabIndex = 4;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(34, 12);
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
            this.btnIncrease.Location = new System.Drawing.Point(63, 3);
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
            // picproduct
            // 
            this.picproduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.picproduct.Location = new System.Drawing.Point(0, 0);
            this.picproduct.Margin = new System.Windows.Forms.Padding(5);
            this.picproduct.Name = "picproduct";
            this.picproduct.Size = new System.Drawing.Size(282, 285);
            this.picproduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picproduct.TabIndex = 0;
            this.picproduct.TabStop = false;
            this.picproduct.Click += new System.EventHandler(this.OnCardClick);
            // 
            // ProductCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelQuantity);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.price);
            this.Controls.Add(this.name);
            this.Controls.Add(this.picproduct);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ProductCard";
            this.Size = new System.Drawing.Size(282, 420);
            this.Click += new System.EventHandler(this.OnCardClick);
            this.panelQuantity.ResumeLayout(false);
            this.panelQuantity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picproduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picproduct;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label price;
        private Guna.UI2.WinForms.Guna2Button btnAddToCart;
        private System.Windows.Forms.Panel panelQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private Guna.UI2.WinForms.Guna2Button btnIncrease;
        private Guna.UI2.WinForms.Guna2Button btnDecrease;
    }
}
