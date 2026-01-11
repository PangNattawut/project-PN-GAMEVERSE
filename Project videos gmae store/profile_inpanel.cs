using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Project_videos_gmae_store
{
    public partial class profile_inpanel : Form
    {
        // 1. สร้างตัวแปร (Field) เพื่อ "จำ" username ที่ได้รับมา
        private string currentUserName;

        public profile_inpanel(string username)
        {
            InitializeComponent();

            // 3. "จำ" username ที่รับมาไว้ในตัวแปร
            this.currentUserName = username;
        }

        private void profile_inpanel_Load(object sender, EventArgs e)
        {
            //ตรวจสอบว่ามี username ส่งมาหรือไม่
            if (string.IsNullOrEmpty(this.currentUserName))
            {
                MessageBox.Show("ไม่ได้ระบุ Username!", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 6. โค้ดเชื่อมต่อฐานข้อมูล
            string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 7. สร้างคำสั่ง SQL 
                    // **** (สำคัญ) แก้ชื่อคอลัมน์ "name", "email", "phone", "address"
                    // **** ให้ตรงกับฐานข้อมูล "user" ของคุณ
                    string query = "SELECT name, surname, email, phone, address, image FROM user WHERE username = @user";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", this.currentUserName);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 10. นำข้อมูลจากฐานข้อมูลไปใส่ใน TextBoxes
                                // **** (สำคัญ) แก้ชื่อ TextBoxes

                                // **** ให้ตรงกับหน้าดีไซน์ของคุณ

                                // อ่านค่าแบบป้องกัน "ค่าว่าง" (DBNull)
                                name.Text = reader["name"] != DBNull.Value ? reader.GetString("name") : "";
                                surname.Text = reader["surname"] != DBNull.Value ? reader.GetString("surname") : "";
                                email.Text = reader["email"] != DBNull.Value ? reader.GetString("email") : "";
                                phone.Text = reader["phone"] != DBNull.Value ? reader.GetString("phone") : "";
                                address.Text = reader["address"] != DBNull.Value ? reader.GetString("address") : "";

                                //    (คัดลอกมาจาก profile_update_Load)
                                // (ส่วนนี้จะทำงานได้แล้ว เพราะ SELECT 'image' มาแล้ว)
                                if (reader["image"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["image"];
                                    if (imageData.Length > 0)
                                    {
                                        try
                                        {
                                            using (MemoryStream ms = new MemoryStream(imageData))
                                            {
                                                picimage.Image = Image.FromStream(ms);
                                            }
                                        }
                                        catch (ArgumentException)
                                        {
                                            picimage.Image = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("ไม่พบข้อมูลผู้ใช้นี้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาดฐานข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
