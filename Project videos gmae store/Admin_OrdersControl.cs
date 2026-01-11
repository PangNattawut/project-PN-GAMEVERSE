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
using System.IO;

namespace Project_videos_gmae_store
{
    public partial class Admin_OrdersControl : UserControl
    {
        // (*** ก๊อป Connection String มาจาก login.cs ***)
        string connStr = "server=localhost;database=projectstore;uid=root;pwd=;";

        public Admin_OrdersControl()
        {
            InitializeComponent();
        }

        private void Admin_OrdersControl_Load(object sender, EventArgs e)
        {
            LoadPendingOrders();
        }

        private void LoadPendingOrders()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    // (เราจะดึงออเดอร์ที่มีสถานะ 'Pending' (ที่ลูกค้าแนบสลิปแล้ว) ขึ้นมาก่อน)
                    // (ตาราง 'orderproduct', คอลัมน์ 'order_id')
                    string query = "SELECT order_id, customer_name, total_amount, status FROM orderproduct WHERE status = 'Pending' ORDER BY order_date ASC";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvOrders.DataSource = dt; // แสดงในตาราง
                                               // --- V V V เพิ่มบรรทัดนี้เพื่อซ่อน Header สีเทา V V V ---
                    dgvOrders.RowHeadersVisible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        // --- 2. โค้ดสำหรับ "คลิกตาราง" แล้ว "โชว์สลิป" ---
        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าผู้ใช้คลิกแถวที่มีข้อมูลจริงๆ
            if (e.RowIndex >= 0 && dgvOrders.Rows[e.RowIndex].Cells["order_id"].Value != null)
            {
                // ดึง order_id จากแถวที่เลือก
                int selectedOrderID = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["order_id"].Value);

                // โหลดสลิป
                LoadSlipImage(selectedOrderID);
            }
        }

        private void LoadSlipImage(int orderID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    // (ดึงคอลัมน์ 'slip_image' จาก 'orderproduct')
                    string query = "SELECT slip_image FROM orderproduct WHERE order_id = @OrderID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);

                        // ดึงข้อมูล BLOB (byte[])
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            byte[] imageBytes = (byte[])result;
                            // แปลง byte[] กลับเป็น Image
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                picSlipViewer.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            picSlipViewer.Image = null; // ถ้าไม่มีสลิป
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading slip: " + ex.Message);
                picSlipViewer.Image = null;
            }
        }

        // --- 3. โค้ดปุ่ม "อนุมัติ" ---
        private void btnApprove_Click(object sender, EventArgs e)
        {
            // (ต้องเช็กก่อนว่าเลือกแถวหรือยัง)
            if (dgvOrders.CurrentRow == null) return;

            int selectedOrderID = Convert.ToInt32(dgvOrders.CurrentRow.Cells["order_id"].Value);

            // (อัปเดตสถานะเป็น 'Completed')
            UpdateOrderStatus(selectedOrderID, "Completed");
        }

        // --- 4. โค้ดปุ่ม "ปฏิเสธ" ---
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null) return;

            int selectedOrderID = Convert.ToInt32(dgvOrders.CurrentRow.Cells["order_id"].Value);

            // (อัปเดตสถานะเป็น 'Rejected')
            UpdateOrderStatus(selectedOrderID, "Rejected");
        }

        // (ฟังก์ชันที่ใช้ร่วมกัน)
        private void UpdateOrderStatus(int orderID, string newStatus)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    // (อัปเดต 'status' ใน 'orderproduct')
                    string query = "UPDATE orderproduct SET status = @Status WHERE order_id = @OrderID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"อัปเดตออเดอร์ {orderID} เป็น '{newStatus}' สำเร็จ!");
                    picSlipViewer.Image = null; // เคลียร์รูป
                    LoadPendingOrders(); // โหลดตารางใหม่
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating status: " + ex.Message);
            }
        }
    }
}
