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
    public partial class frmAddUser : Form
    {
        private ClsXML xml = new ClsXML();
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void loadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadXML();
        }
        private bool LoadXML()
        {
            bool success = false;
            dataGridView1.DataSource = xml.GetUserData().Tables[0];
            
            return success;
        }
        private void LoadRoleToCombo()
        {
            cboRole.DataSource = xml.GetRoleXML();
            cboRole.ValueMember = "id";
            cboRole.DisplayMember = "rolename";            
        }

        private void LoadShopToCombo()
        {
            cboShopID.DataSource = xml.GetCustomerData().Tables[0];
            cboShopID.ValueMember = "OID";
            cboShopID.DisplayMember = "CustName";
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            LoadRoleToCombo();
            LoadShopToCombo();
            LoadXML();
        }

        private void addToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveData() == true)
            {
                MessageBox.Show("Save complete!");
            }
        }
        private bool SaveData()
        {
            try
            {
                ClsConnectSql db = new ClsConnectSql();          
                DataTable dt = new DataTable();
                SqlDataAdapter da = db.DataAdapter("select * from xStaff where id='" + txtID.Text + "'", true);
                da.Fill(dt);
                DataRow row = dt.NewRow();
                if (dt.Rows.Count > 0) row = dt.Rows[0];
                row["id"] = txtID.Text;
                row["name"] = txtName.Text;
                row["password"] = txtPassword.Text;
                row["store"] = txtStore.Text;
                row["branch"] = txtBranch.Text;
                row["department"] = txtDepartment.Text;
                row["shopid"] = cboShopID.SelectedValue;
                row["role"] = cboRole.SelectedValue;
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private bool SaveFormToXML()
        {
            try
            {
                DataTable dt = xml.GetUserXML();
                DataRow[] dr = dt.Select(@"id='" + txtID.Text + @"'");
                DataRow row = dt.NewRow();
                if (dr.Length > 0)
                {
                    foreach (DataRow r in dr)
                    {
                        row = r;
                    }
                }
                row["id"] = txtID.Text;
                row["name"] = txtName.Text;
                row["password"] = txtPassword.Text;
                row["store"] = txtStore.Text;
                row["branch"] = txtBranch.Text;
                row["department"] = txtDepartment.Text;
                row["shopid"] = cboShopID.SelectedValue;
                row["role"] = cboRole.SelectedValue;
                if (row.RowState == DataRowState.Detached)
                {
                    dt.Rows.Add(row);
                }
                dt.WriteXml("Data\\" + "listuser.xml");
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells["password"].Value.ToString();
                cboRole.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["role"].Value.ToString();
                cboShopID.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["shopid"].Value.ToString();
                txtStore.Text = dataGridView1.Rows[e.RowIndex].Cells["store"].Value.ToString();
                txtBranch.Text = dataGridView1.Rows[e.RowIndex].Cells["branch"].Value.ToString();
                txtDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells["department"].Value.ToString();

            }

        }
        private bool DeleteData()
        {
            try
            {
                ClsConnectSql db = new ClsConnectSql();                
                bool chk= db.Execute("delete from xStaff where id='" + txtID.Text + "'"); 
                if (chk==true)
                {
                    chk=DeleteInXML();
                }
                db.CloseConnection();
                return chk;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool DeleteInXML()
        {
            try
            {
                DataTable dt = xml.GetUserXML();
                DataRow[] dr = dt.Select(@"id='" + txtID.Text + @"'");
                if (dr.Length > 0)
                {
                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Remove(r);
                    }
                }
                dt.WriteXml("Data\\" + "listuser.xml");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void deleteFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DeleteData()==true)
            {
                MessageBox.Show("Delete Complete!");
            }
        }
    }
}
