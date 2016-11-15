using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace shopsales_tools
{
    public partial class frmAddGoods : Form
    {
        private DataTable dtColor;
        private DataTable dtModel;
        private DataTable dtSize;
        private DataTable dtType;
        private ClsConnectSql db = new ClsConnectSql();
        public frmAddGoods()
        {
            InitializeComponent();
        }
        private void LoadTypeToCombo()
        {
            dtType = db.LOV_ShoeType();
            cboSTId.DataSource = dtType;
            cboSTId.ValueMember = "STid";
            cboSTId.DisplayMember = "STName";
        }
        private void LoadModelToCombo()
        {
            dtModel = db.LOV_Model();
            cboSMid.DataSource = dtModel;
            cboSMid.ValueMember = "SMid";
            cboSMid.DisplayMember = "SMName";
        }
        private void LoadColorToCombo()
        {
            dtColor = db.LOV_Color();
            cboColId.DataSource = dtColor;
            cboColId.ValueMember = "ColId";
            cboColId.DisplayMember = "ColNameTh";
        }
        private void LoadSizeToCombo()
        {
            dtSize = db.LOV_Size();
            cboSSid.DataSource = dtSize;
            cboSSid.ValueMember = "SSId";
            cboSSid.DisplayMember = "SizeGroup";
        }
        private void LoadKindToCombo()
        {
            cboProductKind.DataSource = db.LOV_ProductKind();
            cboProductKind.ValueMember = "ProdKindId";
            cboProductKind.DisplayMember = "ProdKindName";
        }
        private void LoadCategoryToCombo()
        {
            cboProductCategory.DataSource = db.LOV_ProductCategory();
            cboProductCategory.ValueMember = "ProdCatId";
            cboProductCategory.DisplayMember = "ProdCatName";
        }
        private void LoadGroupToCombo()
        {
            cboProductGroup.DataSource = db.LOV_ProductGroup();
            cboProductGroup.ValueMember = "ProdGroupId";
            cboProductGroup.DisplayMember = "ProdGroupName";
        }
        private void LoadCombo()
        {
            LoadTypeToCombo();
            LoadModelToCombo();
            LoadColorToCombo();
            LoadSizeToCombo();
            LoadKindToCombo();
            LoadCategoryToCombo();
            LoadGroupToCombo();
        }
        private bool LoadData()
        {
            bool success = false;           
            DataTable ds = db.RecordSet(@"select * from xGoodsWithDetail");
            DataTable dt = ds.Clone();
            string cliteria = "";
            if(txtModelSearch.Text !="")
            {
                cliteria += "ModelName like '" + txtModelSearch.Text + "%'";
            }
            if (txtColorSearch.Text != "")
            {
                if (cliteria != "") cliteria += " AND ";
                cliteria += "ColNameTh like '" + txtColorSearch.Text + "%'";
            }
            if (txtSizeSearch.Text != "")
            {
                if (cliteria != "") cliteria += " AND ";
                cliteria += "SizeNo =" + txtSizeSearch.Text + "";
            }
            if (cliteria != "")
            {
                DataRow[] dr = ds.Select(cliteria);
                foreach(DataRow r in dr)
                {
                    dt.ImportRow(r);
                }
            }
            else
            {
                dt = ds;
            }
            dataGridView1.DataSource = dt;

            return success;
        }
        private DataSet GetGoods()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"SELECT OID,GoodsCode,GoodsName,ModelName,ColNameInit,ColNameEng,ColNameTh,ColTypeId,SizeNo,StdSellPrice,ProdStdCost,ProdCatCode,ProdCatName,STCode,STName,STName2,SMCode,SMid,SSid,STid,Colid,ProdGroupId,ProdCatid,ProdKindid FROM xGoodsWithDetail").Fill(ds);
            db.CloseConnection();
            return ds;
        }
        private string InsertCommand()
        {
            string insertcmd = "dbo.sp_add_xgoods ";
            insertcmd += " '" + txtGoodsCode.Text + "',";
            insertcmd += " '" + txtGoodsName.Text + "',";
            insertcmd += " '',";
            insertcmd += " '',";
            insertcmd += " '',";
            insertcmd += " 0" + cboProductKind.SelectedValue.ToString() + ",";
            insertcmd += " 0" + cboProductCategory.SelectedValue.ToString() + ",";
            insertcmd += " 0" + cboProductGroup.SelectedValue.ToString() + ",";
            insertcmd += " 0" + cboSMid.SelectedValue.ToString() + ",";
            insertcmd += " 0" + cboColId.SelectedValue.ToString() + ",";
            insertcmd += " 0" + cboSTId.SelectedValue.ToString() + ",";
            insertcmd += " 0" + cboSSid.SelectedValue.ToString() + ",";
            insertcmd += " 0" + txtSizeNo.Text + ",";
            insertcmd += " 0" + txtStdSellPrice.Text + ",";
            insertcmd += " 0" + txtStdCostPrice.Text + ",";
            insertcmd += "''";

            return insertcmd;
        }
        private bool SaveData()
        {
            bool success = false;
            try
            {
                string oid = db.FindValue(@"select oid from xGoods where GoodsName='" + txtGoodsName.Text + "'");
                if (oid == "")
                {
                    success=db.Execute(InsertCommand ());
                }
                else
                {
                    SqlDataAdapter da = db.DataAdapter(@"select * from xGoods where OID='" + oid + "'",true );
                    DataTable dt = db.RecordSet(da);
                    if(dt.Rows.Count >0)
                    {
                        DataRow dr = dt.Rows[0];
                        dr["SMId"] = cboSMid.SelectedValue.ToString();
                        dr["ColId"] = cboColId.SelectedValue.ToString();
                        dr["STId"] = cboSTId.SelectedValue.ToString();
                        dr["SSId"] = cboSSid.SelectedValue.ToString();
                        dr["ProdGroupId"] = cboProductGroup.SelectedValue.ToString();
                        dr["ProdCatId"] = cboProductCategory.SelectedValue.ToString();
                        dr["ProdKindId"] = cboProductKind.SelectedValue.ToString();
                        dr["GoodsCode"] = txtGoodsCode.Text;
                        dr["GoodsName"] = txtGoodsName.Text;
                        dr["SizeNo"] = txtSizeNo.Text;
                        dr["ProdStdCost"] = txtStdCostPrice.Text;
                        dr["StdSellPrice"] = txtStdSellPrice.Text;
                        da.Update(dt);
                        success = true;
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Record is Deleted!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                success= false;
            }
            return success;
        }
        private void loadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void addToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData()==true)
            {
                MessageBox.Show("Save Complete!");
            }
        }

        private void deleteFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmAddGoods_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void txtModelSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtColorSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSizeSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtOID.Text = dataGridView1.Rows[e.RowIndex].Cells["OID"].Value.ToString();
                cboSMid.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["SMid"].Value.ToString();
                cboColId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["ColId"].Value.ToString();
                cboSTId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["STid"].Value.ToString();
                cboSSid.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["SSid"].Value.ToString();
                cboProductGroup.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["ProdGroupid"].Value.ToString();
                cboProductCategory.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["ProdCatid"].Value.ToString();
                cboProductKind.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["ProdKindid"].Value.ToString();
                txtSizeNo.Text = dataGridView1.Rows[e.RowIndex].Cells["SizeNo"].Value.ToString();
                txtGoodsCode.Text = dataGridView1.Rows[e.RowIndex].Cells["GoodsCode"].Value.ToString();
                txtGoodsName.Text = dataGridView1.Rows[e.RowIndex].Cells["GoodsName"].Value.ToString();
                txtStdSellPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["StdSellPrice"].Value.ToString();
                txtStdCostPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["ProdStdCost"].Value.ToString();
            }
        }

        private void cboModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.CloseConnection();
        }

        private void cboSMid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSMid.SelectedIndex >= 0)
            {
                
            }
        }

        private void cboSMid_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void txtGoodsCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label10_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string colorInit = db.FindRow(dtColor, "Colid=" + cboColId.SelectedValue.ToString())["ColNameInit"].ToString();
                txtGoodsCode.Text = cboSMid.Text + colorInit + (System.Convert.ToInt16(txtSizeNo.Text) * 10).ToString();
                txtGoodsName.Text = cboProductGroup.Text + " " + label6.Text + " " + cboSMid.Text + " " + label7.Text + " " + colorInit  + " " + label18.Text + " " + txtSizeNo.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            txtCmd.Visible = true; 
            txtCmd.Text  = InsertCommand();
        }

        private void txtCmd_DoubleClick(object sender, EventArgs e)
        {
            txtCmd.Visible = false;
        }

        private void autoGenerateGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenGoodsFromSize();
        }
        protected void GenGoodsFromSize()
        {
            FrmAutoGenGoods frm = new FrmAutoGenGoods();
            frm.db = db;
            frm.dtColor = dtColor;
            FrmAutoGenGoods.GoodsData data=new FrmAutoGenGoods.GoodsData();
            data.GoodsCode = cboSMid.Text;
            data.GoodsKind = cboProductGroup.Text;
            data.Colid = cboColId.SelectedValue.ToString();
            data.ColInit = db.FindRow(dtColor, "Colid=" + cboColId.SelectedValue.ToString())["ColNameInit"].ToString();
            data.SSid = cboSSid.SelectedValue.ToString();
            data.STid = cboSTId.SelectedValue.ToString();
            data.SMid = cboSMid.SelectedValue.ToString();
            data.ProdCatId = cboProductCategory.SelectedValue.ToString();
            data.ProdGroupId = cboProductGroup.SelectedValue.ToString();
            data.ProdKindId = cboProductKind.SelectedValue.ToString();
            data.SizeNo = txtSizeNo.Text;
            data.SizeMin = db.FindRow(dtSize, "SSid=" + cboSSid.SelectedValue.ToString())["MinSize"].ToString();
            data.SizeMax = db.FindRow(dtSize, "SSid=" + cboSSid.SelectedValue.ToString())["MaxSize"].ToString();
            data.stdPriceCost = txtStdCostPrice.Text;
            data.stdPriceSell = txtStdSellPrice.Text;
            frm.def = data;
            frm.ShowDialog();
        }
    }
}
