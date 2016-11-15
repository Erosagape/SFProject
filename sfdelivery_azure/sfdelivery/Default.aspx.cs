using System;

namespace sfdelivery
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Browser.IsMobileDevice==true)
            {
                HyperLink1.Visible = true;
            }
            else
            {
                HyperLink1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}