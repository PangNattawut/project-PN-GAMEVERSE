using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Project_videos_gmae_store
{
    public partial class ForgotPasswordForm : Form
    {
        private string randomCode; // ตัวแปรเก็บรหัส OTP
        private string userEmail;    // ตัวแปรเก็บอีเมล

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่ามีอีเมลนี้ในฐานข้อมูลหรือไม่ (คุณต้องเขียนโค้ดส่วนนี้เอง)
            // สมมติว่า "CheckEmailInDB(txtEmail.Text)" คืนค่า true ถ้าเจอ

            if (CheckEmailInDB(txtEmail.Text))
            {
                userEmail = txtEmail.Text; // เก็บอีเมลไว้

                // 2. สร้างรหัสสุ่ม 6 หลัก
                Random rand = new Random();
                randomCode = rand.Next(100000, 999999).ToString();

                try
                {
                    // 3. ตั้งค่าการส่งอีเมล (ตัวอย่างนี้ใช้ Gmail)
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    // "youremail@gmail.com" คือ อีเมลของ *ร้านค้า* ที่จะใช้ส่ง
                    // "YourAppPassword" คือ รหัส App Password ที่สร้างจาก Google Account (ไม่ใช่รหัสผ่าน Gmail ตรงๆ)
                    // ✅ แก้ไขเป็น (ใส่อีเมล *จริง* ของคุณ):
                    client.Credentials = new NetworkCredential("pang13072547@gmail.com", "bocvoqjnvcjdfznf");

                    MailMessage msg = new MailMessage();
                    msg.To.Add(userEmail); // ส่งไปยังอีเมลที่ลูกค้ากรอก
                                           // ✅ แก้ไขเป็น (ใส่อีเมล *จริง* ของคุณ):
                    msg.From = new MailAddress("pang13072547@gmail.com", "PN GAMEVERSE");
                    msg.Subject = "Your Password Reset Code";
                    msg.Body = "รหัสสำหรับรีเซ็ตรหัสผ่านของคุณคือ: " + randomCode;

                    client.Send(msg); // ส่งอีเมล

                    MessageBox.Show("ส่งรหัสไปยังอีเมลแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. สลับไป Panel ถัดไป
                    panelStep1_EnterEmail.Visible = false;
                    panelStep2_EnterCode.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการส่งอีเมล: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("ไม่พบอีเมลนี้ในระบบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            // ใช้ .Trim() เพื่อตัดช่องว่างหน้า-หลัง ออกก่อนเทียบ
            if (txtCode.Text.Trim() == randomCode)
            {
                MessageBox.Show("ยืนยันรหัสสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // สลับไป Panel ตั้งรหัสใหม่
                panelStep2_EnterCode.Visible = false;
                panelStep3_ResetPassword.Visible = true;
            }
            else
            {
                // *** [แก้ไขแล้ว]: แจ้งข้อผิดพลาดที่เหมาะสมกับผู้ใช้โดยไม่เปิดเผยรหัสที่ถูกต้อง ***
                MessageBox.Show("รหัสไม่ถูกต้อง! กรุณากรอกรหัสที่ได้รับใหม่อีกครั้ง",
                                "ข้อผิดพลาด",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnConfirmReset_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == txtConfirmPassword.Text && txtNewPassword.Text.Length > 0)
            {
                // 1. ทำการ UPDATE รหัสผ่านใหม่ในฐานข้อมูล
                // คุณต้องเขียนโค้ด SQL UPDATE เอง โดยใช้ "userEmail" ที่เราเก็บไว้
                // เช่น "UPDATE Users SET Password = '...' WHERE Email = '...'"
                // (***ควร Hash รหัสผ่านก่อนเก็บด้วย***)

                UpdatePasswordInDB(userEmail, txtNewPassword.Text);

                MessageBox.Show("เปลี่ยนรหัสผ่านสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. ปิดหน้าต่างนี้
                this.Close();
            }
            else if (txtNewPassword.Text.Length == 0)
            {
                MessageBox.Show("รหัสผ่านห้ามว่างเปล่า", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("รหัสผ่านใหม่ไม่ตรงกัน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// (ฉบับแก้ไขสำหรับ MySQL)
        /// ฟังก์ชันสำหรับตรวจสอบว่ามีอีเมลนี้ในฐานข้อมูลหรือไม่
        /// </summary>
        private bool CheckEmailInDB(string email)
        {
            // นี่คือ Connection String ที่ถูกต้องจากไฟล์ login.cs และ register.cs ของคุณ
            string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";

            // ผมเดาชื่อตาราง/คอลัมน์จากไฟล์ register.cs ของคุณ
            string query = "SELECT COUNT(*) FROM user WHERE email = @Email";

            bool emailExists = false;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    try
                    {
                        conn.Open();
                        // ใช้ Convert.ToInt32 เพราะ ExecuteScalar ของ MySQL อาจคืนค่าเป็น Long
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            emailExists = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error (CheckEmailInDB): " + ex.Message);
                    }
                }
            }
            return emailExists;
        }

        /// <summary>
        /// (ฉบับแก้ไขสำหรับ MySQL)
        /// ฟังก์ชันสำหรับอัปเดตรหัสผ่านใหม่ลงฐานข้อมูล
        /// </summary>
        private void UpdatePasswordInDB(string email, string newPassword)
        {
            // นี่คือ Connection String ที่ถูกต้องจากไฟล์ login.cs และ register.cs ของคุณ
            string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";

            // ผมเดาชื่อตาราง/คอลัมน์จากไฟล์ register.cs ของคุณ
            string query = "UPDATE user SET password = @NewPassword WHERE email = @Email";

            // (สำคัญ: คุณควร Hash รหัสผ่านก่อนบันทึก เหมือนที่ควรทำในหน้า register)

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    cmd.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery(); // สั่งอัปเดตข้อมูล
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error (UpdatePasswordInDB): " + ex.Message);
                    }
                }
            }
        }

    }
}
