using System;
using System.Web.UI.WebControls;
using System.Data;
namespace shopsales
{
    public partial class listgoods : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUsername.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == "" || cApp.user_role=="1")
            {
                Response.Redirect("index.html", true);
            }
            ViewState["RefUrl"] = "masterfile.aspx";
            if (cboShoeType.DataTextField == "") ClsData.LoadShoeType(cboShoeType, "STName", "STCode");
            if (cboGroup.DataTextField == "") ClsData.LoadShoeGroup(cboGroup, "GroupName", "OID");
            if (cApp.user_role!="0")
            {
                btnAdd.Visible = false;
            }
            Session["cApp"] = cApp;
            //LoadXml();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("addGoods.aspx", true);
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                if(Session["gridGoods"]!=null)
                {
                    int index = Convert.ToInt32(e.CommandArgument.ToString());
                    DataTable dt = (DataTable)Session["gridGoods"];
                    string qrystring = "?model=" + dt.Rows[index]["รุ่น"].ToString();
                    qrystring += "&color=" + dt.Rows[index]["รหัสสี"].ToString();
                    qrystring += "&size=" + dt.Rows[index]["เบอร์"].ToString();
                    Response.Redirect("addGoods.aspx" + qrystring);
                }
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddModel.aspx");
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (Session["gridGoods"] != null)
            {
                DataTable dt = (DataTable)Session["gridGoods"];
                dt.DefaultView.Sort = e.SortExpression;
                GridView1.DataSource = dt.DefaultView.ToTable();
                GridView1.DataBind();
                Session["gridGoods"] = dt.DefaultView.ToTable();
            }            
        }
        protected string GetCliteria()
        {
            string strCliteria = "";
            if (cboShoeType.SelectedIndex > 0)
            {
                strCliteria += "STCode='" + cboShoeType.SelectedValue.ToString() + "'";
            }
            if (cboGroup.SelectedIndex > 0)
            {
                if (strCliteria != "") strCliteria += " AND ";
                strCliteria += "ProdGroupId='" + cboGroup.SelectedValue.ToString() + "'";
            }
            if (txtModel.Text != "")
            {
                if (strCliteria != "") strCliteria += " AND ";
                strCliteria += "ModelName Like '" + txtModel.Text + "%'";
            }
            if (txtColor.Text != "")
            {
                if (strCliteria != "") strCliteria += " AND ";
                strCliteria += "(ColNameTh='" + txtColor.Text + "' or ColNameEng='" + txtColor.Text + "')";
            }
            if (txtSize.Text != "")
            {
                if (strCliteria != "") strCliteria += " AND ";
                strCliteria += " SizeNo=" + txtSize.Text + "";
            }
            return strCliteria;
        }
        protected void LoadGrid()
        {
            DataTable dt = ClsData.ShoeData();
            DataTable rs = dt.Clone();
            string str = GetCliteria();
            if (str == "")
            {
                dt= ClsData.SetGoodsCaption(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["gridGoods"] =dt;
            }
            else
            {
                try
                {
                    DataRow[] dr = dt.Select(str);
                    foreach (DataRow r in dr)
                    {
                        rs.ImportRow(r);
                    }
                    rs = ClsData.SetGoodsCaption(rs);
                    GridView1.DataSource = rs;
                    GridView1.DataBind();
                    Session["gridGoods"] = rs;

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}