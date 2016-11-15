using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SfDeliverTracking
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        protected string ReadData(string fname)
        {
            string JsonString = "";
            try
            {
                StreamReader rd = new StreamReader(fname, System.Text.UTF8Encoding.UTF8);
                JsonString = rd.ReadToEnd();
                rd.Close();
            }
            catch
            {

            }
            return JsonString;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dlg.Filter = "JSON Files|*.json;";
            dlg.ShowDialog();
            string fname = dlg.FileName;
            if (File.Exists(fname) == true)
            {
                string jsonString = ReadData(fname);
                textBox1.Text = jsonString;
                DataTable dt = ClsData.GetDataTableFromJSON(jsonString);
                dataGridView1.DataSource = dt;
                label1.Text = fname;
                MessageBox.Show("Found " + dt.Rows.Count);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(label1.Text, textBox1.Text);
            MessageBox.Show("Finished!");

        }
    }

}

