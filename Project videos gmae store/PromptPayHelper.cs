using System;
using System.Text;

namespace Project_videos_gmae_store
{
    // นี่คือ "เครื่องมือ" ที่เราสร้างเอง เพื่อ "แทนที่" Package ที่พัง
    public static class PromptPayHelper
    {
        // (ค่ามาตรฐานของ PromptPay)
        private const string PAYLOAD_FORMAT = "000201";
        private const string POI_METHOD = "010212"; // 12 = dynamic (มีจำนวนเงิน)
        private const string MERCHANT_INFO_GUID = "0016A000000677010111";
        private const string MERCHANT_INFO_ID = "01"; // 01 = เบอร์มือถือ
        private const string COUNTRY_CODE = "5802TH";
        private const string CURRENCY_CODE = "5303764"; // 764 = THB

        // (ฟังก์ชันหลัก: สร้าง Text)
        public static string GeneratePayload(string targetId, decimal amount)
        {
            // 1. สร้างส่วน "ผู้รับ" (เบอร์มือถือ)
            string merchantInfoTarget = $"{MERCHANT_INFO_ID}{targetId.Length:D2}{targetId}";
            string merchantInfo = $"29{MERCHANT_INFO_GUID.Length + merchantInfoTarget.Length:D2}{MERCHANT_INFO_GUID}{merchantInfoTarget}";

            // 2. สร้างส่วน "จำนวนเงิน"
            string amountString = amount.ToString("0.00"); // (ต้องมีทศนิยม 2 ตำแหน่ง)
            string transactionAmount = $"54{amountString.Length:D2}{amountString}";

            // 3. "รวมร่าง" (ยังไม่เสร็จ)
            string payload = $"{PAYLOAD_FORMAT}{POI_METHOD}{merchantInfo}{COUNTRY_CODE}{CURRENCY_CODE}{transactionAmount}";

            // 4. "สร้าง" Checksum (รหัสตรวจสอบ)
            string crc = GetCRC16(payload + "6304");

            // 5. "รวมร่าง" (เสร็จสมบูรณ์)
            payload = $"{payload}6304{crc}";

            return payload;
        }

        // (ฟังก์ชันสำหรับคำนวณ CRC16)
        private static string GetCRC16(string data)
        {
            ushort crc = 0xFFFF;
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc = (ushort)(crc << 1);
                }
            }
            return crc.ToString("X4"); // (แปลงเป็นเลขฐาน 16)
        }
    }
}