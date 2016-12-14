using System;
using System.IO;
using System.Data;

namespace shopsales
{
    public partial class fileadmin : System.Web.UI.Page
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
                Response.Redirect("index.html", true);
            }
            if (!IsPostBack)
            {
                txtFilter.Text = "*";
                ShowXMLFiles(txtFilter.Text,DropDownList1.SelectedValue.ToString());
            }
        }
        void ShowXMLFiles(string filter,string orderby)
        {
            DataTable dt = ClsData.XMLTableData(filter,orderby);
            ListBox1.DataSource = dt;
            ListBox1.DataTextField = "filename";
            ListBox1.DataValueField = "filename";
            ListBox1.DataBind();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ListBox1.Text != "")
            {
                DataSet ds = new DataSet();
                ds.ReadXml(MapPath("~/" + ListBox1.Text));
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ListBox1.Text != "")
            {
                System.IO.File.Delete(MapPath("~/" + ListBox1.Text));
                ShowXMLFiles(txtFilter.Text,DropDownList1.SelectedValue.ToString());
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsData.GetLockDataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            ClsData.ClearLockTable();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            ShowXMLFiles(txtFilter.Text,DropDownList1.SelectedValue.ToString());
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string filename = ListBox1.Text;
            ClsData.Download(filename , "~/" + ListBox1.Text);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = ClsData.GetDataXML("LoginHistory");
            GridView1.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowXMLFiles(txtFilter.Text, DropDownList1.SelectedValue.ToString());
        }
    }
}