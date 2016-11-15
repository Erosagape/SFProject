using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SfDeliverTracking
{
    public static class CFunction
    {
        public enum enumLogEventID
        {
            Others = 0,
            Query = 1,
            Execute = 2
        }
        #region data type conversion (general)
        public static char CHR(int pmValue)
        {
            try
            {
                return System.Convert.ToChar(pmValue);
            }
            catch
            {
                return System.Convert.ToChar(0);
            };
        }

        /// <summary>
        /// แปลงค่า boolean เป็นค่า 1,0
        /// </summary>
        /// <param name="pmValue">ค่า boolean</param>
        public static string CBitStr(Boolean pmValue)
        {
            if (pmValue == true)
            {
                return "1";
            }
            else
            {
                return "0";
            };
        }
        public static bool CBool(string pmValue)
        {
            bool myReturn = false;

            //สำหรับ MySql
            if (pmValue == "1") return true;
            if (pmValue == "0") return false;

            try
            {
                myReturn = System.Convert.ToBoolean(pmValue);
            }
            catch
            {
                myReturn = false;
            };
            return myReturn;
        }
        public static bool CBool(object pmValue)
        {
            bool myReturn = false;
            try
            {
                myReturn = System.Convert.ToBoolean(pmValue);
            }
            catch
            {
                myReturn = false;
            };
            return myReturn;
        }

        /// <summary>
        /// แปลงค่า boolean ให้เป็น word -> "true","false"
        /// </summary>
        public static string CBoolWord(bool pmValue)
        {
            if (pmValue == true)
            {
                return "true";
            }
            else
            {
                return "false";
            };
        }

        /// <summary>
        /// แปลงค่า boolean ให้อยู่ในรูปของ 0, 1 เพื่อใช้กับ SQL Command
        /// </summary>
        /// <param name="pmValue">ค่า true/false</param>
        public static string CBoolSql(bool pmValue)
        {
            if (pmValue == true)
            {
                return "1";
            }
            else
            {
                return "0";
            };
        }
        public static byte CByte(string pmValue)
        {
            byte myReturn = 0;
            try
            {
                myReturn = System.Convert.ToByte(pmValue);
            }
            catch
            {
                //do nothing
            };
            return myReturn;
        }
        public static string CString(string pmValue)
        {
            string myReturn = string.Empty;
            try
            {
                myReturn = pmValue;
            }
            catch
            {
                myReturn = string.Empty;
            };
            return myReturn;
        }
        public static int CInt(string pmValue)
        {
            //[28-Aug-2010] PQ : แต่เดิมใช้เป็น Int16 แต่กลัวจะเกิดปัญหาจากการนำไปใช้กับค่าที่เกิน 32768
            //ซึ่งจะทำให้เกิด error ขึ้นได้ จึงเปลี่ยนให้ใช้เป็น Int32 ไปเลย ส่วนถ้าต้องการจะใช้ Int16 จริงๆ 
            //ให้ใช้ CInt16 แทน
            int myReturn = 0;
            try
            {
                myReturn = System.Convert.ToInt32(pmValue);
            }
            catch
            {
                //do nothing
            };
            return myReturn;
        }
        public static int CInt(double pmValue)
        {
            try
            {
                return System.Convert.ToInt32(pmValue);
            }
            catch
            {
                return 0;
            };
        }

        public static Int16 CInt16(string pmValue)
        {
            Int16 myReturn = 0;
            try
            {
                myReturn = System.Convert.ToInt16(pmValue);
            }
            catch
            {
                //do nothing
            };
            return myReturn;
        }
        public static Int32 CInt32(string pmValue)
        {
            Int32 myReturn = 0;
            try
            {
                myReturn = System.Convert.ToInt32(pmValue);
            }
            catch
            {
                //do nothing
            };
            return myReturn;
        }
        public static Int64 CInt64(string pmValue)
        {
            Int64 myReturn = 0;
            try
            {
                myReturn = System.Convert.ToInt64(pmValue);
            }
            catch
            {
                //do nothing
            };
            return myReturn;
        }
        public static Double CDouble(string pmValue)
        {
            Double myReturn = 0;
            try
            {
                myReturn = System.Convert.ToDouble(pmValue);
            }
            catch
            {
                //do nothing
            };
            return myReturn;
        }
        public static Decimal CDecimal(string pmValue)
        {
            Decimal myReturn = 0;
            try
            {
                myReturn = System.Convert.ToDecimal(pmValue);
            }
            catch
            {
                //do nothing
            };
            return myReturn;
        }
        #endregion

        #region function สำหรับการ absolute ค่า numberic
        public static int AbsInt(int pmValue)
        {
            if (pmValue >= 0)
            {
                return pmValue;
            }
            else
            {
                return 0 - pmValue;
            };
        }
        public static Int64 AbsInt64(Int64 pmValue)
        {
            if (pmValue >= 0)
            {
                return pmValue;
            }
            else
            {
                return 0 - pmValue;
            };
        }
        public static Double AbsDouble(double pmValue)
        {
            if (pmValue >= 0)
            {
                return pmValue;
            }
            else
            {
                return 0 - pmValue;
            };
        }
        public static Decimal AbsDecimal(decimal pmValue)
        {
            if (pmValue >= 0)
            {
                return pmValue;
            }
            else
            {
                return 0 - pmValue;
            };
        }
        #endregion

        #region data type conversion (date/time) and date function
        /// <summary>
        /// เช็คว่าค่าวันที่ใน type ที่เป็น string ถูกต้องหรือไม่?
        /// </summary>
        /// <param name="pmDate">ค่าวันที่ในรูปแบบ string (dd MMM yyyy)</param>
        public static bool IsDate(string date_long)
        {
            bool IsDate = false;
            try
            {
                DateTime oDate = DateTime.Parse(date_long);
                if ((oDate.Year >= 1900) && (oDate.Year < 2500))
                {
                    IsDate = true;
                };
            }
            catch
            {
                IsDate = false;
            };
            return IsDate;
        }

        public static string GetYear(string date_long)
        {
            try
            {
                DateTime dt = System.Convert.ToDateTime(date_long);
                return dt.Year.ToString();
            }
            catch
            {
                return "";
            };
        }
        /// <summary>
        /// เช็คว่าค่าปี valid หรือไม่ (กำหนด 1901-2300)
        /// </summary>
        /// <param name="pmYear">ค่า year ที่เป็น string</param>
        public static bool IsYear(string pmYear)
        {
            bool value = false;
            try
            {
                int nYear = CInt(pmYear);

                //ถ้าค่าปี มากกว่า 2500 แสดงว่าเป็น พ.ศ.
                if (nYear > 2500)
                {
                    nYear -= 543;
                };

                if ((nYear > 1900) && (nYear < 2100))
                {
                    value = true;
                };
            }
            catch { };
            return value;
        }
        /// <summary>
        /// เช็คค่าที่ระบุ ว่าเป็น numeric หรือไม่?
        /// </summary>
        /// <param name="pmValue">ค่าที่ต้องการตรวจสอบ</param>
        public static bool IsNumeric(string pmValue)
        {
            //** เช็คโดยการทดลองแปลงเป็น double
            try
            {
                double d = System.Convert.ToDouble(pmValue);
                return true;
            }
            catch
            {
                return false;
            };
        }

        /// <summary>
        /// คืนค่า DateTime ที่เป็น null (ระบุเป็น 01-01-1900)
        /// </summary>
        public static DateTime CDateNull
        {
            get { return DateTime.Parse("01 JAN 1900"); }
        }

        /// <summary>
        /// เปลี่ยนวันที่จากหน้าจอ(dd/mm/yyyy) ไปเป็น format สำหรับใช้กับ database (dd mmm yyyy)
        /// </summary>
        /// <param name="pmDate">วันที่ในรูปแบบ dd/mm/yyyy</param>
        public static string CDateLong(string pmDate, bool m_RegionTH = false)
        {
            /*
             * ค่าวันที่ 01/01/1900 ใช้ระบุวันที่ที่เป็น null เพราะ .net ไม่รองรับค่าวันที่ที่เป็น null
            */
            if (string.IsNullOrEmpty(pmDate)) return "";
            if (pmDate.Length > 10) pmDate = pmDate.Substring(0, 10);   //ถ้า length เกิน 10 ต้องตัดออก เพราะส่วนที่เหลือ อาจเป็นข้อมูลเวลา

            if (pmDate.CompareTo(@"01/01/1901") == 0)
            {
                return "";
            };

            string sDateLong = "";
            try
            {
                string[] dt = pmDate.Split('/');
                DateTime dateValue = new DateTime(CInt(dt[2]), CInt(dt[1]), CInt(dt[0]));
                //*** กำหนด format ผ่าน method ของ C# เองไม่ได้ เพราะจะเกิดปัญหาคืนค่าเดือนที่เป็นภาษาไทย (บาง Server)
                string dd, mmm, yyyy;
                int nYear;
                dd = dateValue.Day.ToString("00");
                mmm = GetMonthShort(dateValue.Month);
                if (m_RegionTH == true) mmm = GetMonthThai(dateValue.Month);
                nYear = dateValue.Year;

                //ถ้า .Net คืนค่าปีมาเกิน 2500 แสดงว่ามีปัญหาเรื่องประเภทศักราช ก็จะต้องแก้กลับเองให้เป็น ค.ศ
                if (nYear > 2500) nYear = nYear - 543;

                yyyy = nYear.ToString("0000");
                sDateLong = dd + " " + mmm + " " + yyyy;
                //sDateLong = System.Convert.ToDateTime(dateValue).ToString("dd MMM yyyy");
            }
            catch
            {
                sDateLong = "";
            };
            return sDateLong;
        }
        public static string CDateLong(DateTime pmDate,bool m_RegionTH=false)
        {
            /*
             * ถ้าค่าวันที่น้อยกว่า 1901 แสดงว่าเป็น null (ไม่ระบุวันที่)
             * แต่เช็คแค่ปี ค.ศ ก็พอ เพื่อจะได้ไม่ต้องยุ่งยากกับเรื่อง format วันที่
            */
            if (pmDate.Year < 1901)
            {
                return "";
            };

            string dd, mmm, yyyy;
            int nYear;
            dd = pmDate.Day.ToString("00");
            mmm = GetMonthShort(pmDate.Month);
            if (m_RegionTH == true) mmm = GetMonthThai(pmDate.Month);
            nYear = pmDate.Year;

            //ถ้า .Net คืนค่าปีมาเกิน 2500 แสดงว่ามีปัญหาเรื่องประเภทศักราช ก็จะต้องแก้กลับเองให้เป็น ค.ศ
            if (nYear > 2500) nYear = nYear - 543;

            yyyy = nYear.ToString("0000");
            return dd + " " + mmm + " " + yyyy;
        }
        public static string CDateLong(object pmDate)
        {
            try
            {
                DateTime d = Convert.ToDateTime(pmDate);
                return CDateLong(d);
            }
            catch
            {
                return "";
            };
        }
        public static string GetMonthShort(int pmMonth)
        {
            //คืนเดือนที่เป็น 3 หลัก -> index แรกต้องระบุด้วย เพราะ index ของ array จะเริ่มต้นที่ 0
            string[] aMonth = new string[13] { "XXX", "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            return aMonth[pmMonth];
        }
        /// <summary>
        /// แปลงค่าเดือนจาก string (เช่น JAN, JANUARY) เป็นตัวเลข
        /// </summary>
        /// <param name="pmMonth">ชื่อเดือน</param>
        public static int GetMonthNo(string pmMonth)
        {
            int month = 1;
            switch (Left(pmMonth.ToUpper(), 3))
            {
                case "JAN":
                    month = 1;
                    break;
                case "FEB":
                    month = 2;
                    break;
                case "MAR":
                    month = 3;
                    break;
                case "APR":
                    month = 4;
                    break;
                case "MAY":
                    month = 5;
                    break;
                case "JUN":
                    month = 6;
                    break;
                case "JUL":
                    month = 7;
                    break;
                case "AUG":
                    month = 8;
                    break;
                case "SEP":
                    month = 9;
                    break;
                case "OCT":
                    month = 10;
                    break;
                case "NOV":
                    month = 11;
                    break;
                case "DEC":
                    month = 12;
                    break;
                default:
                    break;
            };

            return month;
        }

        /// <summary>
        /// แปลงวันที่จาก DateTime ให้เป็น string ในรูปแบบ dd/mm/yyyy เพื่อใช้แสดงบนหน้าจอ
        /// </summary>
        /// <param name="pmDate">วันที่ที่มี data type เป็น DateTime</param>
        public static string CDateScreen(DateTime pmDate)
        {
            /*
             * ถ้าค่าวันที่เป็น 01/01/1900 แสดงว่าเป็น null (ไม่ระบุวันที่)
             * แต่เช็คแค่ปี ค.ศ ก็พอ เพื่อจะได้ไม่ต้องยุ่งยากกับเรื่อง format วันที่
            */
            if (pmDate == null)
            {
                return "";
            };

            if (pmDate.Year < 1901)
            {
                return "";
            };

            string dd, mm, yyyy;
            int nYear;
            dd = pmDate.Day.ToString("00");
            mm = pmDate.Month.ToString("00");
            nYear = pmDate.Year;

            //ถ้า .Net คืนค่าปีมาเกิน 2500 แสดงว่ามีปัญหาเรื่องประเภทศักราช ก็จะต้องแก้กลับเองให้เป็น ค.ศ
            if (nYear > 2500) nYear = nYear - 543;

            yyyy = nYear.ToString("0000");
            return dd + "/" + mm + "/" + yyyy;
        }

        public static string CDateScreen(object pmDate)
        {
            string scr_date = "";

            if (pmDate is DateTime)
            {
                try
                {
                    DateTime oDate = Convert.ToDateTime(pmDate);
                    return CDateScreen(oDate);
                }
                catch { };
            };

            return scr_date;
        }
        /// <summary>
        /// แสดงข้อมูลทั้งวันที่และเวลา
        /// </summary>
        /// <param name="pmDate">ค่าวันที่ที่เป็น DateTime Object</param>
        public static string CDateTimeFull(DateTime pmDate)
        {
            /*
             * ถ้าค่าวันที่เป็น 01/01/1900 แสดงว่าเป็น null (ไม่ระบุวันที่)
             * แต่เช็คแค่ปี ค.ศ ก็พอ เพื่อจะได้ไม่ต้องยุ่งยากกับเรื่อง format วันที่
            */
            if (pmDate.Year < 1901)
            {
                return "";
            };

            string dd, MM, yyyy, hh, mm;
            int nYear;
            dd = pmDate.Day.ToString("00");
            MM = pmDate.Month.ToString("00");
            hh = pmDate.Hour.ToString("00");
            mm = pmDate.Minute.ToString("00");
            nYear = pmDate.Year;

            //ถ้า .Net คืนค่าปีมาเกิน 2500 แสดงว่ามีปัญหาเรื่องประเภทศักราช ก็จะต้องแก้กลับเองให้เป็น ค.ศ
            if (nYear > 2500) nYear = nYear - 543;

            yyyy = nYear.ToString("0000");
            return dd + "/" + MM + "/" + yyyy + " " + hh + ":" + mm;
        }
        /// <summary>
        /// แปลงข้อความวันที่สำหรับส่งไปยัง UPDATE statement (Transact-SQL) โดยวันที่ไม่ถูกต้องก็จะระบุข้อความเป็นคำว่า "null"
        /// </summary>
        /// <param name="pmDate">วันที่เป็น string</param>
        public static string CDateSql(string pmDate)
        {
            try
            {
                string sValue = "";
                DateTime oDate = System.Convert.ToDateTime(pmDate);
                if (oDate.Year > 1900)
                {
                    sValue = "'" + CDateLong(oDate) + "'";
                }
                else
                {
                    sValue = "null";
                };
                return sValue;
            }
            catch
            {
                return "null";
            };
        }
        public static string CDateSql(DateTime oDate)
        {
            try
            {
                string sValue = "";
                if (oDate.Year > 1900)
                {
                    sValue = "'" + CDateLong(oDate) + "'";
                }
                else
                {
                    sValue = "null";
                };
                return sValue;
            }
            catch
            {
                return "null";
            };
        }

        /// <summary>
        /// วันที่กรณีที่ค่าวันที่เป็น null
        /// </summary>
        public static DateTime DateNull()
        {
            //เนื่องจากไม่สามารถ set ค่า null ให้กับ Data type ที่เป็น DateTime ได้
            //จึงใช้วิธี set ให้เป็นค่า 01/01/0001 แล้วไปดัก function ที่เกี่ยวข้องเอา
            return DateTime.Parse("01 JAN 0001");
        }
        /// <summary>
        /// แปลงวันที่จาก string ให้เป็น DateTime object
        /// </summary>
        /// <param name="pmDate">วันที่ที่มี type เป็น string</param>
        public static DateTime CDateObject(string pmDate)
        {
            try
            {
                string dt_long = CDateLong(pmDate);
                DateTime dt = Convert.ToDateTime(dt_long);
                return dt;
                //return Convert.ToDateTime(pmDate);
            }
            catch
            {
                //เนื่องจากไม่สามารถ set ค่า null ให้กับ Data type ที่เป็น DateTime ได้
                //จึงใช้วิธี set ให้เป็นค่า 01/01/0001 แล้วไปดัก function ที่เกี่ยวข้องเอา
                //return DateTime.Parse("01 JAN 0001");
                return DateNull();
            };
        }
        /// <summary>
        /// บวกจำนวนวันเข้าไปในวันที่
        /// </summary>
        /// <param name="pmDate">วันที่ตั้งต้น ควรเป็น dd mmm yyyy เพื่อไม่ให้มีปัญหา เดือนสลับกับวัน</param>
        /// <param name="pmDay">จำนวนวันที่จะบวกเข้าไป</param>
        public static string DateAddDay(string pmDate, double pmDay)
        {
            try
            {
                DateTime date = System.Convert.ToDateTime(pmDate);
                date = date.AddDays(pmDay);
                return CDateLong(date);
            }
            catch
            {
                return pmDate;
            };
        }

        /// <summary>
        /// เช็คค่า/ข้อมูล Date แล้วดึงเฉพาะส่วนที่เป็น date ออกมาเท่านั้น ในรูปแบบ dd mmm yyyy(ในกรณีที่มีเวลาผสมอยู่ด้วย)
        /// </summary>
        /// <param name="pmDate">ค่าวันที่ (อาจจะมีข้อมูล time อยู่ด้วยก็ได้)</param>
        public static string DateTrim(string pmDate)
        {
            string sDate = "";
            if (!string.IsNullOrEmpty(pmDate))
            {
                //ถ้ายาวเกิน 11 ตัว แสดงว่ามีเวลาปนอยู่
                if (pmDate.Length > 11)
                {
                    pmDate = Left(pmDate, 10);       //วันที่ที่มีเวลาปนอยู่จะมี format ของวันที่เป็น dd/mm/yyyy ซึ่งก็คือ 10 char.
                    sDate = CDateLong(pmDate);
                }
                else
                {
                    sDate = pmDate;
                };
            };
            return sDate;
        }

        /// <summary>
        /// คืนค่าวันที่ปัจจุบัน
        /// </summary>
        public static string ToDay()
        {
            return DateTime.Now.ToString("dd MMM yyyy");
        }
        public static string TodayThai(bool m_RegionTH = false)
        {
            DateTime dt = DateTime.Now;
            string day = dt.Day.ToString("00");

            if (!IsYear(dt.Year.ToString()))
            {
                dt = dt.AddYears(-543);
            }
            string year = dt.Year.ToString();
            string month = GetMonthShort(dt.Month);
            if (m_RegionTH == true)
            {
                month = GetMonthThai(dt.Month);
            };
            //string month = GetMonthThai(dt.Month);

            string date = day + " " + month + " " + year;
            return date;
        }
        public static string GetDateThai(DateTime Date, bool m_RegionTH = false)
        {
            string date = "";
            try
            {
                string day = Date.Day.ToString("00");

                if (!IsYear(Date.Year.ToString()))
                {
                    Date = Date.AddYears(-543);
                }
                string year = Date.Year.ToString();

                string month = GetMonthShort(Date.Month);
                if (m_RegionTH == true)
                {
                    month = GetMonthThai(Date.Month);
                };
                //string month = GetMonthThai(Date.Month);

                date = day + " " + month + " " + year;
            }
            catch { }
            return date;
        }
        public static string GetDateThai(string Date)
        {
            string date = "";
            try
            {
                string day = Date.Substring(0, 2);
                string month = Date.Substring(3, 2);
                string year = Date.Substring(6, 4);
                if (!IsYear(year))
                {
                    year = (Convert.ToInt32(year) - 543).ToString();
                }

                month = GetMonthThai(Convert.ToInt32(month));

                date = day + " " + month + " " + year;
            }
            catch { }
            return date;
        }
        public static string GetMonthThai(int Month)
        {
            string MonthName = "";

            switch (Month)
            {
                case 1:
                    MonthName = "ม.ค.";
                    break;
                case 2:
                    MonthName = "ก.พ.";
                    break;
                case 3:
                    MonthName = "มี.ค.";
                    break;
                case 4:
                    MonthName = "เม.ย.";
                    break;
                case 5:
                    MonthName = "พ.ค.";
                    break;
                case 6:
                    MonthName = "มิ.ย.";
                    break;
                case 7:
                    MonthName = "ก.ค.";
                    break;
                case 8:
                    MonthName = "ส.ค.";
                    break;
                case 9:
                    MonthName = "ก.ย.";
                    break;
                case 10:
                    MonthName = "ต.ค.";
                    break;
                case 11:
                    MonthName = "พ.ย.";
                    break;
                case 12:
                    MonthName = "ธ.ค.";
                    break;
                default:
                    break;
            };

            return MonthName;
        }
        #endregion

        #region string function
        /// <summary>
        /// แปลงค่า string เป็น blank ถ้า string นั้นมีค่าตามเงื่อนไข (expression)
        /// </summary>
        /// <param name="pmValue">ค่า string</param>
        /// <param name="pmExpression">เงื่อนไขของค่าที่ต้องการให้แปลงเป็น null/blank</param>
        public static string NullIf(string pmValue, string pmExpression)
        {
            /*
             * value มีค่าตามเงื่อนไข expression ก็จะคืนค่าเป็น null
             * แต่ถ้าไม่เท่า ก็จะคืนค่า value กลับไปแทน
            */
            string sValue = null;
            try
            {
                if (pmValue != pmExpression)
                {
                    sValue = pmValue;
                };
            }
            catch
            {
                //do nothing
            };
            return sValue;
        }
        public static string BlankIf(string pmValue, string pmExpression)
        {
            /*
             * value มีค่าตามเงื่อนไข expression ก็จะคืนค่าเป็น blank (ไม่ใช่ null)
             * แต่ถ้าไม่เท่า ก็จะคืนค่า value กลับไปแทน
            */
            string sValue = "";
            try
            {
                if (pmValue != pmExpression)
                {
                    sValue = pmValue;
                };
            }
            catch
            {
                //do nothing
            };
            return sValue;
        }

        /// <summary>
        /// convert ค่าที่เป็น null ให้กลายเป็น blank แต่ถ้ามีค่า ก็จะคืนเป็นค่า string นั้น
        /// </summary>
        /// <param name="pmValue">ค่า string</param>
        public static string CNullToBlank(string pmValue)
        {
            if (string.IsNullOrEmpty(pmValue))
            {
                return "";
            }
            else
            {
                return pmValue;
            };
        }

        /// <summary>
        /// เช็คว่าค่า string ทุกค่าที่ส่งเข้ามานั้น มีค่าไหนเป็น null/blank หรือไม่?
        /// </summary>
        public static bool HaveNull(params string[] list)
        {
            foreach (string str in list)
            {
                if (string.IsNullOrEmpty(str)) return true;
            };

            return false;
        }
        public static string Left(string sText, int nLength)
        {
            if (sText.Length <= nLength)
            {
                return sText;
            }
            else
            {
                return sText.Substring(0, nLength);
            };
        }
        public static string Right(string sText, int nLength)
        {
            if (sText.Length <= nLength)
            {
                return sText;
            }
            else
            {
                return sText.Substring(sText.Length - nLength, nLength);
            };
        }
        public static string PadLeft(string sText, int nLength)
        {
            //ใช้แทน String.PadLeft เพราะ String.PadLeft ไม่ได้ทำการ substring() อย่างถูกต้อง
            if (sText.Length >= nLength)
            {
                return sText.Substring(0, nLength);
            }
            else
            {
                return sText.PadLeft(nLength, ' ');
            };
        }
        public static string PadRight(string sText, int nLength)
        {
            if (sText.Length >= nLength)
            {
                return sText.Substring(0, nLength);
            }
            else
            {
                return sText.PadRight(nLength, ' ');
            };
        }
        /// <summary>
        /// ตัด string ด้านซ้ายออกจำนวน n ตัวอักษร
        /// </summary>
        public static string CutLeft(string sText, int nLength)
        {
            sText += "";    //แก้ปัญหากรณีค่าถูกส่งมาเป็น null
            if (sText.Length <= nLength)
            {
                return sText;
            }
            else
            {
                return sText.Substring(nLength, sText.Length - nLength);
            };
        }

        /// <summary>
        /// ตัด string ด้านขวาออกจำนวน n ตัวอักษร
        /// </summary>
        public static string CutRight(string sText, int nLength)
        {
            sText += "";    //แก้ปัญหากรณีค่าถูกส่งมาเป็น null
            if (sText.Length <= nLength)
            {
                return sText;
            }
            else
            {
                return sText.Substring(0, sText.Length - nLength);
            };
        }
        public static string Substring(string sText, int nStart, int nLength)
        {
            if (sText.Length < nStart)
            {
                return "";
            }
            else
            {
                return sText.Substring(nStart, nLength);
            };
        }
        public static string AppendText(string pmText1, string pmText2, string pmTextMiddle)
        {
            //เชื่อมต่อข้อความเข้าด้วยกันโดยมี text เชื่อมได้ด้วย
            string sText = pmText1;
            if (pmText2 != "")
            {
                if (sText != "") sText += pmTextMiddle;
                sText += pmText2;
            };
            return sText;
        }

        public static string CSqlDate(string pmValue)
        {
            //แปลงวันที่เพื่อใช้ในคำสั่ง SQL
            try
            {
                DateTime oDate = DateTime.Parse(pmValue);
                string sDate = "";
                string dd, mmm, yyyy;
                int nYear;
                dd = oDate.Day.ToString("00");
                mmm = GetMonthShort(oDate.Month);
                nYear = oDate.Year;

                //if (m_IsCultureThai == true)
                //{
                //    if (nYear < 2501) nYear += 543;
                //}
                //else
                //{
                //    //ถ้า .Net คืนค่าปีมาเกิน 2500 แสดงว่ามีปัญหาเรื่องประเภทศักราช ก็จะต้องแก้กลับเองให้เป็น ค.ศ
                //    if (nYear > 2500) nYear = nYear - 543;
                //};
                //if (nYear < 1900) nYear += 543;
                if (nYear > 2500) nYear -= 543;

                yyyy = nYear.ToString("0000");
                sDate = dd + " " + mmm + " " + yyyy;

                return "'" + sDate + "'";
            }
            catch
            {
                return "null";
            };
        }
        /// <summary>
        /// แปลงชุด string ที่คั่นด้วยเครื่องหมาย comma ให้เป็นชุด string สำหรับใช้กับเงื่อนไข IN ในคำสั่ง SQL Select ซึ่งต้องมีเครื่องหมาย single-quote คร่อมแต่ละค่า
        /// </summary>
        /// <param name="pmListPK">list ของค่า PK ที่คั่นด้วยเครื่องหมาย comma เช่น 0001,0002,...</param>
        public static string CSqlListPK(string pmListPK)
        {
            string sList = "", sPK;
            foreach (string pk in pmListPK.Split(','))
            {
                sPK = pk.Replace(" ", "");   //เอาเครื่องหมาย space bar ออกจากค่า PK ก่อนเพื่อไม่ให้เกิดปัญหา

                //ใส่เครื่องหมาย single-quote คร่อมหน้า/หลัง (ถ้ายังไม่ระบุ)
                if (!sPK.StartsWith("'")) sPK = "'" + sPK;
                if (!sPK.EndsWith("'")) sPK += "'";

                if (sList != "") sList += ",";
                sList += sPK;
            };
            return sList;
        }
        /// <summary>
        /// เชื่อมต่อข้อความ 2 ข้อความเข้าด้วยกัน
        /// </summary>
        /// <param name="pmText1">ข้อความที่ 1</param>
        /// <param name="pmText2">ข้อความที่ 2 (ที่จะนำมาต่อ)</param>
        /// <param name="pmMiddle">ข้อความหรือ char ที่จะใช้คั่นข้อความที่ 1 และ 2 เช่น เครื่องหมาย comma (,) เป็นต้น</param>
        public static string TextAppend(string pmText1, string pmText2, string pmMiddle)
        {
            string sText = "";
            if ((!string.IsNullOrEmpty(pmText1)) && (!string.IsNullOrEmpty(pmText2)))
            {
                //ถ้ามีการระบุทั้งข้อความ 1 และ 2
                sText = pmText1 + pmMiddle + pmText2;
            }
            else
            {
                //ถ้ามีเพียงข้อความใดข้อความหนึ่ง หรือไม่มีเลย
                sText = pmText1 + pmText2;
            };
            return sText;
        }

        /// <summary>
        /// ตรวจสอบค่า ว่ามีค่าตามที่ระบุหรือไม่ (เหมือนพวกคำสั่ง IN (...))
        /// </summary>
        /// <param name="pmExpression">ค่าที่ต้องการเช็ค</param>
        /// <param name="pmCondition">ค่าเงื่อนไขสำหรับเทียบกับค่าที่ต้องการเช็ค</param>
        /// <returns></returns>
        public static bool IsValueIn(string pmExpression, params string[] pmCondition)
        {
            bool bValue = false;
            foreach (string cond in pmCondition)
            {
                if (pmExpression.ToLower() == cond.ToLower())
                {
                    bValue = true;
                    break;
                };
            };

            return bValue;
        }

        /// <summary>
        /// แปลงค่าจาก List template ให้เป็น string เดียวโดยคั่นด้วยเครื่องหมาย comma
        /// </summary>
        /// <param name="pmStringList">List Template ของค่าที่เป็น string</param>
        public static string CListToString(List<string> pmStringList)
        {
            string sList = "";
            string sValue = "";

            if (pmStringList != null)
            {
                foreach (string str in pmStringList)
                {
                    sValue = str;
                    sList = TextAppend(sList, sValue, ",");
                };
            };
            return sList;
        }
        /// <summary>
        /// แปลงค่าจาก List template ให้เป็น string เดียวโดยคั่นด้วยเครื่องหมาย comma
        /// </summary>
        /// <param name="pmStringList">List Template ของค่าที่เป็น string</param>
        /// <param name="IsSingleQuote">flag ระบุว่าต้องการให้มีเครื่องหมาย single-quote คร่อมค่าแต่ละค่าหรือไม่?</param>
        public static string CListToString(List<string> pmStringList, bool IsSingleQuote)
        {
            string sList = "";
            string sValue = "";

            foreach (string str in pmStringList)
            {
                sValue = str;
                if (IsSingleQuote == true)
                {
                    if (sValue.StartsWith("'") == false) sValue = "'" + sValue;
                    if (sValue.EndsWith("'") == false) sValue += "'";
                };
                sList = TextAppend(sList, sValue, ",");
            };
            return sList;
        }
        public static string CurrencyFormat(double pmDouble)
        {
            if (pmDouble == 0)
                return "0.00";
            else
                return string.Format("{0:#,##0.00;(#,##0.00);''}", pmDouble);
        }
        #endregion

        #region Function สำหรับใช้กับ Oracle ของ project นี้เท่านั้น
        /// <summary>
        /// แปลงค่า bool char จาก oracle เป็น true/false
        /// </summary>
        /// <summary>
        /// แปลงค่า boolean เป็น "Y","N" เพื่อใช้กับ database
        /// </summary>
        /// <param name="value">ค่า boolean</param>
        /// <returns></returns>
        public static string SetOrclBool(bool value)
        {
            if (value == true)
            {
                return "Y";
            }
            else
            {
                return "N";
            };
        }
        /// <summary>
        /// แปลงค่า date จาก Oracle (YYYYMMDD) เป็น dd/MM/yyyy
        /// </summary>

        /// <summary>
        /// แปลงข้อมูลวันที่จาก screen ให้มี format สำหรับ string field เก็บค่าวันที่
        /// </summary>
        /// <param name="p_date">วันที่ในรูปแบบ dd/MM/yyyy</param>

        #endregion

        #region Numeric function
        public static double Round2(double pmValue)
        {
            try
            {
                string newValue = pmValue.ToString("#,##0.00");
                return Double.Parse(newValue);
            }
            catch
            {
                return pmValue;
            };
        }
        #endregion

        /// <summary>
        /// หาค่า max value ของ parameter ที่ส่งเข้ามา
        /// </summary>
        /// <param name="list">ค่า double ที่ไม่รู้จำนวน parameter ที่แน่นอน</param>
        public static double GetMaxDouble(params double[] list)
        {
            double dValue = list.Max(d => Convert.ToDouble(d));
            return dValue;
        }
        public static double GetMinDouble(params double[] list)
        {
            double dValue = list.Min(d => Convert.ToDouble(d));
            return dValue;
        }
        public static string CDateOra(string str)
        {//ฟังชั่นในการแปลงรูปแบบวั่นที่ให้เป็นแบบ oracle
            string dateconv = CDateLong(str);
            dateconv.Replace(" ", "-");
            return dateconv;
        }
        /// <summary>
        /// เช็คจำนวนข้อมูลใน list โดยมีการ distinct ด้วย เช่น {'a','a','a'} จะได้ผลลัพธ์เป็น 1, {'a','a','b'} จะได้ผลลัพธ์เป็น 2
        /// </summary>
        /// <param name="list">ข้อมูลที่ต้องการจะ distinct count</param>
        //public static int CountDistinct(List<string> list)
        //{
        //    try
        //    {
        //        return list.Distinct().Count();
        //    }
        //    catch
        //    {
        //        return 0;
        //    };
        //}

        public static string Caption(string p_thai, string p_english, bool m_English = false)
        {
            if (m_English == true)
            {
                return p_english;
            }
            else
            {
                return p_thai;
            };
            //return "";
        }

        /// <summary>
        /// add ค่า string ไปยัง List โดยจะ add ถ้ายังไม่มีค่าใน list
        /// </summary>
        public static void AddListNew(ref List<string> list, string str)
        {
            string ss = (from p in list where (p == str) select p).FirstOrDefault();
            if (string.IsNullOrEmpty(ss))
            {
                list.Add(str);
            };
        }
    }

}
