using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_videos_gmae_store
{
    public partial class CartItemCard : UserControl
    {
        // 1. สร้างตัวแปร private
        private int _quantity;
        private decimal _singleItemPrice; // (ตัวแปรใหม่) เก็บราคาต่อ 1 ชิ้น

        public string ProductID { get; set; }

        // 2. สร้าง "สัญญาณ" (Events)
        public event EventHandler QuantityChanged;
        public event EventHandler ItemRemoved;

        public CartItemCard()
        {
            InitializeComponent();
        }

        // 3. สร้าง "ช่อง" (Properties)
        // *** สำคัญ: แก้ชื่อคอนโทรล (lblName, picImage, ...) ให้ตรงกับที่คุณตั้งไว้ใน Design ***

        public string ItemName // (ใช้ ItemName เพื่อกัน Warning)
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public string ProductDescription
        {
            // *** ตรวจสอบว่า Label ที่คุณเพิ่งสร้าง ชื่อ 'lblDescription' ***
            get { return lblDescription.Text; }
            set { lblDescription.Text = value; }
        }

        public Image ProductImage
        {
            get { return picproduct.Image; }
            set { picproduct.Image = value; }
        }

        // (ลบ Property 'ProductPrice' (string) เก่าทิ้งไป)

        // (Property ใหม่) สำหรับรับ "ราคาต่อ 1 ชิ้น" (เป็นตัวเลข)
        public decimal SingleItemPrice
        {
            get { return _singleItemPrice; }
            set { _singleItemPrice = value; }
        }

        // (แก้ไข) Property 'Quantity'
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                lblQuantity.Text = _quantity.ToString(); // อัปเดต Label จำนวน
                UpdateTotalPriceLabel(); // (สำคัญ!) เรียกอัปเดตราคารวม
            }
        }

        // --- (เพิ่มส่วนนี้เข้าไป) ---
        public decimal TotalPrice
        {
            get { return this.Quantity * this.SingleItemPrice; }
        }
        // --- (สิ้นสุดส่วนที่เพิ่ม) ---

        // (ฟังก์ชันใหม่) สำหรับคำนวณและแสดงราคารวม
        private void UpdateTotalPriceLabel()
        {
            decimal totalPrice = this.Quantity * this.SingleItemPrice;

            // *** แก้ 'lblPrice' ให้เป็นชื่อ Label ราคาของคุณ ***
            lblPrice.Text = $"฿{totalPrice:N0}";
        }

        // 4. Event Click (ปุ่ม +, -, X)
        // (โค้ดส่วนนี้จะทำงานถูกต้อง เพราะมันเรียก 'Quantity' set accessor)

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            Quantity++; // (จะไปเรียก set ของ Quantity -> UpdateTotalPriceLabel() อัตโนมัติ)
            if (QuantityChanged != null)
                QuantityChanged(this, EventArgs.Empty);
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            if (Quantity > 1)
            {
                Quantity--; // (จะไปเรียก set ของ Quantity -> UpdateTotalPriceLabel() อัตโนมัติ)
                if (QuantityChanged != null)
                    QuantityChanged(this, EventArgs.Empty);
            }
            else
            {
                if (ItemRemoved != null)
                    ItemRemoved(this, EventArgs.Empty);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (ItemRemoved != null)
                ItemRemoved(this, EventArgs.Empty);
        }
    }
}