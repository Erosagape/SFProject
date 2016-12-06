using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SfWebBackoffice
{
    public partial class frmBackupData : Form
    {
        //const string folder_SalesEntry= "web_salesentry";
        //const string folder_Delivery = "web_delivery";
        public frmBackupData()
        {
            InitializeComponent();
        }

        private void frmBackupData_Load(object sender, EventArgs e)
        {
            textBox2.Text = "web_salesentry";
            textBox3.Text = "web_delivery";
            this.Text = this.Text + " (" + this.ProductVersion + ") ";
        }
        private string DownloadSalesEntryData_GoDaddy()
        {
            string msg = "";
            try
            {
                SalesEntryGoDaddy.DataExchangeClient svc = new SalesEntryGoDaddy.DataExchangeClient();
                string xmlStr = svc.GetXMLFileList("*");
                ClsXML xml = new ClsXML();
                DataTable dt = xml.Datatable(xmlStr);
                foreach (DataRow dr in dt.Rows)
                {
                    string dataname = dr["filename"].ToString().Replace(".xml", "");
                    try
                    {
                        string xmlData = svc.GetDataXML(dataname);
                        DataTable tbData = xml.Datatable(xmlData);
                        tbData.WriteXml(textBox1.Text + @"\" + textBox2.Text + @"\" + dr["filename"].ToString());
                        msg += "\n downloaded ->" + dataname;
                        Listbox1.Items.Add("COMPLETED:" + dataname);
                    }
                    catch (Exception ex)
                    {
                        msg += "\n error " + dataname + " ->" + ex.Message;
                        Listbox1.Items.Add("ERROR " + dataname + ":" + ex.Message);
                    }
                    Application.DoEvents();
                    Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                Listbox1.Items.Add("ERROR:" + e.Message);
                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
            }
            return msg;
        }
        private string DownloadSalesEntryData_Azure()
        {
            string msg = "";
            try
            {
                SalesEntryAzure.DataExchangeClient svc =new SalesEntryAzure.DataExchangeClient();
                string xmlStr = svc.GetXMLFileList("*");
                ClsXML xml = new ClsXML();
                DataTable dt = xml.Datatable(xmlStr);
                foreach (DataRow dr in dt.Rows)
                {
                    string dataname = dr["filename"].ToString().Replace(".xml", "");
                    try
                    {
                        string xmlData = svc.GetDataXML(dataname);
                        DataTable tbData = xml.Datatable(xmlData);
                        tbData.WriteXml(textBox1.Text + @"\" + textBox2.Text + @"\" + dr["filename"].ToString());
                        msg += "\n downloaded ->" + dataname;
                        Listbox1.Items.Add("COMPLETED:" + dataname);
                    }
                    catch (Exception ex)
                    {
                        msg += "\n error " + dataname + " ->" + ex.Message;
                        Listbox1.Items.Add("ERROR " + dataname + ":" + ex.Message);
                    }
                    Application.DoEvents();
                    Listbox1.SetSelected(Listbox1.Items.Count - 1,true);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                Listbox1.Items.Add("ERROR:" + e.Message);
                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
            }
            return msg;
        }
        private string DownloadDeliveryData_Azure()
        {
            string msg = "";
            try
            {
                DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient();
                string xmlStr = svc.GetXMLFileList("*");
                ClsXML xml = new ClsXML();
                DataTable dt = xml.Datatable(xmlStr);
                foreach (DataRow dr in dt.Rows)
                {
                    string dataname = dr["filename"].ToString().Replace(".xml", "");
                    try
                    {
                        string xmlData = svc.GetDataXML(dataname, false);
                        DataTable tbData = xml.Datatable(xmlData);
                        tbData.WriteXml(textBox1.Text + @"\" + textBox3.Text + @"\" + dr["filename"].ToString());
                        msg += "\n downloaded ->" + dataname;
                        Listbox1.Items.Add("COMPLETED:" + dataname);
                    }
                    catch (Exception ex)
                    {
                        msg += "\n error " + dataname + " ->" + ex.Message;
                        Listbox1.Items.Add("ERROR "+ dataname+":" + ex.Message);
                    }
                    Application.DoEvents();
                    Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                Listbox1.Items.Add("ERROR:" + e.Message);
                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
            }
            return msg;
        }
        private string DownloadDeliveryData_GoDaddy()
        {
            string msg = "";
            try
            {
                DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient();
                string xmlStr = svc.GetXMLFileList("*");
                ClsXML xml = new ClsXML();
                DataTable dt = xml.Datatable(xmlStr);
                foreach (DataRow dr in dt.Rows)
                {
                    string dataname = dr["filename"].ToString().Replace(".xml", "");
                    try
                    {
                        string xmlData = svc.GetDataXML(dataname, false);
                        DataTable tbData = xml.Datatable(xmlData);
                        tbData.WriteXml(textBox1.Text + @"\" + textBox3.Text + @"\" + dr["filename"].ToString());
                        msg += "\n downloaded ->" + dataname;
                        Listbox1.Items.Add("COMPLETED:" + dataname);
                    }
                    catch (Exception ex)
                    {
                        msg += "\n error " + dataname + " ->" + ex.Message;
                        Listbox1.Items.Add("ERROR " + dataname + ":" + ex.Message);
                    }
                    Application.DoEvents();
                    Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                Listbox1.Items.Add("ERROR:" + e.Message);
                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
            }
            return msg;
        }
        private int Updatecustomer_Azure()
        {
            int limit = 100;
            DataTable rs = new DataTable();
            DataTable dt = new DataTable();
            Listbox1.Items.Add("Begin Update Customer");
            try
            {
                dt = ClsData.GetCustomer();
                rs = dt.Clone();
                rs.TableName = "Table";
                dt.WriteXml("Customer.xml");
            }
            catch (Exception e)
            {
                Listbox1.Items.Add("ERROR XML Customer >" + e.Message);
            }
            Application.DoEvents();

            string msg = "";
            int i = 0;
            int j = 0;
            int k = limit;
            int l = 0;
            using (DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient())
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
                            Listbox1.Items[Listbox1.Items.Count-1]=("Process " + j + " of " + dt.Rows.Count);
                        }
                        else
                        {
                            Listbox1.Items[Listbox1.Items.Count - 1] = (msg);
                        }
                        Application.DoEvents();
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
            return l;
        }
        private int Updatecustomer_GoDaddy()
        {
            int limit = 100;
            DataTable rs = new DataTable();
            DataTable dt = new DataTable();
            Listbox1.Items.Add("Begin Update Customer");
            try
            {
                dt = ClsData.GetCustomer();
                rs = dt.Clone();
                rs.TableName = "Table";
                dt.WriteXml("Customer.xml");
            }
            catch (Exception e)
            {
                Listbox1.Items.Add("ERROR XML Customer >" + e.Message);
            }
            Application.DoEvents();

            string msg = "";
            int i = 0;
            int j = 0;
            int k = limit;
            int l = 0;
            using (DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient())
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
                            Listbox1.Items[Listbox1.Items.Count - 1] = ("Process " + j + " of " + dt.Rows.Count);
                        }
                        else
                        {
                            Listbox1.Items[Listbox1.Items.Count - 1] = (msg);
                        }
                        Application.DoEvents();
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
            return l;
        }
        private void UpdateAllDataOnWeb_Azure()
        {
            string msg = "Error : Cannot Process";
            //update current month
            using (DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient())
            {
                string fname = "Delivery2*";
                try { msg = svc.UpdateData(fname) + "\n"; } catch (Exception ex) { msg = "Error:" + ex.Message; }
                Application.DoEvents();
                Listbox1.Items.Add(msg);
                /*remove for process detail
                fname = "DeliveryDetails2*";
                try { msg = svc.UpdateData(fname) + "\n"; } catch (Exception ex) { msg = "Error:" + ex.Message; }
                Application.DoEvents();
                listBox1.Items.Add(msg);
                */
            }
            Listbox1.SetSelected(Listbox1.Items.Count - 1,true);
        }
        private void UpdateAllDataOnWeb_GoDaddy()
        {
            string msg = "Error : Cannot Process";
            //update current month
            using (DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient())
            {
                string fname = "Delivery2*";
                try { msg = svc.UpdateData(fname) + "\n"; } catch (Exception ex) { msg = "Error:" + ex.Message; }
                Application.DoEvents();
                Listbox1.Items.Add(msg);
                /*remove for process detail
                fname = "DeliveryDetails2*";
                try { msg = svc.UpdateData(fname) + "\n"; } catch (Exception ex) { msg = "Error:" + ex.Message; }
                Application.DoEvents();
                listBox1.Items.Add(msg);
                */
            }
            Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                DownloadSalesEntryData_Azure();
                MessageBox.Show("Completed! (Azure)");
            }
            else
            {
                DownloadSalesEntryData_GoDaddy();
                MessageBox.Show("Completed! (GoDaddy)");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                DownloadDeliveryData_Azure();
                MessageBox.Show("Completed! (Azure)");
            }
            else
            {
                DownloadDeliveryData_GoDaddy();
                MessageBox.Show("Completed! (GoDaddy)");
            }            
        }
        private void UploadDeliveryData_Azure()
        {
            Updatecustomer_Azure();
            Listbox1.Items.Add("UPDATED Customer(s)");
            CreateDeliveryData_Azure();
            UpdateAllDataOnWeb_Azure();
            ClearDataOnWeb_Azure();
        }
        private void UploadDeliveryData_GoDaddy()
        {
            Updatecustomer_GoDaddy();
            Listbox1.Items.Add("UPDATED Customer(s)");
            CreateDeliveryData_GoDaddy();
            UpdateAllDataOnWeb_GoDaddy();
            ClearDataOnWeb_GoDaddy();
        }

        private void ClearDataOnWeb_Azure()
        {
            using (DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient())
            {
                try
                {
                    string msg = svc.ClearDataJSON("Delivery");
                    Listbox1.Items.Add(msg);                    
                }
                catch (Exception ex)
                {
                    Listbox1.Items.Add("ERROR:" + ex.Message);
                }
                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
            }
        }
        private void ClearDataOnWeb_GoDaddy()
        {
            using (DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient())
            {
                try
                {
                    string msg = svc.ClearDataJSON("Delivery");
                    Listbox1.Items.Add(msg);
                }
                catch (Exception ex)
                {
                    Listbox1.Items.Add("ERROR:" + ex.Message);
                }
                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
            }
        }
        private void CreateDeliveryData_Azure()
        {
            DateTime currentMonth = new DateTime(CFunction.CInt32(DateTime.Today.Year.ToString()), CFunction.CInt32(DateTime.Today.Month.ToString()), 1);
            DateTime previousMonth = currentMonth.AddDays(-1);
            DateTime Last2Month = currentMonth.AddMonths(-2);
            string yymmCurrentMonth = currentMonth.ToString("yyyyMM");
            string yymmLastMonth = previousMonth.ToString("yyyyMM");
            string yymmLast2Month = Last2Month.ToString("yyyyMM");
            //check current month
            ProcessData_Azure(yymmCurrentMonth.Substring(0, 4), yymmCurrentMonth.Substring(4, 2));
            //check previous month
            ProcessData_Azure(yymmLastMonth.Substring(0, 4), yymmLastMonth.Substring(4, 2));
            //check last 2 month
            ProcessData_Azure(yymmLast2Month.Substring(0, 4), yymmLast2Month.Substring(4, 2));
            //refresh files
        }
        private void CreateDeliveryData_GoDaddy()
        {
            DateTime currentMonth = new DateTime(CFunction.CInt32(DateTime.Today.Year.ToString()), CFunction.CInt32(DateTime.Today.Month.ToString()), 1);
            DateTime previousMonth = currentMonth.AddDays(-1);
            DateTime Last2Month = currentMonth.AddMonths(-2);
            string yymmCurrentMonth = currentMonth.ToString("yyyyMM");
            string yymmLastMonth = previousMonth.ToString("yyyyMM");
            string yymmLast2Month = Last2Month.ToString("yyyyMM");
            //check current month
            ProcessData_GoDaddy(yymmCurrentMonth.Substring(0, 4), yymmCurrentMonth.Substring(4, 2));
            //check previous month
            ProcessData_GoDaddy(yymmLastMonth.Substring(0, 4), yymmLastMonth.Substring(4, 2));
            //check last 2 month
            ProcessData_GoDaddy(yymmLast2Month.Substring(0, 4), yymmLast2Month.Substring(4, 2));
            //refresh files
        }

        private DataTable GetDeliveryData(string fname,bool isAzure)
        {
            DataTable dt = new DataTable();
            try
            {
                if(isAzure==true)
                {
                    using (DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient())
                    {
                        string xmlData = svc.GetDelivery(fname);
                        dt = new ClsXML().Datatable(xmlData);
                        svc.Close();
                    }
                }
                else
                {
                    using (DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient())
                    {
                        string xmlData = svc.GetDelivery(fname);
                        dt = new ClsXML().Datatable(xmlData);
                        svc.Close();
                    }
                }
            }
            catch
            {

            }
            return dt;
        }
        private DataTable GetWebData(string fname,bool isAzure)
        {
            DataTable dt = new DataTable();
            try
            {
                if(isAzure==true)
                {
                    using (DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient())
                    {
                        string xmlData = svc.GetDataXML(fname, true);
                        dt = new ClsXML().Datatable(xmlData);
                        svc.Close();
                    }
                }
                else
                {
                    using (DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient())
                    {
                        string xmlData = svc.GetDataXML(fname, true);
                        dt = new ClsXML().Datatable(xmlData);
                        svc.Close();
                    }
                }
            }
            catch
            {

            }
            return dt;
        }
        private string DeleteRowsOnWeb_Azure(string fname, string wherestr)
        {
            string msg = "";
            using (DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient())
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
        private string DeleteRowsOnWeb_GoDaddy(string fname, string wherestr)
        {
            string msg = "";
            using (DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient())
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
        private void PostData_Azure(string str)
        {
            //post difference data to web
            using (DeliveryAzure.DataExchangeClient svc = new DeliveryAzure.DataExchangeClient())
            {
                    try
                    {
                        string FullName = textBox1.Text + @"\\" + textBox3.Text +@"\\" + str;
                        StreamReader rd = new StreamReader(FullName, System.Text.UTF8Encoding.UTF8);
                        String JsonStr = rd.ReadToEnd();
                        rd.Close();
                        if (JsonStr != "")
                        {
                            string msg = svc.WriteDataUpdate(str.Replace(".json", ""), JsonStr);
                            if (msg.Substring(0, 1) == "C")
                            {
                                File.Delete(FullName);
                            }
                            Listbox1.Items.Add(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Listbox1.Items.Add(ex.Message);
                    }
                    Application.DoEvents();
            }
        }
        private void PostData_GoDaddy(string str)
        {
            //post difference data to web
            using (DeliveryGoDaddy.DataExchangeClient svc = new DeliveryGoDaddy.DataExchangeClient())
            {
                try
                {
                    string FullName = textBox1.Text + @"\\" + textBox3.Text + @"\\" + str;
                    StreamReader rd = new StreamReader(FullName, System.Text.UTF8Encoding.UTF8);
                    String JsonStr = rd.ReadToEnd();
                    rd.Close();
                    if (JsonStr != "")
                    {
                        string msg = svc.WriteDataUpdate(str.Replace(".json", ""), JsonStr);
                        if (msg.Substring(0, 1) == "C")
                        {
                            File.Delete(FullName);
                        }
                        Listbox1.Items.Add(msg);
                    }
                }
                catch (Exception ex)
                {
                    Listbox1.Items.Add(ex.Message);
                }
                Application.DoEvents();
            }
        }
        private void ProcessData_Azure(string yy, string mm)
        {
            DataTable rsHeader = new DataTable();
            string wherec = "AND (Year(a.DocDate)=" + CFunction.CInt(yy) + " and Month(a.DocDate)=" + CFunction.CInt(mm) + ")";
            DataTable dtHeader = ClsData.GetDeliveryHeader("Delivery" + yy + mm, wherec);
            Listbox1.Items.Add(dtHeader.TableName + " Created (" + dtHeader.Rows.Count + ")");
            Application.DoEvents();
/*
            DataTable rsDetail = new DataTable();
            DataTable dtDetail = ClsData.GetDeliveryDetail("DeliveryDetails" + yy+mm, wherec);
            if (saveXML == true)
            {
                dtDetail.WriteXml(txtWebSrc.Text + @"\\DeliveryDetails" + yy + mm + ".xml");
            }
            ListAdd(dtDetail.TableName + " Created (" + dtDetail.Rows.Count + ")");
            Application.DoEvents();
*/

            //compare data between web and databases
            //start get web data
            using (ClsXML xml = new ClsXML())
            {
                rsHeader = new DataTable();
                try
                {
                    rsHeader = this.GetDeliveryData(dtHeader.TableName,true);
                    Listbox1.Items.Add(dtHeader.TableName + " Get! (" + rsHeader.Rows.Count + ")");
                }
                catch (Exception e)
                {
                    Listbox1.Items.Add("ERROR (" + dtHeader.TableName + ") " + e.Message);
                }
                Application.DoEvents();
                /* uncomment for process details
                rsDetail = new DataTable();
                try
                {
                    rsDetail = this.GetDeliveryData(dtDetail.TableName);
                    listBox1.Items.Add(dtDetail.TableName + " Get! (" + rsDetail.Rows.Count + ")");
                }
                catch (Exception e)
                {
                    ListAdd("ERROR (" + dtDetail.TableName + ") " + e.Message);
                }
                Application.DoEvents();
                */

                //begin check table
                using (ClsConnectSql db = new ClsConnectSql())
                {
                    int c = 0;
                    int i = 0;
                    int j = 0;
                    int limit = 50;
                    //check header first
                    try
                    {
                        rsHeader= rsHeader.Columns.Count == 1 ? dtHeader.Clone() : rsHeader;
                        DataTable tbHeader = db.CompareTable(dtHeader, rsHeader, true, false, "Mark8");
                        Listbox1.Items.Add("Diff Header=" + tbHeader.Rows.Count);
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
                                    StreamWriter wrHeader = new StreamWriter(textBox1.Text + @"\\" + textBox3.Text + @"\\" +  dtHeader.TableName + "_" + c + ".json");
                                    wrHeader.Write(JsonHeader);
                                    wrHeader.Close();
                                    Listbox1.Items.Add(dtHeader.TableName + "_" + c + ".json Created!");
                                    tmp.Rows.Clear();
                                        
                                    Application.DoEvents();
                                    PostData_Azure(dtHeader.TableName + "_" + c + ".json");
                                    Listbox1.SetSelected(Listbox1.Items.Count - 1,true);
                                    i = 0;
                                }
                                else
                                {
                                    tmp.ImportRow(dr);
                                }
                                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                                Application.DoEvents();
                            }
                        }
                        //check deleted data header
                        tbHeader = db.CompareTable(rsHeader, dtHeader, true, true);
                        if (tbHeader.Rows.Count > 0)
                        {
                            int l = 20;
                            int r = 0;
                            string cliteria = "";
                            string colname = rsHeader.Columns[0].ColumnName;
                            foreach (DataRow row in tbHeader.Rows)
                            {
                                r++;
                                if (r <= l)
                                {
                                    if (cliteria != "") cliteria += " OR ";
                                    cliteria += "[" + colname + "] ='" + row[colname].ToString() + "' ";
                                }
                                if (c == i || c == tbHeader.Rows.Count)
                                {
                                    string msg = this.DeleteRowsOnWeb_Azure(dtHeader.TableName, cliteria);
                                    Listbox1.Items.Add("DEL " + dtHeader.TableName + " > " + msg);
                                    cliteria = "";
                                    r = 0;
                                }
                            }
                        }
                        else
                        {
                            Listbox1.Items.Add(dtHeader.TableName + " No data deleted!");
                        }
                        Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                        Application.DoEvents();
                    }
                    catch (Exception e)
                    {
                        Listbox1.Items.Add("ERROR COMPARE(" + dtHeader.TableName + ") " + e.Message);
                    }
                    Listbox1.SetSelected(Listbox1.Items.Count - 1,true);
                    Application.DoEvents();
                    //check details
                    /*uncomment to process details
                    try
                    {
                        rsDetail=rsDetail.Columns.Count==1 ? dtDetail.Clone() : rsDetail;
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
                    db.CloseConnection();
                    */
                }
            }
        }
        private void ProcessData_GoDaddy(string yy, string mm)
        {
            DataTable rsHeader = new DataTable();
            string wherec = "AND (Year(a.DocDate)=" + CFunction.CInt(yy) + " and Month(a.DocDate)=" + CFunction.CInt(mm) + ")";
            DataTable dtHeader = ClsData.GetDeliveryHeader("Delivery" + yy + mm, wherec);
            Listbox1.Items.Add(dtHeader.TableName + " Created (" + dtHeader.Rows.Count + ")");
            Application.DoEvents();

            DataTable rsDetail = new DataTable();
            DataTable dtDetail = ClsData.GetDeliveryDetail("DeliveryDetails" + yy+mm, wherec);
            Listbox1.Items.Add(dtDetail.TableName + " Created (" + dtDetail.Rows.Count + ")");
            Application.DoEvents();

            //compare data between web and databases
            //start get web data
            using (ClsXML xml = new ClsXML())
            {
                rsHeader = new DataTable();
                try
                {
                    rsHeader = this.GetDeliveryData(dtHeader.TableName, true);
                    Listbox1.Items.Add(dtHeader.TableName + " Get! (" + rsHeader.Rows.Count + ")");
                }
                catch (Exception e)
                {
                    Listbox1.Items.Add("ERROR (" + dtHeader.TableName + ") " + e.Message);
                }
                Application.DoEvents();
/*
                rsDetail = new DataTable();
                try
                {
                    rsDetail = this.GetDeliveryData(dtDetail.TableName,true);
                    Listbox1.Items.Add(dtDetail.TableName + " Get! (" + rsDetail.Rows.Count + ")");
                }
                catch (Exception e)
                {
                    Listbox1.Items.Add("ERROR (" + dtDetail.TableName + ") " + e.Message);
                }
                Application.DoEvents();
*/
                //begin check table
                using (ClsConnectSql db = new ClsConnectSql())
                {
                    int c = 0;
                    int i = 0;
                    int j = 0;
                    int limit = 50;
                    //check header first
                    try
                    {
                        rsHeader = rsHeader.Columns.Count == 1 ? dtHeader.Clone() : rsHeader;
                        DataTable tbHeader = db.CompareTable(dtHeader, rsHeader, true, false, "Mark8");
                        Listbox1.Items.Add("Diff Header=" + tbHeader.Rows.Count);
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
                                    StreamWriter wrHeader = new StreamWriter(textBox1.Text + @"\\" + textBox3.Text + @"\\" + dtHeader.TableName + "_" + c + ".json");
                                    wrHeader.Write(JsonHeader);
                                    wrHeader.Close();
                                    Listbox1.Items.Add(dtHeader.TableName + "_" + c + ".json Created!");
                                    tmp.Rows.Clear();

                                    Application.DoEvents();
                                    PostData_Azure(dtHeader.TableName + "_" + c + ".json");
                                    Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                                    i = 0;
                                }
                                else
                                {
                                    tmp.ImportRow(dr);
                                }
                                Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                                Application.DoEvents();
                            }
                        }
                        //check deleted data header
                        tbHeader = db.CompareTable(rsHeader, dtHeader, true, true);
                        if (tbHeader.Rows.Count > 0)
                        {
                            int l = 20;
                            int r = 0;
                            string cliteria = "";
                            string colname = rsHeader.Columns[0].ColumnName;
                            foreach (DataRow row in tbHeader.Rows)
                            {
                                r++;
                                if (r <= l)
                                {
                                    if (cliteria != "") cliteria += " OR ";
                                    cliteria += "[" + colname + "] ='" + row[colname].ToString() + "' ";
                                }
                                if (c == i || c == tbHeader.Rows.Count)
                                {
                                    string msg = this.DeleteRowsOnWeb_Azure(dtHeader.TableName, cliteria);
                                    Listbox1.Items.Add("DEL " + dtHeader.TableName + " > " + msg);
                                    cliteria = "";
                                    r = 0;
                                }
                            }
                        }
                        else
                        {
                            Listbox1.Items.Add(dtHeader.TableName + " No data deleted!");
                        }
                        Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                        Application.DoEvents();
                    }
                    catch (Exception e)
                    {
                        Listbox1.Items.Add("ERROR COMPARE(" + dtHeader.TableName + ") " + e.Message);
                    }
                    Listbox1.SetSelected(Listbox1.Items.Count - 1, true);
                    Application.DoEvents();
                    //check details
                    /*
                                        try
                                        {
                                            rsDetail=rsDetail.Columns.Count==1 ? dtDetail.Clone() : rsDetail;
                                            DataTable tbDetail = db.CompareTable(dtDetail, rsDetail, true);
                                            Listbox1.Items.Add("Diff Detail=" + tbDetail.Rows.Count);
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
                                                        StreamWriter wrDetail = new StreamWriter(textBox1.Text + @"\\" + dtDetail.TableName + "_" + c + ".json");
                                                        wrDetail.Write(JsonDetail);
                                                        wrDetail.Close();
                                                        Listbox1.Items.Add(dtDetail.TableName + "_" + c + ".json Created!");
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
                                                        string msg=this.DeleteRowsOnWeb_GoDaddy(dtDetail.TableName, cliteria);
                                                        Listbox1.Items.Add("DEL " + dtDetail.TableName + " > " +  msg);
                                                        cliteria = "";
                                                        r = 0;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Listbox1.Items.Add(dtDetail.TableName + " No data deleted!");
                                            }

                                        }
                                        catch (Exception e)
                                        {
                                            Listbox1.Items.Add("ERROR COMPARE(" + dtDetail.TableName + ") " + e.Message);
                                        }
                                                            Application.DoEvents();
                    */
                    db.CloseConnection();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                UploadDeliveryData_Azure();
            }
            else
            {
                UploadDeliveryData_GoDaddy();
            }
            MessageBox.Show("Complete!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if ((DateTime.Now.Minute == 0 || DateTime.Now.Minute == 30) && DateTime.Now.Second == 0)
            {
                if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 20)
                {
                    Listbox1.Items.Add("Start Schedule at " + DateTime.Now.ToString("yyyy-MM-dd"));
                    timer1.Enabled = false;
                    if(radioButton1.Checked==true)
                    {
                        UploadDeliveryData_Azure();
                    }
                    else
                    {
                        UploadDeliveryData_GoDaddy();
                    }
                    if ((DateTime.Now.Hour == 9))
                    {
                        SendDailyReport();
                    }
                    timer1.Enabled = true;
                    Listbox1.Items.Add("Finish Schedule at " + DateTime.Now.ToString("yyyy-MM-dd"));
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendDailyReport();

        }
        private void SendDailyReport()
        {
            Listbox1.Items.Add("START Send Emal: " + DateTime.Now);
            Application.DoEvents();
            try
            {
                if (radioButton1.Checked)
                {
                    SendEmail_Azure(DateTime.UtcNow.AddHours(7).AddDays(-1).ToString("yyyy-MM-dd"));
                }
                else
                {
                    SendEmail_GoDaddy(DateTime.UtcNow.AddHours(7).AddDays(-1).ToString("yyyy-MM-dd"));
                }
            }
            catch (Exception ex)
            {
                Listbox1.Items.Add("ERROR : " + ex.Message);
            }
            Listbox1.Items.Add("FINISHED Send Emal: " + DateTime.Now);
            Application.DoEvents();
        }
        private string SendMail(string ondate,string message,DataTable maillist)
        {
            string msg = "";
            try
            {
                CEMail.NewEmail();
                DataTable dt = maillist;
                foreach (DataRow dr in dt.Rows)
                {
                    string mailto = dr[0].ToString();
                    //string mailto = "littlepuppet123@gmail.com";
                    CEMail.NewEmail();
                    CEMail.MailHost = "smtpout.secureserver.net";
                    CEMail.MailPort = 25;
                    CEMail.MailFrom = "puttipong@summitsf.co.th";
                    CEMail.isSSL = false;
                    CEMail.MailTo.Add(mailto);
                    CEMail.MailPassword = "04071980";
                    CEMail.MailSubject = "[AutoMail] รายงานประจำวันที่ " + ondate;
                    CEMail.MailBody = message;
                    CEMail.isBodyHTML = true;
                    CEMail.SendEmail();
                    msg += "Send to " + mailto + " Complete; ";                    
                }
            }
            catch (Exception e)
            {
                msg= "ERROR : " + e.Message;
            }
            return msg;
        }
        private void SendEmail_GoDaddy(string onDate)
        {
            using (SalesEntryGoDaddy.DataExchangeClient svc=new SalesEntryGoDaddy.DataExchangeClient())
            {
                string msg1 = svc.GetDailyReport(0, "Daily", onDate);
                Listbox1.Items.Add(msg1);
                Application.DoEvents();
                string msg2 = svc.GetDailyReport(1, "Daily", onDate);
                Listbox1.Items.Add(msg2);
                Application.DoEvents();

                ClsXML xml = new ClsXML();
                string xmlstr = svc.GetDataXML("maillist");
                DataTable dt=xml.Datatable(xmlstr);
                string msg3=SendMail(onDate, msg1 + Environment.NewLine + msg2, dt);
                Listbox1.Items.Add(msg3);
                Application.DoEvents();
            }
        }
        private void SendEmail_Azure(string onDate)
        {
            using (SalesEntryAzure.DataExchangeClient svc = new SalesEntryAzure.DataExchangeClient())
            {
                string msg1 = svc.GetDailyReport(0, "Daily", onDate);
                Listbox1.Items.Add(msg1);
                Application.DoEvents();
                string msg2 = svc.GetDailyReport(1, "Daily", onDate);
                Listbox1.Items.Add(msg2);
                Application.DoEvents();

                ClsXML xml = new ClsXML();
                string xmlstr = svc.GetDataXML("maillist");
                DataTable dt = xml.Datatable(xmlstr);

                string body = "เรียนผู้ที่เกี่ยวข้อง<br/>";
                body += "<br/>" + msg1 + "<br/>" + msg2 + "<br/><br/>ขอแสดงความนับถือ";

                string msg3=SendMail(onDate, body, dt);
                Listbox1.Items.Add(msg3);
                Application.DoEvents();

            }
        }
    }
}