using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;

namespace sfdelivery
{
    //โครงสร้างรายชื่อไฟล์ xml ใน server
    public class XMLFileList
    {
        public string filename { get; set; }
        public string modifieddate { get; set; }
        public string filesize { get; set; }
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
    //ฟังก์ชั่น Java script
    public static class JScript
    {
        public static void ShowMessage(Page context,string message)
        {
            context.Response.Write("<script>alert('"+ message +"')</script>");
        }
        public static void Redirect(Page context,string uri)
        {
            context.Response.Write("<script>window.location.href='"+uri+"';</script>");
        }
    }
    //Class นี้ใช้ทดสอบ Linq
    public class ClsEntityDemo
    {
        string err = "";
        public string Error()
        {
            return err;
        }        
        public DataTable GetStaffGroup()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GroupID");
            dt.Columns.Add("GroupName");
            DataRow row = dt.NewRow();
            row[0] = "0";
            row[1] = "Driver";
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = "1";
            row[1] = "Worker";
            dt.Rows.Add(row);
            return dt;
        }
        public List<Staff> GetDemoStaff()
        {
            SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
            var data = from e in db.Staffs
                       select e;
            return data.OrderBy(e => e.StaffName).ToList();
        }
        public int FindIDStaff(string staffname)
        {
            SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
            var emp = from e in db.Staffs
                      where e.StaffName == staffname
                      select e;
            Staff data = emp.SingleOrDefault();
            if (data != null)
            {
                return data.StaffID;
            }
            else
            {
                try
                {
                    return db.Staffs.Max(e => e.StaffID) + 1;
                }
                catch
                {
                    return 1;
                }
            }
            
        }
        public bool SaveDataStaff(Staff value)
        {
            bool success = false;
            try
            {
                SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
                int id = FindIDStaff(value.StaffName);
                Staff data = db.Staffs.Find(id);
                if (data != null)
                {
                    data.StaffName = value.StaffName;
                    data.StaffPosition = value.StaffPosition;
                }
                else
                {
                    value.StaffID = id;
                    db.Staffs.Add(value);
                }
                db.SaveChanges();
                success = true;
            }
            catch(Exception ex)
            {
                err = ex.Message;
            }
            return success;
        }
        public bool InsertDemoData()
        {
            bool success = false;
            try
            {
                SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
                Staff emp = new Staff();
                if (FindIDStaff("Boat")==0)
                {
                    emp.StaffID = 1;
                    emp.StaffName = "Boat";
                    emp.StaffPosition = StaffPosition.Driver;
                    db.Staffs.Add(emp);
                    db.SaveChanges();
                }
                if (FindIDStaff("Nice") == 0)
                {
                    emp = new Staff();
                    emp.StaffID = 2;
                    emp.StaffName = "Nice";
                    emp.StaffPosition = StaffPosition.Driver;
                    db.Staffs.Add(emp);
                    db.SaveChanges();
                }
                if (FindIDStaff("พม่า") == 0)
                {
                    emp = new Staff();
                    emp.StaffID = 3;
                    emp.StaffName = "พม่า";
                    emp.StaffPosition = StaffPosition.Worker;
                    db.Staffs.Add(emp);
                    db.SaveChanges();
                }
                success = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return success;
        }
    }
}