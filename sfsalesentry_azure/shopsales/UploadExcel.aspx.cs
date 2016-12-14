using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using DevExpress.Web;
using System.Web.Services;
namespace shopsales
{    
    public partial class UploadExcel : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ASPxUploadControl1.UploadMode = UploadControlUploadMode.Advanced;
                ASPxUploadControl1.FileSystemSettings.UploadFolder = ClsData.GetPath();
                ASPxUploadControl1.ValidationSettings.AllowedFileExtensions = new string[] { ".xls", ".xlsx", ".xlsm" };
                TextBox2.Text = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmm");
            }
            if (Session["tbExcel"] != null) Label1.Text = Session["tbExcel"].ToString();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(Session ["tbTemp"]!=null)
            {
                DataTable dt = (DataTable)Session["tbTemp"];
                string sessionid=ClsData.CreateSalesData(dt,Session["tbExcel"].ToString());
                if(sessionid!="")
                {
                    Response.Redirect("viewTempData.aspx");
                }
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
    }
}