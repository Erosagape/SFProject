using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SfDeliverTracking
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        private void LoadCustomer()
        {
            cboCustomer.DataSource = ClsData.GetCustomer();
            cboCustomer.ValueMember = "customer";
            cboCustomer.DisplayMember = "CustomerName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            //LoadData(GetCliteria());
            LoadCustomer();
            txtCustomer.Text = "";
        }
        private string GetCliteria(string addWhere="")
        {
            string str = "";
            if (txtCustomer.Text!="")
            {
                str += " Customer='" + txtCustomer.Text + "' ";
            }
            if(str!="")
            {
                str = " WHERE " + str;
                if (addWhere != "") str += " AND " + addWhere;
            }
            else
            {
                if (addWhere != "") str += " WHERE " + addWhere;
            }
            return str;
        }
        protected void LoadData()
        {
            using (ClsConnectSql db = new ClsConnectSql())
            {
                if (db.isReady() == true)
                {
                    dt = db.RecordSet(GetSQLHeader());
                    dt.TableName = "TransHeader";
                    dataGridView1.DataSource = dt;
                }
                db.CloseConnection();
                db.Dispose();
            }                
        }
        private string GetWhere()
        {
            string wc = "";
            string yy = "";
            if (txtYear.Text != "")
            {
                //yy = @"/%" + txtYear.Text;
                yy = " AND Year(a.DocDate)=" + txtYear.Text;
            }
            if (txtMonth.Text != "")
            {
                //yy = @"%/" + txtMonth.Text + yy;
                yy += " AND Month(a.DocDate)=" + txtMonth.Text;
            }
            if (yy != "")
            {
                //wc = @" and (a.Mark6 Like '" + yy + "')";
                wc = yy;
            }
            return wc;
        }

        private void SaveXML()
        {
            ClsConnectSql db = new ClsConnectSql();
            DataTable dtHead = db.RecordSet(GetSQLHeader());
            DataTable dtDet = db.RecordSet(GetSQLDetail());
            dtHead.TableName = "TranHeaders";
            dtDet.TableName = "TranDetails";
            dtHead.WriteXml(defaultfilename() + ".xml");
            dtDet.WriteXml(defaultfilename("Details") + ".xml");
            db.CloseConnection();
            MessageBox.Show("Finished!");
        }

        private void SaveJSON()
        {
            string msg = "Start " + DateTime.Now.ToString() + "\n";
            File.WriteAllText(defaultfilename()+".json", ClsData.GetJSONFromDataTable(dt));
            msg += "Finished " + DateTime.Now.ToString() + "\n";
            MessageBox.Show("Completed \n" + msg);
        }
        protected DataTable GetWebData(string fname)
        {
            DataTable dt = new DataTable();
            using (ClsXML xml=new ClsXML())
            {
                using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
                {
                    string xmlData = svc.GetDataXML(fname,true);
                    dt = xml.Datatable(xmlData);
                    svc.Close();
                }
                xml.Dispose();
            }
            return dt;
        }
        protected string UploadToWeb(string fname,DataTable dt)
        {
            string msg = fname +" Start " + DateTime.Now.ToString() + "\n";
            msg += UploadTable(dt, 100,fname) + "\n";
            msg += fname +" Finished " + DateTime.Now.ToString() + "\n";
            return msg;
        }
        private string UploadTable(DataTable dt,int totalRecord, string filename)
        {
            ClsXML xml = new ClsXML();
            DataSet ds = new DataSet();
            ds.DataSetName = filename + "Error";
            string log = "";
            DataTable src = dt.Copy();
            int rowprocess = 0;
            int tbcount = 0;
            int rowcount = 0;
            DataTable dst = dt.Clone();
            int totalrow = (src.Rows.Count / totalRecord);
            if (totalrow == 0) totalrow = 1;
            if (src.Rows.Count < totalRecord) totalRecord = src.Rows.Count;
            lblStep.Text = "Divided " + src.Rows.Count + " To " + totalrow.ToString() + " Set";
            Application.DoEvents();
            foreach (DataRow dr in src.Rows)
            {
                rowprocess++;
                if(rowprocess >totalRecord)
                {
                    tbcount++;
                    dst.TableName = "TransHeaderSet" + tbcount.ToString();                    
                    
                    string xmlData = xml.GetXML(dst);
                    string result = "";
                    try
                    {
                        IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
                        result = svc.ProcessDataXML(filename, xmlData) + "\n";
                        svc.Close();
                    }
                    catch (Exception ex)
                    {
                        result = "Error : " + ex.Message + "\n";                        
                    }
                    log += result + "\n";
                    if(result.Substring(0,1)!="E")
                    {
                        rowcount++;
                        lblFinish.Text = rowcount.ToString();
                    }
                    else
                    {
                        ds.Tables.Add(dst);
                        lblTotal.Text = (Convert.ToInt32(lblTotal.Text) + 1).ToString();
                    }
                    lblProcess.Text = tbcount.ToString() + " Set";
                    Application.DoEvents();

                    dst = new DataTable();
                    dst = dt.Clone();
                    rowprocess = 1;

                }
                for(int i=0;i<src.Columns.Count;i++)
                {
                    try
                    {
                        dr[i] = dr[i].ToString().Trim();
                    }
                    catch
                    {

                    }
                }
                dst.ImportRow(dr);
            }
            if(dst.Rows.Count>0)
            {
                string xmlData = xml.GetXML(dst);
                string result = "";
                try
                {
                    IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
                    result = svc.ProcessDataXML(filename, xmlData) + "\n";
                    svc.Close();
                }
                catch (Exception ex)
                {
                    result = "Error : " + ex.Message + "\n";
                }
                log += result + "\n";
                if (result.Substring(0, 1) != "E")
                {
                    rowcount++;
                    lblFinish.Text = rowcount.ToString();
                }
                else
                {
                    ds.Tables.Add(dst);
                    lblTotal.Text = (Convert.ToInt32(lblTotal.Text) + 1).ToString();
                }
                lblProcess.Text = tbcount.ToString() + " Set";
                Application.DoEvents();
            }
            if(ds.Tables.Count>0)
            {
                ds.WriteXml(ds.DataSetName + ".xml");
            }
            xml.Dispose();
            return log;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                using (ClsConnectSql db = new ClsConnectSql())
                {
                    if (db.isReady() == true)
                    {
                        dataGridView2.DataSource = db.RecordSet(GetSQLDetail(" ID='" + id + "'"));
                    }
                    db.CloseConnection();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void saveXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveXML();
        }
        private string GetYear(string yy)
        {
            string current = DateTime.Now.Year.ToString();
            string val = yy;
            if(yy.Length ==2)
            {
                int diff = Convert.ToInt32(current) - Convert.ToInt32(yy);
                if(diff<2500 && diff>2000)
                {
                    val = (Convert.ToInt32(yy) + 2000).ToString();
                }
                else
                {
                    val = (Convert.ToInt32(yy) + (2500-543)).ToString();
                }
            }
            if (yy.Length == 4)
            {
                if(Convert.ToInt32(yy)>2500)
                {
                    val = (Convert.ToInt32(yy) - 543).ToString();
                }
            }
            return val;
        }
        private string defaultfilename(string postfix="")
        {
            return "Delivery" + postfix + GetYear(txtYear.Text) + Convert.ToInt16(txtMonth.Text).ToString("00");
        }
        private void loadWebDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetWebData(defaultfilename());
            dataGridView2.DataSource = GetWebData(defaultfilename("Details"));
        }
        private DataTable CompareData(string webdbname,string dbsql)
        {
            DataTable result = new DataTable();
            try
            {
                DataTable tbWeb = GetWebData(webdbname);
                ClsConnectSql db = new ClsConnectSql();
                DataTable dt = db.RecordSet(dbsql);
                if (tbWeb.Columns[0].ColumnName == dt.Columns[0].ColumnName)
                {
                    result = db.CompareTable(dt, tbWeb);
                }
                else
                {
                    result = dt;
                }
                db.CloseConnection();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        private void updateToWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "--Start Process "+ DateTime.Now.ToString()+ "\n";
            string tbNameH = defaultfilename();
            string tbNameD = defaultfilename("Details");

            button2.Enabled = false;
            groupBox1.Visible = true;
            Application.DoEvents();
            //prepare data first
            DataTable dtHead= CompareData(tbNameH,GetSQLHeader());
            DataTable dtDet = CompareData(tbNameD,GetSQLDetail());
            dtHead.TableName = defaultfilename();
            dtDet.TableName = defaultfilename("Details");
            //write data to be sent
            dtHead.WriteXml(dtHead.TableName + ".xml");
            dtDet.WriteXml(dtDet.TableName + ".xml");
            //begin process 
            labRec.Text = "Header =" + dtHead.Rows.Count + " Detail =" + dtDet.Rows.Count;
            Application.DoEvents();
            //process header
            if(dtHead.Rows.Count>0)
            {
                msg += UploadToWeb(defaultfilename(), dtHead) + "\n";
            }
            //process detail
            lblFinish.Text = "0";
            lblTotal.Text = "0";
            if(dtDet.Rows.Count>0)
            {
                msg += UploadToWeb(defaultfilename("Details"), dtDet) + "\n";
            }
            //write log
            StreamWriter wr = new StreamWriter("Process"+ DateTime.Today.ToString ("yyyyMMdd")+".log", false, Encoding.UTF8);
            wr.WriteLine(msg);
            wr.Close();
            msg += "-- Finished " + DateTime.Now.ToString()+ "\n";
            msg = msg.Replace("\n", Environment.NewLine);
            button2.Enabled = true;
            MessageBox.Show("Complete!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtCustomer.Text = cboCustomer.SelectedValue.ToString();
            }
            catch
            {

            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cboCustomer.SelectedValue = txtCustomer.Text;
            }
            catch
            {
                cboCustomer.SelectedValue = -1;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void syncDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void xMLViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private string GetSQLHeader()
        {
            return "select * from " + ClsData.TransHeaderI(GetWhere()) + " " + GetCliteria() + " order by ID";
        }
        private string GetSQLDetail(string addWhere="")
        {
            return "select ID+'_'+convert(varchar,Sequence) as ID_Detail,Sequence as ItemNo,Product,ProductName,Remark,VAT,Packs,DQty,DDiscPer,UnitPrice,DAmount,TotalCost from " + ClsData.TransHeaderI_Detail(GetWhere()) + " " + GetCliteria(addWhere) +" order by ID,Sequence";
        }

        private void viewXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }

        private void viewJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.ShowDialog();
        }

        private void saveJOSNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveJSON();
        }
    }
}
