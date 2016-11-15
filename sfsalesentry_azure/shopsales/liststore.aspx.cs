using System;
using System.Data;

namespace shopsales
{
    public partial class liststore : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUsername.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == ""|| cApp.user_role!="0")
            {
                Response.Redirect("index.html", true);
            }
            ViewState["RefUrl"] = "masterfile.aspx";
            if(!IsPostBack)
            {
                LoadGrid();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnAddEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("addstore.aspx", true);
        }
        protected void btnAddShop_Click(object sender, EventArgs e)
        {
            Response.Redirect("addshop.aspx", true);
        }
        protected void GridView2_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                if(Session["gridStore"]!=null)
                {
                    DataTable dt = (DataTable)Session["gridStore"];
                    string qrystring = "?oid=" + dt.Rows[index]["OID"].ToString();
                    Response.Redirect("addstore.aspx" + qrystring);
                }
            }
        }
        protected void GridView1_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            if(Session["gridStore"]!=null)
            {
                DataTable dt = (DataTable)Session["gridStore"];
                DataView dv = dt.DefaultView;
                dv.Sort = e.SortExpression;
                GridView1.DataSource = dv.ToTable();
                GridView1.DataBind();
                Session["gridStore"] = dv.ToTable();
            }
        }
        protected void LoadGrid()
        {
            DataTable dt = ClsData.ShopData();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["gridStore"] = dt;
        }
    }
}