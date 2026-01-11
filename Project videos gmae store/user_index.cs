using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // มี System.Drawing แล้ว
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics; // (สำหรับ Debug.WriteLine)
// ***************************************
// *** New usings for PDF (iTextSharp) ***
using iTextSharp.text;
using iTextSharp.text.pdf;
// ***************************************


namespace Project_videos_gmae_store
{
    public partial class user_index : Form
    {

        // 1. สร้างตัวแปร (Field) เพื่อ "จำ" ชื่อผู้ใช้
        private string currentUserName;

        // (Connection String ควรเก็บไว้ที่เดียว แต่เพื่อความง่าย จะไว้ตรงนี้ก่อน)
        private string connectionString = "server=localhost;database=projectstore;uid=root;pwd=;";

        private Dictionary<string, int> masterCart = new Dictionary<string, int>();

        public user_index()
        {
            InitializeComponent();
        }

        public user_index(string name)
        {
            InitializeComponent();

            // "จำ" ค่า name ไว้ใช้
            this.currentUserName = name;

            // **** (นี่คือบรรทัดที่น่าจะขาดไป) ****
            // สั่งให้ Label (ที่ชื่อ lblusername) แสดงผลชื่อที่รับมา
            // *** (ตรงนี้ต้องแน่ใจว่า lblusername มีอยู่จริง) ***
            lblusername.Text = name;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void btnprofile_Click(object sender, EventArgs e)
        {
            // สร้าง instance (ออบเจ็กต์) ของ Form ที่ชื่อ profile_index
            profile_index profileForm = new profile_index(this.currentUserName);

            // สั่งให้แสดง Form ใหม่
            profileForm.Show();

            // (ทางเลือก) ถ้าคุณต้องการซ่อนฟอร์มปัจจุบันหลังจากคลิก
            this.Hide();
        }

        private void imagebtnhome_Click(object sender, EventArgs e)
        {
            // สั่งให้แสดง View สินค้า (ฟังก์ชันเดิมของเรา)
            LoadProducts();
            panCartfooter.Visible = false;

            // *** [แก้ไข]: เมื่อกลับหน้า Home ต้องแสดง Search/Category คืน ***
            textsearch.Visible = true;
            comboboxCategory.Visible = true;
        }

        private void flowLayoutproduct_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadProfile()
        {
            // *** 1. แก้ 'picProfile' เป็นชื่อ PictureBox โปรไฟล์ของคุณ ***
            // *** 2. แก้ 'users' เป็นชื่อตารางที่เก็บข้อมูลสมาชิก ***
            // *** 3. แก้ 'image' เป็นชื่อคอลัมน์ที่เก็บรูปโปรไฟล์ ***
            // *** 4. แก้ 'username' เป็นชื่อคอลัมน์ที่เก็บชื่อผู้ใช้ ***

            string query = "SELECT image FROM user WHERE username = @username";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString)) // ใช้ connectionString ตัวเดิม
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // ใช้ 'currentUserName' ที่เราเก็บไว้
                        command.Parameters.AddWithValue("@username", this.currentUserName);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // ถ้าเจอข้อมูล
                            {
                                try
                                {
                                    // แปลง longblob เป็น Image (เหมือนตอนทำ LoadProducts)
                                    byte[] imageBytes = (byte[])reader["image"];
                                    using (MemoryStream ms = new MemoryStream(imageBytes))
                                    {
                                        // *** แก้ 'picProfile' เป็นชื่อ PictureBox โปรไฟล์ของคุณ ***
                                        // *** [แก้: ระบุชื่อเต็ม] ***
                                        picprofileimage.Image = System.Drawing.Image.FromStream(ms);
                                    }
                                }
                                catch (Exception imgEx)
                                {
                                    Console.WriteLine("Error loading profile image: " + imgEx.Message);
                                    // (ตั้งค่ารูป default ถ้าโหลดไม่สำเร็จ)
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดโปรไฟล์: " + ex.Message);
            }
        }

