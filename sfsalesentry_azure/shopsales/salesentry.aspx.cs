using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace shopsales
{
    public partial class salesentry1 : System.Web.UI.Page
    {
        private string salesdate = "";
        private string shopname = "";
        private string branch = "";
        private string note = "";
        private string entryby = "";
        private string shopid = "";
        private string prodid = "";
        private string sharediscount = "";
        private string gpx = "";
        private ClsSessionUser cApp = new ClsSessionUser();
        private static List<string> LOV_Goods = new List<string>();
        private static DataTable dtShoe;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["RefUrl"] = "menu.aspx";
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name != ""&&cApp.shop_id!="")
            { 
                entryby = cApp.user_id;
                salesdate = cApp.working_date;
                shopname = cApp.shop_name;
                sharediscount = cApp.share_discount;
                gpx = cApp.gpx_rate;
                branch = cApp.shop_branch;
                shopid = cApp.shop_id;
                note = cApp.shop_note;
                lblUsername.Text = "Welcome " + cApp.user_name;                
                if (txtDate.Text == "") txtDate.Text = salesdate;
                if (cApp.user_role != "0")
                {
                    linkAddGoods.Visible = false;
                }                    
                else
                {
                    txtDate.Enabled = true;
                }
                lblHead.Text = shopname + " สาขา " + branch.ToString();
                lblMessage.Text = "Ready";
                if (!IsPostBack)
                {
                    //set default visible
                    txtsalesDiscountPerc.Visible = false;
                    //goods
                    LoadGoods();
                }
                if (cbosalesType.DataTextField == "") ClsData.LoadSaleType(cbosalesType, "Description", "OID");
                if (cboColor.DataTextField == "") ClsData.LoadShoeColor(cboColor, "ColTh", "ColNameInit");
            }
            else
            {
                Response.Redirect("index.html", true);
            }
        }
        protected void LoadGoods()
        {
            dtShoe = ClsData.ShoeData();
            LOV_Goods.Clear();
            foreach (DataRow r in dtShoe.Rows)
            {
                LOV_Goods.Add(string.Format("{0}|{1}", r["GoodsName"].ToString(), r["GoodsCode"].ToString()));
            }
        }
        protected string CheckEntry()
        {
            string str = "";
            Double p = 0;
            if (Double.TryParse(txtsalesBuyPrice.Text, out p) == false)
            {
                str = "ราคาขายต้องเป็นตัวเลข";
            }
            if (Double.TryParse(txtsalesQty.Text, out p) == false)
            {
                str = "จำนวนต้องเป็นตัวเลข";
            }
            if (Double.TryParse(txtsalesTagPrice.Text, out p) == false)
            {
                if(cbosalesType.SelectedValue!="2")
                {
                    str = "ราคาป้ายต้องเป็นตัวเลข";
                }
            }
            if (Double.TryParse(txtSize.Text, out p) == false)
            {
                str = "ชนาดต้องเป็นตัวเลข";
            }
            if (txtsalesDiscountPerc.Visible ==true)
            {
                if (Double.TryParse(txtsalesDiscountPerc.Text, out p) == false)
                {
                    str = "ส่วนลดต้องเป็นตัวเลข";
                }
                else
                {
                    if (p < 0)
                    {
                        str = "ส่วนลดต้องเป็นตัวเลขมากกว่าหรือเท่ากับ 0";
                    }
                }
            }
            return str;
        }
        protected string SaveDataXML(string oid)
        {
            string err = CheckEntry();
            try
            {
                if (err == "")
                {
                    DataTable dt = ClsData.GetSalesData(MapPath("~/" + cApp.GetXMLFileName(txtDate.Text)));
                    if (dt.Columns.Count > 0)
                    {
                        DataRow dr = ClsData.QueryData(dt, "OID='" + oid + "'");
                        dr["OID"] = oid;
                        dr["salesDate"] = txtDate.Text;
                        salesdate = txtDate.Text;
                        dr["salesType"] = cbosalesType.SelectedValue.ToString();
                        if(cbosalesType.SelectedValue.ToString()=="3")
                        {
                            dr["discountRate"] = txtsalesDiscountPerc.Text;
                        }
                        else
                        {
                            dr["discountRate"] = 0;
                        }
                        dr["prodID"] = prodid;
                        dr["prodName"] = txtGoods.Value.ToUpper();
                        dr["ModelCode"] = txtModel.Text.ToUpper();
                        dr["ColorCode"] = cboColor.SelectedValue.ToString();
                        dr["ColorName"] = cboColor.SelectedItem.Text;
                        dr["prodCat"] = txtProdCat.Value.ToString();
                        dr["prodType"] = txtProdType.Value.ToString();
                        dr["prodGroup"] = txtProdGroup.Value.ToString();
                        dr["SizeNo"] = txtSize.Text;
                        dr["salesQty"] = txtsalesQty.Text;
                        dr["TagPrice"] = txtsalesTagPrice.Text;
                        dr["salesPrice"] = txtsalesBuyPrice.Text;
                        dr["shopName"] = shopname + branch;
                        dr["entryBy"] = entryby.ToUpper();
                        dr["remark"] = txtsalesRemark.Text;
                        dr["lastupdate"] = DateTime.Now.AddHours(7).ToString();
                        gpx = "100";
                        sharediscount ="0.00";
                        if (chkDiscouht.Checked == true)
                        { 
                            //if discount by value then find by based on no-discount
                            note = "ส่วนลดเงินสด;" + note;
                            Promotion p = ClsData.GetPromotionByDate(txtDate.Text, "1", shopid, 0);
                            if (p != null)
                            {
                                gpx = (p.GPRate() * 100).ToString();
                                sharediscount = p.ShareDiscount.ToString();
                            }
                        }
                        else
                        {
                            //load gpx And sharediscount
                            double discrate = 0;
                            if (dr["salesType"].ToString().Equals("3"))
                            {
                                try { discrate = Convert.ToDouble(dr["discountRate"]) / 100; } catch { }
                            }
                            //find gpx from promotion data
                            Promotion p = ClsData.GetPromotionByDate(txtDate.Text, dr["salesType"].ToString(), shopid, discrate);
                            if (p != null)
                            {
                                gpx = (p.GPRate() * 100).ToString();
                                sharediscount = p.ShareDiscount.ToString();
                            }
                        }
                        dr["ShareDiscount"] = sharediscount;
                        dr["Gpx"] = gpx;
                        dr["note"] = note;
                        dr["postFlag"] = "N";
                        if (dr.RowState == DataRowState.Detached) dt.Rows.Add(dr);
                        dt.WriteXml(MapPath("~/" + cApp.GetXMLFileName(txtDate.Text)));
                        err = "OK";
                    }
                    else
                    {
                        err = "ไม่สามารถสร้างข้อมูลได้";
                    }
                }
            }
            catch(Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }
        protected void LoadProducts(string code)
        {
            if (code != "")
            {
                DataRow dr = ClsData.ShoeDataByCode(code);
                if (dr["GoodsCode"].ToString() == "")
                {
                    txtGoods.Value = ClsData.GetGoodsName(txtModel.Text,cboColor.SelectedValue.ToString(),txtSize.Text);
                    if (cbosalesType.SelectedValue.ToString() == "2")
                    {
                        ShowTagPrice(true);
                        EnableData(true);
                        if (txtsalesBuyPrice.Text != txtsalesTagPrice.Text)
                        {
                            txtsalesTagPrice.Text = txtsalesBuyPrice.Text;
                        }
                        lblMessage.Text = "คุณกำลังทำรายการ " + txtGoods.Value.ToString() + "<br/>" + " ประเภทการขาย=" + cbosalesType.SelectedItem.Text + " ส่วนลด=" + cApp.iif(txtsalesDiscountPerc.Text == "", "0", txtsalesDiscountPerc.Text) + cApp.iif(chkDiscouht.Checked == false, "%", "บาท") + "<br/>จำนวน " + txtsalesQty.Text + " ราคาป้าย " + txtsalesTagPrice.Text + " ยอดขายวันที่ " + txtDate.Text;
                    }
                    else
                    {
                        ShowTagPrice(false);
                        EnableData(false);
                        lblMessage.Text = "ไม่พบข้อมูล (Code) =" + code;
                    }
                    txtProdCat.Value = "";
                    txtProdType.Value = "";
                    txtProdGroup.Value = "";
                }
                else
                {
                    ShowTagPrice(false);
                    string GoodsName = Request.Form[txtSearch.UniqueID];
                    if (GoodsName != dr["GoodsName"].ToString() && GoodsName != "")
                    {
                        lblMessage.Text = "ไม่พบข้อมูล (Name) =" + GoodsName;
                        //EnableData(false);
                        GoodsName = ClsData.GetGoodsName(txtModel.Text,cboColor.SelectedValue.ToString(),txtSize.Text,true);
                        txtGoods.Value = GoodsName;
                        //ClearData();
                        EnableData(false);
                    }
                    else
                    {
                        EnableData(true);
                        prodid = dr["OID"].ToString();
                        txtModel.Text = dr["ModelName"].ToString();
                        cboColor.SelectedValue = dr["ColNameInit"].ToString();
                        txtSize.Text = dr["SizeNo"].ToString();

                        txtProdCat.Value = dr["ProdCatId"].ToString();
                        txtProdType.Value = dr["STId"].ToString();
                        txtProdGroup.Value = dr["prodGroupName"].ToString();
                        txtsalesTagPrice.Text = dr["StdSellPrice"].ToString();
                        GoodsName = dr["GoodsName"].ToString();
                        txtGoods.Value = GoodsName;
                        //uncomment when stable for lock editing price
                        //txtsalesTagPrice.Enabled = false;
                        //txtsalesBuyPrice.Enabled = false;
                        if (cbosalesType.SelectedValue.ToString() == "1"|| cbosalesType.SelectedValue.ToString() == "3")
                        {
                            txtsalesBuyPrice.Text = dr["StdSellPrice"].ToString();
                        }
                        if (cbosalesType.SelectedValue.ToString() == "2")
                        {
                            txtsalesBuyPrice.Enabled = true;
                        }
                        CalculateDiscount();
                        lblMessage.Text = "คุณกำลังทำรายการ " + txtGoods.Value.ToString() + "<br/>" + " ประเภทการขาย=" + cbosalesType.SelectedItem.Text + " ส่วนลด=" + cApp.iif(txtsalesDiscountPerc.Text == "", "0", txtsalesDiscountPerc.Text) + cApp.iif(chkDiscouht.Checked == false, "%", "บาท") + "<br/>จำนวน " + txtsalesQty.Text + " ราคาป้าย " + txtsalesTagPrice.Text + " ยอดขายวันที่ " + txtDate.Text;

                    }
                }
            }
            else
            {
                lblMessage.Text = "กรุณาเลือกสินค้า";
                ClearData();
            }

        }
        protected void CalculateDiscount()
        {
            if (cbosalesType.SelectedIndex >= 0)
            {
                //calculate discount
                if (cbosalesType.SelectedValue.ToString() == "3")
                {
                    try
                    {
                        if (Convert.ToInt16(0 + txtsalesDiscountPerc.Text) > 0)
                        {
                            if (chkDiscouht.Checked == false)
                            {
                                Double rate = Convert.ToDouble(0 + txtsalesDiscountPerc.Text) / 100;
                                Double baseprice = Convert.ToDouble(0 + txtsalesTagPrice.Text);
                                Double discprice = Convert.ToDouble(0 + txtsalesTagPrice.Text) * rate;
                                txtsalesBuyPrice.Text = (baseprice - discprice).ToString();
                            }
                            else
                            {
                                Double baseprice = Convert.ToDouble(0 + txtsalesTagPrice.Text);
                                Double discprice = Convert.ToDouble(0 + txtsalesDiscountPerc.Text);
                                txtsalesBuyPrice.Text = (baseprice - discprice).ToString();
                            }
                        }
                    }
                    finally
                    {

                    }
                }
            }


        }
        protected void ClearData()
        {
            txtGoods.Value = "";
            txtsalesTagPrice.Text = "";
            txtsalesBuyPrice.Text = "";
            prodid = "0";
            txtOID.Value = "";
            txtModel.Text = "";
            cboColor.SelectedIndex = -1;
            txtProdType.Value = "";
            txtProdCat.Value = "";
            txtSize.Text = "";
            txtProdGroup.Value = "";
        }
        protected void ShowTagPrice(bool state)
        {
            lblTagPrice.Visible = state;
            txtsalesTagPrice.Visible = state;
            txtsalesTagPrice.Enabled = state;
        }
        protected void EnableData(bool state)
        {
            //txtsalesBuyPrice.Enabled = state;
            txtsalesQty.Enabled = state;
            //txtsalesDiscountPerc.Enabled = state;
            //cbosalesType.Enabled = state;
            btnSave.Enabled = state;
            txtModel.Enabled = state;
            cboColor.Enabled = state;
            txtSize.Enabled = state;
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtOID.Value != ""||cbosalesType.SelectedIndex==1)
            {
                if(txtOID.Value=="")
                {
                    txtOID.Value = ClsData.GetGoodsCode(txtModel.Text, cboColor.SelectedValue.ToString(), txtSize.Text);
                }
                DataRow dr = ClsData.ShoeDataByCode(txtOID.Value);
                prodid = "";
                if (dr["GoodsCode"].ToString() != "")
                {
                    prodid = dr["OID"].ToString();
                    txtGoods.Value = dr["ModelName"].ToString();
                    txtProdCat.Value = dr["ProdCatId"].ToString();
                    txtProdType.Value = dr["STId"].ToString();
                    txtProdGroup.Value = dr["prodGroupName"].ToString();

                    txtModel.Text = dr["ModelName"].ToString();
                    cboColor.SelectedValue = dr["ColNameInit"].ToString();
                    txtSize.Text = dr["SizeNo"].ToString();
                }
                else
                {
                    txtProdCat.Value = "";
                    txtProdType.Value = "";
                    txtProdGroup.Value = "";
                    txtGoods.Value = ClsData.GetGoodsName(txtModel.Text,cboColor.SelectedValue.ToString(),txtSize.Text,true);
                }
                if (ClsData.IsLockedDataTable(shopid + "_" + txtDate.Text.Replace("-", "") + "_" + cApp.user_id.ToLower(), cApp.user_id) == false)
                {
                    ClsData.LockDataTable(shopid + "_" + txtDate.Text.Replace("-", "") + "_" + cApp.user_id.ToLower(), cApp.user_id);
                    string key = shopid + "_" + txtDate.Text.Replace("-", "") + "_" + cApp.user_id.ToLower() + "_" + txtOID.Value;
                    string msg = SaveDataXML(key);
                    if (msg == "OK")
                    {
                        lblMessage.Text = "บันทึกข้อมูลเรียบร้อย => " + key;
                        btnSave.Enabled = false;
                        ClearData();
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('บันทีกข้อมูลเรียบร้อย " + key + "');", true);

                    }
                    else
                    {
                        lblMessage.Text = msg;
                    }
                    ClsData.UnlockDataTable(shopid + "_" + txtDate.Text.Replace("-", "") + "_" + cApp.user_id.ToLower(), cApp.user_id);
                }
                else
                {
                    lblMessage.Text = "มีคนใช้งานข้อมูลอยู่... กรุณารอสักครู่";
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string GoodsID = Request.Form[txtGoods.UniqueID];
            string GoodsName = Request.Form[txtSearch.UniqueID];
            lblMessage.Text = "Ready";
            LoadProductOID(GoodsID.ToString(),GoodsName.ToString());
            txtSearch.Text = "";
        }
        protected void LoadProductOID(string GoodsID,string GoodsName)
        {
            try
            {
                int i = 0;
                if (GoodsName != "")
                {
                    foreach (string str in GoodsName.Split(' '))
                    {
                        switch (i)
                        {
                            case 0: txtModel.Text = str; break;
                            case 1:
                                try
                                { cboColor.SelectedValue = ClsData.GetColorcodeByName(str.Trim()); }
                                catch { }
                                break;
                            case 2: txtSize.Text = str; break;
                            default: break;
                        }
                        i++;
                    }
                }
                if (GoodsID.ToString() != "")
                {
                    txtOID.Value = GoodsID;
                }
                else
                {
                    txtOID.Value = ClsData.GetGoodsCode(txtModel.Text, cboColor.SelectedValue.ToString(), txtSize.Text, true);
                }
            }
            catch
            {
                txtOID.Value = ClsData.GetGoodsCode(txtModel.Text, cboColor.SelectedValue.ToString(), txtSize.Text, true); 
            }
            LoadProducts(txtOID.Value);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("salesentry.aspx", true);

        }
        protected void cbosalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chkShareDiscount.Visible = true;
            txtsalesDiscountPerc.Visible = true;
            chkDiscouht.Visible = true;
            chkDiscouht.Checked = false;
            txtsalesBuyPrice.Enabled = false;
            if (cbosalesType.SelectedValue.ToString() !="3")
            {
                if(cbosalesType.SelectedValue.ToString()=="2"||cApp.user_role!="1")
                {
                    txtsalesBuyPrice.Enabled = true;
                    txtsalesTagPrice.Enabled = true;
                }
                chkDiscouht.Visible = false;
                txtsalesDiscountPerc.Visible = false;
                txtsalesDiscountPerc.Text = "";
            }
            txtGoods.Value = ClsData.GetGoodsName(txtModel.Text, cboColor.SelectedValue.ToString(), txtSize.Text, true);
            txtOID.Value = ClsData.GetGoodsCode(txtModel.Text, cboColor.SelectedValue.ToString(), txtSize.Text, true); 
            LoadProducts(txtOID.Value);
        }
    }
}