using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace shopsales_tools
{
    class ClsXML : IDisposable
    {
        private DataSet ds;
        private string err = "";
        private string updatecmd = "";
        private string insertcmd = "";
        private string path=Program.StartupPath+"\\Data\\";
        public ClsXML()
        {
            ds = new DataSet();
            err = "";
        }
        public ClsXML(string filename)
        {
            ds = new DataSet();
            err = "";
            LoadXML(filename);
        }
        public string GetXML(string fname)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(fname);
            return xml.OuterXml;
        }
        public string GetXML(DataTable dt)
        {
            StringWriter wr = new StringWriter();
            dt.WriteXml(wr);
            return wr.ToString();
        }
        public bool LoadXML(string fname)
        {
            try
            {
                err = "";
                ds = new DataSet();
                ds.ReadXml(fname);
            }
            catch(Exception ex)
            {
                err = ex.Message;
            }
            if (err == "") { return true; } else { return false; }
        }
        public bool LoadXMLString(string data)
        {
            try
            {
                err = "";
                XmlTextReader rd = new XmlTextReader(new StringReader(data));
                rd.Read();
                ds = new DataSet();
                ds.ReadXml(rd,XmlReadMode.Auto);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            if (err == "") { return true; } else { return false; }
        }
        public bool SaveXML(string fname)
        {
            try
            {
                ds.WriteXml(fname);
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
        public string Errormessage()
        {
            return err;
        }
        public DataSet Dataset()
        {
            return ds;
        }
        public DataTable Datatable(int index=0)
        {
            try
            {
                return ds.Tables[index];
            }
            catch
            {
                return new DataTable();
            }
            
        }
        public DataTable Datatable(string xmlData)
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            if(LoadXMLString(xmlData)==true)
            {
                if(ds.Tables.Count>0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        public string FindValue(string filter,string fldname)
        {
            string val = "";
            try
            {
                DataRow[] dr = ds.Tables[0].Select(filter);
                foreach (DataRow r in dr)
                {
                    val = r[fldname].ToString();
                }
            }
            catch
            {
                val = "";
            }
            return val;
        }
        public DataTable GetColorXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "Color.xml");
            return d.Tables[0];
        }
        public DataTable GetCustomerXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "Customer.xml");
            return d.Tables[0];
        }
        public DataTable GetSalesTypeXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "SalesType.xml");
            return d.Tables[0];
        }
        public DataTable GetUserXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "listuser.xml");
            return d.Tables[0];
        }
        public DataTable GetRoleXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "role.xml");
            return d.Tables[0];
        }
        public DataTable GetShoeGroupXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "ShoeGroup.xml");
            return d.Tables[0];
        }
        public DataTable GetCustomerGroupXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "CustomerGroup.xml");
            return d.Tables[0];
        }
        public DataTable GetShoeModelXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "ShoeModel.xml");
            return d.Tables[0];
        }
        public DataTable GetShoeMoldXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "ShoeMold.xml");
            return d.Tables[0];
        }
        public DataTable GetShoeSizeXML()
        {
            DataSet d = new DataSet();
            d.ReadXml(path + "ShoeSize.xml");
            return d.Tables[0];
        }
        public string GetUpdateCommandData()
        {
            return updatecmd;
        }
        public string GetInsertCommandData()
        {
            return insertcmd;
        }
        public DataSet GetSalesTypeData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"select OID,[Code],[Name] as Description from SaleType").Fill(ds);
            db.CloseConnection();
            updatecmd = "update SaleType set [Code]='{1}',[Name]='{2}' where [OID]='{0}'";
            insertcmd = "insert into SaleType ([Code],[Name],[Name2],[Remark],[Status]) values ('{1}','{2}','','','1')";
            return ds;
        }
        public DataSet GetShoeTypeData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"select stID,STCode,STName,STName2 from ShoeType").Fill(ds);
            db.CloseConnection();
            updatecmd = "update ShoeType set stCode='{1}',STName='{2}',STName2='{3}' where stID='{0}'";
            insertcmd = "insert into ShoeType(STCode,STName,STName2,STRemark,status) value('{1}','{2}','{3}','','1')";
            return ds;
        }
        public DataSet GetShoeGroupData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"select prodGroupID as OID,prodGroupCode as GroupID,prodGroupName as GroupName from ProductGroup").Fill(ds);
            db.CloseConnection();
            updatecmd = "update Productgroup set prodGroupCode='{1}',prodGroupName='{2}' where prodGroupID='{0}'";
            insertcmd = "insert into ProductGroup(prodGroupCode,prodGroupName,prodGroupName2,prodGroupRemark,prodGroupstatus) value('{1}','{2}','','','1')";
            return ds;
        }
        public DataSet GetShoeCategoryData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"select ProdCatID,ProdCatCode,ProdCatName from ProductCategory").Fill(ds);
            updatecmd = "update ProductCategory set ProdCatcode='{1}',ProdCatName='{2}' where ProdCatId='{0}'";
            insertcmd = "insert ProductCategory (ProdCatCode,ProdCatName,ProdCatName2,ProdCatRemark,ProdCatStatus) values('{1}','{2}','','','1')";
            db.CloseConnection();
            return ds;
        }
        public DataSet GetCustomerData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"select OID,[Code] as CustCode,[Name] as CustName,[Name2] as ShopName,[Remark] as Branch,GroupID from xCustomer where status>0 order by GroupID,CustCode").Fill(ds);
            updatecmd = "update xCustomer set [Code]='{1}',[Name]='{2}',[Name2]='{3}',[Remark]='{4}' where [OID]='{0}'";
            insertcmd = "insert into xCustomer ([Code],[Name],[Name2],GroupId,GPx,Remark,Status) values ('{1}','{2}','{3}','1','0','{4}','1')";
            db.CloseConnection();
            return ds;
        }
        public DataSet GetCustomerGroupData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"select OID,[Code] as CustGroupCode,[Name] as CustGroupNameEng,[Name2] as CustGroupNameTh,Remark from xCustomerGroup order by OID").Fill(ds);
            updatecmd = "update xCustomergroup set [Code]='{1}',[Name]='{2}',[Name2]='{3}',[Remark]='{4}' where [OId]='{0}'";
            insertcmd = "";
            db.CloseConnection();
            return ds;
        }
        public DataSet GetGoodsData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"SELECT OID,GoodsCode,GoodsName,ModelName,ColNameInit,ColNameEng,ColNameTh,ColTypeId,SizeNo,StdSellPrice,ProdStdCost,ProdCatCode,ProdCatName,STCode,STName,STName2,SMCode,SMid,SSid,STid,Colid,ProdGroupId,ProdCatid,ProdKindid,ProdGroupName FROM xGoodsWithDetail").Fill(ds);
            insertcmd = "EXEC dbo.sp_add_xgoods '{1}','{2}','','','','{23}','{22}','{21}','{17}','{20}','{19}','{18}','{8}','{9}','{10}',''";
            updatecmd = "update xGoods set GoodsCode='{1}',GoodsName='{2}',SizeNo='{8}',StdSellPrice='{9}',ProdStdCost='{10}',SMid='{17}',SSid='{18}',STid='{19}',Colid='{20}',ProdGroupId='{21}',ProdCatid='{22}',ProdKindid='{23}' where OID='{0}'";
            db.CloseConnection();
            return ds;
        }
        public DataSet GetUserData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"SELECT * from xStaff order by [id]").Fill(ds);
            updatecmd = "update xStaff set id='{1}',[name]='{2}',[password]='{3}',store='{4}',branch='{5}',department='{6}',shopid='{7}',role='{8}' where oid='{0}'";
            insertcmd = "insert into xStaff(id,[password],store,branch,department,shopid,role) values ('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
            db.CloseConnection();
            return ds;

        }
        public DataSet GetColorData()
        {
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(@"SELECT [ColId],[ColNameInit],[ColNameTh] as ColTH,[ColNameEng] as ColEN,[ColTypeID] FROM [ShoeColor] where colstatus=1 order by ColNameTh").Fill(ds);
            insertcmd = "";
            updatecmd = "update shoecolor set ColNameInit='{1}',ColNameTh='{2}',ColNameEng='{3}',ColTypeId='{4}' where Colid='{0}'";
            db.CloseConnection();
            return ds;
        }
        public DataSet GetShoeModelData()
        {
            string sql = @"SELECT [SMId],[SMCode],[Name] as Model,[Name2] as Mold,[MoldId],[SSId],[STId],[ProdCatId],[MinSize],[MaxSize] FROM [ShoeModel] order by [SMId]";
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(sql).Fill(ds);
            insertcmd = "";
            updatecmd = "";
            db.CloseConnection();
            return ds;
        }
        public DataSet GetShoeMoldData()
        {
            string sql = @"SELECT [MoldId],[MoldCode],[MoldName] FROM [ShoeMold] order by [MoldId]";
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(sql).Fill(ds);
            insertcmd = "";
            updatecmd = "";
            db.CloseConnection();
            return ds;
        }
        public DataSet GetShoeSizeData()
        {
            string sql = @"SELECT [SSId],[SSCode],[SSName],[MinSize],[MaxSize],[SStep],[SSRemark] FROM [ShoeSize] order by [SSId]";
            DataSet ds = new DataSet();
            ClsConnectSql db = new ClsConnectSql();
            db.DataAdapter(sql).Fill(ds);
            insertcmd = "";
            updatecmd = "";
            db.CloseConnection();
            return ds;
        }
        public DataTable GetDataForXML(string tbalias)
        {
            DataTable dt = new DataTable();
            err = "";
            switch (tbalias.ToLower())
            {
                case "color":
                    dt = GetColorData().Tables[0];
                    break;
                case "salestype":
                    dt = GetSalesTypeData().Tables[0];
                    break;
                case "shoetype":
                    dt = GetShoeTypeData().Tables[0];
                    break;
                case "shoecategory":
                    dt = GetShoeCategoryData().Tables[0];
                    break;
                case "goods":
                    dt = GetGoodsData().Tables[0];
                    break;
                case "listuser":
                    dt = GetUserData().Tables[0];
                    break;
                case "customer":
                    dt = GetCustomerData().Tables[0];
                    break;
                case "customergroup":
                    dt = GetCustomerGroupData().Tables[0];
                    break;
                case "shoegroup":
                    dt = GetShoeGroupData().Tables[0];
                    break;
                case "shoemodel":
                    dt = GetShoeModelData().Tables[0];
                    break;
                case "shoemold":
                    dt = GetShoeMoldData().Tables[0];
                    break;
                case "shoesize":
                    dt = GetShoeSizeData().Tables[0];
                    break;
                default:
                    err = tbalias + " Not Found";
                    break;
            }
            return dt;
        }

        public void Dispose()
        {
            ds.Dispose();
            //throw new NotImplementedException();
        }
    }
}
