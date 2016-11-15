using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
namespace shopsales
{
    public partial class selectgoods : System.Web.UI.Page
    {
        private static List<string> LOV_Goods = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["returnto"]!=null)
                {
                    txtReturnTo.Value = Request.QueryString["returnto"].ToString();
                }
                CreateList();
            }
        }
        private void CreateList()
        {
            DataTable dtShoe = ClsData.ShoeData();
            LOV_Goods.Clear();
            foreach (DataRow r in dtShoe.Rows)
            {
                LOV_Goods.Add(string.Format("{0}|{1}", r["GoodsName"].ToString(), r["GoodsCode"].ToString()));
            }
        }
        [WebMethod]
        public static string[] GetGoods()
        {
            LOV_Goods.Sort();
            return LOV_Goods.ToArray();
        }
    }
}