        // ใน user_index.cs
        private void LoadProducts()
        {
            flowLayoutproduct.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutproduct.WrapContents = true;

            flowLayoutproduct.Controls.Clear();
            string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=projectstore;";

            // SQL Query ที่มีคอลัมน์ 'image' และ 'category'
            string query = "SELECT id, name, price, image, category FROM product";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductCard card = new ProductCard();

                                // --- 1. ส่งข้อมูลเข้าการ์ด (ครบทุกส่วน) ---

                                card.ProductID = reader["id"].ToString();
                                card.CardName = reader["name"].ToString();

                                // ส่งค่า Category (สำหรับใช้ในการกรอง)
                                card.Category = reader["category"].ToString();

                                decimal price = Convert.ToDecimal(reader["price"]);
                                card.ProductPrice = $"฿{price:N0}";

                                // --- [โค้ดโหลดรูปภาพที่ถูกเพิ่มกลับมา] ---
                                try
                                {
                                    // 1. อ่านข้อมูลรูปภาพจากคอลัมน์ 'image' (เป็น byte array)
                                    byte[] imageBytes = (byte[])reader["image"];

                                    // 2. แปลง byte[] เป็น Image object โดยใช้ MemoryStream
                                    using (MemoryStream ms = new MemoryStream(imageBytes))
                                    {
                                        // *** [แก้: ระบุชื่อเต็ม] ***
                                        card.ProductImage = System.Drawing.Image.FromStream(ms);
                                    }
                                }
                                catch (Exception imgEx)
                                {
                                    // กรณีที่ไม่มีข้อมูลรูปภาพ (NULL) หรือเกิดข้อผิดพลาดในการโหลด
                                    Console.WriteLine("Error loading image for product ID " + reader["id"].ToString() + ": " + imgEx.Message);
                                    card.ProductImage = null; // ตั้งค่าเป็นว่างเปล่า
                                }
                                // ----------------------------------------

                                // --- 2. ดักฟังสัญญาณ (Event) จากการ์ด ---
                                card.QuantityChanged += Card_QuantityChanged;
                                card.ProductClicked += Card_ProductClicked;

                                // --- 3. เพิ่มการ์ดลงในแผง ---
                                flowLayoutproduct.Controls.Add(card);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเชื่อมต่อฐานข้อมูล: " + ex.Message);
            }
        }

        // (ในไฟล์ user_index.cs)
        // *** ก๊อปทับฟังก์ชัน ShowCartView() เดิมทั้งฟังก์ชัน ***
        private void ShowCartView()
        {

            // *** [แก้ไข]: ซ่อน Search Box และ ComboBox ***
            textsearch.Visible = false;
            comboboxCategory.Visible = false;

            flowLayoutproduct.FlowDirection = FlowDirection.TopDown;
            flowLayoutproduct.WrapContents = false;
            flowLayoutproduct.Controls.Clear();

            if (masterCart.Count == 0)
            {
                System.Windows.Forms.Label lblEmpty = new System.Windows.Forms.Label(); // *** [แก้: ระบุชื่อเต็ม] ***
                lblEmpty.Text = "ตะกร้าของคุณว่างเปล่า";
                lblEmpty.Font = new System.Drawing.Font("Arial", 16); // *** [แก้: ระบุชื่อเต็ม] ***
                lblEmpty.AutoSize = true;
                flowLayoutproduct.Controls.Add(lblEmpty);

                UpdateCartTotals(); // เรียกอัปเดต (เพื่อแสดงยอด ฿0)
                return;
            }

            foreach (var item in masterCart.ToList())
            {
                // (โค้ดวนลูปสร้าง CartItemCard ของคุณ...)
                // ... (ก๊อปมาจากโค้ดเดิมของคุณ) ...
                // (ตั้งแต่บรรทัด 244 ถึง 306 ใน user_index.cs)
                string productID = item.Key;
                int quantity = item.Value;
                string query = "SELECT name, price, image, description FROM product WHERE id = @ProductID";
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ProductID", productID);
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    CartItemCard card = new CartItemCard();
                                    card.Width = flowLayoutproduct.ClientSize.Width - 25;
                                    card.ProductID = productID;
                                    card.ItemName = reader["name"].ToString();
                                    card.ProductDescription = reader["description"].ToString();
                                    decimal singlePrice = Convert.ToDecimal(reader["price"]);
                                    card.SingleItemPrice = singlePrice;
                                    card.Quantity = quantity;
                                    try
                                    {
                                        byte[] imageBytes = (byte[])reader["image"];
                                        using (MemoryStream ms = new MemoryStream(imageBytes))
                                        {
                                            // *** [แก้: ระบุชื่อเต็ม] ***
                                            card.ProductImage = System.Drawing.Image.FromStream(ms);
                                        }
                                    }
                                    catch (Exception) { }
                                    card.ItemRemoved += CartItem_ItemRemoved;
                                    card.QuantityChanged += CartItem_QuantityChanged;
                                    flowLayoutproduct.Controls.Add(card);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลตะกร้า: " + ex.Message);
                }
            } // <-- สิ้นสุด FOREACH LOOP

            // (เราลบโค้ดสร้างปุ่ม btnCheckout แบบเก่าทิ้ง)

