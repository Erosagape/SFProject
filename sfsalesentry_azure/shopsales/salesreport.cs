using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using System.Data;

namespace shopsales
{
    public partial class salesreport : DevExpress.XtraReports.UI.XtraReport
    {
        public salesreport(DataTable dt,string caption)
        {
            this.DataSource = dt;
            this.DataAdapter = dt.TableName;
            InitializeComponent();
            this.lblCaption.Text = caption;
        }
    }
}
