using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SfWebBackoffice
{
    class ClsXML : IDisposable
    {
        private DataSet ds;
        private string err = "";
        private string updatecmd = "";
        private string insertcmd = "";
        private string path = Program.StartupPath + "\\Data\\";
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
            StringWriter wr = new StringWriter();
            dt.WriteXml(wr);
            return wr.ToString();
        }
        public bool LoadXML(string fname)
        {
            try
            {
                err = "";
                ds = new DataSet();
                ds.ReadXml(fname);
            }
            catch (Exception ex)
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
                ds.ReadXml(rd, XmlReadMode.Auto);
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
        public DataTable Datatable(int index = 0)
        {
            try
            {
                return ds.Tables[index];
            }
            catch
            {
                return new DataTable();
            }

        }
        public DataTable Datatable(string xmlData)
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            if (LoadXMLString(xmlData) == true)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        public string FindValue(string filter, string fldname)
        {
            string val = "";
            try
            {
                DataRow[] dr = ds.Tables[0].Select(filter);
                foreach (DataRow r in dr)
                {
                    val = r[fldname].ToString();
                }
            }
            catch
            {
                val = "";
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
        public void Dispose()
        {
            ds.Dispose();
            //throw new NotImplementedException();
        }
    }
}
