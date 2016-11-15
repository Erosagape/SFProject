using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace SfDeliverTracking
{
    public static class ClsData
    {
        public static DataSet GetDataSetFromJSON(string JSONString)
        {
            DataSet ds = new DataSet();
            ds = (DataSet)JsonConvert.DeserializeObject(JSONString, typeof(DataSet));
            return ds;
        }
        public static DataTable GetDataTableFromJSON(string JSONString, int tbindex = 0)
        {
            DataTable dt = new DataTable();
            if (JSONString.Substring(0, 1) == "{")
            {
                DataSet ds = GetDataSetFromJSON(JSONString);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[tbindex];
                }
            }
            if (JSONString.Substring(0, 1) == "[")
            {
                dt = (DataTable)JsonConvert.DeserializeObject(JSONString, typeof(DataTable));
            }
            return dt;
        }

        public static string GetXMLFromJSON(string jsonstring)
        {
            string data = "";
            try
            {
                XmlDocument xml = new XmlDocument();
                XmlNode docNode = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                xml.AppendChild(docNode);
                XmlNode root = xml.CreateElement("NewDataSet");
                XmlNode tbl = xml.CreateElement("Table");
                XmlNode node = JsonConvert.DeserializeXmlNode(jsonstring, "Table");
                tbl.AppendChild(node);
                root.AppendChild(tbl);
                xml.AppendChild(root);
                data = xml.OuterXml.ToString();

            }
            catch (Exception ex)
            {
                data = GetJSONMessage("ERROR", ex.Message);
            }
            return data;
        }
        public static string GetJSONMessage(string header, string text)
        {
            return @"{""Message"":[{""" + header + @""":""" + text + @"""}]}";
        }
        public static string GetJSONFromDataTable(DataTable dt)
        {
            string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
        public static string GetJSONFromTableRow(DataRow[] rowset, DataTable dt, string grouplog = "")
        {
            string str = "[";
            if(grouplog!="")
            {
                str = @"{""" + grouplog + @""":" + str;
            }
            int i = 0;
            foreach (DataRow dr in rowset)
            {
                if (i != 0) str += ",";
                str += "{";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j != 0) str += ",";
                    string val = dr[j].ToString().Trim();
                    val = val.Replace(@"""", @"\""");
                    str += @"""" + dt.Columns[j].ColumnName + @""":""" + val + @"""";
                }
                str += "}";
                i = i + 1;
            }
            if (i == 0)
            {
                {
                    str += "{";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != 0) str += ",";
                        str += @"""" + dt.Columns[j].ColumnName + @""":""""";
                    }
                    str += "}";
                }
            }            
            str += "]";
            if (grouplog != "") str += "}";
            return str;
        }
        public static DataTable GetWebUserData()
        {
            ClsConnectSql db = new ClsConnectSql(2);
            string sqlUser = @"select oid,userid as id,empfullname as [name],userpass as [password],usergroup as [zonecode],saleszonename as zonename,usertype as roleid,EmpDesc as roleName,empcode as empid from vWebUsers";
            DataTable dt = db.RecordSet(sqlUser);
            dt.TableName = "listuser";
            db.CloseConnection();
            return dt;
        }
        public static string TransportData()
        {
            string sql = @"
select c.ID as CustID,c.Name as CustName,  ISNULL(SUBSTRING(t.ID,5,LEN(t.ID)),'N/A') as TransID,ISNULL(t.Name,'ไม่ระบุ') as TransName
from XCustomers as c LEFT OUTER JOIN [$ICS_Footwear].dbo.Yamcontrols  as t 
on c.Transport=SUBSTRING(t.ID,5,LEN(t.ID))
--where t.ID like '+ลฮ%'
";
            return sql;
        }
        public static string TransHeaderI_Detail(string wherec)
        {
            string sql = @"(
SELECT a.ID, a.Status AS IStatus, FORMAT(a.DocDate,'dd/MM/yyyy') as DocDate, FORMAT(a.DueDate,'dd/MM/yyyy') as DueDate
, a.Reference, a.Customer, b.Name AS CustomerName, 
ISNULL(a.Salesman,'') as SalesMan, ISNULL(a.Account,'') as Account, 
ISNULL(a.ShipTo1,'') as ShipTo1, ISNULL(a.ShipTo2,'') as ShipTo2, 
ISNULL(a.Remark1,'') as Remark1, ISNULL(a.Remark2,'') as Remark2, 
ISNULL(a.StateNo,'') as StateNo, ISNULL(a.Term,'') as Term, 
ISNULL(a.DiscPer,0) as DiscPer, ISNULL(a.Gross,0) as Gross,ISNULL(a.VatAmt,0) as VatAmt, 
ISNULL(a.Amount,0) as Amount, ISNULL(a.Qty,0) as Qty, ISNULL(a.Box,0) as Box, ISNULL(a.Bundle,0) as Bundle, 
ISNULL(a.Sack,0) as Sack, ISNULL(a.Driver,'') as Driver, ISNULL(a.Mark6,'') as Mark6, 
ISNULL(c.ID,'') AS SID, ISNULL(c.Status,'') AS SStatus, 
FORMAT(c.DocDate,'dd/MM/yyyy') AS SDocDate, ISNULL(b.Salesman,'') AS SSalesMan,ISNULL(a.Mark8,'') as Mark8, 
d.Sequence, ISNULL(d.Product,'') as Product, ISNULL(e.Name,'') AS ProductName, ISNULL(d.Remark,'') as Remark, 
ISNULL(d.VAT,0) as VAT, ISNULL(d.Packs,0) as Packs, ISNULL(d.Qty,0) AS DQty, ISNULL(d.DiscPer,0) AS DDiscPer, ISNULL(d.UnitPrice,0) as UnitPrice, ISNULL(d.Amount,0) AS DAmount, ISNULL(d.TotalCost,0) as TotalCost 
FROM SFOOT_$ICS.dbo.XProducts AS e INNER JOIN 
dbo.TranDetails AS d ON e.ID = d.Product RIGHT OUTER JOIN 
dbo.TranHeaders AS a ON d.ID = a.ID LEFT OUTER JOIN 
dbo.TranHeaders AS c ON SUBSTRING(a.Reference, 2,LEN(a.Reference)) = c.ID LEFT OUTER JOIN 
SFOOT_$ICS.dbo.XCustomers AS b ON a.Customer = b.ID 
WHERE (a.ID LIKE 'I%') AND (c.ID like 'S%') " + wherec + @"
) as t ";
            return sql;
        }
        public static string TransHeaderI(string wherec)
        {
            string sql = @"(
SELECT a.ID, a.Status AS IStatus, FORMAT(a.DocDate,'dd/MM/yyyy') as DocDate, FORMAT(a.DueDate,'dd/MM/yyyy') as DueDate
, a.Reference, a.Customer, b.Name AS CustomerName, 
ISNULL(a.Salesman,'') as SalesMan, ISNULL(a.Account,'') as Account, 
ISNULL(a.ShipTo1,'') as ShipTo1, ISNULL(a.ShipTo2,'') as ShipTo2, 
ISNULL(a.Remark1,'') as Remark1, ISNULL(a.Remark2,'') as Remark2, 
ISNULL(a.StateNo,'') as StateNo, ISNULL(a.Term,'') as Term, 
ISNULL(a.DiscPer,0) as DiscPer, ISNULL(a.Gross,0) as Gross,ISNULL(a.VatAmt,0) as VatAmt, 
ISNULL(a.Amount,0) as Amount, ISNULL(a.Qty,0) as Qty, ISNULL(a.Box,0) as Box, ISNULL(a.Bundle,0) as Bundle, 
ISNULL(a.Sack,0) as Sack, ISNULL(a.Driver,'') as Driver, ISNULL(a.Mark6,'') as Mark6, 
ISNULL(c.ID,'') AS SID, ISNULL(c.Status,'') AS SStatus, 
FORMAT(c.DocDate,'dd/MM/yyyy') AS SDocDate, ISNULL(b.Salesman,'') AS SSalesMan,ISNULL(a.Mark8,'') as Mark8,
ISNULL(SUBSTRING(d.ID,5,LEN(d.ID)),'N/A') as TransID,ISNULL(d.Name,'ไม่ระบุ') as TransName 
FROM dbo.TranHeaders AS a LEFT OUTER JOIN
 dbo.TranHeaders AS c ON SUBSTRING(a.Reference, 2,LEN(a.Reference)) = c.ID LEFT OUTER JOIN
 SFOOT_$ICS.dbo.XCustomers AS b ON a.Customer = b.ID 
LEFT OUTER JOIN [$ICS_Footwear].dbo.Yamcontrols d ON b.Transport=SUBSTRING(d.ID,5,LEN(d.ID)) 
WHERE (a.ID LIKE 'I%') " + wherec + @"
) as t ";
            return sql;
        }
        public static DataTable GetCustomer()
        {
            DataTable dt = new DataTable();
            using (ClsConnectSql db = new ClsConnectSql())
            {
                if (db.isReady() == true)
                {
                    dt = db.RecordSet("select distinct customer,CustomerName from " + ClsData.TransHeaderI("") + " where CustomerName is not null order by CustomerName");
                    dt.TableName = "Customer";
                }
            }
            return dt.Copy();
        }
        public static DataTable GetDeliveryHeader(string tbname,string addwhere)
        {
            DataTable dtHead = new DataTable();
            using (ClsConnectSql db = new ClsConnectSql())
            {
               dtHead= db.RecordSet(@"select * from " + ClsData.TransHeaderI(addwhere) + " order by ID");
               dtHead.TableName = tbname;
            }
            return dtHead.Copy();
        }
        public static DataTable GetDeliveryDetail(string tbname,string addwhere)
        {
            DataTable dtHead = new DataTable();
            using (ClsConnectSql db = new ClsConnectSql())
            {
                dtHead = db.RecordSet(@"select ID+'_'+convert(varchar,Sequence) as ID_Detail,Sequence as ItemNo,Product,ProductName,Remark,VAT,Packs,DQty,DDiscPer,UnitPrice,DAmount,TotalCost from " + ClsData.TransHeaderI_Detail(addwhere) + " order by ID,Sequence");
                dtHead.TableName = tbname;
            }
            return dtHead.Copy();
        }
    }
}
