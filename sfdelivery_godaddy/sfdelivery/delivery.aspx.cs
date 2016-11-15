using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{    
    public partial class delivery : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        private Table tb = new Table();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name == ""||cApp.user_role=="2")
            {
                Response.Redirect("Default.aspx", true);
            }
            else
            {
                lblUser.Text = cApp.user_name;
            }
            if (IsPostBack==true)
            {
                if(Session["tbDelivery"]!=null)
                {
                    tb = (Table)Session["tbDelivery"];
                    panel.Controls.Clear();
                    panel.Controls.Add(tb);
                }                
            }
            else
            {
                Button2.ToolTip = "0";
                Session["tbDelivery"] = null;
                ClsData.LoadStatus(cboStatus);
                LoadMonthYear();
                string d = DateTime.Today.AddYears(543).ToString("d/M/y");
                txtDeliveryDate.Text = d;
            }
        }

        protected string GetCliteria()
        {
            string str = "ID Like '%"+txtDocNo.Text+"%'";
            if (txtDeliveryDate.Text != "")
            {
                str += "and [Mark6] Like '%" + txtDeliveryDate.Text + "%'";
            }
            return str;
        }
        protected Table CreateTableStruct(Table tbl,DataTable rs)
        {
            tbl = new Table();
            TableRow header = new TableRow();
            TableCell chk = new TableCell();
            chk.ID = "h0";
            chk.Text = "#";
            header.Cells.Add(chk);
            TableCell data = new TableCell();
            data.ID = "t0";
            data.Text = "#Status";
            header.Cells.Add(data);
            for (int c = 0; c < rs.Columns.Count; c++)
            {
                TableCell tc = new TableCell();
                tc.ID = "h" + rs.Columns[c].ColumnName;
                tc.Text=ClsData.GetColumnNameTh(rs.Columns[c].ColumnName);
                tc.ToolTip= rs.Columns[c].ColumnName;
                header.Cells.Add(tc);
            }
            tbl.Rows.Add(header);
            return tbl;
        }
        protected void LoadData(string fname)
        {
            DataTable dt = ClsData.QueryData(fname, GetCliteria()).Copy();
            tb=CreateTableStruct(tb, dt);
            int r = 0;
            foreach (DataRow dr in dt.Rows)
            {
                TableRow tr = new TableRow();
                tr.ID = "r" + (r+1);
                TableCell chk = new TableCell();
                chk.ID = "h" + (r + 1);
                CheckBox c = new CheckBox();
                c.ID = "chk" + (r + 1);
                c.Checked = true;
                chk.Controls.Add(c);
                tr.Cells.Add(chk);
                TableCell text = new TableCell();
                text.ID = "data" + (r + 1);
                TextBox t = new TextBox();
                t.ID = "text" + (r + 1);
                t.Text = dr["Mark8"].ToString();
                text.Controls.Add(t);
                tr.Cells.Add(text);
                for (int i=0;i<dt.Columns.Count;i++)
                {
                    TableCell tc = new TableCell();
                    tc.ID = "c"+(i+1)+"r" + (r + 1);
                    TextBox txt = new TextBox();
                    txt.ID = "txt" + dt.Columns[i].ColumnName+(r+1);
                    txt.Text = dr[i].ToString();
                    tc.Controls.Add(txt);
                    tr.Cells.Add(tc);
                }
                tb.Rows.Add(tr);
                r = (r + 1);
            }
        }
        protected string GetFileName()
        {
            try
            {
                return "Delivery" + cApp.working_date;
            }
            catch
            {
                return "XXYY";
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = GetFileName();
                if(System.IO.File.Exists(ClsData.GetPath()+ filename + ".xml")==true)
                {
                    panel.Controls.Clear();
                    LoadData(filename);
                    panel.Controls.Add(tb);
                    Session["tbDelivery"] = tb;
                    lblRecCount.Text = (tb.Rows.Count - 1).ToString();
                }
            }
            catch(Exception ex)
            {
                lblRecCount.Text = ex.Message;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SelectCheckBox(tb,true);
            Session["tbDelivery"] = tb;
            cApp.working_date = cboMonthYear.SelectedItem.Text;
            Session["cApp"] = cApp;

        }
        protected void SetValue(Table table,string value)
        {
            for (int i = 1; i < table.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)table.Rows[i].Cells[0].FindControl("chk" + i);
                if (chk != null)
                {
                    if (chk.Checked == true)
                    {
                        TextBox txt = (TextBox)table.Rows[i].Cells[1].FindControl("text" + i);
                        if (txt != null)
                        {
                            txt.Text = value;
                        }

                    }
                }
            }
        }
        protected void SelectCheckBox(Table table,bool checkboxstate)
        {
            if (checkboxstate == true)
            {
                Button2.ToolTip = "1";
            }
            else
            {
                Button2.ToolTip = "0";
            }

            for (int i = 1; i < table.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)table.Rows[i].Cells[0].FindControl("chk" + i);
                if (chk != null)
                {
                    chk.Checked = checkboxstate;
                }
            }
        }
        protected void SaveData()
        {
            if (ClsData.IsLockedDataTable("Delivery", "Admin") == false)
            {
                ClsData.LockDataTable("Delivery", "Admin");
                DataTable dt = ClsData.GetDataXML(GetFileName());
                if(ClsData.Error()=="")
                {
                    int j = 0;
                    for (int i = 1; i < tb.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)tb.Rows[i].Cells[0].FindControl("chk" + i);
                        if (chk != null)
                        {
                            if (chk.Checked == true)
                            {
                                string Mark8 = "";
                                string ID = "";
                                TextBox txt = (TextBox)tb.Rows[i].FindControl("text" + i);
                                if (txt != null)
                                {
                                    Mark8=txt.Text;
                                }
                                txt = (TextBox)tb.Rows[i].FindControl("txtMark8" + i);
                                if (txt != null)
                                {                                    
                                    txt.Text = Mark8;
                                }
                                txt = (TextBox)tb.Rows[i].FindControl("txtID" + i);
                                if (txt != null)
                                {
                                    ID = txt.Text;
                                }
                                if (ID != "")
                                {
                                    DataRow[] dr = dt.Select("ID='" + ID + "'");
                                    foreach (DataRow r in dr)
                                    {
                                        r["Mark8"] = Mark8;
                                        j = j + 1;
                                    }
                                }
                            }
                        }
                    }
                    dt.WriteXml(ClsData.GetPath() + GetFileName() + ".xml");
                    ClsData.UnlockDataTable("Delivery", "Admin");
                    lblRecCount.Text = j.ToString() + " Rows Updated!";
                }
            }
            else
            {
                lblRecCount.Text = "มีคนกำลังใช้ข้อมูลอยู่.. กรุณารอสักครู่";
            }
            Session["tbDelivery"] = tb;

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            SaveData();
            cApp.working_date = cboMonthYear.SelectedItem.Text;
            Session["cApp"] = cApp;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            SelectCheckBox(tb, false);
            Session["tbDelivery"] = tb;
            cApp.working_date = cboMonthYear.SelectedItem.Text;
            Session["cApp"] = cApp;
        }

        protected void cboMonthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cApp.working_date = cboMonthYear.SelectedItem.Text;
            Session["cApp"] = cApp;

        }
        private void LoadMonthYear()
        {
            IService sv = new IService();
            DataTable dt = ClsData.GetDataTableFromXML(sv.GetXMLFileList("Delivery*"), "FileList").Copy();
            DataTable rs = dt.Clone();
            string current = DateTime.Now.ToString("yyyyMM");
            int idx = 0;
            int p = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].ToString().IndexOf("Detail") < 0)
                {
                    dr[0] = dr[0].ToString().Replace("Delivery", "");
                    dr[0] = dr[0].ToString().Replace(".xml", "");
                    if (dr[0].ToString() == current) p = idx;
                    rs.ImportRow(dr);
                    idx++;
                }
            }
            cboMonthYear.DataSource = rs;
            cboMonthYear.DataValueField = "modifieddate";
            cboMonthYear.DataTextField = "filename";
            cboMonthYear.DataBind();
            cboMonthYear.SelectedIndex = p;
            cApp.working_date = current;
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetValue(tb, (cboStatus.SelectedItem.Text + ":" + txtRemark.Text).Trim());
        }
    }
}