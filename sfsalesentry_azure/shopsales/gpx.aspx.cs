using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class gpx : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUserName.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == ""||cApp.user_role=="1")
            {
                Response.Redirect("index.html", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    ClsData.LoadShopGroup(cboGroupCode, "CustGroupNameTh", "OID");
                    ClsData.LoadSaleType(cboSalesType, "Description", "OID");
                    ClsData.LoadGPType(cboCalType, "Desc", "ID");
                    LoadGrid(cboGroupCode.SelectedValue.ToString());
                    PlaceHolder1.Visible = false;
                }
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="View")
            {
                Int32 index = Convert.ToInt32(e.CommandArgument);
                lblOID.Text = GridView1.Rows[index].Cells[1].Text;
                Loaddata(lblOID.Text);
                PlaceHolder1.Visible = true;
                Panel1.Visible = false;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable("gpx","System")==false)
            {
                ClsData.LockDataTable("gpx", "System");
                lblMessage.Text = SaveData();
                ClsData.UnlockDataTable("gpx", "System");
            }
        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            LoadGrid(cboGroupCode.SelectedValue.ToString());
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearData();
            Panel1.Visible = false;
            PlaceHolder1.Visible = true;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("masterfile.aspx");
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int irow = 0;
            foreach (DataRow shop in ClsData.ShopData(cboGroupCode.SelectedValue.ToString()).Rows)
            {
                foreach (XMLFileList file in ClsData.GetXMLTableList(shop["OID"].ToString() + "_*_*"))
                {
                    bool isUpdate = false;
                    DataTable sales = ClsData.GetSalesData(ClsData.GetPath() + file.filename);
                    foreach (DataRow dr in sales.Rows)
                    {
                        try
                        {
                            double discrate = 0;
                            string salesTypecal = dr["salesType"].ToString();
                            if (salesTypecal=="3")
                            {
                                discrate = Convert.ToDouble(cApp.RPNull(dr["DiscountRate"], 0)) / 100;
                            }
                            if(dr["note"].ToString().IndexOf("ส่วนลดเงินสด")>=0)
                            {
                                discrate = 0;
                                salesTypecal = "1";
                            }
                            Promotion p = ClsData.GetPromotionByDate(dr["SalesDate"].ToString(), salesTypecal, shop["OID"].ToString(), discrate);
                            if (p != null)
                            {
                                string gpx = (Convert.ToDouble(p.GPRate()) * 100).ToString("0.00");
                                string sharediscount = (Convert.ToDouble(p.ShareDiscount) * 100).ToString("0.00");
                                isUpdate = true;
                                dr["ShareDiscount"] = sharediscount;
                                dr["Gpx"] = gpx;
                            }
                            else
                            {
                                isUpdate = true;
                                dr["ShareDiscount"] = 0;
                                dr["Gpx"] = 100-(discrate*100);
                            }
                            irow++;
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                    }
                    if (isUpdate == true)
                    {
                        sales.WriteXml(ClsData.GetPath() + file.filename);
                    }
                }
            }
            lblReady.Text = irow + " updated!";
        }
        protected void btnCal_Click(object sender, EventArgs e)
        {
            Promotion promo = new Promotion();
            promo.CalculateType = Convert.ToInt32(cboCalType.SelectedValue);
            promo.ShareDiscount = Convert.ToDouble(txtShareDiscount.Text);
            promo.DiscountRate = Convert.ToDouble(txtDiscountRate.Text);
            promo.GPX = Convert.ToDouble(txtGPxRate.Text);
            txtGPCal.Text = promo.GPRate().ToString();
        }
        protected void cboGroupCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(cboGroupCode.SelectedValue.ToString());
        }
        protected void Loaddata(string oid)
        {
            DataTable dt = ClsData.QueryData("gpx", "OID='" + oid + "'");
            if (dt.Rows.Count > 0)
            {
                lblShopName.Text = cboGroupCode.SelectedItem.Text;
                txtStartDate.Text = dt.Rows[0]["EffectiveDateFrom"].ToString();
                txtEndDate.Text = dt.Rows[0]["EffectiveDateTo"].ToString();
                txtGPxRate.Text = cApp.CPercent(dt.Rows[0]["GPx"].ToString(), false).ToString("0.00");
                txtShareDiscount.Text = cApp.CPercent(dt.Rows[0]["ShareDiscount"].ToString(), false).ToString("0.00");
                txtDiscountRate.Text = cApp.CPercent(dt.Rows[0]["DiscountRate"].ToString(), false).ToString("0.00");
                cboSalesType.SelectedValue = dt.Rows[0]["SalesType"].ToString();
                cboCalType.SelectedValue = dt.Rows[0]["CalculateType"].ToString();
                chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["Active"].ToString() == "Y");
                txtGPCal.Text = dt.Rows[0]["GPCal"].ToString();
            }
        }
        protected void LoadGrid(string shopgroup)
        {
            DataTable dt = ClsData.QueryData("gpx", "GroupID='" + shopgroup + "' And Active='Y'");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (dt.Rows.Count > 0)
            {
                Panel1.Visible = true;
                PlaceHolder1.Visible = false;
            }
        }
        protected string SaveData()
        {
            string msg = "";
            try
            {
                DataTable dt = ClsData.GetDataXML("gpx");
                string oid = lblOID.Text;
                if (oid == "" || oid == "(New)")
                {
                    oid = ClsData.GetNewOID(dt, "gpx", "OID");
                }
                DataRow dr = ClsData.QueryData(dt, "OID='" + oid + "'");
                dr["OID"] = oid;
                dr["EffectiveDateFrom"] = txtStartDate.Text;
                dr["EffectiveDateTo"] = txtEndDate.Text;
                dr["GroupID"] = cboGroupCode.SelectedValue.ToString();
                dr["SalesType"] = cboSalesType.SelectedValue.ToString();
                dr["SalesTypeName"] = cboSalesType.SelectedItem.Text;
                dr["GPx"] = cApp.CPercent(cApp.RPNull(txtGPxRate.Text, 0)).ToString("0.00");
                dr["ShareDiscount"] = cApp.CPercent(cApp.RPNull(txtShareDiscount.Text, 0)).ToString("0.00");
                dr["DiscountRate"] = cApp.CPercent(cApp.RPNull(txtDiscountRate.Text, 0)).ToString("0.00");
                dr["CalculateType"] = cboCalType.SelectedValue;
                dr["GPCal"] = txtGPCal.Text;
                dr["Active"] = cApp.iif(chkActive.Checked, "Y", "N");
                if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                dt.WriteXml(ClsData.GetPath() + "gpx.xml");
                lblOID.Text = oid;
                msg = "Save Complete! ID=" + oid;
            }
            catch (Exception ex)
            {
                msg = "ERROR: " + ex.Message;
            }
            return msg;
        }
        protected void ClearData()
        {
            lblShopName.Text = cboGroupCode.SelectedItem.Text;
            lblOID.Text = "(New)";
            txtDiscountRate.Text = "0";
            txtShareDiscount.Text = "0";
            txtEndDate.Text = "";
            txtStartDate.Text = "";
            txtGPxRate.Text = "0";
            txtGPCal.Text = "";
            chkActive.Checked = true;
            cboSalesType.SelectedIndex = 0;
            lblMessage.Text = "Ready";
        }
        protected string GetCliteria()
        {
            string str = "";
            str += "[SalesDate]>='" + txtStartDate.Text + "'";
            str += " And [SalesDate]<='" + txtEndDate.Text + "'";
            return str;
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            lblOID.Text = "(New)";
        }
    }
}