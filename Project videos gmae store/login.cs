using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;


namespace Project_videos_gmae_store
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่าผู้ใช้กรอกข้อมูลครบหรือไม่
            if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(password.Text))
            {
                MessageBox.Show("กรุณากรอก Username และ Password", "เข้าสู่ระบบล้มเหลว", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. ตั้งค่าการเชื่อมต่อฐานข้อมูล
            string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();

                // 3. สร้างคำสั่ง SQL 
                string query = "SELECT role FROM user WHERE username = @user AND password = @pass";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                // 4. ใช้ Parameters 
                cmd.Parameters.AddWithValue("@user", username.Text);
                cmd.Parameters.AddWithValue("@pass", password.Text);

                // 5. ดึงข้อมูล
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    // 6. พบผู้ใช้, ตรวจสอบ role
                    string role = result.ToString();

                    // ---> (นี่คือสิ่งที่คุณกรอกมา: username.Text)
                    string name = username.Text;

                    if (role == "admin")
                    {
                        // ถ้าเป็น Admin
                        MessageBox.Show("ยินดีต้อนรับ Admin!", "เข้าสู่ระบบสำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // **** แก้ไขบรรทัดนี้ ****
                        // ❌ ของเดิม: admin_index adminForm = new admin_index();
                        // ✅ ของใหม่: ส่งชื่อ (name) และบทบาท (role) เข้าไป
                        admin_index adminForm = new admin_index(name, role);

                        adminForm.Show();
                        this.Hide();
                    }
                    else if (role == "user")
                    {
                        // ถ้าเป็น User
                        MessageBox.Show("ยินดีต้อนรับคุณ " + name + "!", "เข้าสู่ระบบสำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // **** แก้ไขบรรทัดนี้ (เผื่อคุณต้องใช้ในหน้า User) ****
                        // ❌ ของเดิม: user_index userForm = new user_index();
                        // ✅ ของใหม่: ส่งชื่อ (name)
                        user_index userForm = new user_index(name); // (ต้องแน่ใจว่า user_index.cs มี Constructor แบบนี้ด้วย)

                        userForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบบทบาทผู้ใช้งานนี้", "ข้อผิดพลาดในการเข้าสู่ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // 7. ไม่พบผู้ใช้ หรือ รหัสผ่านผิด
                    MessageBox.Show("Username หรือ Password ไม่ถูกต้อง", "เข้าสู่ระบบล้มเหลว", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 8. แสดง Error กรณีเชื่อมต่อไม่ได้ หรือ Query ผิด
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "ข้อผิดพลาดฐานข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 9. ปิดการเชื่อมต่อ
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void checkpassword_CheckedChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่า CheckBox ถูกติ๊กหรือไม่
            if (checkpassword.Checked)
            {
                // ถ้าถูกติ๊ก (Checked = True) -> ให้ "โชว์" รหัสผ่าน
                // โดยการ "ปิด" การใช้งานตัวซ่อนรหัสผ่าน
                password.UseSystemPasswordChar = false;
            }
            else
            {
                // ถ้าไม่ได้ติ๊ก (Checked = False) -> ให้ "ซ่อน" รหัสผ่าน
                // โดยการ "เปิด" การใช้งานตัวซ่อนรหัสผ่าน
                password.UseSystemPasswordChar = true;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            // สร้างหน้า Register (หน้าสมัครสมาชิก) ขึ้นมาใหม่
            register registerForm = new register();

            // แสดงหน้า Register
            registerForm.Show();

            // ซ่อนหน้า Login ปัจจุบัน
            this.Hide();
        }

        private void linkLabelForgot_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.ShowDialog(); // ใช้ ShowDialog() เพื่อให้หน้าอื่นค้างไว้
        }
    }
}
                
            
        
    
