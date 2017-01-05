using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
namespace shopsales
{
    public partial class viewTempData : System.Web.UI.Page
    {
        protected static string statustext = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["tbExcel"] != null)
                {
                    TextBox1.Text = Session["tbExcel"].ToString();
                }
                Loaddata();
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
        
        protected void InsertSalesEntry()
        {
            statustext = "Start Process";
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
            int complete = 0;
            int err = 0;
            if(tb.Rows.Count>0)
            {
                string fname = GetXMLFileName(tb.Rows[0]);
                DataTable dt = ClsData.GetSalesData(ClsData.GetPath() + @"\\" +  fname);
                foreach (DataRow dr in tb.Rows)
                {
                    try
                    {
                        if (dt.Columns.Count > 0)
                        {
                            rowid++;
                            if (fname != GetXMLFileName(dr))
                            {
                                dt.WriteXml(ClsData.GetPath() + @"\\" + fname);
                                fname = GetXMLFileName(dr);
                                dt = ClsData.GetSalesData(ClsData.GetPath()+ @"\\" + fname);
                            }
                            statustext = "Processing " + fname + " (Total=" + rowid + " of " + tb.Rows.Count +")";
                            DataRow r = ClsData.QueryData(dt, "OID='" + dr["oid"].ToString() + "'");
                            if (r["oid"].ToString() != "")
                            {
                                dt.Rows.Remove(r);
                            }
                            dt.ImportRow(dr);
                            sales.ImportRow(dr);
                            complete++;
                            if (rowid==tb.Rows.Count)
                            {
                                dt.WriteXml(ClsData.GetPath() + @"\\" + fname);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        err++;
                        ClsData.SaveLogData("UploadExcel", "SYSTEM", "ADDROW " + rowid, e.Message, DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHMM"), "ERR", "viewTempData.aspx", "InsertSalesEntry");
                    }
                }
            }
            statustext = "Complete! (Total import=" + sales.Rows.Count +" complete=" + complete +" error=" + err +")";
            sales.Dispose();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Enabled = false;
            Timer1.Enabled = true;
            Thread task=new Thread(new ThreadStart(InsertSalesEntry));
            task.Start();            
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
            statustext = "";
        }
        protected void LoadTempData()
        {
            DataTable tb = ClsData.GetDataXML(TextBox1.Text);
            try
            {
                tb.DefaultView.Sort = "oid";
                DataTable dt = tb.DefaultView.ToTable();
                Session["tbTemp"] = dt;
                Label1.Text = dt.Rows.Count + " Rows";
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["tbExcel"] = TextBox1.Text;
            }
            catch
            {
                Session["tbTemp"] = null;
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Button2.Enabled = false;
            Loaddata();
            Button2.Enabled = true;
        }
        protected void Loaddata()
        {
            if (TextBox1.Text != "")
            {
                if(System.IO.File.Exists (ClsData.GetPath() + @"\\" + TextBox1.Text + ".xml"))
                {
                    Button1.Enabled = false;
                    LoadTempData();
                    if (Session["tbTemp"] != null)
                    {
                        PopulateDropDown();
                        Button1.Enabled = true;
                        return;
                    }
                }
            }
            Label1.Text = "File Not Found!";
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if(statustext!="") Label1.Text = statustext;
            if(statustext.Substring(0,1)=="C")
            {
                Button1.Enabled = true;
                statustext = "";
                Timer1.Enabled = false;
            }
        }
    }
}