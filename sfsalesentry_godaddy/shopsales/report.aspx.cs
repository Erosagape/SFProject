using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class report : System.Web.UI.Page
    {
        private string shopid = "";
        private string cliteria = "";
        private DataTable dtShop = new DataTable();
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name != "")
            { 
                lblUsername.Text = "Welcome " + cApp.user_name;
                if(!IsPostBack)
                {
                    if (txtDateF.Text == "")
                    {
                        txtDateF.Text = cApp.working_date;
                    }
                    if (txtDateT.Text == "")
                    {
                        txtDateT.Text = cApp.working_date;
                    }
                }
                if (cboShoeType.DataTextField == "") ClsData.LoadShoeType(cboShoeType, "STName", "STId");
                if (cbosalesType.DataTextField == "") ClsData.LoadSaleType(cbosalesType, "Description", "OID",true);
                if (cboprodGroup.DataTextField == "") ClsData.LoadShoeGroup(cboprodGroup, "GroupName", "OID");
                if (cboSumType.Items.Count == 0) ClsData.LoadReportType(cboSumType,cApp.user_role);
                if(cboMonth.DataTextField =="")
                {
                    ClsData.LoadMonth(cboMonth, "MonthNameTH", "MonthID");
                    txtYear.Text = ClsUtil.GetCurrentTHDate().Year.ToString();
                    cboMonth.SelectedIndex = 0;
                }
                if (cApp.user_role!="1")
                {
                    lblShop.Text = "เลือกจุดขาย";
                    cboShop.Visible = true;
                    if(cboShop.DataTextField=="")
                    {
                        ClsData.LoadShopByGroup(cboShop,cApp.shop_group,"custname","oid",true,true);
                        shopid = cApp.shop_id;
                        try { if (shopid != "") cboShop.SelectedValue = shopid; } catch { }
                    }
                    btnPrint.Visible = true;
                }
                else
                {
                    lblShop.Text = cApp.shop_name + " สาขา " + cApp.shop_branch;
                    cboShop.Visible = false;
                    shopid = cApp.shop_id;
                    btnPrint.Visible =false;
                }
                ViewState["RefUrl"] = "menu.aspx";
                if (shopid == ""||shopid=="-")
                {
                    if (cboCustGroup.DataTextField == "")
                    {
                        LoadCustGroup();
                    }
                    else
                    {
                        cboCustGroup.Visible = true;
                        lblCustGroup.Visible = true;
                    }
                }
                else
                {
                    lblCustGroup.Visible = false;
                    cboCustGroup.Visible = false;
                    cboShop.SelectedValue = shopid;
                }
            }
            else
            {
                Response.Redirect("index.html", true);
                Response.End();
            }
        }
        protected void LoadCustGroup()
        {
            if (cboCustGroup.DataTextField == "")
            {
                ClsData.LoadShopGroup(cApp.shop_group, cboCustGroup, "CustGroupNameTH", "OID",true);
            }
            else
            {
                cboCustGroup.SelectedIndex = 0;
            }
            lblCustGroup.Visible = true;
            cboCustGroup.Visible = true;
        }
        protected DataTable ShowReport()
        {
            DataTable dt = new DataTable();
            string id = shopid;
            if (shopid == "" && cboShop.SelectedValue.ToString() != "-")
            {
                shopid = cboShop.SelectedValue.ToString();
            }
            if (shopid == "") shopid = "-";
            string custgroup = "";
            if (cboCustGroup.Visible == true && !cboCustGroup.SelectedValue.ToString().Equals(""))
            {
                custgroup = cboCustGroup.SelectedValue.ToString();
            }
            else
            {
                if(shopid!="-")
                {
                    custgroup = ClsData.GetValueXML("customer", "OID='" + shopid + "'", "GroupID");
                }
                else
                {
                    custgroup = cApp.shop_group;
                }
            }
            //by shop
            if (cboSumType.SelectedIndex==0)
            {
                dt = ClsData.GetReportDataByPOS(txtDateF.Text, txtDateT.Text, custgroup, shopid, GetCliteria(), ChkSummaryOnly.Checked);
            }
            //by user
            if(cboSumType.SelectedIndex==1)
            {
                dt = ClsData.GetReportDataByUSER(txtDateF.Text, txtDateT.Text, custgroup, shopid, GetCliteria(), ChkSummaryOnly.Checked,cApp.user_role);
            }
            //by date
            if(cboSumType.SelectedIndex==2|| cboSumType.SelectedIndex == 3)
            {
                dt = ClsData.GetReportDataByDATE(cboSumType.SelectedIndex, txtDateF.Text, txtDateT.Text, custgroup, shopid, GetCliteria(), ChkSummaryOnly.Checked);
            }
            if(cboSumType.SelectedIndex==4)
            {
                dt = ClsData.GetReportByQuarter( txtYear.Text, custgroup, shopid, GetCliteria(isYearly:true), ChkSummaryOnly.Checked);
            }
            if (cboSumType.SelectedIndex == 5)
            {
                dt = ClsData.GetReportByYearly( txtYear.Text, custgroup, shopid, GetCliteria(isYearly:true), ChkSummaryOnly.Checked);
            }

            shopid = id;            
            ViewState["ReportSales"] = dt;
            return dt;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            SetSession();
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
            {
                Response.Redirect((string)refUrl);
            }

        }
        private void SetSession()
        {
            cApp.shop_id = shopid;
            cApp.working_date = cApp.CurrentDate();
            Session["cApp"] = cApp;
        }
        private string GetCliteria(string AddWhere="",bool isYearly=false)
        {
            string str = "";
            cliteria = "";
            if(isYearly==true)
            {
                txtDateF.Text = txtYear.Text + "-01-01";
                txtDateT.Text = txtYear.Text + "-12-31";
            }
            if (txtDateF.Text !="")
            {
                str += "salesDate>='" + txtDateF.Text + "'";
                cliteria += "ตั้งแต่วันที่ขาย=" + txtDateF.Text + " ";
            }
            if (txtDateT.Text != "")
            {
                if (str != "") str += " AND ";
                str += "salesDate<='" + txtDateT.Text + "'";
                cliteria += "จนถึงวันที่ขาย=" + txtDateT.Text + " ";
            }
            if (txtModel.Text !="")
            {
                if (str != "") str += " AND ";
                str += "ModelCode='" + txtModel.Text + "'";
                cliteria += "รุ่น=" + txtModel.Text + " ";
            }
            if (txtColor.Text != "")
            {
                if (str != "") str += " AND ";
                str += "(ColorName ='" + txtColor.Text + "' or ColorCode='" + txtColor.Text + "')";
                cliteria += "สี=" + txtColor.Text + " ";
            }
            if (txtSize.Text != "")
            {
                if (str != "") str += " AND ";
                str += "SizeNo='" + txtSize.Text + "'";
                cliteria += "ขนาด=" + txtSize.Text + " ";
            }
            if (cboShoeType.SelectedIndex>0)
            {
                if (str != "") str += " AND ";
                str += "prodType='" + cboShoeType.SelectedValue.ToString() + "'";
                cliteria += "สินค้า=" + cboShoeType.SelectedItem.Text + " ";
            }
            if (cbosalesType.SelectedIndex>0)
            {
                if (str != "") str += " AND ";
                str += "salesType='" + cbosalesType.SelectedValue.ToString() + "'";
                cliteria += "จัดรายการ=" + cbosalesType.SelectedItem.Text + " ";
            }
            if (cboprodGroup.SelectedIndex > 0)
            {
                if (str != "") str += " AND ";
                str += "prodGroup='" + cboprodGroup.SelectedItem.Text + "'";
                cliteria += "ประเภท=" + cboprodGroup.SelectedItem.Text + " ";
            }
            if(cboShop.SelectedIndex>0)
            {
                cliteria += "จุดขาย=" + cboShop.SelectedItem.Text + " ";
            }
            if (cboCustGroup.SelectedIndex > 0)
            {
                cliteria += "กลุ่ม=" + cboCustGroup.SelectedItem.Text + " ";
            }
            if(AddWhere !="")
            {
                if (str != "") str += " AND ";
                str += AddWhere;
            }
            if(cboSumType.SelectedIndex>=2)
            {
                cliteria += "ยอดสะสม " + GetDiffMonth();
            }
            return str;
        }
        private string GetDiffMonth()
        {
            string str = "ไม่ระบุ";
            int diff = 0;            
            try
            {
                int dstart=0;
                int dend=0;
                if (txtDateF.Text != "")
                {
                    dstart = Convert.ToDateTime(txtDateF.Text).Month;
                }
                if (txtDateT.Text != "")
                {
                    dend = Convert.ToDateTime(txtDateT.Text).Month;
                }
                diff = (dend - dstart) + 1;
                if(diff>0)
                {
                    str = diff + " เดือน";
                }
            }
            finally
            {

            }
            return str;
        }
        private string ExportToFile(DataTable dt)
        {
            string filename = cApp.user_id + "_"+ ClsUtil.GetCurrentTHDate().ToString("yyyyMMddHHmmss") + ".xml";
            try
            {
                ClsData.ExportToXMLFile(MapPath("~/"+filename),dt);
            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }
            return filename;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string datafilename = ExportToFile(ShowReport());
            if (System.IO.File.Exists (MapPath("~/"+datafilename)) ==true)
            {
                ClsData.Download(datafilename, "~/" + datafilename);
                System.IO.File.Delete(MapPath("~/" + datafilename));
            }
            SetSession();       
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshReport();
            }
            catch(Exception ex)
            {
                Response.Write("ไม่สามารถเรียกรายงานได้ " + ex.Message);
            }
        }
        protected void cboShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            shopid = cboShop.SelectedValue.ToString();
            cApp.shop_id = shopid;
            if(shopid=="-")
            {
                LoadCustGroup();
            }
            else
            {
                lblCustGroup.Visible = false;
                cboCustGroup.Visible = false;
                cboCustGroup.SelectedIndex = -1;
            }
            SetSession();
        }
        protected void SetDate()
        {
            try
            {
                if(cboMonth.SelectedValue.ToString()!="")
                {
                    DateTime datevar = new DateTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(cboMonth.SelectedValue.ToString()), 1);
                    txtDateF.Text = datevar.ToString("yyyy-MM-dd");
                    txtDateT.Text = datevar.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                    txtDateF.Enabled = false;
                    txtDateT.Enabled = false;
                }
                else
                {
                    txtDateF.Enabled = true;
                    txtDateT.Enabled = true;
                }
            }
            catch
            {
                
            }
        }
        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboMonth.SelectedValue!=null)
            {
                SetDate();
            }
            SetSession();
        }
        protected void cboShoeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboShoeType.SelectedValue!=null)
            {
                try
                {
                    DataRow data = ClsData.QueryData(ClsData.ShoeTypeData(), "STId='" + cboShoeType.SelectedValue.ToString() + "'");
                    string shoeGroup = data["GroupOID"].ToString();
                    if(shoeGroup=="")
                    {
                        cboprodGroup.SelectedIndex=-1;
                    }
                    else
                    {
                        cboprodGroup.SelectedValue = shoeGroup;
                    }
                }
                catch(Exception ex)
                {
                    string err = ex.Message;
                }

            }
            SetSession();
        }
        protected void PrepareData()
        {
            Session["rptData"] = ShowReport();
            Session["rptCliteria"] = cliteria;
            SetSession();
        }
        protected void RefreshReport()
        {
            PrepareData();
            Session["reportName"] = GetReportName();
            Response.Redirect("xmlreport.aspx?reportno=" + cboSumType.SelectedIndex +"&reporttype="+ ChkSummaryOnly.Checked);
        }
        protected void PreviewReport()
        {
            PrepareData();
            string qrystr = GetReportName();
            if (qrystr != "")
            {
                Response.Redirect("testdx.aspx?reportname=" + qrystr, true);
            }
        }
        protected string GetReportName()
        {
            string rptname = "";
            switch (cboSumType.SelectedIndex)
            {
                case 0:
                    rptname = "salesreport";
                    break;
                case 1:
                    rptname = "salesreport";
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    rptname = "monthreport";
                    break;
            }
            if (ChkSummaryOnly.Checked == true && rptname == "salesreport") rptname = "sumreport";
            return rptname;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PreviewReport();
            }
            catch (Exception ex)
            {
                Response.Write("ไม่สามารถเรียกรายงานได้ " + ex.Message);
            }
        }
    }
}