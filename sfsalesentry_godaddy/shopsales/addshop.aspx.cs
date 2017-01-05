using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class addshop : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        private string tbname = "CustomerGroup";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUsername.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == ""||cApp.user_role!="0")
            {
                Response.Redirect("index.html", true);
            }
            ViewState["RefUrl"] = "liststore.aspx";
            if (cboOID.DataTextField == "") ClsData.LoadShopGroup(cboOID, "CustGroupNameTh", "OID", true);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void cboOID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOID.SelectedIndex >= 0)
            {
                LoadData(cboOID.SelectedValue.ToString());
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable(tbname,cApp.session_id)==false)
            {
                ClsData.LockDataTable(tbname, cApp.session_id);
                SaveData(cboOID.SelectedValue.ToString());
                ClsData.UnlockDataTable(tbname, cApp.session_id);
            }
            else
            {
                lblMessage.Text = "คนอื่นกำลังใช้อยู่..กรุณาลองใหม่สักครู่";
            }
        }
        protected void LoadData(string oid)
        {
            DataTable dt = ClsData.CustomerGroupData();
            DataRow dr = ClsData.QueryData(dt, "OID='" + oid + "'");
            txtCustGroupCode.Text = dr["CustGroupCode"].ToString();
            txtCustGroupNameTh.Text = dr["CustGroupNameTh"].ToString();
            txtCustGroupNameEng.Text = dr["CustGroupNameEng"].ToString();
            txtRemark.Text = dr["Remark"].ToString();
            LoadGrid(oid);
            //txtGPx.Text = dr["GPx"].ToString();
        }
        protected void LoadGrid(string oid)
        {
            DataTable dt = ClsData.FilterData("gpx", "GroupID", oid);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void SaveData(string oid)
        {
            try
            {
                DataTable dt = ClsData.CustomerGroupData();
                DataRow dr = dt.NewRow();
                if (oid == "") oid = ClsData.GetNewOID(dt, tbname, "OID");
                if (oid != "")
                {
                    dr = ClsData.QueryData(dt, "OID='" + oid + "'");
                    dr["OID"] = oid;
                    dr["CustGroupCode"] = txtCustGroupCode.Text;
                    dr["CustGroupNameEng"] = txtCustGroupNameEng.Text;
                    dr["CustGroupNameTh"] = txtCustGroupNameTh.Text;
                    dr["Remark"] = txtRemark.Text;
                    //dr["GPx"] = txtGPx.Text;
                    if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                    dt.WriteXml(MapPath("~/CustomerGroup.xml"));
                    ClsData.LoadShopGroup(cboOID, "CustGroupNameTh", "OID", true);
                    cboOID.SelectedValue = oid;
                    lblMessage.Text = "บันทึกข้อมูลเรียบร้อย";
                }
                else
                {
                    lblMessage.Text = "ไม่สามารถติดต่อกับฐานข้อมูลได้..กรุณารอสักครู่";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}