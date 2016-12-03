using System;
using System.Data;

namespace shopsales
{
    public partial class editsales : System.Web.UI.Page
    {
        private string salesdate = "";
        private string shopname = "";
        private string shopnote = "";
        private string branch = "";
        private string entryby = "";
        private string shopid = "";
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                entryby = cApp.user_id + " - " + cApp.user_name;
                salesdate = cApp.working_date;
                shopname = cApp.shop_name;
                branch = cApp.shop_branch;
                shopid = cApp.shop_id;
                shopnote = cApp.shop_note;
                lblUsername.Text = "Welcome " + entryby;
                //if (txtSalesDate.Text == "") txtDate.Text = salesdate;
                lblHead.Text = shopname + " สาขา " + branch + " วันที่ " + salesdate;
                lblMessage.Text = "Ready";
                if (txtSaleDate.Text == "")
                {
                    txtSaleDate.Text = salesdate;
                }
            }
            if (cApp.user_name == ""|| cApp.user_role=="1")
            {
                Response.Redirect("index.html", true);
            }
            if(!IsPostBack)
            {
                if (Request.QueryString.Count == 1)
                {
                    string rowid = Request.QueryString["rowid"].ToString();
                    txtOID.Text = rowid;
                    btnSearch_Click(this, new EventArgs());
                }
            }
            ViewState["RefUrl"] = "menu.aspx";
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            txtOID.Text = DeleteData();
            if (txtOID.Text == "")
            {
                lblMessage.Text = "Data Deleted!";
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtOID.Text = SearchData();
            if(txtOID.Text !="")
            {
                lblMessage.Text = "Data Searched!";
            }
            else
            {
                lblMessage.Text = "Data Not Found!";
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string chk = CheckEntry();
            if (chk=="")
            {
                txtOID.Text = SaveData();
                if (txtOID.Text != "")
                {
                    lblMessage.Text = "Data Saved!";
                }
            }
            else
            {
                lblMessage.Text = chk;
            }
        }
        private string CheckEntry()
        {
            string str = "";
            Double p = 0;
            if (Double.TryParse(txtSalesPrice.Text, out p) == false)
            {
                str = "ราคาขายต้องเป็นตัวเลข";
            }
            if (Double.TryParse(txtSalesQty.Text, out p) == false)
            {
                str = "จำนวนต้องเป็นตัวเลข";
            }
            if (Double.TryParse(txtTagPrice.Text, out p) == false)
            {
                str = "ราคาป้ายต้องเป็นตัวเลข";
            }
            if (Double.TryParse(txtSizeNo.Text, out p) == false)
            {
                str = "ชนาดต้องเป็นตัวเลข";
            }
            if (txtSaleType.Text == "2")
            {
                if (Double.TryParse(txtDiscountRate.Text, out p) == false)
                {
                    str = "ส่วนลดต้องเป็นตัวเลข";
                }
                else
                {
                    if (p < 0)
                    {
                        str = "ส่วนลดต้องเป็นตัวเลขมากกว่าหรือเท่ากับ 0";
                    }
                }
            }

            return str;
        }
        private string GetFileName()
        {
            if (txtOID.Text != "")
            {
                return txtOID.Text.Split('_')[0] + "_" + txtOID.Text.Split('_')[1].Substring(0,6) + "_" + cApp.user_id;
            }
            return ClsData.GetSalesFileName(txtOID.Text, cApp.shop_id, txtSaleDate.Text, cApp.user_id);
        }
        private string GetOID()
        {
            if (txtOID.Text == "")
            {
                string colorcode = txtColorCode.Text;
                if (colorcode == "" && txtColorName.Text != "")
                {
                    colorcode = ClsData.QueryData(ClsData.ShoeColorData(), "ColTh='" + txtColorName.Text + "'")["ColNameInit"].ToString();
                }
                return ClsData.GetSalesOID(txtOID.Text, txtModelCode.Text, colorcode, txtSizeNo.Text);
            }
            else
            {
                return txtOID.Text;
            }
        }
        private string DeleteData()
        {
            string filename = ClsData.GetFullPath(GetFileName() + ".xml");
            string oid = GetOID();
            if (System.IO.File.Exists(filename) == true)
            {
                DataTable dt = ClsData.GetSalesData(filename);
                DataRow[] dr = dt.Select("OID='" + oid + "'");
                foreach (DataRow r in dr)
                {
                    dt.Rows.Remove(r);
                }
                dt.WriteXml(filename);
            }
            return SearchData();
        }
        private string SearchData()
        {
            string filename = ClsData.GetFullPath(GetFileName() + ".xml");
            string oid = GetOID();
            if (System.IO.File.Exists(filename) == true)
            {
                DataRow data = ClsData.QueryData(ClsData.GetSalesData(filename), "OID='" + oid + "'");
                oid = data["OID"].ToString();
                txtSaleDate.Text = data["salesDate"].ToString();
                txtSaleType.Text = data["salesType"].ToString();
                txtDiscountRate.Text = data["discountRate"].ToString();
                txtProductID.Text = data["prodID"].ToString();
                txtProductName.Text = data["prodName"].ToString();
                txtModelCode.Text = data["ModelCode"].ToString().ToUpper();
                txtColorCode.Text = data["Colorcode"].ToString().ToUpper();
                txtColorName.Text = data["ColorName"].ToString();
                txtSizeNo.Text = data["sizeNo"].ToString();
                txtSalesQty.Text = data["salesQty"].ToString();
                txtTagPrice.Text = data["TagPrice"].ToString();
                txtSalesPrice.Text = data["salesPrice"].ToString();
                txtRemark.Text = data["remark"].ToString();
                txtprodCat.Text = data["prodCat"].ToString();
                txtprodType.Text = data["prodType"].ToString();
                txtprodGroup.Text = data["prodGroup"].ToString();
                txtNote.Text = data["note"].ToString();
                txtShareDiscount.Text = data["sharediscount"].ToString();
                txtGPX.Text = data["gpx"].ToString();
                //txtShareDiscount.Text = data["sharediscount"].ToString();
            }
            else
            {
                oid = "";
            }
            return oid;
        }
        private string SaveData()
        {
            string filename = ClsData.GetFullPath(GetFileName() + ".xml");
            string oid = GetOID();
            if (System.IO.File.Exists(filename) == true)
            {
                DataTable dt = ClsData.GetSalesData(filename);
                DataRow data = ClsData.QueryData(dt, "OID='" + oid + "'");
                if (data["OID"].ToString() == "")
                {
                    data["OID"] = oid;
                }
                data["salesDate"] = txtSaleDate.Text;
                data["salesType"] = txtSaleType.Text;
                data["discountRate"] = txtDiscountRate.Text;
                data["prodID"] = txtProductID.Text;
                data["prodName"] = txtProductName.Text;
                data["ModelCode"] = txtModelCode.Text;
                data["Colorcode"] = txtColorCode.Text;
                data["ColorName"] = txtColorName.Text;
                data["sizeNo"] = txtSizeNo.Text;
                data["salesQty"] = txtSalesQty.Text;
                data["TagPrice"] = txtTagPrice.Text;
                data["salesPrice"] = txtSalesPrice.Text;
                data["remark"] = txtRemark.Text;
                data["prodType"] = txtprodType.Text;
                data["prodCat"] = txtprodCat.Text;
                data["prodGroup"] = txtprodGroup.Text;
                data["lastupdate"] = DateTime.Now.AddHours(7).ToString();
                data["note"] = txtNote.Text;
                data["sharediscount"] = txtShareDiscount.Text;
                data["gpx"] = txtGPX.Text;
                data["entryby"] = cApp.user_id + " - " + cApp.user_name;
                data["postflag"] = "N";
                if (data.RowState == DataRowState.Detached) dt.Rows.Add(data);
                dt.WriteXml(filename);
            }
            return SearchData();

        }
    }
}