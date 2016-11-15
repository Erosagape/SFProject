using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace sfdelivery
{
    public class ClsDeliverDb
    {
        string err = "";
        public string Error()
        {
            return err;
        }
        public List<DeliverPlace> GetDataCustomer()
        {
            SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
            var data = from e in db.DeliverPlaces
                       select e;
            return data.OrderBy(e => e.DeliverPlaceName).ToList();
        }
        public List<Staff> GetDataStaff()
        {
            SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
            var data = from e in db.Staffs
                       select e;
            return data.OrderBy(e => e.StaffName).ToList();
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
        public int FindIDCust(string custcode)
        {
            SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
            var cust = from e in db.DeliverPlaces
                       where e.DeliverPlaceCode == custcode
                       select e;
            DeliverPlace data = cust.SingleOrDefault();
            if (data!=null)
            {
                return data.DeliverPlaceId;
            }
            else
            {
                try
                {
                    return db.DeliverPlaces.Max(e => e.DeliverPlaceId) + 1;
                }
                catch
                {
                    return 1;
                }
            }
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
        public bool SaveDataCust(DeliverPlace value)
        {
            bool success = false;
            try
            {
                SfDeliveryTrackingEntities db = new SfDeliveryTrackingEntities();
                int id = FindIDCust(value.DeliverPlaceCode);
                DeliverPlace data = db.DeliverPlaces.Find(id);
                if (data != null)
                {
                    data.DeliverPlaceAddress = value.DeliverPlaceAddress;
                    data.DeliverPlaceName = value.DeliverPlaceName;
                }
                else
                {
                    value.DeliverPlaceId = id;
                    db.DeliverPlaces.Add(value);
                }
                db.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return success;
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
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return success;
        }
    }
}