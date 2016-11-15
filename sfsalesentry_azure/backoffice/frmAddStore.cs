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
    public partial class frmAddStore : Form
    {
        private ClsXML xml = new ClsXML();
        public frmAddStore()
        {
            InitializeComponent();
        }

        private void loadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadXML();
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
            if (DeleteData() == true)
            {
                MessageBox.Show("Delete Complete!");
            }
        }

        private void txtShopName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBranch_TextChanged(object sender, EventArgs e)
        {
            ChangeFullName();
        }
        private void ChangeFullName()
        {
            txtCustName.Text = txtShopName.Text + " " + txtBranch.Text;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtOID.Text = dataGridView1.Rows[e.RowIndex].Cells["OID"].Value.ToString();
                txtCustCode.Text = dataGridView1.Rows[e.RowIndex].Cells["CustCode"].Value.ToString();
                txtCustName.Text = dataGridView1.Rows[e.RowIndex].Cells["CustName"].Value.ToString();
                txtShopName.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["GroupID"].Value.ToString();
                txtShopName.Text = dataGridView1.Rows[e.RowIndex].Cells["ShopName"].Value.ToString();
                txtBranch.Text = dataGridView1.Rows[e.RowIndex].Cells["Branch"].Value.ToString();                
            }
        }
        private bool LoadXML()
        {
            bool success = false;
            dataGridView1.DataSource =xml.GetCustomerData().Tables[0];

            return success;
        }

        private void frmAddStore_Load(object sender, EventArgs e)
        {
            LoadXML();
            LoadGroup();
        }
        private void LoadGroup()
        {
            txtShopName.DataSource = xml.GetCustomerGroupData().Tables[0];
            txtShopName.DisplayMember = "CustGroupNameTH";
            txtShopName.ValueMember = "OID";
        }
        private bool SaveData()
        {
            try
            {
                ClsConnectSql db = new ClsConnectSql();
                DataTable dt = new DataTable();
                SqlDataAdapter da = db.DataAdapter("select * from xCustomer where oid='" + txtOID.Text + "'", true);
                da.Fill(dt);
                DataRow row = dt.NewRow();
                if (dt.Rows.Count > 0) row = dt.Rows[0];
                row["OID"] = txtOID.Text;
                row["Code"] = txtCustCode.Text;
                row["Name"] = txtCustName.Text;
                row["Name2"] = txtShopName.Text;
                row["remark"] = txtBranch.Text;
                row["GroupID"] = txtShopName.SelectedValue.ToString();
                if (row.RowState == DataRowState.Detached)
                {
                    dt.Rows.Add(row);
                }
                int i = da.Update(dt);
                db.CloseConnection();
                if (i > 0)
                {
                    return SaveFormToXML();
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private bool SaveFormToXML()
        {
            try
            {
                DataTable dt = xml.GetCustomerXML();
                DataRow[] dr = dt.Select(@"OID='" + txtOID.Text + @"'");
                DataRow row = dt.NewRow();
                if (dr.Length > 0)
                {
                    foreach (DataRow r in dr)
                    {
                        row = r;
                    }
                }
                row["OID"] = txtOID.Text;
                row["CustCode"] = txtCustCode.Text;
                row["CustName"] = txtCustName.Text;
                row["ShopName"] = txtShopName.Text;
                row["branch"] = txtBranch.Text;
                row["GroupID"] = txtShopName.SelectedValue.ToString();
                if (row.RowState == DataRowState.Detached)
                {
                    dt.Rows.Add(row);
                }
                dt.WriteXml("Data\\" + "Customer.xml");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool DeleteData()
        {
            try
            {
                ClsConnectSql db = new ClsConnectSql();
                bool chk = db.Execute("delete from xCustomer where oid='" + txtOID.Text + "'");
                if (chk == true)
                {
                    chk = DeleteInXML();
                }
                db.CloseConnection();
                return chk;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool DeleteInXML()
        {
            try
            {
                DataTable dt = xml.GetCustomerXML();
                DataRow[] dr = dt.Select(@"OID='" + txtOID.Text + @"'");
                if (dr.Length > 0)
                {
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                    }
                }
                dt.WriteXml("Data\\" + "Customer.xml");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtShopName_TextUpdate(object sender, EventArgs e)
        {

        }

        private void txtShopName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFullName();
        }
    }
}
