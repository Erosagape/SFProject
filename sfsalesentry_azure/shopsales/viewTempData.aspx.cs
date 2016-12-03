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
                DataTable dt = (DataTable)Session["tbExcel"];
                Label1.Text = dt.Rows.Count + " Rows";
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        private string GetXMLFileName(DataRow dr)
        {
            string sdate = Convert.ToDateTime(dr["salesDate"]).ToString("yyyyMM");
            string fname = dr["shopno"].ToString() + "_" + sdate + "_" + dr["salesBy"].ToString() + ".xml";
            return fname.ToUpper();
        }
        private DataTable  InsertSalesEntry()
        {
            DataTable sales = ClsData.NewSalesData(new DataSet());
            DataTable tb = (DataTable)Session["tbExcel"];
            int rowid = 0;
            foreach (DataRow dr in tb.Rows)
            {
                try
                {
                    rowid++;
                    string fname = GetXMLFileName(dr);
                    DataTable dt = ClsData.GetSalesData(MapPath("~/" + fname));
                    if (dt.Columns.Count > 0)
                    {                        
                        string sdate = Convert.ToDateTime(dr["salesDate"]).ToString("yyyy-MM-dd");
                        string oid = dr["shopno"] + "_" + sdate.Replace("-","") +"_"+ ClsData.GetGoodsCode(dr["ModelCode"].ToString(), dr["ColorCode"].ToString(), dr["SizeNo"].ToString()) + "_" + rowid; 
                        DataRow r = ClsData.QueryData(dt, "OID='" + oid + "'");
                        r["OID"] = oid;
                        r["salesDate"] = sdate;
                        r["salesType"] = dr["salesType"];
                        r["discountRate"] = dr["discountRate"];
                        string GoodsCode = dr["ModelCode"].ToString() + dr["ColorCode"].ToString() + (Convert.ToInt32(dr["SizeNo"].ToString()) * 10).ToString();
                        var shoe = ClsData.ShoeDataByCode(GoodsCode);
                        r["prodID"] = shoe["OID"];
                        r["prodName"] = shoe["GoodsName"];
                        r["ModelCode"] = dr["ModelCode"];
                        r["ColorCode"] = dr["ColorCode"];
                        r["ColorName"] = shoe["colNameTh"];
                        r["prodCat"] = shoe["ProdCatCode"];
                        r["prodType"] = shoe["STId"];
                        r["prodGroup"] = shoe["ProdGroupName"];
                        r["SizeNo"] = dr["sizeNo"];
                        r["salesQty"] = dr["salesQty"];
                        //r["TagPrice"] = shoe["stdSellPrice"];
                        r["TagPrice"] = dr["TagPrice"];
                        Double rate = Convert.ToDouble(0 + dr["discountRate"].ToString()) / 100;
                        Double baseprice = Convert.ToDouble(0 + r["TagPrice"].ToString());
                        Double discprice = Convert.ToDouble(0 + r["TagPrice"].ToString()) * rate;
                        //txtsalesBuyPrice.Text = (baseprice - discprice).ToString();
                        r["salesPrice"] = (baseprice - discprice).ToString();
                        r["shopName"] = dr["shopname"];
                        r["entryBy"] = dr["salesBy"];
                        r["remark"] = "";
                        r["lastupdate"] = DateTime.Now.AddHours(7).ToString();

                        var sharediscount = "0.00";
                        var gpx = "100";
                        //load gpx And sharediscount
                        double discrate = 0;
                        if (r["salesType"].ToString().Equals("3"))
                        {
                            try { discrate = Convert.ToDouble(dr["discountRate"]) / 100; } catch { }
                        }
                        //find gpx from promotion data
                        Promotion p = ClsData.GetPromotionByDate(sdate, dr["salesType"].ToString(), dr["shopNo"].ToString(), discrate);
                        if (p != null)
                        {
                            gpx = (p.GPRate() * 100).ToString();
                            sharediscount = p.ShareDiscount.ToString();
                        }
                        r["ShareDiscount"] = sharediscount;
                        r["Gpx"] = gpx;
                        r["note"] = "";
                        r["postFlag"] = "N";
                        if (r.RowState == DataRowState.Detached) dt.Rows.Add(r);
                        dt.WriteXml(MapPath("~/" + fname));

                        sales.ImportRow(r);
                    }
                }
                catch
                {
                    
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
    }
}