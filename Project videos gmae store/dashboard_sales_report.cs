// (นี่คือโค้ดใน dashboard_sales_report.cs)

using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting; // (*** [สำคัญ!] เพิ่ม using นี้สำหรับกราฟ ***)

namespace Project_videos_gmae_store
{
    // (*** [แก้ไข!] ใช้ชื่อ Class ที่ถูกต้องตามชื่อไฟล์ของคุณ ***)
    public partial class dashboard_sales_report : UserControl
    {
        // (*** ก๊อป Connection String มาจาก login.cs ***)
        string connStr = "server=localhost;database=projectstore;uid=root;pwd=;";

        public dashboard_sales_report()
        {
            InitializeComponent();
        }

        // --- (โค้ด "โหลด" หน้า: อัปเกรดแล้ว) ---
        private void dashboard_sales_report_Load(object sender, EventArgs e)
        {
            // 1. โหลด Stat Card 4 อันบนสุด (เพิ่มใหม่)
            LoadAllTimeStats();

            // 2. ตั้งค่าตาราง (จากโค้ดเก่าของคุณ)
            dgvSalesData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSalesData.RowHeadersVisible = false;

            // 3. ตั้งค่ากราฟ (เพิ่มใหม่)
            SetupChart();

            // 4. โหลดข้อมูล "ทั้งหมด" เป็นค่าเริ่มต้น
            btnAllTime_Click(sender, e);
        }

        // --- (ฟังก์ชันใหม่: ตั้งค่ากราฟ) ---
        private void SetupChart()
        {
            // (*** [สำคัญ!] ตรวจสอบว่า Chart ของคุณชื่อ 'chartSales' ***)
            chartSales.Series.Clear();
            chartSales.ChartAreas[0].AxisX.Title = "วันที่ (Date)";
            chartSales.ChartAreas[0].AxisY.Title = "ยอดขาย (Revenue)";
            chartSales.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yy";
        }

        // --- (ฟังก์ชันใหม่: โหลด Stat Cards 4 อัน) ---
        private void LoadAllTimeStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // (Query ที่ 1: ยอดรวม & ออเดอร์ จาก 'orderproduct')
                    // (*** [สำคัญ!] นับเฉพาะ 'Completed' ***)
                    string queryOrders = @"
                        SELECT 
                            SUM(total_amount) AS TotalRevenue, 
                            COUNT(order_id) AS TotalOrders 
                        FROM orderproduct 
                        WHERE status = 'Completed'";

                    using (MySqlCommand cmd = new MySqlCommand(queryOrders, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // (*** [สำคัญ!] ตรวจสอบว่า Label 4 อันของคุณชื่อตรงกัน ***)
                                // (lblTotalRevenue, lblTotalOrders)
                                if (reader["TotalRevenue"] != DBNull.Value)
                                    lblTotalRevenue.Text = reader.GetDecimal("TotalRevenue").ToString("N2") + " ฿";
                                else
                                    lblTotalRevenue.Text = "0.00 ฿";

                                lblTotalOrders.Text = reader.GetInt32("TotalOrders").ToString();
                            }
                        }
                    }

                    // (Query ที่ 2: ลูกค้า จาก 'user')
                    string queryCustomers = "SELECT COUNT(id) FROM user WHERE role = 'user'";
                    using (MySqlCommand cmd = new MySqlCommand(queryCustomers, conn))
                    {
                        // (lblTotalCustomers)
                        lblTotalCustomers.Text = cmd.ExecuteScalar().ToString();
                    }

