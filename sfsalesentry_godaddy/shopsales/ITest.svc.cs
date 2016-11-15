using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;

namespace shopsales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class IService : IDataExchange
    {
        public string ShowData(string data)
        {
            return "ข้อมูลที่เราได้รับคือ = "+ data;
        }
        public string GetDataXML(string dataname)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(ClsData.GetPath().ToUpper() + dataname.ToUpper() + ".XML");
                return xml.OuterXml.ToString();
            }
            catch (Exception ex)
            {
                return ClsData.GetXMLMessage("ERROR",ex.Message);
            }
        }
        public string GetDataJSON(string dataname)
        {
            try
            {
                DataTable dt = ClsData.GetDataXML(dataname);
                return ClsData.GetJSONFromTable(dt,dataname);
            }
            catch (Exception ex)
            {
                return ClsData.GetJSONMessage("ERROR",ex.Message);
            }
        }
        public string QueryDataXML(string dataname,string cliteria)
        {                        
            try
            {
                DataSet ds = new DataSet();
                string fname = ClsData.GetPath() + dataname + ".xml";
                ds.ReadXml(fname.ToUpper());
                DataTable dt = ds.Tables[0].Clone();
                DataRow[] dr = ds.Tables[0].Select(cliteria);
                foreach (DataRow r in dr)
                {
                    dt.ImportRow(r);
                }
                StringWriter wr = new StringWriter();
                dt.WriteXml(wr);
                return wr.ToString();
            }
            catch(Exception ex)
            {
                return ClsData.GetXMLMessage("ERROR",ex.Message);
            }
        }
        public string RemoveDataXML(string dataname, string cliteria)
        {
            try
            {
                int i = ClsData.DeleteDataXML(dataname, cliteria);
                return "Completed! "+ i.ToString() +" rows deleted";
            }
            catch (Exception ex)
            {
                return "ERROR"+ ex.Message;
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
                    if ((add + upd) > 0)
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
                string fname = ClsData.GetPath() + dataname + ".xml";
                if (System.IO.File.Exists(fname.ToUpper()) == false)
                {
                    string xmlstr = ClsData.GetXMLFromJSON(jsondata,dataname);
                    ClsData.WriteDataXML(dataname, xmlstr);
                }
                DataSet ds = new DataSet();
                ds.ReadXml(fname.ToUpper());
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
                    if (ClsData.CompareRow(row, rowsrc, dtSrc) == false)
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
                dtDest.WriteXml(fname.ToLower());
                return String.Format("Complete! Add {0} Update {1}", add.ToString(), upd.ToString());
            }
            catch (Exception ex)
            {
                return "Error! " + ex.Message;
            }
        }
        public string GetXMLFileList(string filterstr="*")
        {
            DataTable dt = ClsData.XMLTableData(filterstr);
            return ClsData.GetXML(dt);
        }
        public string Login(string user,string password)
        {
            DataTable dt = ClsData.UserData();
            DataRow dr = ClsData.QueryData(dt, "[id]='" + user + "'");
            string str = "ERROR:Data Not Found";
            try
            {
                if(password.ToUpper().Equals(dr["password"].ToString().ToUpper()))
                {
                    str = "Welcome :" + dr["name"].ToString();
                }
            }
            catch (Exception ex)
            {
                str = "ERROR:" + ex.Message;
            }
            return str;
        }
        public string GetDailyReport(int index,string uid, string ondate)
        {
            string data = "";
            try
            {
                DataTable dt = ClsData.GetDailyReport(ondate).Tables[index];
                string header = "";
                string desc = "";
                int rptindex = 0;
                if(index==0)
                {
                    rptindex = 2;
                    header = uid +"Sales-";
                    desc = "สรุปยอดขายประจำวันที่ ";
                }
                if (index == 1)
                {
                    rptindex = 3;
                    header = uid+"Volume-";
                    desc = "สรุปจำนวนขายประจำวันที่ ";
                }
                data =ClsData.GetReportDataForEmail(dt, rptindex, header, ondate, desc + ondate);
            }
            catch (Exception ex)
            {
                data= "Error: " + ex.Message;
            }
            return data;
        }
        public string SendDailyReport(string ondate,string data)
        {
            string msg = "";
            try
            {
                string mailbody = data+"<br/><br/>";
                DataSet ds = ClsData.GetDailyReport(ondate);
                string report1 = ClsData.GetReportDataForEmail(ds.Tables[0], 2, "DailySales-", ondate, "สรุปยอดขายรายเดือนวันที่ " + ondate);
                string report2 = ClsData.GetReportDataForEmail(ds.Tables[1], 3, "DailyVolume-", ondate, "สรุปจำนวนสินค้าที่ขายวันที่ " + ondate);
                mailbody+= report1 + "<br/>" + report2;                
                msg ="COMPLETE:"+ ClsData.SendMailDailyReport("[AutoMail] รายงานประจำวันที่ " + ondate,mailbody);
            }
            catch (Exception e)
            {
                msg = "ERROR:" + e.Message;
            }
            return msg;
        }
    }
}
