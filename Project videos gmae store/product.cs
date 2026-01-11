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
    public partial class product : Form
    {

        // สร้าง connection string (เชื่อมกับ database ชื่อ user)
        string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=projectstore;";

        public product()
        {
            InitializeComponent();
        }

        // ฟังก์ชันสำหรับสร้าง Connection (เหมือนเดิม)
        private MySqlConnection databaseConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void showProduct()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                // (แก้ไข) ดึงข้อมูลจากตาราง product
                string sql = "SELECT * FROM product";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

                // (สำคัญ!) สมมติว่า DataGridView ของคุณชื่อ 'dataproduct'
                dataproduct.DataSource = ds.Tables[0];

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
        // --- ^ ^ ^ จบส่วนที่เพิ่มเข้ามา ^ ^ ^ ---

        private void product_Load(object sender, EventArgs e)
        {
            // เรียกฟังก์ชัน showProduct() ตอนฟอร์มโหลด
            showProduct();
        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                string sql = "SELECT * FROM product WHERE name LIKE @search";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@search", "%" + textsearch.Text + "%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
                dataproduct.DataSource = ds.Tables[0];
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

        private void btninsert_Click(object sender, EventArgs e)
        {
            // 1. สร้างหน้าต่าง (Form) insert_product
            // (คุณต้องสร้างฟอร์มนี้ใน Project ของคุณก่อน)
            insert_product addForm = new insert_product();

            // 2. แสดงหน้าต่างแบบ Dialog (จะหยุดฟอร์มนี้ไว้จนกว่าหน้าต่างใหม่จะปิด)
            addForm.ShowDialog();

            // 3. หลังจากที่ฟอร์มเพิ่มข้อมูลปิดไป ให้โหลดข้อมูลสินค้าใหม่
            showProduct();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่ามีการเลือกแถวใน DataGridView หรือไม่
            if (dataproduct.SelectedRows.Count > 0)
            {
                // 2. ดึง ID (หรือ key หลัก) ของแถวที่เลือก
                //    (สำคัญ!) แก้ "id" ให้เป็นชื่อคอลัมน์ Primary Key ของคุณ
                string selectedProductID = dataproduct.SelectedRows[0].Cells["id"].Value.ToString();

                // 3. สร้างหน้าต่าง update_product โดยส่ง ID ของสินค้าที่จะแก้ไขไปด้วย
                // (คุณต้องไปสร้าง Constructor ใน update_product.cs ให้รับ string ID นี้ด้วย)
                update_product editForm = new update_product(selectedProductID);

                // 4. แสดงหน้าต่างแบบ Dialog
                editForm.ShowDialog();

                // 5. หลังจากปิดฟอร์มแก้ไข ให้โหลดข้อมูลใหม่
                showProduct();
            }
            else
            {
                // ถ้าไม่ได้เลือกแถว
                MessageBox.Show("โปรดเลือกสินค้าที่ต้องการแก้ไข", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่ามีการเลือกแถวหรือไม่
            if (dataproduct.SelectedRows.Count > 0)
            {
                // 2. แสดงหน้าต่างยืนยันตามที่คุณต้องการ
                DialogResult result = MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่?",
                                                 "ยืนยันการลบ",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

                // 3. ถ้าผู้ใช้กดยืนยัน (Yes)
                if (result == DialogResult.Yes)
                {
                    // 4. ดึง ID ของแถวที่เลือก
                    // (สำคัญ!) แก้ "id" ให้เป็นชื่อคอลัมน์ Primary Key ของคุณ
                    string selectedProductID = dataproduct.SelectedRows[0].Cells["id"].Value.ToString();

                    // 5. เชื่อมต่อและลบข้อมูล
                    MySqlConnection conn = databaseConnection();
                    try
                    {
                        conn.Open();
                        // ใช้ Parameterized Query (@ProductID) เพื่อป้องกัน SQL Injection
                        string sql = "DELETE FROM product WHERE id = @ProductID";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ProductID", selectedProductID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("ลบข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // 6. โหลดข้อมูลใหม่หลังลบ
                            showProduct();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่จะลบ", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // ถ้าผู้ใช้กด No ก็ไม่ต้องทำอะไร
            }
            else
            {
                // ถ้าไม่ได้เลือกแถว
                MessageBox.Show("โปรดเลือกสินค้าที่ต้องการลบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
