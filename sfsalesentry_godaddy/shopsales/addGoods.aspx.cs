using System;
using System.Data;
namespace shopsales
{
    public partial class addGoods : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        private DataTable dtGoods;
        private DataTable dsColor;
        private DataTable dsType;
        private DataTable dsCategory;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUsername.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == ""||cApp.user_role=="1")
            {
                Response.Redirect("index.html", true);
            }
            ViewState["RefUrl"] = "listgoods.aspx";
            LoadTable();
            if(!IsPostBack)
            {
                if (Request.QueryString.Count == 3)
                {
                    txtModelName.Text = Request.QueryString["model"].ToString();
                    cboColNameTh.SelectedValue = Request.QueryString["color"].ToString();
                    txtSizeNo.Text = Request.QueryString["size"].ToString();
                    SearchData();
                }
            }
            Session["cApp"] = cApp;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string codevalue = ClsData.GetGoodsCode(txtModelName.Text,cboColNameTh.SelectedValue.ToString(),txtSizeNo.Text);
            if (codevalue!="")
            {
                if(ClsData.IsLockedDataTable("Goods",cApp.session_id)==false)
                {
                    ClsData.LockDataTable("Goods", cApp.session_id);
                    dtGoods = ClsData.ShoeData();
                    if (txtOID.Value == "")
                    {
                        txtOID.Value = ClsData.GetNewOID(dtGoods, "Goods", "OID");
                    }
                    if (txtOID.Value != "")
                    {
                        SaveData(codevalue);
                    }
                    else
                    {
                        lblMessage.Text = "Can not save new Data.. Please Check";
                    }
                    ClsData.UnlockDataTable("Goods", cApp.session_id);
                }
                else
                {
                    lblMessage.Text = "Can not save.. data is locked";
                }
            }
            else
            {
                lblMessage.Text = "Data not complete.. Please check";
            }
        }
        protected void LoadTable()
        {
            dsType = ClsData.ShoeTypeData();
            dsCategory = ClsData.ShoeCategoryData();
            dsColor = ClsData.ShoeColorData();
            if (cboSTcode.DataTextField == "") ClsData.LoadCombo(dsType, cboSTcode, "STName", "stID");
            if (cboColNameTh.DataTextField == "") ClsData.LoadCombo(dsColor, cboColNameTh, "ColTh", "ColNameInit");
            if (cboProdCatCode.DataTextField == "") ClsData.LoadCombo(dsCategory, cboProdCatCode, "ProdCatName", "ProdCatID");
            if (cboGroupCode.DataTextField == "") ClsData.LoadShoeGroup(cboGroupCode, "GroupName", "OID");
        }
        protected void LoadSTCode(string stCode)
        {
            DataRow[] drType = dsType.Select("stID='" + stCode + "'");
            DataRow r;
            txtSTCode.Value = "";
            txtSTName.Value = "";
            txtSTName2.Value = "";
            foreach (DataRow row in drType)
            {
                r = row;
                txtSTCode.Value = r["STCode"].ToString();
                txtSTName.Value = r["STName"].ToString();
                txtSTName2.Value = r["STName2"].ToString();
            }
        }
        protected void LoadCategory(string catID)
        {
            DataRow[] drCategory = dsCategory.Select("ProdCatId='" + catID + "'");
            DataRow r;
            txtProdCatCode.Value = "";
            txtProdCatID.Value = "";
            foreach (DataRow row in drCategory)
            {
                r = row;
                txtProdCatCode.Value = r["ProdCatCode"].ToString();
                txtProdCatID.Value = r["ProdCatID"].ToString();
            }
        }
        protected void LoadColor(string colcode)
        {
            DataRow[] drColor = dsColor.Select("ColNameInit='" + colcode + "'");
            DataRow r;
            txtColId.Value = "";
            txtColNameInit.Text = "";
            txtColNameTh.Text = "";
            txtColNameEng.Text = "";
            txtColTypeID.Value = "";
            txtGoodsCode.Text = "";
            txtGoodsName.Text = "";
            foreach (DataRow row in drColor)
            {
                r = row;
                txtColId.Value = r["ColId"].ToString();
                txtColNameInit.Text = r["ColNameInit"].ToString();
                txtColNameTh.Text = r["ColTH"].ToString();
                txtColNameEng.Text = r["ColEN"].ToString();
                txtColTypeID.Value = r["ColTypeID"].ToString();
                txtGoodsCode.Text = ClsData.GetGoodsCode(txtModelName.Text, txtColNameInit.Text, txtSizeNo.Text);
                if (txtGoodsCode.Text != "")
                {
                    txtGoodsName.Text = ClsData.GetGoodsName(txtModelName.Text, cboColNameTh.SelectedItem.Text, txtSizeNo.Text);
                }
            }
        }
        protected void SearchData()
        {
            string codevalue = ClsData.GetGoodsCode(txtModelName.Text, cboColNameTh.SelectedValue.ToString(), txtSizeNo.Text);
            if (codevalue != "")
            {
                dtGoods = ClsData.ShoeData();
                DataRow dr = ClsData.QueryData(dtGoods, "GoodsCode='" + codevalue + "'");
                if (dr["OID"].ToString() != "")
                {
                    cboProdCatCode.SelectedValue = dr["ProdCatId"].ToString();
                    LoadCategory(dr["ProdCatId"].ToString());
                    cboSTcode.SelectedValue = dr["STid"].ToString();
                    LoadSTCode(dr["STId"].ToString());
                    cboColNameTh.SelectedValue = dr["ColNameInit"].ToString();
                    LoadColor(dr["ColNameInit"].ToString());
                    txtOID.Value = dr["OID"].ToString();
                    txtGoodsCode.Text = dr["GoodsCode"].ToString();
                    txtGoodsName.Text = dr["GoodsName"].ToString();
                    txtColNameEng.Text = dr["ColNameEng"].ToString();
                    txtColNameTh.Text = dr["ColNameTh"].ToString();
                    txtColNameInit.Text = dr["ColNameInit"].ToString();
                    txtColTypeID.Value = dr["ColTypeId"].ToString();
                    txtStdSellPrice.Text = dr["StdSellPrice"].ToString();
                    txtProdCatCode.Value = dr["ProdCatCode"].ToString();
                    txtColId.Value = dr["ColId"].ToString();
                    txtSTCode.Value = dr["STCode"].ToString();
                    cboGroupCode.SelectedValue = dr["ProdGroupId"].ToString();
                    txtProdCatID.Value = dr["ProdCatId"].ToString();
                    lblMessage.Text = "Data Searched!";
                }
                else
                {
                    cboProdCatCode.SelectedIndex = -1;
                    txtProdCatCode.Value = "";
                    txtProdCatID.Value = "";
                    cboSTcode.SelectedIndex = -1;
                    txtSTCode.Value = "";
                    txtSTName.Value = "";
                    txtSTName2.Value = "";
                    txtOID.Value = "";
                    txtColNameInit.Text = cboColNameTh.SelectedValue.ToString();
                    LoadColor(txtColNameInit.Text);
                    txtGoodsCode.Text = ClsData.GetGoodsCode(txtModelName.Text, cboColNameTh.SelectedValue.ToString(), txtSizeNo.Text);
                    txtGoodsName.Text = ClsData.GetGoodsName(txtModelName.Text, cboColNameTh.SelectedItem.Text, txtSizeNo.Text);
                    txtStdSellPrice.Text = "";
                    cboGroupCode.SelectedIndex = -1;

                    lblMessage.Text = "Data Not found!";
                }
            }

        }
        protected void SaveData(string code)
        {
            try
            {
                DataRow[] dr = dtGoods.Select("GoodsCode='" + code + "'");
                DataRow r = dtGoods.NewRow();
                foreach (DataRow row in dr)
                {
                    r = row;
                }
                r["OID"] = txtOID.Value;
                r["GoodsCode"] = txtGoodsCode.Text;
                r["GoodsName"] = txtGoodsName.Text;
                r["ModelName"] = txtModelName.Text;
                r["ColId"] = txtColId.Value;
                r["ColNameInit"] = txtColNameInit.Text;
                r["ColNameEng"] = txtColNameEng.Text;
                r["ColNameTh"] = txtColNameTh.Text;
                r["ColTypeId"] = txtColTypeID.Value;
                r["SizeNo"] = txtSizeNo.Text;
                r["StdSellPrice"] = txtStdSellPrice.Text;
                r["ProdStdCost"] = r["StdSellPrice"].ToString();
                r["ProdCatId"] = cboProdCatCode.SelectedValue.ToString();
                r["ProdCatcode"] = txtProdCatCode.Value;
                r["ProdCatName"] = cboProdCatCode.SelectedItem.Text;
                r["STId"] = cboSTcode.SelectedValue.ToString();
                r["STCode"] = txtSTCode.Value;
                r["STName"] = txtSTName.Value;
                r["STName2"] = txtSTName2.Value;
                r["ProdKindId"] = 1;
                r["ProdGroupId"] = cboGroupCode.SelectedValue.ToString();
                r["prodGroupName"] = cboGroupCode.SelectedItem.Text;
                UpdateModel(r);
                if (r.RowState == DataRowState.Detached) dtGoods.Rows.Add(r);
                dtGoods.WriteXml(MapPath("~/Goods.xml"));
                lblMessage.Text = "Save Complete!";
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void UpdateModel(DataRow r)
        {
            DataTable tb = ClsData.ShoeModelData();
            DataRow dr = ClsData.QueryData (tb,"Model='"+r["ModelName"].ToString()+"'");
            if (dr["SMId"].ToString()=="")
            {
                dr["SMId"] = ClsData.GetNewOID(ClsData.ShoeModelData(),"ShoeModel","SMId");
                dr["SMCode"] = ClsData.GetNewOID(ClsData.ShoeModelData(), "ShoeModel", "SMCode");
                dr["Model"] = r["ModelName"].ToString();
                dr["Mold"] = "ไม่ระบุ";
                dr["MoldId"] = "1";
                dr["STId"] = cboSTcode.SelectedValue.ToString();
                dr["ProdCatId"] = cboProdCatCode.SelectedValue.ToString();
                dr["SSId"] = "1";
                dr["MinSize"] = "0";
                dr["MaxSize"] = "0";
                if (dr.RowState == DataRowState.Detached) tb.Rows.Add(dr);
                tb.WriteXml(MapPath("~/ShoeModel.xml"));
            }
            r["SMId"] = dr["SMId"].ToString();
            r["SMCode"] = dr["SMCode"].ToString();
            r["SSId"] = dr["SSId"].ToString();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        protected void cboColNameTh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboColNameTh.SelectedIndex >=0)
            {
                LoadColor( cboColNameTh.SelectedValue.ToString());

            }
        }
        protected void cboSTcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboSTcode.SelectedIndex>=0)
            {
                LoadSTCode(cboSTcode.SelectedValue.ToString());
            }
        }
        protected void cboProdCatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboProdCatCode.SelectedIndex>=0)
            {
                LoadCategory(cboProdCatCode.SelectedValue.ToString());
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string model = txtModelName.Text;
            foreach (XMLFileList file in ClsData.GetXMLTableList("*_*_*"))
            {
                string fname = ClsData.GetPath() + file.filename;
                DataTable dt = ClsData.GetSalesData(fname);
                foreach (DataRow dr in dt.Select("ModelCode like '"+ model+"%'"))
                {
                    dr["prodType"] = cboSTcode.SelectedValue.ToString();
                    dr["prodCat"] = cboProdCatCode.SelectedValue.ToString();
                    dr["prodGroup"] = cboGroupCode.SelectedItem.Text;
                }
                dt.WriteXml(fname);
            }
            lblMessage.Text = "Data Updated!";
        }
    }
}