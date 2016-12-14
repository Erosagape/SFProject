using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
namespace shopsales
{
    //class ใช้สำหรับส่งอีเมล์
    public static class CEMail
    {
        public static string MailFrom { get; set; }
        public static List<string> MailTo { get; set; }
        public static string MailHost { get; set; }
        public static string MailPassword { get; set; }
        public static bool isSSL { get; set; }
        public static bool isBodyHTML { get; set; }
        public static string MailSubject { get; set; }
        public static string MailBody { get; set; }
        public static int MailPort { get; set; }
        public static void NewEmail()
        {
            MailFrom = "";
            MailTo = new List<string>();
            MailHost = "";
            MailPassword = "";
            isSSL = false;
            isBodyHTML = true;
            MailSubject = "";
            MailBody = "";
            MailPort = 25;
        }
        public static string SendEmail()
        {
            string msg = "";
            try
            {
                SmtpClient mailClient = new SmtpClient(MailHost, MailPort);
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.EnableSsl = isSSL;
                mailClient.Credentials = new System.Net.NetworkCredential(MailFrom, MailPassword);

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = isBodyHTML;
                mail.From = new MailAddress(MailFrom);
                foreach(string toaddr in MailTo)
                {
                    mail.To.Add(toaddr);
                }
                mail.Body = MailBody;
                mail.Subject = MailSubject;

                mailClient.Send(mail);
                msg = "Complete! Sent Successfully";
            }
            catch (Exception ex)
            {
                msg = "ERROR ->" + ex.Message;
            }
            return msg;
        }
        public static string SendDemoMail()
        {
            string msg = "";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net", 25);

                mail.From = new MailAddress("puttipong@summitsf.co.th");

                mail.To.Add("leoputti@hotmail.com");
                mail.To.Add("littlepuppet123@gmail.com");
                mail.Body = @"ทดสอบส่งอีเมล์จาก C# ครับ" + @"<br/>ลองคลิกลิ้งก์  <a href='http://"+ ClsData.hostname +@"'>เว็บคีย์ยอดขาย</a>";
                mail.Subject = "Test Send E-Mail";
                mail.IsBodyHtml = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("puttipong@summitsf.co.th", "04071980");
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.EnableSsl =false;
                SmtpServer.Send(mail);
                msg= "Complete! :Mail Sent";
            }
            catch (Exception ex)
            {
                msg= "ERROR! : " + ex.Message;
            }
            return msg;
        }
    }
    //class สำหรับสร้าง Dynamic table สำหรับหน้าเพจ
    public static class CDymamicForm
    {
        public static string[] hidefields = { "oid", "refno","transactiontype","approvecode","acceptcode","confirmcode","transactiondate" };
        public static TableRow NewTableRow(DataTable dt, string prefix, string postfix, bool isheader, int rowindex = 0,bool defaultCheck=true)
        {
            TableRow t = new TableRow();
            t.HorizontalAlign = HorizontalAlign.Center;
            
            TableCell chk = new TableCell();
            chk.ID = "H" + postfix;
            chk.Text = "#";
            if(isheader==false)
            {
                CheckBox c = new CheckBox();
                c.ID = "chkD" + postfix;
                c.Text = postfix;
                c.Checked = defaultCheck;
                chk.Controls.Add(c);
            }
            t.Cells.Add(chk);
            foreach (DataColumn col in dt.Columns)
            {
                TableCell tc = new TableCell();
                if (isheader)
                {
                    tc.ID = prefix + col.ColumnName;
                    tc.Text = ClsData.GetFieldCaption(col.ColumnName);
                }
                else
                {
                    string val = "";
                    if (rowindex > 0)
                    {
                        val = dt.Rows[rowindex - 1][col.ColumnName].ToString();
                    }
                    tc.ID = prefix + col.ColumnName + postfix;
                    Control ctl=AddControl(col.ColumnName, prefix + col.ColumnName + postfix, val);
                    tc.Controls.Add(ctl);
                }
                if (hidefields.FindIndex(e => e.Equals(col.ColumnName.ToLower()))>=0)
                {
                    tc.Visible=false;
                }
                t.Cells.Add(tc);
/*
                if (col.ColumnName.ToLower() == "goodscode")
                {
                    tc = new TableCell();
                    tc.ID = prefix + "browsegoods" + postfix;
                    tc.Text = "";
                    if(!isheader)
                    {
                        tc.Controls.Add(AddControl("browsegoods", prefix + "browsegoods" + postfix));
                    }
                    t.Cells.Add(tc);
                }
*/
            }
            return t;
        }
        public static Control AddControl(string colname, string id, string value = "")
        {
            switch (colname.ToLower())
            {
                case "prodcatname":
                    DropDownList cboCat = new DropDownList();
                    cboCat.ID = "fld" + id;
                    cboCat.DataSource = ClsData.ShoeTypeData();
                    cboCat.DataTextField = "STName";
                    cboCat.DataValueField = "STName";
                    cboCat.DataBind();
                    if (value != "") cboCat.SelectedItem.Text = value;
                    return cboCat;
                case "color":
                    DropDownList cboColor = new DropDownList();
                    cboColor.ID = "fld" + id;
                    cboColor.DataSource = ClsData.ShoeColorData();
                    cboColor.DataTextField = "ColTH";
                    cboColor.DataValueField = "ColNameInit";
                    cboColor.DataBind();
                    if (value != "") cboColor.SelectedValue = ClsData.GetColorcodeByName(value);
                    return cboColor;
                case "refno":
                case "oid":
                    TextBox txtRef = new TextBox();
                    txtRef.ID = "fld" + id;
                    txtRef.Visible = false;
                    txtRef.Width = 0;
                    if (value != "") txtRef.Text = value;
                    return txtRef;
                case "approvecode":
                case "acceptcode":
                case "confirmcode":
                case "transactiontype":
                case "transactiondate":
                    TextBox txtC = new TextBox();
                    txtC.ID = "fld" + id;                    
                    txtC.Enabled = false;
                    if (value != "") txtC.Text = value;
                    return txtC;
                case "browsegoods":
                    Button btn = new Button();
                    btn.ID = "fld" + id;
                    btn.Text = "...";
                    string idx = btn.ID.ToLower().Replace("fldd_browsegoods", "");
                    btn.OnClientClick = "SelectGoods('"+ idx +"');";
                    if (value != "") btn.Text = value;
                    return btn;
                default:
                    TextBox txt = new TextBox();
                    txt.ID = "fld" + id;
                    if (value != "") txt.Text = value;
                    return txt;
            }
        }
        public static void setDataInRow(TableRow tr, string id, string value)
        {
            try
            {
                Control ctl = tr.FindControl(id);
                switch (ctl.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        TextBox txt = (TextBox)ctl;
                        txt.Text = value;
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        DropDownList cbo = (DropDownList)ctl;
                        cbo.SelectedValue = value;
                        break;
                    case "System.Web.UI.WebControls.CheckBox":
                        CheckBox chk = (CheckBox)ctl;
                        chk.Checked = Convert.ToBoolean(value);
                        break;
                    case "System.Web.UI.WebControls.Button":
                        Button btn = (Button)ctl;
                        btn.Text = value;
                        break;
                }
            }
            catch
            {

            }
        }
        public static string getDataFromRow(TableRow tr, string id, string defaultval = "", bool getValue = false)
        {
            string retval = "";
            try
            {
                Control ctl = tr.FindControl(id);
                switch (ctl.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        TextBox txt = (TextBox)ctl;
                        retval = txt.Text;
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        DropDownList cbo = (DropDownList)ctl;
                        if (getValue == true)
                        {
                            retval = cbo.SelectedValue;
                        }
                        else
                        {
                            retval = cbo.SelectedItem.Text;
                        }
                        break;
                    case "System.Web.UI.WebControls.CheckBox":
                        CheckBox chk = (CheckBox)ctl;
                        retval = chk.Checked.ToString();
                        break;
                    case "System.Web.UI.WebControls.Button":
                        Button btn = (Button)ctl;
                        retval = btn.Text;
                        break;
                }
            }
            catch
            {
            }
            if (retval == "") retval = defaultval;
            return retval;
        }

    }
    //class สำหรับสร้าง Dynamic รายงานด้วย XTraReport
    public static class CReport
    {
        public static XtraReport DynamicReport(DataTable dt, string header = "", string cliteria = "")
        {
            float titleheight = 55;
            float headerheight = 35;
            float footerheight = 35;
            float fieldwidth = (float)106.27;
            float columnheight = 23;
            float keycolwidth = (float)212.54;
            float titlewidth = 1169;
            //set report defaults
            XtraReport rpt = new XtraReport();
            //set datatable
            rpt.DataSource = dt;
            rpt.DataAdapter = dt.TableName;
            rpt.PaperKind = System.Drawing.Printing.PaperKind.A4;
            rpt.Landscape = true;

            DetailBand dtl = new DetailBand();
            PageHeaderBand hdr = new PageHeaderBand();
            PageFooterBand ftr = new PageFooterBand();
            ReportHeaderBand title = new ReportHeaderBand();

            title.HeightF = titleheight;
            hdr.HeightF = headerheight;
            ftr.HeightF = footerheight;
            rpt.Bands.AddRange(new Band[] { dtl, hdr, ftr, title });
            rpt.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Regular);
            float pagewidth = rpt.PageWidth;
            //set header
            XRLabel lblReport = new XRLabel();
            lblReport.Text = header;
            lblReport.HeightF = columnheight;
            lblReport.WidthF = titlewidth;
            lblReport.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            title.Controls.Add(lblReport);

            XRLabel lblCaption = new XRLabel();
            lblCaption.Text = cliteria;
            lblCaption.HeightF = columnheight;
            lblCaption.WidthF = titlewidth;
            lblCaption.TopF = lblReport.TopF + lblCaption.HeightF;
            lblCaption.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Regular);
            title.Controls.Add(lblCaption);

            XRLabel lblHeader = new XRLabel();
            lblHeader.Text = "Group";
            lblHeader.WidthF = keycolwidth;
            lblHeader.HeightF = columnheight;
            lblHeader.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            hdr.Controls.Add(lblHeader);
            //fetch column header
            float pos = lblHeader.LeftF;
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                XRLabel lblField = new XRLabel();
                lblField.Text = dt.Columns[i].ColumnName;
                lblField.WidthF = fieldwidth;
                lblField.HeightF = columnheight;
                lblField.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
                lblField.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if ((pos + fieldwidth) > pagewidth)
                {
                    pos = pagewidth + fieldwidth;
                    pagewidth = pos;
                }
                else
                {
                    pos = pos + fieldwidth;
                }
                lblField.LeftF = pos;
                hdr.Controls.Add(lblField);
            }
            //set key fields
            XRLabel lblDetail = new XRLabel();
            lblDetail.WidthF = keycolwidth;
            lblDetail.HeightF = columnheight;
            lblDetail.DataBindings.Add("Text", dt, "Group");
            dtl.Controls.Add(lblDetail);
            //fetch detail fields
            pos = 0;
            pagewidth = rpt.PageWidth;
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                XRLabel lblData = new XRLabel();
                lblData.WidthF = fieldwidth;
                lblData.HeightF = columnheight;
                if ((pos + fieldwidth) > pagewidth)
                {
                    pos = pagewidth + fieldwidth;
                    pagewidth = pos;
                }
                else
                {
                    pos = pos + fieldwidth;
                }

                lblData.LeftF = pos;
                lblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                lblData.DataBindings.Add("Text", dt, dt.Columns[j].ColumnName);
                dtl.Controls.Add(lblData);
            }
            //set page properties
            dtl.HeightF = columnheight;
            rpt.CreateDocument(); //<-----INSERT THIS LINE
            rpt.PrintingSystem.PageMargins.Left = 140;
            System.Drawing.Printing.Margins m = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            rpt.PrintingSystem.PageSettings.Assign(m, rpt.PrintingSystem.PageSettings.PaperKind, rpt.PrintingSystem.PageSettings.Landscape);
            return rpt;
        }

    }
    //class สำหรับเชื่อมต่อกับ Excel
    public class CExcel
    {
        public string filename { get; set; }
        public string sheetname { get; set; }
        public CExcel()
        {
            this.filename = string.Empty;
            this.sheetname = string.Empty;
        }
        public CExcel(string fname)
        {
            this.filename = fname;
            this.sheetname = string.Empty;
        }
        public CExcel(string fname,string sheet)
        {
            this.filename = fname;
            this.sheetname = sheet;
        }
        public DataTable QueryDataTable(string sql)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrWhiteSpace(this.filename))
            {
                throw new ArgumentNullException("Filename Must not Empty");
            }
            else
            {
                System.IO.FileInfo finfo = new System.IO.FileInfo(this.filename);
                if(finfo.Exists)
                {
                    dt = new DataTable();
                    dt.TableName = sheetname;
                    using (var cls = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;"""))
                    {
                        cls.Open();
                        string sname = sheetname;
                        if(string.IsNullOrWhiteSpace(sname))
                        {
                            sname = finfo.Name.Split('.')[0].Trim();
                        }
                        System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql,cls);
                        da.Fill(dt);
                        cls.Close();
                    }
                }
                else
                {
                    throw new Exception("File does not exist");
                }
            }
            return dt.Copy();
        }
    }
}