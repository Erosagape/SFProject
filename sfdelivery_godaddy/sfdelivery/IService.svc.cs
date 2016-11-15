using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;

namespace sfdelivery
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ITest" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ITest.svc or ITest.svc.cs at the Solution Explorer and start debugging.
    public class IService : IDataExchange
    {
        public string Login(string user,string password)
        {
            string fname = "Failed! User or Password Incorrect";
            DataTable dt = ClsData.QueryData("listuser", "[id]='" + user + "' and [password]='" + password + "'");
            if(dt.Rows.Count>0)
            {
                fname ="Welcome! "+ dt.Rows[0]["name"].ToString();
            }
            return fname;
        }
        public string ShowData(string data)
        {
            return "ข้อมูลที่เราได้รับคือ = " + data;
        }
        public string GetDataXML(string dataname,bool createnew)
        {
            try
            {
                bool isFound = true;
                if(System.IO.File.Exists(ClsData.GetPath() + dataname + ".xml")==false)
                {
                    isFound = false;
                    if(createnew==true)
                    {
                        isFound=ClsData.NewDataXML(dataname, "NewDataSet", "Table");
                    }
                }
                XmlDocument xml = new XmlDocument();
                if(isFound==true)
                {
                    xml.Load(ClsData.GetPath() + dataname + ".xml");
                }
                else
                {
                    XmlNode docNode = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xml.AppendChild(docNode);
                    XmlNode root = xml.CreateElement("NewDataSet");
                    XmlNode tbl = xml.CreateElement("Table");
                    root.AppendChild(tbl);
                    xml.AppendChild(root);
                }
                return xml.OuterXml.ToString();
            }
            catch (Exception ex)
            {
                return ClsData.GetXMLMessage("ERROR", ex.Message);
            }
        }
        public string GetDataJSON(string dataname)
        {
            try
            {
                DataTable dt = ClsData.GetDataXML(dataname);
                return ClsData.GetJSONFromTable(dt, dataname);
            }
            catch (Exception ex)
            {
                return ClsData.GetJSONMessage("ERROR", ex.Message);
            }
        }
        public string QueryDataXML(string dataname, string cliteria)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(ClsData.GetPath() + dataname + ".xml");
                DataTable dt = ds.Tables[0].Clone();
                dt.TableName = dataname;
                DataRow[] dr = ds.Tables[0].Select(cliteria);
                foreach (DataRow r in dr)
                {
                    dt.ImportRow(r);
                }
                StringWriter wr = new StringWriter();
                dt.WriteXml(wr);
                return wr.ToString();
            }
            catch (Exception ex)
            {
                return ClsData.GetXMLMessage("ERROR", ex.Message);
            }
        }
        public string QueryDataJSON(string dataname, string cliteria)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(ClsData.GetPath() + dataname + ".xml");
                DataTable dt = ds.Tables[0].Clone();
                DataRow[] dr = ds.Tables[0].Select(cliteria);
                foreach (DataRow r in dr)
                {
                    dt.ImportRow(r);
                }
                string json = ClsData.GetJSONFromTable(dt, dataname);
                return json;
            }
            catch (Exception ex)
            {
                return ClsData.GetJSONMessage("ERROR", ex.Message);
            }
        }
        public string RemoveDataXML(string dataname, string cliteria)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(ClsData.GetPath() + dataname + ".xml");
                DataTable dt = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select(cliteria);
                int i = 0;
                foreach (DataRow r in dr)
                {
                    dt.Rows.Remove(r);
                    i++;
                }
                dt.WriteXml(ClsData.GetPath() + dataname + ".xml");
                return "Completed! " + i.ToString() + " rows deleted";
            }
            catch (Exception ex)
            {
                return "ERROR" + ex.Message;
            }
        }
        public string ProcessDataXML(string dataname, string xmlstr)
        {
            try
            {
                if (System.IO.File.Exists(ClsData.GetPath() + dataname + ".xml") == false)
                {
                    ClsData.WriteDataXML(dataname, xmlstr);
                    return String.Format("Complete! '{0}' Created!", dataname);
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(ClsData.GetPath() + dataname + ".xml");
                    DataTable dtDest = ds.Tables[0];

                    XmlTextReader rd = new XmlTextReader(new StringReader(xmlstr));
                    rd.Read();
                    DataSet src = new DataSet();
                    src.ReadXml(rd, XmlReadMode.Auto);
                    int add = 0;
                    int upd = 0;
                    DataTable dtSrc = src.Tables[0];
                    string keyname = dtSrc.Columns[0].ColumnName;
                    foreach (DataRow rowsrc in dtSrc.Rows)
                    {
                        DataRow[] rows = dtDest.Select(keyname + "='" + rowsrc[0].ToString() + "'");
                        DataRow row = dtDest.NewRow();
                        foreach (DataRow rowdest in rows)
                        {
                            row = rowdest;
                        }
                        if (ClsData.CompareRow(row, rowsrc, dtDest) == false)
                        {
                            for (int i = 0; i < dtSrc.Columns.Count; i++)
                            {
                                row[dtSrc.Columns[i].ColumnName] = rowsrc[dtSrc.Columns[i].ColumnName];
                            }
                            if (row.RowState == DataRowState.Detached)
                            {
                                dtDest.Rows.Add(row);
                                add++;
                            }
                            else
                            {
                                upd++;
                            }
                        }
                    }
                    if((add + upd)>0)
                    {
                        dtDest.WriteXml(ClsData.GetPath() + dataname + ".xml");
                        return String.Format("Complete! Add {0} Update {1}", add.ToString(), upd.ToString());
                    }
                    else
                    {
                        return String.Format("Complete! '{0}' already Updated", dataname);
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error! " + ex.Message;
            }
        }
        public string ProcessDataJSON(string dataname, string jsondata)
        {
            try
            {
                if (System.IO.File.Exists(ClsData.GetPath() + dataname + ".xml") == false)
                {
                    string xmlstr = ClsData.GetXMLFromJSON(jsondata);
                    ClsData.WriteDataXML(dataname, xmlstr);
                }
                DataSet ds = new DataSet();
                ds.ReadXml(ClsData.GetPath() + dataname + ".xml");
                DataTable dtDest = ds.Tables[0];

                int add = 0;
                int upd = 0;
                DataTable dtSrc = ClsData.GetDataTableFromJSON(jsondata);
                string keyname = dtSrc.Columns[0].ColumnName;
                foreach (DataRow rowsrc in dtSrc.Rows)
                {
                    DataRow[] rows = dtDest.Select(keyname + "='" + rowsrc[0].ToString() + "'");
                    DataRow row = dtDest.NewRow();
                    foreach (DataRow rowdest in rows)
                    {
                        row = rowdest;
                    }
                    if (ClsData.CompareRow(rowsrc, row, dtSrc) == false)
                    {
                        for (int i = 0; i < dtSrc.Columns.Count; i++)
                        {
                            row[dtSrc.Columns[i].ColumnName] = rowsrc[dtSrc.Columns[i].ColumnName];
                        }
                        if (row.RowState == DataRowState.Detached)
                        {
                            dtDest.Rows.Add(row);
                            add++;
                        }
                        else
                        {
                            upd++;
                        }
                    }
                }
                dtDest.WriteXml(ClsData.GetPath() + dataname + ".xml");
                return String.Format("Complete! Add {0} Update {1}", add.ToString(), upd.ToString());
            }
            catch (Exception ex)
            {
                return "Error! " + ex.Message;
            }
        }
        public string GetXMLFileList(string filterstr = "*")
        {
            DataTable dt = ClsData.XMLTableData(filterstr);
            return ClsData.GetXML(dt);
        }
        public string GetDeliveryDataXML(string dataname,string cliteria)
        {
            DataTable dt = ClsData.GetDeliveryData(dataname, cliteria);
            return ClsData.GetXML(dt);
        }
        public string GetCustomerListXML(string username,string dataname)
        {
            return ClsData.GetXML(ClsData.GetCustomerByUser(username,dataname));
        }
        public string GetDeliveryInfo(string docno,string doctype)
        {                        
            string msg = "ไม่พบเอกสาร "+ docno;
            try
            {
                DataTable dt = ClsData.GetDeliveryData(doctype, "[ID]='" + docno + "'");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    msg = "";
                    msg += "เลขที่เอกสาร=" + dr["ID"].ToString() + "\n";
                    msg += "วันที่ส่งของ=" + dr["Mark6"].ToString() + "\n";
                    msg += "ลูกค้า=" + dr["CustomerName"].ToString() + "\n";
                    msg += "จำนวนคู่=" + dr["Qty"].ToString() + "\n";
                    msg += "กล่อง=" + dr["Box"].ToString() + "\n";
                    msg += "มัด=" + dr["Bundle"].ToString() + "\n";
                    msg += "กระสอบ=" + dr["Sack"].ToString() + "\n";
                    msg += "ใบสั่งขาย=" + dr["SID"].ToString() + "\n";
                    msg += "สถานะ=" + dr["Mark8"].ToString() + "\n";
                }
            }
            catch(Exception ex)
            {
                msg = "Error->" + ex.Message;
            }
            return msg;
        }
        public string UpdateDeliveryInfo(string docno,string cmdset)
        {
            string msg = "";
            try
            {
                DataTable files = ClsData.XMLTableData("Delivery2*");
                foreach(DataRow file in files.Rows)
                {                    
                    string fname = file["filename"].ToString().Replace(".xml","");
                    if(ClsData.IsLockedDataTable(fname,"Admin")==false)
                    {
                        ClsData.LockDataTable(fname, "Admin");
                        DataTable dt = ClsData.GetDataXML(fname);
                        bool isfound = false;
                        foreach (DataRow dr in dt.Select("[ID]='"+docno+"'"))
                        {
                            foreach (string cmd in cmdset.Split(';'))
                            {
                                if (cmd.IndexOf("|") > 0)
                                {
                                    string[] str = cmd.Split('|');
                                    string fieldname = str[0];
                                    string fieldval = str[1];
                                    dr[fieldname] = fieldval;
                                    isfound = true;
                                }
                            }
                        }
                        if(isfound==true)
                        {
                            dt.WriteXml(ClsData.GetPath() + file["filename"].ToString());
                            msg = "Completed!->บันทึก "+ docno +" เรียบร้อย!";
                            ClsData.UnlockDataTable(fname, "Admin");
                            break;
                        }
                        else
                        {
                            msg = "Failed->ไม่พบข้อมูล!";
                        }
                        ClsData.UnlockDataTable(fname, "Admin");
                    }
                    else
                    {
                        msg = "Failed->ข้อมูลถูกใช้อยู่";
                    }
                }
            }
            catch(Exception ex)
            {
                msg = "ERROR->" + ex.Message;
            }
            return msg;
        }
        public string WriteDataUpdate(string dataname,string jsondata)
        {
            string msg = "ERROR!->Cannot Process Files";
            try
            {
                StreamWriter wr = new StreamWriter(ClsData.GetPath() + dataname + ".json", true);
                wr.WriteLine(jsondata);
                wr.Close();
                msg = "Complete!->File "+ dataname +".json uploaded";
            }
            catch(Exception e)
            {
                msg = "ERROR!->"+e.Message;
            }
            return msg;
        }
        public string UpdateData(string dataname)
        {
            string msg = "Complete! No data updated";
            try
            {
                int add = 0;
                int upd = 0;
                int tot = 0;
                int countfile = 0;
                DirectoryInfo dir = new System.IO.DirectoryInfo(ClsData.GetPath());
                foreach (FileInfo file in dir.GetFiles(dataname + ".json"))
                {
                    countfile++;
                    StreamReader rd = new StreamReader(file.FullName);
                    string jsonStr = rd.ReadToEnd();
                    rd.Close();
                    string fname = file.Name;
                    bool complete = false;
                    if(fname.IndexOf("_")>0)
                    {
                        fname = file.Name.Substring(0, file.Name.IndexOf("_") );
                    }
                    DataTable dtSrc = ClsData.GetDataTableFromJSON(jsonStr);
                    dtSrc.TableName = fname;
                    if (File.Exists (ClsData.GetPath() +fname + ".xml")== false)
                    {
                        dtSrc.TableName = fname;
                        string strXML = ClsData.GetXML(dtSrc);
                        complete=ClsData.WriteDataXML(fname, strXML);
                        if(complete==true)
                        {
                            tot += dtSrc.Rows.Count;
                            add += dtSrc.Rows.Count;
                            file.Delete();
                        }
                    }
                    else
                    {
                        
                        DataTable dtDest = ClsData.GetDataXML(fname);
                        if (dtDest.Columns.Count == 1) dtDest = dtSrc.Clone();
                        tot += dtSrc.Rows.Count;
                        foreach (DataRow drSrc in dtSrc.Rows)
                        {
                            string keyname = dtSrc.Columns[0].ColumnName;
                            string keyval = drSrc[keyname].ToString();
                            DataRow row = dtDest.NewRow();
                            bool bfound = false;
                            foreach (DataRow drDest in dtDest.Select("[" + keyname.Trim() + "]='" + keyval.Trim() + "'"))
                            {
                                bfound = true;
                                if (ClsData.CompareRow(drSrc, drDest, dtSrc) == false)
                                {
                                    row = drDest;
                                    for (int i = 0; i < dtSrc.Columns.Count; i++)
                                    {
                                        row[dtSrc.Columns[i].ColumnName] = drSrc[dtSrc.Columns[i].ColumnName];
                                    }
                                    upd++;
                                }
                                complete = true;
                            }
                            if (bfound == false)
                            {
                                for (int i = 0; i < dtSrc.Columns.Count; i++)
                                {
                                    row[dtSrc.Columns[i].ColumnName] = drSrc[dtSrc.Columns[i].ColumnName];
                                }
                                if (row.RowState == DataRowState.Detached)
                                {
                                    dtDest.Rows.Add(row);
                                    complete = true;
                                    add++;
                                }
                            }
                        }
                        if (complete == true)
                        {
                            file.Delete();
                            dtDest.WriteXml(ClsData.GetPath() + fname + ".xml");
                            dtDest.Dispose();
                        }
                    }
                    dtSrc.Dispose();
                }
                msg = "Completed! "+dataname+" files="+countfile+" rec=" + tot + " add=" + add + ",update=" + upd;
            }
            catch(Exception e)
            {
                msg = "ERROR!=" + e.Message;
            }
            return msg;
        }
        public string ClearDataJSON(string dataname)
        {
            string msg = "Complete!";
            try
            {
                DirectoryInfo dir = new System.IO.DirectoryInfo(ClsData.GetPath());
                foreach (FileInfo file in dir.GetFiles(dataname + "*.json"))
                {
                    string fname = file.Name;
                    file.Delete();
                    msg = fname + " Deleted!" + (msg=="" ? "":",") +msg;
                }
            }
            catch (Exception e)
            {
                msg = "ERROR:" + e.Message;
            }
            return msg;
        }
        public string OpenDataForupdate(string dataname)
        {
            string msg = "Failed!";
            try
            {
                ClsData.tempTable = new DataTable();
                ClsData.tempTable = ClsData.GetDataXML(dataname).Copy();
                msg = dataname +" Opened!";
            }
            catch (Exception e)
            {
                msg = "ERROR:" + e.Message;
            }
            return msg;
        }
        public string ReadDataForupdate(string dataname)
        {
            string msg = "Read "+ dataname +" Failed!";
            try
            {
                int add = 0;
                int upd = 0;
                int tot = 0;
                int countfile = 0;
                DirectoryInfo dir = new System.IO.DirectoryInfo(ClsData.GetPath());
                foreach (FileInfo file in dir.GetFiles(dataname + ".json"))
                {
                    countfile++;
                    StreamReader rd = new StreamReader(file.FullName);
                    string jsonStr = rd.ReadToEnd();
                    rd.Close();
                    rd.Dispose();
                    string fname = dataname;
                    bool complete = false;
                    if (fname.IndexOf("_") > 0)
                    {
                        fname = dataname.Substring(0, dataname.IndexOf("_"));
                    }
                    DataTable dtSrc = ClsData.GetDataTableFromJSON(jsonStr);
                    if (File.Exists(ClsData.GetPath() + fname + ".xml") == false)
                    {
                        dtSrc.TableName = fname;
                        string strXML = ClsData.GetXML(dtSrc);
                        complete = ClsData.WriteDataXML(fname, strXML);
                        if (complete == true)
                        {
                            tot += dtSrc.Rows.Count;
                            add += dtSrc.Rows.Count;
                            file.Delete();
                        }
                    }
                    else
                    {
                        DataTable dtDest = ClsData.tempTable;
                        if(dtDest.Columns.Count==1)
                        {
                            dtDest = dtSrc.Copy();
                            add = dtSrc.Rows.Count;
                            tot = dtSrc.Rows.Count;
                            ClsData.tempTable = dtDest.Copy();
                            complete = true;
                        }
                        else
                        {
                            tot += dtSrc.Rows.Count;
                            foreach (DataRow drSrc in dtSrc.Rows)
                            {
                                string keyname = dtSrc.Columns[0].ColumnName;
                                string keyval = drSrc[keyname].ToString();
                                DataRow row = dtDest.NewRow();
                                bool bfound = false;
                                foreach (DataRow drDest in dtDest.Select("[" + keyname.Trim() + "]='" + keyval.Trim() + "'"))
                                {
                                    bfound = true;
                                    if (ClsData.CompareRow(drSrc, drDest, dtSrc) == false)
                                    {
                                        row = drDest;
                                        for (int i = 0; i < dtSrc.Columns.Count; i++)
                                        {
                                            row[dtSrc.Columns[i].ColumnName] = drSrc[dtSrc.Columns[i].ColumnName];
                                        }
                                        upd++;
                                    }
                                    complete = true;
                                }
                                if (bfound == false)
                                {
                                    for (int i = 0; i < dtSrc.Columns.Count; i++)
                                    {
                                        row[dtSrc.Columns[i].ColumnName] = drSrc[dtSrc.Columns[i].ColumnName];
                                    }
                                    if (row.RowState == DataRowState.Detached)
                                    {
                                        dtDest.Rows.Add(row);
                                        complete = true;
                                        add++;
                                    }
                                }
                            }
                        }
                        if (complete == true)
                        {
                            file.Delete();
                        }
                    }
                    dtSrc.Dispose();
                }
                msg = "Completed! " + dataname + " files=" + countfile + " rec=" + tot + " add=" + add + ",update=" + upd;
            }
            catch (Exception e)
            {
                msg = "ERROR:" + e.Message;
            }
            return msg;
        }
        public string CloseDataForupdate(string dataname)
        {
            string msg = "Close "+dataname+" Failed!";
            try
            {
                if(ClsData.tempTable.Rows.Count>0)
                {
                    ClsData.tempTable.WriteXml(ClsData.GetPath() + dataname + ".xml");
                    ClsData.tempTable.Dispose();
                    msg = dataname + " Updated!";
                }
                else
                {
                    msg = "No Data To Write!";
                }
            }
            catch (Exception e)
            {
                msg = "ERROR!" + e.Message;
            }
            return msg;
        }
        public string GetDelivery(string datafile)
        {
            DataTable dt = ClsData.GetDataXML(datafile);
            if (dt.Columns.Count==1)
            {
                dt = ClsData.GetDataXML("StructDelivery");
                dt.WriteXml(ClsData.GetPath() + datafile + ".xml");
            }
            return ClsData.GetXML(dt);
        }

    }

}
