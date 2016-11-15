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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            txtAppSrc.Text = Application.StartupPath;
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            DateTime currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime previousMonth = currentMonth.AddDays(-1);
            LoadList(currentMonth.ToString("yyyyMM"), previousMonth.ToString("yyyyMM"));
        }
        private void LoadList(string currentMonth, string previousMonth)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Delivery" + currentMonth);
            listBox1.Items.Add("DeliveryDetails" + currentMonth);
            listBox1.Items.Add("Delivery" + previousMonth);
            listBox1.Items.Add("DeliveryDetails" + previousMonth);
        }
        private void AddLog(string str)
        {
            StreamWriter wr = new StreamWriter("Process"+DateTime.Now.ToString("yyyyMMdd")+".log",true, UTF8Encoding.UTF8);
            wr.WriteLine(str);
            wr.Close();

            listBox2.Items.Add(str);
            listBox2.SetSelected(listBox2.Items.Count - 1, true);
            Application.DoEvents();
        }
        private string GetWhere(string datein)
        {
            string str = @" AND ((";
            datein = datein.Substring(datein.Length - 6);
            string yy = datein.Substring(0, 4);
            string mm = datein.Substring(4, 2);
            str += "Year(a.DocDate)=" + yy + " AND ";
            str += "Month(a.DocDate)=" + mm + ") ";
            //str +=" OR (";
            //str += @"a.Mark6 Like '%" + Convert.ToInt32(mm).ToString() + "/"+(CFunction.CInt32(yy) + 543 - 2500).ToString() + "%')";
            str += ")";
            return str;
        }
        private void CompareDatabaseToWeb(bool CheckWeb = false,bool UpdateWeb=false)
        {
            ClsXML xml = new ClsXML();
            ClsConnectSql db = new ClsConnectSql();
            int tbCount = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                DataTable dt = new DataTable(); //database source
                DataTable rs = new DataTable(); //web source
                bool loadOK = false;
                if (CheckWeb == false)
                {
                    loadOK = xml.LoadXML(txtWebSrc.Text + @"\\" + listBox1.Items[i].ToString() + ".xml", true);
                    if(loadOK==true)
                    {
                        rs = xml.Datatable().Copy();
                    }
                }
                else
                {
                    rs = xml.GetWebData(listBox1.Items[i].ToString());
                    loadOK = (rs.Columns.Count > 1);
                    if (loadOK == true)
                    {
                        rs.WriteXml(txtWebSrc.Text + @"\" + listBox1.Items[i].ToString() + ".xml", true);
                    }
                }
                if (loadOK == true)
                {
                    tbCount++;
                    rs.TableName = listBox1.Items[i].ToString();
                    AddLog("Found " + rs.Rows.Count + " On Web >" + listBox1.Items[i].ToString() + ".xml");
                    if (listBox1.Items[i].ToString().IndexOf("Details") > 0)
                    {
                        dt = SaveXMLDetail(listBox1.Items[i].ToString(),"TranDetails");
                        dt.TableName = listBox1.Items[i].ToString();
                    }
                    else
                    {
                        dt = SaveXMLHeader(listBox1.Items[i].ToString(),"TranHeaders");
                        dt.TableName = listBox1.Items[i].ToString();
                    }
                    //check data on web compare to db if not found =delete
                    DataTable tmp = db.CompareTable(rs, dt, true,true);
                    if(tmp.Rows.Count>0)
                    {
                        AddLog("Checking Deleted Data.." + listBox1.Items[i].ToString());
                        string fname = listBox1.Items[i].ToString();
                        string colname = tmp.Columns[0].ColumnName;
                        string cliteria = "";
                        AddLog("Begin Check " + fname);
                        int c = 0;
                        int t = 0;
                        int limit = 20;
                        cliteria = "";
                        IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
                        foreach (DataRow dr in tmp.Rows)
                        {
                            c++;
                            t++;
                            if(t==tmp.Rows.Count)
                            {
                                if (cliteria != "") cliteria += " OR ";
                                cliteria += "[" + colname + "]='" + dr[0].ToString() + "'";
                            }
                            if (c >limit||t==tmp.Rows.Count)
                            {
                                if (UpdateWeb == true)
                                {
                                    try
                                    {
                                        //AddLog("Deleting " + cliteria);
                                        string msg = svc.RemoveDataXML(fname, cliteria);
                                        if (msg.Substring(0, 1) == "C")
                                        {
                                            AddLog("Deleted " + cliteria);
                                        }
                                        else
                                        {
                                            AddLog("Delete Fail " + cliteria);
                                            AddLog("ERROR :" + msg);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        AddLog("ERROR : " + ex.Message);
                                    }
                                }
                                else
                                {
                                    AddLog("Found " + cliteria);
                                }
                                c = 1;
                                cliteria = "";
                                cliteria = "[" + colname + "]='" + dr[0].ToString() + "'";
                            }
                            else
                            {
                                if (cliteria != "") cliteria += " OR ";
                                cliteria += "[" + colname + "]='" + dr[0].ToString() + "'";
                            }
                        }
                        svc.Close();
                        AddLog("End Check " + fname);
                    }
                    else
                    {
                        AddLog(listBox1.Items[i].ToString() + " have no deleted data");
                    }
                    //check data on db compare with web if found=update or insert
                    DataTable tb = db.CompareTable(dt, rs, true);
                    if (tb.Rows.Count > 0)
                    {                        
                        AddLog("Compare " + listBox1.Items[i].ToString() + " New=" + dt.Rows.Count + " Old=" + rs.Rows.Count + " Diff=" + tb.Rows.Count);
                        tb.TableName = listBox1.Items[i].ToString();
                        string fname = tb.TableName;
                        if (CreateDataToWeb(fname, tb) == true)
                        {
                            if (UpdateWeb == true)
                            {
                                AddLog("Begin update " + fname);
                                ProcessUpdate(fname);
                                AddLog("End update " + fname);
                            }
                        }
                    }
                    else
                    {
                        AddLog(listBox1.Items[i].ToString() + " have no changed data");
                    }
                    listBox2.TopIndex = listBox2.Items.Count - 1;
                }
                else
                {
                    AddLog("Cannot Read Data->" + listBox1.Items[i].ToString());
                }
            }
            db.CloseConnection();
            xml.Dispose();
        }
        protected bool CreateDataToWeb(string fname, DataTable dt)
        {
            bool success = false;
            AddLog(" Start Created " + fname + " at " + DateTime.Now.ToString() + "");
            try
            {
                success= CreateJSONTable(dt, CFunction.CInt(txtRows.Text), fname);
            }
            catch (Exception ex)
            {
                AddLog("ERROR " + ex.Message);
            }
            AddLog(" Finished Process " + fname + " at " + DateTime.Now.ToString() + "");
            return success;
        }
        private bool CreateJSONTable(DataTable dt, int totalRecord, string filename)
        {
            bool isCreated = false;
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            DataSet ds = new DataSet();
            ds.DataSetName = filename + "Error";
            string log = "";
            DataTable src = dt.Copy();
            int rowprocess = 0;
            int tbcount = 0;
            int rowcount = 0;
            int currentrow = 0;
            DataTable dst = dt.Clone();
            int totalrow = (src.Rows.Count / totalRecord);
            if (totalrow == 0) totalrow = 1;
            if (src.Rows.Count < totalRecord) totalRecord = src.Rows.Count;
            AddLog("Divided " + src.Rows.Count + " To " + totalrow.ToString() + " Set");
            AddLog("Creating...");
            foreach (DataRow dr in src.Rows)
            {
                rowprocess++;
                currentrow++;
                if (rowprocess > totalRecord||currentrow==src.Rows.Count)
                {
                    tbcount++;
                    if(currentrow ==src.Rows.Count)
                    {
                        for (int i = 0; i < src.Columns.Count; i++)
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
                    dst.TableName = "TransHeaderSet" + tbcount.ToString();

                    string JSONData = ClsData.GetJSONFromDataTable(dst);
                    string result = "";
                    try
                    {
                        StreamWriter wr = new StreamWriter(filename + "_" + tbcount.ToString() + ".json", false, Encoding.UTF8);
                        wr.Write(JSONData);
                        wr.Close();
                        result = "Complete!";
                    }
                    catch (Exception ex)
                    {
                        result = "Error : " + ex.Message + "";
                    }
                    log += result + "";
                    if (result.Substring(0, 1) != "E")
                    {
                        rowcount++;
                        listBox2.Items[listBox2.Items.Count - 1] = "Created " + rowcount.ToString() + " Set Of " + totalrow.ToString();
                        Application.DoEvents();
                    }
                    else
                    {
                        listBox2.Items[listBox2.Items.Count - 1] = "ERROR " + result;
                        ds.Tables.Add(dst);
                        Application.DoEvents();
                    }
                    dst = new DataTable();
                    dst = dt.Clone();
                    rowprocess = 1;
                }
                if(currentrow <src.Rows.Count)
                {
                    for (int i = 0; i < src.Columns.Count; i++)
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
            }
            //save xml
            if (ds.Tables.Count > 0)
            {
                ds.WriteXml(ds.DataSetName + ".xml");
                AddLog("Create Error Data->" + ds.DataSetName + ".xml");
            }
            svc.Close();
            isCreated = (tbcount > 0);
            return isCreated;
        }

        private DataTable SaveXMLHeader(string fname,string tbName)
        {
            DataTable dtHead = ClsData.GetDeliveryHeader(tbName, GetWhere(fname));
            dtHead.WriteXml(fname + ".xml");
            AddLog(fname + " Created :Rows=" + dtHead.Rows.Count);
            return dtHead;
        }
        private DataTable SaveXMLDetail(string fname,string tbName)
        {
            DataTable dtDet = ClsData.GetDeliveryDetail(tbName, GetWhere(fname));
            dtDet.WriteXml(fname + ".xml");
            AddLog(fname + " Created :Rows=" + dtDet.Rows.Count);
            return dtDet;
        }
        private void CreateXML()
        {
            int c = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                DataTable dt = new DataTable();
                try
                {
                    if (listBox1.Items[i].ToString().IndexOf("Details") > 0)
                    {
                        dt.TableName = listBox1.Items[i].ToString();
                        dt = SaveXMLDetail(txtAppSrc.Text + @"\" + listBox1.Items[i].ToString(),dt.TableName);                        
                    }
                    else
                    {
                        dt.TableName = listBox1.Items[i].ToString();
                        dt = SaveXMLHeader(txtAppSrc.Text + @"\" + listBox1.Items[i].ToString(),dt.TableName);
                    }
                    c++;
                }
                catch
                {

                }
            }
            MessageBox.Show("Finished! (" + c.ToString() + " Files created!)");
        }
        private void RefreshFileList()
        {
            DateTime currentMonth = new DateTime(CFunction.CInt(txtYear.Text), CFunction.CInt(txtMonth.Text), 1);
            DateTime previousMonth = currentMonth.AddDays(-1);
            LoadList(currentMonth.ToString("yyyyMM"), previousMonth.ToString("yyyyMM"));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshFileList();
            button1.Enabled = false;
            CreateXML();
            button1.Enabled = true;
        }
        private int DownloadWeb()
        {
            int c = 0;
            using (ClsXML xml = new ClsXML())
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    DataTable rs = new DataTable();
                    try
                    {
                        rs = xml.GetWebData(listBox1.Items[i].ToString());
                        rs.WriteXml(txtWebSrc.Text + @"\" + listBox1.Items[i].ToString() + ".xml", true);
                        AddLog(txtWebSrc.Text + @"\" + listBox1.Items[i].ToString() + ".xml ROWS=" + rs.Rows.Count.ToString());
                        c++;
                    }
                    catch (Exception e)
                    {
                        AddLog("ERROR " + listBox1.Items[i].ToString() + "> " + e.Message);
                    }
                }
                xml.Dispose();
            }            
            return c;
        }
        private void CheckData(bool fromweb = false, bool autoupdate = false)
        {
            button1.Enabled = false;
            AddLog("CHECKING Start! " + DateTime.Now.ToString());

            CompareDatabaseToWeb(fromweb,autoupdate);

            AddLog("CHECKING Finished! " + DateTime.Now.ToString());
            button1.Enabled = true;
        }
        private void CopyFiles()
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string copyfrom = txtAppSrc.Text + @"\\" + listBox1.Items[i].ToString() + ".xml";
                string copyto = txtWebSrc.Text + @"\\" + listBox1.Items[i].ToString() + ".xml";
                try
                {
                    if(System.IO.File.Exists(copyfrom)==true)
                    {
                        System.IO.File.Copy(copyfrom, copyto, true);
                        System.IO.File.Delete(copyfrom);
                    }
                }
                catch
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            CopyFiles();
            button2.Enabled = true;
            MessageBox.Show("Finished!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            CheckData(true);
            listBox2.SetSelected(listBox2.Items.Count - 1, true);
            button3.Enabled = true;
        }



        private void lblTimes_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtMonth_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtAppSrc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtWebSrc_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dlg.Filter = "JSON Files|*.json;";
            dlg.ShowDialog();
            lblFileName.Text = dlg.FileName;
            if (File.Exists(lblFileName.Text) == true)
            {
                DataTable dt = ClsData.GetDataTableFromJSON(ReadData(lblFileName.Text));
                dataGridView1.DataSource = dt;
                lblTimes.Text = dt.Rows.Count.ToString();
            }

        }
        protected string ReadData(string fname)
        {
            string JsonString = "";
            try
            {
                StreamReader rd = new StreamReader(fname, System.Text.UTF8Encoding.UTF8);
                JsonString = rd.ReadToEnd();
                rd.Close();
            }
            catch
            {

            }
            return JsonString;
        }
        protected string GetTableName(string filename)
        {
            var finfo = new System.IO.FileInfo(filename);
            string fname = finfo.Name;
            fname = fname.Replace(".json", "") + "_";
            var split = fname.Split('_');
            return split[0].ToString();

        }
        private void ProcessUpdate(string fname)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
                IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
                foreach (FileInfo file in dir.GetFiles(fname + "*.json"))
                {
                    AddLog("Reading File " + file.Name);
                    string JsonData = ReadData(file.FullName);
                    string tableName = GetTableName(file.FullName);
                    if (JsonData != "")
                    {
                        AddLog("Updating File " + file.FullName);
                        try
                        {
                            string msg = svc.ProcessDataJSON(tableName, JsonData);
                            if (msg.Substring(0, 1) == "C")
                            {
                                file.Delete();
                            }
                            AddLog(msg);
                        }
                        catch (Exception ex)
                        {
                            AddLog("ERROR:" + ex.Message);
                        }
                    }
                }
                svc.Close();
                }
           catch (Exception ex)
           {
                AddLog("ERROR :" + ex.Message);
           }

        }
        private void Updatecustomer()
        {
            DataTable tb = ClsData.GetCustomer();
            DataTable rs = tb.Clone();
            rs.TableName = "Table";
            tb.WriteXml("Customer.xml");
            AddLog("Customer.xml created! Rows=" + tb.Rows.Count);
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();            
            ClsXML xml = new ClsXML();
            ClsConnectSql db = new ClsConnectSql();
            xml.LoadXMLString(svc.GetDataXML("Customer", false));
            DataTable web = xml.Datatable();
            DataTable dt = db.CompareTable(tb, web);            
            if(dt.Rows.Count>1)
            {
                AddLog("Check Customer Web=" + web.Rows.Count + " Diff="+dt.Rows.Count);
                string msg = "";
                int i = 0;
                int j = 0;
                int k = Convert.ToInt16(txtRows.Text);
                int l = 0;
                rs.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    j++; //row count
                    if (j == dt.Rows.Count)
                    {
                        //if reach final record then process immediately
                        i = k;
                        rs.ImportRow(dr);
                    }
                    if (i >= k)
                    {
                        //when reach limit uploaded to web
                        msg = svc.ProcessDataXML("Customer", xml.GetXML(rs));
                        if (msg.Substring(0, 1) == "C")
                        {
                            Application.DoEvents();
                            l++;
                            AddLog("Process Customer " + j + " of " + dt.Rows.Count);
                        }
                        //reset value and begin to read next set of data
                        i = 1;
                        if (j < dt.Rows.Count)
                        {
                            //if not last record
                            rs.Rows.Clear();
                            rs.ImportRow(dr);
                        }
                    }
                    else
                    {
                        //read data
                        i++;
                        rs.ImportRow(dr);
                    }
                }
                AddLog("Customer =" + l.ToString() + " Rows Updated!");
            }
            else
            {
                AddLog("Customer data Updated!");
            }
            svc.Close();
        }
        private void UpdateWeb()
        {
            timer1.Enabled = false;
            DateTime currentMonth = new DateTime(CFunction.CInt(txtYear.Text), CFunction.CInt(txtMonth.Text), 1);
            DateTime previousMonth = currentMonth.AddDays(-1);
            LoadList(currentMonth.ToString("yyyyMM"), previousMonth.ToString("yyyyMM"));
            AddLog("START Update Web " + DateTime.Now.ToString());
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                AddLog("Updating File " + listBox1.Items[i].ToString());
                ProcessUpdate(listBox1.Items[i].ToString());
            }
            AddLog("FINISHED Update Web " + DateTime.Now.ToString());
            timer1.Enabled = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Updatecustomer();
            UpdateWeb();
        }

        private void lblFileName_Click(object sender, EventArgs e)
        {
            textBox1.Visible = !textBox1.Visible;
            if(textBox1.Visible==true)
            {
                if (File.Exists(lblFileName.Text) == true)
                {
                    textBox1.Text = ReadData(lblFileName.Text);
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }
        private void ProcessData()
        {
            timer1.Enabled = false;
            AddLog("SCHEDULE RUN AT:" + DateTime.Now.ToString());
            try
            {
                CheckData(true, true);
                CopyFiles();
                AddLog("SCHEDULE FINISHED AT:" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                AddLog("SCHEDULE ERROR : " + ex.Message);
            }
            timer1.Enabled = true;
        }
        private void ScheduleRun()
        {
            this.Text = DateTime.Now.ToString();
            if (this.Tag == null)
            {
                this.Tag = DateTime.Now.Hour.ToString();
            }
            if(DateTime.Now.Hour>=8 && DateTime.Now.Hour <=18)
            {
                if (this.Tag.ToString() != DateTime.Now.Hour.ToString() || (DateTime.Now.Minute == 30 && DateTime.Now.Second == 1))
                {
                    listBox2.Items.Clear();
                    ProcessData();
                    this.Tag = DateTime.Now.Hour.ToString();
                }
                if ((DateTime.Now.Minute == 15 || DateTime.Now.Minute == 45) && DateTime.Now.Second == 1)
                {
                    listBox2.Items.Clear();
                    Updatecustomer();
                    UpdateWeb();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ScheduleRun();   
        }

        private void dataViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }

        private void xMLViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }

        private void jSONViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.ShowDialog();
        }

        private void clearLogFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Windows\System32\NOTEPAD.exe", Application.StartupPath + @"\Process"+ DateTime.Now.ToString("yyyyMMdd") + ".log");
        }

        private void scheduleRunNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Updatecustomer();
            //update old data first
            UpdateWeb();
            //update new data
            ProcessData();
        }

        private void downloadFromWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int c=DownloadWeb();
            MessageBox.Show("Finished! (" + c.ToString() + " Files created!)");
        }

        private void compareXMLDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            CheckData(false);
            listBox2.SetSelected(listBox2.Items.Count - 1, true);
            button3.Enabled = true;
        }

        private void uploadCustomerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.ShowDialog();
        }

        private void testConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            //MessageBox.Show(svc.GetDeliveryInfo("I1-9020276","2"));
            MessageBox.Show(svc.Login("wm001", "4780"));
            svc.Close();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string fname = listBox1.SelectedItem.ToString();
            ClsXML xml = new ClsXML(txtWebSrc.Text + "\\"+ fname+ ".xml");
            dataGridView1.DataSource = xml.Datatable();
        }

        private void uploadJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm = new Form6();
            frm.ShowDialog();
        }

        private void todayLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete(Application.StartupPath + @"\\Process" + DateTime.Now.ToString("yyyyMMdd") + ".log");
            MessageBox.Show("Completed!");
            listBox2.Items.Clear();
        }

        private void allLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
            foreach (FileInfo file in dir.GetFiles("*.log"))
            {
                file.Delete();
            }
            listBox2.Items.Clear();
            MessageBox.Show("Completed!");
        }

        private void dlg_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void clearJSONDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
            foreach (FileInfo file in dir.GetFiles("*.json"))
            {
                file.Delete();
            }
            listBox2.Items.Clear();
            MessageBox.Show("Completed!");
        }
    }
}