                    // (Query ที่ 3: สินค้า จาก 'product')
                    string queryProducts = "SELECT COUNT(id) FROM product";
                    using (MySqlCommand cmd = new MySqlCommand(queryProducts, conn))
                    {
                        // (lblTotalProducts)
                        lblTotalProducts.Text = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message);
            }
        }

        // --- (ฟังก์ชันใหม่: โหลดข้อมูลกราฟ) ---
        private void LoadChartData(DateTime? startDate, DateTime? endDate)
        {
            // (*** [สำคัญ!] ตรวจสอบว่า Chart ของคุณชื่อ 'chartSales' ***)
            chartSales.Series.Clear();
            var series = new Series("ยอดขาย (Sales)");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    // (Query นี้จะดึงยอดขาย "รายวัน" จาก 'orderproduct')
                    string query = @"
                        SELECT 
                            DATE(order_date) AS 'SaleDate', 
                            SUM(total_amount) AS 'DailyTotal'
                        FROM orderproduct
                        WHERE status = 'Completed' ";

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND order_date BETWEEN @StartDate AND @EndDate ";
                    }
                    query += " GROUP BY DATE(order_date) ORDER BY SaleDate ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDate.Value.Date);
                            cmd.Parameters.AddWithValue("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1));
                        }
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                series.Points.AddXY(reader.GetDateTime("SaleDate"), reader.GetDouble("DailyTotal"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chart: " + ex.Message);
            }
            chartSales.Series.Add(series);
        }

        // --- (ฟังก์ชันเดิมของคุณ: "อัปเกรด" ให้ฟิลเตอร์ได้) ---
        // (*** [แก้ไข!] เปลี่ยนชื่อ 'LoadSalesData' เป็น 'LoadBestSellers' ***)
        private void LoadBestSellers(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // (*** [อัปเกรด!] Query นี้ Join 3 ตาราง และ กรอง 'status' ***)
                    string query = @"
                        SELECT 
                            p.name AS 'ชื่อสินค้า (Product Name)', 
                            SUM(oi.quantity) AS 'ยอดขาย (Units Sold)', 
                            SUM(oi.quantity * oi.price_at_purchase) AS 'ยอดรวม (Total Revenue)' 
                        FROM product p
                        JOIN order_items oi ON p.id = oi.product_id
                        JOIN orderproduct op ON oi.order_id = op.order_id
                        WHERE op.status = 'Completed' "; // <-- (นับเฉพาะที่อนุมัติแล้ว)

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND op.order_date BETWEEN @StartDate AND @EndDate ";
                    }
                    query += " GROUP BY p.id, p.name ORDER BY SUM(oi.quantity) DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDate.Value.Date);
                            cmd.Parameters.AddWithValue("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1));
                        }

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // (*** [แก้ไข!] ใช้ชื่อตาราง 'dgvSalesData' ตามโค้ดเก่าของคุณ ***)
                        dgvSalesData.DataSource = dt;

                        // (คำนวณยอดรวม "ตามที่เลือก" เพื่อโชว์ใน Stat Card อันที่ 3 (ถ้ามี))
                        // (*** [สำคัญ!] ตรวจสอบว่า Label นี้ชื่อ 'lblFilteredRevenue' ***)
                        decimal filteredTotal = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            filteredTotal += Convert.ToDecimal(row["ยอดรวม (Total Revenue)"]);
                        }
                        lblTotalRevenue.Text = filteredTotal.ToString("N2") + " ฿";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading best sellers: " + ex.Message);
            }
        }

        // --- (ฟังก์ชันใหม่: เติมโค้ดให้ปุ่มฟิลเตอร์) ---

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // (*** [สำคัญ!] ตรวจสอบว่า DateTimePicker ชื่อ 'dtpStart' และ 'dtpEnd' ***)
            LoadChartData(dtpStart.Value, dtpEnd.Value);
            LoadBestSellers(dtpStart.Value, dtpEnd.Value);
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            LoadChartData(today, today);
            LoadBestSellers(today, today);
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            DateTime monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1);
            LoadChartData(monthStart, monthEnd);
            LoadBestSellers(monthStart, monthEnd);
        }

        private void btnThisYear_Click(object sender, EventArgs e)
        {
            DateTime yearStart = new DateTime(DateTime.Today.Year, 1, 1);
            DateTime yearEnd = yearStart.AddYears(1).AddSeconds(-1);
            LoadChartData(yearStart, yearEnd);
            LoadBestSellers(yearStart, yearEnd);
        }

        private void btnAllTime_Click(object sender, EventArgs e)
        {
            LoadChartData(null, null);
            LoadBestSellers(null, null);
        }

        // (*** [ลบออก!] เราไม่ได้ใช้ฟังก์ชัน 'LoadSalesData' แบบเก่าอีกแล้ว ***)
        // private void LoadSalesData() { ... } 
    }
}