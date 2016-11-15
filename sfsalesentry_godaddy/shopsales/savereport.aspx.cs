using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class savereport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice == true)
            {
                LinkButton1.Visible = true;
            }
            else
            {
                LinkButton1.Visible = false;
            }
            this.Title = "รายงานยอดขาย";
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    try
                    {
                        ASPxGridView1.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<br/>ERROR :" + ex.Message);
                        LinkButton1.Visible = false;
                    }
                }
                else
                {
                    Response.Write("<br/>ไม่สามารถเรียกรายงานได้เนื่องจากไม่พบข้อมูล");
                    LinkButton1.Visible = false;
                }
            }
        }
        protected void LoadTable()
        {
            DataTable dt = new DataTable();
            if (Session["rptData"] != null)
            {
                dt = (DataTable)Session["rptData"];
                if (Request.QueryString["reportid"] != null)
                {
                    string fname = Request.QueryString["reportid"].ToString() + ".xml";
                    dt.WriteXml(ClsData.GetPath() + fname);
                    Session["rptData"] = null;
                    Label1.Text = Request.QueryString["reportid"].ToString();
                }
                if (Request.QueryString.Count == 2)
                {
                    if (Request.QueryString["shareLine"] == "1")
                    {
                        Response.Redirect("http://line.me/R/msg/text/?คลิกดูรายงานสรุป -> http://"+ClsData.hostname+"/savereport.aspx?reportid=" + Request.QueryString["reportid"].ToString());
                    }
                }
            }
            else
            {
                if (Request.QueryString["reportid"] != null)
                {
                    string fname = Request.QueryString["reportid"].ToString();
                    if (System.IO.File.Exists(ClsData.GetPath() + fname + ".xml") == true)
                    {
                        dt = ClsData.GetDataXML(fname);
                    }
                    else
                    {
                        Response.Write("<br/>ไม่มีข้อมูล");
                    }
                    Label1.Text = Request.QueryString["reportid"].ToString();
                }
            }
            ASPxGridView1.DataSource = dt;
            if (dt.Columns.IndexOf("Group") < 0)
            {
                ASPxGridView1.KeyFieldName = "Row ID";
            }
            else
            {
                ASPxGridView1.KeyFieldName = "Group";
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["reportid"] != null)
            {
                string fname = Request.QueryString["reportid"].ToString();
                if(System.IO.File.Exists (ClsData.GetPath()+ fname + ".xml") == true)
                {
                    System.IO.File.Delete(ClsData.GetPath() + fname + ".xml");
                }
                Response.Write("<br/>ลบข้อมูลรายงานแล้ว");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://line.me/R/msg/text/?คลิกดูรายงานสรุป -> http://"+ClsData.hostname+"/savereport.aspx?reportid=" + Label1.Text);
        }

        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            LoadTable();
        }

        protected void ASPxGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
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
    }
}