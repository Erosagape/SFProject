using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using DevExpress.Web;
using System.Threading;
namespace shopsales
{    
    public partial class UploadExcel : System.Web.UI.Page
    {
        protected static string statustext = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ASPxUploadControl1.UploadMode = UploadControlUploadMode.Advanced;
                ASPxUploadControl1.FileSystemSettings.UploadFolder = ClsData.GetPath();
                ASPxUploadControl1.ValidationSettings.AllowedFileExtensions = new string[] { ".xls", ".xlsx", ".xlsm" };
                TextBox2.Text = "";
                Session["tbTemp"] = null;
                Session["tbExcel"] = null;
                Button1.Enabled = false;
                if (cboCounter.Items.Count == 0) ClsData.LoadCounterType(cboCounter, "CounterName", "OID", false);
            }
            //if (Session["tbExcel"] != null) TextBox2.Text = Session["tbExcel"].ToString();
        }

        protected void ASPxUploadControl1_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            e.UploadedFile.SaveAs(ClsData.GetPath() + @"\\" + e.UploadedFile.FileName,true);
            //e.CallbackData = "";
            //ASPxLabel1.Text = e.UploadedFile.FileNameInStorage;
            try
            {
                Session["tbExcel"] = LoadExcel(ClsData.GetPath() + @"\\" + e.UploadedFile.FileNameInStorage);
                e.CallbackData = Session["tbExcel"].ToString();
                //e.CallbackData = "redirect|viewTempData.aspx";
            }
            finally
            {
                System.IO.File.Delete(ClsData.GetPath() + @"\\" + e.UploadedFile.FileNameInStorage);
            }
        }
        protected string LoadExcel(string fname)
        {
            CExcel xls = new CExcel(fname, TextBox1.Text);
            DataTable dt = xls.QueryDataTable(SQLSalesData(TextBox1.Text));
            Session["tbTemp"] = dt;
            return "rpt_import" + DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmm");
        }
        protected void CreateSalesData()
        {            
            int i = 0;
            try
            {
                statustext = "Start Process";
                DataTable dt = (DataTable)Session["tbTemp"];
                string sessionid = Session["tbExcel"].ToString();
                DataTable shoes = ClsData.ShoeData();
                DataTable shops = ClsData.ShopData();
                //dt.DefaultView.Sort = "shopno,salesDate,modelcode,colorcode,sizeno";
                DataTable tb = ClsData.NewSalesData(new DataSet());
                foreach (DataRow dr in dt.Rows)
                {
                    i++;

                    DataRow r = tb.NewRow();
                    string sdate = Convert.ToDateTime(dr["salesDate"]).ToString("yyyy-MM-dd");
                    string oid = dr["shopno"] + "_" + sdate.Replace("-", "") + "_" + dr["SalesBy"].ToString() + "_" + ClsData.GetGoodsCode(dr["ModelCode"].ToString(), dr["ColorCode"].ToString(), dr["SizeNo"].ToString()) + "_" + dr["salesType"].ToString() + "_" + i;
                    r["OID"] = oid;
                    r["salesDate"] = sdate;
                    r["salesType"] = dr["salesType"];
                    r["discountRate"] = dr["discountRate"];
                    string GoodsCode = dr["ModelCode"].ToString() + dr["ColorCode"].ToString() + (Convert.ToInt32(dr["SizeNo"].ToString()) * 10).ToString();
                    var shoe = ClsData.QueryData(shoes, "GoodsCode='" + GoodsCode + "'");
                    if (shoe["oid"].ToString() != "")
                    {
                        r["prodID"] = shoe["OID"];
                        r["prodName"] = shoe["GoodsName"];
                        r["ColorName"] = shoe["colNameTh"];
                        r["prodCat"] = shoe["ProdCatCode"];
                        r["prodType"] = shoe["STId"];
                        r["prodGroup"] = shoe["ProdGroupName"];
                    }
                    else
                    {
                        r["prodID"] = "0";
                        r["prodName"] = dr["ModelCode"].ToString() + " " + dr["ColorNameTH"].ToString() + " " + dr["SizeNo"].ToString();
                        r["ColorName"] = dr["ColorNameTh"].ToString();
                        r["prodCat"] = "";
                        r["prodType"] = "";
                        r["prodGroup"] = "";
                    }
                    r["ModelCode"] = dr["ModelCode"];
                    r["ColorCode"] = dr["ColorCode"];
                    r["SizeNo"] = dr["sizeNo"];
                    r["salesQty"] = dr["salesQty"];

                    r["TagPrice"] = dr["TagPrice"];
                    Double rate = Convert.ToDouble(0 + dr["discountRate"].ToString()) / 100;
                    Double baseprice = Convert.ToDouble(0 + r["TagPrice"].ToString());
                    Double discprice = Convert.ToDouble(0 + r["TagPrice"].ToString()) * rate;
                    r["salesPrice"] = (baseprice - discprice).ToString();

                    r["Area"] = "";
                    r["salesCode"] = "";
                    r["supCode"] = "";
                    r["zoneCode"] = "";
                    foreach (DataRow s in shops.Select("OID='" + dr["shopno"] + "'"))
                    {
                        r["Area"] = s["Area"].ToString();
                        r["salesCode"] = s["salesCode"].ToString();
                        r["supCode"] = s["supCode"].ToString();
                        r["zoneCode"] = s["zone"].ToString();
                    }
                    r["CounterType"] = cboCounter.SelectedItem.Text;
                    r["shopName"] = dr["shopname"];
                    r["entryBy"] = dr["salesBy"];
                    r["remark"] = r["remark"].ToString();
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
                    r["note"] = sessionid;
                    r["postFlag"] = "N";
                    tb.Rows.Add(r);
                    statustext = "Writing row " + i + " of " + dt.Rows.Count;
                }
                tb.WriteXml(ClsData.GetPath() + "\\" + sessionid + ".xml");
                statustext = "Complete!";
            }
            catch (Exception e)
            {
                ClsData.SaveLogData("UploadExcel", "SYSTEM", "ADDROW " + i, e.Message, DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHMM"), "ERR", "ClsData", "CreateSalesData");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
            if (Session ["tbTemp"]!=null && Session["tbExcel"].ToString()!="")
            {
                Button1.Enabled = false;
                Thread task=new Thread(new ThreadStart(CreateSalesData));
                task.Start();
            }
            else
            {
                Response.Redirect("viewTempData.aspx");
            }
        }
        private string SQLSalesData(string sname)
        {
            return @"select 
            salesDate,salesBy,salesType,SaleTypeName,discountRate,ModelCode,Colorcode,ColorNameTH,sizeNo,ShopID,ShopNo,ShopName
            ,sum(salesQty) as salesQty,Max(TagPrice) as TagPrice
            from [" + sname + @"$]
            group by salesDate,salesBy,salesType,SaleTypeName,discountRate,ModelCode,Colorcode,ColorNameTH,sizeNo,ShopID,ShopNo,ShopName
            ";
        }
        protected void ASPxUploadControl1_FilesUploadComplete(object sender, FilesUploadCompleteEventArgs e)
        {
            
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if(statustext!="") Label1.Text = statustext;
            if(Label1.Text=="Complete!")
            {
                Response.Redirect("viewTempData.aspx");
            }
        }
    }
}