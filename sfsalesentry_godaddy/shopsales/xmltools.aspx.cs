using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class mainapp : System.Web.UI.Page
    {
        private string userid = "";
        private string userName = "";
        private string password = "";
        private string shopid = "";
        private string shopname = "";
        private string shopnote = "";
        private string branch = "";
        private string roleid = "";
        private string workdate = "";
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsData.SetPath(MapPath("~"));
            var nvc = Request.Form;
            if (!string.IsNullOrEmpty(nvc["uname"])) //ตรวจสอบว่ามีการส่งค่ามาจากเพจ index.html ไหม
            {
                userid = nvc["uname"]; //รับค่า userid
                if (!string.IsNullOrEmpty(nvc["pword"]))
                {
                    password = nvc["pword"]; //รับค่า password
                }
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
            }
            SaveSessionValue();
            if(cApp.user_id==""||cApp.shop_id!="1")
            {
                Response.Redirect("Default.aspx", true);
            }
            else
            {
                if (cApp.user_role != "0")
                {
                    PlaceAdmin.Visible = false;
                }
            }
        }
        private string CheckFullname(string uid, string pwd)
        {
            DataTable dt = ClsData.UserData();
            DataRow[] dr = dt.Select(@"id='" + uid + @"'");

            string fname = "";
            roleid = "";
            shopid = "";
            foreach (DataRow r in dr)
            {
                if (r["password"].ToString() == pwd)
                {
                    fname = r["id"].ToString() + " : " + r["name"].ToString();
                    roleid = r["role"].ToString();
                    shopid = r["shopid"].ToString();
                    CheckShopData(shopid);
                }
            }
            return fname;
        }
        private string CheckShopData(string oid)
        {
            DataTable dt = ClsData.ShopData();
            DataRow[] dr = dt.Select(@"OID='" + oid + "'");
            string fullname = "";
            shopname = "";
            branch = "";
            shopnote = "";
            foreach (DataRow r in dr)
            {
                fullname = r["custname"].ToString();
                shopname = r["shopname"].ToString();
                branch = r["branch"].ToString();
                DataRow[] rows = ClsData.CustomerGroupData().Select("OID='" + r["GroupID"].ToString() + "'");
                foreach (DataRow row in rows)
                {
                    shopnote = row["remark"].ToString();
                }
            }
            return fullname;
        }
        void ClearVariables()
        {
            userid = "";
            userName = "";
            password = "";
            shopid = "";
            shopname = "";
            branch = "";
            roleid = "";
            workdate = "";
            shopnote = "";
        }
        void LoadSessionValue()
        {
            shopid = cApp.shop_id;
            shopname = cApp.shop_name;
            userid = cApp.user_id;
            userName = cApp.user_name;
            roleid = cApp.user_role;
            workdate = cApp.working_date;
            branch = cApp.shop_branch;
            shopnote = cApp.shop_note;
            password = cApp.user_password;
        }
        void SaveSessionValue()
        {
            cApp.session_id = GetSession();
            cApp.shop_id = shopid;
            cApp.shop_name = shopname;
            cApp.user_id = userid;
            cApp.user_name = userName;
            cApp.user_role = roleid;
            cApp.working_date = workdate;
            cApp.shop_branch = branch;
            cApp.shop_note = shopnote;
            cApp.user_password = password;
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
    }
}