using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using DevExpress.Web;

namespace shopsales
{
    public partial class RequestStock : System.Web.UI.Page
    {
        ClsSessionUser cApp = new ClsSessionUser();
        Table Table1 = new Table();
        static bool isShowAll = true;
        static List<string> LOV_Goods;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
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
                    //Panel1.Controls.Clear();
                    if (!IsPostBack)
                    {
                        isShowAll = true;
                        txtDate.Text = cApp.working_date;
                        //LoadTable();
                        LoadGoods();                        
                        ASPxGridView1.DataBind();
                        (ASPxGridView1.Columns[7] as GridViewDataColumn).GroupBy();
                        
                        for (int i=5;i<ASPxGridView1.Columns.Count;i++)
                        {
                            ASPxGridView1.Columns[i].SetColVisible(false);
                        }
                        ASPxGridView1.Columns[8].SetColVisible(true);
                        cboProdType.Items.Add("Normal Order");
                        cboProdType.Items.Add("One Price");
                        btnConfirm.Visible = false;
                        Session["cApp"] = cApp;
                      }
                    else
                    {
                        Table1 = (Table)Session["tbRequest"];
                    }
                    //Panel1.Controls.Add(Table1);
                    ViewState["RefUrl"] = "menu.aspx";
                }
            }
            else
            {
                Response.Redirect("index.html");
            }
        }
        protected void CreateCommandCol()
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            GridViewCommandColumnCustomButton btn = new GridViewCommandColumnCustomButton();
            btn.ID = "btnConfirm";
            btn.Text = "Confirm";
            col.CustomButtons.Add(btn);
            ASPxGridView1.Columns.Insert(0, col);
        }
        protected DataTable GetDataStructure()
        {
            DataTable dt = new DataTable();
            bool chk = chkShowReceived.Items[0].Selected;
            dt = ClsData.NewProductRequest(chk, chk);
            return dt;
        }
        private DataTable GetDataSource()
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
                rs = ClsData.NewProductRequest(chkShowReceived.Items[0].Selected, true);
                string sqlw = "RefNo like '%" + Convert.ToInt32(cApp.shop_id).ToString("000") + "%' " + addWhere;
                foreach (DataRow file in ClsData.XMLTableData("*st"+ cApp.shop_id).Rows)
                {
                    
                    dt = ClsData.QueryData(file["filename"].ToString(),sqlw );
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
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = GetDataSource();
            ASPxGridView1.KeyFieldName = "OID";
            ASPxGridView1.ExpandAll();
        }

        protected void chkShowReceived_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chkShowReceived.Items[0].Selected==true)
            {
                isShowAll = false;
                btnConfirm.Visible = true;
                ASPxGridView1.DataBind();
            }
            else
            {
                isShowAll = true;
                btnConfirm.Visible = false;
                ASPxGridView1.DataBind();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            isShowAll = false;
            ASPxGridView1.DataBind();
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
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
                    r["TransactionType"] = r["TransactionType"].ToString().Replace("สั่ง","เติม");
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
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                var ls = ASPxGridView1.GetSelectedFieldValues("OID");
                foreach (object fld in ls)
                {
                    string val = fld.ToString();
                    ConfirmData(val);
                }
                ASPxGridView1.DataBind();
            }
        }

        protected void btbAdd_Click(object sender, EventArgs e)
        {
            cApp.working_date = txtDate.Text;
            Response.Redirect("AddRequest.aspx?type=" + cboProdType.SelectedIndex+ "&mode=Add");
        }

        protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if(e.ButtonID=="btnConfirm")
            {
                try
                {
                    DataRow dr = ASPxGridView1.GetDataRow(e.VisibleIndex);
                    ConfirmData(dr["oid"].ToString());
                    ASPxGridView1.DataBind();
                }
                catch
                {

                }                
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddRequest.aspx?type=" + cboProdType.SelectedIndex + "&mode=Edit");
        }
    }
}