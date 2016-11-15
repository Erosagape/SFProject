using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace SfDeliverTracking
{
    public partial class Form6 : Form
    {
        private int limit = 50;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            txtAppSrc.Text = Application.StartupPath;
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtLimit.Text = limit.ToString();
            LoadFileList(txtAppSrc.Text);
            this.Text = this.Text + " (" + this.ProductVersion +")";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadFileList(txtAppSrc.Text);
        }
        private void LoadFileList(string path)
        {
            listBox1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo file in dir.GetFiles("*.json"))
            {
                listBox1.Items.Add(file.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostData(false);
            MessageBox.Show("Complete!..Please see log");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateData();
            MessageBox.Show("Complete!..Please see log");
        }
        private void UpdateData()
        {
            string msg = "";
            string chk = "";
            int fcount = 1;
            try
            {
                using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                    
                        string fname = listBox1.Items[i].ToString();
                        string dataname = fname.Substring(0, fname.IndexOf("_"));
                        if(fcount==6||chk=="")
                        {
                            fcount = 1;
                            if (chk != "")
                            {
                                msg=svc.CloseDataForupdate(chk);
                                ListAdd("Save " + chk + ":" + msg);
                            }                                
                            chk = dataname;
                            msg=svc.OpenDataForupdate(chk);
                            ListAdd("Open " + chk+ ":" + msg);
                            Application.DoEvents();
                        }
                        fcount++;
                        msg = svc.ReadDataForupdate(fname.Replace(".json",""));
                        if(msg.Substring(0,1)=="C")
                        {
                            File.Delete(txtAppSrc.Text + "\\" + listBox1.Items[i].ToString());
                        }
                        ListAdd("Read " + fname + ":" + msg);
                        Application.DoEvents();
                    }
                    if (chk != "")
                    {
                        msg = svc.CloseDataForupdate(chk);
                        ListAdd("Save " + chk + ":" + msg);
                    }
                }
            }
            catch (Exception ex)
            {
                ListAdd("RRROR:" + ex.Message);
                Application.DoEvents();
            }
            LoadFileList(txtAppSrc.Text);
        } 
        private void UpdateAllDataOnWeb()
        {
            string msg = "Error : Cannot Process";            
            //update current month
            using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
            {                
                string fname = "Delivery2*";
                try { msg = svc.UpdateData(fname) + "\n"; } catch (Exception ex) { msg = "Error:" + ex.Message; }
                Application.DoEvents();
                ListAdd(msg);

                fname = "DeliveryDetails2*";
                try { msg = svc.UpdateData(fname) + "\n"; } catch (Exception ex) { msg = "Error:" + ex.Message; }
                Application.DoEvents();
                ListAdd(msg);
            }                                    
        }
        private void PostData(bool DeleteNow)
        {
            //post difference data to web
            using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    try
                    {
                        string FullName = txtAppSrc.Text + "\\" + listBox1.Items[i].ToString();
                        StreamReader rd = new StreamReader(FullName, System.Text.UTF8Encoding.UTF8);
                        String JsonStr = rd.ReadToEnd();
                        rd.Close();
                        if (JsonStr != "")
                        {
                            string msg = svc.WriteDataUpdate(listBox1.Items[i].ToString().Replace(".json", ""), JsonStr);
                            if(DeleteNow==true)
                            {
                                if (msg.Substring(0, 1) == "C")
                                {
                                    File.Delete(txtAppSrc.Text + "\\" + listBox1.Items[i].ToString());
                                }
                            }
                            ListAdd(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        ListAdd(ex.Message);
                    }
                    Application.DoEvents();
                }
            }
            if(DeleteNow) LoadFileList(txtAppSrc.Text);
        }
        private void ProcessData(string yy, string mm, bool saveXML,bool createJSON)
        {
            string wherec = "AND (Year(a.DocDate)=" + CFunction.CInt(yy) + " and Month(a.DocDate)=" + CFunction.CInt(mm) + ")";
            DataTable rsHeader = new DataTable();
            DataTable rsDetail = new DataTable();

            DataTable dtHeader = ClsData.GetDeliveryHeader("Delivery" + yy + mm, wherec);
            if(saveXML==true)
            {
                dtHeader.WriteXml(txtWebSrc.Text + @"\\Delivery" + yy+ mm+ ".xml");
            }
            ListAdd(dtHeader.TableName + " Created (" + dtHeader.Rows.Count + ")");
            Application.DoEvents();
/*
            DataTable dtDetail = ClsData.GetDeliveryDetail("DeliveryDetails" + yy+mm, wherec);
            if (saveXML == true)
            {
                dtDetail.WriteXml(txtWebSrc.Text + @"\\DeliveryDetails" + yy + mm + ".xml");
            }
            ListAdd(dtDetail.TableName + " Created (" + dtDetail.Rows.Count + ")");
            Application.DoEvents();
*/
            //compare data between web and databases
            if (createJSON==true)
            {
                //start get web data
                using (ClsXML xml = new ClsXML())
                {
                    rsHeader = new DataTable();
                    try
                    {
                        rsHeader = xml.GetWebData(dtHeader.TableName);
                        if (xml.Errormessage() != "")
                        {
                            ListAdd(xml.Errormessage());
                        }
                        else
                        {
                            ListAdd(dtHeader.TableName + " Get! (" + rsHeader.Rows.Count + ")");
                        }
                    }
                    catch (Exception e)
                    {
                        ListAdd("ERROR (" + dtHeader.TableName + ") " + e.Message);
                    }
                    Application.DoEvents();
/*
                    rsDetail = new DataTable();
                    try
                    {
                        rsDetail = xml.GetWebData(dtDetail.TableName);
                        if (xml.Errormessage() != "")
                        {
                            ListAdd(xml.Errormessage());
                        }
                        else
                        {
                            ListAdd(dtDetail.TableName + " Get! (" + rsDetail.Rows.Count + ")");
                        }
                    }
                    catch (Exception e)
                    {
                        ListAdd("ERROR (" + dtDetail.TableName + ") " + e.Message);
                    }
                    Application.DoEvents();
*/
                }

                //begin check table
                using (ClsConnectSql db = new ClsConnectSql())
                {
                    int c = 0;
                    int i = 0;
                    int j = 0;
                    try { limit = Convert.ToInt32(txtLimit.Text); } catch { }
                    //check header first
                    if (rsHeader.Rows.Count > 0)
                    {
                        try
                        {                            
                            DataTable tbHeader = rsHeader.Columns.Count==1 ? dtHeader : db.CompareTable(dtHeader, rsHeader, true,false,"Mark8");
                            ListAdd("Diff Header=" + tbHeader.Rows.Count);
                            if (tbHeader.Rows.Count > 0)
                            {
                                DataTable tmp = tbHeader.Clone();
                                foreach (DataRow dr in tbHeader.Rows)
                                {
                                    i++;
                                    j++;
                                    if (i == limit || j == tbHeader.Rows.Count)
                                    {
                                        c++;
                                        tmp.ImportRow(dr);
                                        string JsonHeader = ClsData.GetJSONFromDataTable(tmp);
                                        StreamWriter wrHeader = new StreamWriter(txtAppSrc.Text + @"\\" + dtHeader.TableName + "_" + c + ".json");
                                        wrHeader.Write(JsonHeader);
                                        wrHeader.Close();
                                        ListAdd(dtHeader.TableName + "_" + c + ".json Created!");
                                        tmp.Rows.Clear();
                                        Application.DoEvents();
                                        i = 0;
                                    }
                                    else
                                    {
                                        tmp.ImportRow(dr);
                                    }
                                }
                            }
                            //check deleted data header
                            tbHeader = rsHeader.Columns.Count == 1 ? new DataTable() : db.CompareTable(rsHeader, dtHeader, true, true);
                            if (tbHeader.Rows.Count>0)
                            {
                                int l = 20;
                                int r = 0;
                                string cliteria = "";
                                string colname = rsHeader.Columns[0].ColumnName;
                                foreach(DataRow row in tbHeader.Rows)
                                {
                                    r++;
                                    if (r <= l)
                                    {
                                        if (cliteria != "") cliteria += " OR ";
                                        cliteria += "[" + colname + "] ='" + row[colname].ToString() + "' ";
                                    }
                                    if(c==i||c==tbHeader.Rows.Count)
                                    {
                                        string msg= DeleteRows(dtHeader.TableName, cliteria);
                                        ListAdd("DEL " + dtHeader.TableName + " > " +  msg);
                                        cliteria = "";
                                        r = 0;
                                    }
                                }
                            }
                            else
                            {
                                ListAdd(dtHeader.TableName + " No data deleted!");
                            }
                        }
                        catch (Exception e)
                        {
                            ListAdd("ERROR COMPARE(" + dtHeader.TableName + ") " + e.Message);
                        }
                        Application.DoEvents();
                    }
                    else
                    {
                        ListAdd("ERROR Cannot Compare " + dtHeader.TableName);
                        Application.DoEvents();
                    }
                    //check details
/*
                    if (rsDetail.Rows.Count > 0)
                    {
                        try
                        {
                            DataTable tbDetail = db.CompareTable(dtDetail, rsDetail, true);
                            ListAdd("Diff Detail=" + tbDetail.Rows.Count);
                            if (tbDetail.Rows.Count > 0)
                            {
                                c = 0;
                                i = 0;
                                j = 0;
                                DataTable tmp = tbDetail.Clone();
                                foreach (DataRow dr in tbDetail.Rows)
                                {
                                    i++;
                                    j++;
                                    if (i == limit || j == tbDetail.Rows.Count)
                                    {
                                        c++;
                                        tmp.ImportRow(dr);
                                        string JsonDetail = ClsData.GetJSONFromDataTable(tmp);
                                        StreamWriter wrDetail = new StreamWriter(txtAppSrc.Text + @"\\" + dtDetail.TableName + "_" + c + ".json");
                                        wrDetail.Write(JsonDetail);
                                        wrDetail.Close();
                                        ListAdd(dtDetail.TableName + "_" + c + ".json Created!");
                                        tmp.Rows.Clear();
                                        Application.DoEvents();
                                        i = 0;
                                    }
                                    else
                                    {
                                        tmp.ImportRow(dr);
                                    }
                                }
                            }
                            //check deleted data detail
                            tbDetail = db.CompareTable(rsDetail, dtDetail, true, true);
                            if (tbDetail.Rows.Count > 0)
                            {
                                int l = 20;
                                int r = 0;
                                string cliteria = "";
                                string colname = rsDetail.Columns[0].ColumnName;
                                foreach (DataRow row in tbDetail.Rows)
                                {
                                    r++;
                                    if (r <= l)
                                    {
                                        if (cliteria != "") cliteria += " OR ";
                                        cliteria += "[" + colname + "] ='" + row[colname].ToString() + "' ";
                                    }
                                    if (c == i || c == tbDetail.Rows.Count)
                                    {
                                        string msg=DeleteRows(dtDetail.TableName, cliteria);
                                        ListAdd("DEL " + dtDetail.TableName + " > " +  msg);
                                        cliteria = "";
                                        r = 0;
                                    }
                                }
                            }
                            else
                            {
                                ListAdd(dtDetail.TableName + " No data deleted!");
                            }

                        }
                        catch (Exception e)
                        {
                            ListAdd("ERROR COMPARE(" + dtDetail.TableName + ") " + e.Message);
                        }
                        Application.DoEvents();
                    }
                    else
                    {
                        ListAdd("Cannot Compare " + dtDetail.TableName);
                        Application.DoEvents();
                    }
                    db.CloseConnection();
*/
                }

            }
        }
        private void CreateData(bool genXML,bool genJSON)
        {
            DateTime currentMonth = new DateTime(CFunction.CInt32(txtYear.Text), CFunction.CInt32(txtMonth.Text), 1);
            DateTime previousMonth = currentMonth.AddDays(-1);
            DateTime Last2Month = currentMonth.AddMonths(-2);
            string yymmCurrentMonth = currentMonth.ToString("yyyyMM");
            string yymmLastMonth = previousMonth.ToString("yyyyMM");
            string yymmLast2Month = Last2Month.ToString("yyyyMM");
            //check current month
            ProcessData(yymmCurrentMonth.Substring(0, 4), yymmCurrentMonth.Substring(4, 2),genXML,genJSON);
            //check previous month
            ProcessData(yymmLastMonth.Substring(0, 4), yymmLastMonth.Substring(4, 2),genXML,genJSON);
            //check last 2 month
            ProcessData(yymmLast2Month.Substring(0, 4), yymmLast2Month.Substring(4, 2), genXML, genJSON);
            //refresh files
            LoadFileList(txtAppSrc.Text);
            Application.DoEvents();
        }
        private void RunBundleProcess()
        {
            CreateData(false, true);
            PostData(false);
            UpdateData();
        }

        private void createJSONDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Updatecustomer(100);
            RunBundleProcess();
            MessageBox.Show("Complete!..Please see log");
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadJsonToGrid()
        {
            try
            {
                StreamReader rd = new StreamReader(txtAppSrc.Text + @"\\" + listBox1.SelectedItem.ToString(), System.Text.UTF8Encoding.UTF8);
                string JsonString = rd.ReadToEnd();
                rd.Close();
                DataTable dt = ClsData.GetDataTableFromJSON(JsonString);
                dataGridView1.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedIndex>=0)
            {
                LoadJsonToGrid();
            }
        }

        private void ClearJSONfiles()
        {
            DirectoryInfo dir = new DirectoryInfo(txtAppSrc.Text);
            foreach (FileInfo file in dir.GetFiles("*.json"))
            {
                file.Delete();
            }
        }
        private void clearJSONDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearJSONfiles();
            LoadFileList(txtAppSrc.Text);
            MessageBox.Show("Completed!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateData(false,true);
            MessageBox.Show("Complete!..Please see log");
        }
        private void ListAdd(string msg, bool updateLast = false)
        {
            if(updateLast==true)
            {
                listBox2.Items[listBox2.Items.Count - 1] = msg;
            }
            else
            {
                listBox2.Items.Add(msg);
            }
            listBox2.SetSelected(listBox2.Items.Count - 1, true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if((DateTime.Now.Minute ==0|| DateTime.Now.Minute == 30) && DateTime.Now.Second==0)
            {
                label5.Text = DateTime.Now.ToString();
                listBox2.Items.Clear();
                ListAdd("Start Schedule At :" + label5.Text);
                timer1.Enabled = false;
                Updatecustomer(100);
                RunBundleProcess();
                ListAdd("End Schedule At :" + DateTime.Now.ToString());
                SaveLog();
            }
            timer1.Enabled = true;
            label5.Text = DateTime.Now.ToString();
        }
        private void SaveLog()
        {
            StreamWriter wr = new StreamWriter("Process" + DateTime.Now.ToString("yyyyMMdd") + ".log", true, UTF8Encoding.UTF8);
            for(int i=0;i<listBox2.Items.Count;i++)
            {
                wr.WriteLine(listBox2.Items[i].ToString());
            }
            wr.Close();
        }
        private string DeleteRows(string fname,string wherestr)
        {
            string msg = "";
            using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
            {
                try
                {
                    msg = svc.RemoveDataXML(fname, wherestr);
                }
                catch (Exception e)
                {
                    msg = "ERROR ->" + e.Message;
                }
            }
            return msg;
        }
        private int Updatecustomer(int limit)
        {
            DataTable rs = new DataTable();
            DataTable dt = new DataTable();
            ListAdd("Begin Update Customer");
            try
            {
                dt = ClsData.GetCustomer();
                rs = dt.Clone();
                rs.TableName = "Table";
                dt.WriteXml("Customer.xml");
            }
            catch (Exception e)
            {
                ListAdd("ERROR XML Customer >" + e.Message);
            }
            string msg = "";
            int i = 0;
            int j = 0;
            int k = limit;
            int l = 0;
            using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
            {
                ClsXML xml = new ClsXML();
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
                        try
                        {
                            msg = svc.ProcessDataXML("Customer", xml.GetXML(rs));
                        }
                        catch (Exception ex)
                        {
                            msg = "ERROR > " + ex.Message;
                        }
                        if (msg.Substring(0, 1) == "C")
                        {
                            l++;
                            ListAdd("Process " + j + " of " + dt.Rows.Count, true);
                            Application.DoEvents();
                        }
                        else
                        {
                            ListAdd(msg);
                            Application.DoEvents();
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
                svc.Close();
            }
            return i;
        }
        private void updateCustomerToWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = Updatecustomer(100);
            ListAdd("Updated! " + i + " Customers");
        }

        private void showCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt= ClsData.GetCustomer();
            dataGridView1.DataSource = dt;
        }
        private void ClearDataOnWeb()
        {
            using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
            {
                try
                {
                    string msg = svc.ClearDataJSON("Delivery");
                    ListAdd(msg);
                }
                catch (Exception ex)
                {
                    ListAdd("ERROR:"+ ex.Message);
                }
            }
        }
        private int DownloadWebData()
        {
            int c = 0;
            using (ClsXML xml = new ClsXML())
            {
                DataTable files = new DataTable();
                using (IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient())
                {
                    files = xml.Datatable( svc.GetXMLFileList("Delivery*"));
                }
                foreach (DataRow file in files.Rows)
                {
                    DataTable rs = new DataTable();
                    try
                    {
                        ListAdd("Downloading.." + file["filename"].ToString().Replace(".xml", ""));
                        Application.DoEvents();
                        rs = xml.GetWebData(file["filename"].ToString().Replace(".xml", ""));
                        rs.WriteXml(txtWebSrc.Text + @"\" + file["filename"].ToString(), true);
                        ListAdd("SAVED! " + file["filename"].ToString() + " ROWS=" + rs.Rows.Count.ToString());
                        c++;
                    }
                    catch (Exception e)
                    {
                        ListAdd("ERROR " + file["filename"].ToString() + "> " + e.Message);
                    }
                    Application.DoEvents();
                }
                xml.Dispose();
            }
            return c;
        }
        private void xMLManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateData(true, false);
            MessageBox.Show("Complete!...Please see Log!");
        }

        private void getDataFromWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Finished! (" + DownloadWebData().ToString() + " Files created!)");
        }

        private void deliveryViewerToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UpdateAllDataOnWeb();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ClearDataOnWeb();
        }

        private void openConnectxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("NOTEPAD.exe", "Connect.xml");
        }
    }
}
