using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class SendStock : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        Table Table1 = new Table();
        static bool isCheck = false;
        static bool isShowAll = true;
        static bool isOnePrice = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name != "" && cApp.user_role != "1")
            {
                Panel1.Controls.Clear();
                lblUsername.Text = cApp.user_name;
                if (!IsPostBack)
                {
                    isShowAll = true;
                    chkShowAll.Checked = isShowAll;
                    if (cApp.user_role != "0")
                    {
                        LoadShop(cApp.shop_group);
                    }
                    else
                    {
                        LoadShop("");
                        cboShop.SelectedValue = "-";
                    }
                    //txtYear.Text = DateTime.Now.Year.ToString("0000");
                    //txtMonth.Text = DateTime.Now.Month.ToString("00");
                    cboStatus.Items.Add("PC สั่งของ");
                    cboStatus.Items.Add("ตรวจสอบแล้ว");
                    cboStatus.Items.Add("อนุมัติส่งของแล้ว");
                    cboStatus.Items.Add("ยืนยันรับสินค้าแล้ว");
                    cboStatus.Items.Add("ทั้งหมด");

                    cboType.Items.Add("สินค้าปกติ");
                    cboType.Items.Add("สินค้า One Price");
                    //ClsData.LoadProdType(cboTypeProduct);
                    txtDate.Text = cApp.working_date;
                    LoadGrid();
                }
                else
                {
                    if (Session["tbStock"] != null)
                    {
                        Table1 = (Table)Session["tbStock"];
                    }
                }
                Panel1.Controls.Add(Table1);
            }
            else
            {
                Response.Redirect("index.html");
            }
            ViewState["RefUrl"] = "menu.aspx";
        }
        protected void LoadShop(string groupid)
        {
            if (cboShop.DataTextField == "")
            {
                ClsData.LoadShopByGroup(cboShop, groupid, "custname", "oid", true, true);
                if (cApp.shop_id != "")
                {
                    cboShop.SelectedValue = cApp.shop_id;
                }
            }
        }
        protected string GetFileNameFromRef(string str)
        {
            string refno = str.Substring(3, str.Length - 4);
            string yymm = refno.Substring(0, 6);
            int shopno = Convert.ToInt32(refno.Substring(0,9).Replace(yymm, ""));
            return yymm + "st" + shopno;
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grid = (GridView)sender;
            if (e.CommandName == "Delete")
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    Int32 index = Convert.ToInt32(e.CommandArgument.ToString());
                    string refno = GetFileNameFromRef(grid.Rows[index].Cells[12].Text);
                    ClsData.UpdateDataXML(refno, "oid='" + grid.Rows[index].Cells[13].Text + "'", "TransactionState=DEL;");
                    LoadGrid();
                }
            }
        }
        protected DataTable GetDataSource(bool isForGrid,bool isShowall)
        {
            DataTable rs = ClsData.NewProductRequest(true);
            rs.Columns.Add(new DataColumn("ShopName",Type.GetType("System.String")));
            try
            {
                string addWhere = " AND NOT TransactionState='DEL' ";
                if (isShowall == false)
                {
                    addWhere += " And TransactionDate='" + txtDate.Text + "'";
                }
                switch (cboStatus.SelectedIndex)
                {
                    case 0:
                        addWhere += " AND StockType ='0' AND TransactionState='PCR' ";
                        break;
                    case 1:
                        addWhere += " AND ConfirmCode='' AND ApproveCode='' AND NOT AcceptCode='' AND TransactionState='STI' ";
                        break;
                    case 2:
                        addWhere += " AND ConfirmCode='' AND NOT ApproveCode='' AND TransactionState='STI' ";
                        break;
                    case 3:
                        addWhere += " AND NOT ConfirmCode='' AND NOT ApproveCode='' AND NOT AcceptCode='' AND TransactionState='STI' ";
                        break;
                }
                if(!isForGrid)
                {
                    if (!isOnePrice)
                    {
                        addWhere += " And TransactionType Like '%ปกติ%' ";
                    }
                    else
                    {
                        addWhere += " And TransactionType Like '%One Price%'  ";
                    }
                }
                DataTable dtShop = ClsData.ShopData();
                DataRowCollection shops;
                if(cboShop.SelectedIndex>0)
                {
                    DataView dv = dtShop.DefaultView;
                    dv.RowFilter = "OID=" + cboShop.SelectedValue.ToString();
                    dtShop=dv.ToTable();
                }
                shops = dtShop.Rows;
                foreach (DataRow shop in shops)
                {
                     foreach(XMLFileList file in ClsData.GetXMLTableList(getYYMM() + "st" + shop["oid"].ToString()))
                    {
                        string fname = file.filename;
                        if (System.IO.File.Exists(ClsData.GetPath() + fname))
                        {
                            DataTable dt = ClsData.QueryData(fname.Replace(".xml",""), "RefNo Like '%' " + addWhere);
                            foreach (DataRow dr in dt.Rows)
                            {
                                DataRow row = rs.NewRow();
                                row["ShopName"] = shop["CustName"].ToString();
                                foreach (DataColumn col in rs.Columns)
                                {
                                    if (dt.Columns.IndexOf(col.ColumnName) >= 0)
                                    {
                                        row[col.ColumnName] = dr[col.ColumnName];
                                    }
                                }
                                rs.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
            rs.DefaultView.Sort = "TransactionType,RefNo";
            return rs.DefaultView.ToTable();
        }
        protected void LoadTable()
        {
            DataTable dt = ClsData.NewProductRequest(true);
            Table1.Rows.Clear();
            Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "h_", "", true));
        }
        protected void LoadData()
        {
            LoadTable();
            DataTable dt = GetDataSource(false,isShowAll);
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "D_", i.ToString(), false, i));
                i++;
            }
            Session["tbStock"] = Table1;
            lblMessage.Text = "Ready";
        }
        protected void LoadGrid()
        {
            //GridView1.DataSource = ClsData.SetReportStockCaption(GetDataSource(false,ShowAll));
            //GridView1.DataBind();
            //GridView2.DataSource = ClsData.SetReportStockCaption(GetDataSource(true,ShowAll));
            //GridView2.DataBind();
            ASPxGridView1.DataBind();
            (ASPxGridView1.Columns[15] as GridViewDataColumn).GroupBy();
            (ASPxGridView1.Columns[13] as GridViewDataColumn).GroupBy();
            ASPxGridView1.Columns[11].SetColVisible(false);
            ASPxGridView1.Columns[12].SetColVisible(false);
        }
        protected string getYYMM()
        {
            if(isShowAll==false)
            {
                return txtDate.Text.Substring(0, 7).Replace("-", "");
            }
            else
            {
                return "*";
            }
        }

        protected string getFileName(string shopno)
        {
            string fname = "";
            try
            {                
                fname = getYYMM() + "st" + shopno;
            }
            catch
            {

            }
            return fname;
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            chkShowAll.Checked = false;
            isShowAll = false;          
            LoadGrid();
        }
        private void ShowDetail(bool oneprice)
        {
            isOnePrice = oneprice;
            placeHeader.Visible = false;
            placeDetail.Visible = true;
            cboType.SelectedIndex = (oneprice ==true ? 1 : 0);
            txtStockDate.Text = txtDate.Text;
            LoadData();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ShowDetail(false);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsData.NewProductRequest(true);
            Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "D_", Table1.Rows.Count.ToString(), false));
            Session["tbStock"] = Table1;
        }
        protected Running SaveData(string oid, string refno,string model,string color,string size,string prodcode,int qty,double price,string stockdate,string prodcat,string apprcode,string fname,string shopno)
        {
            Running msg= new Running();
            try
            {
                DataTable dt = ClsData.GetStockData(ClsData.GetPath() + fname + ".xml");
                if (refno == "")
                {
                    oid = ClsData.GetNewOID(dt, fname, "OID");
                    refno = "STI" + getYYMM() + Convert.ToInt32(shopno).ToString("000") + Convert.ToInt32(oid).ToString("0000");
                }
                else
                {
                    oid = Convert.ToInt32(refno.Substring(refno.Length - 4, 4)).ToString();
                }
                DataRow r = ClsData.QueryData(dt, "RefNo='" + refno + "'");
                if (r["OID"].ToString() == "")
                {
                    r["oid"] = oid;
                    r["GPx"] = 0;
                    r["ShareDiscount"] = 0;
                    r["DiscountRate"] = 0;
                    r["TransactionBy"] = cApp.user_id;
                    r["StockType"] = 1;
                    r["TransactionDate"] = cApp.CurrentDate();
                    r["ProdQty"] = qty;
                    r["TransactionType"] = "เติมสินค้าปกติ";
                }
                r["RefNo"] = refno;
                if(prodcode!="")
                {
                    r["TransactionState"] = "STI";
                    r["TransactionType"] = r["TransactionType"].ToString().Replace("สั่ง","เติม");
                    r["AcceptCode"] = cApp.user_id;
                }
                r["StockDate"] = stockdate;
                r["ApproveCode"] = apprcode;
                r["ConfirmCode"] = r["ConfirmCode"].ToString();
                r["ModelCode"] = model;
                r["Color"] = color;
                r["SizeNo"] = size;
                r["GoodsCode"] = prodcode;
                if(prodcat!="") r["ProdCatName"] = prodcat;
                r["TagPrice"] = price;
                r["StockQty"] = qty;
                r["Cal"] = 1 - (Convert.ToDouble(r["GPx"]) + ((Convert.ToDouble(r["DiscountRate"]) / 100) * Convert.ToDouble(r["ShareDiscount"])));
                r["SalesIn"] =((Convert.ToDouble(r["StockQty"]) * Convert.ToDouble(r["TagPrice"])) * Convert.ToDouble(r["Cal"])) * Convert.ToDouble(r["StockType"]);
                r["SalesOut"] = (Convert.ToDouble(r["StockQty"]) * Convert.ToDouble(r["TagPrice"])) * Convert.ToDouble(r["StockType"]);                
                r["UPriceOut"] = (Convert.ToDouble(r["SalesOut"]) / Convert.ToDouble(r["StockQty"])) * Convert.ToDouble(r["StockType"]);
                r["UPriceIn"] = (Convert.ToDouble(r["SalesIn"]) / Convert.ToDouble(r["StockQty"])) * Convert.ToDouble(r["StockType"]);
                r["StockIn"] = (Convert.ToDouble(r["UPriceIn"]) * Convert.ToDouble(r["StockQty"])) * Convert.ToDouble(r["StockType"]);
                r["StockOut"] = (Convert.ToDouble(r["UPriceOut"]) * Convert.ToDouble(r["StockQty"])) * Convert.ToDouble(r["StockType"]);
                if (r.RowState == DataRowState.Detached) dt.Rows.Add(r);
                dt.WriteXml(ClsData.GetPath() + fname + ".xml");
                msg.OID = r["OID"].ToString();
                msg.RunningNo = r["RefNo"].ToString();
                msg.Message = "OK";
            }
            catch (Exception ex)
            {
                msg.Message = "ERROR:"+ ex.Message;
            }
            return msg;
        }
        protected int SaveAll()
        {
            int i = 1;
            int j = 0;

            foreach (TableRow tr in Table1.Rows)
            {
                if (CDymamicForm.getDataFromRow(tr, "chkD" + i.ToString()) == "True")
                {
                    string id = CDymamicForm.getDataFromRow(tr, "fldD_OID" + i.ToString());
                    string refno = CDymamicForm.getDataFromRow(tr, "fldD_RefNo" + i.ToString());
                    string shopno = "";
                    string fname = "";
                    if(refno=="" && cboShop.SelectedIndex>0)
                    {
                        shopno = cboShop.SelectedValue.ToString();
                        fname = getFileName(shopno);
                    }
                    else
                    {
                        fname = GetFileNameFromRef(refno);
                        shopno = fname.Split('t')[1].ToString();
                    }                    
                    string model = CDymamicForm.getDataFromRow(tr, "fldD_ModelCode" + i.ToString());
                    string color = CDymamicForm.getDataFromRow(tr, "fldD_Color" + i.ToString(), "", true);
                    string colorname = CDymamicForm.getDataFromRow(tr, "fldD_Color" + i.ToString());
                    string size = CDymamicForm.getDataFromRow(tr, "fldD_SizeNo" + i.ToString());
                    string stockdate = CDymamicForm.getDataFromRow(tr, "fldD_StockDate" + i.ToString());
                    string apprcode = CDymamicForm.getDataFromRow(tr, "fldD_ApproveCode" + i.ToString());
                    string prodCode = "";
                    if (size != "" && model != "" && color != "")
                    {
                        prodCode = model + color + (Convert.ToInt32(size) * 10).ToString("000");
                    }
                    DataRow prod = ClsData.ShoeDataByCode(prodCode);
                    string prodcat = prod["STName"].ToString();
                    if(prodcat=="")
                    {
                        prodcat= CDymamicForm.getDataFromRow(tr, "fldD_ProdCatName" + i.ToString());
                    }
                    double price = Convert.ToDouble(CDymamicForm.getDataFromRow(tr, "fldD_TagPrice" + i.ToString(), "0"));
                    int qty = Convert.ToInt32(CDymamicForm.getDataFromRow(tr, "fldD_StockQty" + i.ToString(), "0"));

                    Running result = SaveData(id, refno, model, colorname, size, prodCode, qty, price, stockdate,prodcat, apprcode,fname,shopno);
                    if (result.Message == "OK")
                    {
                        CDymamicForm.setDataInRow(tr, "fldD_OID" + i.ToString(), result.OID);
                        CDymamicForm.setDataInRow(tr, "fldD_RefNo" + i.ToString(), result.RunningNo);
                        j++;
                    }
                    else
                    {
                        CDymamicForm.setDataInRow(tr, "fldD_OID" + i.ToString(), "FAILED");
                        CDymamicForm.setDataInRow(tr, "fldD_RefNo" + i.ToString(), result.Message);
                    }
                }
                i++;
            }

            return j;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int j=SaveAll();
            Session["tbStock"] = Table1;
            if(j>0) lblMessage.Text = "Save Complete! " + j.ToString() + " Rows";
        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            placeHeader.Visible = true;
            placeDetail.Visible = false;
            Table1.Rows.Clear();
            Session["tbStock"] = null;
            LoadGrid();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            isCheck = !isCheck;
            int i = 1;
            foreach(TableRow tr in Table1.Rows) 
            {
                CDymamicForm.setDataInRow(tr, "chkD"+i.ToString(), isCheck.ToString());
                i++;
            }
            Session["tbStock"] = Table1;
        }
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            int i = 1;
            string prodCode = "";
            foreach (TableRow tr in Table1.Rows)
            {
                if(CDymamicForm.getDataFromRow(tr, "chkD" + i.ToString()) == "True")
                {
                    string model = CDymamicForm.getDataFromRow(tr, "fldD_ModelCode" + i.ToString());
                    string color = CDymamicForm.getDataFromRow(tr, "fldD_Color" + i.ToString(), "", true);
                    string size = CDymamicForm.getDataFromRow(tr, "fldD_SizeNo" + i.ToString(), "0");
                    if(model!="" && size!="")
                    {
                        prodCode = model + color + (Convert.ToInt32(size) * 10).ToString("000");
                        DataRow prod = ClsData.ShoeDataByCode(prodCode);
                        string price = prod["StdSellPrice"].ToString();
                        string prodcat = prod["ProdCatName"].ToString();
                        if(prodcat!="") CDymamicForm.setDataInRow(tr, "fldD_ProdCatName" + i.ToString(), prodcat);
                        CDymamicForm.setDataInRow(tr, "fldD_TagPrice" + i.ToString(), price);
                        string qty = CDymamicForm.getDataFromRow(tr, "fldD_ProdQty" + i.ToString(), "0");
                        CDymamicForm.setDataInRow(tr, "fldD_StockQty" + i.ToString(), qty);
                    }
                }
                i++;
            }
            i= SaveAll();
            if(i>0)
            {
                lblMessage.Text = i.ToString() + " Rows Updated!";
            }
            Session["tbStock"] = Table1;
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int i = 1;
            int j = 0;
            foreach (TableRow tr in Table1.Rows)
            {
                if (CDymamicForm.getDataFromRow(tr, "chkD" + i.ToString()) == "True")
                {
                    string oid = CDymamicForm.getDataFromRow(tr, "fldD_OID" + i.ToString());
                    if(oid!="")
                    {
                        CDymamicForm.setDataInRow(tr, "fldD_ApproveCode" + i.ToString(), cApp.user_id);
                        string qty = CDymamicForm.getDataFromRow(tr, "fldD_ProdQty" + i.ToString(), "0");
                        CDymamicForm.setDataInRow(tr, "fldD_StockQty" + i.ToString(), qty);
                    }
                }
                i++;
            }
            try
            {
                j = SaveAll();
                if(j>0) lblMessage.Text = j.ToString() + " Rows Approved!";
                Session["tbStock"] = Table1;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }            
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void cboTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void btnEdit2_Click(object sender, EventArgs e)
        {
            ShowDetail(true);
        }
