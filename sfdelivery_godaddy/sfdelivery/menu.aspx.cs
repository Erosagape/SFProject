using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class menu : System.Web.UI.Page
    {
        private string userid = "";
        private string empid = "";
        private string userName = "";
        private string password = "";
        private string groupid = "";
        private string groupname = "";
        private string roleid = "";
        private ClsSessionUser cApp = new ClsSessionUser();
        private bool isPostLogin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            var nvc = Request.Form;
            if (!string.IsNullOrEmpty(nvc["uname"])) //ตรวจสอบว่ามีการส่งค่ามาจากเพจ index.html ไหม
            {
                userid = nvc["uname"]; //รับค่า userid
                if (!string.IsNullOrEmpty(nvc["pword"]))
                {
                    password = nvc["pword"]; //รับค่า password
                }
                isPostLogin = true;
            }
            else
            {
                if (Session["cApp"] != null)
                {
                    cApp = (ClsSessionUser)Session["cApp"];
                }
                LoadSessionValue();
            }
            if(!IsPostBack)
            {
                userName = CheckFullname(userid, password); //ตรวจหาชื่อ user และเช็ครหัสผ่าน
                if (userName == "")
                {//ถ้า logon ไม่ผ่าน ก็เคลียร์ค่าเซสชั่นใหม่หมด
                    ClearVariables();
                    JScript.ShowMessage(this, "ชื่อหรือรหัสผ่านไม่ถูกต้อง");
                    JScript.Redirect(this, "mlogin.html");
                }
                else
                {
                    SaveSessionValue();
                    lblUser.Text = cApp.user_name;
                    selDate.SelectedDate = DateTime.Today;
                    txtDate.Text = selDate.SelectedDate.AddYears(543).ToString("d/M/y");
                }
            }
        }
        void LoadSessionValue()
        {
            empid = cApp.emp_id;
            groupid = cApp.group_id;
            groupname = cApp.group_name;
            userid = cApp.user_id;
            userName = cApp.user_name;
            roleid = cApp.user_role;
            password = cApp.user_password;
        }
        void SaveSessionValue()
        {
            cApp.session_id = GetSession();
            cApp.group_id = groupid;
            cApp.group_name = groupname;
            cApp.user_id = userid;
            cApp.user_name = userName;
            cApp.user_role = roleid;
            cApp.user_password = password;
            cApp.from_mobile = Request.Browser.IsMobileDevice;
            cApp.emp_id = empid;
            if (userName != "" && isPostLogin == true)
            {
                cApp.SaveLogin();
            }
            Session["cApp"] = cApp;
        }
        private string GetSession()
        {
            if (Session["SessionID"] == null)
            {
                Session["SessionID"] = HttpContext.Current.Session.SessionID;
            }
            return Session["SessionID"].ToString();
        }
        private string CheckFullname(string uid, string pwd)
        {
            DataTable dt = ClsData.UserData(true);
            DataRow[] dr = dt.Select(@"id='" + uid + @"'");

            string fname = "";
            roleid = "";
            groupid = "";
            groupname = "";
            empid = "";
            userid = "";
            foreach (DataRow r in dr)
            {
                if (r["password"].ToString().ToUpper() == pwd.ToUpper())
                {
                    userid = r["id"].ToString();
                    empid = r["empid"].ToString();
                    fname = r["id"].ToString() + " (" + r["name"].ToString() + ")";
                    roleid = r["roleid"].ToString();
                    groupid = r["zonecode"].ToString();
                    groupname = r["zonename"].ToString();
                }
            }
            return fname;
        }
        void ClearVariables()
        {
            userid = "";
            userName = "";
            password = "";
            groupid = "";
            groupname = "";
            roleid = "";
            empid = "";
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void LoadData()
        {
            DataTable rs = ClsData.GetDeliveryDataByEmp(cApp.emp_id, "2", "[Mark6] Like '%" + txtDate.Text + "%' And [CustomerName] Like '%" + txtCustomer.Text + "%'");
            string colshow = "ID,Reference,Account,CustomerName,SalesMan,Qty,".ToUpper();
            DataTable dt = rs.Copy();
            for (int i = 0; i < rs.Columns.Count; i++)
            {
                if (colshow.IndexOf(rs.Columns[i].ColumnName.ToUpper()+ ",") < 0)
                {
                    dt.Columns.Remove(rs.Columns[i].ColumnName);
                }
            }
            dt = ClsData.SetCaptionDataDelivery(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="View")
            {
                Int32 index = Convert.ToInt32(e.CommandArgument);
                string refno=GridView1.Rows[index].Cells[1].Text;
                Response.Redirect("mdelivery.aspx?id=" + refno,true);
            }
        }

        protected void selDate_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = selDate.SelectedDate.AddYears(543).ToString("d/M/y");
            LoadData();
        }
    }
}