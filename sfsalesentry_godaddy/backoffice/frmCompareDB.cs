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
    public partial class frmCompareDB : Form
    {
        ClsConnectSql cnTest = new ClsConnectSql(0);
        ClsConnectSql cnProduction = new ClsConnectSql(1);
        public frmCompareDB()
        {
            InitializeComponent();
        }

        private void frmCompareDB_Load(object sender, EventArgs e)
        {
            LoadTable();
        }
        private void LoadTable()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("ProductCategory");
            listBox1.Items.Add("ProductGroup");
            listBox1.Items.Add("ProductKind");
            listBox1.Items.Add("SaleType");
            listBox1.Items.Add("ShoeColor");
            listBox1.Items.Add("ShoeModel");
            listBox1.Items.Add("ShoeMold");
            listBox1.Items.Add("ShoeSize");
            listBox1.Items.Add("ShoeType");
            listBox1.Items.Add("xCustomer");
            listBox1.Items.Add("xCustomerGroup");
            listBox1.Items.Add("xGoods");
            listBox1.Items.Add("xStaff");
            listBox1.Items.Add("SOHd");
            listBox1.Items.Add("SODt");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = cnProduction.RecordSet("select * from " + listBox1.Text + " order by 1");
            dataGridView2.DataSource = cnTest.RecordSet("select * from " + listBox1.Text + " order by 1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string log = SyncTable();
            MessageBox.Show(log);
        }
        protected string SyncTable()
        {
            string msg = "";
            string tbname = listBox1.Text;
            if (checkBox1.Checked ==true )
            {
                cnProduction.Execute("delete from " + tbname + "");
            }
            DataTable tbsource = cnTest.RecordSet("select * from " + tbname + "");
            int i = 0;
            foreach (DataRow rowsource in tbsource.Rows)
            {
                if (cnProduction.UpdateRow("select * from " + tbname + " where " + tbsource.Columns[0].ColumnName + "='" + rowsource[0].ToString() + "'", rowsource) == true)
                {
                    i++;
                }
                else
                {
                    msg += cnProduction.ErrorMessage() + " \n";
                }
            }
            msg += i.ToString() + " row(s) processed!";
            return msg;
        }
    }
}
