using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;

namespace shopsales
{
    //คลาสหลักสำหรับเชื่อมต่อฐานข้อมูล
    public static class ClsData
    {
        public static string hostname = "pc.sfaerosoft.com";
        private static string serverpath = "";
        private static string errMessage = "";
        private static List<AccessTableLog> OpeningData = new List<AccessTableLog>();
        public static string Error()
        {
            return errMessage;
        }
        #region common function of class
        #region --- Database Utility
        public static bool CompareRow(DataRow row1, DataRow row2, DataTable tb)
        {
            //row1 = row to compare
            //row2 = row source to compare with
            bool pass = true;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                bool match = row1[i].ToString().Trim() == row2[i].ToString().Trim();
                if (match == false)
                {
                    pass = false;
                    break;
                }

            }
            return pass;
        }
        public static void LoadCombo(DataTable dt, DropDownList cbo, string fldname, string fldval)
        {
            cbo.DataSource = dt;
            cbo.DataTextField = fldname;
            cbo.DataValueField = fldval;
            cbo.DataBind();
        }
        public static DataTable EnumToDataTable(Type enumType)
        {
            DataTable table = new DataTable();

            //Column that contains the Captions/Keys of Enum        
            table.Columns.Add("Desc", typeof(string));
            //Get the type of ENUM for DataColumn
            table.Columns.Add("Id", Enum.GetUnderlyingType(enumType));
            //Add the items from the enum:
            foreach (string name in Enum.GetNames(enumType))
            {
                //Replace underscores with space from caption/key and add item to collection:
                table.Rows.Add(name.Replace('_', ' '), Enum.Parse(enumType, name));
            }
            return table;
        }
        private static DataTable ToDataTable<TSource>(this IList<TSource> data)
        {
            DataTable dataTable = new DataTable(typeof(TSource).Name);
            PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static DataRow QueryData(DataTable dt, string Cliteria)
        {
            DataRow dr = dt.NewRow();
            DataRow[] rowSet = dt.Select(Cliteria);
            foreach (DataRow row in rowSet)
            {
                dr = row;
                break;
            }
            return dr;
        }
        #endregion
        #region --- Web Path Function
        public static void SetPath(string path)
        {
            serverpath = path;
        }
        public static string GetPath()
        {
            if (serverpath == "") serverpath = WebServerPath() + "\\..\\";
            return serverpath;
        }
        public static string GetFullPath(string fname)
        {
            return GetPath() + fname;
        }
        private static string WebServerPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
        #endregion
        #region --- Files Utility
        public static void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }
        public static List<XMLFileList> GetSalesList(List<string> months, string filter)
        {
            List<XMLFileList> lst = new List<XMLFileList>();
            DirectoryInfo dir = new System.IO.DirectoryInfo(GetPath());
            foreach (FileInfo file in dir.GetFiles(filter + ".xml"))
            {
                if (months.Contains(file.Name.Split('_')[1].ToString()))
                {
                    lst.Add(new XMLFileList() { filename = file.Name.ToLower(), filesize = file.Length.ToString(), modifieddate = ClsUtil.GetTHDate(file.LastWriteTime).ToString("yyyy/MM/dd HH:mm") });
                }
            }
            lst.OrderBy(s => s.filename);
            return lst;
        }
        public static List<XMLFileList> GetXMLTableList(string filter = "*")
        {
            List<XMLFileList> lst = new List<XMLFileList>();
            DirectoryInfo dir = new System.IO.DirectoryInfo(GetPath());
            foreach (FileInfo file in dir.GetFiles(filter + ".xml"))
            {
                lst.Add(new XMLFileList() { filename = file.Name.ToLower(), filesize = file.Length.ToString(), modifieddate = ClsUtil.GetTHDate(file.LastWriteTime).ToString("yyyy/MM/dd HH:mm") });
            }
            lst.OrderBy(s => s.filename);
            return lst;
        }
        public static DataTable XMLTableData(string filterstr = "*")
        {
            DataTable dt = GetXMLTableList(filterstr).ToDataTable();
            return dt;
        }
        public static string ExportToXMLFile(string filename, DataTable dt)
        {
            errMessage = "";
            try
            {
                dt.WriteXml(filename);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }
            return filename;
        }
        public static bool CheckDataXML(string fname)
        {
            bool success = false;
            try
            {
                if (System.IO.File.Exists(GetPath() + fname) == true)
                {
                    success = true;
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }
        public static bool NewDataXML(string fname, string rootname = "NewDataSet")
        {
            bool success = false;
            try
            {
                XmlDocument xml = new XmlDocument();
                XmlNode docNode = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                xml.AppendChild(docNode);
                XmlNode root = xml.CreateElement(rootname);
                xml.AppendChild(root);
                xml.Save(GetPath() + fname);
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }
        public static bool NewDataXML(string fname, string rootname, string tablename)
        {
            bool success = false;
            try
            {
                XmlDocument xml = new XmlDocument();
                XmlNode docNode = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                xml.AppendChild(docNode);
                XmlNode root = xml.CreateElement(rootname);
                XmlNode tbl = xml.CreateElement(tablename);
                root.AppendChild(tbl);
                xml.AppendChild(root);
                xml.Save(GetPath() + fname + ".xml");
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }
        public static DataTable GetLockDataTable()
        {
            DataTable dt = OpeningData.ToDataTable();
            return dt;
        }
        public static bool IsLockedDataTable(string tbname, string sid)
        {
            AccessTableLog log = OpeningData.Find(x => x.tablename.Equals(tbname));
            if (log == null)
            {
                return false;
            }
            else
            {
                bool isowner = Convert.ToBoolean(sid == log.sessionid);
                if (isowner == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public static bool LockDataTable(string tbname, string sid)
        {
            AccessTableLog log = OpeningData.Find(x => x.tablename.Equals(tbname));
            if (log == null)
            {
                log = new AccessTableLog();

                log.tablename = tbname;
                log.sessionid = sid;
                if (OpeningData.IndexOf(log) == -1)
                {
                    OpeningData.Add(log);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void ClearLockTable()
        {
            OpeningData = new List<AccessTableLog>();
        }
        public static bool UnlockDataTable(string tbname, string sid)
        {
            bool unlock = false;
            AccessTableLog log = OpeningData.Find(x => x.tablename.Equals(tbname));
            if (log != null)
            {
                if (log.sessionid == sid)
                {
                    OpeningData.Remove(log);
                    unlock = true;

                }
            }
            return unlock;
        }
        #endregion
        #region --- XML Database Utility Function
        public static string GetXML(DataTable dt)
        {
            StringWriter wr = new StringWriter();
            dt.WriteXml(wr);
            return wr.ToString();
        }
        public static string GetValueXML(string fname, string filter, string fldname)
        {
            string str = "";
            foreach (DataRow dr in GetDataXML(fname, filter))
            {
                str = dr[fldname].ToString();
                break;
            }
            return str;
        }
        public static DataRow[] GetDataXML(string fname, string filter)
        {
            DataTable dt = GetDataXML(fname);
            DataRow[] dr = dt.Select(filter);
            return dr;
        }
        public static DataTable GetDataXML(string tbname)
        {
            DataSet ds = new DataSet();
            tbname = tbname.Replace(".xml", "");
            ds.ReadXml(GetPath() + tbname + ".xml");
            return ds.Tables[0];
        }
        public static DataTable FilterData(string tbname, string fieldname, string fieldval, string namesp = "Table")
        {
            XDocument doc = XDocument.Load(GetPath() + tbname + ".xml");
            var nodes = from node in doc.Descendants(namesp)
                        where node.Element(fieldname).Value.Equals(fieldval)
                        select node;
            var xml = new XElement("NewDataSet", nodes);
            return GetDataTableFromXML(xml.ToString(), tbname);
        }
        public static DataTable QueryData(string tbname, string Cliteria)
        {
            DataTable dt = GetDataXML(tbname);
            DataTable tb = new DataTable();
            if (Cliteria == "")
            {
                tb = dt.Copy();
            }
            else
            {
                tb = dt.Clone();
                DataRow[] rowSet = dt.Select(Cliteria);
                foreach (DataRow row in rowSet)
                {
                    tb.ImportRow(row);
                }
            }
            return tb;
        }
        public static bool WriteDataXML(string fname, string XMLData)
        {
            bool success = true;
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(XMLData);
                xml.Save(GetPath() + fname + ".xml");
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                success = false;
            }
            return success;
        }
        public static int UpdateDataXML(string dataname, string cliteria, string updatestr)
        {
            int i = 0;
            errMessage = "";
            string nodename = "";
            string nodevalue = "";
            try
            {
                DataSet ds = new DataSet();
                string fname = GetPath() + dataname.ToLower() + ".xml";
                ds.ReadXml(fname.ToUpper());
                DataTable dt = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select(cliteria);
                foreach (DataRow r in dr)
                {
                    foreach (string str in updatestr.Split(';'))
                    {
                        if (str.IndexOf("=") > 0)
                        {
                            string[] arr = str.Split('=');
                            nodename = arr[0].ToString().Trim();
                            nodevalue = arr[1].ToString();
                            r[nodename] = nodevalue;
                        }
                    }
                    i++;
                }
                dt.WriteXml(GetPath() + dataname.ToLower() + ".xml");
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }
            return i;
        }
        public static int DeleteDataXML(string dataname, string cliteria)
        {
            int i = 0;
            errMessage = "";
            try
            {
                DataSet ds = new DataSet();
                string fname = GetPath() + dataname + ".xml";
                ds.ReadXml(fname.ToUpper());
                DataTable dt = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select(cliteria);
                foreach (DataRow r in dr)
                {
                    dt.Rows.Remove(r);
                    i++;
                }
                dt.WriteXml(GetPath() + dataname.ToLower() + ".xml");
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }
            return i;
        }
        public static string GetNewOID(DataTable rs, string tbname, string fldname)
        {
            //check if file still call by same procedure
            errMessage = "";
            string runno = "runno";
            Int32 oid = 0;
            if (rs.Columns.IndexOf(runno) > 0)
            {
                rs.Columns.Remove(runno);
            }
            if (rs.Columns.IndexOf(runno) < 0)
            {
                try
                {
                    rs.Columns.Add(runno, typeof(int), "Convert(" + fldname + ",'System.Int32')");
                    object values = (object)rs.Compute("Max(" + runno + ")", string.Empty);
                    rs.Columns.Remove(runno);
                    oid = Convert.ToInt32(values) + 1;
                }
                catch (Exception ex)
                {
                    errMessage = ex.Message;
                    oid = 1;
                }
            }
            return oid.ToString();
        }
        #endregion
        #endregion
        #region master files data 
        public static DataTable RoleData()
        {
            DataTable dt = GetRole().ToDataTable();
            return dt;
        }
        public static DataTable SalesTypeData(bool addNew = false)
        {
            DataSet dtSalesType = new DataSet();
            dtSalesType.ReadXml(GetPath() + "SalesType.xml");
            DataTable dt = dtSalesType.Tables[0].Copy();
            if (addNew == true)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "-";
                dr[1] = "000";
                dr[2] = "ไม่ระบุ";
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }
        public static DataTable CustomerGroupData(bool addNew = false)
        {
            DataSet dtCustGroup = new DataSet();
            dtCustGroup.ReadXml(GetPath() + "CustomerGroup.xml");
            DataTable dt = dtCustGroup.Tables[0].Copy();
            if (addNew == true)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[3] = "{สร้างใหม่}";
                dt.Rows.InsertAt(dr, 0);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "CustGroupNameTh";
            return dv.ToTable();
        }
        public static DataTable ShopGroupData(string custgroup, bool addNew = false)
        {
            DataTable rs = CustomerGroupData();
            DataTable dt = rs.Clone();
            if (addNew == true)
            {
                DataRow r = dt.NewRow();
                r[0] = "";
                r[1] = "";
                r[2] = "{ทั้งหมด}";
                r[3] = "{ทั้งหมด}";
                dt.Rows.InsertAt(r, 0);
            }
            if (custgroup != "")
            {
                string chk = "," + custgroup + ",";
                foreach (DataRow dr in rs.Rows)
                {
                    if (chk.IndexOf("," + dr["OID"].ToString() + ",") >= 0)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            else
            {
                foreach (DataRow dr in rs.Rows)
                {
                    dt.ImportRow(dr);
                }
            }
            return dt;
        }
        public static DataTable ShoeGroupData()
        {
            DataSet dtShoeGroup = new DataSet();
            dtShoeGroup.ReadXml(GetPath() + "ShoeGroup.xml");
            DataTable dt = dtShoeGroup.Tables[0].Copy();
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "-";
            dr[2] = "ไม่ระบุ";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        public static DataTable ShoeModelData(bool addNew = false)
        {
            DataSet dtShoeModel = new DataSet();
            dtShoeModel.ReadXml(GetPath() + "ShoeModel.xml");
            DataTable dt = dtShoeModel.Tables[0].Copy();
            if (addNew == true)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[2] = "{สร้างใหม่}";
                dt.Rows.InsertAt(dr, 0);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "Model";
            return dv.ToTable();
        }
        public static DataTable ShoeModelData(string model)
        {
            DataTable dt = FilterData("ShoeModel", "Model", model);
            return dt;
        }
        public static DataTable ShoeMoldData()
        {
            DataSet dtShoeMold = new DataSet();
            dtShoeMold.ReadXml(GetPath() + "ShoeMold.xml");
            DataView dv = dtShoeMold.Tables[0].DefaultView;
            dv.Sort = "MoldName";
            return dv.ToTable();
        }
        public static DataTable ShoeSizeData()
        {
            DataSet dtShoeSize = new DataSet();
            dtShoeSize.ReadXml(GetPath() + "ShoeSize.xml");
            return dtShoeSize.Tables[0].Copy();
        }
        public static DataTable ShoeTypeData()
        {
            DataSet dtShoeType = new DataSet();
            dtShoeType.ReadXml(GetPath() + "ShoeType.xml");
            return dtShoeType.Tables[0].Copy();
        }
        public static DataTable ShoeCategoryData()
        {
            DataSet dtShoeCategory = new DataSet();
            dtShoeCategory.ReadXml(GetPath() + "ShoeCategory.xml");
            return dtShoeCategory.Tables[0].Copy();
        }
        public static DataTable ShoeColorData()
        {
            DataSet dtShoeColor = new DataSet();
            dtShoeColor.ReadXml(GetPath() + "Color.xml");
            return dtShoeColor.Tables[0].Copy();
        }
        public static DataTable ShoeData()
        {
            DataSet dtShoe = new DataSet();
            dtShoe.ReadXml(GetPath() + "Goods.xml");
            return dtShoe.Tables[0].Copy();
        }
        public static DataTable ShoeDataByModel(string ModelCode)
        {
            DataTable dt = FilterData("Goods", "ModelName", ModelCode);
            return dt;
        }
        public static DataTable ShoeData(string oid)
        {
            DataTable dt = FilterData("Goods", "OID", oid);
            return dt;
        }
        public static DataTable UserData()
        {
            DataSet dtUser = new DataSet();
            dtUser.ReadXml(GetPath() + "listuser.xml");
            return dtUser.Tables[0].Copy();
        }
        public static DataTable UserData(string shopid)
        {
            DataTable rs = FilterData("listuser", "shopid", shopid);
            return rs;
        }
        public static DataTable UserDataByGroup(string groupid)
        {
            if (groupid == "")
            {
                return UserData();
            }
            else
            {
                DataTable rs = UserData();
                DataTable dt = rs.Clone();
                foreach (DataRow r in rs.Rows)
                {
                    string chk = "," + r["shopgroup"].ToString() + ",";
                    if (r["shopgroup"].ToString().Length == 0)
                    {
                        dt.ImportRow(r);
                    }
                    else
                    {
                        if (chk.IndexOf("," + groupid + ",") >= 0)
                        {
                            dt.ImportRow(r);
                        }
                    }
                }
                return dt;
            }
        }
        public static DataTable ShopData(bool addNew = false, bool changeBlank = false)
        {
            DataSet dtShop = new DataSet();
            dtShop.ReadXml(GetPath() + "Customer.xml");
            DataTable dt = dtShop.Tables[0].Copy();
            if (addNew == true)
            {
                DataRow dr = dt.NewRow();

                dr[0] = "";
                if (changeBlank == true)
                {
                    dr[1] = "";
                    dr[2] = "{ไม่ระบุ}";
                }
                else
                {
                    dr[1] = "(New)";
                    dr[2] = "{สร้างใหม่}";
                }
                dt.Rows.InsertAt(dr, 0);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "custname";
            return dv.ToTable();
        }
        public static DataTable ShopData(string groupid, bool foradd = false)
        {
            if (groupid != "")
            {
                DataTable dt = ShopData(foradd);
                DataTable rs = dt.Clone();
                if (groupid.IndexOf("','") < 0)
                {
                    groupid = groupid.Replace(",", "','");
                }
                if (dt.Rows[0][0].ToString() == "")
                {
                    if (foradd == true)
                    {
                        rs.ImportRow(dt.Rows[0]);
                        DataRow dr = rs.Rows[0];
                        dr[0] = "-";
                        dr[1] = "{All}";
                        dr[2] = "(ไม่ระบุ)";
                    }
                }
                foreach (DataRow dr in dt.Select("GroupID IN('" + groupid + "')"))
                {
                    rs.ImportRow(dr);
                }
                rs.DefaultView.Sort = "custname";
                rs = rs.DefaultView.ToTable();
                return rs;
            }
            else
            {
                return ShopData(foradd);
            }
        }
        public static DataTable GetMonth()
        {
            List<MonthLOV> Months = new List<MonthLOV>();
            Months.Add(new MonthLOV() { MonthID = "", MonthNameEN = "(N/A)", MonthNameTH = "{ไม่ระบุ}" });
            Months.Add(new MonthLOV() { MonthID = "01", MonthNameEN = "January", MonthNameTH = "มกราคม" });
            Months.Add(new MonthLOV() { MonthID = "02", MonthNameEN = "Febuary", MonthNameTH = "กุมภาพันธ์" });
            Months.Add(new MonthLOV() { MonthID = "03", MonthNameEN = "March", MonthNameTH = "มีนาคม" });
            Months.Add(new MonthLOV() { MonthID = "04", MonthNameEN = "April", MonthNameTH = "เมษายน" });
            Months.Add(new MonthLOV() { MonthID = "05", MonthNameEN = "May", MonthNameTH = "พฤษภาคม" });
            Months.Add(new MonthLOV() { MonthID = "06", MonthNameEN = "June", MonthNameTH = "มิถุนายน" });
            Months.Add(new MonthLOV() { MonthID = "07", MonthNameEN = "July", MonthNameTH = "กรกฏาคม" });
            Months.Add(new MonthLOV() { MonthID = "08", MonthNameEN = "August", MonthNameTH = "สิงหาคม" });
            Months.Add(new MonthLOV() { MonthID = "09", MonthNameEN = "September", MonthNameTH = "กันยายน" });
            Months.Add(new MonthLOV() { MonthID = "10", MonthNameEN = "October", MonthNameTH = "ตุลาคม" });
            Months.Add(new MonthLOV() { MonthID = "11", MonthNameEN = "November", MonthNameTH = "พฤษจิกายน" });
            Months.Add(new MonthLOV() { MonthID = "12", MonthNameEN = "December", MonthNameTH = "ธันวาคม" });
            return Months.ToDataTable();
        }
        public static DataTable GetCalGPType()
        {
            return EnumToDataTable(typeof(CalGPType));
        }
        private static List<RoleStruct> GetRole()
        {
            List<RoleStruct> RoleList = new List<RoleStruct>();
            RoleList.Add(new RoleStruct() { roleID = 0, roleName = "Manager" });
            RoleList.Add(new RoleStruct() { roleID = 1, roleName = "PC Staff" });
            RoleList.Add(new RoleStruct() { roleID = 2, roleName = "PC Supervisor" });
            RoleList.Add(new RoleStruct() { roleID = 3, roleName = "Aerosoft Staff" });
            return RoleList;
        }
        public static List<Promotion> GetPromotionByDate(string salesDate, string shopid)
        {
            List<Promotion> lst = new List<Promotion>();
            if (CheckDataXML("gpx.xml") == true)
            {
                string GroupID = GetValueXML("customer", "OID='" + shopid + "'", "GroupID");
                string filterstr = "GroupID='" + GroupID + "' And Active='Y'";
                filterstr += " And EffectiveDateFrom<='" + salesDate + "' ";
                filterstr += " And EffectiveDateTo>='" + salesDate + "' ";
                DataRow[] gpList = GetDataXML("gpx", filterstr);
                foreach (DataRow row in gpList.OrderByDescending(e => e["DiscountRate"]))
                {
                    Promotion data = new Promotion();
                    data.SalesType = row["SalesType"].ToString();
                    data.GPX = Convert.ToDouble(row["GPx"].ToString()) * 100;
                    data.ShareDiscount = Convert.ToDouble(row["ShareDiscount"].ToString()) * 100;
                    data.DiscountRate = Convert.ToDouble(row["DiscountRate"].ToString()) * 100;
                    data.CalculateType = Convert.ToInt32(row["CalculateType"].ToString());
                    lst.Add(data);
                }
            }
            return lst;
        }
        #endregion
        #region master files service function
        public static Promotion GetPromotionByDate(string salesDate, string salesType, string shopid)
        {
            var data = GetPromotionByDate(salesDate, shopid).Where(e => e.SalesType.Equals(salesType)).FirstOrDefault();
            return data;
        }
        public static Promotion GetPromotionByDate(string salesDate, string salesType, string shopid, double discountRate)
        {
            var data = GetPromotionByDate(salesDate, shopid).Where(e => e.SalesType.Equals(salesType)
                        ).Where(e => e.DiscountRate <= (discountRate * 100)).FirstOrDefault();
            if (data != null)
            {
                data.DiscountRate = discountRate * 100;
            }
            return data;
        }
        public static DataRow ShoeDataByCode(string code)
        {
            try
            {
                DataRow row = FilterData("Goods", "GoodsCode", code).Rows[0];
                return row;
            }
            catch
            {
                return ShoeData().NewRow();
            }

        }
        public static string GetprodType(string codein)
        {
            DataRow[] dr = ShoeTypeData().Select("STid='" + codein + "'");
            string value = codein;
            foreach (DataRow r in dr)
            {
                value = r["STName"].ToString();
            }
            return value;
        }
        public static string GetprodGroup(string codein)
        {
            DataRow[] dr = ShoeGroupData().Select("GroupID='" + codein + "'");
            string value = codein;
            foreach (DataRow r in dr)
            {
                value = r["GroupName"].ToString();
            }
            return value;
        }
        public static string GetSalesType(string codein)
        {
            DataRow[] dr = SalesTypeData().Select("OID='" + codein + "'");
            string value = codein;
            foreach (DataRow r in dr)
            {
                value = r["description"].ToString();
            }
            return value;
        }
        public static string GetColorcodeByName(string namein)
        {
            DataRow[] dr = ShoeColorData().Select("ColTh='" + namein + "'");
            string value = "";
            foreach (DataRow r in dr)
            {
                value = r["ColNameInit"].ToString();
            }
            return value;
        }
        public static string GetMoldCode(string modelcode)
        {
            string str = "";
            string numberstr = "0123456789";
            int count = 0;
            for (int i = 0; i < modelcode.Length; i++)
            {
                if (numberstr.IndexOf(modelcode.Substring(i, 1)) >= 0)
                {
                    count++;
                    if (count == 3)
                    {
                        break;
                    }
                    else
                    {
                        str = str + modelcode.Substring(i, 1);
                    }
                }
                else
                {
                    str = str + modelcode.Substring(i, 1);
                }
            }
            return str;
        }
        public static string GetGoodsCode(string model, string color, string size, bool checkcomplete = false)
        {
            try
            {
                string str = model.Trim() + color.Trim() + (Convert.ToDouble(size.Trim()) * 10).ToString();
                if (checkcomplete == true)
                {
                    if (model.Trim() == "" || color.Trim() == "")
                    {
                        str = "";
                    }
                }
                return str;
            }
            catch
            {
                return "";
            }
        }
        public static string GetGoodsName(string model, string color, string size, bool checkcomplete = false)
        {
            string str = model.Trim() + " " + color.Trim() + " " + size.Trim();
            if (checkcomplete == true)
            {
                if (model.Trim() == "" || color.Trim() == "" || size.Trim() == "")
                {
                    str = "";
                }
            }
            return str;
        }
        public static string GetStockFileName(string datein, string userid)
        {
            return datein.Substring(0, 7).Replace("-", "") + "st" + userid;
        }
        public static string GetSalesFileName(string oid, string shopid, string ondate, string user)
        {
            string str = "";
            if (oid != "")
            {
                str = GetSalesDataFileName(oid);
            }
            else
            {
                str = GetSalesDataFileName(shopid, ondate, user);
            }
            string[] arr = str.Split('_');
            int i = 1;
            str = "";
            foreach (string txt in arr)
            {
                if (i <= 3)
                {
                    if (str != "") str += "_";
                    if (i == 2)
                    {
                        str += txt.Substring(0, 6);
                    }
                    else
                    {
                        str += txt;
                    }
                }
                i = i + 1;
            }
            return str;
        }
        public static string GetSalesDataFileName(string oid)
        {
            string str = "";
            if (oid != "")
            {
                string[] arr = oid.Split('_');
                int i = 1;
                foreach (string txt in arr)
                {
                    if (i <= 3)
                    {
                        if (str != "") str += "_";
                        str += txt;
                    }
                    i = i + 1;
                }
            }
            return str;
        }
        public static string GetSalesDataFileName(string shopid, string ondate, string user)
        {
            string str = "";
            str = shopid + "_" + ondate.ToString().Replace("-", "") + "_" + user;
            return str;
        }
        public static string GetSalesOID(string prefix, string model, string color, string size)
        {
            return prefix + "_" + model + "" + color + "" + (System.Convert.ToInt16("0" + size) * 10).ToString();
        }
        public static double GetGPCal(int calType, double discountRate, double shareRate, double gp)
        {
            Promotion promo = new Promotion();
            promo.DiscountRate = discountRate;
            promo.CalculateType = calType;
            promo.ShareDiscount = shareRate;
            promo.GPX = gp;
            return promo.GPRate();
        }
        public static List<MonthLOV> GetQuarterMonth(string period)
        {
            List<MonthLOV> lst = new List<MonthLOV>();
            int i = 0;
            switch (period)
            {
                case "Q1":
                    i = 1;
                    break;
                case "Q2":
                    i = 4;
                    break;
                case "Q3":
                    i = 7;
                    break;
                case "Q4":
                    i = 10;
                    break;
            }
            if (i > 0)
            {
                for (int j = 0; j <= 2; j++)
                {
                    MonthLOV m = new MonthLOV((i + j).ToString("00"));
                    lst.Add(m);
                }
            }
            return lst;
        }
        public static bool AddNewMold(string moldcode, ref string id)
        {
            bool success = false;
            try
            {
                string oid = QueryData(ShoeMoldData(), "MoldName='" + moldcode + "'")["MoldId"].ToString();
                if (oid == "")
                {
                    oid = GetNewOID(ShoeMoldData(), "ShoeMold", "MoldId");
                    if (oid != "")
                    {
                        DataTable dt = ShoeMoldData();
                        DataRow dr = dt.NewRow();
                        DataRow[] rows = dt.Select("MoldId='" + oid + "'");
                        bool bnew = true;
                        foreach (DataRow row in rows)
                        {
                            dr = row;
                            bnew = false;
                        }
                        dr["MoldId"] = oid;
                        if (bnew == true) dr["MoldCode"] = GetNewOID(dt, "ShoeMold", "MoldCode");
                        dr["MoldName"] = moldcode;
                        if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                        dt.WriteXml(GetPath() + "ShoeMold.xml");
                    }
                }
                id = oid;
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }
        #endregion
        #region transaction data and report function
        public static DataTable NewReportDaily(DateTime dfrom, DateTime dto)
        {
            DataTable dt = new DataTable();
            int totalDays = dto.Subtract(dfrom).Days + 1;
            dt.TableName = "Table";
            dt.Columns.Add("Group");
            for (int i = 0; i < totalDays; i++)
            {
                string dfield = dfrom.AddDays(i).ToString("yyyyMMdd");
                dt.Columns.Add(dfield);
            }
            dt.Columns.Add("Total");
            dt.Columns.Add("GrandTotal");
            return dt;
        }
        public static DataTable NewReportByPeriod(string yy, ReportPeriod period)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            dt.Columns.Add("Group");
            dt.Columns.Add("Type");
            if (period == ReportPeriod.Quarter)
            {
                for (int i = 1; i <= 4; i++)
                {
                    string str = "Q" + i.ToString();
                    dt.Columns.Add("QTY-" + str);
                    dt.Columns.Add("SALESOUT-" + str);
                    dt.Columns.Add("SALESIN-" + str);
                }
                dt.Columns.Add("QTY");
                dt.Columns.Add("SALESOUT");
                dt.Columns.Add("SALESIN");
            }
            if (period == ReportPeriod.Monthly)
            {
                DataTable months = GetMonth();
                foreach (DataRow m in months.Select("MonthID<>''"))
                {
                    dt.Columns.Add(m["MonthID"].ToString() + "/" + yy);
                }
                dt.Columns.Add("Total");
            }
            return dt;
        }
        public static DataTable NewProductOnhandData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Clear();
                dt.Columns.Add(new DataColumn("ModelCode", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Color", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("SizeNo", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("ProdCatName", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("GoodsCode", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("ProdQty", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("SalesPrice", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("ProdAmount", System.Type.GetType("System.Double")));
            }
            return dt;
        }
        public static DataTable NewStockData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Clear();
                dt.Columns.Add(new DataColumn("OID", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("ModelCode", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Color", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("SizeNo", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("StockType", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("TransactionState", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("TransactionType", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("StockDate", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("ProdCatName", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("GoodsCode", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("ProdQty", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("StockQty", System.Type.GetType("System.Double"), "[ProdQty]*[StockType]"));
                dt.Columns.Add(new DataColumn("TagPrice", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("GPx", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("DiscountRate", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("ShareDiscount", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("Cal", System.Type.GetType("System.Double"), "1-([GPx]+(([DiscountRate]/100)*[ShareDiscount]))"));
                dt.Columns.Add(new DataColumn("SalesIn", System.Type.GetType("System.Double"), "([ProdQty]*[TagPrice])*[Cal]"));
                dt.Columns.Add(new DataColumn("SalesOut", System.Type.GetType("System.Double")));
                dt.Columns.Add(new DataColumn("UPriceIn", System.Type.GetType("System.Double"), "[SalesIn]/[ProdQty]"));
                dt.Columns.Add(new DataColumn("UPriceOut", System.Type.GetType("System.Double"), "[SalesOut]/[ProdQty]"));
                dt.Columns.Add(new DataColumn("StockIn", System.Type.GetType("System.Double"), "[UPriceIn]*[StockQty]"));
                dt.Columns.Add(new DataColumn("StockOut", System.Type.GetType("System.Double"), "[UPriceOut]*[StockQty]"));
                dt.Columns.Add(new DataColumn("TransactionDate", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("RefNo", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("TransactionBy", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("ApproveCode", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("ConfirmCode", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("AcceptCode", System.Type.GetType("System.String")));
            }
            return dt;
        }
        public static DataTable NewSalesData(DataSet ds)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            if (ds.Tables.Count == 0)
            {
                ds.Tables.Add(dt);
            }
            dt = ds.Tables[0];
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Clear();
                dt.Columns.Add("OID");
                dt.Columns.Add("salesDate");
                dt.Columns.Add("salesType");
                dt.Columns.Add("discountRate");
                dt.Columns.Add("prodID");
                dt.Columns.Add("prodName");
                dt.Columns.Add("ModelCode");
                dt.Columns.Add("ColorCode");
                dt.Columns.Add("ColorName");
                dt.Columns.Add("sizeNo");
                dt.Columns.Add("salesQty");
                dt.Columns.Add("TagPrice");
                dt.Columns.Add("salesPrice");
                dt.Columns.Add("shopName");
                dt.Columns.Add("entryBy");
                dt.Columns.Add("remark");
                dt.Columns.Add("prodType");
                dt.Columns.Add("prodCat");
                dt.Columns.Add("prodGroup");
                dt.Columns.Add("lastUpdate");
                dt.Columns.Add("ShareDiscount");
                dt.Columns.Add("GPx");
                dt.Columns.Add("note");
                dt.Columns.Add("postFlag");
            }
            return dt;
        }
        public static DataTable NewReportSales(DataSet ds)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            if (ds.Tables.Count == 0)
            {
                ds.Tables.Add(dt);
            }
            dt.Columns.Clear();
            dt.Columns.Add("salesDate");
            dt.Columns.Add("Model");
            dt.Columns.Add("Color");
            dt.Columns.Add("size");
            dt.Columns.Add("Qty");
            dt.Columns.Add("TagPrice");
            dt.Columns.Add("discountRate");
            dt.Columns.Add("SalesOut");
            dt.Columns.Add("SalesIn");
            dt.Columns.Add("shopName");
            dt.Columns.Add("salesType");
            dt.Columns.Add("prodGroup");
            dt.Columns.Add("prodType");
            dt.Columns.Add("entryBy");
            dt.Columns.Add("remark");
            dt.Columns.Add("note");
            dt.Columns.Add("RowID");
            dt.Columns.Add("CalGPX");
            return dt;
        }
        public static DataTable NewProductRequest(bool forapprove = false, bool hideCode = false)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Clear();
                dt.Columns.Add(new DataColumn("ModelCode", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Color", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("SizeNo", System.Type.GetType("System.String")));
                if (forapprove == true)
                {
                    dt.Columns.Add(new DataColumn("ProdQty", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("StockQty", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("TagPrice", System.Type.GetType("System.Double")));
                    if (hideCode == false)
                    {
                        dt.Columns.Add(new DataColumn("StockDate", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("AcceptCode", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("ApproveCode", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("ConfirmCode", System.Type.GetType("System.String")));
                    }
                }
                else
                {
                    dt.Columns.Add(new DataColumn("ProdQty", System.Type.GetType("System.Int32")));
                }
                dt.Columns.Add(new DataColumn("ProdCatName", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("RefNo", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("OID", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("TransactionType", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("TransactionDate", System.Type.GetType("System.String")));
            }
            return dt;
        }
        public static DataTable NewOnePriceRequest(bool forapprove = false, bool hideCode = false)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Clear();
                dt.Columns.Add(new DataColumn("ProdCatName", System.Type.GetType("System.String")));
                if (forapprove == true)
                {
                    dt.Columns.Add(new DataColumn("ModelCode", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("Color", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("SizeNo", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("ProdQty", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("StockQty", System.Type.GetType("System.Int32")));
                    dt.Columns.Add(new DataColumn("TagPrice", System.Type.GetType("System.Double")));
                    if (hideCode == false)
                    {
                        dt.Columns.Add(new DataColumn("StockDate", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("AcceptCode", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("ApproveCode", System.Type.GetType("System.String")));
                        dt.Columns.Add(new DataColumn("ConfirmCode", System.Type.GetType("System.String")));
                    }
                }
                else
                {
                    dt.Columns.Add(new DataColumn("ProdQty", System.Type.GetType("System.Int32")));
                }
                dt.Columns.Add(new DataColumn("RefNo", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("OID", System.Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("TransactionType", System.Type.GetType("System.String")));
            }
            return dt;
        }
        public static DataTable GetSalesData(string fname)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                ds.ReadXml(fname);
                dt = ds.Tables[0];
            }
            catch
            {
                dt = NewSalesData(ds);
            }
            return dt;
        }
        public static DataTable GetStockData(string fname)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                ds.ReadXml(fname);
                dt = ds.Tables[0];
            }
            catch
            {
                dt = NewStockData();
            }
            return dt;
        }
        public static DataTable GetStockTransaction(string shopid, string yymm, string userid)
        {

            string tablename = yymm + "st" + shopid + ".xml";
            DataTable dt = GetStockData(GetPath() + tablename);
            DataTable files = XMLTableData(shopid + "_" + yymm + "*_*");
            int i = 0;
            double Gpx = 0;
            double sharedis = 0;
            DataTable shop = QueryData("Customer", "OID='" + shopid + "'");
            if (shop.Rows.Count > 0)
            {
                Gpx = Convert.ToDouble(shop.Rows[0]["GPx"].ToString());
                sharedis = Convert.ToDouble(shop.Rows[0]["ShareDiscount"].ToString());
            }
            foreach (DataRow file in files.Rows)
            {
                DataTable rs = GetSalesData(GetPath() + file["filename"].ToString());
                foreach (DataRow r in rs.Rows)
                {
                    i++;
                    string refno = "STO" + yymm + Convert.ToInt32(shopid).ToString("000") + i.ToString("0000");
                    DataRow dr = QueryData(dt, "RefNo='" + refno + "'");
                    if (dr["RefNo"].ToString() == "")
                    {
                        dr["RefNo"] = refno;
                        dr["OID"] = GetNewOID(dt, tablename, "OID");
                        dr["AcceptCode"] = userid;
                    }
                    dr["GPx"] = Gpx;
                    dr["ShareDiscount"] = sharedis;
                    dr = ProcessRowStockBySales(r, dr);
                    if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                }
            }
            dt.WriteXml(GetPath() + tablename);
            return dt;
        }
        public static DataTable FilterStockTransaction(string fname, string filter, string value)
        {
            DataTable dt = NewStockData();
            dt = FilterData(fname, filter, value);
            return dt;
        }
        public static string ProcessUpdateGPX()
        {
            string msg = "";
            foreach (XMLFileList file in GetXMLTableList("*_*_*"))
            {
                try
                {
                    string fname = file.filename;
                    DataTable sales = GetSalesData(GetPath() + fname);
                    if (sales.Columns.Count > 0 && sales.Rows.Count > 0)
                    {
                        string shopid = fname.Substring(0, fname.IndexOf('_'));
                        DataRow shop = QueryData("Customer", "OID='" + shopid + "'").Rows[0];
                        bool isupdate = false;
                        if (sales.Columns.IndexOf("sharediscount") < 0)
                        {
                            DataColumn col = new DataColumn("sharediscount", System.Type.GetType("System.Double"));
                            col.DefaultValue = Convert.ToDouble(0 + shop["sharediscount"].ToString());
                            sales.Columns.Add(col);
                            msg += "<br/> Add sharediscount at " + fname;
                            isupdate = true;
                        }
                        if (sales.Columns.IndexOf("gpx") < 0)
                        {
                            DataColumn col = new DataColumn("gpx", System.Type.GetType("System.Double"));
                            col.DefaultValue = Convert.ToDouble(0 + shop["gpx"].ToString());
                            sales.Columns.Add(col);
                            msg += "<br/> Add gpx at " + fname;
                            isupdate = true;
                        }
                        if (isupdate == true)
                        {
                            sales.WriteXml(GetPath() + fname);
                            msg += "<br/> " + fname + " updated!";
                        }
                        else
                        {
                            msg += "<br/>" + fname + " processed!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg += "<br/> ERROR " + file.filename + ">" + ex.Message;
                }
            }
            return msg;
        }
        public static string CheckErrorData()
        {
            string msg = "";
            foreach (XMLFileList file in GetXMLTableList("*_*_*"))
            {
                try
                {
                    string fname = file.filename;
                    DataTable sales = GetSalesData(GetPath() + fname);
                    bool isupdate = false;
                    foreach (DataRow sale in sales.Rows)
                    {
                        string[] str = sale["OID"].ToString().Split('_');
                        string sdate = str[1].ToString();
                        if (sale["salesDate"].ToString().Replace("-", "") != sdate)
                        {
                            if (sdate.Length == 8)
                            {
                                msg += "<br/> change salesDate from " + sale["salesDate"] + " TO " + sdate.Substring(0, 4) + "-" + sdate.Substring(4, 2) + "-" + sdate.Substring(6, 2) + " OID =" + sale["OID"].ToString();
                                //sale["salesDate"] = sdate.Substring(0, 4) + "-" + sdate.Substring(4, 2) + "-" + sdate.Substring(6, 2);                                
                                isupdate = true;
                            }
                            else
                            {
                                if (str.Length == 4)
                                {
                                    msg += "<br/> change OID from " + sale["OID"] + " TO " + str[0] + "_" + sale["salesDate"].ToString().Replace("-", "") + "_" + str[2] + "_" + str[3];
                                    //sale["OID"] = str[0] + "_" + sale["salesDate"].ToString().Replace("-", "") + "_" + str[2] + "_" + str[3];
                                    isupdate = true;
                                }

                            }
                        }
                    }
                    if (isupdate == true)
                    {
                        //sales.WriteXml(GetPath() + fname);
                        //msg += "<br/> update " + fname;
                    }
                }
                catch (Exception ex)
                {
                    msg += "<br/> ERROR " + file.filename + ">" + ex.Message;
                }
            }
            return msg;
        }
        public static void CalculateSalesGP(DataRow r, DataRow row, string fldSalesIn, string fldGpx, bool recal = false)
        {
            double discrate = 0;
            double gpx = 1;
            double salesin = 0;
            double salesamt = 0;
            string shopid = row["OID"].ToString().Substring(0, row["OID"].ToString().IndexOf("_"));
            string salesType = row["salesType"].ToString();
            try
            {

                salesamt = (Convert.ToDouble(row["salesQty"]) * Convert.ToDouble(row["TagPrice"]));
                //if one-price 
                if (row["salesType"].ToString().Equals("2")) //one price
                {
                    salesamt = (Convert.ToDouble(row["salesQty"]) * Convert.ToDouble(row["salesPrice"]));
                }
                else
                {
                    if (row["salesType"].ToString().Equals("3")) //discount
                    {
                        try { discrate = Convert.ToDouble(row["discountRate"]) / 100; } catch { }
                        salesamt = (Convert.ToDouble(row["salesQty"]) * Convert.ToDouble(row["salesPrice"]));
                    }
                }
                //if discount by price
                if (row["note"].ToString().IndexOf("ส่วนลดเงินสด") >= 0)
                {
                    discrate = 0;
                    salesamt = (Convert.ToDouble(row["salesQty"]) * Convert.ToDouble(row["salesPrice"]));
                    salesType = "1";
                }
                gpx = Convert.ToDouble(row["GPx"]);
                salesin = salesamt * (gpx / 100);
            }
            catch
            {

            }
            if (recal == true)
            {
                Promotion p = GetPromotionByDate(row["salesDate"].ToString(), salesType, shopid, discrate);
                if (p != null)
                {
                    gpx = p.GPRate() * 100;
                    try { salesin = CalculateSalesIn(salesamt, p); } catch { }
                }
                try
                {
                    r["GPx"] = gpx;
                    r["ShareDiscount"] = p.ShareDiscount;
                }
                catch { }
            }
            r[fldSalesIn] = salesin.ToString("#,###,##0.00");
            r[fldGpx] = gpx.ToString("0.00");
        }
        public static double CalculateSalesIn(double salesout, Promotion data)
        {
            if (data != null)
            {
                double salesin = salesout * data.GPRate();
                return salesin;
            }
            else
            {
                return salesout;
            }
        }
        public static DataRow ProcessRowStockBySales(DataRow s, DataRow r)
        {
            r["TransactionDate"] = s["salesDate"];
            r["TransactionBy"] = s["entryBy"];
            r["StockType"] = -1;
            r["TransactionState"] = "SAL";
            r["TransactionType"] = "ขายสินค้า";
            r["StockDate"] = s["salesDate"];
            r["ApproveCode"] = s["entryBy"];
            r["ConfirmCode"] = "System";
            r["ModelCode"] = s["ModelCode"];
            r["Color"] = s["colorName"];
            r["SizeNo"] = s["SizeNo"];
            r["GoodsCode"] = s["ModelCode"].ToString().Trim() + s["Colorcode"].ToString().Trim() + (Convert.ToInt32(s["SizeNo"].ToString()) * 10).ToString();
            r["ProdCatName"] = GetprodType(s["prodType"].ToString());
            r["ProdQty"] = Convert.ToDouble(0 + s["salesQty"].ToString());
            r["TagPrice"] = Convert.ToDouble(0 + s["TagPrice"].ToString());
            r["DiscountRate"] = Convert.ToDouble(0 + s["discountRate"].ToString());
            r["StockQty"] = Convert.ToDouble(r["ProdQty"]) * Convert.ToDouble(r["StockType"]);
            r["SalesOut"] = Convert.ToDouble(s["salesQty"]) * Convert.ToDouble(s["salesPrice"]);
            CalculateSalesGP(r, s, "SalesIn", "Cal");
            r["UPriceOut"] = (Convert.ToDouble(r["SalesOut"]) / Convert.ToDouble(r["ProdQty"])).ToString("#0.00");
            r["UPriceIn"] = (Convert.ToDouble(r["SalesIn"]) / Convert.ToDouble(r["ProdQty"])).ToString("#0.00");
            r["StockIn"] = (Convert.ToDouble(r["UPriceIn"]) * Convert.ToDouble(r["StockQty"])).ToString("#0.00");
            r["StockOut"] = (Convert.ToDouble(r["UPriceOut"]) * Convert.ToDouble(r["StockQty"])).ToString("#0.00");
            return r;
        }
        public static DataRow ProcessRowReportSales(DataRow s, DataRow r, bool calGP = false)
        {
            try
            {
                r["salesDate"] = s["salesDate"];
                r["Model"] = s["ModelCode"];
                r["Color"] = s["ColorName"].ToString();
                r["size"] = s["SizeNo"];
                r["shopName"] = s["shopName"];
                r["salesType"] = GetSalesType(s["salesType"].ToString());
                r["prodGroup"] = GetprodGroup(s["prodGroup"].ToString());
                r["prodType"] = GetprodType(s["prodType"].ToString());
                r["entryBy"] =  s["entryBy"];
                r["RowID"] = s["OID"];
                r["Qty"] = s["salesQty"];
                r["TagPrice"] = s["TagPrice"];
                r["discountRate"] = s["discountRate"];
                r["SalesOut"] = 0;
                r["SalesIn"] = 0;
                try { r["SalesOut"] = (Convert.ToDouble(s["salesQty"]) * Convert.ToDouble(s["salesPrice"])).ToString("#,###,##0.00"); } catch { }
                CalculateSalesGP(r, s, "SalesIn", "CalGPX", calGP);
                try { r["remark"] = s["remark"]; } catch { }
                try { r["note"] = s["note"]; } catch { }
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }
            return r;
        }
        public static string GetNewReportID(DataTable dt, string userid, string datetimenow)
        {
            try
            {
                string fname = "rpt" + userid + datetimenow;
                dt.WriteXml(GetPath() + fname + ".xml");
                return fname;
            }
            catch
            {
                return "";
            }
        }
        public static void ReadSalesReportData(DataTable dt, string filename, string cliteria, ref double sumqty, ref double sumsale1, ref double sumsale2, ref double sumsale, bool sumonly)
        {
            if (System.IO.File.Exists(GetPath() + filename) == true)
            {
                DataSet ds = new DataSet();
                try
                {
                    DataTable rs = GetSalesData(GetPath() + filename).Copy();
                    rs.DefaultView.Sort = "salesDate";
                    rs = rs.DefaultView.ToTable();
                    DataRow[] dr = rs.Select(cliteria);
                    if (dr.Length > 0)
                    {
                        foreach (DataRow r in dr)
                        {
                            DataRow row = ProcessRowReportSales(r, dt.NewRow());
                            if (sumonly == false)
                            {
                                dt.Rows.Add(row);
                            }
                            try { sumqty += Convert.ToDouble(r["salesQty"]); } catch { }
                            try { sumsale1 += Convert.ToDouble(row["SalesIn"]); } catch { }
                            try { sumsale2 += Convert.ToDouble(row["SalesOut"]); } catch { }
                            try { sumsale += (Convert.ToDouble(r["salesQty"]) * Convert.ToDouble(r["TagPrice"])); } catch { }
                        }
                    }
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
            }
        }
        public static void ReadReportDataByDate(DataTable tbTemp, DataTable data, DataRow row, string idshop, string period, string cliteria, string wherec, string fieldsum, ref double gtotal)
        {
            var files = GetXMLTableList(idshop + "_" + period + "_*");
            foreach (XMLFileList file in files)
            {
                DataTable dt = GetSalesData(GetPath() + file.filename);
                string wc = cliteria;
                double total = Convert.ToDouble("0" + row["Total"].ToString());
                DataRow[] rows = dt.Select(wc);
                foreach (DataRow dr in dt.Select(wherec))
                {
                    DataRow tmp = tbTemp.NewRow();
                    tmp = ProcessRowReportSales(dr, tmp);
                    if (rows.Contains(dr))
                    {
                        string colname = tmp["salesDate"].ToString().Replace("-", "");
                        if (data.Columns.IndexOf(colname) >= 0)
                        {
                            string formatstr = "";
                            if (fieldsum == "SalesIn")
                            {
                                formatstr = "#,###,##0.00";
                            }
                            try { row[colname] = (Convert.ToDouble("0" + row[colname].ToString()) + Convert.ToDouble(tmp[fieldsum])).ToString(formatstr); } catch { }
                            try { total += Convert.ToDouble(tmp[fieldsum]); } catch { }
                        }
                    }
                    try { gtotal += Convert.ToDouble(tmp[fieldsum]); } catch { }
                }
                if (fieldsum == "Qty")
                {
                    row["TOTAL"] = total;
                    row["GRANDTOTAL"] = gtotal;
                }
                else
                {
                    row["TOTAL"] = total.ToString("#,###,##0.00");
                    row["GRANDTOTAL"] = gtotal.ToString("#,###,##0.00");
                }
            }
        }
        public static void LoadReportDataByDate(DataTable data, string fieldsum, string custgroup, string shopid, string cliteria, bool groupByHQ = false, string FirstDay = "", string LastDay = "")
        {
            //default where for summary accumulate
            string wherec = "SalesDate>='" + FirstDay + "' and SalesDate<='" + LastDay + "'";
            DateTime dateBegin = Convert.ToDateTime(FirstDay);
            DateTime dateEnd = Convert.ToDateTime(LastDay);
            MonthLOV m = new MonthLOV();
            List<string> periods = m.GetPeriod(dateBegin, dateEnd);
            DataTable rs = new DataTable();
            DataTable tbTemp = NewReportSales(new DataSet());
            DataRow row;
            double gtotal = 0;
            if (groupByHQ == true)
            {
                //group by customer group
                DataTable tb = new DataTable();
                if (custgroup.Equals("") == false)
                {
                    tb = QueryData("CustomerGroup", "OID IN('" + custgroup.Replace(",", "','") + "')");
                }
                else
                {
                    tb = CustomerGroupData();
                }
                foreach (DataRow grp in tb.Rows)
                {
                    row = data.NewRow();
                    row["Group"] = grp["CustGroupNameTH"].ToString();
                    gtotal = 0;
                    rs = ShopData(grp["OID"].ToString());
                    foreach (DataRow shop in rs.Rows)
                    {
                        if (shopid == "-" || shop["OID"].ToString() == shopid)
                        {
                            foreach (string period in periods)
                            {
                                ReadReportDataByDate(tbTemp, data, row, shop["OID"].ToString(), period, cliteria, wherec, fieldsum, ref gtotal);
                            }
                        }
                    }
                    data.Rows.Add(row);
                }
            }
            else
            {
                //group by POS
                if (custgroup.Equals("") == false)
                {
                    rs = ShopData(custgroup);
                }
                else
                {
                    rs = ShopData();
                }
                //loop each shop by check from combo's selected value 
                foreach (DataRow shop in rs.Rows)
                {
                    row = data.NewRow();
                    row["Group"] = shop["custname"].ToString();
                    gtotal = 0;
                    if (shopid == "-" || shop["OID"].ToString() == shopid)
                    {
                        foreach (string period in periods)
                        {
                            ReadReportDataByDate(tbTemp, data, row, shop["OID"].ToString(), period, cliteria, wherec, fieldsum, ref gtotal);
                        }
                    }
                    data.Rows.Add(row);
                }
            }
            //summary section
            row = data.NewRow();
            row["Group"] = "Grand Total";
            for (int i = 1; i <= data.Columns.Count - 1; i++)
            {
                double sumTotal = 0;
                foreach (DataRow r in data.Rows)
                {
                    try { sumTotal += Convert.ToDouble(r[data.Columns[i].ColumnName].ToString()); } catch { }
                }
                if (fieldsum == "Qty")
                {
                    row[data.Columns[i].ColumnName] = sumTotal;
                }
                else
                {
                    row[data.Columns[i].ColumnName] = sumTotal.ToString("#,###,##0.00");
                }
            }
            data.Rows.Add(row);
        }
        public static void LoadReportDataByPOS(string datefrom, string dateto, DataTable data, string idshop, string shopname, string cliteria, bool sumonly)
        {
            string startYearMonth = datefrom.Substring(0, 7).Replace("-", "");
            string endYearMonth = dateto.Substring(0, 7).Replace("-", "");
            string strYear = "";
            string strMonth = "";
            double totalqty = 0;
            double totalPrice = 0;
            double totalsalesIn = 0;
            double totalsalesOut = 0;
            int startVar = Convert.ToInt32(startYearMonth);
            int endVar = Convert.ToInt32(endYearMonth);
            while (startVar <= endVar)
            {
                foreach (DataRow user in UserData().Rows)
                {
                    string fname = GetXMLFileName(idshop, startVar.ToString(), user["id"].ToString());
                    ReadSalesReportData(data, fname, cliteria, ref totalqty, ref totalsalesIn, ref totalsalesOut, ref totalPrice, sumonly);
                }
                //check for next month
                strYear = startVar.ToString().Substring(0, 4);
                strMonth = startVar.ToString().Substring(4, 2);
                if (strMonth == "12")
                {
                    strMonth = "01";
                    strYear = (Convert.ToInt32(strYear) + 1).ToString();
                }
                else
                {
                    strMonth = (Convert.ToInt32(strMonth) + 1).ToString("00");
                }
                startVar = Convert.ToInt32(strYear.ToString() + "" + strMonth.ToString());
            }
            DataRow sumrow = data.NewRow();
            sumrow["salesDate"] = ClsUtil.GetCurrentTHDate().ToString("yyyy-MM-dd");
            sumrow["Model"] = "รวม " + shopname;
            if (totalqty > 0)
            {
                sumrow["Qty"] = totalqty;
                sumrow["TagPrice"] = totalPrice.ToString("#,###,##0.00");
                sumrow["SalesIn"] = totalsalesIn.ToString("#,###,##0.00");
                sumrow["SalesOut"] = totalsalesOut.ToString("#,###,##0.00");
            }
            data.Rows.Add(sumrow);
        }
        public static void LoadReportDataByUSER(string datefrom, string dateto, DataTable data, string iduser, string shopid, string username, string groupid, string cliteria, bool sumonly, string role)
        {
            string startYearMonth = datefrom.Substring(0, 7).Replace("-", "");
            string endYearMonth = dateto.Substring(0, 7).Replace("-", "");
            string strYear = "";
            string strMonth = "";
            double totalqty = 0;
            double totalPrice = 0;
            double totalsalesIn = 0;
            double totalsalesOut = 0;
            int startVar = Convert.ToInt32(startYearMonth);
            int endVar = Convert.ToInt32(endYearMonth);
            DataTable rs = new DataTable();
            if (shopid == "") shopid = "-";
            rs = ShopData(groupid);
            while (startVar <= endVar)
            {
                foreach (DataRow shop in rs.Rows)
                {
                    if (shopid == "-" || shop["OID"].ToString() == shopid)
                    {
                        string fname = GetXMLFileName(shop["OID"].ToString(), startVar.ToString(), iduser);
                        ReadSalesReportData(data, fname, cliteria, ref totalqty, ref totalsalesIn, ref totalsalesOut, ref totalPrice, sumonly);
                    }
                }
                //check for next month
                strYear = startVar.ToString().Substring(0, 4);
                strMonth = startVar.ToString().Substring(4, 2);
                if (strMonth == "12")
                {
                    strMonth = "01";
                    strYear = (Convert.ToInt32(strYear) + 1).ToString();
                }
                else
                {
                    strMonth = (Convert.ToInt32(strMonth) + 1).ToString("00");
                }
                startVar = Convert.ToInt32(strYear.ToString() + "" + strMonth.ToString());
            }
            DataRow sumrow = data.NewRow();
            sumrow["salesDate"] = "TOTAL";
            sumrow["Model"] = "รวม " + iduser + "-" + username;
            if (totalqty > 0)
            {
                sumrow["Qty"] = totalqty;
                sumrow["TagPrice"] = totalPrice.ToString("#,###,##0.00");
                sumrow["SalesIn"] = totalsalesIn.ToString("#,###,##0.00");
                sumrow["SalesOut"] = totalsalesOut.ToString("#,###,##0.00");
            }
            if (!(role == "1" && totalqty == 0)) data.Rows.Add(sumrow);
        }
        public static DataTable GetReportDataByPOS(string datefrom, string dateto, string custgroup, string shopid, string cliteria, bool sumonly)
        {
            DataSet ds = new DataSet();
            DataTable dt = NewReportSales(ds);
            DataTable rs = new DataTable();
            if (custgroup.Equals("") == false)
            {
                rs = ShopData(custgroup);
            }
            else
            {
                rs = ShopData();
            }
            //loop each shop by check from combo's selected value 
            foreach (DataRow shop in rs.Rows)
            {
                if (shopid == "-" || shop["OID"].ToString() == shopid)
                {
                    try
                    {
                        LoadReportDataByPOS(datefrom, dateto, dt, shop["OID"].ToString(), shop["custname"].ToString(), cliteria, sumonly);
                    }
                    catch (Exception e)
                    {
                        errMessage = e.Message;
                    }
                }
            }
            AddSummaryReportSales(dt, sumonly);
            SetReportSalesCaption(dt);
            return dt;
        }
        public static DataTable GetReportDataByUSER(string datefrom, string dateto, string custgroup, string shopid, string cliteria, bool sumonly, string role)
        {
            DataSet ds = new DataSet();
            DataTable dt = NewReportSales(ds);
            DataTable users = UserData();
            users.DefaultView.Sort = "id";
            users = users.DefaultView.ToTable();
            foreach (DataRow user in users.Rows)
            {
                try
                {
                    LoadReportDataByUSER(datefrom, dateto, dt, user["id"].ToString(), shopid, user["name"].ToString(), custgroup, cliteria, sumonly, role);
                }
                catch (Exception e)
                {
                    errMessage = e.Message;
                }
            }
            AddSummaryReportSales(dt, sumonly);
            SetReportSalesCaption(dt);
            return dt;
        }
        public static DataSet GetDailyReport(string datesend)
        {
            //prepare variables
            string chkCust = "";
            int filecount = 0;
            double totalS = 0;
            double totalV = 0;
            //find data on yesterday
            DateTime fordate = Convert.ToDateTime(datesend).AddDays(-1);
            string datefrom = "";
            string dateto = "";
            //get current month data
            MonthLOV m = new MonthLOV();
            m.GetMonthValue(fordate.ToString("yyyy-MM-dd"));
            datefrom = m.FirstDate;
            dateto = m.LastDate;
            DateTime dateStart = Convert.ToDateTime(datefrom);
            DateTime dateFinish = Convert.ToDateTime(dateto);
            DateTime firstDate = new DateTime(dateStart.Year, dateStart.Month, 1);
            DateTime lastDate = new DateTime(dateFinish.Year, dateFinish.Month, 1).AddMonths(1).AddDays(-1);
            List<string> monthList = m.GetPeriod();
            //create sql for table
            string cliteria = "";
            cliteria += "SalesDate>='" + datefrom + "' AND SalesDate<='" + dateto + "'";
            DataTable dtSales = NewReportDaily(dateStart, dateFinish);
            DataTable dtVolume = dtSales.Clone();
            DataRow rowSales = dtSales.NewRow();
            DataRow rowVolume = dtVolume.NewRow();
            DataSet ds = new DataSet();
            ds.DataSetName = "DailyReport";
            dtSales.TableName = "Sales";
            dtVolume.TableName = "Volume";

            //get list of sales entry by period selected
            List<XMLFileList> files = GetSalesList(monthList, "*_*_*");
            foreach (XMLFileList file in files)
            {
                filecount++;
                //each file sort by same costomer
                string custcode = file.filename.Split('_')[0].ToString();
                if (chkCust != custcode)
                {
                    //if change customer then add previous row
                    if (chkCust != "")
                    {
                        dtSales.Rows.Add(rowSales);
                        dtVolume.Rows.Add(rowVolume);
                        totalS = 0;
                        totalV = 0;
                    }
                    chkCust = custcode;
                    //find customer data
                    string custname = GetValueXML("customer", "oid='" + custcode + "'", "custname");
                    rowSales = dtSales.NewRow();
                    rowSales["Group"] = custname;
                    rowVolume = dtVolume.NewRow();
                    rowVolume["Group"] = custname;
                }

                using (DataTable rs = NewReportSales(new DataSet()))
                {
                    DataRow tmp = rs.NewRow();
                    List<string> list = new List<string>();
                    List<double> gt = new List<double>();
                    DataTable data = NewSalesData(new DataSet());
                    for (int i = 1; i < dtSales.Columns.Count - 2; i++)
                    {
                        double valS = 0;
                        double valV = 0;
                        string fname = custcode + "_" + dtSales.Columns[i].ToString().Substring(0, 6) + "*";
                        string ondate = dtSales.Columns[i].ColumnName.Substring(0, 4) + "-" + dtSales.Columns[i].ColumnName.Substring(4, 2) + "-" + dtSales.Columns[i].ColumnName.Substring(6, 2);
                        string wherec = "SalesDate='" + ondate + "'";
                        //read sa;es data
                        data = GetSalesData(GetPath() + file.filename).Copy();
                        DataRow[] rows = data.Select(wherec);
                        foreach (DataRow dr in rows)
                        {
                            tmp = ProcessRowReportSales(dr, tmp);

                            try { valS += Convert.ToDouble(tmp["SalesIn"]); } catch { }
                            try { totalS += Convert.ToDouble(tmp["SalesIn"]); } catch { }
                            try { valV += Convert.ToDouble(tmp["Qty"]); } catch { }
                            try { totalV += Convert.ToDouble(tmp["Qty"]); } catch { }
                        }
                        if (valV > 0)
                        {
                            rowVolume[dtSales.Columns[i].ColumnName] = Convert.ToInt32("0" + rowVolume[dtSales.Columns[i].ColumnName]) + Convert.ToInt32(valV);
                        }
                        if (valS > 0)
                        {
                            rowSales[dtSales.Columns[i].ColumnName] = (Convert.ToDouble("0" + rowSales[dtSales.Columns[i].ColumnName]) + Convert.ToDouble(valS)).ToString("#,###,##0.00");
                        }

                    }
                    rowVolume["Total"] = Convert.ToInt32(totalV);
                    rowVolume["GrandTotal"] = Convert.ToInt32(totalV);
                    rowSales["Total"] = totalS.ToString("#,###,##0.00");
                    rowSales["GrandTotal"] = totalS.ToString("#,###,##0.00");
                }
                //if last file process then add last row
                if (filecount == files.Count)
                {
                    dtSales.Rows.Add(rowSales);
                    dtVolume.Rows.Add(rowVolume);
                }
            }
            //change caption of data
            FormatDataToReadable(dtSales);
            FormatDataToReadable(dtVolume);
            //sort data
            dtSales.DefaultView.Sort = "Group";
            dtSales = dtSales.DefaultView.ToTable();
            dtVolume.DefaultView.Sort = "Group";
            dtVolume = dtVolume.DefaultView.ToTable();

            //summary section
            rowSales = dtSales.NewRow();
            rowVolume = dtVolume.NewRow();
            rowSales["Group"] = "Grand Total";
            rowVolume["Group"] = "Grand Total";
            for (int i = 1; i <= dtSales.Columns.Count - 1; i++)
            {
                double sumTotalS = 0;
                double sumTotalV = 0;
                foreach (DataRow r in dtSales.Rows)
                {
                    try { sumTotalS += Convert.ToDouble(r[dtSales.Columns[i].ColumnName].ToString()); } catch { }
                }
                foreach (DataRow r in dtVolume.Rows)
                {
                    try { sumTotalV += Convert.ToDouble(r[dtSales.Columns[i].ColumnName].ToString()); } catch { }
                }
                rowVolume[dtVolume.Columns[i].ColumnName] = sumTotalV;
                rowSales[dtSales.Columns[i].ColumnName] = sumTotalS.ToString("#,###,##0.00");
            }
            dtSales.Rows.Add(rowSales);
            dtVolume.Rows.Add(rowVolume);
            //return table set
            ds.Tables.Add(dtSales);
            ds.Tables.Add(dtVolume);
            return ds;
        }
        public static DataTable GetReportDataByDATE(int reportindex, string datefrom, string dateto, string custgroup, string shopid, string cliteria, bool sumonly)
        {
            DateTime dateStart = Convert.ToDateTime(datefrom);
            DateTime dateFinish = Convert.ToDateTime(dateto);
            DateTime firstDate = new DateTime(dateStart.Year, dateStart.Month, 1);
            DateTime lastDate = new DateTime(dateFinish.Year, dateFinish.Month, 1).AddMonths(1).AddDays(-1);
            DataTable dt = NewReportDaily(dateStart, dateFinish);
            string fldname = "";
            if (reportindex == 2) //สรุปยอดขาย
            {
                fldname = "SalesIn";
            }
            if (reportindex == 3) //สรุปจำนวนขาย
            {
                fldname = "Qty";
            }
            LoadReportDataByDate(dt, fldname, custgroup, shopid, cliteria, sumonly, firstDate.ToString("yyyy-MM-dd"), lastDate.ToString("yyyy-MM-dd"));
            FormatDataToReadable(dt);
            return dt;
        }
        public static DataTable GetReportByYearly(string onyear, string custgroup, string shopid, string cliteria, bool sumonly)
        {
            DataTable dt = NewReportByPeriod(onyear, ReportPeriod.Monthly);
            DataTable rs = NewReportSales(new DataSet());
            string startDate = new DateTime(Convert.ToInt32(onyear), 1, 1).ToString("yyyy-MM-dd");
            string endDate = new DateTime(Convert.ToInt32(onyear), 12, 31).ToString("yyyy-MM-dd");
            //set default value
            double totalAmt = 0;
            double valAmt = 0;
            double sumAmt = 0;
            double totalQty = 0;
            double valQty = 0;
            double sumQty = 0;
            double gtAmt = 0;
            double gtQty = 0;
            //prepare grand total rows
            DataRow totAmt = dt.NewRow();
            totAmt["Group"] = "GRAND TOTAL";
            totAmt["Type"] = "SalesIn";
            DataRow totQty = dt.NewRow();
            totQty["Group"] = "GRAND TOTAL";
            totQty["Type"] = "Qty";
            //check cliteria
            if (sumonly == true)
            {
                //sum by cust group
                DataTable custs = ShopGroupData(custgroup);
                foreach (DataRow cust in custs.Rows)
                {
                    //for each customer group
                    sumAmt = 0;
                    sumQty = 0;
                    DataRow rowAmt = dt.NewRow();
                    DataRow rowQty = dt.NewRow();
                    rowAmt["Group"] = cust["custgroupnameth"].ToString();
                    rowAmt["Type"] = "SalesIn";
                    rowQty["Group"] = cust["custgroupnameth"].ToString();
                    rowQty["Type"] = "Qty";
                    //open shops data
                    DataTable shops = ShopData(cust["OID"].ToString());
                    for (int i = 2; i < dt.Columns.Count - 1; i++)
                    {
                        totalAmt = 0;
                        totalQty = 0;
                        //for each shop
                        foreach (DataRow shop in shops.Rows)
                        {
                            if (shopid == shop["OID"].ToString() || shopid == "-")
                            {
                                //format 01/2016
                                var v = dt.Columns[i].ColumnName.Split('/');
                                string monthyear = (v[1] + v[0]).ToString();
                                DataTable files = XMLTableData(shop["OID"].ToString() + "_" + monthyear + "_*");
                                foreach (DataRow File in files.Rows)
                                {
                                    DataTable data = GetSalesData(GetPath() + File["filename"].ToString());
                                    foreach (DataRow dr in data.Select(cliteria))
                                    {
                                        try
                                        {
                                            DataRow tmp = rs.NewRow();
                                            tmp = ProcessRowReportSales(dr, tmp);
                                            valAmt = Convert.ToDouble(tmp["SalesIn"]);
                                            valQty = Convert.ToDouble(tmp["Qty"]);
                                            totalAmt += valAmt;
                                            sumAmt += valAmt;
                                            totalQty += valQty;
                                            sumQty += valQty;
                                        }
                                        finally
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        if (totalAmt > 0) rowAmt[i] = totalAmt.ToString("#,###,##0.00");
                        if (totalQty > 0) rowQty[i] = totalQty;
                    }
                    rowAmt["Total"] = sumAmt.ToString("#,###,##0.00");
                    rowQty["Total"] = sumQty;
                    dt.Rows.Add(rowQty);
                    dt.Rows.Add(rowAmt);
                }
            }
            else
            {
                //show details             
                DataTable shops = ShopData(custgroup);
                foreach (DataRow shop in shops.Rows)
                {
                    if (shopid == shop["OID"].ToString() || shopid == "-")
                    {
                        sumAmt = 0;
                        sumQty = 0;
                        //for each shop
                        DataRow rowAmt = dt.NewRow();
                        DataRow rowQty = dt.NewRow();
                        rowAmt["Group"] = shop["custname"].ToString();
                        rowAmt["Type"] = "SalesIn";
                        rowQty["Group"] = shop["custname"].ToString();
                        rowQty["Type"] = "Qty";

                        for (int i = 2; i < dt.Columns.Count - 1; i++)
                        {
                            totalAmt = 0;
                            totalQty = 0;
                            //format : 01/2016
                            var v = dt.Columns[i].ColumnName.Split('/');
                            string monthyear = (v[1] + v[0]).ToString();
                            //seach data entry
                            DataTable files = XMLTableData(shop["OID"].ToString() + "_" + monthyear + "_*");
                            foreach (DataRow File in files.Rows)
                            {
                                DataTable data = GetSalesData(GetPath() + File["filename"].ToString());
                                foreach (DataRow dr in data.Select(cliteria))
                                {
                                    try
                                    {
                                        DataRow tmp = rs.NewRow();
                                        tmp = ProcessRowReportSales(dr, tmp);
                                        valAmt = Convert.ToDouble(tmp["SalesIn"]);
                                        valQty = Convert.ToDouble(tmp["Qty"]);
                                        totalAmt += valAmt;
                                        sumAmt += valAmt;
                                        totalQty += valQty;
                                        sumQty += valQty;
                                    }
                                    finally
                                    {

                                    }
                                }
                            }
                            if (totalAmt > 0) rowAmt[i] = totalAmt.ToString("#,###,##0.00");
                            if (totalQty > 0) rowQty[i] = totalQty;
                        }
                        rowAmt["Total"] = sumAmt.ToString("#,###,##0.00");
                        rowQty["Total"] = sumQty;

                        dt.Rows.Add(rowQty);
                        dt.Rows.Add(rowAmt);
                    }
                }
            }
            //sum all value from all rows
            for (int i = 2; i < dt.Columns.Count; i++)
            {
                gtAmt = 0;
                gtQty = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        if (dr["Type"].ToString() == "Qty")
                        {
                            try { gtQty += Convert.ToDouble(dr[i]); } catch { }
                        }
                        if (dr["Type"].ToString() == "SalesIn")
                        {
                            try { gtAmt += Convert.ToDouble(dr[i]); } catch { }
                        }
                    }
                    finally
                    {

                    }
                }
                totAmt[i] = gtAmt.ToString("#,###,##0.00");
                totQty[i] = gtQty;
            }
            dt.Rows.Add(totQty);
            dt.Rows.Add(totAmt);

            dt.DefaultView.Sort = "Type";
            return dt.DefaultView.ToTable();
        }
        public static DataTable GetReportByQuarter(string onyear, string custgroup, string shopid, string cliteria, bool sumonly)
        {
            DataTable dt = NewReportByPeriod(onyear, ReportPeriod.Quarter);
            DataTable rs = NewReportSales(new DataSet());

            string startDate = new DateTime(Convert.ToInt32(onyear), 1, 1).ToString("yyyy-MM-dd");
            string endDate = new DateTime(Convert.ToInt32(onyear), 12, 31).ToString("yyyy-MM-dd");
            //set default value            
            double valAmt = 0;
            double valQty = 0;
            double valSale = 0;

            double sumAmt = 0;
            double sumQty = 0;
            double sumSale = 0;

            double totalAmt = 0;
            double totalQty = 0;
            double totalSale = 0;

            //prepare grand total rows
            DataRow totAmt = dt.NewRow();
            totAmt["Group"] = "GRAND TOTAL";
            totAmt["Type"] = onyear;
            //check cliteria
            if (sumonly == true)
            {
                //sum by cust group
                DataTable custs = ShopGroupData(custgroup);
                foreach (DataRow cust in custs.Rows)
                {
                    //for each customer group
                    sumAmt = 0;
                    sumQty = 0;
                    sumSale = 0;

                    DataRow rowAmt = dt.NewRow();
                    rowAmt["Group"] = cust["custgroupnameth"].ToString();
                    rowAmt["Type"] = onyear;

                    //open shops data
                    DataTable shops = ShopData(cust["OID"].ToString());
                    for (int i = 1; i <= 4; i++)
                    {
                        totalAmt = 0;
                        totalQty = 0;
                        totalSale = 0;
                        //format : Q1/2016
                        string quarter = "Q" + i;
                        string yy = onyear;
                        //for each shop
                        foreach (DataRow shop in shops.Rows)
                        {
                            if (shopid == shop["OID"].ToString() || shopid == "-")
                            {
                                //get each month of quarter
                                List<MonthLOV> months = GetQuarterMonth(quarter);
                                foreach (MonthLOV month in months)
                                {
                                    string monthyear = yy + month.MonthID;
                                    //search data entry files
                                    DataTable files = XMLTableData(shop["OID"].ToString() + "_" + monthyear + "_*");
                                    foreach (DataRow File in files.Rows)
                                    {
                                        DataTable data = GetSalesData(GetPath() + File["filename"].ToString());
                                        foreach (DataRow dr in data.Select(cliteria))
                                        {
                                            try
                                            {
                                                DataRow tmp = rs.NewRow();
                                                tmp = ProcessRowReportSales(dr, tmp);
                                                valAmt = Convert.ToDouble(tmp["SalesIn"]);
                                                valSale = Convert.ToDouble(tmp["SalesOut"]);
                                                valQty = Convert.ToDouble(tmp["Qty"]);
                                            }
                                            catch (Exception ex)
                                            {
                                                errMessage = ex.Message;
                                                valAmt = 0;
                                                valQty = 0;
                                                valSale = 0;
                                            }
                                            totalAmt += valAmt;
                                            sumAmt += valAmt;
                                            totalQty += valQty;
                                            sumQty += valQty;
                                            totalSale += valSale;
                                            sumSale += valSale;
                                        }
                                    }
                                }
                            }
                        }
                        if (totalAmt > 0) rowAmt["SALESIN-" + quarter] = totalAmt.ToString("#,###,##0.00");
                        if (totalQty > 0) rowAmt["QTY-" + quarter] = totalQty;
                        if (totalSale > 0) rowAmt["SALESOUT-" + quarter] = totalSale.ToString("#,###,##0.00");
                    }
                    rowAmt["SALESIN"] = sumAmt.ToString("#,###,##0.00");
                    rowAmt["QTY"] = sumQty;
                    rowAmt["SALESOUT"] = sumSale.ToString("#,###,##0.00");
                    dt.Rows.Add(rowAmt);
                }
            }
            else
            {
                //show details             
                DataTable shops = ShopData(custgroup);
                foreach (DataRow shop in shops.Rows)
                {
                    if (shopid == shop["OID"].ToString() || shopid == "-")
                    {
                        sumAmt = 0;
                        sumQty = 0;
                        sumSale = 0;
                        //for each shop
                        DataRow rowAmt = dt.NewRow();
                        rowAmt["Group"] = shop["custname"].ToString();
                        rowAmt["Type"] = onyear;
                        for (int i = 1; i <= 4; i++)
                        {
                            totalAmt = 0;
                            totalQty = 0;
                            totalSale = 0;
                            //format : Q1/2016
                            string quarter = "Q" + i.ToString();
                            string yy = onyear;
                            List<MonthLOV> months = GetQuarterMonth(quarter);
                            foreach (MonthLOV month in months)
                            {
                                string monthyear = yy + month.MonthID;
                                //search data entry
                                DataTable files = XMLTableData(shop["OID"].ToString() + "_" + monthyear + "_*");
                                foreach (DataRow File in files.Rows)
                                {
                                    DataTable data = GetSalesData(GetPath() + File["filename"].ToString());
                                    DataRow tmp = rs.NewRow();
                                    foreach (DataRow dr in data.Select(cliteria))
                                    {
                                        try
                                        {
                                            tmp = ProcessRowReportSales(dr, tmp);
                                            valSale = Convert.ToDouble(tmp["SalesOut"]);
                                            valAmt = Convert.ToDouble(tmp["SalesIn"]);
                                            valQty = Convert.ToDouble(tmp["Qty"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            errMessage = ex.Message;
                                            valAmt = 0;
                                            valQty = 0;
                                            valSale = 0;
                                        }
                                        totalAmt += valAmt;
                                        sumAmt += valAmt;
                                        totalQty += valQty;
                                        sumQty += valQty;
                                        totalSale += valSale;
                                        sumSale += valSale;
                                    }
                                }
                            }
                            if (totalAmt > 0) rowAmt["SALESIN-" + quarter] = totalAmt.ToString("#,###,##0.00");
                            if (totalQty > 0) rowAmt["QTY-" + quarter] = totalQty;
                            if (totalSale > 0) rowAmt["SALESOUT-" + quarter] = totalSale.ToString("#,###,##0.00");
                        }
                        rowAmt["SALESIN"] = sumAmt.ToString("#,###,##0.00");
                        rowAmt["QTY"] = sumQty.ToString();
                        rowAmt["SALESOUT"] = sumSale.ToString("#,###,##0.00");
                        dt.Rows.Add(rowAmt);
                    }
                }
            }
            //sum all value from all rows
            for (int i = 2; i < dt.Columns.Count; i++)
            {
                double gt = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    try { gt += Convert.ToDouble(dr[i].ToString()); } catch { }
                }
                if (dt.Columns[i].ColumnName.IndexOf("QTY") >= 0)
                {
                    totAmt[i] = gt;
                }
                else
                {
                    totAmt[i] = gt.ToString("#,###,##0.00");
                }
            }
            dt.Rows.Add(totAmt);
            return dt;
        }
        public static void FormatDataToReadable(DataTable rs)
        {
            for (int i = 1; i < rs.Columns.Count - 2; i++)
            {
                string dstr = rs.Columns[i].ColumnName.Substring(6, 2) + "/" + rs.Columns[i].ColumnName.Substring(4, 2) + "/" + rs.Columns[i].ColumnName.Substring(0, 4);
                rs.Columns[i].ColumnName = dstr;
            }
        }
        public static DataTable SetGoodsCaption(DataTable rs)
        {
            DataTable dt = rs.Copy();
            DataView dv = dt.DefaultView;
            dv.Sort = "GoodsCode";
            dt = dv.ToTable();
            dt.Columns.Remove("OID");
            dt.Columns.Remove("GoodsCode");
            dt.Columns.Remove("GoodsName");
            dt.Columns.Remove("SMid");
            dt.Columns.Remove("SSid");
            dt.Columns.Remove("STid");
            dt.Columns.Remove("ColTypeid");
            dt.Columns.Remove("Colid");
            dt.Columns.Remove("ProdCatName");
            dt.Columns.Remove("ProdCatid");
            dt.Columns.Remove("ProdKindid");
            dt.Columns.Remove("ProdCatCode");
            dt.Columns.Remove("STName2");
            dt.Columns.Remove("SMCode");
            dt.Columns.Remove("STCode");
            dt.Columns.Remove("ProdGroupID");

            dt.Columns["ProdGroupName"].ColumnName = "ชนิด";
            dt.Columns["STName"].ColumnName = "สำหรับ";
            dt.Columns["ModelName"].ColumnName = "รุ่น";
            dt.Columns["ColNameEng"].ColumnName = "Color";
            dt.Columns["ColNameTh"].ColumnName = "สี";
            dt.Columns["ColNameInit"].ColumnName = "รหัสสี";
            dt.Columns["SizeNo"].ColumnName = "เบอร์";
            dt.Columns["StdSellPrice"].ColumnName = "ราคาป้าย";
            return dt;
        }
        public static void SetReportSalesCaption(DataTable dt)
        {
            dt.Columns[0].ColumnName = ("วันที่");
            dt.Columns[1].ColumnName = ("รุ่น");
            dt.Columns[2].ColumnName = ("สี");
            dt.Columns[3].ColumnName = ("ขนาด");
            dt.Columns[4].ColumnName = ("จำนวน");
            dt.Columns[5].ColumnName = ("ราคาป้าย");
            dt.Columns[6].ColumnName = ("ส่วนลด");
            dt.Columns[7].ColumnName = ("ราคาขาย");
            dt.Columns[8].ColumnName = ("Sales In");
            dt.Columns[9].ColumnName = ("จุดขาย");
            dt.Columns[10].ColumnName = ("ประเภทขาย");
            dt.Columns[11].ColumnName = ("กลุ่มสินค้า");
            dt.Columns[12].ColumnName = ("ชนิด");
            dt.Columns[13].ColumnName = ("ผู้บันทึก");
            dt.Columns[14].ColumnName = ("หมายเหตุ");
            dt.Columns[15].ColumnName = ("บันทึก");
            dt.Columns[16].ColumnName = ("Row ID");
            dt.Columns[17].ColumnName = ("สัมประสิทธิ์");
        }
        public static string GetFieldCaption(string fld)
        {
            string str =fld;
            switch(fld)
            {
                case "ModelCode": str = "รุ่น"; break;
                case "Color": str = "สี"; break;
                case "SizeNo": str = "ขนาด"; break;
                case "ProdQty": str = "จำนวนที่ต้องการ"; break;
                case "StockQty": str = "จำนวนที่จัดส่ง"; break;
                case "TagPrice": str = "ราคาป้าย"; break;
                case "StockDate": str = "วันที่ส่งของ"; break;
                case "AcceptCode": str = "ผู้ตรวจสอบ"; break;
                case "ConfirmCode": str = "ผู้รับสินค้า"; break;
                case "ApproveCode": str = "ผู้อนุมัติ"; break;
                case "ProdCatName": str = "ประเภทสินค้า"; break;
                case "RefNo": str = "เลขที่อ้างอิง"; break;
                case "OID": str = "ดำดับที่"; break;
                case "TransactionType": str = "ประเภทรายการ"; break;
                case "TransactionDate": str = "วันที่"; break;
                case "ShopName": str = "จุดขาย"; break;
            }
            return str;
        }
        public static DataTable SetReportStockCaption(DataTable dt)
        {
            try
            {
                for(int i=0;i<=dt.Columns.Count-1;i++)
                {
                    dt.Columns[i].ColumnName = GetFieldCaption(dt.Columns[i].ColumnName);
                }
            }
            catch
            {

            }
            return dt;
        }
        public static int ProcessStockReportSales(string shopid, string yymm)
        {
            double Gpx = 0;
            double sharedis = 0;
            DataTable shop = QueryData("Customer", "OID='" + shopid + "'");
            if (shop.Rows.Count > 0)
            {
                try { Gpx = Convert.ToDouble(shop.Rows[0]["GPx"].ToString()); } catch { }
                try { sharedis = Convert.ToDouble(shop.Rows[0]["ShareDiscount"].ToString()); } catch { }
            }
            string fname = yymm + "st" + shopid;
            int rowprocess = 0;
            DataTable stock = GetStockData(GetPath() + fname + ".xml");
            foreach (DataRow r in stock.Select("RefNo Like '" + "STO" + yymm + Convert.ToInt32(shopid).ToString("000") + "%'"))
            {
                r.Delete();
            }
            List<XMLFileList> files = GetXMLTableList(shopid + "_" + yymm + "_*");
            foreach (XMLFileList file in files)
            {
                DataTable sales = GetSalesData(GetPath() + file.filename);
                if (sales.Rows.Count > 0)
                {
                    sales.DefaultView.Sort = "ModelCode,ColorCode,SizeNo,salesDate,salesType,DiscountRate ";
                    string codevalue = "";
                    string chkstr = "";
                    bool flag = false;
                    DataRow r = stock.NewRow();
                    foreach (DataRow row in sales.DefaultView.ToTable().Rows)
                    {
                        flag = false;
                        chkstr = (row["ModelCode"].ToString().Trim() + row["ColorCode"].ToString().Trim() + (Convert.ToInt32(row["SizeNo"].ToString()) * 10).ToString()).ToString() + "D" + row["salesDate"].ToString() + row["salesType"].ToString() + "%" + row["discountRate"].ToString() + "P" + row["salesPrice"];
                        if (codevalue != chkstr)
                        {
                            r["TransactionDate"] = row["salesDate"];
                            r["TransactionBy"] = row["entryBy"];
                            r["StockType"] = -1;
                            r["TransactionState"] = "SAL";
                            r["TransactionType"] = "ขายสินค้า";
                            r["StockDate"] = row["salesDate"];
                            r["ApproveCode"] = row["entryBy"];
                            r["ConfirmCode"] = "System";
                            r["ModelCode"] = row["ModelCode"];
                            r["Color"] = row["colorName"];
                            r["SizeNo"] = row["SizeNo"];
                            r["GoodsCode"] = row["ModelCode"].ToString().Trim() + row["Colorcode"].ToString().Trim() + (Convert.ToInt32(row["SizeNo"].ToString()) * 10).ToString();
                            r["ProdCatName"] = GetprodType(row["prodType"].ToString());
                            r["ProdQty"] = Convert.ToDouble(0 + row["salesQty"].ToString());
                            r["TagPrice"] = Convert.ToDouble(0 + row["TagPrice"].ToString());
                            r["DiscountRate"] = Convert.ToDouble(0 + row["discountRate"].ToString());
                            flag = true;
                            codevalue = chkstr;
                        }
                        else
                        {
                            r["ProdQty"] = Convert.ToDouble(r["ProdQty"]) + Convert.ToDouble(0 + row["salesQty"].ToString());
                            if ((rowprocess + 1) == sales.Rows.Count)
                            {
                                flag = true;
                            }
                        }
                        if (flag == true)
                        {
                            rowprocess++;
                            string refno = "STO" + yymm + Convert.ToInt32(shopid).ToString("000") + rowprocess.ToString("0000");
                            r["RefNo"] = refno;
                            r["OID"] = GetNewOID(stock, fname, "OID");
                            r["AcceptCode"] = "System";
                            r["StockQty"] = Convert.ToDouble(r["ProdQty"]) * Convert.ToDouble(r["StockType"]);
                            r["SalesOut"] = (Convert.ToDouble(row["salesQty"]) * Convert.ToDouble(row["salesPrice"])).ToString("#0.00");
                            CalculateSalesGP(r, row, "SalesIn", "Cal");
                            r["UPriceOut"] = (Convert.ToDouble(r["SalesOut"]) / Convert.ToDouble(r["ProdQty"])).ToString("#0.00");
                            r["UPriceIn"] = (Convert.ToDouble(r["SalesIn"]) / Convert.ToDouble(r["ProdQty"])).ToString("#0.00");
                            r["StockIn"] = (Convert.ToDouble(r["UPriceIn"]) * Convert.ToDouble(r["StockQty"])).ToString("#0.00");
                            r["StockOut"] = (Convert.ToDouble(r["UPriceOut"]) * Convert.ToDouble(r["StockQty"])).ToString("#0.00");
                            stock.Rows.Add(r);
                            r = stock.NewRow();
                        }
                    }

                }
            }
            stock.WriteXml(GetPath() + fname + ".xml");
            return rowprocess;
        }
        public static void AddSummaryReportSales(DataTable dt, bool Nocheck)
        {
            double TotalQty = 0;
            double totalPrice = 0;
            double totalSalesIn = 0;
            double totalSalesOut = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (Nocheck == true)
                {
                    if (r["Qty"].ToString() != "")
                    {
                        try { TotalQty += Convert.ToDouble(r["Qty"]);} catch { }
                        try { totalPrice += Convert.ToDouble(r["TagPrice"]); } catch { }
                        try { totalSalesIn += Convert.ToDouble(r["SalesIn"]); } catch { }
                        try { totalSalesOut += Convert.ToDouble(r["SalesOut"]); } catch { }
                    }
                }
                else
                {
                    if (r[16].ToString() != "" && r["Qty"].ToString() != "")
                    {
                        try { TotalQty += Convert.ToDouble(r["Qty"]); } catch { }
                        try { totalPrice += (Convert.ToDouble(r["Qty"]) * Convert.ToDouble(r["TagPrice"])); } catch { }
                        try { totalSalesIn += Convert.ToDouble(r["SalesIn"]); } catch { }
                        try { totalSalesOut += Convert.ToDouble(r["SalesOut"]); } catch { }
                    }
                }
            }
            DataRow row = dt.NewRow();
            row["salesDate"] = "TOTAL";
            row["Model"] = "รวมทั้งสิ้น";
            row["Qty"] = TotalQty;
            row["TagPrice"] = totalPrice.ToString("#,###,##0.00");
            row["SalesOut"] = totalSalesOut.ToString("#,###,##0.00");
            row["SalesIn"] = totalSalesIn.ToString("#,###,##0.00");
            dt.Rows.Add(row);
        }
        public static string GetReportDataForEmail(DataTable dt,int rptindex,string uid, string datesend,string header,string linktext="Click")
        {
            string host = "http://"+ClsData.hostname+"/";
            string msg = "";
            try
            {                
                string url = "";
                string reportid = GetNewReportID(dt, uid, datesend);
                if (reportid != "")
                {
                    url = "savereport.aspx?reportid=" + reportid;
                }
                if (url != "")
                {
                    msg += header+ " ";
                    msg += "<a href='" + host + url + "'>"+ linktext +"</a>";
                }
            }
            catch(Exception e)
            {
                msg = "ERROR!:" + e.Message;
            }
            return msg;
        }
        public static string SendDataToEmail(DataTable dt,string uid,string ondate,string subject,string body,string mailto)
        {
            string msg = "";
            try
            {
                string reportid = GetNewReportID(dt, uid, ondate);
                if(reportid!="")
                {
                    string url = "http://" + ClsData.hostname +"/savereport.aspx?reportid=" + reportid;
                    CEMail.NewEmail();
                    CEMail.MailHost = "mail.summitsf.co.th";
                    CEMail.MailPort = 25;
                    CEMail.MailFrom = "puttipong@summitsf.co.th";
                    CEMail.MailPassword = "04071980";
                    CEMail.MailTo.Add(mailto);
                    CEMail.MailSubject = subject;
                    CEMail.MailBody = body +"<br/>คลิกดูรายงาน-><a href='" + url + "'>Click</a>";
                    msg = CEMail.SendEmail();
                }
                else
                {
                    msg = "ERROR!=No data to send!";
                }
            }
            catch (Exception ex)
            {
                msg = "ERROR!=" + ex.Message;
            }
            return msg;
        }
        public static string SendMailDailyReport(string subject,string data)
        {
            string msg = "";

            if(data!="")
            {
                foreach (DataRow dr in GetDataXML("maillist", ""))
                {
                    if (msg != "") msg += ",";
                    string mailto = dr[0].ToString();
                    try
                    {
                        CEMail.NewEmail();
                        CEMail.MailHost = "smtp.live.com";
                        CEMail.MailPort = 587;
                        CEMail.MailFrom = "leoputti@hotmail.com";
                        CEMail.isSSL = true;
                        CEMail.MailTo.Add(mailto);
                        CEMail.MailPassword = "Bo@t1234";
                        CEMail.MailSubject = subject;
                        CEMail.MailBody = data;
                        CEMail.isBodyHTML = true;
                        msg += "["+ mailto +"] " + CEMail.SendEmail();
                    }
                    catch (Exception ex)
                    {
                        msg += "["+mailto+"] ERROR!" + ex.Message;
                    }
                }
            }
            return msg;
        }
        #endregion
        #region xml-json utility function
        public static DataTable GetDataTableFromXML(string XMLString,string tbname)
        {
            DataSet ds = new DataSet();
            XmlTextReader rd = new XmlTextReader(new StringReader(XMLString));
            ds.ReadXml(rd,XmlReadMode.Auto);
            if(ds.Tables.Count>=1)
            {
                ds.Tables[0].TableName = tbname;
                return ds.Tables[0];
            }
            else
            {
                return new DataTable();
            }
        }
        public static DataTable GetDataTableFromJSON(string JSONString)
        {
            DataSet ds = new DataSet();
            string json = JSONString.Replace(@"\", "");
            string tmp = json.Substring(json.IndexOf('['), json.IndexOf(']') - json.IndexOf('[') + 1);
            tmp = @"{""NewDataSet"":" + tmp + @"}";
            ds =(DataSet) JsonConvert.DeserializeObject<DataSet>(tmp);
            return ds.Tables[0];
        }
        public static string GetXMLFromJSON(string jsonstring,string datasetname)
        {
            string data = "";
            try
            {
                XDocument doc = JsonConvert.DeserializeXNode(jsonstring);
                XmlReader xReader = doc.CreateReader();
                xReader.MoveToContent();
                data = @"<?xml version=""1.0"" standalone=""yes""?>";
                data = data + @"<"+datasetname+">";
                data = data + xReader.ReadInnerXml();
                data = data + @"</"+datasetname+">";
                xReader.Close();
                /*
                XmlDocument xml = new XmlDocument();
                XmlNode docNode = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                xml.AppendChild(docNode);
                XmlNode root = xml.CreateElement("NewDataSet");
                XmlNode tbl = xml.CreateElement("Table");
                XmlNode node = JsonConvert.DeserializeXmlNode(jsonstring, "Table");
                tbl.AppendChild(node);
                root.AppendChild(tbl);
                xml.AppendChild(root);
                data = xml.OuterXml.ToString();
                */
            }
            catch(Exception ex)
            {
                errMessage = GetXMLMessage("ERROR", ex.Message);
            }
            return data;
        }
        public static string GetXMLFileName(string shop, string yymm, string uid)
        {
            return shop.ToLower() + "_" + yymm + "_" + uid.ToLower() + ".xml";
        }
        public static string GetXMLMessage(string header,string text)
        {
            string data = "";
            data = @"<?xml version=""1.0"" standalone=""yes""?>";
            data += "<DocumentElement><Message><" + header + @">"+ text +@"</" + header + @"></Message></DocumentElement>";
            return data;
        }
        public static string GetJSONMessage(string header, string text)
        {
            return @"{""Message"":[{""" + header + @""":""" + text + @"""}]}";
        }
        public static string GetJSONFromXML(string tablename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GetPath() + tablename + ".xml");
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }
        public static string GetJSONFromXMLString(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc.DocumentElement);
            return jsonText;
        }
        public static string GetJSONFromTable(DataTable dt, string grouplog = "")
        {
            string str = @"{""" + grouplog + @""":[";
            if (dt.Rows.Count > 0)
            {                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0) str += ",";
                    str += "{";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != 0) str += ",";
                        string val = dt.Rows[i][j].ToString().Trim();
                        val = val.Replace(@"""", @"\""");
                        str += @"""" + dt.Columns[j].ColumnName + @""":""" + val + @"""";
                    }
                    str += "}";
                }
            }
            else
            {
                str += "{";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j != 0) str += ",";
                    str += @"""" + dt.Columns[j].ColumnName + @""":""""";
                }
                str += "}";
            }
            str += "]}";
            return str;
        }
        #endregion
        #region service function for UI
        public static void LoadShoe(DropDownList cbo,string fldshow,string fldval,string model="")
        {
            if(model!="")
            {
                LoadCombo(ShoeDataByModel(model),cbo, fldshow, fldval);
            }
            else
            {
                LoadCombo(ShoeData(), cbo, fldshow, fldval);
            }
        }
        public static void LoadShoeColor(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(ShoeColorData(),cbo, fldshow, fldval);
        }
        public static void LoadShoeCategory(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(ShoeCategoryData(), cbo, fldshow, fldval);
        }
        public static void LoadShoeSize(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(ShoeSizeData(),cbo, fldshow, fldval);
        }
        public static void LoadShoeType(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(ShoeTypeData(), cbo, fldshow, fldval);
        }
        public static void LoadShoeGroup(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(ShoeGroupData(), cbo, fldshow, fldval);
        }
        public static void LoadMold(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(ShoeMoldData(),cbo, fldshow, fldval);
        }
        public static void LoadModel(DropDownList cbo,string fldshow,string fldval,bool foradd=false)
        {
            LoadCombo(ShoeModelData(foradd),cbo, fldshow, fldval);
        }
        public static void LoadShopGroup(DropDownList cbo,string fldshow,string fldval,bool foradd=false)
        {
            LoadCombo(CustomerGroupData(foradd), cbo, fldshow, fldval);
        }
        public static void LoadShopGroup(string shopgroup,DropDownList cbo,string fldshow,string fldval,bool foradd=false)
        {
            LoadCombo(ShopGroupData(shopgroup, foradd), cbo, fldshow, fldval);
        }
        public static void LoadShop(DropDownList cbo,string fldshow,string fldval,bool foradd =false,bool changetoBlank=false)
        {
            LoadCombo(ShopData(foradd,changetoBlank), cbo, fldshow, fldval);
        }
        public static void LoadShopByGroup(DropDownList cbo,string groupstr,string fldshow,string fldval,bool foradd=false,bool forall=false)
        {
            DataTable dt = ShopData(groupstr,foradd);
            if(forall==true && foradd==true)
            {
                dt.Rows[0][0] = "-";
                dt.Rows[0][1] = "0";
                dt.Rows[0][2] = "(ทั้งหมด)";
            }
            LoadCombo(dt, cbo, fldshow, fldval);
        }
        public static void LoadRole(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(RoleData(), cbo, fldshow, fldval);
        }
        public static void LoadSaleType(DropDownList cbo,string fldshow,string fldval,bool foradd=false)
        {
            LoadCombo(SalesTypeData(foradd), cbo, fldshow, fldval);
        }
        public static void LoadGPType(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(GetCalGPType(),cbo, fldshow, fldval);
        }
        public static void LoadMonth(DropDownList cbo,string fldshow,string fldval)
        {
            LoadCombo(GetMonth(), cbo, fldshow, fldval);
        }
        public static void LoadReportType(DropDownList cbo, string role)
        {
            cbo.Items.Clear();
            cbo.Items.Add("ยอดขายตามจุดขาย");
            if (role == "2" || role == "0")
            {
                cbo.Items.Add("ยอดขายตามพนักงาน");
                cbo.Items.Add("ยอดขายตามวันที่");
                cbo.Items.Add("จำนวนขายตามวันที่");
                cbo.Items.Add("ยอดขายรายไตรมาส");
                cbo.Items.Add("ยอดขายรายปี");
            }
        }
        public static void LoadProdType(DropDownList cbo)
        {
            cbo.Items.Clear();
            cbo.Items.Add("สินค้าปกติ");
            cbo.Items.Add("One Price");
        }
        #endregion
    }

}  