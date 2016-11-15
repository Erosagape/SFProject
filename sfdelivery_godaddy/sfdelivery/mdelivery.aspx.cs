using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class mdelivery : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            else
            {
                JScript.Redirect(this.Page, "mlogin.html");
            }
            if(cApp.user_name!="")
            {
                if(!IsPostBack)
                {
                    if (Request.QueryString.Count == 1)
                    {
                        string docno = Request.QueryString[0].ToString();
                        LoadDelivery(docno);
                    }
                }
                lblUser.Text = cApp.user_name;
            }
        }
        protected void LoadDelivery(string docno)
        {
            DataTable dt = ClsData.GetDeliveryData("2", "[ID]='" + docno + "'");
            if(dt.Rows.Count>0)
            {
                DataRow dr = dt.Rows[0];
                txtID.Text = dr["ID"].ToString();
                lblMark6.Text = dr["Mark6"].ToString();
                lblDriver.Text = dr["Driver"].ToString();
                lblTransport.Text = dr["TransName"].ToString();
                lblShipTo.Text = dr["ShipTo1"].ToString() + " " + dr["ShipTo2"].ToString();
                lblRemark1.Text = dr["Remark1"].ToString();
                string packqty = "";
                if (dr["Bundle"].ToString() != "") packqty += dr["Bundle"].ToString() + " มัด ";
                if (dr["Box"].ToString() != "") packqty += dr["Box"].ToString() + " กล่อง ";
                if (dr["Sack"].ToString() != "") packqty += dr["Sack"].ToString() + " กระสอบ ";
                lblQty.Text = dr["Qty"].ToString() + " คู่ ";
                if (packqty != "") lblQty.Text += "(" + packqty + ")";
                if(dr["Account"].ToString()!="")
                {
                    optStatus.SelectedIndex = 0;
                }
                else
                {
                    optStatus.SelectedIndex = 1;
                }
                LoadGrid(txtID.Text,dr["filename"].ToString().Replace("Delivery","DeliveryDetails"));
            }                        
        }
        protected void LoadGrid(string docno,string tablename)
        {
            DataTable rs = ClsData.GetDeliveryData(tablename.Replace("Delivery",""), "[ID_Detail] Like '" + docno + "%'");
            DataTable dt = rs.Copy();
            string showcol = "ItemNo,ProductName,DQty,DAmount,".ToUpper();
            for(int i=0;i<rs.Columns.Count;i++)
            {
                if(showcol.IndexOf( rs.Columns[i].ColumnName.ToUpper() + ",")<0)
                {
                    dt.Columns.Remove(rs.Columns[i].ColumnName);
                }
            }
            dt.Columns.Add(new DataColumn("No", Type.GetType("System.Int32"), "Convert(ItemNo,'System.Int32')"));
            dt.DefaultView.Sort = "No";
            dt.Columns.Remove("No");
            dt = ClsData.SetCaptionDataDelivery(dt.DefaultView.ToTable());
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDelivery(txtID.Text);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("menu.aspx");
        }
    }
}