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
using System.IO; // <-- เพิ่มบรรทัดนี้

namespace Project_videos_gmae_store
{
    public partial class insert_product : Form
    {

        // สร้าง connection string
        string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=projectstore;";

        public insert_product()
        {
            InitializeComponent();
        }

        // ฟังก์ชันสำหรับสร้าง Connection
        private MySqlConnection databaseConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            // --- 1. ตรวจสอบข้อมูลเบื้องต้น ---
            if (string.IsNullOrEmpty(name.Text) ||
                string.IsNullOrEmpty(price.Text) ||
                string.IsNullOrEmpty(quantity.Text) ||
                picimage.Image == null) // <-- ตรวจสอบ picimage ด้วย
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

            // ใช้วิธีสร้าง Bitmap ใหม่ (เหมือนตอนแก้ update_product)
            using (MemoryStream ms = new MemoryStream())
            {
                // 1. สร้าง Bitmap "ใหม่" (สำเนา) จากรูปใน PictureBox
                using (Bitmap bmp = new Bitmap(picimage.Image))
                {
                    // 2. สั่ง Save "Bitmap ใหม่" (bmp) ลง Stream
                    //    (แก้ไข) เปลี่ยนจาก RawFormat เป็น Png เพื่อความเสถียร
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }

                // 3. ดึงข้อมูล byte[]
                imgBytes = ms.ToArray();
            }
            // --- สิ้นสุดส่วนที่แก้ไข ---

            // --- 4. บันทึกลงฐานข้อมูล ---
            MySqlConnection conn = databaseConnection();
            try
            {
                conn.Open();

                string sql = "INSERT INTO product (name, description, price, quantity, category, image) " +
                             "VALUES (@name, @desc, @price, @qty, @cat, @img)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                // ส่ง Parameters
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@desc", description.Text);
                cmd.Parameters.AddWithValue("@price", priceValue);
                cmd.Parameters.AddWithValue("@qty", quantityValue);
                cmd.Parameters.AddWithValue("@cat", category.Text);
                cmd.Parameters.AddWithValue("@img", imgBytes);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {

                    // เพิ่มลง ComboBox ถ้ายังไม่มี
                    if (!category.Items.Contains(category.Text))
                    {
                        category.Items.Add(category.Text);
                    }

                    MessageBox.Show("เพิ่มสินค้าสำเร็จ!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์ม "Insert Product" นี้
            this.Close();
        }

        private void btnselectimage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // --- [แก้ไข] ---
                    // โหลดรูปภาพด้วยวิธีที่ไม่ "ล็อค" ไฟล์
                    // 1. โหลดไฟล์ต้นฉบับเข้ามาใน 'tempImage'
                    using (Image tempImage = Image.FromFile(ofd.FileName))
                    {
                        // 2. "คัดลอก" รูปภาพนั้นสร้างเป็น Bitmap ใหม่
                        //    (ตัว 'picimage.Image' จะไม่ผูกกับไฟล์เดิมอีกต่อไป)
                        picimage.Image = new Bitmap(tempImage);
                    }
                }
                catch (Exception ex)
                {
                    // ถ้าไฟล์ที่เลือกไม่ใช่รูปภาพ หรือไฟล์เสีย (แก้ปัญหา OutOfMemoryException)
                    MessageBox.Show("ไม่สามารถโหลดไฟล์รูปภาพได้: " + ex.Message, "ไฟล์ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void search(string keyword)
        {
            using (MySqlConnection conn = databaseConnection())
            {
                DataSet ds = new DataSet();
                conn.Open();
                string query = "SELECT * FROM product WHERE category LIKE @keyword";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }

                // ลบบรรทัดนี้ออก:
                // showhistLearn.DataSource = ds.Tables[0].DefaultView; 
            }
        }

        private void insert_product_Load(object sender, EventArgs e)
        {
            LoadCategories();   // <-- ต้องเรียกใช้ตรงนี้


            if (category.Items.Count > 0)
                category.SelectedIndex = 0;
        }

        private void LoadCategories()
        {
            category.Items.Clear();
            var acs = new AutoCompleteStringCollection();

            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();
                // ถ้าคุณมีตาราง categories ใช้จากตารางนั้นแทน product
                string sql = "SELECT DISTINCT category FROM product WHERE category IS NOT NULL AND TRIM(category) <> ''";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string cat = rd.GetString(0);
                        if (!category.Items.Contains(cat))
                        {
                            category.Items.Add(cat);
                            acs.Add(cat);
                        }
                    }
                }
            }

            // ตั้งค่าให้พิมพ์ได้และแนะนำรายการ
            category.DropDownStyle = ComboBoxStyle.DropDown;
            category.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            category.AutoCompleteSource = AutoCompleteSource.CustomSource;
            category.AutoCompleteCustomSource = acs;
        }

        private void category_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cat = category.Text.Trim();
                if (!string.IsNullOrEmpty(cat) && !category.Items.Contains(cat))
                {
                    category.Items.Add(cat);

                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        //private void category_TextChanged(object sender, EventArgs e)
        //{
        //search(guna2TextBox1.Text);
        //}
    }
    
}
