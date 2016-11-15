using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace shopsales_tools
{
    public partial class frmAddModel : Form
    {
        private ClsConnectSql db = new ClsConnectSql();
        public frmAddModel()
        {
            InitializeComponent();
        }

        private bool LoadData()
        {
            dataGridView1.DataSource = db.RecordSet(@"select * from ShoeModel");
            return true;
        }
        private void LoadTypeCombo()
        {
            cboSTId.DataSource = db.LOV_ShoeType();
            cboSTId.ValueMember = "STid";
            cboSTId.DisplayMember = "STName";
        }
        private void LoadMoldCombo()
        {
            cboMoldId.DataSource = db.LOV_Mold();
            cboMoldId.ValueMember = "MoldId";
            cboMoldId.DisplayMember = "MoldName";
        }
        private void LoadSizeCombo()
        {
            cboSSId.DataSource = db.LOV_Size();
            cboSSId.ValueMember = "SSid";
            cboSSId.DisplayMember = "SizeGroup";
        }
        private void LoadCategoryCombo()
        {
            cboProdCatId.DataSource = db.LOV_ProductCategory();
            cboProdCatId.ValueMember = "ProdCatid";
            cboProdCatId.DisplayMember = "ProdCatName";
        }
        private void LoadCombo()
        {
            LoadMoldCombo();
            LoadSizeCombo();
            LoadCategoryCombo();
            LoadTypeCombo();
        }
        private bool SaveData()
        {
            bool success = false;
            try
            {
                string oid = db.FindValue(@"select SMid from ShoeModel where [Name]='" + txtName.Text + "'");
                if (oid == "")
                {
                    string insertcmd = "";
                    insertcmd += @"dbo.sp_insert_shoemodel ";
                    insertcmd += "'" + txtName.Text + "','',";
                    insertcmd += "" + cboMoldId.SelectedValue.ToString() + ",";
                    insertcmd += "" + cboSSId.SelectedValue.ToString() + ",";
                    insertcmd += "" + cboSTId.SelectedValue.ToString() + ",";
                    insertcmd += "" + cboProdCatId.SelectedValue.ToString() + ",";
                    insertcmd += "0" + txtMinSize.Text  + ",";
                    insertcmd += "0" + txtMaxSize.Text + ",'' ";

                    db.Execute(insertcmd);
                }
                else
                {
                    SqlDataAdapter da = db.DataAdapter(@"select * from ShoeModel where SMid='" + oid + "'",true);
                    DataTable dt = db.RecordSet(da);
                    if(dt.Rows.Count >0)
                    {
                        DataRow dr = dt.Rows[0];
                        dr["SMCode"] = txtSMCode.Text;
                        dr["Name"] = txtName.Text;
                        dr["Name2"] = dr["Name2"].ToString ();
                        dr["Moldid"] = cboMoldId.SelectedValue.ToString();
                        dr["SSid"] = cboSSId.SelectedValue.ToString();
                        dr["STid"] = cboSTId.SelectedValue.ToString();
                        dr["ProdCatId"] = cboProdCatId.SelectedValue.ToString();
                        dr["MinSize"] = txtMinSize.Text;
                        dr["MaxSize"] = txtMaxSize.Text;
                        dr["Remark"] = dr["Remark"].ToString();
                        dr["ItemStatus"] = dr["ItemStatus"].ToString();
                        dr["LastUpdate"] = DateTime.Now;
                        da.Update(dt);
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Record is deleted!");
                    }
                }                
                success = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return success;
        }
        private void loadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void addToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData() == true)
            {
                MessageBox.Show("Save complete!");
            }
        }

        private void deleteFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmAddModel_Load(object sender, EventArgs e)
        {
            LoadCombo();
            LoadData();
        }

        private void frmAddModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.CloseConnection();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                txtSMId.Text = dataGridView1.Rows[e.RowIndex].Cells["SMId"].Value.ToString();
                txtSMCode.Text = dataGridView1.Rows[e.RowIndex].Cells["SMCode"].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtMinSize.Text = dataGridView1.Rows[e.RowIndex].Cells["MinSize"].Value.ToString();
                txtMaxSize.Text = dataGridView1.Rows[e.RowIndex].Cells["MaxSize"].Value.ToString();
                cboMoldId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["Moldid"].Value.ToString();
                cboSSId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["SSid"].Value.ToString();
                cboSTId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["STid"].Value.ToString();
                cboProdCatId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["ProdCatId"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void clearScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSMId.Text = "";
            txtSMCode.Text = "";
            txtName.Text = "";
            txtMinSize.Text = "";
            txtMaxSize.Text = "";
            cboMoldId.SelectedValue = -1;
            cboSSId.SelectedValue = -1;
            cboSTId.SelectedValue = -1;
            cboProdCatId.SelectedValue = -1;
        }
    }
}
