using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
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
            if(!IsPostBack)
            {
                if (Request.QueryString.Count >= 1)
                {
                    txtID.Text = Request.QueryString["id"].ToString();
                    ShowGrid(txtID.Text);
                    GridView1.DataBind();
                }
            }
            lblUser.Text = cApp.user_name;
            lblFileName.Text = "DeliveryDetails" + cApp.working_date + ".xml";
        }
        protected void ShowGrid(string id_detail)
        {
            DataTable dt = ClsData.GetDeliveryData("Details"+ cApp.working_date, "ID_Detail like '" + id_detail + "%'");
            if(dt.Rows.Count>0)
            {
                dt.Columns.Add(new DataColumn("No", Type.GetType("System.Int32"), "Convert(ItemNo,'System.Int32')"));
                DataView dv = dt.DefaultView;
                dv.Sort = "No ASC";
                dt = dv.ToTable();
                dt.Columns.Remove("No");
                dt = ClsData.SetCaptionDataDelivery(dt);
            }
            GridView1.DataSource = dt;
            GridView1.KeyFieldName = "เลขที่เอกสาร";
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowGrid(txtID.Text);
            GridView1.DataBind();
        }

        protected void GridView_DataBinding(object sender, EventArgs e)
        {
            ShowGrid(txtID.Text);            
        }
    }
}