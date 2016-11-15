using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class xmlquery : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name == "")
            {
                Response.Redirect("Default.aspx", true);
            }
            else
            {
                if(!IsPostBack)
                {
                    DropDownList1.Items.Add("From Text");
                    DropDownList1.Items.Add("Role");
                    DropDownList1.Items.Add("User Login");
                    DropDownList1.Items.Add("Data file List");
                }
            }
            cApp.session_id = GetSession();
        }
        private string GetSession()
        {
            if(Session["SessionID"]==null)
            {
                Session["SessionID"]= HttpContext.Current.Session.SessionID;
            }
            return Session["SessionID"].ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable(TextBox1.Text,cApp.session_id)==false )
            {
                IService svc = new IService();
                string data = "";
                if(chkJSON.Checked==true)
                {
                    data = svc.QueryDataJSON(TextBox1.Text, TextBox3.Text);
                }
                else
                {
                    data = svc.QueryDataXML(TextBox1.Text, TextBox3.Text);
                }
                TextBox2.Text =data;
            }
            else
            {
                TextBox2.Text = "Table is locked";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ClsData.IsLockedDataTable(TextBox1.Text, cApp.session_id) == false)
            {
                Showdata();
                IService svc = new IService();
                if(chkJSON.Checked==true)
                {
                    string data = svc.ProcessDataJSON(TextBox1.Text, TextBox2.Text);
                    TextBox3.Text = data;
                }
                else
                {
                    string data = svc.ProcessDataXML(TextBox1.Text, TextBox2.Text);
                    TextBox3.Text = data;
                }
            }
            else
            {
                TextBox2.Text = "Table is locked";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable(TextBox1.Text,cApp.session_id)==false)
            {
                if (ClsData.LockDataTable(TextBox1.Text, cApp.session_id)== true)
                {
                    TextBox2.Text = "Locked Successfully";
                }
                else
                {
                    TextBox2.Text = "Cannot lock table";
                }

            }
            else
            {
                TextBox2.Text = "Table is already locked";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (ClsData.UnlockDataTable(TextBox1.Text, cApp.session_id)== true)
            {
                TextBox2.Text = "Table unlocked!";
            }
            else
            {
                TextBox2.Text = "Cannot unlock";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (ClsData.IsLockedDataTable(TextBox1.Text, cApp.session_id) == false)
            {
                IService svc = new IService();
                string data = svc.RemoveDataXML(TextBox1.Text, TextBox3.Text);
                TextBox2.Text = svc.ShowData(data);
            }
            else
            {
                TextBox2.Text = "Table is locked";
            }
        }
        protected void Showdata()
        {
            if (chkJSON.Checked == true)
            {
                GridView1.DataSource = ClsData.GetDataTableFromJSON(TextBox2.Text);
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ClsData.GetDataTableFromXML(TextBox2.Text, "Table");
                GridView1.DataBind();
            }
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            switch (DropDownList1.SelectedIndex)
            {                
                case 1:
                    dt= ClsData.RoleData();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    TextBox2.Text = ClsData.GetXML(dt);
                    break;
                case 2:
                    dt = ClsData.GetDataXML("LoginHistory");
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    TextBox2.Text = ClsData.GetXML(dt);
                    break;
                case 3:
                    dt = ClsData.XMLTableData("Delivery*");
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    TextBox2.Text = ClsData.GetXML(dt);
                    break;
                default:
                    Showdata();
                    break;
            }
            
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            string username = TextBox4.Text;
            if(username!="")
            {
                DataTable dt = ClsData.GetCustomerByUser(username);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                TextBox2.Text = ClsData.GetXML(dt);
            }
        }
    }
}