using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class AddStock : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name != "")
            {
                lblUsername.Text = cApp.user_name;
                if (!IsPostBack)
                {
                    if (cApp.user_role != "0")
                    {
                        LoadShop(cApp.shop_group);
                    }
                    else
                    {
                        LoadShop("");
                    }
                    LoadColor();
                    LoadShoeType();
                    txtYear.Text = DateTime.Now.Year.ToString("0000");
                    txtMonth.Text = DateTime.Now.Month.ToString("00");
                    optDataselect.SelectedValue = "0";
                    ShowGrid(true);
                }
            }
            else
            {
                Response.Redirect("index.html");
            }
            ViewState["RefUrl"] = "menu.aspx";
        }
        protected void LoadShoeType()
        {
            if(cboSTName.DataTextField=="")
            {
                cboSTName.DataSource = ClsData.ShoeTypeData();
                cboSTName.DataTextField = "STName";
                cboSTName.DataValueField = "stID";
                cboSTName.DataBind();
            }
        }
        protected void LoadColor()
        {
            if(cboColor.DataTextField=="")
            {
                cboColor.DataSource = ClsData.ShoeColorData();
                cboColor.DataValueField = "ColNameInit";
                cboColor.DataTextField = "ColTh";
                cboColor.DataBind();
            }
        }
        protected void LoadShop(string groupid)
        {
            if(cboShop.DataTextField=="")
            {
                DataTable dt = ClsData.ShopData(groupid);
                dt.DefaultView.Sort = "custname";
                cboShop.DataSource = dt.DefaultView.ToTable();
                cboShop.DataTextField = "custname";
                cboShop.DataValueField = "oid";
                cboShop.DataBind();
                if (cApp.shop_id != "")
                {
                    cboShop.SelectedValue = cApp.shop_id;
                }
            }
        }
        protected void ClearData()
        {
            txtModelCode.Text = "";
            txtRefNo.Text = "";
            cboColor.SelectedIndex = 0;
            cboSTName.SelectedIndex = 0;
            txtQty.Text = "";
            txtSizeNo.Text = "";
            txtStockDate.Text = "";
            txtTagPrice.Text = "";
            txtTransDate.Text = "";
        }
        protected string getFileName()
        {
            string fname = "";
            try
            {
                fname=txtYear.Text + txtMonth.Text + "st" + cboShop.SelectedValue.ToString();
            }
            catch
            {

            }
            return fname;
        }

        protected void cboShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblShop.Text = getFileName();
            LoadData(lblShop.Text);
            ShowGrid(true);
        }
        protected void LoadData(string fname)
        {

            DataTable dt = ClsData.NewStockData();
            try
            {
                string addWhere = "";
                if(optDataselect.SelectedValue!="2")
                {
                    switch(optDataselect.SelectedValue.ToString())
                    {
                        case "0":
                            addWhere += " AND TransactionState='PCR'";
                            break;
                        case "1":
                            addWhere += " AND TransactionState='STI'";
                            break;
                        default:
                            addWhere += " AND NOT TransactionState='DEL'";
                            break;
                    }
                    
                }
                dt= ClsData.QueryData(fname, "RefNo Like 'STI%' "+ addWhere);
            }
            catch
            {

            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void cboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblColor.Text = cboColor.SelectedValue.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string prodcode = txtModelCode.Text + cboColor.SelectedValue.ToString() + (Convert.ToInt32(txtSizeNo.Text) * 10).ToString();
                string fname = getFileName();
                string yymm = txtYear.Text + txtMonth.Text;
                DataTable dt = ClsData.GetStockData(ClsData.GetPath() + fname + ".xml");
                string oid = "";
                string refno = txtRefNo.Text;
                if (refno == "")
                {
                    oid = ClsData.GetNewOID(dt, fname, "OID");
                    refno = "STI" + yymm + Convert.ToInt32(cboShop.SelectedValue.ToString()).ToString("000") + Convert.ToInt32(oid).ToString("0000");
                }
                else
                {
                    oid = Convert.ToInt32(refno.Substring(refno.Length - 4, 4)).ToString();
                }
                DataRow r = ClsData.QueryData(dt, "RefNo='" + refno + "'");
                if (r["OID"].ToString() == "")
                {
                    r["oid"] = oid;
                    r["GPx"] = 0;
                    r["ShareDiscount"] = 0;
                    r["DiscountRate"] = 0;
                    r["TransactionBy"] = cApp.user_id;
                    r["StockType"] = 1;
                }
                r["RefNo"] = refno;
                r["AcceptCode"] = cApp.user_id;
                r["TransactionDate"] = txtTransDate.Text;
                r["TransactionState"] = "STI";
                r["TransactionType"] = "เติมของ";
                r["StockDate"] = txtStockDate.Text;
                r["ApproveCode"] = cApp.user_id;
                r["ConfirmCode"] = r["ConfirmCode"].ToString();
                r["ModelCode"] = txtModelCode.Text;
                r["Color"] = cboColor.SelectedItem.Text;
                r["SizeNo"] = txtSizeNo.Text;
                r["GoodsCode"] = txtModelCode.Text + cboColor.SelectedValue.ToString() + (Convert.ToInt32(txtSizeNo.Text) * 10).ToString();
                r["ProdCatName"] = cboSTName.SelectedItem.Text;
                r["ProdQty"] = Convert.ToDouble(txtQty.Text);
                r["TagPrice"] = Convert.ToDouble(txtTagPrice.Text);
                r["SalesOut"] = Convert.ToDouble(r["ProdQty"]) * Convert.ToDouble(r["TagPrice"]);
                r["StockQty"] = Convert.ToDouble(r["ProdQty"]) * Convert.ToDouble(r["StockType"]);
                r["Cal"] = 1 - (Convert.ToDouble(r["GPx"]) + ((Convert.ToDouble(r["DiscountRate"]) / 100) * Convert.ToDouble(r["ShareDiscount"])));
                r["SalesIn"] = (Convert.ToDouble(r["ProdQty"]) * Convert.ToDouble(r["TagPrice"])) * Convert.ToDouble(r["Cal"]);
                r["UPriceOut"] =Convert.ToDouble(r["SalesOut"]) /Convert.ToDouble( r["ProdQty"]);
                r["UPriceIn"] = Convert.ToDouble(r["SalesIn"]) / Convert.ToDouble(r["ProdQty"]);
                r["StockIn"] = Convert.ToDouble(r["UPriceIn"]) * Convert.ToDouble(r["StockQty"]);
                r["StockOut"] = Convert.ToDouble(r["UPriceOut"]) * Convert.ToDouble(r["StockQty"]);
                if (r.RowState == DataRowState.Detached) dt.Rows.Add(r);
                dt.WriteXml(ClsData.GetPath() + fname + ".xml");
                lblMessage.Text = "บันทีกข้อมูลเรียบร้อย ->" + refno;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="View")
            {
                Int32 index = Convert.ToInt32(e.CommandArgument);
                string refno = GridView1.Rows[index].Cells[25].Text;
                txtRefNo.Text = refno;
                SearchData(txtRefNo.Text);
                    ShowGrid(false);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadData(getFileName());
            ShowGrid(true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData(txtRefNo.Text);
        }
        protected void SearchData(string refno)
        {
            DataTable dt=ClsData.GetStockData(ClsData.GetPath() + getFileName() + ".xml");
            DataRow dr=ClsData.QueryData(dt,"RefNo Like '"+ refno +"%'");
            txtRefNo.Text = dr["refno"].ToString();
            txtTransDate.Text = dr["TransactionDate"].ToString();
            txtStockDate.Text = dr["StockDate"].ToString();
            txtModelCode.Text = dr["ModelCode"].ToString();
            try { cboColor.SelectedValue = ClsData.GetColorcodeByName(dr["Color"].ToString()); } catch { }
            lblColor.Text = dr["Color"].ToString();
            txtSizeNo.Text = dr["SizeNo"].ToString();
            if(dr["ProdCatName"].ToString()!="")
            {
                cboSTName.SelectedItem.Text = dr["ProdCatName"].ToString();
            }
            txtQty.Text = dr["ProdQty"].ToString();
            txtTagPrice.Text = dr["TagPrice"].ToString();
            dt.Dispose();
        }
        protected void ShowGrid(bool visible)
        {
            Panel1.Visible = visible;
            Panel2.Visible = !visible;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            LoadData(getFileName());
            ShowGrid(true);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearData();
            ShowGrid(false);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }

        protected void lnkCheckPrice_Click(object sender, EventArgs e)
        {
            string goodscode = txtModelCode.Text + cboColor.SelectedValue.ToString() + (Convert.ToInt32(txtSizeNo.Text) * 10).ToString();
            DataRow prod = ClsData.ShoeDataByCode(goodscode);
            if(prod["OID"].ToString()!="")
            {
                cboSTName.SelectedValue = prod["STid"].ToString();
                txtTagPrice.Text = prod["StdSellPrice"].ToString();
            }
        }
    }
}