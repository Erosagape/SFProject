using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class testdx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count >= 1)
            {
                CreateReport();
            }
        }
        protected void CreateReport()
        {
            bool bComplete = false;
            XtraReport rpt = new XtraReport();
            switch (Request.QueryString["reportname"].ToString())
            {
                case "salesreport":
                    if (Session["rptData"] != null)
                    {
                        string str = Session["rptCliteria"].ToString();
                        DataTable dt = (DataTable)Session["rptData"];
                        rpt = new salesreport(dt, str);
                        bComplete = true;
                    }
                    break;
                case "monthreport":
                    if (Session["rptData"] != null)
                    {
                        string str = Session["rptCliteria"].ToString();
                        DataTable dt = (DataTable)Session["rptData"];
                        rpt = CReport.DynamicReport(dt, "รายงานสรุปยอดขาย", str);
                        bComplete = true;
                    }
                    break;
                case "sumreport":
                    if(Session["rptData"]!=null)
                    {
                        string str=Session["rptCliteria"].ToString();
                        DataTable dt = (DataTable)Session["rptData"];
                        rpt = new sumreport(dt, str);
                        bComplete = true;
                    }
                    break;
                default:
                    rpt = CReport.DynamicReport(demoTable(), "รายงานทดสอบ", "แสดงผลทุกรายการ");
                    bComplete = true;
                    break;
            }
            if (bComplete)
            {
                ASPxDocumentViewer1.Report = rpt;
            }
        }
        DataTable demoTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Test";
            dt.Columns.Add(new DataColumn("group",Type.GetType("System.String")));
            for(int i=1;i<=30;i++)
            {
                dt.Columns.Add(new DataColumn("F" + i,Type.GetType("System.Double")));
            }
            for(int j=1;j<=4;j++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "Group" + j;
                for(int k=1;k<=30;k++)
                {
                    dr["F" + k] = 1;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}