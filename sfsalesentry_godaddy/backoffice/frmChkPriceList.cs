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
    public partial class frmChkPriceList : Form
    {
        public frmChkPriceList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                LoadPriceList();
            }
            if (radioButton2.Checked == true)
            {
                LoadNewGoods();
            }
        }

        private void LoadPriceList()
        {
            ClsConnectSql db = new ClsConnectSql();
            dataGridView1.DataSource = db.RecordSet("select * from vPriceNotMatch ");
            db.CloseConnection();
        }
        private void UpdatePriceList()
        {
            ClsConnectSql db = new ClsConnectSql();
            DataTable dt= db.RecordSet("select * from vPriceNotMatch ");
            foreach(DataRow dr in dt.Rows)
            {
                db.Execute("Update xGoods set StdSellPrice=" + dr["Price"].ToString() + ",ProdStdCost=" + dr["Price"].ToString() + " where OID='" + dr["OID"].ToString() + "'");
            }
            db.CloseConnection();
        }
        private void LoadNewGoods()
        {
            ClsConnectSql db = new ClsConnectSql();
            dataGridView1.DataSource = db.RecordSet("select * from vCheckNewGoods ");
            db.CloseConnection();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true) LoadPriceList();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) LoadNewGoods();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                UpdatePriceList();
                LoadPriceList();
            }
        }
    }
}
