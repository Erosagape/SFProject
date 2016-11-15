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
    public partial class FrmAutoGenGoods : Form
    {
        public ClsConnectSql db;
        public DataTable dtColor;
        public struct GoodsData
        {
            public string GoodsCode;
            public string GoodsName;
            public string GoodsKind;
            public string SMid;
            public string Colid;
            public string ColInit;
            public string STid;
            public string SSid;
            public string SizeNo;
            public string SizeMin;
            public string SizeMax;
            public string ProdKindId;
            public string ProdCatId;
            public string ProdGroupId;
            public string stdPriceSell;
            public string stdPriceCost;
        }
        private List<GoodsData> lst = new List<GoodsData>();
        public GoodsData def=new GoodsData();
        public FrmAutoGenGoods()
        {
            InitializeComponent();
        }

        private void FrmAutoGenGoods_Load(object sender, EventArgs e)
        {
            ShowDefault();
        }
        void PopulateListColor()
        {
            DataView dv = dtColor.DefaultView;
            dv.RowFilter = "Colid>1";
            dv.Sort = "ColNameTh";
            DataTable dt = dv.ToTable();
            listColor.Items.Clear();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                listColor.Items.Add(dt.Rows[i]["ColNameTh"].ToString() + " : " + dt.Rows[i]["ColNameInit"].ToString());
            }
        }
        void ShowDefault()
        {
            txtSM.Text = def.GoodsCode;
            txtMinSize.Text = def.SizeNo;
            txtMaxSize.Text = def.SizeMax;
            PopulateListColor();
        }
        private string InsertCommand(GoodsData data)
        {
            string insertcmd = "dbo.sp_add_xgoods ";
            insertcmd += " '" + data.GoodsCode  + "',";
            insertcmd += " '" + data.GoodsName  + "',";
            insertcmd += " '',";
            insertcmd += " '',";
            insertcmd += " '',";
            insertcmd += " 0" + data.ProdKindId  + ",";
            insertcmd += " 0" + data.ProdCatId  + ",";
            insertcmd += " 0" + data.ProdGroupId  + ",";
            insertcmd += " 0" + data.SMid  + ",";
            insertcmd += " 0" + data.Colid  + ",";
            insertcmd += " 0" + data.STid  + ",";
            insertcmd += " 0" + data.SSid  + ",";
            insertcmd += " 0" + data.SizeNo + ",";
            insertcmd += " 0" + data.stdPriceSell  + ",";
            insertcmd += " 0" + data.stdPriceCost  + ",";
            insertcmd += "''";

            return insertcmd;
        }
        private void PrepareList()
        {
            int minSize = System.Convert.ToInt16(txtMinSize.Text);
            int maxSize = System.Convert.ToInt16(txtMaxSize.Text);
            lst = new List<GoodsData>();
            for (int idx=0;idx<listColor.CheckedItems.Count;idx++)
            {
                string colCode = listColor.CheckedItems[idx].ToString().Substring (listColor.CheckedItems[idx].ToString().IndexOf (":")+1);
                DataRow rowColor = db.FindRow(dtColor, "ColNameInit='" + colCode.Trim()+"'");
                for (int i = minSize; i <= maxSize; i++)
                {                    
                    def.ColInit = rowColor["ColNameInit"].ToString ();
                    def.Colid = rowColor["ColId"].ToString();
                    def.GoodsCode = txtSM.Text + def.ColInit  + (i * 10).ToString();
                    def.GoodsName = txtSM.Text + " " +  rowColor["ColNameTh"].ToString().Trim() + " " + i.ToString();
                    def.SizeNo = i.ToString();
                    lst.Add(def);
                }
            }            
        }
        private int ProcessList()
        {
            int rec = 0;
            int i = 0;
            string cmd = "";
            listBox1.Items.Clear();
            try
            {
                PrepareList();
                foreach (GoodsData item in lst)
                {
                    i = i + 1;
                    cmd= InsertCommand(item);
                    if (db.Execute (cmd)==true)
                    {
                        rec = rec + 1;
                        listBox1.Items.Add(i + " Rows Added ->" + item.GoodsCode);
                    }
                    else
                    {
                        listBox1.Items.Add(i + " Rows Failed:" + cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(i + "Records Error ->"+ex.Message);
            }
            return rec;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ProcessList() + " Records Processed!");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.Text;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.Text);
        }
    }
}
