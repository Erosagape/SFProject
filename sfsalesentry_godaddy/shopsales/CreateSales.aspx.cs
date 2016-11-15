using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class createsales : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                if (Session["cApp"] != null)
                {
                    cApp = (ClsSessionUser)Session["cApp"];
                }
                if (cApp.user_name == "" || cApp.user_role != "0")
                {
                    Response.Redirect("masterfile.aspx", true);
                }
                if (!IsPostBack)
                {
                    LoadCustomer();
                    txtYear.Text = ClsUtil.GetCurrentTHDate().Year.ToString("0000");
                    txtMonth.Text = ClsUtil.GetCurrentTHDate().Month.ToString("00");
                }
            }
            else
            {
                Response.Redirect("index.html");
            }
            ViewState["RefUrl"] = "masterfile.aspx";
        }
        protected void LoadCustomer()
        {
            ClsData.LoadShop(cboCust, "shopname", "oid");
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsData.GetStockTransaction(cboCust.SelectedValue.ToString(), txtYear.Text + "" + txtMonth.Text,cApp.user_id);
            lblMessage.Text = "Found " + dt.Rows.Count + " Records";
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnUpdateGPX_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ClsData.ProcessUpdateGPX();
        }
        protected void btnCheckData_Click(object sender, EventArgs e)
        {

            lblMessage.Text = ClsData.CheckErrorData();
        }
    }
}