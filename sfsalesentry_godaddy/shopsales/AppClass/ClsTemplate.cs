using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using System.Data;
namespace shopsales
{
    //ประเภทการคำนวณส่วนแบ่งห้าง
    public enum CalGPType
    {
        AverageDiscount = 0, //ไม่แชร์ส่วนลด
        ShareDiscount = 1, //แชร์ส่วนลด
        CompanyTakeDiscount = 2 //บริษัทรับภาระส่วนลดเอง
    }
    //ประเภทรายงานสรุป
    public enum ReportPeriod
    {
        Monthly =0,
        Quarter =1
    }
    //โครงสร้าง running
    public class Running
    {
        public string OID { get; set; }
        public string RunningNo { get; set; }
        public string Message { get; set; }
    }
    //โครงสร้างเดือน
    public class MonthLOV
    {
        //<summary>
        //class for display month in any language
        //</summary>
        public string MonthID { get; set; } //id for month
        public string MonthNameEN { get; set; } //name Eng
        public string MonthNameTH { get; set; } // name th
        public string LastDate { get; set; }
        public string FirstDate { get; set; }
        public MonthLOV()
        {

        }
        public MonthLOV(string month)
        {
            MonthID = month;
            MonthNameTH = GetMonthNameTH();
            MonthNameEN = GetMonthNameEN();
            LastDate= Convert.ToDateTime(DateTime.Today.Year.ToString("0000")+"-" + MonthID + "-01").AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            FirstDate = DateTime.Today.Year.ToString("0000") + "-" + MonthID + "-01";
        }
        public void GetMonthValue(string ondate)
        {
            MonthID = Convert.ToDateTime(ondate).Month.ToString("00");
            LastDate = Convert.ToDateTime(Convert.ToDateTime(ondate).Year.ToString("0000")+"-" + MonthID + "-01").AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            FirstDate = Convert.ToDateTime(ondate).Year + "-" + MonthID + "-01";
            MonthNameTH = GetMonthNameTH();
            MonthNameEN = GetMonthNameEN();
        }
        public double DayDiff(DateTime d1,DateTime d2)
        {
            TimeSpan diff = (d2 - d1);
            return diff.TotalDays;
        }
        public int MonthDiff(DateTime d1,DateTime d2)
        {
            int diff = 0;
            if(d1.Year ==d2.Year )
            {
                diff = (d2.Month - d1.Month)+1;
            }
            else
            {
                int diff1 = (12 - d1.Month)+1; //diff for first year
                int diff2 = d2.Month; //diff for last year
                int diffyear = ((d2.Year - d1.Year) - 1) * 12; //diff year 
                diff = diff1 + diff2 + diffyear; 
            }
            return diff;
        }
        public List<string> GetPeriod()
        {
            DateTime d1 = Convert.ToDateTime(FirstDate);
            DateTime d2 = Convert.ToDateTime(LastDate);
            return GetPeriod(d1, d2);
        }
        public List<string> GetPeriod(DateTime d1,DateTime d2)
        {
            int diff = MonthDiff(d1,d2);
            List<string> lov = new List<string>();
            for(int i=0;i< diff;i++)
            {
                lov.Add(d1.AddMonths(i).ToString("yyyyMM"));
            }
            return lov;
        }
        private string GetMonthNameTH()
        {
            string msg = "";
            switch (MonthID)
            {
                case "01": msg = "ม.ค."; break;
                case "02": msg = "ก.พ."; break;
                case "03": msg = "มี.ค."; break;
                case "04": msg = "เม.ย."; break;
                case "05": msg = "พ.ค."; break;
                case "06": msg = "มิ.ย."; break;
                case "07": msg = "ก.ค."; break;
                case "08": msg = "ส.ค."; break;
                case "09": msg = "ก.ย."; break;
                case "10": msg = "ต.ค."; break;
                case "11": msg = "พ.ย."; break;
                case "12": msg = "ธ.ค."; break;
                default: msg = "(ไม่ระบุ)"; break;
            }
            return msg;
        }
        private string GetMonthNameEN()
        {
            string msg = "";
            switch (MonthID)
            {
                case "01": msg = "Jan"; break;
                case "02": msg = "Feb"; break;
                case "03": msg = "Mar"; break;
                case "04": msg = "Apr"; break;
                case "05": msg = "May"; break;
                case "06": msg = "Jun"; break;
                case "07": msg = "Jul"; break;
                case "08": msg = "Aug"; break;
                case "09": msg = "Sep"; break;
                case "10": msg = "Oct"; break;
                case "11": msg = "Nov"; break;
                case "12": msg = "Dec"; break;
                default: msg = "(N/A)"; break;
            }
            return msg;
        }
    }
    //โครงสร้างรายชื่อไฟล์ xml ใน server
    public class XMLFileList
    {
        public string filename { get; set; }
        public DateTime modifieddate { get; set; }
        public long filesize { get; set; }
    }
    //โครงสร้างข้อมูลการจัดรายการ
    public class Promotion
    {
        public string SalesType { get; set; }
        public double GPX { get; set; }
        public double ShareDiscount { get; set; }
        public double DiscountRate { get; set; }
        public int CalculateType { get; set; }
        public double GPRate()
        {
            double gp = 100 - GPX;
            switch (CalculateType)
            {
                case 0: //ไม่แชร์ส่วนลด
                    if (DiscountRate > 0)
                    {
                        gp = (100 - DiscountRate) - (GPX * ((100 - DiscountRate) / 100));
                    }
                    break;
                case 1: //แชร์ส่วนลด
                    gp = gp - (DiscountRate * (ShareDiscount / 100));
                    break;
                case 2: //หักส่วนลด
                    gp = gp - DiscountRate;
                    break;
                default:
                    break;
            }
            return Convert.ToDouble(gp * 0.01);
        }
    }
    //โครงสร้างสำหรับเก็บการเข้าถึงข้อมูล
    public class AccessTableLog
    {
        public string tablename
        {
            get; set;
        }
        public string sessionid
        {
            get; set;
        }
    }
    //โครงสร้างสิทธิการใช้งาน
    public class RoleStruct
    {
        public int roleID { get; set; }
        public string roleName { get; set; }
    }
    //โครงสร้างข้อมูลสต๊อกสินค้า
    public class StockData
    {
        public int oid { get; set; }
        public string StockCode { get; set; }
        public string GoodsCode { get; set; }
        public string GoodsName { get; set; }
        public int StockType { get; set; }
        public string TransactionState { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public double StockQty { get; set; }
        public double StockPrice { get; set; }
        public double StockAmount { get; set; }
        public double AdjustAmount { get; set; }
        public double StockTotal { get; set; }
        public string CreateDate { get; set; }
    }
}