            // เรียกคำนวณยอดรวม *หลังจาก* เพิ่มการ์ดทั้งหมดเสร็จแล้ว
            UpdateCartTotals();
        }

        private void ShowOrderHistoryView()
        {

            // *** [แก้ไข]: ซ่อน Search Box และ ComboBox ***
            textsearch.Visible = false;
            comboboxCategory.Visible = false;

            // *** สำคัญ: แก้ 'flowLayoutproduct' ให้เป็นชื่อ FlowLayoutPanel ของหน้านั้นๆ ***
            flowLayoutproduct.FlowDirection = FlowDirection.TopDown;
            flowLayoutproduct.WrapContents = false;
            flowLayoutproduct.Controls.Clear();

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
                                System.Windows.Forms.Label lblEmpty = new System.Windows.Forms.Label { Text = "คุณยังไม่มีประวัติคำสั่งซื้อ", Font = new System.Drawing.Font("Arial", 16), AutoSize = true }; // *** [แก้: ระบุชื่อเต็ม] ***
                                flowLayoutproduct.Controls.Add(lblEmpty); // (*** แก้ชื่อ Panel ถ้าจำเป็น ***)
                                return;
                            }
                            while (reader.Read())
                            {
                                OrderHistoryItemCard card = new OrderHistoryItemCard();
                                card.Width = flowLayoutproduct.ClientSize.Width - 25; // (*** แก้ชื่อ Panel ถ้าจำเป็น ***)

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
                                        // *** [แก้: ระบุชื่อเต็ม] ***
                                        card.ProductImage = System.Drawing.Image.FromStream(ms);
                                    }
                                }
                                catch (Exception) { }

                                flowLayoutproduct.Controls.Add(card); // (*** แก้ชื่อ Panel ถ้าจำเป็น ***)
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

        // ฟังก์ชันนี้จะทำงานเมื่อกด "X" บนแถบตะกร้า
        // (ฟังก์ชันสำหรับ "รับสัญญาณ" จากปุ่ม "X" ในตะกร้า)
        private void CartItem_ItemRemoved(object sender, EventArgs e)
        {
            CartItemCard card = (CartItemCard)sender;
            string productID = card.ProductID;

            // ลบออกจาก "ตะกร้าหลัก"
            if (masterCart.ContainsKey(productID))
            {
                masterCart.Remove(productID);
            }

            // อัปเดตไอคอน (i)
            UpdateCartSummary();

            // "ล้างและสร้างใหม่"
            // (ฟังก์ชัน ShowCartView ที่เราแก้ใหม่ จะเรียก UpdateCartTotals() ให้อัตโนมัติ)
            ShowCartView();
        }

        // ฟังก์ชันนี้จะทำงานเมื่อกด +/- บนแถบตะกร้า
        private void CartItem_QuantityChanged(object sender, EventArgs e)
        {
            CartItemCard card = (CartItemCard)sender;
            string productID = card.ProductID;
            int newQuantity = card.Quantity;

            if (masterCart.ContainsKey(productID))
            {
                masterCart[productID] = newQuantity;
            }
            UpdateCartSummary();

            // --- [เพิ่ม!] ---
            UpdateCartTotals();
        }

        // [เพิ่มฟังก์ชันนี้] (สำหรับรับสัญญาณ "คลิก" ไปหน้า Detail)
        // (ฟังก์ชันนี้อยู่ใน user_index.cs)
        private void Card_ProductClicked(object sender, EventArgs e)
        {
            ProductCard clickedCard = (ProductCard)sender;
            string productID = clickedCard.ProductID;

            // 1. [แก้ไข!] ดึงจำนวน "ที่เลือกอยู่" จากการ์ดที่ถูกคลิก
            //    (ไม่ใช่จาก masterCart)
            int currentQuantity = clickedCard.Quantity;

            // 2. เปิดหน้า Detail (ส่ง productID และ "จำนวนปัจจุบัน" ที่ถูกต้องไป)
            detail_product detailForm = new detail_product(productID, currentQuantity);

            // 3. (สำคัญ!) "รอ" ให้ผู้ใช้ปิดหน้า Detail
            detailForm.ShowDialog();

            // --- (โค้ดนี้จะทำงาน "หลังจาก" ที่ผู้ใช้กด "หยิบใส่ตะกร้า" (สีฟ้า) ในหน้า Detail) ---

            // 4. ดึงจำนวน "ใหม่" ที่ผู้ใช้กด ออกมาจากหน้า Detail
            int newQuantity = detailForm.Quantity;

            // 5. อัปเดต "ตะกร้าหลัก" (masterCart)
            if (newQuantity > 0)
            {
                // (*** เพิ่ม: เช็คสต็อกก่อนอัปเดตตะกร้าจากหน้า Detail! ***)
                // (เราจะเพิ่มทีหลัง ตอนนี้เอาแค่นี้ก่อน)

                masterCart[productID] = newQuantity;
            }
            else // ถ้าผู้ใช้กดลดจนเหลือ 0
            {
                if (masterCart.ContainsKey(productID))
                {
                    masterCart.Remove(productID); // ลบออกจากตะกร้า
                }
            }

            // 6. อัปเดต UI (ไอคอนตะกร้า และ การ์ดที่หน้า Home)
            UpdateCartSummary();
            clickedCard.Quantity = newQuantity; // อัปเดตตัวเลขบนการ์ด (หน้า Home) ทันที
        }

        // (ฟังก์ชันนี้อยู่ใน user_index.cs)
        private void Card_QuantityChanged(object sender, EventArgs e)
        {
            ProductCard clickedCard = (ProductCard)sender;
            string productID = clickedCard.ProductID;
            int newQuantity = clickedCard.Quantity; // (นี่คือจำนวนที่ผู้ใช้ "เลือก" เช่น 3)

            if (newQuantity <= 0)
            {
                if (masterCart.ContainsKey(productID))
                {
                    masterCart.Remove(productID);
                }
            }
            else
            {
                // 1. [แก้ไข!] ดึง "ราคา" และ "ชื่อ" จริงจาก DB เพื่อมาทำ MessageBox
                decimal pricePerItem = 0;
                string productName = "";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT name, price FROM product WHERE id = @ProductID";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productID);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    productName = reader["name"].ToString();
                                    pricePerItem = Convert.ToDecimal(reader["price"]);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading product price: " + ex.Message);
                    return;
                }

                // 2. อัปเดต "ตะกร้าหลัก"
                masterCart[productID] = newQuantity;

                // 3. [นี่คือสิ่งที่คุณต้องการ!] คำนวณและแสดง MessageBox
                decimal totalPrice = newQuantity * pricePerItem;
                string message = $"เพิ่ม '{productName}'\n" +
                                 $"จำนวน: {newQuantity} ชิ้น\n" +
                                 $"ราคารวม: {totalPrice:N0} ฿\n\nลงในตะกร้าเรียบร้อย";

                MessageBox.Show(message, "เพิ่มสินค้าแล้ว", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information); // *** [แก้: ระบุชื่อเต็ม] ***
            }

            // 4. อัปเดตไอคอนตะกร้า (บนเมนู)
            UpdateCartSummary();
        }

        // [เพิ่มฟังก์ชันนี้] (สำหรับอัปเดตยอดรวมตะกร้า)
        private void UpdateCartSummary()
        {
            int totalItems = masterCart.Values.Sum(); // นับจำนวนชิ้นทั้งหมด

            // (*** แก้ 'lblCartCount' ให้เป็นชื่อ Label ที่คุณใช้แสดงผลยอดรวม ***)
            // lblCartCount.Text = $"ตะกร้า ({totalItems})";

            // แสดงผลใน Console เพื่อ Debug
            Console.WriteLine($"ยอดรวมในตะกร้า: {totalItems}");
        }

        // --- (เพิ่มฟังก์ชันใหม่นี้เข้าไป) ---
        // (วางในไฟล์ user_index.cs)
        // *** ก๊อปทับ UpdateCartTotals() ตัวเก่า ***
        private void UpdateCartTotals()
        {
            decimal subtotal = 0;

            foreach (CartItemCard item in flowLayoutproduct.Controls.OfType<CartItemCard>())
            {
                subtotal += item.TotalPrice;
            }

            // *** แก้ L-B-L เป็น L-B (ตัวเล็ก) ***
            lblSubtotal.Text = $"ยอดรวมทั้งหมด ฿{subtotal:N0}";



        }
        // --- (สิ้นสุดฟังก์ชันใหม่) ---

        // *** นี่คือชื่อฟังก์ชันที่ถูกต้อง (ที่เชื่อมกับ Event Load) ***
        private void user_index_Load(object sender, EventArgs e)
        {
            // 1. โหลดข้อมูลโปรไฟล์ก่อน
            LoadProfile();

            // 2. ตั้งค่า ComboBox หมวดหมู่ <-- [เพิ่มบรรทัดนี้]
            SetupCategoryComboBox();

            // 3. แล้วค่อยโหลดสินค้า
            LoadProducts();
        }

        // ใน user_index.cs
        private void SetupCategoryComboBox()
        {
            // 1. ดึงรายการหมวดหมู่ที่ไม่ซ้ำกันจากฐานข้อมูล
            List<string> categories = new List<string>();
            categories.Add("ทั้งหมด"); // ตัวเลือกแรก

            string query = "SELECT DISTINCT category FROM product";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["category"].ToString();
                                if (!string.IsNullOrEmpty(category))
                                {
                                    categories.Add(category);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading categories for ComboBox: " + ex.Message);
            }

            // 2. ผูกข้อมูลเข้ากับ ComboBox
            comboboxCategory.DataSource = categories;
            comboboxCategory.SelectedIndex = 0; // เลือก "ทั้งหมด" เป็นค่าเริ่มต้น
        }

        private void preorderproduct_Click(object sender, EventArgs e)
        {
            // สั่งให้แสดง View ตะกร้า
            ShowCartView();
            // สั่งให้แสดงแถบยอดรวม
            panCartfooter.Visible = true;
        }

        private void orderproduct_Click(object sender, EventArgs e)
        {
            // สั่งให้แสดง View ประวัติคำสั่งซื้อ
            ShowOrderHistoryView();
            panCartfooter.Visible = false;
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (masterCart.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("ตะกร้าว่างเปล่า!"); // *** [แก้: ระบุชื่อเต็ม] ***
                return;
            }

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlTransaction transaction = null;

            decimal totalAmount = 0;
            Dictionary<string, decimal> prices = new Dictionary<string, decimal>();
            int currentUserID = -1;
            string customerName = "", shippingAddress = "", shippingPhone = "";
            long newOrderID = -1; // (เปลี่ยนเป็น long เผื่อ ID เยอะ)

            try
            {
                // --- [ขั้นตอนที่ 1: เช็กสต็อก และ ดึงข้อมูล (เหมือนเดิม)] ---
                connection.Open();
                transaction = connection.BeginTransaction();

                // 1a. ดึงข้อมูล User (เหมือนเดิม)
                string userQuery = "SELECT id, name, address, phone FROM user WHERE username = @username";
                using (MySqlCommand userCmd = new MySqlCommand(userQuery, connection, transaction))
                {
                    userCmd.Parameters.AddWithValue("@username", this.currentUserName);
                    using (MySqlDataReader userReader = userCmd.ExecuteReader())
                    {
                        if (userReader.Read())
                        {
                            currentUserID = userReader.GetInt32("id");
                            customerName = userReader.GetString("name");
                            shippingAddress = userReader.GetString("address");
                            shippingPhone = userReader.GetString("phone");
                        }
                    }
                }
                if (currentUserID == -1) throw new Exception("ไม่พบ User ID");

                // 1b. [Loop 1] เช็คสต็อก และ คำนวณยอดรวม (เหมือนเดิม)
                foreach (var item in masterCart.ToList())
                {
                    string productID = item.Key;
                    int quantityInCart = item.Value;

                    string stockQuery = "SELECT name, price, quantity FROM product WHERE id = @ProductID";
                    using (MySqlCommand stockCmd = new MySqlCommand(stockQuery, connection, transaction))
                    {
                        stockCmd.Parameters.AddWithValue("@ProductID", productID);
                        using (MySqlDataReader reader = stockCmd.ExecuteReader())
                        {
                            if (!reader.Read()) throw new Exception($"ไม่พบสินค้า ID: {productID}");

                            int stock_quantity = reader.GetInt32("quantity");
                            if (quantityInCart > stock_quantity)
                            {
                                string productName = reader.GetString("name");
                                throw new Exception($"สินค้า '{productName}' มีในสต็อกไม่พอ!"); // (โยน Error ดีกว่า)
                            }

                            decimal price = reader.GetDecimal("price");
                            prices[productID] = price;
                            totalAmount += (price * quantityInCart);
                        }
                    }
                }

                // --- [ขั้นตอนที่ 2: (แก้ไข!) สร้างออเดอร์ "ทันที"] ---

                // 2a. INSERT ลง 'orderproduct' (ใบเสร็จหลัก)
                // (*** [สำคัญ!] ผมเปลี่ยนชื่อตารางเป็น 'orders' และเพิ่ม 'status' ตามที่เราคุยกัน ***)
                string orderQuery = @"
    INSERT INTO orderproduct (user_id, total_amount, customer_name, shipping_address, shipping_phone, status)
    VALUES (@UserID, @Total, @Name, @Address, @Phone, 'Waiting for Payment');
    SELECT LAST_INSERT_ID();";

                using (MySqlCommand orderCmd = new MySqlCommand(orderQuery, connection, transaction))
                {
                    orderCmd.Parameters.AddWithValue("@UserID", currentUserID);
                    orderCmd.Parameters.AddWithValue("@Total", totalAmount);
                    orderCmd.Parameters.AddWithValue("@Name", customerName);
                    orderCmd.Parameters.AddWithValue("@Address", shippingAddress);
                    orderCmd.Parameters.AddWithValue("@Phone", shippingPhone);
                    newOrderID = Convert.ToInt64(orderCmd.ExecuteScalar()); // ดึง ID ออเดอร์ใหม่
                }

                // 2b. [Loop 2] INSERT ลง 'order_items' และ "ตัดสต็อก" (เหมือนเดิม)
                foreach (var item in masterCart.ToList())
                {
                    string productID = item.Key;
                    int quantity = item.Value;
                    decimal priceAtPurchase = prices[productID];

                    string itemQuery = @"INSERT INTO order_items (order_id, product_id, quantity, price_at_purchase) VALUES (@OrderID, @ProductID, @Qty, @Price);";
                    using (MySqlCommand itemCmd = new MySqlCommand(itemQuery, connection, transaction))
                    {
                        itemCmd.Parameters.AddWithValue("@OrderID", newOrderID);
                        itemCmd.Parameters.AddWithValue("@ProductID", productID);
                        itemCmd.Parameters.AddWithValue("@Qty", quantity);
                        itemCmd.Parameters.AddWithValue("@Price", priceAtPurchase);
                        itemCmd.ExecuteNonQuery();
                    }

                    string deductStockQuery = @"UPDATE product SET quantity = quantity - @Qty WHERE id = @ProductID";
                    using (MySqlCommand deductCmd = new MySqlCommand(deductStockQuery, connection, transaction))
                    {
                        deductCmd.Parameters.AddWithValue("@Qty", quantity);
                        deductCmd.Parameters.AddWithValue("@ProductID", productID);
                        deductCmd.ExecuteNonQuery();
                    }
                }

                // 2c. ถ้าทุกอย่างสำเร็จ
                transaction.Commit(); // <--- ยืนยันการตัดสต็อกและสร้างออเดอร์
            }
            catch (Exception ex)
            {
                // ถ้าเกิด Error (เช่น สต็อกไม่พอ หรือ DB พัง)
                try { transaction?.Rollback(); } // <--- ยกเลิกทั้งหมด
                catch { }
                System.Windows.Forms.MessageBox.Show("เกิดข้อผิดพลาดในการสร้างออเดอร์: \n" + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); // *** [แก้: ระบุชื่อเต็ม] ***
                return; // ไม่ไปต่อ
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            // --- [ขั้นตอนที่ 3: (แก้ไข!) เปิดหน้า PaymentForm "หลังจาก" สร้างออเดอร์แล้ว] ---

            // (ถ้า newOrderID > 0 แปลว่า ขั้นตอนที่ 1 และ 2 สำเร็จ)
            if (newOrderID > 0)
            {
                // [นี่คือจุดที่ "แก้ Error" CS7036 (รูป 152511.png)]
                // เราส่ง ยอดเงิน (totalAmount) และ ID ออเดอร์ (newOrderID) ไป
                using (PaymentForm paymentPopup = new PaymentForm(totalAmount, (int)newOrderID))
                {
                    var result = paymentPopup.ShowDialog();

                    // (ตรรกะนี้เปลี่ยนไป)
                    if (result == System.Windows.Forms.DialogResult.OK) // *** [แก้: ระบุชื่อเต็ม] ***
                    {
                        // ถ้าผู้ใช้กด "ฉันชำระเงินแล้ว" (สลิปถูกอัปโหลดแล้ว)
                        System.Windows.Forms.MessageBox.Show("สั่งซื้อสำเร็จ! กรุณารอแอดมินตรวจสอบสลิป", "สำเร็จ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information); // *** [แก้: ระบุชื่อเต็ม] ***

                        // (โค้ดสร้างใบเสร็จ PDF ถูกย้ายมาตรงนี้แทน)
                        // *** [เปลี่ยนเป็นเรียกใช้ GenerateReceiptPdf] ***
                        GenerateReceiptPdf(newOrderID, customerName, shippingAddress, shippingPhone, totalAmount, prices);

                        // ล้างตะกร้า
                        masterCart.Clear();
                        UpdateCartSummary();
                        ShowCartView();
                    }
                    else
                    {
                        // ถ้าผู้ใช้กด "ยกเลิก" ในหน้าจ่ายเงิน
                        System.Windows.Forms.MessageBox.Show("ยกเลิกการชำระเงิน\nออเดอร์ของคุณถูกสร้างแล้ว (รอชำระเงิน) สามารถดูได้ที่หน้า 'คำสั่งซื้อ'", "ยกเลิก", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information); // *** [แก้: ระบุชื่อเต็ม] ***
                    }
                }
            }
        }

        // *****************************************************************
        // *** START: ฟังก์ชันใหม่ GenerateReceiptPdf (แทน GenerateReceiptHtml) ***
        // *****************************************************************
        private void GenerateReceiptPdf(long newOrderID, string customerName, string shippingAddress, string shippingPhone, decimal totalAmount, Dictionary<string, decimal> prices)
        {
            // กำหนดรูปแบบราคา
            string FormatPrice(decimal price) => $"฿{price:N2}";

            Dictionary<string, string> productNames = new Dictionary<string, string>();
            // 1. ดึงชื่อสินค้า (จำเป็นต้องทำก่อน masterCart จะถูกล้าง)
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (var item in masterCart)
                    {
                        string pQuery = "SELECT name FROM product WHERE id = @ProductID";
                        using (MySqlCommand pCmd = new MySqlCommand(pQuery, conn))
                        {
                            pCmd.Parameters.AddWithValue("@ProductID", item.Key);
                            object result = pCmd.ExecuteScalar();
                            productNames[item.Key] = result != null ? result.ToString() : "Unknown Product";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching product names for PDF: " + ex.Message);
            }

            // 2. กำหนดที่อยู่ไฟล์ (ใช้ตาม Path ที่คุณระบุในตัวอย่าง)
            // *** [แก้ไข]: เปลี่ยนไปใช้ Path ที่เข้าถึงง่ายกว่า หรือใช้โฟลเดอร์ Documents ***
            // string folderPath = @"C:\Users\asus\Desktop\receiptC"; // โค้ดเดิมที่มีปัญหา
            string folderPath = @"C:\Receipts"; // **คำแนะนำ:** คุณต้องสร้างโฟลเดอร์ชื่อ 'Receipts' บน Drive C: ด้วยตนเอง

            if (!System.IO.Directory.Exists(folderPath)) // *** [แก้: ระบุชื่อเต็ม] ***
                System.IO.Directory.CreateDirectory(folderPath); // *** [แก้: ระบุชื่อเต็ม] ***

            string fileName = $"Receipt_Order_{newOrderID}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string filePath = System.IO.Path.Combine(folderPath, fileName); // *** [แก้: ระบุชื่อเต็ม] ***

            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create)) // *** [แก้: ระบุชื่อเต็ม] ***
                {
                    // ใช้ขนาด A5 ตามตัวอย่างที่คุณให้มา
                    Document doc = new Document(PageSize.A5);
                    PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // *** [รองรับภาษาไทย] ***
                    string fontPath = "c:\\windows\\fonts\\arial.ttf";
                    BaseFont bf;
                    try
                    {
                        bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    }
                    catch (Exception)
                    {
                        bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }

                    // *** [แก้: ใช้คลาส Font ของ iTextSharp] ***
                    iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font bodyFont = new iTextSharp.text.Font(bf, 12);

                    // --- หัวข้อและข้อมูลร้านค้า ---
                    doc.Add(new Paragraph("PN GAMEVERSE\n44/15 Game Street, Khonkaen, Thailand", headerFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph($"Order ID: {newOrderID}\nDate: {DateTime.Now:dd/MM/yyyy} Time: {DateTime.Now:HH:mm}", bodyFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph($"Customer: {this.currentUserName} ({customerName})", bodyFont));
                    doc.Add(new Paragraph("Shipping Address: " + shippingAddress, bodyFont));
                    doc.Add(new Paragraph("Phone: " + shippingPhone + "\n", bodyFont));
                    doc.Add(new Paragraph("-------------------------------------------------------"));

                    // --- ตารางรายการสินค้า ---
                    PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
                    // *** [แก้ปัญหา CS1503: String to Float ได้รับการยืนยันว่าแก้ไขแล้ว] ***
                    table.SetWidths(new float[] { 2.0f, 4.0f, 2.0f, 2.0f }); // [Product ID, Name, Qty, Price]
                    table.SpacingBefore = 10f;

                    // Header
                    // *** [แก้: ใช้คลาส Phrase ของ iTextSharp] ***
                    table.AddCell(new PdfPCell(new Phrase("ID", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Product", headerFont)));
                    table.AddCell(new PdfPCell(new Phrase("Quantity", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Total price", headerFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });

                    decimal subTotal = 0;
                    foreach (var item in masterCart)
                    {
                        string productID = item.Key;
                        int pQty = item.Value;
                        string pName = productNames.ContainsKey(productID) ? productNames[productID] : "Unknown Product";
                        decimal pPrice = prices[productID]; // ราคาต่อหน่วย
                        decimal pTotal = pPrice * pQty; // ราคารวมต่อรายการ

                        table.AddCell(new PdfPCell(new Phrase(productID, bodyFont)));
                        table.AddCell(new PdfPCell(new Phrase(pName, bodyFont)));
                        table.AddCell(new PdfPCell(new Phrase(pQty.ToString(), bodyFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(FormatPrice(pTotal), bodyFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        subTotal += pTotal;
                    }

                    doc.Add(table);

                    // --- สรุปยอดรวม ---
                    doc.Add(new Paragraph("-------------------------------------------------------"));

                    // ใช้แค่ยอดรวม totalAmount ที่คำนวณไว้แล้ว
                    doc.Add(new Paragraph($"Grand Total: {FormatPrice(totalAmount)}", headerFont) { Alignment = Element.ALIGN_RIGHT });

                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("Thank you for using PN GAMEVERSE.", bodyFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Close();
                }

                // 4. แสดงผลสำเร็จและเปิดไฟล์
                System.Windows.Forms.MessageBox.Show("ใบเสร็จ (PDF) ถูกบันทึกที่: " + filePath, "บันทึกใบเสร็จสำเร็จ (PDF)", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information); // *** [แก้: ระบุชื่อเต็ม] ***
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() { FileName = filePath, UseShellExecute = true }); // *** [แก้: ระบุชื่อเต็ม] ***
            }
            catch (Exception pdfEx)
            {
                System.Windows.Forms.MessageBox.Show("เกิดข้อผิดพลาดในการสร้าง PDF ใบเสร็จ: " + pdfEx.Message, "PDF Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); // *** [แก้: ระบุชื่อเต็ม] ***
            }
        }
        // *****************************************************************
        // *** END: ฟังก์ชันใหม่ GenerateReceiptPdf ***
        // *****************************************************************

        // (ลบ GenerateReceiptHtml ทิ้ง และโค้ดส่วนที่เหลือเหมือนเดิม)

        private void aboutus_Click(object sender, EventArgs e)
        {
            // 1. สร้าง "หน้าต่าง" AboutUsForm ขึ้นมา
            // (*** ถ้า Form ของคุณชื่ออื่น ให้แก้ตรงนี้ ***)
            AboutUsForm aboutForm = new AboutUsForm();

            // 2. สั่งให้ "แสดง" แบบ Pop-up (ShowDialog)
            aboutForm.ShowDialog();
        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {
            // 1. รับข้อความที่ค้นหาและแปลงเป็นตัวพิมพ์เล็กทั้งหมด
            string searchText = textsearch.Text.Trim().ToLower();

            // 2. วนลูปตรวจสอบ Control ทุกตัวที่อยู่ใน FlowLayoutPanel ชื่อ flowlayoutproduct
            //    *ตอนนี้ flowlayoutproduct จะเข้าถึงได้แล้ว*
            foreach (Control control in flowLayoutproduct.Controls)
            {
                // 3. ตรวจสอบว่า Control ที่วนลูปถึงนั้นเป็น ProductCard UserControl หรือไม่
                if (control is ProductCard productCard)
                {
                    // 4. เข้าถึงชื่อสินค้าโดยใช้ Property CardName ที่คุณสร้างไว้ใน ProductCard
                    string productName = productCard.CardName.Trim().ToLower();

                    // 5. ตั้งค่าการแสดงผล (Visible)
                    bool isVisible = string.IsNullOrWhiteSpace(searchText) || productName.Contains(searchText);

                    productCard.Visible = isVisible;
                }
            }
        }

        private void comboboxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. ดึงหมวดหมู่ที่ถูกเลือก
            string selectedCategory = comboboxCategory.SelectedItem.ToString();

            // 2. วนลูปตรวจสอบ Control ทุกตัวที่อยู่ใน FlowLayoutPanel
            foreach (Control control in flowLayoutproduct.Controls)
            {
                // 3. ตรวจสอบว่าเป็น ProductCard UserControl
                if (control is ProductCard productCard)
                {
                    // 4. ตั้งค่าการแสดงผล (Visible)
                    //    - ถ้าเลือก "ทั้งหมด" หรือ Category ตรงกัน ให้แสดง
                    bool isVisible = (selectedCategory == "ทั้งหมด") ||
                                     (productCard.Category == selectedCategory);

                    productCard.Visible = isVisible;
                }
            }
        }
    }
}