using System;
using System.Data;
using System.Xml;
namespace shopsales
{
    public partial class listuser : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUsername.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == "" || cApp.user_role!="0")
            {
                Response.Redirect("index.html", true);
            }
            ViewState["RefUrl"] = "masterfile.aspx";
            if(!IsPostBack) LoadGrid();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("adduser.aspx", true);
        }
        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if(e.CommandName=="View")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                if(Session["gridUser"]!=null)
                {
                    DataTable dt = (DataTable)Session["gridUser"];
                    string qrystring = "?oid=" + dt.Rows[index]["id"].ToString();
                    Response.Redirect("adduser.aspx" + qrystring);
                }
            }
        }
        protected void GridView1_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            if(Session["gridUser"]!=null)
            {
                DataTable dt = (DataTable)Session["gridUser"];
                dt.DefaultView.Sort = e.SortExpression;
                GridView1.DataSource = dt.DefaultView.ToTable();
                GridView1.DataBind();
                Session["gridUser"] = dt.DefaultView.ToTable();
            }
        }
        protected void LoadGrid()
        {
            DataTable dt = ClsData.UserData();
            dt.DefaultView.Sort = "role,store,branch";
            GridView1.DataSource = dt.DefaultView.ToTable();
            GridView1.DataBind();
            Session["gridUser"] = dt.DefaultView.ToTable();
        }
    }
}