using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                LoadList();
            }
        }
        protected void LoadList()
        {
            DirectoryInfo files = new DirectoryInfo(ClsData.GetPath());
            foreach (FileInfo file in files.GetFiles("*.json"))
            {
                ListBox1.Items.Add(file.Name);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IService svc = new IService();
            string msg=svc.WriteDataUpdate(TextBox1.Text,TextBox2.Text);
            Label1.Text = msg;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            IService svc = new IService();
            string msg = svc.UpdateData(TextBox1.Text.Replace(".json", ""));
            Label1.Text = msg;
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StreamReader rd = new StreamReader(ClsData.GetPath() + ListBox1.SelectedItem.Text,System.Text.UTF8Encoding.UTF8);
            TextBox2.Text = rd.ReadToEnd();
            TextBox1.Text= ListBox1.SelectedItem.Text;
            rd.Close();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DirectoryInfo files = new DirectoryInfo(ClsData.GetPath());
            foreach (FileInfo file in files.GetFiles("*.json"))
            {
                file.Delete();
            }
            LoadList();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            IService svc = new IService();
            string msg = "";
            for(int i=0;i<ListBox1.Items.Count;i++)
            {
                string dataname = ListBox1.Items[i].Text.Replace(".json", "");
                try
                {
                    msg += svc.UpdateData(dataname) + "<br/>";
                }
                catch (Exception ex)
                {
                    msg += "ERROR " + ex.Message + "<br/>";
                }
            }
            LoadList();
            Label1.Text = msg;
        }
    }
}