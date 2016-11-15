using System;

namespace shopsales
{
    public partial class masterfile : System.Web.UI.Page
    {
        private string roleid = "";
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUsername.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name=="")
            {
                Response.Redirect("index.html", true);
            }
            ViewState["RefUrl"] = "menu.aspx";
            roleid = cApp.user_role;
            CheckRights();
        }
        protected void btnStore_Click(object sender, EventArgs e)
        {
            Response.Redirect("liststore.aspx",true);
        }
        protected void btnStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("listuser.aspx",true);
        }
        protected void btnProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("listgoods.aspx",true);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnStockReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("StockView.aspx");
        }
        protected void btnGPx_Click(object sender, EventArgs e)
        {
            Response.Redirect("gpx.aspx");
        }
        protected void CheckRights()
        {
            if (roleid == "2")
            {
                btnStore.Enabled = false;
                btnStaff.Enabled = false;
                btnGPx.Enabled = false;
                btnStockReport.Enabled = false;
            }
        }
    }
}