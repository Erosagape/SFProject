using System;
using System.Data;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class adduser : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                lblUsername.Text = "Welcome " + cApp.user_name;
            }
            if (cApp.user_name == "" || cApp.user_role != "0")
            {
                Response.Redirect("index.html", true);
            }
            ViewState["RefUrl"] = "listuser.aspx";
            if (cboRole.DataTextField == "") ClsData.LoadRole(cboRole, "roleName", "roleID");
            if (cboShopID.DataTextField == "")
            {
                ClsData.LoadShop(cboShopID, "custname", "oid", true,true);
            }                
            ShowCustGroup();
            if(!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    string oid = Request.QueryString["oid"].ToString();
                    if (oid != "")
                    {
                        txtID.Text = oid;
                        LoadData();
                    }

                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable("listuser",cApp.session_id)==false)
            {
                ClsData.LockDataTable("listuser", cApp.session_id);
                if (SaveData() == true)
                {
                    lblMessage.Text = "Save " + txtID.Text + " Complete";
                }
                ClsData.UnlockDataTable("listuser", cApp.session_id);
            }
            else
            {
                lblMessage.Text = "Data is locked,Please try again later";
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void cboShopID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboShopID.SelectedIndex>=0)
            {
                string str = cboShopID.SelectedItem.Text + " ";
                string shopname = str.Substring(0, str.IndexOf(' '));
                string shopbranch = str.Replace(shopname, "").Trim().ToString();
                txtStore.Text = shopname;
                txtBranch.Text = shopbranch;
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("addstore.aspx");
        }
        protected void LoadData()
        {
            if (SearchData() == false)
            {
                txtName.Text = "";
                txtPassword.Text = "";
                cboRole.SelectedIndex = -1;
                cboShopID.SelectedIndex = -1;
                txtDepartment.Text = "";
                txtStore.Text = "";
                txtBranch.Text = "";
                txtCustGroup.Text = "";
                lblMessage.Text = "Data ID Not found!";
            }
        }
        private void ShowCustGroup()
        {
            if (lblCustGroup.Text == "")
            {
                DataTable dt = ClsData.CustomerGroupData();
                String str = "";
                foreach (DataRow dr in dt.Rows)
                {
                    str += dr[0].ToString() + "=" + dr[2].ToString() + "(" + dr[1].ToString() + ")</br>";
                }
                lblCustGroup.Text = str;
            }
        }
        protected bool SearchData()
        {
            bool found = false;
            DataRow row = ClsData.QueryData(ClsData.UserData(), "id='" + txtID.Text + "'");
            if (row[0].ToString() != "")
            {
                found = true;
                lblMessage.Text = "Found Data ID=" + row["oid"].ToString();
                txtName.Text = row["name"].ToString();
                txtPassword.Text = row["password"].ToString();
                cboRole.SelectedValue = row["role"].ToString();
                cboShopID.SelectedValue = row["shopid"].ToString();
                txtDepartment.Text = row["department"].ToString();
                txtStore.Text = row["store"].ToString();
                txtBranch.Text = row["branch"].ToString();
                //set checkboxlist value
                txtCustGroup.Text = row["shopgroup"].ToString();
            }
            return found;
        }
        private bool SaveData()
        {
            bool success = true;
            try
            {
                DataTable dt = ClsData.UserData();
                DataRow dr = ClsData.QueryData(dt, "id='" + txtID.Text + "'");
                dr["id"] = txtID.Text;
                dr["name"] = txtName.Text;
                dr["password"] = txtPassword.Text;
                dr["role"] = cboRole.SelectedValue.ToString();
                dr["shopid"] = cboShopID.SelectedValue.ToString();
                dr["store"] = txtStore.Text;
                dr["branch"] = txtBranch.Text;
                dr["department"] = txtDepartment.Text;
                dr["shopgroup"] = txtCustGroup.Text;
                if (dr.RowState == DataRowState.Detached)
                {
                    dr["oid"] = ClsData.GetNewOID(dt, "listuser", "oid");
                    dt.Rows.Add(dr);
                }
                dt.WriteXml(MapPath("~/listuser.xml"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                success = false;
            }
            return success;
        }
    }
}