using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Project_videos_gmae_store
{
    public partial class detail_product : Form
    {
        private string productID;
        private int _quantity;
        private string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=projectstore;";

        // (เพิ่ม!) ตัวแปรสำหรับจำ "ราคาต่อชิ้น"
        private decimal _pricePerItem = 0;

        // Property นี้จะทำให้ฟอร์มแม่ (user_index) "ดึง" จำนวนล่าสุดกลับไปได้
        public int Quantity
        {
            get { return _quantity; }
            private set
            {
                _quantity = value;
                lblQuantity.Text = _quantity.ToString(); // อัปเดต Label อัตโนมัติ
            }
        }

        // Constructor (ตัวสร้างฟอร์ม)
        public detail_product(string id, int currentQuantityInCart)
        {
            InitializeComponent();
            this.productID = id;

            LoadProductData(); // โหลดข้อมูล (ชื่อ, รูป, ราคา)

            // ตั้งค่าจำนวนเริ่มต้นตามที่รับมา
            this.Quantity = currentQuantityInCart;
        }

        // (ฟังก์ชันเปล่าๆ นี้จำเป็น เผื่อ Designer เรียก)
        public detail_product()
        {
            InitializeComponent();
        }

        // ใช้เพื่อโหลด รูป, ชื่อ, ราคา, รายละเอียด มาแสดง
        private void LoadProductData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT name, description, price, image FROM product WHERE id = @ProductID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", this.productID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // (*** แก้ชื่อคอนโทรล name, description, ... ให้ตรงกับ Designer.cs ของคุณ ***)
                                this.Text = reader["name"].ToString(); // ตั้งชื่อ Title ของหน้าต่าง

                                this.name.Text = reader["name"].ToString();
                                this.description.Text = reader["description"].ToString();

                                // (แก้ไข) จำราคาไว้
                                this._pricePerItem = Convert.ToDecimal(reader["price"]);
                                this.lblprice.Text = $"฿{this._pricePerItem:N0}";

                                byte[] imageBytes = (byte[])reader["image"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    this.picproduct.Image = Image.FromStream(ms);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลสินค้า: " + ex.Message);
            }
        }

        // --- [แก้ไข!] Event Click สำหรับปุ่ม "หยิบใส่ตะกร้า" (สีฟ้า) ---
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            // --- [แก้ไข!] ---
            // 1. (ตรวจสอบว่าผู้ใช้เลือกจำนวนหรือยัง)
            if (this.Quantity == 0)
            {
                // ถ้ายังไม่เลือก (เป็น 0) ให้แจ้งเตือน
                MessageBox.Show("กรุณากดปุ่ม + เพื่อเลือกจำนวนสินค้าก่อน", "ยังไม่เลือกจำนวน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ออกจากฟังก์ชัน (ไม่ทำอะไรต่อ)
            }

            // 2. (ถ้า Quantity > 0) คำนวณราคารวม
            decimal totalPrice = this.Quantity * this._pricePerItem;

            // 3. แจ้งเตือน (สำเร็จ)
            string message = $"เพิ่ม '{this.name.Text}'\n" +
                             $"จำนวน: {this.Quantity} ชิ้น\n" +
                             $"ราคารวม: {totalPrice:N0} ฿\n\nลงในตะกร้าเรียบร้อย";

            MessageBox.Show(message, "เพิ่มสินค้าแล้ว", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 4. (สำคัญ!) "ปิด" ฟอร์มนี้
            this.Close();
            // (การปิดฟอร์ม จะทำให้ ShowDialog() ใน user_index ทำงานต่อ)
        }

        // --- Event Click สำหรับปุ่ม +/- (ตัวเลือก) ---

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            // (ทำงานแค่ "เพิ่ม" ตัวเลข ไม่ทำอย่างอื่น)
            this.Quantity++;
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            // (ทำงานแค่ "ลด" ตัวเลข ไม่ทำอย่างอื่น)
            if (this.Quantity > 0) // กันไม่ให้ต่ำกว่า 0
            {
                this.Quantity--;
            }
        }
    }
}