using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class Customer : System.Web.UI.Page
    {
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
            else
            {
                if(!IsPostBack)
                {
                    lblUser.Text = cApp.user_name;
                    LoadMonthYear();
                    GridView.DataBind();
                    CreateCommandCol();
                }
                Session["cApp"] = cApp;
            }
        }
        private void RefreshGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Custname"));
            foreach(string data in ClsData.GetCustomerList(cApp.emp_id, cApp.working_date))
            {
                DataRow dr = dt.NewRow();
                dr[0] = data;
                dt.Rows.Add(dr);
            }
            GridView.DataSource = dt;
            GridView.KeyFieldName = "Custname";
        }
        private void LoadMonthYear()
        {
            IService sv = new IService();
            DataTable dt = ClsData.GetDataTableFromXML(sv.GetXMLFileList("Delivery*"), "FileList").Copy();
            DataTable rs = dt.Clone();
            string current = cApp.working_date;
            int idx = 0;
            int p = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].ToString().IndexOf("Detail") < 0)
                {
                    dr[0] = dr[0].ToString().Replace("Delivery", "");
                    dr[0] = dr[0].ToString().Replace(".xml", "");
                    if (dr[0].ToString() == current) p = idx;
                    rs.ImportRow(dr);
                    idx++;
                }
            }
            cboMonthYear.DataSource = rs;
            cboMonthYear.DataValueField = "modifieddate";
            cboMonthYear.DataTextField = "filename";
            cboMonthYear.DataBind();
            cboMonthYear.SelectedIndex = p;
            cApp.working_date = current;
        }
        protected void CreateCommandCol()
        {
            GridViewCommandColumn col = new GridViewCommandColumn("Action");
            col.CustomButtons.Add(CreateViewButton());
            GridView.Columns.Insert(0, col);
        }
        GridViewCommandColumnCustomButton CreateViewButton()
        {
            GridViewCommandColumnCustomButton btn = new GridViewCommandColumnCustomButton();
            btn.ID = "btnView";
            btn.Text = "View";
            btn.Visibility = GridViewCustomButtonVisibility.BrowsableRow;
            return btn;
        }
        protected void GridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

            if (e.ButtonID == "btnView")
            {
                string code = txtCustcode.Text.Split(':')[0].Trim();
                string name = txtCustcode.Text.Split(':')[1].Trim();
                ASPxWebControl.RedirectOnCallback("deliveryhd.aspx?custid=" + code + "&custname=" + name);
            }

        }

        protected void cboMonthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cApp.working_date = cboMonthYear.SelectedItem.Text;
            Session["cApp"] = cApp;
            GridView.DataBind();
            if(GridView.FocusedRowIndex>=0)
            {
                DataRow dr = GridView.GetDataRow(GridView.FocusedRowIndex);
                txtCustcode.Text = dr[0].ToString();
            }
        }

        protected void GridView_DataBinding(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}