using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace shopsales_tools
{
    public partial class frmUtil : Form
    {
        public struct GoodsData
        {
            public string GoodsCode;
            public string GoodsName;
            public string GoodsKind;
            public string SMid;
            public string Colid;
            public string ColInit;
            public string STid;
            public string SSid;
            public string SizeNo;
            public string SizeMin;
            public string SizeMax;
            public string ProdKindId;
            public string ProdCatId;
            public string ProdGroupId;
            public string stdPriceSell;
            public string stdPriceCost;
            public string minPriceSell;
        }
        ClsConnectSql db = new ClsConnectSql();
        public frmUtil()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet xmlDs = new DataSet();
            xmlDs.DataSetName = "NewDataSet";
            DataTable ds = db.RecordSet("select * from xGoods order by GoodsCode");
            ds.TableName = "Table";
            ds.WriteXml("Data\\" + "Goods.xml");
            MessageBox.Show("Completed!");
        }
        DataTable GoodsXMLStruct()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("OID");
            dt.Columns.Add("GoodsCode");
            dt.Columns.Add("GoodsName");
            dt.Columns.Add("ModelName");
            dt.Columns.Add("ColNameInit");
            dt.Columns.Add("ColNameEng");
            dt.Columns.Add("ColNameTh");
            dt.Columns.Add("SizeNo");
            dt.Columns.Add("StdSellPrice");
            dt.Columns.Add("ProdCatCode");
            dt.Columns.Add("ProdCatName");
            dt.Columns.Add("STCode");
            dt.Columns.Add("STName");
            dt.Columns.Add("SMId");
            dt.Columns.Add("STId");
            dt.Columns.Add("SSId");
            dt.Columns.Add("ColId");
            dt.Columns.Add("ProdKindId");
            dt.Columns.Add("ProdCatId");
            dt.Columns.Add("ProdGroupId");

            return dt;
        }
        void LoadData(bool autoupdate = false)
        {
            DataTable DestData = GoodsXMLStruct();            
            DataTable src = db.RecordSet("select * from PriceList order by [Code]");
            string colorstr = "";
            string colorcode = "";
            string colornameth = "";
            string colornameen = "";
            string oid = "";
            string goodscode = "";
            string goodsname = "";
            string sizestr = "";
            int minsize = 0;
            int maxsize = 0;
            int i = 0;
            int j = 0;
            string[] colorlist;
            string[] sizelist;

            foreach (DataRow srcdata in src.Rows)
            {
                colorstr = srcdata["Color"].ToString()+",";
                sizestr = srcdata["Group1"].ToString()+"-";
                colorlist = colorstr.Split(',');
                sizelist = sizestr.Split('-');
                //find dize min,max of each item
                i = 0;
                minsize = 0;
                maxsize = 0;
                foreach (string sz in sizelist)
                {
                    if (sz!="")
                    {
                        if (i == 0)
                        {
                            minsize = Convert.ToInt16(sz);
                            maxsize = Convert.ToInt16(sz);
                        }
                        else
                        {
                            maxsize = Convert.ToInt16(sz);
                        }
                        i = i + 1;

                    }
                }
                //loop each size
                ClsXML xmlType = new ClsXML("Data\\ShoeType.xml");
                ClsXML xmlCategory = new ClsXML("Data\\ShoeCategory.xml");
                for(j=minsize;j<=maxsize;j++)
                {
                    foreach (string col in colorlist)
                    {
                        //for each color name
                        if (col.Trim()!="")
                        {
                            DataRow row = DestData.NewRow();

                            colorcode = db.FindValue("select ColnameInit from shoeColor where ColNameTh='" + col.Trim() + "' and colstatus=1 ", "-");
                            colornameth = col.Trim();
                            colornameen = col.Trim();
                            SqlDataReader colordata = db.DataReader("select * from ShoeColor where colNameInit='" + colorcode + "' and colstatus=1 ");
                            if(colordata.HasRows==true)
                            {
                                colornameth = colordata["ColNameTh"].ToString ();
                                colornameen = colordata["ColNameEng"].ToString();
                                row["ColId"] = colordata["ColId"].ToString();
                            }
                            else
                            {
                                row["ColId"] = "";
                            }
                            colordata.Close();
                            goodscode = srcdata["Code"].ToString().Trim() + colorcode.Trim() + (j * 10).ToString();
                            goodsname = (srcdata["Code"].ToString().Trim () + " " + col.Trim() + " " + j).ToString();
                            //find existing data
                            SqlDataReader modeldata = db.DataReader("select * from xGoodsWithDetail where GoodsCode='" + goodscode + "'");
                            oid = "";
                            if(modeldata.HasRows==true)
                            {
                                oid = modeldata["OID"].ToString();
                                row["SMId"] = modeldata["SMId"].ToString();
                                row["SSId"] = modeldata["SSId"].ToString();
                                row["ProdKindId"] = modeldata["ProdKindId"].ToString();
                                row["ProdCatCode"] = modeldata["ProdCatCode"].ToString();
                                row["ProdCatId"] = modeldata["ProdCatId"].ToString();
                                row["ProdCatName"] = xmlCategory.FindValue("ProdCatId='" + modeldata["ProdCatId"].ToString() + "'", "ProdCatName");
                                modeldata.Close();
                                if(autoupdate) db.Execute("update xGoods set GoodsCode='"+ goodscode.Trim() +"',GoodsName='"+ goodsname.Trim() + "',StdSellPrice =" + srcdata["Price"].ToString() + ",ProdGroupId='" + srcdata["Group"].ToString () + "',STid='" + srcdata["Type"].ToString () +"' where GoodsCode='" + goodscode + "'");
                            }
                            else
                            {
                                modeldata.Close();                                
                                row["ProdKindId"] = 1; //FG
                                row["ProdCatId"] = 1; //not define
                                row["ProdCatcode"] = "--";
                                row["ProdCatName"] = "ไม่ระบุ";
                                row["SMId"] = "";

                                row["SSId"] = 1; //default

                                modeldata = db.DataReader("select * from ShoeModelWithDetail where [Name]='" + srcdata["Code"].ToString().Trim() + "'");
                                if (modeldata.HasRows )
                                {
                                    row["SMId"] = modeldata["SMId"].ToString();
                                    row["SSId"] = modeldata["SSId"].ToString();
                                    row["ProdCatCode"] = modeldata["ProdCatCode"].ToString();
                                    row["ProdCatId"] = modeldata["ProdCatId"].ToString();
                                    modeldata.Close();
                                }
                                else
                                {
                                    modeldata.Close();
                                    string mold = db.FindValue("select moldid from shoemold where moldname='"+srcdata["Code"].ToString().Substring(0,3)+"'","1");
                                    if(autoupdate)
                                    {
                                        if (insertModel(srcdata["Code"].ToString().Trim(), "", mold, row["SSId"].ToString(), row["STId"].ToString(), row["ProdCatId"].ToString(), minsize.ToString(), maxsize.ToString(), "") == true)
                                        {
                                            row["SMId"] = db.FindValue("select SMId from ShoeModel where [Name]='" + srcdata["Code"].ToString() + "'");
                                        }
                                    }
                                }
                                //insert new goods data
                                GoodsData data = new GoodsData();
                                data.GoodsCode = goodscode;
                                data.GoodsName = goodsname;
                                data.ProdKindId = row["ProdKindId"].ToString();
                                data.ProdCatId = row["ProdCatId"].ToString();
                                data.ProdGroupId = srcdata["Group"].ToString();
                                data.STid = srcdata["Type"].ToString();

                                data.SMid =row["SMId"].ToString();
                                data.Colid = row["ColId"].ToString();                                
                                data.SSid = row["SSId"].ToString();
                                data.SizeNo = j.ToString();
                                
                                data.stdPriceSell = srcdata["Price"].ToString();
                                data.stdPriceCost = srcdata["Price"].ToString();
                                if(autoupdate)
                                {
                                    if (InsertGoods(data) == true)
                                    {
                                        oid = db.FindValue("select OID from xGoods where GoodsCode='" + goodscode + "'");
                                    }
                                }
                            }
                            row["OID"] = oid;
                            row["GoodsCode"] = goodscode;
                            row["GoodsName"] = goodsname;
                            row["ModelName"] = srcdata["Code"].ToString().Trim();
                            row["ColNameInit"] = colorcode;
                            row["ColNameTh"] = colornameth;
                            row["ColNameEng"] = colornameen;
                            row["SizeNo"] = j;
                            row["StdSellPrice"] = srcdata["Price"];
                            row["ProdGroupId"] = srcdata["Group"].ToString(); //Shoes
                            row["STId"] = srcdata["Type"].ToString(); 
                            row["STCode"]= xmlType.FindValue("STId='" + row["STid"].ToString() + "'", "STCode");
                            row["STName"] = xmlType.FindValue("STCode='" + row["STCode"].ToString()+"'", "STName");

                            row["ProdCatName"] = xmlCategory.FindValue("ProdCatCode='" + row["ProdCatCode"].ToString()+"'", "ProdCatName");
                            DestData.Rows.Add(row);
                        }
                    }
                }
            }
            dataGridView1.DataSource = DestData;
        }
        bool insertModel(string n1,string n2,string mold,string ss,string st,string cat,string min,string max,string remark)
        {
            string sqlcmd = "dbo.sp_insert_shoemodel '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}' ";
            sqlcmd = string.Format(sqlcmd, n1, n2, mold, ss, st, cat, min, max, remark);
            return db.Execute(sqlcmd);
        }
        bool InsertGoods(GoodsData data)
        {
            string insertcmd = "dbo.sp_add_xgoods ";
            insertcmd += " '" + data.GoodsCode + "',";
            insertcmd += " '" + data.GoodsName + "',";
            insertcmd += " '',";
            insertcmd += " '',";
            insertcmd += " '',";
            insertcmd += " 0" + data.ProdKindId + ",";
            insertcmd += " 0" + data.ProdCatId + ",";
            insertcmd += " 0" + data.ProdGroupId + ",";
            insertcmd += " 0" + data.SMid + ",";
            insertcmd += " 0" + data.Colid + ",";
            insertcmd += " 0" + data.STid + ",";
            insertcmd += " 0" + data.SSid + ",";
            insertcmd += " 0" + data.SizeNo + ",";
            insertcmd += " 0" + data.stdPriceSell + ",";
            insertcmd += " 0" + data.stdPriceCost + ",";
            insertcmd += "''";

            return db.Execute(insertcmd);
        }
        private void frmUtil_Load(object sender, EventArgs e)
        {            
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData(checkBox1.Checked);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmUpdateToWeb frm = new frmUpdateToWeb();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCompareDB frm = new frmCompareDB();
            frm.ShowDialog();
        }
    }
}