/*
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string fname = "RequestOnePrice" + cApp.shop_id + ".xml";
            DataTable dt = ClsData.SetReportStockCaption(GetDataSource(true,));
            ClsData.ExportToXMLFile(ClsData.GetPath() + fname, dt);
            ClsData.Download(fname, "~/" + fname);
        }

        protected void btnDownLoad_Click1(object sender, EventArgs e)
        {
            string fname = "RequestStdPrice" + cApp.shop_id + ".xml";
            DataTable dt = ClsData.SetReportStockCaption(GetDataSource(true,false));
            ClsData.ExportToXMLFile(ClsData.GetPath() + fname, dt);
            ClsData.Download(fname, "~/" + fname);
        }
*/
        protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboType.SelectedIndex!=0)
            {
                isOnePrice = true;
            }
            else
            {
                isOnePrice = false;
            }
            LoadData();
        }

        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource=ClsData.SetReportStockCaption(GetDataSource(true,isShowAll));
            ASPxGridView1.KeyFieldName = "OID";
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void cboShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            isShowAll = chkShowAll.Checked;
            LoadGrid();
        }

        protected void btnSetDate_Click(object sender, EventArgs e)
        {
            int i = 1;
            int j = 0;
            foreach (TableRow tr in Table1.Rows)
            {
                if (CDymamicForm.getDataFromRow(tr, "chkD" + i.ToString()) == "True")
                {
                    string oid = CDymamicForm.getDataFromRow(tr, "fldD_OID" + i.ToString());
                    if (oid != "")
                    {
                        CDymamicForm.setDataInRow(tr, "fldD_StockDate" + i.ToString(), txtStockDate.Text);
                    }
                }
                i++;
            }
            try
            {
                j = SaveAll();
                if (j > 0) lblMessage.Text = j.ToString() + " Rows Set Complete!";
                Session["tbStock"] = Table1;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
    }
}