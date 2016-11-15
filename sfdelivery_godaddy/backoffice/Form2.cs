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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void saveListuserxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveUserXML();
            MessageBox.Show("Finished");
            dataGridView1.DataSource = ClsData.GetWebUserData();
        }
        private void SaveUserXML()
        {
            DataTable dt = ClsData.GetWebUserData();
            dt.WriteXml("listuser.xml");
        }        
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dlg.Filter = "XML|*.xml";
            dlg.ShowDialog();
            label1.Text = dlg.FileName;
            if(System.IO.File.Exists(label1.Text)==true)
            {
                ClsXML xml = new ClsXML(label1.Text);
                dataGridView1.DataSource = xml.Datatable();
            }
        }
    }
}
