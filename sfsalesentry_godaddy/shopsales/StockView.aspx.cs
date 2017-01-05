using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class StockView : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                if (!IsPostBack)
                {
                    ClsData.LoadShop(cboCust, "custname", "oid");
                    ClsData.LoadModel(cboGoods, "Model", "Model");
                    txtYear.Text = DateTime.Now.Year.ToString("0000");
                    txtMonth.Text = DateTime.Now.Month.ToString("00");
                }
            }
            else
            {
                Response.Redirect("menu.aspx");
            }
            ViewState["RefUrl"] = "menu.aspx";
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string tablename = txtYear.Text + txtMonth.Text + "st" + cboCust.SelectedValue.ToString();
                DataTable dt=ClsData.FilterStockTransaction(tablename, "ModelCode", cboGoods.SelectedValue.ToString());
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }            
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsData.NewStockData();
            try
            {
                int i= ClsData.ProcessStockReportSales(cboCust.SelectedValue.ToString(), txtYear.Text + txtMonth.Text);

                string filename = txtYear.Text + txtMonth.Text + "st" + cboCust.SelectedValue.ToString()+ ".xml";
                dt = ClsData.GetStockData(ClsData.GetPath() + filename);
                dt.DefaultView.Sort = "GoodsCode,StockDate,RefNo";
                DataTable tb = dt.DefaultView.ToTable();
                GridView1.DataSource = tb;
                GridView1.DataBind();
                lblMessage.Text = i + " Rows Process!";
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btnOnhand_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsData.NewProductOnhandData();
            try
            {
                string filename = txtYear.Text + txtMonth.Text + "st" + cboCust.SelectedValue.ToString() + ".xml";
                DataTable tb = ClsData.GetStockData(ClsData.GetPath() + filename);
                tb.DefaultView.Sort = "GoodsCode,StockDate,RefNo";
                string code = "";
                int i = 0;
                double qty = 0;
                double amt = 0;
                int icount = tb.Rows.Count;
                DataRow r = dt.NewRow();
                DataTable rs = tb.DefaultView.ToTable();
                dt.Rows.Clear();
                foreach (DataRow dr in rs.Rows)
                {
                    i++;
                    if(code=="")
                    {
                        code = dr["GoodsCode"].ToString().Trim();
                    }
                    if(code!=dr["GoodsCode"].ToString().Trim())
                    {
                        dt.Rows.Add(r);
                        r = dt.NewRow();
                        qty = 0;
                        amt = 0;
                        code = dr["GoodsCode"].ToString().Trim();
                    }
                    try { qty += Convert.ToDouble(dr["StockQty"]); } catch { }
                    try { amt += Convert.ToDouble(dr["StockOut"]); } catch { }
                    r["ModelCode"] = dr["ModelCode"].ToString();
                    r["Color"] = dr["Color"].ToString();
                    r["SizeNo"] = dr["SizeNo"].ToString();
                    r["ProdCatName"] = dr["ProdCatName"].ToString();
                    r["GoodsCode"] = dr["GoodsCode"].ToString();
                    r["ProdQty"] = qty;
                    r["SalesPrice"] = dr["TagPrice"];
                    r["ProdAmount"] = amt;
                    if(i==icount)
                    {
                        dt.Rows.Add(r);
                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}