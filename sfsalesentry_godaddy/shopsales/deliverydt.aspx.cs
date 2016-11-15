using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class deliverydt : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
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
            if(Request.QueryString.Count>=1)
            {
                txtID.Text = Request.QueryString["id"].ToString();
                ShowGrid(txtID.Text);
                
            }
        }
        protected void ShowGrid(string id_detail)
        {
            GridView1.DataSource = ClsData.QueryData("DeliveryDetails201601", "ID_Detail like '" + id_detail + "%'");
            GridView1.DataBind();
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowGrid(txtID.Text);
        }
    }
}