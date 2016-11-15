using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace SfDeliverTracking
{
    class ClsXML : IDisposable 
    {
        private DataSet ds;
        private string err = "";
        private string updatecmd = "";
        private string insertcmd = "";
        public ClsXML()
        {
            ds = new DataSet();
            err = "";
        }
        public ClsXML(string filename)
        {
            ds = new DataSet();
            err = "";
            LoadXML(filename);
        }
        public string GetXML(string fname)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(fname);
            return xml.OuterXml;
        }
        public string GetXML(DataTable dt)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode def = xml.CreateXmlDeclaration("1.0", "UTF-8",null);
            xml.AppendChild(def);
            XmlNode root = xml.CreateElement("NewDataSet");
            foreach(DataRow dr in dt.Rows)
            {
                XmlNode data = xml.CreateElement("Table");
                foreach(DataColumn dc in dt.Columns)
                {
                    XmlNode fld = xml.CreateElement(dc.ColumnName.Trim());
                    XmlText txt = xml.CreateTextNode(dr[dc].ToString().Trim());
                    fld.AppendChild(txt);
                    data.AppendChild(fld);
                }
                root.AppendChild(data);
            }
            xml.AppendChild(root);
            xml.Save("tmp.xml");
            return xml.OuterXml;
                
        }
        public bool NewXML(string fname, string rootname, string tablename)
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
                xml.Save(fname + ".xml");
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }
        public bool LoadXML(string fname, bool AutoCreate = false)
        {
            try
            {
                err = "";
                if (System.IO.File.Exists(fname)==true)
                {
                    ds = new DataSet();
                    ds.ReadXml(fname);
                }
                else
                {
                    if(AutoCreate==true)
                    {
                        if(NewXML(fname,"NewDataSet","Table")==false)
                        {
                            err = "Cannot Create file";
                        }
                        else
                        {
                            ds = new DataSet();
                            ds.ReadXml(fname);
                        }
                    }
                    else
                    {
                        err = "File Not Found";
                    }
                }
            }
            catch(Exception ex)
            {
                err = ex.Message;
            }
            if (err == "") { return true; } else { return false; }
        }
        public bool LoadXMLString(string data)
        {
            try
            {
                err = "";
                XmlTextReader rd = new XmlTextReader(new StringReader(data));
                rd.Read();
                ds = new DataSet();
                ds.ReadXml(rd,XmlReadMode.Auto);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            if (err == "") { return true; } else { return false; }
        }
        public bool SaveXML(string fname)
        {
            try
            {
                ds.WriteXml(fname);
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
        public string Errormessage()
        {
            return err;
        }
        public DataSet Dataset()
        {
            return ds;
        }
        public DataTable Datatable(int index=0)
        {
            return ds.Tables[index];
        }
        public DataTable Datatable(string xmlData)
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            if (LoadXMLString(xmlData) == true)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        public string FindValue(string filter,string fldname)
        {
            DataRow[] dr = ds.Tables[0].Select(filter);
            string val = "";
            foreach(DataRow r in dr)
            {
                val = r[fldname].ToString();
            }
            return val;
        }
        public string GetUpdateCommandData()
        {
            return updatecmd;
        }
        public string GetInsertCommandData()
        {
            return insertcmd;
        }
        public DataTable GetWebData(string fname)
        {
            err = "";
            DataTable dt = new DataTable();
            try
            {
                using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
                {
                    string xmlData = svc.GetDataXML(fname,false);
                    dt = Datatable(xmlData);                    
                    svc.Close();
                }
            }
            catch(Exception ex)
            {
                err = ex.Message;
            }                
            return dt;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    ds.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.                
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ClsXML() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
