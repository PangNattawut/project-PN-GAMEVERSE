namespace Project_videos_gmae_store
{
    partial class CartItemCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CartItemCard));
            this.lblName = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnIncrease = new Guna.UI2.WinForms.Guna2Button();
            this.btnDecrease = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemove = new Guna.UI2.WinForms.Guna2ImageButton();
            this.picproduct = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picproduct)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(275, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(65, 22);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label1";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(943, 216);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(65, 22);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "label1";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(787, 209);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(19, 21);
            this.lblQuantity.TabIndex = 10;
            this.lblQuantity.Text = "1";
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
            this.btnIncrease.Location = new System.Drawing.Point(818, 201);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(28, 37);
            this.btnIncrease.TabIndex = 9;
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
            this.btnDecrease.Location = new System.Drawing.Point(753, 201);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(28, 37);
            this.btnDecrease.TabIndex = 8;
            this.btnDecrease.Text = "-";
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(885, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "ราคา";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnRemove.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnRemove.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRemove.ImageRotate = 0F;
            this.btnRemove.Location = new System.Drawing.Point(1014, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnRemove.Size = new System.Drawing.Size(64, 59);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // picproduct
            // 
            this.picproduct.Location = new System.Drawing.Point(0, 0);
            this.picproduct.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.picproduct.Name = "picproduct";
            this.picproduct.Size = new System.Drawing.Size(266, 255);
            this.picproduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picproduct.TabIndex = 0;
            this.picproduct.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(275, 66);
            this.lblDescription.MaximumSize = new System.Drawing.Size(550, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(65, 22);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "label1";
            // 
            // CartItemCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.btnIncrease);
            this.Controls.Add(this.btnDecrease);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picproduct);
            this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "CartItemCard";
            this.Size = new System.Drawing.Size(1076, 253);
            ((System.ComponentModel.ISupportInitialize)(this.picproduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picproduct;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private Guna.UI2.WinForms.Guna2Button btnIncrease;
        private Guna.UI2.WinForms.Guna2Button btnDecrease;
        private Guna.UI2.WinForms.Guna2ImageButton btnRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDescription;
    }
}
