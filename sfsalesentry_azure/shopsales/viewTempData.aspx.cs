using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace shopsales
{
    public partial class viewTempData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["tbExcel"]!=null)
            {
                TextBox1.Text = Session["tbExcel"].ToString();
                if(!IsPostBack)
                {
                    LoadTempData();
                    PopulateDropDown();
                }
            }
        }
        private void PopulateDropDown()
        {
            DropDownList1.Items.Clear();
            DataTable dt = (DataTable)Session["tbTemp"];
            dt.DefaultView.Sort = "ShopName";
            DataTable rs = dt.DefaultView.ToTable();
            string shop = "";
            DropDownList1.Items.Add(shop);
            foreach (DataRow r in rs.Rows)
            {
                if (shop!=r["ShopName"].ToString())
                {
                    shop = r["ShopName"].ToString();
                    DropDownList1.Items.Add(shop);
                }
            }
        }
        private string GetXMLFileName(DataRow dr)
        {
            //string sdate = Convert.ToDateTime(dr["salesDate"]).ToString("yyyyMM");
            var oid = dr["oid"].ToString().Split('_');
            string fname = oid[0] + "_" + oid[1].Substring(0,6) + "_"  + oid[2] +".xml";
            return fname.ToUpper();
        }
        
        private DataTable InsertSalesEntry()
        {
            string dateimport = TextBox1.Text;
            DataTable sales = ClsData.NewSalesData(new DataSet());
            DataTable rs = (DataTable)Session["tbTemp"];
            if(DropDownList1.SelectedValue.ToString()!="")
            {
                rs.DefaultView.RowFilter = "ShopName='" + DropDownList1.SelectedValue.ToString() + "'";
            }
            else
            {
                rs.DefaultView.RowFilter = "";                
            }
            rs.DefaultView.Sort = "oid";
            DataTable tb = rs.DefaultView.ToTable();
            int rowid = 0;
            if(tb.Rows.Count>0)
            {
                string fname = GetXMLFileName(tb.Rows[0]);
                DataTable dt = ClsData.GetSalesData(MapPath("~/" + fname));
                foreach (DataRow dr in tb.Rows)
                {
                    rowid++;
                    try
                    {
                        if (dt.Columns.Count > 0)
                        {
                            if (fname != GetXMLFileName(dr))
                            {
                                dt.WriteXml(MapPath("~/" + fname));
                                fname = GetXMLFileName(dr);
                                dt = ClsData.GetSalesData(MapPath("~/" + fname));
                            }
                            DataRow r = ClsData.QueryData(dt, "OID='" + dr["oid"].ToString() + "'");
                            if (r["oid"].ToString() != "")
                            {
                                dt.Rows.Remove(r);
                            }
                            dt.ImportRow(dr);
                            sales.ImportRow(dr);
                            if (rowid==tb.Rows.Count)
                            {
                                dt.WriteXml(MapPath("~/" + fname));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ClsData.SaveLogData("UploadExcel", "SYSTEM", "ADDROW " + rowid, e.Message, DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHMM"), "ERR", "viewTempData.aspx", "InsertSalesEntry");
                    }
                }
            }
            return sales;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            DataTable dt = InsertSalesEntry();
            Label1.Text = dt.Rows.Count + " Rows Inserted!";
            GridView1.DataSource = dt;
            GridView1.DataBind();
            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.ToString()!="")
            {
                DataTable tb = (DataTable)Session["tbTemp"];
                tb.DefaultView.RowFilter = "ShopName='" + DropDownList1.SelectedValue.ToString() + "'";
                DataTable dt = tb.DefaultView.ToTable();
                Label1.Text = dt.Rows.Count + " Rows Found!";
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                LoadTempData();
            }
        }
        protected void LoadTempData()
        {
            DataTable tb = ClsData.GetDataXML(TextBox1.Text);
            tb.DefaultView.Sort = "oid";
            DataTable dt = tb.DefaultView.ToTable();
            Session["tbTemp"] = dt;
            Label1.Text = dt.Rows.Count + " Rows";
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            LoadTempData();
            PopulateDropDown();
        }
    }
}