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
    public partial class ProductCard : UserControl
    {
        // 1. สร้างตัวแปร private
        private int _quantity = 0;

        // 2. สร้าง "ช่อง" (Properties)
        public string ProductID { get; set; }
        public string Category { get; set; } // <--- [เพิ่มบรรทัดนี้]

        // 3. สร้าง "สัญญาณ" (Events)
        public event EventHandler QuantityChanged; // (สัญญาณนี้จะถูกส่งโดย btnAddToCart)
        public event EventHandler ProductClicked;

        // 4. สร้าง Properties สำหรับ UI
        // (*** แก้ชื่อ name, picproduct, price, lblQuantity ให้ตรงกับ Design ***)

        public string CardName
        {
            get { return name.Text; }
            set { name.Text = value; }
        }

        public Image ProductImage
        {
            get { return picproduct.Image; }
            // *** [สำคัญ] ตรวจสอบว่าชื่อ 'picproduct' ตรงกับชื่อ PictureBox ของคุณ 100%
            set { picproduct.Image = value; }
        }

        public string ProductPrice
        {
            get { return price.Text; }
            set { price.Text = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                lblQuantity.Text = _quantity.ToString();
            }
        }

        // 5. Constructor (ทำงานตอนสร้าง)
        public ProductCard()
        {
            InitializeComponent();
            this.Quantity = 0; // ตั้งค่าเริ่มต้นเป็น 0
        }

        // 6. Event Click สำหรับปุ่ม (3 ปุ่ม)

        // (ปุ่ม "หยิบใส่ตะกร้า" (สีฟ้า) - ปุ่ม "ยืนยัน")
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            // --- [แก้ไข!] ---
            // (ตรวจสอบว่าผู้ใช้เลือกจำนวนหรือยัง)
            if (this.Quantity == 0)
            {
                // ถ้ายังไม่เลือก (เป็น 0) ให้แจ้งเตือน
                MessageBox.Show("กรุณากดปุ่ม + เพื่อเลือกจำนวนสินค้าก่อน", "ยังไม่เลือกจำนวน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ออกจากฟังก์ชัน (ไม่ทำอะไรต่อ)
            }

            // (ถ้า Quantity > 0)
            // "ส่งสัญญาณ" บอกฟอร์มแม่ (user_index)
            // ว่าผู้ใช้ "ยืนยัน" ที่จะหยิบ 'this.Quantity' ชิ้น
            if (QuantityChanged != null)
                QuantityChanged(this, EventArgs.Empty);
        }

        // (ปุ่ม "+")
        private void btnIncrease_Click(object sender, EventArgs e)
        {
            // (ให้ปุ่มนี้ "เพิ่ม" ตัวเลขอย่างเดียว)
            this.Quantity++;
        }

        // (ปุ่ม "-")
        private void btnDecrease_Click(object sender, EventArgs e)
        {
            if (this.Quantity > 0) // ป้องกันไม่ให้ต่ำกว่า 0
            {
                // (ให้ปุ่มนี้ "ลด" ตัวเลขอย่างเดียว)
                this.Quantity--;
            }
        }

        // 7. Event Click สำหรับการ์ด (ไปหน้า Detail)
        private void OnCardClick(object sender, EventArgs e)
        {
            if (ProductClicked != null)
            {
                ProductClicked(this, EventArgs.Empty);
            }
        }
    }
}