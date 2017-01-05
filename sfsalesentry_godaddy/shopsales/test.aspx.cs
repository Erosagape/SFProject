using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TextBox1.Text = ClsData.GetJSONFromXML("LoginHistory");
                IService svc = new IService();
                //Label1.Text= svc.Login("wm001", "4780");
                Label1.Text = TimeZone.CurrentTimeZone.StandardName + " " + TimeZone.CurrentTimeZone.ToUniversalTime(DateTime.Now) + " (" + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now) + ") -> " + TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ClsData.GetJSONFromXMLString(TextBox1.Text);
            GridView1.DataSource = ClsData.GetDataTableFromJSON(TextBox1.Text);
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ClsData.GetXMLFromJSON(TextBox1.Text,"LoginHistory");
            GridView1.DataSource = ClsData.GetDataTableFromXML(TextBox1.Text,"LoginHistory");
            GridView1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            IService svc = new IService();
            Label1.Text =svc.SendDailyReport(DateTime.Today.ToString ("yyyy-MM-dd"),"เรียนผู้เกี่ยวข้อง");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string msg = "";
            //clear all reports shared files
            foreach(XMLFileList lst in ClsData.GetXMLTableList("rpt*"))
            {
                System.IO.File.Delete(ClsData.GetPath() + lst.filename);
                msg += lst.filename + " Deleted" + Environment.NewLine;
            }
            //clear all reports shared files
            foreach (System.Data.DataRow dr in ClsData.UserData().Rows)
            {
                foreach (XMLFileList lst in ClsData.GetXMLTableList(dr["id"].ToString() + "_*"))
                {
                    System.IO.File.Delete(ClsData.GetPath() + lst.filename);
                    msg += lst.filename+ " Deleted"+ Environment.NewLine;
                }
            }
            TextBox1.Text = msg;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            IService svc = new IService();
            string ondate = DateTime.Today.ToString("yyyy-MM-dd");
            TextBox1.Text = svc.GetDailyReport(0, "Daily", ondate);
            TextBox1.Text += Environment.NewLine+ svc.GetDailyReport(1, "Daily", ondate); 
        }
/*
        protected void Button6_Click(object sender, EventArgs e)
        {
            List<XMLFileList> lists = ClsData.GetXMLTableList("*20161*adm002*");
            DataTable tmp = ClsData.NewSalesData(new DataSet());
            foreach (XMLFileList file in lists)
            {
                DataRow[] rows = ClsData.GetDataXML(file.filename.Replace(".xml",""), "OID Not Like '%adm002%'");
                foreach (DataRow row in rows)
                {                    
                    tmp.ImportRow(row);
                }
            }
            Label1.Text ="Found ="+ tmp.Rows.Count.ToString();
            GridView1.DataSource = tmp;
            GridView1.DataBind();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            List<XMLFileList> lists = ClsData.GetXMLTableList("*20161*adm002*");
            DataTable tmp = ClsData.NewSalesData(new DataSet());
            foreach (XMLFileList file in lists)
            {
                Label1.Text += "<br/>Deleted=" + ClsData.DeleteDataXML(file.filename.Replace(".xml", ""), "OID Not Like '%adm002%'");

            }
        }
*/
        protected void Button8_Click(object sender, EventArgs e)
        {
            string msg = "";
            foreach (XMLFileList lst in ClsData.GetXMLTableList("*_*_*"))
            {
                if (lst.filesize==54)
                {
                    System.IO.File.Delete(ClsData.GetPath() + lst.filename);
                    msg += lst.filename + " Deleted" + "<br/>";
                }
            }
            Label1.Text = msg;
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            CExcel xls = new CExcel(ClsData.GetPath() + "StockBalance.xls", "Stock");
            DataTable tb=xls.QueryDataTable("select * from [Stock$]");
            DataTable dt = new DataTable();
            dt.TableName = "Stock";
            dt.Columns.Add("Model", Type.GetType("System.String"));
            dt.Columns.Add("ColorCode", Type.GetType("System.String"));
            dt.Columns.Add("ColorName", Type.GetType("System.String"));
            dt.Columns.Add("Size", Type.GetType("System.String"));
            dt.Columns.Add("Qty", Type.GetType("System.Double"));
            dt.Columns.Add("Unit", Type.GetType("System.String"));
            dt.Columns.Add("Bldg", Type.GetType("System.String"));
            dt.Columns.Add("Floor", Type.GetType("System.String"));
            dt.Columns.Add("Location", Type.GetType("System.String"));
            int row = 0;
            foreach (DataRow dr in tb.Rows)
            {
                for (int i = 0; i <= 10; i++)
                {
                    row++;
                    try
                    {
                        DataRow r = dt.NewRow();
                        r["Model"] = dr["Model"].ToString();
                        r["ColorCode"] = dr["Code"].ToString();
                        r["ColorName"] = dr["ColorName"].ToString();
                        var sz = Convert.ToDouble(dr["SizeBegin"].ToString()) + (Convert.ToDouble(dr["Step"].ToString()) * i);
                        r["Size"] = sz.ToString();
                        double qty = 0;
                        double.TryParse(dr["S" + i.ToString()].ToString(), out qty);
                        r["Qty"] = qty;
                        r["Unit"] = dr["Packing"].ToString();
                        r["Bldg"] = dr["bld"].ToString();
                        r["Floor"] = dr["floor"].ToString();
                        r["Location"] = dr["loc"].ToString();
                        dt.Rows.Add(r);
                    }
                    catch(Exception ex)
                    {
                        Response.Write("ROW=" + row + " ERR="+ ex.Message);
                    }
                }
            }
            dt.WriteXml(ClsData.GetPath() + "temp.xml");
            Response.Write("Total row = " + row);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string msg = "Total Files Process {0}";
            string oid = "";
            string area = "";
            string zone = "";
            string sup = "";
            string sale = "";
            string countertype = "เค้าท์เตอร์ปกติ";
            int fcount = 0;
            foreach (XMLFileList lst in ClsData.GetXMLTableList("*_*_*"))
            {
                if (oid != lst.filename.Split('_')[0])
                {
                    oid = lst.filename.Split('_')[0];
                    area = "";
                    zone = "";
                    sup = "";
                    sale = "";
                    
                    DataRow[] s = ClsData.ShopData().Select("OID='" + oid + "'");
                    foreach(DataRow r in s)
                    {
                        area = r["area"].ToString();
                        zone = r["zone"].ToString();
                        sale = r["salescode"].ToString();
                        sup = r["supcode"].ToString();
                    }
                }
                string fname = lst.filename.Replace(".xml", "");
                DataTable dt = ClsData.GetSalesData(ClsData.GetPath() + fname+ ".xml");
                if( dt.Rows.Count>0)
                {
                    if (dt.Columns.IndexOf("Area") < 0) dt.Columns.Add(new DataColumn("Area") { DefaultValue = area });
                    if (dt.Columns.IndexOf("zoneCode") < 0) dt.Columns.Add(new DataColumn("zoneCode") { DefaultValue = zone });
                    if (dt.Columns.IndexOf("salesCode") < 0) dt.Columns.Add(new DataColumn("salesCode") { DefaultValue = sale });
                    if (dt.Columns.IndexOf("supCode") < 0) dt.Columns.Add(new DataColumn("supCode") { DefaultValue = sup });
                    if (dt.Columns.IndexOf("CounterType") < 0) dt.Columns.Add(new DataColumn("CounterTYpe") { DefaultValue = countertype });
                }                
                dt.WriteXml(ClsData.GetPath() + fname + ".xml");
                fcount++;
            }
            Label1.Text = string.Format(msg,fcount);
        }
    }
}