using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Project_videos_gmae_store
{
    public partial class admin_userform : Form
    {
        // สร้าง connection string (เชื่อมกับ database ชื่อ user)
        string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=projectstore;";

        public admin_userform()
        {
            InitializeComponent();
        }

        // ฟังก์ชันสำหรับสร้าง Connection
        private MySqlConnection databaseConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        // ฟังก์ชันแสดงข้อมูลผู้ใช้
        private void showuser()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                // 🔄 แก้ไข SQL: กลับไปดึงข้อมูลทั้งหมด (รวม Admin)
                string sql = "SELECT * FROM user";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

                datauser.DataSource = ds.Tables[0];

                // 🔽 ซ่อนคอลัมน์ที่ไม่ต้องการให้แสดง
                if (datauser.Columns.Contains("username"))
                    datauser.Columns["username"].Visible = false;
                if (datauser.Columns.Contains("password"))
                    datauser.Columns["password"].Visible = false;
                if (datauser.Columns.Contains("image"))
                    datauser.Columns["image"].Visible = false;

                datauser.RowHeadersVisible = false;
                datauser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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


        // เป็นฟังก์ชัน search ข้อมูล
        private void admin_userform_Load(object sender, EventArgs e)
        {
            showuser();

        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textsearch.Text.Trim();  // เอาข้อความที่พิมพ์มา

            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                // 🔄 แก้ไข SQL: กลับไปดึงข้อมูลทั้งหมด แล้วค้นหา (รวม Admin ในผลลัพธ์การค้นหาด้วย)
                string sql = "SELECT * FROM user WHERE name LIKE @search";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

                datauser.DataSource = ds.Tables[0];

                // ซ่อนคอลัมน์ username, password, image เหมือนเดิม
                if (datauser.Columns.Contains("username"))
                    datauser.Columns["username"].Visible = false;

                if (datauser.Columns.Contains("password"))
                    datauser.Columns["password"].Visible = false;

                if (datauser.Columns.Contains("image"))
                    datauser.Columns["image"].Visible = false;
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่ามีแถวที่ถูกเลือก (active) หรือไม่
            if (datauser.CurrentRow == null)
            {
                MessageBox.Show("กรุณาเลือกผู้ใช้ที่ต้องการลบ", "ไม่ได้เลือกรายการ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🛡️ ป้องกันการลบ: ตรวจสอบและยกเลิกการลบถ้า role คือ 'admin'
            if (datauser.Columns.Contains("role"))
            {
                string userRole = datauser.CurrentRow.Cells["role"].Value.ToString();
                if (userRole.ToLower() == "admin")
                {
                    MessageBox.Show("ไม่สามารถลบผู้ใช้ที่มีสิทธิ์ Admin ได้ (เพื่อป้องกันความปลอดภัยของระบบ)", "ปฏิเสธการลบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // ยกเลิกการลบ
                }
            }


            // 2. ถามยืนยันการลบ
            DialogResult result = MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่?",
                                                 "ยืนยันการลบ",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            // 3. ถ้าผู้ใช้ไม่กด "Yes" ให้ออกจากฟังก์ชัน
            if (result != DialogResult.Yes)
            {
                return;
            }

            // 4. ถ้ากด "Yes" ให้เริ่มกระบวนการลบ
            MySqlConnection conn = databaseConnection();
            try
            {
                // 5. ดึง ID ของแถวที่กำลังเลือก 
                int userId = Convert.ToInt32(datauser.CurrentRow.Cells["id"].Value);

                conn.Open();

                // 6. สร้างคำสั่ง SQL DELETE โดยอ้างอิงจาก id
                string sql = "DELETE FROM user WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                // 7. สั่งลบข้อมูล
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("ลบข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลที่จะลบ (อาจถูกลบไปแล้ว)", "ล้มเหลว", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();

                // 8. โหลดข้อมูลใน DataGridView ใหม่ (เพื่ออัปเดตตาราง)
                showuser();
            }
        }
    }
}