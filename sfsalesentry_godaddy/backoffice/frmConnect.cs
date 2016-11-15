using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace shopsales_tools
{
    public partial class frmConnect : Form
    {
        public frmConnect()
        {
            InitializeComponent();
        }

        private void frmConnect_Load(object sender, EventArgs e)
        {
            LoadConnectionToForm(0);
            txtPath.Text = Program.StartupPath;
        }

        private void LoadConnectionToForm(int idx)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath + @"\\Connect.xml");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count>0)
                {
                    textBox1.Text = dt.Rows[idx]["Server"].ToString();
                    textBox2.Text = dt.Rows[idx]["User"].ToString();
                    textBox3.Text = dt.Rows[idx]["Password"].ToString();
                    textBox4.Text = dt.Rows[idx]["Database"].ToString();
                }
                dt.Dispose();
                ds.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool SaveConnectionToXML(int idx)
        {
            bool success = false;
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Application.StartupPath + @"\\Connect.xml");
                XmlNode root = xml.DocumentElement;
                foreach (XmlNode nde in root.ChildNodes[idx].ChildNodes)
                {
                    if (nde.Name == "Server") nde.InnerText = textBox1.Text;
                    if (nde.Name == "User") nde.InnerText = textBox2.Text;
                    if (nde.Name == "Password") nde.InnerText = textBox3.Text;
                    if (nde.Name == "Database") nde.InnerText = textBox4.Text;
                }
                xml.Save(Application.StartupPath+@"\\Connect.xml");
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return success;
        }

        private bool TestConnection()
        {
            bool success = true;
            try
            {
                string strconn = @"server=" + textBox1.Text + @";user id=" + textBox2.Text + @";password=" + textBox3.Text + @";database=" + textBox4.Text;
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                success = false;
            }
            return success;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TestConnection() == true)
            {
                if (SaveConnectionToXML((int)connID.Value) == true)
                {
                    MessageBox.Show("Save Connection "+connID.Value.ToString()+" Complete!");
                }
            }
        }

        private void connID_ValueChanged(object sender, EventArgs e)
        {
            LoadConnectionToForm((int)connID.Value);
        }
    }
}
