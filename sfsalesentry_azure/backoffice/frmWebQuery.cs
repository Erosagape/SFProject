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
    public partial class frmWebQuery : Form
    {
        public frmWebQuery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dlg.ShowDialog();
            txtFolder.Text = dlg.SelectedPath;
        }

        private void frmWebQuery_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        private void LoadList()
        {
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            string tableList = svc.GetXMLFileList(txtFilter.Text);
            ClsXML xml = new ClsXML();
            if (xml.LoadXMLString(tableList)==true)
            {
                listBox1.DataSource = xml.Datatable();
                listBox1.DisplayMember = "filename";
                listBox1.ValueMember = "filename";
                dataGridView1.DataSource = xml.Datatable();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData(GetFileData());
        }
        private void ShowData(string fname)
        {
            ClsXML xml = new ClsXML();
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            string dataXML = svc.GetDataXML(fname);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = xml.Datatable(dataXML);
        }
        private string GetCliteria()
        {
            return "["+textBox1.Text + "]" + textBox2.Text;
        }
        private void SearchData(string fname,string cliteria)
        {
            ClsXML xml = new ClsXML();
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            string dataXML = svc.QueryDataXML(fname, cliteria);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = xml.Datatable(dataXML);
        }
        private void btnGetData_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedValue!=null)
            {
                SearchData(GetFileData(), GetCliteria());
            }
        }
        private bool SaveXML(string fname)
        {
            bool success = false;
            ClsXML xml = new ClsXML();
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if(xml.LoadXMLString (xml.GetXML(dt))==true)
            {
                string xmlfile = txtFolder.Text + @"\" + fname + ".xml"; 
                xml.SaveXML(xmlfile);
                if(System.IO.File.Exists(xmlfile)==true)
                {
                    success = true;
                }
            }
            return success;
        }
        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            if(SaveXML(GetFileData())==true)
            {
                MessageBox.Show("Save "+listBox1.SelectedValue.ToString()+" completed!");
            }
        }
        string GetFileData()
        {
            return listBox1.SelectedValue.ToString().Replace(".xml", "");
        }
        private string DownloadAllXMLFromWeb()
        {
            string log = "Start "+DateTime.Now + Environment.NewLine;
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            ClsXML xml = new ClsXML();
            DataTable lists = xml.Datatable(svc.GetXMLFileList(txtFilter.Text)).Copy();
            if(lists.Columns.Count>1)
            {
                //foreach (DataRow file in lists.Select("[modifieddate] Like '"+ DateTime.Now.AddHours(7).ToString("yyyy/MM")+"%'"))
                foreach (DataRow file in lists.Rows)
                {
                    string filename = file[0].ToString().Replace(".xml", "");
                    string filedata = svc.GetDataXML(filename);
                    if(xml.LoadXMLString (filedata)==true)
                    {
                        if (xml.SaveXML(txtFolder.Text +@"\"+ filename + ".xml")==true )
                        {
                            log += " Downloaded " + filename + " at " + DateTime.Now + Environment.NewLine;
                        }
                    }
                }
            }
            return log;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            textBox3.Text= DownloadAllXMLFromWeb();
            textBox3.Visible = true;
            MessageBox.Show("Complete!");
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedIndex);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox3.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string datenow = this.Text.Split(' ')[0].ToString();
            string timenow = this.Text.Split(' ')[1].ToString().Substring(0, 5);
            if (timenow=="08:00")
            {
                timer1.Enabled = false;
                textBox3.Visible = true;
                string str = "เรียนผู้เกี่ยวข้อง";
                string msg = "กำลังส่งเมล์ " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine;
                textBox3.Text = msg;
                Application.DoEvents();

                IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
                msg = svc.SendDailyReport(datenow, str) + Environment.NewLine;
                msg += "เสร็จสิ้น " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                textBox3.Text += msg;
                Application.DoEvents();
                timer1.Enabled = true;
            }
        }
    }
}
