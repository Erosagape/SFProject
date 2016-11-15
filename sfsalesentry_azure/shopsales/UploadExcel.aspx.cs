using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using DevExpress.Web;

namespace shopsales
{    
    public partial class UploadExcel : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ASPxUploadControl1.FileSystemSettings.UploadFolder = ClsData.GetPath();
                ASPxUploadControl1.ValidationSettings.AllowedFileExtensions = new string[] { ".xls", ".xlsx", ".xlsm" };
                //ASPxUploadControl1.FileUploadMode = DevExpress.Web.UploadControlFileUploadMode.OnPageLoad;
            }
        }

        protected void ASPxUploadControl1_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            e.UploadedFile.SaveAs(ClsData.GetPath() + @"\\" + e.UploadedFile.FileName,true);
            //ASPxLabel1.Text = e.UploadedFile.FileNameInStorage;
            try
            {
                LoadExcel(ClsData.GetPath() + @"\\" + e.UploadedFile.FileNameInStorage);
            }
            finally
            {
                System.IO.File.Delete(ClsData.GetPath() + @"\\" + e.UploadedFile.FileNameInStorage);
            }
        }
        protected void LoadExcel(string fname)
        {
            CExcel xls = new CExcel(fname, "Template");
            Session["tbExcel"] = xls.QueryDataTable();            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewTempData.aspx");
        }
    }
}