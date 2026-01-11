using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_videos_gmae_store
{
    public partial class OrderHistoryItemCard : UserControl
    {
        public OrderHistoryItemCard()
        {
            InitializeComponent();
        }

        // *** สำคัญ: แก้ชื่อคอนโทรล (lblName, picImage, lblQuantity, lblTotalPrice) 
        //     ให้ตรงกับที่คุณตั้งไว้ในหน้า Design ของ UserControl นี้ ***

        // (ช่องสำหรับรับ "ชื่อ")
        public string ItemName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public string ProductDescription
        {
            // *** ตรวจสอบว่า Label ที่คุณเพิ่งสร้าง ชื่อ 'lblDescription' ***
            get { return lblDescription.Text; }
            set { lblDescription.Text = value; }
        }

        // (ช่องสำหรับรับ "รูป")
        public Image ProductImage
        {
            get { return picproduct.Image; }
            set { picproduct.Image = value; }
        }

        // (ฟังก์ชันสำหรับรับ "จำนวน" และ "ราคารวม" ที่ Error CS1061 ถามหา)
        public void SetOrderDetails(int total_quantity_bought, decimal total_revenue_for_item)
        {
            // (แก้ไข) เปลี่ยนข้อความ Label
            lblQuantity.Text = $"จำนวนรวม: {total_quantity_bought} ชิ้น";
            lblTotalPrice.Text = $"ยอดซื้อรวม: {total_revenue_for_item:N0} ฿";
        }
    }
}
