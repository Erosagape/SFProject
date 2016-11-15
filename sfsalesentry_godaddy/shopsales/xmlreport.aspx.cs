using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace shopsales
{
    public partial class xmlreport : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        private string rptNo = "";
        private string rptType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Browser.IsMobileDevice==true)
            {
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
            }
            if(Session["cApp"]!=null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblMessage.Text = "ผู้เรียกรายงาน :" + cApp.user_name + " วันที่ " + ClsUtil.GetCurrentTHDate().ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                Response.Redirect("index.html",true);
            }
            if(Session["rptData"]==null)
            {
                Response.Redirect("report.aspx");
            }
            else
            {
                try
                {
                    if(Request.QueryString.Count>0)
                    {
                        rptNo = Request.QueryString[0];
                        rptType = Request.QueryString[1];
                    }
                    labCliteria.Text = Session["rptCliteria"].ToString();
                    ASPxGridView1.DataBind();
                    if (cApp.user_role == "1" || cApp.user_role == "3")
                    {
                        if(rptNo=="0"|| rptNo == "1")
                        {
                            ASPxGridView1.Columns[8].Visible = false; //hidden sales-In column for Aerosoft see only
                            ASPxGridView1.Columns[17].Visible = false; //hidden columns GPX calculate
                        }
                    }
                }
                catch(Exception ex)
                {
                    Session["rptData"] = null;
                    Response.Write("ไม่สามารถเรียกรายงานได้ กรุณารอสักครู่แล้วเรียกใหม่ </br>" +ex.Message);
                }
            }
        }        

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string datafilename = ExportToFile((DataTable)Session["rptData"]);
                if (System.IO.File.Exists(MapPath("~/" + datafilename)) == true)
                {
                    ClsData.Download(datafilename, "~/" + datafilename);
                    System.IO.File.Delete(MapPath("~/" + datafilename));
                }
            }
            catch
            {

            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //send to line app
            Response.Redirect("savereport.aspx?reportid=rpt" + cApp.user_id + ClsUtil.GetCurrentTHDate().ToString("yyyyMMddHHmm") + "&shareLine=1");            
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            //send report to email
            if(txtEMail.Text!="")
            {
                DataTable dt = (DataTable)Session["rptData"];
                lblStatus.Text = ClsData.SendDataToEmail(dt, cApp.user_id, ClsUtil.GetCurrentTHDate().ToString("yyyyMMddHHmm"), Label1.Text, labCliteria.Text,txtEMail.Text);
            }
        }
        protected void ShowReport(DataTable dt)
        {
            ASPxGridView1.DataSource = dt;
            if(dt.Columns.IndexOf("Row ID")>=0)
            {
                ASPxGridView1.KeyFieldName = "Row ID";
            }
            if (dt.Columns.IndexOf("Group") >= 0)
            {
                ASPxGridView1.KeyFieldName = "Group";
            }
            Session["rptData"] = dt;
        }
        protected string ExportToFile(DataTable dt)
        {
            string filename = cApp.user_id + "_" + ClsUtil.GetCurrentTHDate().ToString("yyyyMMddHHmmss") + ".xml";
            try
            {
                ClsData.ExportToXMLFile(MapPath("~/" + filename), dt);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return filename;
        }

        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["rptData"];
            ShowReport(dt);            
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if(Session["reportName"].ToString()!="")
            {
                string qrystr = Session["reportName"].ToString();
                Response.Redirect("testdx.aspx?reportname=" + qrystr, true);
            }
        }

        protected void ASPxGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (rptNo == "4" || rptNo == "5")
            {
                if (e.DataColumn.FieldName.IndexOf("QTY") >= 0)
                {
                    e.Cell.Font.Bold = true;
                    e.Cell.BackColor = Color.LightCyan;
                }
                if (e.VisibleIndex >= 0)
                {
                    if (ASPxGridView1.GetRowValues(e.VisibleIndex, "Group").ToString() == "GRAND TOTAL")
                    {
                        e.Cell.Font.Bold = true;
                        e.Cell.BackColor = Color.LightGreen;
                    }
                }

            }
            if (rptNo == "2" || rptNo == "3")
            {
                if (e.DataColumn.FieldName.ToUpper().IndexOf("GRAND") >= 0)
                {
                    e.Cell.Font.Bold = true;
                    e.Cell.BackColor = Color.LightGreen;
                }
                if (e.DataColumn.FieldName.ToUpper().IndexOf("TOTAL") == 0)
                {
                    e.Cell.Font.Bold = true;
                    e.Cell.BackColor = Color.LightCyan;
                }
                if (ASPxGridView1.GetRowValues(e.VisibleIndex, "Group").ToString().ToUpper() == "GRAND TOTAL")
                {
                    e.Cell.Font.Bold = true;
                    e.Cell.BackColor = Color.LightGreen;
                }
            }
            if (rptNo == "0" || rptNo == "1")
            {
                if (ASPxGridView1.GetRowValues(e.VisibleIndex, "รุ่น").ToString().IndexOf("รวม") >= 0)
                {
                    e.Cell.Font.Bold = true;
                    if (ASPxGridView1.GetRowValues(e.VisibleIndex, "รุ่น").ToString().IndexOf("ทั้งสิ้น") >= 0)
                    {
                        e.Cell.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        if (rptType != "True") e.Cell.BackColor = Color.LightBlue;
                    }
                }
            }
        }

    }
}