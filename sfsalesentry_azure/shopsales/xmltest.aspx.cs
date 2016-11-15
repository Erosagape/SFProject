using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace shopsales
{
    public partial class xmltest : System.Web.UI.Page
    {
        static DataTable ds = new DataTable();
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name == "")
            {
                Response.Redirect("Default.aspx", true);
            }
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            txtXML.Text = GetDataFromWeb(txtDataName.Text);
            UpdateGrid();
        }
        protected void UpdateGrid()
        {            
            if(chkJSON.Checked ==true)
            {
                GridView1.DataSource= ClsData.GetDataTableFromJSON(txtXML.Text);
            }
            else
            {
                GridView1.DataSource = ClsData.GetDataTableFromXML(txtXML.Text, txtDataName.Text);
            }
            GridView1.DataBind();
        }
        string GetDataFromWeb(string fname)
        {
            IService svc = new IService();
            string data = "";
            try
            {
                if(chkJSON.Checked ==true )
                {
                    data = svc.GetDataJSON(fname);
                    if(data.Substring(0,1)!="{")
                    {
                        data = @"{"""+ fname+ @""":[{""Table"":''}]}";
                    }
                }
                else
                {
                    data = svc.GetDataXML(fname);
                    if (data.Substring(0, 1) != "<")
                    {
                        data = @"<?xml version=""1.0"" standalone=""yes""?>";
                        data += "<DocumentElement><"+fname+@"><Table/></"+fname+@"></DocumentElement>";
                    }
                }
            }
            catch(Exception ex)
            {
                data = ex.Message;
            }
            return data;
        }
        string UpdateDataToWeb(string xmldata,string fname)
        {
            IService svc = new IService();
            try
            {
                string msg = "";
                if (chkJSON.Checked==true)
                {
                    msg = svc.ProcessDataJSON(fname, xmldata);
                    
                }
                else
                {
                    msg = svc.ProcessDataXML(fname, xmldata);
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        protected void btnSetData_Click(object sender, EventArgs e)
        {
            lblMessage.Text = UpdateDataToWeb(txtXML.Text, txtDataName.Text);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool onread = true;
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (onread)
                {
                    DataView dv =
                         (e.Row.DataItem as DataRowView).DataView;
                    ds = dv.ToTable();
                    onread = false;
                }
        }

        protected void btnUpdateGrid_Click(object sender, EventArgs e)
        {
            if(chkJSON.Checked==true)
            {
                txtXML.Text = ClsData.GetJSONFromTable(ds,txtDataName.Text);
            }
            else
            {
                txtXML.Text = ClsData.GetXML(ds);
            }
        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            UpdateGrid();
        }
    }
}