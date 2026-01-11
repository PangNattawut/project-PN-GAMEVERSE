using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging; // (จำเป็นสำหรับ ImageFormat)
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project_videos_gmae_store
{
    public partial class profile_update : Form
    {
        // 1. สร้างตัวแปร (Field) เพื่อ "จำ" username ที่ได้รับมา
        private string currentUserName;
        private string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";

        // --- V V V [เพิ่ม!] V V V ---
        // (นี่คือ "สัญญาณ" ที่จะส่งกลับไปหา 'profile_index')
        public event EventHandler ProfileUpdated;
        // --- ^ ^ ^ [เพิ่ม!] ^ ^ ^ ---

        public profile_update(string username)
        {
            InitializeComponent();
            this.currentUserName = username;
        }

        private void profile_update_Load(object sender, EventArgs e)
        {
            // (โค้ดส่วน Load ของคุณ ... สมบูรณ์ดีแล้วครับ)
            if (string.IsNullOrEmpty(this.currentUserName)) return;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT name, surname, email, phone, address, image FROM user WHERE username = @user";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", this.currentUserName);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name.Text = reader["name"] != DBNull.Value ? reader.GetString("name") : "";
                                surname.Text = reader["surname"] != DBNull.Value ? reader.GetString("surname") : "";
                                email.Text = reader["email"] != DBNull.Value ? reader.GetString("email") : "";
                                phone.Text = reader["phone"] != DBNull.Value ? reader.GetString("phone") : "";
                                address.Text = reader["address"] != DBNull.Value ? reader.GetString("address") : "";
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
                                    else { picimage.Image = null; }
                                }
                                else { picimage.Image = null; }
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

        // (โค้ด LoadUserProfile() ของคุณไม่ได้ถูกเรียกใช้... อาจจะลบออกได้)
        // private void LoadUserProfile() { ... }

        private void btnsave_Click(object sender, EventArgs e)
        {
            // (โค้ดส่วน "บันทึกข้อมูล" ของคุณ ... สมบูรณ์ดีแล้วครับ)
            byte[] imageData = null;
            if (picimage.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // (*** แก้ไขเล็กน้อย: บันทึกเป็น Png หรือ Jpeg จะดีกว่า)
                    picimage.Image.Save(ms, ImageFormat.Png);
                    imageData = ms.ToArray();
                }
            }
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE user SET 
                                        name = @newName, 
                                        surname = @newSurname, 
                                        email = @newEmail, 
                                        phone = @newPhone, 
                                        address = @newAddress,
                                        image = @newImage 
                                    WHERE 
                                        username = @userToUpdate";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@newName", name.Text);
                        cmd.Parameters.AddWithValue("@newSurname", surname.Text);
                        cmd.Parameters.AddWithValue("@newEmail", email.Text);
                        cmd.Parameters.AddWithValue("@newPhone", phone.Text);
                        cmd.Parameters.AddWithValue("@newAddress", address.Text);
                        cmd.Parameters.AddWithValue("@userToUpdate", this.currentUserName);

                        if (imageData != null)
                        {
                            cmd.Parameters.AddWithValue("@newImage", imageData);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@newImage", DBNull.Value);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("บันทึกข้อมูลโปรไฟล์สำเร็จ!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // --- V V V [เพิ่ม!] V V V ---
                            // (ยิง "สัญญาณ" บอก 'profile_index' ว่า "อัปเดตเสร็จแล้วนะ!")
                            ProfileUpdated?.Invoke(this, EventArgs.Empty);
                            // --- ^ ^ ^ [เพิ่ม!] ^ ^ ^ ---

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลผู้ใช้ที่จะอัปเดต", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาดฐานข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            // (โค้ดนี้หมายถึง "ปิดหน้าต่างนี้" ซึ่งคือสิ่งที่ปุ่ม Cancel ควรทำ)
            this.Close();
        }

        private void btnselectimage_Click(object sender, EventArgs e)
        {
            // (โค้ดส่วน "เลือกรูปภาพ" ของคุณ ... สมบูรณ์ดีแล้วครับ)
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picimage.Image = Image.FromFile(ofd.FileName);
            }
        }
    }
}