using System;
using System.Drawing;
using System.Windows.Forms;
using QRCoder; // (*** เราจะใช้แค่ตัวนี้ตัวเดียว ***)
using System.IO;                  // <--- (เพิ่ม) สำหรับจัดการไฟล์
using MySql.Data.MySqlClient;   // <--- (เพิ่ม) สำหรับเชื่อมต่อ MySQL
// (เราไม่ 'using' PromptPay... อีกแล้ว, เราจะใช้ Helper ของเรา)

namespace Project_videos_gmae_store
{
    public partial class PaymentForm : Form
    {
        private decimal totalAmount;
        private int currentOrderID; // (*** [สำคัญ!] คุณต้องส่ง Order ID เข้ามาที่ฟอร์มนี้ ***)

        // (*** [สำคัญ!] ใส่เบอร์ PromptPay ของร้านคุณที่นี่ ***)
        private string MY_PROMPTPAY_ID = "0647567493";

        // (เพิ่ม) ตัวแปรสำหรับเก็บข้อมูลรูปภาพ (ในรูปแบบ byte array)
        private byte[] selectedImageBytes = null;

        // (*** [แก้ไข!] Constructor ต้องรับ Order ID เข้ามาด้วย ***)
        public PaymentForm(decimal totalAmount, int orderID) // เช่น (990, 123)
        {
            InitializeComponent();
            this.totalAmount = totalAmount;
            this.currentOrderID = orderID; // <--- เก็บ Order ID ไว้
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            // 1. ตั้งค่า Label ยอดเงิน
            lblTotal.Text = $"ยอดชำระ: {this.totalAmount:N2} บาท";
            lblTotal.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTotal.AutoSize = true;

            // 2. [แก้ไข!] สร้าง Text สำหรับ PromptPay (ใช้ 'PromptPayHelper')
            string qrText = PromptPayHelper.GeneratePayload(
                this.MY_PROMPTPAY_ID,
                this.totalAmount
            );

            // 3. สร้าง QR Code จาก Text นั้น
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            // 4. แสดงรูป QR ใน PictureBox
            // (*** ตรวจสอบว่า PictureBox นี้ชื่อ 'picQRCode' ***)
            picQRCode.Image = qrCodeImage;
            picQRCode.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // (ปุ่ม "ฉันชำระเงินแล้ว")
        private void btnPaid_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่าผู้ใช้แนบสลิปหรือยัง
            if (selectedImageBytes == null)
            {
                MessageBox.Show("กรุณาแนบสลิปก่อนยืนยันการชำระเงิน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ไม่ทำต่อ
            }

            // 2. เชื่อมต่อ DB และ UPDATE สลิป
            try
            {
                // (ใช้ Connection String จากไฟล์ login.cs / register.cs ของคุณ)
                string connStr = "server=localhost;database=projectstore;uid=root;pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // (*** [สำคัญ!] แก้ไข 'orderproduct', 'slip_image', 'order_id' ให้ตรงกับ DB ของคุณ ***)
                    string query = "UPDATE orderproduct SET slip_image = @SlipData, status = 'Pending' WHERE order_id = @OrderID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // ส่งข้อมูลรูป (byte[]) เข้าไปที่ Parameter @SlipData
                        cmd.Parameters.AddWithValue("@SlipData", selectedImageBytes);

                        // ส่ง ID ของออเดอร์ (ที่เราเก็บไว้ตอนเปิดฟอร์ม)
                        cmd.Parameters.AddWithValue("@OrderID", this.currentOrderID);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("อัปโหลดสลิปสำเร็จ!\nกรุณารอการตรวจสอบจากแอดมิน", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 3. ถ้าสำเร็จ ค่อยส่งสัญญาณ "OK" กลับ
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการอัปโหลดสลิป: " + ex.Message);
            }
        }

        // (ปุ่ม "ยกเลิก")
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // ส่งสัญญาณ "Cancel" กลับ
            this.Close();
        }

        private void btnAttachSlip_Click(object sender, EventArgs e)
        {
            // 1. สร้าง OpenFileDialog (กล่องเลือกไฟล์)
            OpenFileDialog openFile = new OpenFileDialog();

            // 2. ตั้งค่าให้กรองเฉพาะไฟล์รูปภาพ
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFile.Title = "กรุณาเลือกสลิปโอนเงิน";

            // 3. ถ้าผู้ใช้เลือกไฟล์ และกดปุ่ม OK
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFile.FileName;

                    // 4. แสดงรูปภาพใน PictureBox
                    // (*** [สำคัญ!] ตรวจสอบว่า PictureBox สี่เหลี่ยมประของคุณ ชื่อ 'picSlip' ***)
                    picSlip.Image = Image.FromFile(filePath);
                    picSlip.SizeMode = PictureBoxSizeMode.Zoom; // (แนะนำ)

                    // 5. (สำคัญที่สุด) อ่านไฟล์รูปภาพแล้วแปลงเป็น byte[]
                    // เพื่อเก็บไว้ในตัวแปร selectedImageBytes
                    selectedImageBytes = File.ReadAllBytes(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถโหลดรูปภาพได้: " + ex.Message, "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}