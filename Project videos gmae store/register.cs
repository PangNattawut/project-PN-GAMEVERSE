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
using System.Xml.Linq;

namespace Project_videos_gmae_store
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void btnregister_Click(object sender, EventArgs e)
        {
            // (ตรวจสอบชื่อ TextBox ทั้งหมด: name, surname, email, username, password, confirmpassword)

            // --- 1. ตรวจสอบข้อมูลเบื้องต้น ---
            if (string.IsNullOrEmpty(name.Text) ||
                string.IsNullOrEmpty(surname.Text) ||
                string.IsNullOrEmpty(email.Text) ||
                string.IsNullOrEmpty(username.Text) ||
                string.IsNullOrEmpty(password.Text) ||
                string.IsNullOrEmpty(confirmpassword.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- V V V ส่วนที่เพิ่มเข้ามา V V V ---
            // --- 2. ตรวจสอบความยาวรหัสผ่าน (ต้องไม่ต่ำกว่า 8) ---
            if (password.Text.Length < 8)
            {
                MessageBox.Show("รหัสผ่านต้องมีอย่างน้อย 8 ตัวอักษร", "รหัสผ่านสั้นเกินไป", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // หยุดการทำงาน
            }
            // --- ^ ^ ^ จบส่วนที่เพิ่มเข้ามา ^ ^ ^ ---

            // --- 3. ตรวจสอบรหัสผ่านตรงกัน ---
            if (password.Text != confirmpassword.Text)
            {
                MessageBox.Show("รหัสผ่าน และ ยืนยันรหัสผ่าน ไม่ตรงกัน", "รหัสผ่านไม่ตรงกัน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- 4. ตั้งค่าการเชื่อมต่อ ---
            string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();

                // 5.1 ตรวจสอบ Username ซ้ำ
                string checkUser = "SELECT COUNT(*) FROM user WHERE username = @user";
                MySqlCommand cmdUser = new MySqlCommand(checkUser, conn);
                cmdUser.Parameters.AddWithValue("@user", username.Text);
                if (Convert.ToInt32(cmdUser.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Username นี้มีผู้ใช้งานแล้ว", "Username ซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 5.2 ตรวจสอบ Email ซ้ำ
                string checkEmail = "SELECT COUNT(*) FROM user WHERE email = @email";
                MySqlCommand cmdEmail = new MySqlCommand(checkEmail, conn);
                cmdEmail.Parameters.AddWithValue("@email", email.Text);
                if (Convert.ToInt32(cmdEmail.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Email นี้ถูกลงทะเบียนไปแล้ว", "Email ซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // --- 6. ถ้า Username ว่าง, ทำการ INSERT ข้อมูล ---
                string insertQuery = "INSERT INTO user (name, surname, email, username, password, role) " +
                                     "VALUES (@name, @surname, @email, @user, @pass, @role)";

                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);

                insertCmd.Parameters.AddWithValue("@name", name.Text);
                insertCmd.Parameters.AddWithValue("@surname", surname.Text);
                insertCmd.Parameters.AddWithValue("@email", email.Text);
                insertCmd.Parameters.AddWithValue("@user", username.Text);
                insertCmd.Parameters.AddWithValue("@pass", password.Text); // (ควร Hashed)
                insertCmd.Parameters.AddWithValue("@role", "user"); // (บังคับเป็น user)

                int rowsAffected = insertCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("สมัครสมาชิกสำเร็จ!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    login loginForm = new login();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("สมัครสมาชิกไม่สำเร็จ (ไม่สามารถเพิ่มข้อมูลได้)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // (ปุ่มยกเลิก)
            login loginForm = new login();
            loginForm.Show();
            this.Hide();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkpassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkpassword1.Checked)
            {
                // ถ้าถูกติ๊ก (Checked = True) -> ให้ "โชว์" รหัสผ่าน
                // โดยการ "ปิด" การใช้งานตัวซ่อนรหัสผ่าน
                password.UseSystemPasswordChar = false;
                confirmpassword.UseSystemPasswordChar = false;
            }
            else
            {
                // ถ้าไม่ได้ติ๊ก (Checked = False) -> ให้ "ซ่อน" รหัสผ่าน
                // โดยการ "เปิด" การใช้งานตัวซ่อนรหัสผ่าน
                password.UseSystemPasswordChar = true;
                confirmpassword.UseSystemPasswordChar = true;
            }
        }
    }
}

