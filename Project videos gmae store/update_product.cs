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
    public partial class update_product : Form
    {
        private string productID_to_update;
        string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=projectstore;";

        private MySqlConnection databaseConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        public update_product()
        {
            InitializeComponent();
        }

        public update_product(string id_from_main_form)
        {
            InitializeComponent();
            this.productID_to_update = id_from_main_form;
        }

        // --- (แก้ไข) ---
        private void update_product_Load(object sender, EventArgs e)
        {

            // ดึงหมวดหมู่จากฐานข้อมูลมาใส่ ComboBox
            LoadCategories();

            // ดึงข้อมูลสินค้ามาแสดง (และเลือก category ตามชื่อที่ได้)
            LoadDataForUpdate();
        }


        private void LoadDataForUpdate()
        {
            

            if (string.IsNullOrEmpty(this.productID_to_update))
            {
                MessageBox.Show("ไม่ได้ระบุ ID ของสินค้า");
                this.Close();
                return;
            }

            MySqlConnection conn = databaseConnection();
            try
            {
                conn.Open();
                // (สำคัญ!) ตรวจสอบว่าคอลัมน์ category ในตาราง product เป็น "ชื่อ" หรือ "id"
                // โค้ดนี้ดึงมาทุกอย่าง
                string sql = "SELECT name, description, price, quantity, category, image FROM product WHERE id = @ProductID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductID", this.productID_to_update);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    

                    // (สมมติว่า TextBox ชื่อ 'name', 'description', 'price', 'quantity')
                    name.Text = reader["name"].ToString();
                    description.Text = reader["description"].ToString();
                    price.Text = reader["price"].ToString();
                    quantity.Text = reader["quantity"].ToString();

                    // (สำคัญ!)
                    // โค้ดนี้ทำงานได้ ถ้าตาราง product เก็บ "ชื่อ" category (เช่น "Games")
                    string productCategoryName = reader["category"].ToString();
                    category.Text = productCategoryName; // สั่งให้ ComboBox เลือกตาม "ชื่อ"

                    // (ถ้าตาราง product เก็บ "ID" ของ category ให้ใช้บรรทัดนี้แทน)
                    // category.SelectedValue = reader["category_id"].ToString();


                    // --- ส่วนสำหรับโหลดรูปภาพ ---
                    if (reader["image"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["image"];
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            // (สมมติว่า PictureBox ชื่อ 'picimage')
                            picimage.Image = Image.FromStream(ms);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error: ค้นหาข้อมูลไม่เจอ (ID: " + this.productID_to_update + ")");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnselectimage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picimage.Image = Image.FromFile(ofd.FileName);
            }
        }

        // --- (แก้ไขส่วนนี้ทั้งหมด!) ---
        private void btnupdate_Click(object sender, EventArgs e)
        {
            // --- 1. ตรวจสอบข้อมูลเบื้องต้น ---
            if (string.IsNullOrEmpty(name.Text) ||
                string.IsNullOrEmpty(price.Text) ||
                string.IsNullOrEmpty(quantity.Text) ||
                category.SelectedItem == null ||
                picimage.Image == null) // (เพิ่มการตรวจสอบ picimage.Image == null ที่นี่เลย)
            {
                MessageBox.Show("กรุณากรอกข้อมูลและเลือกรูปภาพให้ครบถ้วน", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. ตรวจสอบ Price และ Quantity ว่าเป็นตัวเลขหรือไม่ ---
            decimal priceValue;
            int quantityValue;

            if (!Decimal.TryParse(price.Text, out priceValue))
            {
                MessageBox.Show("กรุณากรอก 'ราคา' เป็นตัวเลขเท่านั้น", "ข้อมูลผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Int32.TryParse(quantity.Text, out quantityValue))
            {
                MessageBox.Show("กรุณากรอก 'จำนวน' เป็นตัวเลขเท่านั้น (จำนวนเต็ม)", "ข้อมูลผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 3. [แก้ไข] แปลงรูปภาพเป็น byte[] (BLOB) (แก้ GDI+ Error) ---
            byte[] imgBytes; // ประกาศตัวแปรไว้นอก using

            // ใช้วิธีนี้เพื่อหลีกเลี่ยง GDI+ Error
            // เราต้องสร้างสำเนา (Bitmap) ของรูปภาพขึ้นมาใหม่ก่อน
            using (MemoryStream ms = new MemoryStream())
            {
                // 1. (สำคัญ!) สร้าง Bitmap "ใหม่" จากรูปใน PictureBox
                //    เพื่อตัดการเชื่อมต่อจากไฟล์เดิม
                using (Bitmap bmp = new Bitmap(picimage.Image))
                {
                    // 2. สั่ง Save "Bitmap ใหม่" (bmp) ลง Stream แทน
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }

                // 3. ดึงข้อมูล byte[]
                imgBytes = ms.ToArray();
            }
            // --- สิ้นสุดส่วนที่แก้ไข ---

            // --- 4. บันทึกลงฐานข้อมูล (แก้ไขเป็น UPDATE) ---
            MySqlConnection conn = databaseConnection();
            try
            {
                conn.Open();

                // (แก้ไข) เปลี่ยนจาก INSERT เป็น UPDATE และเพิ่ม WHERE
                string sql = "UPDATE product SET name = @name, description = @desc, " +
                             "price = @price, quantity = @qty, category = @cat, image = @img " +
                             "WHERE id = @ProductID"; // <-- (สำคัญ!) ต้องระบุ ID ที่จะแก้

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                // ส่ง Parameters
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@desc", description.Text);
                cmd.Parameters.AddWithValue("@price", priceValue);
                cmd.Parameters.AddWithValue("@qty", quantityValue);
                cmd.Parameters.AddWithValue("@cat", category.Text); // (บันทึกเป็น "ชื่อ" category)
                cmd.Parameters.AddWithValue("@img", imgBytes);

                // (สำคัญ!) เพิ่ม Parameter ID สำหรับ WHERE
                cmd.Parameters.AddWithValue("@ProductID", this.productID_to_update);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // (แก้ไข) เปลี่ยนข้อความ
                    MessageBox.Show("แก้ไขสินค้าสำเร็จ!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // ปิดหน้า Update Product
                }
                else
                {
                    MessageBox.Show("แก้ไขสินค้าไม่สำเร็จ (ไม่พบ ID หรือข้อมูลเหมือนเดิม)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void LoadCategories()
        {
            category.Items.Clear();

            try
            {
                using (MySqlConnection conn = databaseConnection())
                {
                    conn.Open();
                    string sql = "SELECT DISTINCT category FROM product WHERE category IS NOT NULL AND TRIM(category) <> '' ORDER BY category";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string cat = rd.IsDBNull(0) ? "" : rd.GetString(0).Trim();
                            if (!string.IsNullOrEmpty(cat) && !category.Items.Contains(cat))
                            {
                                category.Items.Add(cat);
                            }
                        }
                    }
                }

                // ตั้งค่าให้พิมพ์ได้และแนะนำ (ถ้าใช้ standard ComboBox)
                category.DropDownStyle = ComboBoxStyle.DropDown;
                category.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                category.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadCategories error: " + ex.Message);
            }
        }

    }
}