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
    public partial class frmGetXML : Form
    {
        private ClsXML xml = new ClsXML(Program.StartupPath+@"\\WebDB.xml");
        public frmGetXML()
        {
            InitializeComponent();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConnect frm = new frmConnect();
            frm.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmGetXML_Load(object sender, EventArgs e)
        {
            SetState();
        }
        public void SetState()
        {
            ClsConnectSql db = new ClsConnectSql();
            bool enable = db.isReady();
            addGoodsToolStripMenuItem.Enabled = enable;
            addStoreToolStripMenuItem.Enabled = enable;
            addModelMenuItem3.Enabled = enable;
            addUserToolStripMenuItem.Enabled = enable;
            regenerateXMLToolStripMenuItem.Enabled = enable;
            updateDataToolStripMenuItem.Enabled = enable;
            updateDataToServerToolStripMenuItem.Enabled = enable;
            queryDataToolStripMenuItem.Enabled = enable;
            loadSalesDataToolStripMenuItem.Enabled = enable;
            comboBox1.Enabled = enable;
            db.CloseConnection();
            if(enable==true)
            {
                comboBox1.DataSource = xml.Datatable();
                comboBox1.DisplayMember = "FileDesc";
                comboBox1.ValueMember = "FileName";
            }
            //comboBox1.SelectedIndex = -1;
        }

        private void frmGetXML_Activated(object sender, EventArgs e)
        {
            SetState();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUser frm = new frmAddUser();
            frm.ShowDialog();
        }

        private void addStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddStore frm = new frmAddStore();
            frm.ShowDialog();
        }

        private void addGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddGoods frm = new frmAddGoods();
            frm.ShowDialog();
        }

        private void regenerateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void customerxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetCustomerData().WriteXml("Data\\" + "Customer.xml");
            MessageBox.Show("Finished");
        }

        private void goodsxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetGoodsData().WriteXml("Data\\" + "Goods.xml");
            MessageBox.Show("Finished");
        }

        private void shoeTypexmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetShoeTypeData().WriteXml("Data\\" + "ShoeType.xml");
            MessageBox.Show("Finished");
        }

        private void shoeCategoryxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetShoeCategoryData().WriteXml("Data\\" + "ShoeCategory.xml");
            MessageBox.Show("Finished");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmAddModel frm = new frmAddModel();
            frm.ShowDialog();
        }

        private void updatePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void colorxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetColorData().WriteXml("Data\\" + "Color.xml");
            MessageBox.Show("Finished");
        }

        private void listuserxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetUserData().WriteXml("Data\\" + "listuser.xml");
            MessageBox.Show("Finished");
        }

        private void shoeGroupxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetShoeGroupData().WriteXml("Data\\" + "ShoeGroup.xml");
            MessageBox.Show("Finished");
        }

        private void salesTypexmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetSalesTypeData().WriteXml("Data\\" + "SalesType.xml");
            MessageBox.Show("Finished");
        }

        private void updateDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateToWeb frm = new frmUpdateToWeb();
            frm.ShowDialog();
        }

        private void customerGroupcmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetCustomerGroupData().WriteXml("Data\\" + "CustomerGroup.xml");
            MessageBox.Show("Finished!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedValue !=null)
            {
                string tbname = comboBox1.SelectedValue.ToString();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = xml.GetDataForXML(tbname);

            }
        }

        private void queryDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWebQuery frm = new frmWebQuery();
            frm.ShowDialog();
        }

        private void loadSalesDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateSales frm = new frmUpdateSales();
            frm.ShowDialog();
        }

        private void updateDataToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompareDB frm = new frmCompareDB();
            frm.ShowDialog();
        }

        private void shoeModelxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetShoeModelData().WriteXml("Data\\" + "ShoeModel.xml");
            MessageBox.Show("Finished!");
        }

        private void shoeMoldxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetShoeMoldData().WriteXml("Data\\" + "ShoeMold.xml");
            MessageBox.Show("Finished!");
        }

        private void shoeSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.GetShoeSizeData().WriteXml("Data\\" + "ShoeSize.xml");
            MessageBox.Show("Finished!");
        }

        private void updatePriceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChkPriceList frm = new frmChkPriceList();
            frm.ShowDialog();
        }

        private void utilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUtil frm = new frmUtil();
            frm.ShowDialog();
        }

        private void sendDailyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSentMail frm = new frmSentMail();
            frm.ShowDialog();
        }
    }
}
