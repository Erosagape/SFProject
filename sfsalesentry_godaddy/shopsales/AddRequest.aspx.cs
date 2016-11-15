using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class addrequest : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        Table Table1 = new Table();
        static bool isCheck = false;
        static bool isOnePrice = false;
        static List<string> LOV_Goods;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session ["cApp"]!=null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
                if (cApp.user_name == "")
                {
                    Response.Redirect("index.html", true);
                }
                else
                {
                    lblUsername.Text = cApp.user_name;
                    lblShopName.Text = cApp.shop_name;
                    Panel1.Controls.Clear();
                    if (!IsPostBack)
                    {
                        isOnePrice = false;
                        txtDate.Text = cApp.working_date;
                        if (Request.QueryString["type"] != null)
                        {
                            if (Request.QueryString["type"].ToString() == "1")
                            {
                                isOnePrice = true;
                            }
                            AddNewRow();
                        }
                        LoadTable();
                        LoadGoods();
                        if (Request.QueryString["mode"] != "Add")
                        {
                            LoadData(GetDataSource(false, isOnePrice,true), true);
                        }
                        else
                        {
                            btnConfirm.Visible = false;
                        }
                        if(isOnePrice==true)
                        {
                            txtModel.Visible = false;
                            btnLoadGoods.Visible = false;
                        }
                        Session["cApp"] = cApp;
                    }
                    else
                    {
                        Table1 = (Table)Session["tbRequest"];
                    }
                    Panel1.Controls.Add(Table1);                    
                    ViewState["RefUrl"] = "menu.aspx";
                }
            }
            else
            {
                Response.Redirect("index.html");
            }         
        }
        protected void chkShowReceived_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(GetDataSource(false, isOnePrice, false), true);
            btnConfirm.Visible = chkShowReceived.Items[0].Selected;
        }
        protected DataTable GetDataStructure(bool isOnePrice)
        {
            DataTable dt = new DataTable();
            bool chk=chkShowReceived.Items[0].Selected;
            if(!isOnePrice)
            {
                dt = ClsData.NewProductRequest(chk,chk);
            }
            else
            {
                dt = ClsData.NewOnePriceRequest(chk,chk);
            }
            return dt;
        }
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        private void AddNewRow()
        {
            DataTable dt = GetDataStructure(isOnePrice);
            Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "D_", Table1.Rows.Count.ToString(), false));
            Session["tbRequest"] = Table1;
        }
        private DataTable GoodsList()
        {
            DataTable dt = ClsData.ShoeData();
            DataTable rs = new DataTable();
            rs.Columns.Add(new DataColumn("GoodsCode", Type.GetType("System.String")));
            rs.Columns.Add(new DataColumn("GoodsName", Type.GetType("System.String")));
            foreach(DataRow dr in dt.Rows)
            {
                DataRow r = rs.NewRow();
                r[0] = dr["GoodsCode"].ToString();
                r[1] = dr["GoodsName"].ToString();
                rs.Rows.Add(r);
            }
            return rs;
        }
        private DataTable GetDataSource(bool forgrid=false,bool isOnePrice=false,bool isShowAll=false)
        {
            string yymm = txtDate.Text.Substring(0, 7).Replace("-", "");
            DataTable dt = ClsData.NewStockData();
            DataTable rs = new DataTable();
            try
            {
                string addWhere = " And NOT TransactionState='DEL' ";
                if (chkShowReceived.Items[0].Selected == false)
                {
                    if (isShowAll == false) addWhere += "And TransactionDate='" + txtDate.Text + "'";
                    addWhere += " And StockType='0' And ApproveCode='' ";
                    //addWhere += " And ApproveCode='' ";
                }
                else
                {
                    if (isShowAll == false) addWhere += "And StockDate='" + txtDate.Text + "'";
                    addWhere += " And StockType='0' And ApproveCode<>'' ";
                    //addWhere += " And ApproveCode<>'' ";
                }
                string type = "ปกติ";
                if(isOnePrice==true)
                {
                    type = "One Price";
                }
                addWhere += " And TransactionType like '%" + type + "%' ";
                if (forgrid == true)
                {
                    rs = ClsData.NewProductRequest(chkShowReceived.Items[0].Selected,true);
                }
                else
                {
                    rs = GetDataStructure(isOnePrice);
                }
                string sqlw = "RefNo like '%" + Convert.ToInt32(cApp.shop_id).ToString("000") + "%' " + addWhere;
                foreach (DataRow file in ClsData.XMLTableData("*st" + cApp.shop_id).Rows)
                {

                    dt = ClsData.QueryData(file["filename"].ToString(), sqlw);
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow row = rs.NewRow();
                        foreach (DataColumn col in rs.Columns)
                        {
                            row[col] = dr[col.ColumnName];
                        }
                        rs.Rows.Add(row);
                    }

                }
            }
            catch
            {

            }
            return rs;
        }
        private void ConfirmData(string oid)
        {
            DataTable dt = ClsData.GetStockData(ClsData.GetPath() + ClsData.GetStockFileName(txtDate.Text, cApp.shop_id) + ".xml");
            if (dt.Rows.Count > 0)
            {
                DataRow r = ClsData.QueryData(dt, "oid='" + oid + "'");
                if (r["oid"].ToString() != "")
                {
                    r["StockType"] = 1;
                    r["ConfirmCode"] = cApp.user_id;
                    r["TransactionState"] = "STI";
                    string postfix = "ปกติ";
                    if(isOnePrice)
                    {
                        postfix = " One Price";
                    }
                    r["TransactionType"] = "เติมสินค้า" + postfix;
                    r["SalesOut"] = Convert.ToDouble(r["StockQty"]) * Convert.ToDouble(r["TagPrice"]);
                    r["StockQty"] = Convert.ToDouble(r["StockQty"]) * Convert.ToDouble(r["StockType"]);
                    r["Cal"] = 1 - (Convert.ToDouble(r["GPx"]) + ((Convert.ToDouble(r["DiscountRate"]) / 100) * Convert.ToDouble(r["ShareDiscount"])));
                    r["SalesIn"] = (Convert.ToDouble(r["StockQty"]) * Convert.ToDouble(r["TagPrice"])) * Convert.ToDouble(r["Cal"]);
                    r["UPriceOut"] = Convert.ToDouble(r["SalesOut"]) / Convert.ToDouble(r["StockQty"]);
                    r["UPriceIn"] = Convert.ToDouble(r["SalesIn"]) / Convert.ToDouble(r["StockQty"]);
                    r["StockIn"] = Convert.ToDouble(r["UPriceIn"]) * Convert.ToDouble(r["StockQty"]);
                    r["StockOut"] = Convert.ToDouble(r["UPriceOut"]) * Convert.ToDouble(r["StockQty"]);
                    dt.WriteXml(ClsData.GetPath() + ClsData.GetStockFileName(txtDate.Text, cApp.shop_id) + ".xml");
                }
            }

        }

        private void LoadTable()
        {
            DataTable dt =GetDataStructure(isOnePrice);
            Table1.Rows.Clear();
            Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "h_", "", true));
            if(isOnePrice)
            {
                Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "D_", Table1.Rows.Count.ToString(), false));
            }
            Session["tbRequest"] = Table1;
            lblMessage.Text = "Ready";
        }
        private void LoadData(DataTable dt,bool chk)
        {
            Table1.Rows.Clear();
            Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "h_", "", true));
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                Table1.Rows.Add(CDymamicForm.NewTableRow(dt, "D_", i.ToString(), false, i,chk));
                i++;
            }
            Session["tbRequest"] = Table1;
            lblMessage.Text = "Ready";
        }
        Running SaveData(string oid,string refno,string model,string color,string colorname,string size,int qty,string prodcat)
        {
            Running chk = new Running();
            string msg = "";
            try
            {
                string prodcode = ClsData.GetGoodsCode(model,color, size);
                string fname = ClsData.GetStockFileName(txtDate.Text,cApp.shop_id);
                string yymm = txtDate.Text.Substring(0, 7).Replace("-","");
                DataTable dt = ClsData.GetStockData(ClsData.GetPath() + fname + ".xml");
                if(oid=="") oid = ClsData.GetNewOID(dt, fname, "OID");
                if(refno=="") refno = "STI" + yymm + Convert.ToInt32(cApp.shop_id).ToString("000") + Convert.ToInt32(oid).ToString("0000");
                DataRow r = ClsData.QueryData(dt, "RefNo='" + refno + "'");
                if (r["OID"].ToString() == "") r["oid"] = oid;
                r["ModelCode"] = model;
                r["Color"] = colorname;
                r["SizeNo"] = size;
                if (r["StockType"].ToString() == "") r["StockType"] = 0;
                if (r["TransactionState"].ToString() == "")
                {
                    r["TransactionState"] = "PCR";
                }
                if (r["TransactionType"].ToString() == "")
                {
                    string postfix = "ปกติ";
                    if(isOnePrice)
                    {
                        postfix = " One Price";
                    }
                    r["TransactionType"] = "สั่งสินค้า" + postfix;
                }                    
                r["StockDate"] = r["StockDate"].ToString();
                if(prodcode !="" && prodcat=="")
                {
                    prodcat = ClsData.ShoeDataByCode(prodcode)["STName"].ToString();
                }
                r["ProdCatName"] = prodcat;
                r["GoodsCode"] = prodcode;
                if(chkShowReceived.Items[0].Selected==true)
                {
                    r["StockQty"] = qty;
                }
                else
                {
                    r["ProdQty"] = qty;
                }
                if (r["StockQty"].ToString() == "") r["StockQty"] = 0;
                if (r["TagPrice"].ToString() == "") r["TagPrice"] = 0;
                if (r["GPx"].ToString() == "") r["GPx"] = 0;
                if (r["DiscountRate"].ToString() == "") r["DiscountRate"] = 0;
                if (r["ShareDiscount"].ToString() == "") r["ShareDiscount"] = 0;
                if (r["Cal"].ToString() == "") r["Cal"] = 0;
                if (r["SalesIn"].ToString() == "") r["SalesIn"] = 0;
                if (r["SalesOut"].ToString() == "") r["SalesOut"] = 0;
                if (r["UPriceIn"].ToString() == "") r["UPriceIn"] = 0;
                if (r["UPriceOut"].ToString() == "") r["UPriceOut"] = 0;
                if (r["StockIn"].ToString() == "") r["StockIn"] = 0;
                if (r["StockOut"].ToString() == "") r["StockOut"] = 0;
                r["RefNo"] = refno;
                r["TransactionDate"] = txtDate.Text;
                r["TransactionBy"] = cApp.user_id;
                r["AcceptCode"] = r["AcceptCode"].ToString();
                r["ApproveCode"] = r["ApproveCode"].ToString();
                r["ConfirmCode"] = r["ConfirmCode"].ToString();

                if (r.RowState == DataRowState.Detached) dt.Rows.Add(r);
                dt.WriteXml(ClsData.GetPath() + fname + ".xml");
                chk.Message = "OK";
                chk.OID = oid;
                chk.RunningNo = refno;
            }
            catch (Exception e)
            {
                msg = e.Message;
                chk.Message = msg;
            }
            return chk;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(cApp.shop_id!="")
            {
                int i = 1;
                int j = 0;
                foreach (TableRow tr in Table1.Rows)
                {
                    bool chkPass = (CDymamicForm.getDataFromRow(tr, "chkD" + i.ToString()) == "True");
                    if(chkPass==true)
                    {
                        chkPass = (CDymamicForm.getDataFromRow(tr, "fldD_ProdQty" + i.ToString()) != "");
                    }
                    if(chkPass==true)
                    {
                        string id = CDymamicForm.getDataFromRow(tr, "fldD_OID" + i.ToString());
                        string refno = CDymamicForm.getDataFromRow(tr, "fldD_RefNo" + i.ToString());
                        string model = CDymamicForm.getDataFromRow(tr, "fldD_ModelCode" + i.ToString());
                        string color = CDymamicForm.getDataFromRow(tr, "fldD_Color" + i.ToString(), "", true);
                        string colorname = CDymamicForm.getDataFromRow(tr, "fldD_Color" + i.ToString());
                        string size = CDymamicForm.getDataFromRow(tr, "fldD_SizeNo" + i.ToString());
                        string prodcat = CDymamicForm.getDataFromRow(tr, "fldD_ProdCatName" + i.ToString());
                        int qty = 0;
                        if(chkShowReceived.Items[0].Selected==true)
                        {
                            qty = Convert.ToInt32(CDymamicForm.getDataFromRow(tr, "fldD_StockQty" + i.ToString(), "0"));
                        }
                        else
                        {
                            qty = Convert.ToInt32(CDymamicForm.getDataFromRow(tr, "fldD_ProdQty" + i.ToString(), "0"));
                        }
                        Running result = SaveData(id, refno, model, color, colorname, size, qty,prodcat);
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
                lblMessage.Text = "Save Complete! " + j.ToString() + " Rows";
            }
            Session["tbRequest"] = Table1;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
/*
            GridView grid = (GridView)sender;
            if (e.CommandName == "Del")
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    Int32 index = Convert.ToInt32(e.CommandArgument.ToString());
                    ClsData.UpdateDataXML(ClsData.GetStockFileName (txtDate.Text,cApp.shop_id), "oid='" + grid.Rows[index].Cells[7].Text + "'", "TransactionState=DEL;");
                    LoadGrid(true);
                }
            }
            if (e.CommandName == "Approve")
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    try
                    {
                        Int32 index = Convert.ToInt32(e.CommandArgument.ToString());
                        ConfirmData(grid.Rows[index].Cells[9].Text);
                    }
                    catch
                    {

                    }
                    LoadGrid(true);
                }
            }
*/
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
/*
            if (e.Row.RowIndex >= 0)
            {
                LinkButton btn = (LinkButton)e.Row.Cells[0].FindControl("LinkButton1");
                if (btn != null)
                {
                    if (chkShowReceived.Items[0].Selected == true)
                    {
                        btn.Text = "Confirm";
                        btn.CommandName = "Approve";
                    }
                    else
                    {
                        btn.Text = "Delete";
                        btn.CommandName = "Del";
                    }
                }
            }
*/
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            isCheck = !isCheck;
            int i = 1;
            foreach (TableRow tr in Table1.Rows)
            {
                CDymamicForm.setDataInRow(tr, "chkD" + i.ToString(), isCheck.ToString());
                i++;
            }
            Session["tbRequest"] = Table1;
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
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
                        ConfirmData(oid);
                        j++;
                    }
                }                
                i++;
            }
            lblMessage.Text = j.ToString() + " Rows Confirmed!";
            Session["tbRequest"] = Table1;
        }
        private void LoadGoods()
        {
            DataTable dtShoe = ClsData.ShoeData();
            LOV_Goods = new List<string>();
            foreach (DataRow r in dtShoe.Rows)
            {
                LOV_Goods.Add(string.Format("{0}|{1}", r["GoodsName"].ToString(), r["GoodsCode"].ToString()));
            }
        }
        [WebMethod]
        public static string[] GetGoods(string prefix)
        {
            List<string> goods = new List<string>();
            if (prefix != "")
            {
                goods = LOV_Goods.Where(s => s.StartsWith(prefix.ToUpper())).ToList();
            }
            else
            {
                goods = LOV_Goods;
            }
            goods.Sort();
            return goods.ToArray();
        }

        protected void btnLoadGoods_Click(object sender, EventArgs e)
        {
            DataTable dt =ClsData.NewProductRequest();
            List<string> goods = LOV_Goods.Where(s => s.StartsWith(txtModel.Text.ToUpper())).ToList();
            foreach(string str in goods)
            {
                DataRow dr = dt.NewRow();
                string goodscode = str.Split('|')[1].ToString();
                string goodsName = str.Split('|')[0].ToString();
                string model = goodsName.Split(' ')[0].ToString();
                string color = goodsName.Split(' ')[1].ToString(); 
                string size = goodsName.Split(' ')[2].ToString();
                dr["ModelCode"] = model;
                dr["Color"] = color;
                dr["SizeNo"] = size;
                if (goodscode != "")
                {
                    dr["ProdCatName"] = ClsData.ShoeDataByCode(goodscode)["STName"].ToString();
                }                
                dt.Rows.Add(dr);
            }
            LoadData(dt,true);
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            LoadData(GetDataSource(false, isOnePrice, false), true);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestStock.aspx");
        }
    }
}