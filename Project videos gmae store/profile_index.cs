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

namespace Project_videos_gmae_store
{
    public partial class profile_index : Form
    {

        // 1. สร้างตัวแปร (Field) เพื่อ "จำ" ชื่อผู้ใช้
        private string currentUserName;
        private string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=projectstore;";

        public profile_index()
        {
            InitializeComponent();
        }

        public profile_index(string name)
        {
            InitializeComponent(); // ต้องเรียกอันนี้ก่อนเสมอ

            // นำค่าที่ได้รับมาไปใส่ใน Label
            lblusername.Text = name;

            this.currentUserName = name;
        }

        // ฟังก์ชันสำหรับโหลดฟอร์มอื่นเข้ามาใน Panel
        private void loadFormIntoPanel(object Form)
        {
            // 1. ตรวจสอบว่า Panel นี้ (panelChile) มีฟอร์มอื่นเปิดค้างอยู่หรือไม่
            if (this.panelChile.Controls.Count > 0)
            {
                // 2. ถ้ามี ให้ลบฟอร์มเก่านั้นทิ้งไปก่อน
                this.panelChile.Controls.RemoveAt(0);
            }

            // 3. ตั้งค่าฟอร์มใหม่ที่จะโหลดเข้ามา
            Form form = Form as Form;
            form.TopLevel = false; // (สำคัญ) ตั้งค่าให้ไม่ใช่ฟอร์มหลัก
            form.Dock = DockStyle.Fill; // (สำคัญ) ตั้งค่าให้ขยายเต็ม Panel

            // 4. เพิ่มฟอร์มใหม่เข้าไปใน Panel
            this.panelChile.Controls.Add(form);
            this.panelChile.Tag = form;

            // 5. แสดงฟอร์ม
            form.Show();
        }

        private void btnprofile_Click(object sender, EventArgs e)
        {
            // เรียกใช้ฟังก์ชันที่เราสร้างไว้
            // โดยส่งหน้า admin_userform ที่สร้างใหม่เข้าไป
            // (this.currentUserName คือตัวแปรที่คุณใช้เก็บชื่อที่รับมาจากหน้า user_index)
            loadFormIntoPanel(new profile_inpanel(this.currentUserName));

            // 2. เปลี่ยน Title
            lblTitle.Text = "Profile";
        }

        private void btnprofile_update_Click(object sender, EventArgs e)
        {
            // 1. สร้าง Child
            profile_update updateView = new profile_update(this.currentUserName);

            // --- V V V เพิ่มบรรทัดนี้ V V V ---
            // (บอกว่า: "ถ้า" updateView "ยิงสัญญาณ" ProfileUpdated)
            // (ให้ไปเรียกฟังก์ชันชื่อ 'OnProfileUpdated' มาทำงาน)
            updateView.ProfileUpdated += OnProfileUpdated;
            // --- ^ ^ ^ ------------------- ^ ^ ^ ---

            // 2. โหลด Child (เหมือนเดิม)
            loadFormIntoPanel(updateView);

            // 3. เปลี่ยน Title (เหมือนเดิม)
            lblTitle.Text = "Update Profile";
        }

        // --- V V V สร้างฟังก์ชันใหม่นี้ด้วย V V V ---
        // (ฟังก์ชันนี้จะทำงาน "ทันที" เมื่อได้รับสัญญาณจาก Child)
        private void OnProfileUpdated(object sender, EventArgs e)
        {
            // (สั่งให้ 'profile_index' (Parent) โหลดรูปตัวเองใหม่)
            LoadProfilePic();
        }
        

        private void btnoder_Click(object sender, EventArgs e)
        {
            ShowOrderHistoryView();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // 1. สร้างหน้า login (login) ขึ้นมาใหม่
            login loginForm = new login();

            // 2. แสดงหน้า loginForm
            loginForm.Show();

            // 3. ซ่อนหน้าปัจจุบัน (หน้าที่คุณกดปุ่ม Logout)
            this.Hide();
        }

        private void imagebtnhome_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่าคุณส่ง "ชื่อ" กลับไป (ไม่ว่าจะจากตัวแปร หรือจาก Label)
            user_index userForm = new user_index(this.currentUserName);

            // สั่งให้แสดง Form ใหม่
            userForm.Show();

