using System;
using System.Data;

namespace shopsales
{
    public partial class addstore : System.Web.UI.Page
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
            ViewState["RefUrl"] = "liststore.aspx";
            if (cboOID.DataTextField == "") ClsData.LoadShop(cboOID, "custname", "oid", true);
            if (cboShopName.DataTextField == "") ClsData.LoadShopGroup(cboShopName, "CustGroupNameTh", "OID");
            if (cboProvince.DataTextField == "") ClsData.LoadProvince(cboProvince, "ProvinceName", "ProvinceCode");
            if(!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    string oid = Request.QueryString["oid"].ToString();
                    if (oid != "")
                    {
                        cboOID.SelectedValue = oid;
                        LoadData(oid);
                    }
                }

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ClsData.IsLockedDataTable("Customer", cApp.session_id) == false)
            {
                ClsData.LockDataTable("Customer", cApp.session_id);
                SaveData(cboOID.SelectedValue.ToString());
                ClsData.UnlockDataTable("Customer", cApp.session_id);
            }
            else
            {
                lblMessage.Text = "มีคนกำลังแก้ไขข้อมูลอยู่..กรุณารอสักครู่";
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void cboOID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboOID.SelectedIndex>=0)
            {
                LoadData(cboOID.SelectedValue.ToString());
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("addshop.aspx");
        }
        protected void LoadData(string oid)
        {
            DataTable dt = ClsData.ShopData();
            DataRow dr = ClsData.QueryData(dt, "OID='" + oid + "'");
            if (dr["OID"].ToString() != "")
            {
                txtCustCode.Text = dr["Custcode"].ToString();
                txtCustName.Text = dr["CustName"].ToString();
                cboShopName.SelectedValue = dr["GroupID"].ToString();
                txtShopName.Text = dr["ShopName"].ToString();
                txtBranch.Text = dr["Branch"].ToString();
                txtGPx.Text = (Convert.ToDouble(dr["GPx"].ToString()) * 100).ToString();
                txtShareDiscount.Text = (Convert.ToDouble(dr["ShareDiscount"].ToString()) * 100).ToString();
                cboProvince.SelectedIndex = cboProvince.Items.IndexOf(cboProvince.Items.FindByText(dr["Province"].ToString()));
                txtSalesCode.Text = dr["salesCode"].ToString();
                txtSupcode.Text = dr["SupCode"].ToString();
                txtArea.Text = dr["Area"].ToString();
                txtZone.Text = dr["Zone"].ToString();
                
            }
            else
            {
                txtCustCode.Text = "";
                txtCustName.Text = "";
                cboShopName.SelectedIndex = -1;
                txtBranch.Text = "";
                txtShopName.Text = "";
                txtGPx.Text = "0";
                txtShareDiscount.Text = "0";
                cboProvince.SelectedIndex = -1;
                txtSalesCode.Text = "";
                txtSupcode.Text = "";
                txtArea.Text = "";
                txtZone.Text = "";
            }
            //txtGPx.Text = dr["GPx"].ToString();
        }
        protected void SaveData(string oid)
        {
            try
            {
                DataTable dt = ClsData.ShopData();
                DataRow dr = ClsData.QueryData(dt, "OID='" + oid + "'");
                if (oid == "") oid = ClsData.GetNewOID(dt, "Customer", "OID");
                if (oid != "")
                {
                    dr["OID"] = oid;
                    dr["CustCode"] = txtCustCode.Text;
                    dr["CustName"] = txtCustName.Text;
                    dr["ShopName"] = txtShopName.Text;
                    dr["Branch"] = txtBranch.Text;
                    dr["GroupID"] = cboShopName.SelectedValue.ToString();
                    dr["GPx"] = Convert.ToDouble(txtGPx.Text) / 100;
                    dr["ShareDiscount"] = Convert.ToDouble(txtShareDiscount.Text) / 100;
                    dr["Province"] = cboProvince.SelectedItem.Text;
                    dr["salesCode"] = txtSalesCode.Text;
                    dr["Area"] = txtArea.Text;
                    dr["Zone"] = txtZone.Text;
                    dr["SupCode"] = txtSupcode.Text;
                    //dr["GPx"] = txtGPx.Text;
                    if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                    dt.WriteXml(MapPath("~/Customer.xml"));
                    ClsData.LoadShop(cboOID, "custname", "oid", true);
                    cboOID.SelectedValue = oid;
                    lblMessage.Text = "บันทึกข้อมูลเรียบร้อย";
                }
                else
                {
                    lblMessage.Text = "ไม่สามารถบันทึกข้อมูลในขณะนี้..กรุณาลองใหม่";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            var p = cboProvince.SelectedValue.ToString();
            using (DataTable dt = ClsData.ProvinceData())
            {
                foreach(DataRow dr in dt.Select("ProvinceCode='" + p + "'"))
                {
                    txtArea.Text = (dr["region"].ToString() + "-").Split('-')[0];
                    txtSalesCode.Text = dr["salesCode"].ToString();
                    txtZone.Text = dr["zoneCode"].ToString();
                }
            }
        }
    }
}