using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class dailiyreport : System.Web.UI.Page
    {
        protected string host ="";
        protected string mailserver = "";
        protected int mailport=587;
        protected string mailaccount = "";
        protected bool mailssl = true;
        protected string mailpassword = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadConfig();
            if (!IsPostBack)
            {                
                txtDate.Text = Calendar1.TodaysDate.ToString("yyyy-MM-dd");
                TextBox1.Text = "สรุปยอดขายรายเดือนวันที่ " + txtDate.Text + " <a href='" + host + "savereport.aspx?reportid=rptDailySales-" + txtDate.Text + "'>Click</a>";
                TextBox2.Text = "สรุปจำนวนสินค้าที่ขายวันที่ " + txtDate.Text + " <a href='" + host + "savereport.aspx?reportid=rptDailyVolume-" + txtDate.Text + "'>Click</a>";
            }
        }
        protected void LoadConfig()
        {
            host = @"http:\\" + HttpContext.Current.Request.Url.Authority + @"\";
            DataTable dt = ClsData.GetDataXML("mailsetup");
            if(dt.Rows.Count >0)
            {
                mailserver = dt.Rows[0]["host"].ToString();
                mailport = Convert.ToInt32( dt.Rows[0]["port"]);
                mailaccount = dt.Rows[0]["account"].ToString();
                mailpassword = dt.Rows[0]["password"].ToString();
                mailssl =  dt.Rows[0]["ssl"].ToString() =="1" ? true : false;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = ClsData.GetDailyReport(txtDate.Text);
            TextBox1.Text = ClsData.GetReportDataForEmail(ds.Tables[0], 2, "DailySales-", txtDate.Text, "สรุปยอดขายรายเดือนวันที่ " + txtDate.Text);
            lblMessage.Text = "Process Complete!</br>" + TextBox1.Text;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataSet ds = ClsData.GetDailyReport(txtDate.Text);
            TextBox2.Text = ClsData.GetReportDataForEmail(ds.Tables[1],3, "DailyVolume-", txtDate.Text, "สรุปจำนวนสินค้าที่ขายวันที่ " + txtDate.Text);
            lblMessage.Text = "Process Complete!</br>" + TextBox2.Text;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string emailBody= GetMailBody();
            lblMessage.Text = "Begin Sending mail..";
            DataTable dt = ClsData.GetDataXML("maillist");
            foreach (DataRow dr in dt.Rows)
            {
                string mailto = dr[0].ToString();
                CEMail.NewEmail();
                CEMail.MailHost = mailserver;
                CEMail.MailPort = mailport;
                CEMail.MailFrom = mailaccount;
                CEMail.isSSL = mailssl;
                CEMail.MailTo.Add(mailto);
                CEMail.MailPassword = mailpassword;
                CEMail.MailSubject = "[AutoMail] รายงานประจำวันที่ " + txtDate.Text;
                CEMail.MailBody = emailBody;
                CEMail.isBodyHTML = true;
                lblMessage.Text += "<br/>[" + mailto + "]" + CEMail.SendEmail();
            }            
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            TextBox1.Text = "สรุปยอดขายรายเดือนวันที่ " + txtDate.Text + " <a href='" + host + "savereport.aspx?reportid=rptDailySales-" + txtDate.Text + "'>Click</a>";
            TextBox2.Text = "สรุปจำนวนสินค้าที่ขายวันที่ " + txtDate.Text + " <a href='" + host + "savereport.aspx?reportid=rptDailyVolume-" + txtDate.Text + "'>Click</a>";
        }
        private string GetMailBody()
        {
            string str = "เรียนผู้เกี่ยวข้อง <br/><br/>";
            str += TextBox1.Text + "<br/>" + TextBox2.Text;
            return str;
        }
        
        protected void Button4_Click(object sender, EventArgs e)
        {
            DataSet ds = ClsData.GetDailyReport(txtDate.Text);
            string report1 = ClsData.GetReportDataForEmail(ds.Tables[0], 2, "DailySales-", txtDate.Text, "สรุปยอดขายรายเดือนวันที่ " + txtDate.Text);
            string report2 = ClsData.GetReportDataForEmail(ds.Tables[1], 3, "DailyVolume-", txtDate.Text, "สรุปจำนวนสินค้าที่ขายวันที่ " + txtDate.Text);
            TextBox1.Text = report1;
            TextBox2.Text = report2;
            lblMessage.Text = "Process Complete!</br>" + report1 + "</br>" + report2;
        }
    }
}