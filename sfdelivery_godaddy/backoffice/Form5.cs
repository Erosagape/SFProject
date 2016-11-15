using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SfDeliverTracking
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void uploadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i=Updatecustomer(100);
            MessageBox.Show(i.ToString() + " Rows Processed!");
        }
        private int Updatecustomer(int limit)
        {
            DataTable dt = ClsData.GetCustomer();
            DataTable rs = dt.Clone();
            rs.TableName = "Table";
            dt.WriteXml("Customer.xml");
            IWebService.DataExchangeClient svc = new IWebService.DataExchangeClient();
            ClsXML xml = new ClsXML();
            string msg = "";
            int i = 0;
            int j = 0;
            int k = limit;
            int l = 0;
            rs.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                j++; //row count
                if (j==dt.Rows.Count)
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
                        this.Text = "Process " + j + " of " + dt.Rows.Count;
                    }
                    //reset value and begin to read next set of data
                    i = 1;
                    if(j<dt.Rows.Count)
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
            return i;
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ClsData.GetCustomer();
        }
    }
}
