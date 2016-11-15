using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class testws : System.Web.UI.Page
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
            lstFiles.Items.Clear();
            foreach (FileInfo file in files.GetFiles(txtFilter.Text +"*.json"))
            {
                lstFiles.Items.Add(file.Name);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            IService sv = new IService();            
            string msg = sv.OpenDataForupdate(txtFilter.Text);
            lstLog.Items.Add(msg);
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            IService sv = new IService();
            if(lstFiles.SelectedItem.Text=="")
            {
                foreach (ListItem lst in lstFiles.Items)
                {
                    string msg = sv.ReadDataForupdate(lst.Text.Replace(".json", ""));
                    lstLog.Items.Add(msg);
                }
            }
            else
            {
                string msg = sv.ReadDataForupdate(lstFiles.SelectedItem.Text.Replace(".json", ""));
                lstLog.Items.Add(msg);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            IService sv = new IService();
            string msg=sv.CloseDataForupdate(txtFilter.Text);
            lstLog.Items.Add(msg);
        }
    }
}