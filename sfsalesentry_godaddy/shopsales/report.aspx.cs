using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

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
                if (cboSalesCode.DataTextField == "") ClsData.LoadEmployee(cboSalesCode, "SALE", "EmpCode", "OID",true);
                if (cboSupCode.DataTextField == "") ClsData.LoadEmployee(cboSupCode, "PCSUP", "EmpCode", "OID",true);
                if (cboCounter.DataTextField == "") ClsData.LoadCounterType(cboCounter, "CounterName", "OID",true);
                if (cboArea.Items.Count == 0) ClsData.LoadArea(cboArea);
                if (cboSumType.Items.Count == 0) ClsData.LoadReportType(cboSumType,cApp.user_role);
                if(cboMonth.DataTextField =="")
                {
                    ClsData.LoadMonth(cboMonth, "MonthNameTH", "MonthID");
                    txtYear.Text = DateTime.Now.AddHours(7).Year.ToString();
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
        protected DataTable _ShowReport()
        {
            string id = shopid; //keep old data before            
            shopid = cboShop.SelectedValue.ToString() == "" ? "*" : cboShop.SelectedValue.ToString();
            if (shopid == "-") shopid = "*";
            string custgroup = cboCustGroup.Visible ? cboCustGroup.SelectedValue.ToString() : ClsData.GetValueXML("customer", "OID='" + shopid + "'", "GroupID");
            if (custgroup == "") custgroup = cApp.shop_group;

            DataTable shops = ClsData.ShopData(custgroup);
            List<string> chkList = shops.AsEnumerable()
                                       .Select(r => r.Field<string>("oid"))
                                       .ToList();
            DataTable dt = new DataTable();
            var lists = ClsData.GetXMLTableList(shopid + "_*_*");
            if(lists.Count>0)
            {
                /*
                    XDocument doc=new XDocument();
                    for(int i=0;i<=lists.Count;i++)
                    {
                        try
                        {
                            string fname = lists[i].filename;
                            string shopno = fname.Split('_')[0];
                            bool bPass = shopid == "*" ? true : Convert.ToBoolean(shopid == shopno);
                            bPass = custgroup == "" ? bPass : chkList.Contains(shopno);
                            if(bPass)
                            {
                                if (doc.Root==null)
                                {
                                    doc = XDocument.Load(ClsData.GetPath() + fname);
                                }
                                else
                                {
                                    doc.Root.Add(XDocument.Load(ClsData.GetPath() + fname).Root.Elements());
                                }
                            }
                        }
                        catch { }
                    }
                    */
                dt = ClsData.GetDataXML("TempReport");
                DataTable tb = new DataTable();                
                string sortby = "";

                //dt = ClsData.GetDataTableFromXML(doc.ToString(), "Table");
                //dt.Columns.Add(new DataColumn() { ColumnName = "shopid", Expression = "" });
                dt.DefaultView.RowFilter = GetCliteria();
                switch (cboSumType.SelectedIndex)
                {
                    case 1: //by user
                        tb = ClsData.NewReportSales(new DataSet());
                        sortby = "entryBy";
                        dt.DefaultView.Sort = sortby;                        
                        this.ProcessSalesReport(dt.DefaultView.ToTable(), ref tb, sortby, ChkSummaryOnly.Checked,shops);
                        ClsData.SetReportSalesCaption(tb);
                        break;
                    case 2: //by 
                    case 3: //by Monthly
                        string reportField = "";
                        if (cboSumType.SelectedIndex == 2) reportField = "SalesOut";
                        if (cboSumType.SelectedIndex == 3) reportField = "Qty";
                        tb = ClsData.NewReportDaily(Convert.ToDateTime(txtDateF.Text), Convert.ToDateTime(txtDateT.Text));
                        sortby = "salesDate";
                        dt.DefaultView.Sort = sortby;
                        dt.DefaultView.RowFilter = "salesDate>='" + txtYear.Text + "-01-01' and salesDate<='" + txtYear.Text + "-12-31'";
                        this.ProcessSumReportByDate(dt.DefaultView.ToTable(), ref tb, ChkSummaryOnly.Checked, shops,reportField);
                        break;
                    case 4: //by quarter
                    case 5: //by yearly
                        ReportPeriod typereport = cboSumType.SelectedIndex == 4 ? ReportPeriod.Quarter : ReportPeriod.Monthly;
                        tb = ClsData.NewReportByPeriod(txtYear.Text,typereport );
                        foreach (DataRow shop in shops.Rows)
                        {
                            DataRow r = tb.NewRow();
                            r["Group"] = shop["CustName"].ToString();
                            r["Type"] = shop["oid"].ToString();
                            tb.Rows.Add(r);
                        }
                        sortby = "salesDate";
                        dt.DefaultView.Sort = sortby;
                        this.ProcessSumReportByPeriod(dt.DefaultView.ToTable(), ref tb, "salesOut", ChkSummaryOnly.Checked,typereport);
                        break;
                    case 6: //by model
                        tb = ClsData.NewReportByShopGroup(custgroup);
                        sortby = "ModelCode";
                        dt.DefaultView.Sort = sortby;
                        this.ProcessSumReportByModel(dt.DefaultView.ToTable(), ref tb, sortby, ChkSummaryOnly.Checked);
                        break;
                    default:
                        tb = ClsData.NewReportSales(new DataSet());
                        sortby = "RowID";
                        dt.DefaultView.Sort = sortby;
                        this.ProcessSalesReport(dt.DefaultView.ToTable(), ref tb, sortby,ChkSummaryOnly.Checked,shops);
                        ClsData.SetReportSalesCaption(tb);
                        break;
                }
                
                dt = tb;                
            }
            ViewState["ReportSales"] = dt;
            shopid = id; //return old data
            return dt;
        }
        protected void ProcessSumReportByModel(DataTable dtSrc, ref DataTable dtDest, string groupfield, bool sumonly)
        {

        }
        protected void ProcessSumReportByPeriod(DataTable dtSrc, ref DataTable dtDest, string fieldval, bool sumonly,ReportPeriod type)
        {
            if(type==ReportPeriod.Monthly)
            {
                DataTable dt = ClsData.NewReportSales(new DataSet());
                foreach(DataRow dr in dtDest.Rows)
                {
                    double total = 0;
                    for(int i=1;i<=12;i++)
                    {
                        string yy = dtDest.Columns[i + 1].ColumnName.ToString().Split('/')[1];
                        string mm = dtDest.Columns[i + 1].ColumnName.ToString().Split('/')[0];
                        double newval= 0;
                        if (dtSrc.Columns.IndexOf("val") < 0) dtSrc.Columns.Add(new DataColumn("val", typeof(double), "Convert("+fieldval+",'System.Double')"));
                        try { newval = Convert.ToDouble((object)dtSrc.Compute("Sum(val)", String.Format("RowID like '" + dr["Type"].ToString() + "_{0}{1}%' ", yy, mm))); } catch (Exception e) { string str = e.Message; }
                        total += newval;
                        if (newval > 0) dr[i + 1] = newval.ToString("#,###,##0.00");
                        /*                      
                                                foreach (DataRow r in dtSrc.Select("RowID like '" + dr["Type"].ToString() + "_%' " + string.Format("and salesDate Like '{0}-{1}%'", yy, mm))))
                                                {
                                                    double newval = 0;
                                                    double.TryParse(r[fieldval].ToString(), out newval);
                                                    total += newval;
                                                    double oldval = 0;
                                                    double.TryParse(dr[i+1].ToString(), out oldval);
                                                    newval = oldval + newval;
                                                    if (newval > 0) dr[i+1] = newval;
                                                }
                        */
                        dr["Total"] = total.ToString("#,###,##0.00");
                    }
                }
            }            
        }
        protected void ProcessSumReportByDate(DataTable dtSrc, ref DataTable dtDest, bool sumonly, DataTable rsShop,string fieldval)
        {
            int rowcount = 0;
            double total = 0;
            double grandtotal = 0;
            string fldGroup = sumonly==true?"groupid":"oid";
            DataRow r;
            DataRow t = dtDest.NewRow();
            t["Group"] = "Grand Total";
            foreach (DataRow shop in rsShop.Rows)
            {
                r = dtDest.NewRow();
                r["Group"] = shop["CustName"].ToString();
                foreach (DataRow dr in dtSrc.Select("RowID like '"+shop["oid"].ToString() +"_%'") )
                {
                    rowcount++;
                    var idx = dtDest.Columns.IndexOf(dr["salesDate"].ToString().Replace("-",""));
                    double newval = 0;
                    double.TryParse(dr[fieldval].ToString(), out newval);
                    if (idx>0)
                    {
                        total += newval;
                        double oldval = 0;
                        double.TryParse(r[idx].ToString(), out oldval);
                        newval = oldval + newval;
                        if(newval >0) r[idx] = newval;
                    }
                    grandtotal += newval;
                }
                r["Total"] = total.ToString("#,###,##0.00");
                r["GrandTotal"] = grandtotal.ToString("#,###,##0.00");
                dtDest.Rows.Add(r);
            }            
        }
        protected void ProcessSalesReport(DataTable dtSrc,ref DataTable dtDest, string groupfield,bool sumonly,DataTable rsShop)
        {

            string chk = null;
            double totalqty = 0;
            double totalsalesIn = 0;
            double totalsalesOut = 0;
            double totalPrice = 0;
            int rowcount = 0;
            string totalstr = "";

            foreach (DataRow dr in dtSrc.Rows)
            {
                rowcount++;
                if (chk == null) chk = (dr[groupfield].ToString() + "_").Split('_')[0];
                if (groupfield == "RowID")
                {
                    DataRow[] rec = rsShop.Select("OID='" + chk + "'");
                    if (rec.Length > 0)
                    {
                        totalstr = "รวม " + rec[0]["CustName"];
                    }
                    else
                    {
                        totalstr = "รวม " + chk;
                    }
                }
                else
                {
                    totalstr = "รวม " + chk;
                }
                if (chk != (dr[groupfield].ToString() + "_").Split('_')[0])
                {
                    AddSubTotal(dtDest, totalstr, totalqty, totalPrice, totalsalesIn, totalsalesOut);

                    totalqty = 0;
                    totalPrice = 0;
                    totalsalesIn = 0;
                    totalsalesOut = 0;

                    chk = (dr[groupfield].ToString() + "_").Split('_')[0];
                }
                if (sumonly == false)
                {
                    dtDest.ImportRow(dr);
                }
                try { totalqty += Convert.ToDouble(dr["Qty"]); } catch { }
                try { totalsalesIn += Convert.ToDouble(dr["SalesIn"]); } catch { }
                try { totalsalesOut += Convert.ToDouble(dr["SalesOut"]); } catch { }
                try { totalPrice += (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["TagPrice"])); } catch { }
                if (rowcount == dtSrc.Rows.Count)
                {
                    AddSubTotal(dtDest, totalstr, totalqty, totalPrice, totalsalesIn, totalsalesOut);
                }
            }
            ClsData.AddSummaryReportSales(dtDest, sumonly);

        }
        protected void AddSubTotal(DataTable dt,string caption,double totalqty,double totalPrice,double totalsalesIn,double totalsalesOut)
        {
            DataRow r = dt.NewRow();
            r["salesDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            r["Model"] = caption;
            if (totalqty > 0)
            {
                r["Qty"] = totalqty;
                r["TagPrice"] = totalPrice.ToString("#,###,##0.00");
                r["SalesIn"] = totalsalesIn.ToString("#,###,##0.00");
                r["SalesOut"] = totalsalesOut.ToString("#,###,##0.00");
            }
            dt.Rows.Add(r);
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
            if(cboSumType.SelectedIndex==6)
            {

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
            if (cboCounter.SelectedIndex > 0)
            {
                if (str != "") str += " AND ";
                str += "CounterType='" + cboCounter.SelectedItem.Text + "'";
                cliteria += "เค้าท์เตอร์=" + cboCounter.SelectedItem.Text + " ";
            }
            if (cboArea.SelectedIndex > 0)
            {
                if (str != "") str += " AND ";
                str += "Area='" + cboArea.SelectedItem.Text + "'";
                cliteria += "ภาค=" + cboArea.SelectedItem.Text + " ";
            }
            if (cboSalesCode.SelectedIndex > 0)
            {
                if (str != "") str += " AND ";
                str += "salesCode='" + cboSalesCode.SelectedItem.Text + "'";
                cliteria += "พนักงานขาย=" + cboSalesCode.SelectedItem.Text + " ";
            }
            if (cboSupCode.SelectedIndex > 0)
            {
                if (str != "") str += " AND ";
                str += "supCode='" + cboSupCode.SelectedItem.Text + "'";
                cliteria += "PC Supervisor=" + cboSupCode.SelectedItem.Text + " ";
            }
            if (cboShop.SelectedIndex>0)
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
            string filename = cApp.user_id + "_"+ DateTime.Now.AddHours(7).ToString("yyyyMMddHHmmss") + ".xml";
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
            //Session["rptData"] = _ShowReport();
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