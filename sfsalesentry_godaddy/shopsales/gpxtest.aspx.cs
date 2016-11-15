using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class gpxtest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadSalesType();
                LoadShop();
            }
        }
        protected void LoadSalesType()
        {
            cboSalesType.DataSource = ClsData.SalesTypeData();
            cboSalesType.DataTextField = "Description";
            cboSalesType.DataValueField = "OID";
            cboSalesType.DataBind();
        }
        protected void LoadShop()
        {
            cboShop.DataSource = ClsData.ShopData();
            cboShop.DataTextField = "CustName";
            cboShop.DataValueField = "OID";
            cboShop.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Promotion data = ClsData.GetPromotionByDate(TextBox1.Text, TextBox2.Text, TextBox3.Text,Convert.ToDouble(TextBox4.Text));
            if(data!=null)
            {
                Label1.Text = "<br/> GPX= " + data.GPX + "<br/>ShareDiscount= " + data.ShareDiscount + "<br/> Discount= " + data.DiscountRate+ "<br/> Rate= " + data.GPRate();
            }
            else
            {
                Label1.Text = "<br/>Not Found";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Promotion data = ClsData.GetPromotionByDate(TextBox1.Text, TextBox2.Text, TextBox3.Text, Convert.ToDouble(TextBox4.Text));
            TextBox6.Text = ClsData.CalculateSalesIn(Convert.ToDouble(TextBox5.Text), data).ToString("0.00");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsData.GetDataXML("gpx");
            if(dt.Columns.IndexOf("CalculateType")<0)
            {
                dt.Columns.Add("CalculateType");
            }
            if (dt.Columns.IndexOf("GPCal") < 0)
            {
                dt.Columns.Add("GPCal");
            }
            Promotion p = new Promotion();
            foreach (DataRow dr in dt.Rows)
            {                
                if(dr["CalculateType"].ToString()=="")
                {
                    if (Convert.ToDouble(dr["ShareDiscount"].ToString()) > 0)
                    {
                        dr["CalculateType"] = "1";
                        p.CalculateType = 1;
                    }
                    else
                    {
                        dr["CalculateType"] = "0";
                        p.CalculateType = 0;
                    }
                }
                else
                {
                    p.CalculateType = Convert.ToInt32(dr["CalculateType"].ToString());
                }
                p.DiscountRate = Convert.ToDouble(dr["DiscountRate"]) * 100;
                p.ShareDiscount = Convert.ToDouble(dr["ShareDiscount"]) * 100;
                p.GPX= Convert.ToDouble(dr["GPX"]) * 100;
                p.SalesType = dr["SalesType"].ToString();
                dr["GPCal"] = p.GPRate();
            }
            dt.WriteXml(ClsData.GetPath() + "gpx.xml");
            Label1.Text = "Complete!";
        }

        protected void cboSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox2.Text = cboSalesType.SelectedValue.ToString();
        }

        protected void cboShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox3.Text = cboShop.SelectedValue.ToString();
        }
    }
}