using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;

namespace shopsales
{
    public partial class menu : System.Web.UI.Page
    {
        private string userid = "";
        private string userName = "";
        private string password = "";
        private string shopid = "";
        private string shopname = "";
        private string shopgroup = "";
        private string shopnote = "";
        private string shopsharedisc = "";
        private string shopgpx="";
        private string branch = "";
        private string roleid = "";
        private string workdate = "";
        private string area = "";
        private string salescode = "";
        private string supcode = "";
        private string zonecode = "";
        private ClsSessionUser cApp = new ClsSessionUser();
        private string displayText = "Please Log-in";
        private bool IsPostLogin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsData.SetPath(MapPath("~"));
            NameValueCollection nvc = Request.Form;
            if (!string.IsNullOrEmpty(nvc["uname"])) //ตรวจสอบว่ามีการส่งค่ามาจากเพจ index.html ไหม
            {
                userid = nvc["uname"]; //รับค่า userid
                if (!string.IsNullOrEmpty(nvc["pword"]))
                {
                    password = nvc["pword"]; //รับค่า password
                }
                IsPostLogin = true;
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
            if (userName != "")
            {
                LoadShopCombo();
                displayText = "Welcome " + userName;
                lblheader.Text = displayText;
                if (txtsalesDate.Text == "")
                {
                    txtsalesDate.Text = cApp.CurrentDate();
                }
            }
            else
            {//ถ้า logon ไม่ผ่าน ก็เคลียร์ค่าเซสชั่นใหม่หมด
                ClearVariables();
            }
            SaveSessionValue();
            //ปิด-เปิดปุ่มและคอนโทรล
            EnableButton(userName);
        }
        protected void btnMasterMenu_Click(object sender, EventArgs e)
        {
            SetSession();
            Response.Redirect("masterfile.aspx");
        }
        protected void btnSales_Click(object sender, EventArgs e)
        {
            SetSession();
            Response.Redirect("salesentry.aspx");
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            SetSession();
            Response.Redirect("report.aspx");
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            ClearVariables();
            SaveSessionValue();
            Response.Redirect("index.html", true);
        }
        protected void cboShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            shopid = cboShop.SelectedValue.ToString();
            CheckShopData(shopid);
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SetSession();
            Response.Redirect("editsales.aspx");
        }
        protected void btnRequestStock_Click(object sender, EventArgs e)
        {
            SetSession();
            Response.Redirect("RequestStock.aspx");
        }
        protected void btnAddStock_Click(object sender, EventArgs e)
        {
            SetSession();
            Response.Redirect("SendStock.aspx");
        }
        protected void SetSession() //ส่งค่า ตัวแปรไปอีกเพจ
        {
            workdate = txtsalesDate.Text;
            if (cboShop.Visible == true) //กรณีเปลี่ยนค่าได้ต้องอ่านค่าใหม่จาก cboshop ทุกครั้ง
            {
                shopid = cboShop.SelectedValue.ToString();
                shopname =CheckShopData(shopid);
            }
            SaveSessionValue();
        }
        protected void LoadSessionValue()
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
            shopgroup = cApp.shop_group;
            shopsharedisc = cApp.share_discount;
            shopgpx = cApp.gpx_rate;
            zonecode = cApp.shop_zonecode;
            area = cApp.shop_areacode;
            salescode = cApp.shop_salescode;
            supcode = cApp.shop_supcode;
        }
        protected void SaveSessionValue()
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
            cApp.shop_group = shopgroup;
            cApp.gpx_rate = shopgpx;
            cApp.share_discount = shopsharedisc;
            cApp.shop_areacode = area;
            cApp.shop_salescode = salescode;
            cApp.shop_supcode = supcode;
            cApp.shop_zonecode = zonecode;
            if (userName != "" && IsPostLogin == true)
            {
                cApp.SaveLogin(Request.Browser.IsMobileDevice);
            }
            Session["cApp"] = cApp;
        }
        protected string GetSession()
        {
            if (Session["SessionID"] == null)
            {
                Session["SessionID"] = HttpContext.Current.Session.SessionID;
            }
            return Session["SessionID"].ToString();
        }
        protected void LoadShopCombo()
        {
            if (cboShop.DataTextField == "")
            {
                DataTable dt;
                if (shopgroup != "")
                {
                    string group = shopgroup.Replace(",", "','");
                    dt = ClsData.ShopData(group);
                }
                else
                {
                    dt = ClsData.ShopData();
                }
                ClsData.LoadCombo(dt, cboShop, "custname", "oid");
            }
        }
        protected string CheckShopData(string oid)
        {
            DataTable dt = ClsData.ShopData();
            DataRow[] dr = dt.Select(@"OID='" + oid + "'");
            string fullname = "";
            shopname = "";
            branch = "";
            shopnote = "";
            shopgpx = "";
            shopsharedisc = "";
            area = "";
            salescode = "";
            supcode = "";
            zonecode = "";
            foreach (DataRow r in dr)
            {
                fullname = r["custname"].ToString();
                shopname = r["shopname"].ToString();
                branch = r["branch"].ToString();
                shopgpx = r["GPx"].ToString();
                shopsharedisc = r["ShareDiscount"].ToString();
                area = r["area"].ToString();
                salescode = r["salescode"].ToString();
                supcode = r["supcode"].ToString();
                zonecode = r["zone"].ToString();
                DataRow[] rows = ClsData.CustomerGroupData().Select("OID='" + r["GroupID"].ToString() + "'");
                foreach (DataRow row in rows)
                {
                    shopnote = row["remark"].ToString();
                }
            }
            return fullname;
        }
        protected string CheckFullname(string uid, string pwd)
        {
            DataTable dt = ClsData.UserData();
            DataRow[] dr = dt.Select(@"id='" + uid + @"'");

            string fname = "";
            roleid = "";
            shopid = "";
            shopgroup = "";
            foreach (DataRow r in dr)
            {
                if (r["password"].ToString().ToUpper() == pwd.ToUpper())
                {
                    fname = r["id"].ToString() + " : " + r["name"].ToString();
                    roleid = r["role"].ToString();
                    shopid = r["shopid"].ToString();
                    shopgroup = r["shopgroup"].ToString();
                    CheckShopData(shopid);
                }
                else
                {
                    displayText = "Password Incorrect";
                }
            }
            return fname;
        }
        protected void EnableButton(string uname)
        {
            bool enable = false;
            if (uname != "") //ถ้าล็อกออนสำเร็จ ค่้อยเปิดคอนโทรล
            {
                enable = true;
            }
            else
            {
                lblShop.Text = displayText;
                btnLogout.Text = "Log in";
            }
            lblSalesDate.Visible = enable;
            btnMasterMenu.Visible = enable;
            btnSales.Visible = enable;
            btnEdit.Visible = enable;
            btnReport.Visible = enable;
            txtsalesDate.Visible = enable;
            cboShop.Visible = enable;
            btnAddStock.Visible = enable;
            btnRequestStock.Visible = enable;
            if (enable == true) CheckRights();
        }
        protected void CheckRights()
        {
            if (shopid != "")
            {
                cboShop.Visible = false;
                lblShop.Text = shopname + " สาขา " + branch;
            }
            else
            {
                cboShop.Visible = true;
            }

            //check role
            if (roleid == "1")
            {
                //สิทธิ์ PC จะล็อกเมนูมาสเตอร์ไฟล์
                btnMasterMenu.Enabled = false;
                btnEdit.Enabled = false;
                btnAddStock.Enabled = false;
            }
            else
            {
                //สิทธิ์ 0 Admin ทำได้ทุกอย่าง
                //สิทธิ์ 1 Staff แก้ยอดกับคีย์ยอดขายไม่ได้
                //สิทธิ์ 2 Supervisor ทำได้หมดยกเว้นเพิ่ม Master Files
                cboShop.Visible = true;
                lblShop.Text = "เลือกสาขา :";
                txtsalesDate.Enabled = true;
                if (!IsPostBack) cboShop.SelectedValue = cApp.shop_id;
                if (roleid != "0")
                {
                    btnMasterMenu.Enabled = false;
                }
                if (roleid == "3")
                {
                    btnEdit.Enabled = false;
                    btnSales.Enabled = false;
                    btnRequestStock.Enabled = false;
                    btnAddStock.Enabled = false;
                }
            }

        }
        protected void ClearVariables()
        {
            userid = "";
            userName = "";
            password = "";
            shopid = "";
            shopname = "";
            shopgroup = "";
            branch = "";
            roleid = "";
            workdate = "";
            shopnote = "";
            shopgpx = "";
            shopsharedisc = "";
            salescode = "";
            supcode = "";
            area = "";
            zonecode = "";
        }
    }
}