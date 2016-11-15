using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class test : System.Web.UI.Page
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
            cApp.session_id = GetSession();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable(TextBox1.Text,cApp.session_id)==false )
            {
                IService svc = new IService();
                string data = svc.QueryDataXML(TextBox1.Text, TextBox3.Text);
                TextBox2.Text = svc.ShowData(data);
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
                IService svc = new IService();
                string data = svc.ProcessDataXML(TextBox1.Text, TextBox2.Text);
                TextBox3.Text = data;
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
        protected void Button6_Click(object sender, EventArgs e)
        {
            if (ClsData.IsLockedDataTable(TextBox1.Text, cApp.session_id) == false)
            {
                TextBox2.Text = ClsData.UpdateDataXML(TextBox1.Text,TextBox3.Text,TextBox2.Text) + " Rows Updated"; 
            }
            else
            {
                TextBox2.Text = "Table is locked";
            }
        }
        protected string GetSession()
        {
            if (Session["SessionID"] == null)
            {
                Session["SessionID"] = HttpContext.Current.Session.SessionID;
            }
            return Session["SessionID"].ToString();
        }
    }
}