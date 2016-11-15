using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using System.Data;

namespace shopsales
{
    public partial class sumreport : DevExpress.XtraReports.UI.XtraReport
    {
        public sumreport()
        {

        }
        public sumreport(DataTable dt,string caption)
        {
            this.DataSource = dt;
            this.DataAdapter = dt.TableName;
            InitializeComponent();
            this.lblCaption.Text = caption;
        }

    }
}
