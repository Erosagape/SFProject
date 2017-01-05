using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class AddModel : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblMessage.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == "" || cApp.user_role == "1")
            {
                Response.Redirect("index.html", true);
            }
            if (cboOID.DataTextField == "") ClsData.LoadModel(cboOID, "Model", "SMid",true);
            if (cboMold.DataTextField == "") ClsData.LoadMold(cboMold, "MoldName", "MoldId");
            if (cboShoeType.DataTextField == "") ClsData.LoadShoeType(cboShoeType, "STName", "STId");
            if (cboProdCat.DataTextField == "") ClsData.LoadShoeCategory(cboProdCat, "ProdCatName", "ProdCatId");
            if (cboSize.DataTextField == "") ClsData.LoadShoeSize(cboSize, "SSName", "SSId");
        }
        protected void cboOID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboOID.SelectedIndex>=0)
            {
                LoadData("SMId='" + cboOID.SelectedValue.ToString() + "'");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable("ShoeModel","Admin")==false)
            {
                ClsData.LockDataTable("ShoeModel", "Admin");
                lblMessage.Text = SaveData();
                ClsData.UnlockDataTable("ShoeModel", "Admin");
            }
        }
        protected void cboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboSize.SelectedIndex>=0)
            {
                DataRow dr = ClsData.QueryData(ClsData.ShoeSizeData(), "SSid='" + cboSize.SelectedValue.ToString() + "'");
                if(dr["SSId"].ToString()!="")
                {
                    txtMinSize.Text = dr["MinSize"].ToString();
                    txtMaxSize.Text = dr["MaxSize"].ToString();
                }
            }
        }
        protected void btnGenNew_Click(object sender, EventArgs e)
        {
            txtSMCode.Text = ClsData.GetNewOID(ClsData.ShoeModelData(), "ShoeModel", "SMCode");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string moldno = ClsData.GetMoldCode(txtSMName.Text);
            string oid = "";
            if(ClsData.AddNewMold(moldno,ref oid)==true)
            {
                if (oid != "")
                {
                    ClsData.LoadMold(cboMold,"MoldName","MoldId");
                    cboMold.SelectedValue = oid;
                }
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadData("Model='"+txtSMName.Text   +"'");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateGoods.aspx?Model=" + txtSMName.Text);
        }
        protected void LoadData(string cliteria)
        {
            DataRow dr = ClsData.QueryData(ClsData.ShoeModelData(), cliteria);
            txtSMCode.Text = dr["SMCode"].ToString();
            txtSMName.Text = dr["Model"].ToString();
            try { cboMold.SelectedValue = dr["MoldId"].ToString(); } catch { cboMold.SelectedIndex = -1; }
            try { cboShoeType.SelectedValue = dr["STId"].ToString(); } catch { cboShoeType.SelectedIndex = -1; }
            try { cboSize.SelectedValue = dr["SSId"].ToString(); } catch { cboSize.SelectedIndex = -1; }
            try { cboProdCat.SelectedValue = dr["ProdCatId"].ToString(); } catch { cboProdCat.SelectedIndex = -1; }
            txtMinSize.Text = dr["MinSize"].ToString();
            txtMaxSize.Text = dr["MaxSize"].ToString();
            try { cboOID.SelectedValue = dr["SMId"].ToString(); } catch { cboOID.SelectedIndex = -1; }
        }
        private string SaveData()
        {
            string msg = "";
            try
            {
                string oid = cboOID.SelectedValue.ToString();
                if (oid == "")
                {
                    oid = ClsData.QueryData(ClsData.ShoeModelData(), "Model='" + txtSMName.Text.Trim() + "'")["SMId"].ToString();
                    if (oid == "") oid = ClsData.GetNewOID(ClsData.ShoeModelData(), "ShoeModel", "SMid");
                }
                if (oid == "")
                {
                    msg = "ไม่สามารถบันทึกข้อมูลได้";
                }
                else
                {
                    DataTable dt = ClsData.ShoeModelData();
                    DataRow dr = dt.NewRow();
                    DataRow[] rows = dt.Select("SMId='" + oid + "'");
                    foreach (DataRow row in rows)
                    {
                        dr = row;
                    }
                    dr["SMId"] = oid;
                    dr["SMCode"] = txtSMCode.Text;
                    dr["Model"] = txtSMName.Text;
                    dr["Mold"] = cboMold.SelectedItem.Text;
                    dr["MoldId"] = cboMold.SelectedValue.ToString();
                    dr["STId"] = cboShoeType.SelectedValue.ToString();
                    dr["ProdCatId"] = cboProdCat.SelectedValue.ToString();
                    dr["SSId"] = cboSize.SelectedValue.ToString();
                    dr["MinSize"] = txtMinSize.Text;
                    dr["MaxSize"] = txtMaxSize.Text;
                    if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                    dt.WriteXml(MapPath("~/ShoeModel.xml"));
                    ClsData.LoadModel(cboOID, "Model", "SMId");
                    cboOID.SelectedValue = oid;
                    msg = "บันทึกข้อมูลเรียบร้อย";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}