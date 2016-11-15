using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shopsales
{
    public partial class CreateGoods : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["cApp"]!=null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name == "" || cApp.user_role != "0")
            {
                Response.Redirect("index.html", true);
            }
            if (!IsPostBack)
            {
                ClsData.LoadShoeColor(cboColor,"ColTh","ColId");
                ClsData.LoadShoeType(cboShoeType, "STName", "STId");
                ClsData.LoadModel(cboModel, "Model", "SMId");
                LstColor.Items.Clear();
                LoadGoodsCombo();
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            if(cboGoods.SelectedValue !=null)
            {
                string oid = cboGoods.SelectedValue.ToString();
                LoadGrid(oid);
            }
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if(ClsData.IsLockedDataTable("Goods","Admin")==false)
            {
                try
                {
                    ClsData.LockDataTable("Goods", "Admin");
                    Int16 szBegin = Convert.ToInt16(txtSizeFrom.Text);
                    Int16 szEnd = Convert.ToInt16(txtSizeTo.Text);
                    string model = txtItem.Text;
                    Int32 oid = 0;
                    foreach (ListItem lst in LstColor.Items)
                    {
                        for (int sz = szBegin; sz <= szEnd; sz++)
                        {
                            string colcode = ClsData.GetColorcodeByName(lst.Text);
                            string codevalue = ClsData.GetGoodsCode(model,colcode, sz.ToString());
                            string codename = ClsData.GetGoodsName(model, lst.Text, sz.ToString());
                            DataTable Goods = ClsData.ShoeData();
                            DataRow r = ClsData.QueryData(Goods, "GoodsCode='" + codevalue + "'");
                            string msg = "Update";
                            if (r["OID"].ToString() == "")
                            {
                                if (oid == 0)
                                {
                                    oid = Convert.ToInt32(ClsData.GetNewOID(Goods, "Goods", "OID"));
                                }
                                else
                                {
                                    oid++;
                                }
                                r["OID"] = oid;
                                msg = "Add";
                            }
                            r["GoodsCode"] = codevalue;
                            r["GoodsName"] = codename;
                            r["ModelName"] = txtItem.Text;

                            r["ColNameInit"] = colcode;
                            r["ColNameTh"] = lst.Text;
                            DataRow color = ClsData.QueryData(ClsData.ShoeColorData(), "ColNameInit='" + colcode + "'");
                            if (color["ColId"].ToString() != "")
                            {
                                r["ColId"] = color["ColId"].ToString();
                                r["ColNameEng"] = color["ColEN"].ToString();
                                r["ColTypeId"] = color["ColTypeId"].ToString();
                            }
                            r["ProdKindId"] = 1;
                            r["SizeNo"] = sz;
                            r["StdSellPrice"] = txtPrice.Text;
                            r["ProdStdCost"] = r["StdSellPrice"].ToString();

                            r["STId"] = cboShoeType.SelectedValue.ToString();
                            r["ProdGroupId"] = 1;
                            if (Convert.ToInt16(r["STId"].ToString()) > 4)
                            {
                                r["ProdGroupId"] = 2;
                            }
                            r["prodGroupName"] = ClsData.QueryData(ClsData.ShoeGroupData(), "OID='" + r["ProdGroupID"].ToString() + "'")["GroupName"].ToString();
                            DataRow st = ClsData.QueryData(ClsData.ShoeTypeData(), "STId='" + r["STId"].ToString() + "'");
                            if (st["STCode"].ToString() != "")
                            {
                                r["STCode"] = st["STCode"].ToString();
                                r["STName"] = st["STName"].ToString();
                                r["STName2"] = st["STName2"].ToString();
                            }

                            DataRow mdl = ClsData.QueryData(ClsData.ShoeModelData(), "SMId='" + cboModel.SelectedValue.ToString() + "'");
                            if(mdl["Model"].ToString()!="")
                            {
                                r["SMCode"] = mdl["SMCode"].ToString();
                                r["SMId"] = mdl["SMId"].ToString();
                                r["SSid"] = mdl["SSId"].ToString();
                                r["ProdCatId"] = mdl["ProdCatId"].ToString();
                            }
                            DataRow cat = ClsData.QueryData(ClsData.ShoeCategoryData(), "ProdCatId='" + r["ProdCatId"].ToString() + "'");
                            if (cat["ProdCatId"].ToString() != "")
                            {
                                r["ProdCatcode"] = cat["ProdCatCode"].ToString();
                                r["ProdCatName"] = cat["ProdCatName"].ToString();
                            }

                            if (r.RowState == DataRowState.Detached) Goods.Rows.Add(r);
                            Goods.WriteXml(MapPath("~/Goods.xml"));
                            listLog.Items.Add(msg+" " + codevalue +" ID="+ r["OID"].ToString());
                        }
                    }
                    ClsData.UnlockDataTable("Goods", "Admin");
                }
                catch (Exception ex)
                {
                    listLog.Items.Add(ex.Message);
                }

            }
        }
        protected void btnAddcolor_Click(object sender, EventArgs e)
        {
            LstColor.Items.Add(cboColor.SelectedItem.Text);
        }
        protected void LstColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void btnRemoveColor_Click(object sender, EventArgs e)
        {
            LstColor.Items.Remove(LstColor.SelectedItem);
        }
        protected void btnAddModel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddModel.aspx");
        }
        protected void cboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboModel.SelectedIndex>=0)
            {
                ShowModel("SMId='" + cboModel.SelectedValue.ToString() + "'");
            }
        }
        protected void LoadGoodsCombo()
        {
            if (Request.QueryString.Count == 1)
            {
                String ModelCode = Request.QueryString[0];
                ClsData.LoadShoe(cboGoods, "GoodsName", "OID", ModelCode);
            }
            else
            {
                ClsData.LoadShoe(cboGoods, "GoodsName", "OID");
            }
        }
        protected void ShowModel(string cliteria)
        {
            DataRow model = ClsData.QueryData(ClsData.ShoeModelData(), cliteria);
            txtSizeFrom.Text = model["MinSize"].ToString();
            txtSizeTo.Text = model["MaxSize"].ToString();
            cboShoeType.SelectedValue = model["STId"].ToString();
        }
        protected void LoadGrid(string id)
        {
            DataTable rs = ClsData.ShoeData(id);
            GridView1.DataSource = rs;
            GridView1.DataBind();

            if (rs.Rows.Count > 0)
            {
                cboModel.SelectedValue = rs.Rows[0]["SMId"].ToString();
                if (cboModel.SelectedIndex >= 0)
                {
                    ShowModel("SMId='" + cboModel.SelectedValue.ToString() + "'");
                }
                cboColor.SelectedValue = rs.Rows[0]["ColId"].ToString();
                cboShoeType.SelectedValue = rs.Rows[0]["STId"].ToString();
                txtItem.Text = rs.Rows[0]["ModelName"].ToString();
                txtPrice.Text = rs.Rows[0]["StdSellPrice"].ToString();
            }
        }
    }
}