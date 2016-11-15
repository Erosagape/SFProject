using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace shopsales_tools
{
    public partial class frmUpdateSales : Form
    {
        public frmUpdateSales()
        {
            InitializeComponent();
        }

        private void frmUpdateSales_Load(object sender, EventArgs e)
        {
            LoadCombo();
            txtYear.Text = DateTime.Now.Year.ToString();
        }
        private void LoadCombo()
        {
            using (ClsConnectSql db = new ClsConnectSql())
            {
                cboCustomer.DataSource = db.LOV_Customer(true);
                cboCustomer.DisplayMember = "CustName";
                cboCustomer.ValueMember = "OID";

                cboMonth.DataSource = db.LOV_Month();
                cboMonth.DisplayMember = "MonthNameTH";
                cboMonth.ValueMember = "MonthId";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt= GetXMLDataList();
            dataGridView1.DataSource = dt;
        }
        private string ProcessData()
        {
            int rowadd = 0;
            int rowupdate = 0;
            string msg = "";
            using (ClsXML xml = new ClsXML("UpdateCheck.xml"))
            {
                using (ClsConnectSql db = new ClsConnectSql())
                {
                    DataTable saleTypes = db.LOV_SaleType();
                    DataTable tb = new DataTable();
                    bool bfirst = true;
                    DataTable files = GetXMLDataList();
                    if (files.Rows.Count > 0 && files.Columns.Count > 0)
                    {
                        foreach (DataRow file in files.Rows)
                        {
                            msg += "-- Processing " + file["filename"].ToString()+ "\n";
                            string chkDate = xml.FindValue("filename='" + file["filename"].ToString() + "'", "modifieddate");
                            bool bPass = false;
                            if(chkDate!="")
                            {
                                if(chkDate==file["modifieddate"].ToString())
                                {
                                    bPass = true;
                                }
                            }
                            if(bPass ==false)
                            {
                                string cust = file["filename"].ToString().Substring(0, file["filename"].ToString().IndexOf("_"));
                                IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
                                string dataXML = svc.GetDataXML(file["filename"].ToString().ToLower().Replace(".xml", ""));
                                svc.Close();
                                DataTable src = xml.Datatable(dataXML);
                                if(src.Rows.Count>0)
                                {
                                    if (bfirst)
                                    {
                                        tb = src.Clone();
                                        bfirst = false;
                                    }
                                    for (int i = 0; i < saleTypes.Rows.Count; i++)
                                    {
                                        msg += "++" + saleTypes.Rows[i]["saleType"].ToString() + "++\n";
                                        DataRow[] rows = src.Select("salesType='" + saleTypes.Rows[i]["OID"].ToString() + "'");
                                        DataTable sales = new DataTable();
                                        if (rows.Length > 0)
                                        {
                                            sales = rows.OrderBy(row => row["salesDate"]).CopyToDataTable();
                                            if (sales.Rows.Count > 0)
                                            {
                                                string chk = "";
                                                string oid = "";
                                                foreach (DataRow data in sales.Rows)
                                                {
                                                    if (chk != cust + data["salesDate"].ToString() + saleTypes.Rows[i]["OID"].ToString())
                                                    {
                                                        oid = oid = db.FindValue("select OID from SOHd where SODate='" + data["salesDate"].ToString() + "' and Customer='" + cust + "' and SaleType='" + saleTypes.Rows[i]["OID"].ToString() + "' and Remark='" + file["filename"].ToString() + "'");
                                                        if (oid == "")
                                                        {
                                                            string cmd = "dbo.sp_insert_sohd '" + data["salesDate"].ToString() + "','" + cust + "','" + saleTypes.Rows[i]["OID"].ToString() + "','0','" + file["filename"].ToString() + "'";
                                                            if (db.Execute(cmd) == true)
                                                            {
                                                                oid = db.FindValue("select OID from SOHd where SODate='" + data["salesDate"].ToString() + "' and Customer='" + cust + "' and SaleType='" + saleTypes.Rows[i]["OID"].ToString() + "' and Remark='" + file["filename"].ToString() + "'");
                                                                msg += "-> Create " + db.FindValue("select [Code] from SOHd where OID='" + oid + "'") + " \n";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            msg += "-->Update " + db.FindValue("select [Code] from SOHd where OID='" + oid + "'") + " \n";
                                                        }
                                                        chk = cust + data["salesDate"].ToString() + saleTypes.Rows[i]["OID"].ToString();
                                                    }
                                                    if (oid == "")
                                                    {
                                                        msg += "cannot Process -> " + data["OID"].ToString() + " \n";
                                                    }
                                                    else
                                                    {
                                                        string oiddt = db.FindValue("select OID from SODt where SOID='" + oid + "' and Remark='" + data["OID"].ToString() + "'");
                                                        string prodid = data["prodID"].ToString();
                                                        if (prodid == "")
                                                        {
                                                            prodid = db.FindValue("select oid from xGoods where GoodsName like '" + data["prodName"].ToString() + "%'");
                                                        }
                                                        if (prodid == "")
                                                        {
                                                            msg += "cannot found product =" + data["prodName"].ToString() + "\n";
                                                        }
                                                        else
                                                        {
                                                            if (oiddt == "")
                                                            {
                                                                string cmd = "dbo.sp_insert_sodt '" + oid + "','" + prodid + "','" + data["TagPrice"].ToString() + "','" + data["salesPrice"].ToString() + "','" + data["salesQty"].ToString() + "','0" + data["discountRate"].ToString() + "','0','" + data["OID"].ToString() + "'";
                                                                if (db.Execute(cmd) == true)
                                                                {
                                                                    rowadd++;
                                                                }
                                                                else
                                                                {
                                                                    msg += "cannot insert row ->" + data["OID"].ToString() + "\n";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                string cmd = "update SODt set Goods='" + prodid + "',StdSalePrice='" + data["TagPrice"].ToString() + "',SalePrice='" + data["salesPrice"].ToString() + "',Quantity='" + data["salesQty"].ToString() + "',DisCount='0" + data["discountRate"].ToString() + "' where OID='" + oiddt + "'";
                                                                if (db.Execute(cmd) == true)
                                                                {
                                                                    rowupdate++;
                                                                }
                                                                else
                                                                {
                                                                    msg += "cannot update oid ->" + oiddt + "\n";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    tb.ImportRow(data);
                                                }
                                            }
                                        }
                                    }
                                }                                
                            }
                            
                        }
                    }
                    files.TableName = "UpdateCheck";
                    files.WriteXml(files.TableName + ".xml");
                    dataGridView1.DataSource = tb;
                }
                
            }
            msg += rowadd + " rows added " + rowupdate + " rows updated \n";
            return msg.Replace("\n",Environment.NewLine);
        }
        private DataTable GetXMLDataList()
        {
            DataTable dt = new DataTable();
            using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
            {
                string dataXML = svc.GetXMLFileList(GetfileNameFilter());
                using (ClsXML xml = new ClsXML())
                {
                    dt = xml.Datatable(dataXML);
                }
            }
            return dt;
        }
        private string GetfileNameFilter()
        {
            string cust = "*";
            string yy = "*";
            string mm = "*";
            if (cboCustomer.SelectedValue.ToString() !="0")
            {
                cust = cboCustomer.SelectedValue.ToString();
            }
            if(txtYear.Text!="")
            {
                yy = txtYear.Text;
            }
            if(cboMonth.SelectedValue.ToString()!="")
            {
                mm = cboMonth.SelectedValue.ToString();
            }
            return cust + "_" + yy + mm + "_*";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtLog.Text = ProcessData();
            StreamWriter txt = new StreamWriter("Log.txt", true, UTF8Encoding.UTF8);
            txt.WriteLine(txtLog.Text);
            txt.Close();

            MessageBox.Show("Completed!");
        }
    }
}
