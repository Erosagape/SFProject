using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace shopsales_tools
{
    public partial class frmUpdateToWeb : Form
    {
        public frmUpdateToWeb()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            string dataXML = svc.GetDataXML(cboDBList.SelectedValue.ToString());
            if(dataXML.Substring(0,1)=="<")
            {
                ClsXML xml = new ClsXML();
                if (xml.LoadXMLString(dataXML)==true )
                {
                    dataGridView1.DataSource = xml.Datatable();
                }
            }
            else
            {
                MessageBox.Show(dataXML);
            }
        }
        protected void SyncData(bool webtodb=false)
        {
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            ClsXML xml = new ClsXML();
            ClsConnectSql db = new ClsConnectSql();
            if (db.isReady()==true)
            {
                DataTable dt = new DataTable();
                DataTable rs = new DataTable();
                string dataxml = svc.GetDataXML(cboDBList.SelectedValue.ToString());
                bool isprocess = false;
                if (dataxml.Substring(0, 1) == "<")
                {
                    if (xml.LoadXMLString(dataxml) == true)
                    {
                        rs = xml.Datatable();
                        isprocess = true;

                        dt = xml.GetDataForXML(cboDBList.SelectedValue.ToString().ToLower());
                        if (xml.Errormessage() != "") isprocess = false;
                    }
                    if (isprocess == true)
                    {
                        textBox2.Text = "";
                        DataTable tb = new DataTable();
                        if (webtodb == true)
                        {
                            tb = CompareTableRowByRow(rs, dt);
                            int iAdd = 0;
                            int iUpd = 0;
                            if (checkBox1.Checked == true)
                            {
                                foreach (DataRow row in tb.Rows)
                                {
                                    string sql = xml.GetInsertCommandData();
                                    bool isAdd = true;
                                    DataRow[] rows = dt.Select(tb.Columns[0].ColumnName + "=" + row[0].ToString() + "");
                                    foreach (DataRow r in rows)
                                    {
                                        sql = xml.GetUpdateCommandData();
                                        isAdd = false;
                                    }
                                    for (int i = 0; i < tb.Columns.Count; i++)
                                    {
                                        sql = sql.Replace("{" + i + "}", db.SQLVal(row[i].ToString()));
                                    }
                                    if (sql != "")
                                    {
                                        if (db.Execute(sql) == false)
                                        {
                                            textBox2.Text += "ERROR " + db.ErrorMessage() + "\n";
                                        }
                                        else
                                        {
                                            if(isAdd ==true)
                                            {
                                                iAdd++;
                                            }
                                            else
                                            {
                                                iUpd++;
                                            }
                                        }
                                    }
                                }
                                textBox2.Text += iAdd.ToString() + " rows Added " + iUpd.ToString() + " rows Updated \n";
                            }                            
                            textBox2.Text += " Web =" + rs.Rows.Count + " Records \n";
                            textBox2.Text += cboDBList.SelectedText + " DB =" + dt.Rows.Count + " Records \n";
                        }
                        else
                        {
                            //db=>Web
                            tb = CompareTableRowByRow(dt, rs);
                            if (checkBox1.Checked == true)
                            {
                                if (tb.Columns.Count > 0 && tb.Rows.Count > 0)
                                {
                                    string strXML = xml.GetXML(tb);
                                    textBox2.Text += svc.ProcessDataXML(cboDBList.SelectedValue.ToString(), strXML) + " \n";
                                }
                            }
                            textBox2.Text += cboDBList.SelectedText + " DB =" + dt.Rows.Count + " Records \n";
                            textBox2.Text += "Web =" + rs.Rows.Count + " Records \n";
                        }

                        textBox2.Text += "found Difference =" + tb.Rows.Count + " Records";

                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = tb;
                    }
                    else
                    {
                        textBox2.Text = dataxml;
                    }
                }
                else
                {
                    textBox2.Text = dataxml;
                }
                textBox2.Visible = true;
                db.CloseConnection();
            }
            svc.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            SyncData(false);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private DataTable CompareTableRowByRow(DataTable tSource,DataTable tDest)
        {
            ClsConnectSql cn = new ClsConnectSql();
            DataTable dt = cn.CompareTable(tSource, tDest);
            cn.CloseConnection();
            return dt;
        }
        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            textBox2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            SyncData(true);
        }
        protected void LoadDBList()
        {
            ClsXML xml = new ClsXML(Program.StartupPath+@"\\WebDB.xml");
            cboDBList.DataSource = xml.Datatable();
            cboDBList.DisplayMember = "fileDesc";
            cboDBList.ValueMember = "filename";
            cboDBList.SelectedIndex = 0;
        }
        private void frmUpdateToWeb_Load(object sender, EventArgs e)
        {
            LoadDBList();
        }
    }
}