            // (ทางเลือก) ถ้าต้องการซ่อนฟอร์มปัจจุบัน
            this.Hide();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // (สร้างฟังก์ชันใหม่นี้ใน profile_index.cs)
        private void LoadProfilePic()
        {
            // (this.currentUserName คือตัวแปรที่คุณ "จำ" ค่า username ไว้)
            if (string.IsNullOrEmpty(this.currentUserName))
            {
                if (string.IsNullOrEmpty(this.currentUserName)) return;
            }

            // (*** นี่คือโค้ดทั้งหมดจาก 'profile_index_Load' ของคุณ ***)
            string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT image FROM user WHERE username = @user";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", this.currentUserName);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            byte[] imageData = (byte[])result;
                            if (imageData.Length > 0)
                            {
                                try
                                {
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        // (ชื่อ PictureBox 'picprofileimage' ของคุณถูกต้อง)
                                        picprofileimage.Image = Image.FromStream(ms);
                                    }
                                }
                                catch (ArgumentException) { picprofileimage.Image = null; }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดรูปโปรไฟล์: " + ex.Message, "ข้อผิดพลาดฐานข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void profile_index_Load(object sender, EventArgs e)
        {
            // (เรียกใช้ฟังก์ชันใหม่)
            LoadProfilePic();
        }

        private void ShowOrderHistoryView()
        {
            // *** สำคัญ: แก้ 'flowLayoutproduct' ให้เป็นชื่อ FlowLayoutPanel ของหน้านั้นๆ ***
            historyFlowPanel.FlowDirection = FlowDirection.TopDown;
            historyFlowPanel.WrapContents = false;
            historyFlowPanel.Controls.Clear();

            int currentUserID = -1;
            try
            {
                // (โค้ดหา user_id - เหมือนเดิม)
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string userQuery = "SELECT id FROM user WHERE username = @username";
                    using (MySqlCommand userCmd = new MySqlCommand(userQuery, conn))
                    {
                        userCmd.Parameters.AddWithValue("@username", this.currentUserName);
                        object result = userCmd.ExecuteScalar();
                        if (result != null) currentUserID = Convert.ToInt32(result);
                    }
                }

                if (currentUserID == -1)
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ใช้");
                    return;
                }

                // --- [แก้ไข SQL Query!] ---
                // (เพิ่ม p.description เข้ามา)
                string query = @"
            SELECT 
                p.name, 
                p.image, 
                p.description, -- <-- [เพิ่มบรรทัดนี้]
                SUM(oi.quantity) AS total_quantity, 
                SUM(oi.quantity * oi.price_at_purchase) AS total_revenue 
            FROM order_items oi 
            JOIN orderproduct o ON oi.order_id = o.order_id 
            JOIN product p ON oi.product_id = p.id
            WHERE o.user_id = @CurrentUserID
            GROUP BY p.id, p.name, p.image, p.description  -- <-- [เพิ่ม p.description]
            ORDER BY p.name;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CurrentUserID", currentUserID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Label lblEmpty = new Label { Text = "คุณยังไม่มีประวัติคำสั่งซื้อ", Font = new Font("Arial", 16), AutoSize = true };
                                historyFlowPanel.Controls.Add(lblEmpty); // (*** แก้ชื่อ Panel ถ้าจำเป็น ***)
                                return;
                            }
                            while (reader.Read())
                            {
                                OrderHistoryItemCard card = new OrderHistoryItemCard();
                                card.Width = historyFlowPanel.ClientSize.Width - 25; // (*** แก้ชื่อ Panel ถ้าจำเป็น ***)

                                // [ส่งข้อมูลเข้า Property]
                                card.ItemName = reader["name"].ToString();

                                // --- [เพิ่มบรรทัดนี้] ---
                                card.ProductDescription = reader["description"].ToString();

                                // (โค้ดแสดงจำนวน/ราคา - เหมือนเดิม)
                                int total_quantity = Convert.ToInt32(reader["total_quantity"]);
                                decimal total_revenue = Convert.ToDecimal(reader["total_revenue"]);
                                card.SetOrderDetails(total_quantity, total_revenue);

                                try
                                {
                                    byte[] imageBytes = (byte[])reader["image"];
                                    using (MemoryStream ms = new MemoryStream(imageBytes))
                                    {
                                        card.ProductImage = Image.FromStream(ms);
                                    }
                                }
                                catch (Exception) { }

                                historyFlowPanel.Controls.Add(card); // (*** แก้ชื่อ Panel ถ้าจำเป็น ***)
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดประวัติคำสั่งซื้อ: " + ex.Message);
            }
        }

        private void aboutus_Click(object sender, EventArgs e)
        {
            // 1. สร้าง "หน้าต่าง" AboutUsForm ขึ้นมา
            // (*** ถ้า Form ของคุณชื่ออื่น ให้แก้ตรงนี้ ***)
            AboutUsForm aboutForm = new AboutUsForm();

            // 2. สั่งให้ "แสดง" แบบ Pop-up (ShowDialog)
            aboutForm.ShowDialog();
        }
    }
}
