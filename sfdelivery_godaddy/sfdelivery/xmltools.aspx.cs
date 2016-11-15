using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class mainapp : System.Web.UI.Page
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
            Session.Timeout = 60;
            ClsData.SetPath(MapPath("~"));
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
            userName = CheckFullname(userid, password); //ตรวจหาชื่อ user และเช็ครหัสผ่าน
            if (userName == "")            
            {//ถ้า logon ไม่ผ่าน ก็เคลียร์ค่าเซสชั่นใหม่หมด
                ClearVariables();
                PlaceAdmin.Visible = false;
                PlaceUser.Visible = false;
                lblstatus.Text = "ชื่อหรือรหัสผ่านไม่ถูกต้อง";
                btnSignOut.Text = "ลองใหม่อีกครั้ง";
            }
            SaveSessionValue();
            if(cApp.user_id!="")
            {
                if (cApp.user_role != "0")
                {
                    PlaceAdmin.Visible = false;
                }
                if (cApp.user_role == "3")
                {
                    Response.Redirect("delivery.aspx", true);
                }
                lblMessage.Text = cApp.user_name;
                if(cboMonthYear.DataTextField=="")
                {
                    LoadMonthYear();
                }
            }
        }
        private void LoadMonthYear()
        {
            IService sv = new IService();
            DataTable dt=ClsData.GetDataTableFromXML( sv.GetXMLFileList("Delivery*"),"FileList").Copy();
            DataTable rs = dt.Clone();
            string current= DateTime.Now.ToString("yyyyMM");
            int idx = 0;
            int p = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if(dr[0].ToString().IndexOf("Detail")<0)
                {
                    dr[0] = dr[0].ToString().Replace("Delivery", "");
                    dr[0] = dr[0].ToString().Replace(".xml", "");
                    if (dr[0].ToString() == current) p = idx;
                    rs.ImportRow(dr);
                    idx++;
                }
            }
            cboMonthYear.DataSource = rs;
            cboMonthYear.DataValueField = "modifieddate";
            cboMonthYear.DataTextField = "filename";
            cboMonthYear.DataBind();
            cboMonthYear.SelectedIndex = p;
            
            lblDateModified.Text = "Update ล่าสุด " + Convert.ToDateTime(cboMonthYear.SelectedValue.ToString()).ToString(@"yyyy-MM-dd HH:mm");
            
            cApp.working_date = current;
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
                    fname = r["id"].ToString() + " (" + r["name"].ToString()+ ")";
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
            cApp.emp_id = empid;
            if (userName != "" && isPostLogin == true)
            {
                cApp.SaveLogin();
            }
            cApp.from_mobile = Request.Browser.IsMobileDevice;
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

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            ClearVariables();
            SaveSessionValue();
            Response.Redirect("Default.aspx",true);
        }

        protected void cboMonthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDateModified.Text ="Update ล่าสุด "+ Convert.ToDateTime(cboMonthYear.SelectedValue.ToString()).ToString(@"yyyy-MM-dd HH:mm");
            cApp.working_date = cboMonthYear.SelectedItem.Text;
            Session["cApp"] = cApp;
        }
    }
}