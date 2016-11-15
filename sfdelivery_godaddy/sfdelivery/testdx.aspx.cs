using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
namespace sfdelivery
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString.Count==0)
                {
                    GridView1.DataBind();
                    CreateCommandCol();
                }
                else
                {
                    GridView1.Visible = false;
                    txtCustomerCode.Text = Request.QueryString["custcode"];
                    txtCustomerName.Text = Request.QueryString["custname"];
                }
            }
        }
        protected void CreateCommandCol()
        {
            GridViewCommandColumn col = new GridViewCommandColumn("Action");
            col.CustomButtons.Add(CreateViewButton());
            GridView1.Columns.Insert(0, col);
        }
        GridViewCommandColumnCustomButton CreateViewButton()
        {
            GridViewCommandColumnCustomButton btn = new GridViewCommandColumnCustomButton();
            btn.ID = "btnView";
            btn.Text = "View";
            btn.Visibility = GridViewCustomButtonVisibility.BrowsableRow;
            return btn;
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {
            GridView1.DataSource = ClsData.GetDataXML("customer");
            GridView1.KeyFieldName = "customer";
        }

        protected void GridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            
            if(e.ButtonID=="btnView")
            {
                string custcode = txtCustomerCode.Text;
                string custname = txtCustomerName.Text;
                ASPxWebControl.RedirectOnCallback("testdx.aspx?custcode=" + custcode + "&custname=" + custname);
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("testdx.aspx");
        }

    }
}