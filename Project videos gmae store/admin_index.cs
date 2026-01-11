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

namespace Project_videos_gmae_store
{
    public partial class admin_index : Form
    {
        public admin_index()
        {
            InitializeComponent();
        }

        public admin_index(string name, string role)
        {
            InitializeComponent(); // ต้องเรียกอันนี้ก่อนเสมอ

            // นำค่าที่ได้รับมาไปใส่ใน Label
            lblusername.Text = name;
            lblrole.Text = role;
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

        private void LoadControlToPanel(UserControl control)
        {
            // 'panelChile' คือ Panel ใหญ่ที่เราตั้งชื่อไว้ในขั้นตอนที่ 1
            panelChile.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelChile.Controls.Add(control);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void guna2Button4_Click(object sender, EventArgs e)
        //{

        //}

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // (*** [สำคัญ!] 'pnlMain' คือ "Panel" ที่คุณใช้แสดงเนื้อหา ***)
            // (ถ้าของคุณชื่ออื่น (เช่น 'panelContainer') ให้แก้ชื่อตรงนี้ครับ)

            // 1. "ล้าง" หน้าเก่า (เช่น หน้า User) ทิ้งไปก่อน
            panelChile.Controls.Clear();

            // 2. "สร้าง" หน้า Dashboard (ที่เราเพิ่งทำ)
            dashboard_sales_report dashControl = new dashboard_sales_report();
            dashControl.Dock = DockStyle.Fill; // (บังคับให้ "เต็ม" Panel)

            // 3. "โหลด" หน้านี้ เข้าไปใน Panel
            panelChile.Controls.Add(dashControl);
            //เปลี่ยน Title
            lblTitle.Text = "Dashboard";
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            // เรียกใช้ฟังก์ชันที่เราสร้างไว้
            // โดยส่งหน้า admin_userform ที่สร้างใหม่เข้าไป
            // 1. โหลดหน้า User
            loadFormIntoPanel(new admin_userform());

            // 2. เปลี่ยน Title
            lblTitle.Text = "User Management";
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            loadFormIntoPanel(new product());

            // 2. เปลี่ยน Title
            lblTitle.Text = "Product";
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

        private void btnOrders_Click(object sender, EventArgs e)
        {
            // สร้างหน้าจัดการออเดอร์
            Admin_OrdersControl ordersView = new Admin_OrdersControl();

            // โหลดหน้านั้นไปใส่ใน Panel หลัก
            LoadControlToPanel(ordersView);

            // 2. เปลี่ยน Title
            lblTitle.Text = "Orders";
        }
    }
}
