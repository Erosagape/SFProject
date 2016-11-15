using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class WebUser : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name == "" || cApp.user_role != "0")
            {
                Response.Redirect("Default.aspx", true);
            }
            else
            {
                lblUser.Text = cApp.user_name;
                if(!IsPostBack)
                {
                    LoadData();
                    LoadRole();
                }
            }
        }
        private void LoadRole()
        {
            cboRole.DataSource = ClsData.RoleData();
            cboRole.DataTextField = "rolename";
            cboRole.DataValueField = "roleid";
            cboRole.DataBind();

        }
        private void LoadData()
        {
            GridView1.DataSource = GetUser();
            GridView1.DataBind();
        }
        private DataTable GetUser()
        {
            DataTable dt = ClsData.GetDataXML("listuser");
            return dt;
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                fldOID.Value = GridView1.Rows[index].Cells[1].Text;
                lblMessage.Text = "Ready";
                txtid.Text = GridView1.Rows[index].Cells[2].Text;
                txtname.Text = GridView1.Rows[index].Cells[3].Text;
                txtpassword.Text= GridView1.Rows[index].Cells[4].Text;
                txtzonecode.Text = GridView1.Rows[index].Cells[5].Text;
                txtzonename.Text = GridView1.Rows[index].Cells[6].Text;
                cboRole.SelectedValue= GridView1.Rows[index].Cells[7].Text;
                txtroleName.Text = GridView1.Rows[index].Cells[8].Text;
                txtempid.Text = GridView1.Rows[index].Cells[9].Text;
                GridView1.Visible = false;
                pControl.Visible = true;                
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            pControl.Visible = false;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "{NEW}";
            fldOID.Value = null;
            txtid.Text = "";
            txtname.Text = "";
            txtpassword.Text = "";
            txtzonecode.Text = "";
            txtzonename.Text = "";
            cboRole.SelectedIndex = -1;
            txtroleName.Text = "";
            txtempid.Text = "";
        }
        private string SaveData()
        {
            string msg = "";
            try
            {
                if (ClsData.IsLockedDataTable("listuser", cApp.user_id) == false)
                {
                    ClsData.LockDataTable("listuser", cApp.user_id);
                    DataTable dt = ClsData.GetDataXML("listuser");
                    if (fldOID.Value == "") fldOID.Value = ClsData.GetNewOID(dt, "listuser", "oid");
                    DataRow dr = dt.NewRow();
                    foreach (DataRow r in dt.Select("OID='" + fldOID.Value.ToString() + "'"))
                    {
                        dr = r;
                    }
                    dr["OID"] = fldOID.Value;
                    dr["id"] = txtid.Text;
                    dr["name"] = txtname.Text;
                    dr["password"] = txtpassword.Text;
                    dr["roleid"] = cboRole.SelectedValue.ToString();
                    dr["roleName"] = txtroleName.Text;
                    dr["zonecode"] = txtzonecode.Text;
                    dr["zonename"] = txtzonename.Text;
                    dr["empid"] = txtempid.Text;
                    if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                    dt.WriteXml(ClsData.GetPath() +"listuser.xml");
                    ClsData.UnlockDataTable("listuser", cApp.user_id);
                    LoadData();
                    msg = "Data Saved!";
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(txtid.Text!="" && txtname.Text!="")
            {
                lblMessage.Text = SaveData();
            }
            else
            {
                lblMessage.Text = "ข้อมูลไม่เพียงพอ";
            }
        }
    }
}