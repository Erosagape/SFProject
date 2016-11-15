using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TextBox1.Text = ClsData.GetJSONFromXML("LoginHistory");
                IService svc = new IService();
                //Label1.Text= svc.Login("wm001", "4780");
                Label1.Text = TimeZone.CurrentTimeZone.StandardName + " " + TimeZone.CurrentTimeZone.ToUniversalTime(DateTime.Now) + " (" + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now) + ") -> " + TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ClsData.GetJSONFromXMLString(TextBox1.Text);
            GridView1.DataSource = ClsData.GetDataTableFromJSON(TextBox1.Text);
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ClsData.GetXMLFromJSON(TextBox1.Text,"LoginHistory");
            GridView1.DataSource = ClsData.GetDataTableFromXML(TextBox1.Text,"LoginHistory");
            GridView1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            IService svc = new IService();
            Label1.Text =svc.SendDailyReport(DateTime.Today.ToString ("yyyy-MM-dd"),"เรียนผู้เกี่ยวข้อง");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string msg = "";
            //clear all reports shared files
            foreach(XMLFileList lst in ClsData.GetXMLTableList("rpt*"))
            {
                System.IO.File.Delete(ClsData.GetPath() + lst.filename);
                msg += lst.filename + " Deleted" + Environment.NewLine;
            }
            //clear all reports shared files
            foreach (System.Data.DataRow dr in ClsData.UserData().Rows)
            {
                foreach (XMLFileList lst in ClsData.GetXMLTableList(dr["id"].ToString() + "_*"))
                {
                    System.IO.File.Delete(ClsData.GetPath() + lst.filename);
                    msg += lst.filename+ " Deleted"+ Environment.NewLine;
                }
            }
            TextBox1.Text = msg;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            IService svc = new IService();
            string ondate = DateTime.Today.ToString("yyyy-MM-dd");
            TextBox1.Text = svc.GetDailyReport(0, "Daily", ondate);
            TextBox1.Text += Environment.NewLine+ svc.GetDailyReport(1, "Daily", ondate); 
        }
    }
}