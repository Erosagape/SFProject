using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Services;
using DevExpress.Web;

namespace sfdelivery
{
    public partial class deliveryhd : System.Web.UI.Page
    {
        private static List<string> LOV_Customer = new List<string>();
        ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name == "")
            {
                Response.Redirect("Default.aspx", true);
            }
            else
            {
                lblMessage.Text = "Ready";
                if (!IsPostBack)
                {
                    txtSalesCode.Text = cApp.emp_id;
                    if (txtSalesCode.Text != "") txtSalesCode.Enabled = false;
                    lblMessage.Text = "Loading Data";
                    LOV_Customer = GetCustomerList(cApp.working_date);
                    ClsData.LoadStatus(cboStatus);
                    if(Request.QueryString.Count==2)
                    {
                        txtCustCode.Text = Request.QueryString["custid"];
                        txtCustName.Text = Request.QueryString["custname"];
                        ChkAllData.Checked = false;
                    }
                    else
                    {
                        string d = DateTime.Today.AddYears(543).ToString("d/M/y");
                        txtDeliverDate.Text = d;
                        ChkAllData.Checked = true;
                    }
                    LoadData(GetCliteria());
                    CreateCommandCol();
                }
                lblUser.Text = cApp.user_name;
                lblfileName.Text = "Delivery" + cApp.working_date + ".xml";
            }
        }
        protected List<string> GetCustomerList(string yymm)
        {            
            var rs = (from DataRow dr in ClsData.GetDataXML("Customer").Rows
                        where ((string)dr["Customer"] != "")
                        orderby (string)dr["CustomerName"]
                        select (string)dr["Customer"] + "|" + (string)dr["CustomerName"]).Distinct();
            return rs.ToList();
        }
        [WebMethod]
        public static string[] GetCustomer(string searchstr)
        {
            var data = LOV_Customer.Where(e => e.Contains(searchstr));
            return data.ToArray();
        }
        protected void LoadData(string cliteria)
        {
            string filter = cApp.working_date;
            if(ChkAllData.Checked ==true)
            {
                filter = "2"; //year begin with 2
            }
            DataTable dt = ClsData.GetDeliveryData(filter, cliteria);
            if(dt.Rows.Count>0)
            {
                dt = ClsData.SetCaptionDataDelivery(dt);
                dt.Columns.Remove("filename");
            }

            ViewState["ReportDelivery"] = dt;
            GridView1.DataBind();
            lblMessage.Text = dt.Rows.Count + " Records found!";
        }
        protected string GetCliteria()
        {
            string str = "";
            if (txtCustCode.Text != "")
            {
                if (str != "") str += " And ";
                str += "[Customer] Like '%" + txtCustCode.Text.Trim() + "%'";
            }
            if (txtCustName.Text !="")
            {
                if (str != "") str += " And ";
                str += "[CustomerName] Like '%" + txtCustName.Text.Trim() + "%'";
            }
            if(txtDeliverDate.Text !="")
            {
                if (str != "") str += " And ";
                str += "[Mark6] Like '%" + txtDeliverDate.Text + "%'";
            }
            if(txtDeliverTo.Text!="")
            {
                if (str != "") str += " And ";
                str += "([ShipTo1] Like '%" + txtDeliverTo.Text.Trim() + "%' or [ShipTo2] Like '%" + txtDeliverTo.Text.Trim() + "%')";
            }
            if(txtDriver.Text!="")
            {
                if (str != "") str += " And ";
                str += "[Driver] Like '%" + txtDriver.Text.Trim() + "%'";
            }
            if(txtINo.Text!="")
            {
                if (str != "") str += " And ";
                str += "[ID] Like '%" + txtINo.Text.Trim() + "%'";
            }
            if(txtSalesCode.Text!=""&&txtSalesCode.Text!="ALL")
            {
                if (str != "") str += " And ";
                str += "[SalesMan] IN('" + txtSalesCode.Text.Trim().Replace(",","','") + "')";
            }
            if(txtSNo.Text!="")
            {
                if (str != "") str += " And ";
                str += "[SID] Like '%" + txtSNo.Text.Trim() + "%'";
            }
            if(CheckBox2.Checked==false)
            {
                if (str != "") str += " And ";
                str += "Not [Account]=''";
            }
            if (CheckBox1.Checked == false)
            {
                if (str != "") str += " And ";
                str += "[Account] =''";
            }
            if (RadioButtonList1.SelectedIndex>0)
            {
                if(RadioButtonList1.SelectedIndex==1)
                {
                    if (str != "") str += " And ";
                    str += "[Mark6] =''";
                }
                if (RadioButtonList1.SelectedIndex == 2)
                {
                    if (str != "") str += " And ";
                    str += "Not [Mark6] =''";
                }
                if (RadioButtonList1.SelectedIndex == 3)
                {
                    if (str != "") str += " And ";
                    str += "Not [Mark8] =''";
                }            
            }
            if (cboStatus.Text != "")
            {
                if (str != "") str += " And ";
                str += "[Mark8] Like '" + cboStatus.Text + "%'";
            }
            if (txtTransport.Text!="")
            {
                if (str != "") str += " And ";
                str += "[TransName] Like '%" + txtTransport.Text.Trim() + "%'";
            }
            return str;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(GetCliteria());
        }

        protected void txtCustCode_TextChanged(object sender, EventArgs e)
        {
            string value = "";
            try
            {
                if(txtCustCode.Text!="")
                {
                    value = LOV_Customer.FindAll(data => data.StartsWith(txtCustCode.Text)).FirstOrDefault().ToString();
                    value = value.Substring(value.IndexOf('|') + 1);
                }
            }
            catch
            {
                value = "Not Found";
            }            
            txtCustName.Text = value;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt=(DataTable)ViewState["ReportDelivery"];
                string fname = "Report" + cApp.user_id + ".xml";
                dt.WriteXml(ClsData.GetPath() + fname);
                ClsData.Download(fname, "~/"+ fname);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void CreateCommandCol()
        {
            GridViewCommandColumn col = new GridViewCommandColumn("Action");
            col.CustomButtons.Add(CreateViewButton());
            col.SetColVisibleIndex(0);
            GridView1.Columns.Add(col);
        }
        GridViewCommandColumnCustomButton CreateViewButton()
        {
            GridViewCommandColumnCustomButton btn = new GridViewCommandColumnCustomButton();
            btn.ID = "btnView";
            btn.Text = "View";
            btn.Visibility = GridViewCustomButtonVisibility.BrowsableRow;
            return btn;
        }
        protected void GridView_DataBinding(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["ReportDelivery"];
            GridView1.DataSource = dt;
            GridView1.KeyFieldName = "เลขที่เอกสาร";
        }

        protected void GridView_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "btnView")
            {
                ShowDetail(true);
            }
        }
        protected void ShowDetail(bool CallBack=false)        
        {
            string id = txtID.Text;
            string date = txtDate.Text;
            if(date!="" && id!="")
            {
                string[] serial = date.Split('/');
                cApp.working_date = serial[2] + serial[1];
                Session["cApp"] = cApp;
                if (CallBack == false)
                {
                    Response.Redirect("deliverydt.aspx?id=" + id);
                }
                else
                {
                    ASPxWebControl.RedirectOnCallback("deliverydt.aspx?id=" + id);
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(GetCliteria());
        }
    }
}