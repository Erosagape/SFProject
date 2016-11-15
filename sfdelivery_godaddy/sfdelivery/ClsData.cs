using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Xml;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    //คลาสหลักสำหรับเชื่อมต่อฐานข้อมูล
    public static class ClsData
    {
        public static DataTable tempTable;
        private static string serverpath = "";
        private static DataSet dtUser = new DataSet();
        private static string errMessage = "";
        private static List<AccessTableLog> OpeningData = new List<AccessTableLog>();
        public static string Error()
        {
            return errMessage;
        }
        public static void SetPath(string path)
        {
            serverpath = path;
        }
        public static string GetPath()
        {
            if (serverpath == "") serverpath = WebServerPath() + "\\..\\";
            return serverpath;
        }
        private static string WebServerPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
        public static DataTable UserData(bool reset = false)
        {
            if (reset == true) dtUser = new DataSet();
            if (dtUser.Tables.Count == 0) dtUser.ReadXml(GetPath() + "listuser.xml");
            return dtUser.Tables[0];
        }
        public static DataTable UserData(string zoneid, bool reset = false)
        {
            if (reset == true) dtUser = new DataSet();
            if (dtUser.Tables.Count == 0) dtUser.ReadXml(GetPath() + "listuser.xml");
            DataTable dt = dtUser.Tables[0].Copy();
            DataTable rs = dt.Clone();
            foreach (DataRow dr in dt.Select("zonecode='" + zoneid + "'"))
            {
                rs.ImportRow(dr);
            }
            return rs;
        }
        private static List<XMLFileList> GetXMLTableList(string filter = "*")
        {
            List<XMLFileList> lst = new List<XMLFileList>();
            DirectoryInfo dir = new System.IO.DirectoryInfo(GetPath());
            foreach (FileInfo file in dir.GetFiles(filter + ".xml"))
            {
                //7 + 7 เพราะ server Godaddy = เวลา สหรัฐ (-7 จากเวลาโลก) และต้อง + 7 กลับจากเวลาโลกให้เป็นเวลาไทย (7+7=14 ชั่วโมง)
                string lastupdate = file.LastWriteTime.AddHours(7 + 7).ToString();
                lst.Add(new XMLFileList() { filename = file.Name, filesize = file.Length.ToString(), modifieddate = lastupdate });
            }
            return lst;
        }
        private static List<RoleStruct> GetRole()
        {
            List<RoleStruct> RoleList = new List<RoleStruct>();
            RoleList.Add(new RoleStruct() { roleID = 0, roleName = "Admin" });
            RoleList.Add(new RoleStruct() { roleID = 1, roleName = "Sales supervisor" });
            RoleList.Add(new RoleStruct() { roleID = 2, roleName = "Sales" });
            RoleList.Add(new RoleStruct() { roleID = 3, roleName = "Delivery Staff" });
            return RoleList;
        }
        public static DataTable XMLTableData(string filterstr = "*")
        {
            DataTable dt = GetXMLTableList(filterstr).ToDataTable();
            return dt;
        }
        public static DataTable RoleData()
        {
            DataTable dt = GetRole().ToDataTable();
            return dt;
        }
        public static DataTable ToDataTable<TSource>(this IList<TSource> data)
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
        public static DataRow QueryData(DataTable dt, string Cliteria)
        {
            DataRow dr = dt.NewRow();
            DataRow[] rowSet = dt.Select(Cliteria);
            foreach (DataRow row in rowSet)
            {
                dr = row;
            }
            return dr;
        }
        public static bool WriteDataXML(string fname, string XMLData)
        {
            bool success = true;
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(XMLData);
                fname = fname.Replace(".xml", "");
                xml.Save(GetPath() + fname + ".xml");
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
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
                fname = fname.Replace(".xml", "");
                xml.Save(GetPath() + fname + ".xml");
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
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
        public static string GetXML(DataTable dt)
        {
            StringWriter wr = new StringWriter();
            dt.WriteXml(wr);
            return wr.ToString();
        }
        public static DataTable GetDataXML(string tbname)
        {
            errMessage = "";
            DataTable dt = new DataTable();
            dt.TableName = tbname;
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetPath() + tbname + ".xml");
                dt = ds.Tables[0];
            }
            catch(Exception ex)
            {
                errMessage = ex.Message;
            }
            return dt;
        }
        public static DataTable GetDataTableFromXML(string XMLString, string tbname)
        {
            DataSet ds = new DataSet();
            XmlTextReader rd = new XmlTextReader(new StringReader(XMLString));
            ds.ReadXml(rd, XmlReadMode.Auto);
            ds.Tables[0].TableName = tbname;
            return ds.Tables[0];
        }
        public static DataTable GetDataTableFromJSON(string JSONString,int tbIndex=0)
        {
            errMessage = "";
            DataTable dt = new DataTable();
            try
            {
                string json = JSONString.Replace(@"\", "");
                if (json.Substring(0, 1) == "[")
                {
                    dt = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));
                }
                if (json.Substring(0, 1) == "{")
                {
                    DataSet ds = (DataSet)JsonConvert.DeserializeObject(json, typeof(DataSet));
                    dt = ds.Tables[tbIndex];
                }
            }
            catch(Exception ex)
            {
                errMessage = ex.Message;
            }
            return dt;
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
        public static DataTable GetLockDataTable()
        {
            DataTable dt = OpeningData.ToDataTable();
            return dt;
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
        public static string GetNewOID(DataTable rs, string tbname, string fldname)
        {
            //check if file still call by same procedure
            if (rs.Columns.IndexOf("PK") > 0)
            {
                rs.Columns.Remove("PK");
            }
            if (rs.Columns.IndexOf("PK") < 0)
            {
                Int32 oid = 0;
                try
                {
                    rs.Columns.Add("PK", typeof(int), "Convert(" + fldname + ",'System.Int32')");
                    object values = (object)rs.Compute("Max(PK)", string.Empty);
                    rs.Columns.Remove("PK");
                    oid = Convert.ToInt32(values) + 1;
                }
                catch (Exception ex)
                {
                    errMessage = ex.Message;
                }
                return oid.ToString();
            }
            else
            {
                return "";
            }
        }
        public static void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }
        public static string GetXMLFromJSON(string jsonstring)
        {
            string data = "";
            try
            {
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
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }
            return data;
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
        public static bool CompareRow(DataRow row1, DataRow row2, DataTable tb)
        {
            //row1 = row to compare
            //row2 = row source to compare with
            bool pass = true;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                string colname = tb.Columns[i].ColumnName;
                try
                {
                    bool match = row1[colname].ToString().Trim() == row2[colname].ToString().Trim();
                    if (match == false)
                    {
                        pass = false;
                        break;
                    }
                }
                catch
                {
                    pass = false;
                }
            }
            return pass;
        }
        public static string GetJSONMessage(string header, string text)
        {
            return @"{""Message"":[{""" + header + @""":""" + text + @"""}]}";
        }
        public static string GetXMLMessage(string header, string text)
        {
            string data = "";
            data = @"<?xml version=""1.0"" standalone=""yes""?>";
            data += "<DocumentElement><Message><" + header + @">" + text + @"</" + header + @"></Message></DocumentElement>";
            return data;
        }
        public static string GetJSONFromTable(DataTable dt, string grouplog = "")
        {
            string str = "";
            if(grouplog!="")
            {
                str = @"{""" + grouplog + @""":";
            }
            str += @"[";
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
            str += "]";
            if (grouplog != "") str += @"}";
            return str;
        }
        public static DataTable SetCaptionDataDelivery(DataTable dt)
        {
            DataTable rs = dt.Copy();
            for (int j = 0; j < rs.Columns.Count; j++)
            {
                rs.Columns[j].ColumnName = GetColumnNameTh(rs.Columns[j].ColumnName);
            }
            return rs;
        }
        public static string GetColumnNameTh(string nameeng)
        {
            string col = nameeng;
            if (nameeng == "ID") col = "เลขที่เอกสาร";
            if (nameeng == "IStatus") col = "สถานะเอกสาร";
            if (nameeng == "DocDate") col = "วันที่ออกเอกสาร";
            if (nameeng == "DueDate") col = "ดิวเอกสาร";
            if (nameeng == "Reference") col = "อ้างถึงเอกสาร";
            if (nameeng == "Customer") col = "รหัสลูกค้า";
            if (nameeng == "CustomerName") col = "ชื่อลูกค้า";
            if (nameeng == "SalesMan") col = "ผู้ออกเอกสาร";
            if (nameeng == "Account") col = "เลขที่ใบตุม";
            if (nameeng == "ShipTo1") col = "สถานที่ส่ง1";
            if (nameeng == "ShipTo2") col = "สถานที่ส่ง2";
            if (nameeng == "Remark1") col = "หมายเหตุ1";
            if (nameeng == "Remark2") col = "หมายเหตุ2";
            if (nameeng == "Term") col = "ดิวชำระเงิน";
            if (nameeng == "Gross") col = "ยอดเงินก่อนVAT";
            if (nameeng == "Amount") col = "ยอดเงินสุทธิ";
            if (nameeng == "Qty") col = "จำนวน";
            if (nameeng == "Driver") col = "คนงาน-คนขับรถ";
            if (nameeng == "Mark6") col = "วันที่ส่งของ";
            if (nameeng == "Mark8") col = "สถานะการส่งของ";
            if (nameeng == "SID") col = "เลขSalesOrder";
            if (nameeng == "SStatus") col = "สถานะSO";
            if (nameeng == "SDocDate") col = "วันที่SO";
            if (nameeng == "SSalesMan") col = "พนักงานขาย";
            if (nameeng == "ID_Detail") col = "เลขที่เอกสาร";
            if (nameeng == "ItemNo") col = "ลำดับที่";
            if (nameeng == "Product") col = "รหัส";
            if (nameeng == "ProductName") col = "ชื่อสินค้า";
            if (nameeng == "VAT" || nameeng=="VatAmt") col = "VAT";
            if (nameeng == "DQty") col = "จำนวน";
            if (nameeng == "UnitPrice") col = "ราคาต่อหน่วย";
            if (nameeng == "DAmount") col = "ยอดเงิน";
            if (nameeng == "Box") col = "กล่อง";
            if (nameeng == "Bundle") col = "มัด";
            if (nameeng == "Sack") col = "กระสอบ";
            if (nameeng == "DiscPer") col = "ส่วนลด";
            if (nameeng == "StateNo") col = "เลขที่RS";
            if (nameeng == "TransID") col = "รหัสผู้ขนส่ง";
            if (nameeng == "TransName") col = "ชื่อผู้ขนส่ง";
            return col;
        }
        public static DataTable GetDeliveryData(string filterstr,string cliteria)
        {
            DataTable files = XMLTableData("Delivery" + filterstr+ "*");
            DataTable data = new DataTable();
            data.TableName = "Table";
            bool isFirsttime = true;
            foreach (DataRow file in files.Rows)
            {
                string fname = file["filename"].ToString().Replace(".xml", "");
                DataTable dt = QueryData(fname, cliteria);
                dt.Columns.Add(new DataColumn("filename"));
                if (isFirsttime == true)
                {
                    data = dt.Clone();                    
                    isFirsttime = false;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        dr["filename"] = fname;
                        data.ImportRow(dr);
                    }
                    catch
                    {

                    }
                }
            }
            return data;
        }
        public static DataTable GetDeliveryDataByEmp(string empid,string filter,string cliteria)
        {
            DataTable dt = new DataTable();
            if (empid != "" && empid != "ALL")
            {
                dt = ClsData.GetDeliveryData(filter, "[SalesMan] in('" + empid.Trim().Replace(",", "','") + "')" + cliteria);
            }
            else
            {
                dt = ClsData.GetDeliveryData(filter, cliteria);
            }
            return dt;
        }
        public static List<string> GetCustomerList(string empid, string filter)
        {
            var rs = (from DataRow dr in GetDeliveryDataByEmp(empid, filter, "").Rows
                      orderby dr["CustomerName"]
                      select new { Custname = (string)dr["Customer"] + " : " + (string)dr["CustomerName"] }).Distinct();
            return rs.Select(i=> i.Custname).ToList();
        }
        public static DataTable GetCustomerByUser(string username,string filealias="2")
        {            
            DataRow r = ClsData.QueryData("listuser", "id='" + username + "'").Rows[0];
            string empid = r["empid"].ToString();
            DataTable dt= new DataTable();
            dt.TableName = "CustomerList";
            dt.Columns.Add(new DataColumn("CodeName", typeof(string)));
            foreach (string str in GetCustomerList(empid, filealias))
            {
                DataRow dr = dt.NewRow();
                dr[0] = str.Trim();
                dt.Rows.Add(str);
            }
            return dt;
        }
        public static void LoadStatus(DropDownList cbo)
        {
            cbo.Items.Add("");
            cbo.Items.Add("ส่งแล้ว");
            cbo.Items.Add("ยังไม่ได้ส่ง");
            cbo.Items.Add("ยกเลิกการส่ง");
        }
    }
}