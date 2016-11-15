using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class testemail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                LoadConfig();
            }
        }
        protected void LoadConfig()
        {
            DataTable dt = ClsData.GetDataXML("mailsetup");
            if (dt.Rows.Count > 0)
            {
                txtHost.Text = dt.Rows[0]["host"].ToString();
                txtPort.Text = dt.Rows[0]["port"].ToString();
                txtAccount.Text = dt.Rows[0]["account"].ToString();
                txtPassword.Text = dt.Rows[0]["password"].ToString();
                chkSSL.Checked= dt.Rows[0]["ssl"].ToString() == "1" ? true : false;
            }
            txtSubject.Text = "Test ส่งเมล์";
            txtBody.Text = "Complete!";
            IService svc = new IService();
            txtXMLMailTo.Text = svc.GetDataXML("maillist");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            CEMail.NewEmail();
            CEMail.MailHost = txtHost.Text;
            CEMail.isBodyHTML = true;
            CEMail.isSSL = chkSSL.Checked;
            CEMail.MailBody = txtBody.Text;
            CEMail.MailFrom = txtAccount.Text;
            CEMail.MailPassword = txtPassword.Text;
            CEMail.MailPort = Convert.ToInt32( txtPort.Text);
            CEMail.MailSubject = txtSubject.Text;
            CEMail.MailTo.Add(txtTo.Text);
            Label1.Text=CEMail.SendEmail();            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string updateString = "";
            updateString += "host=" + txtHost.Text + ";";
            updateString += "account=" + txtAccount.Text + ";";
            updateString += "port=" + txtPort.Text + ";";
            updateString += "password=" + txtPassword.Text + ";";
            updateString += "ssl=" + (chkSSL.Checked ? "1":"0") + ";";
            ClsData.UpdateDataXML("mailsetup","",updateString);
            LoadConfig();
            Label1.Text = "Save successfully!";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ClsData.WriteDataXML("maillist", txtXMLMailTo.Text);
            Label1.Text = "Update List Successfully";
        }
    }
}