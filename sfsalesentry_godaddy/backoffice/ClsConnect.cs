using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Reflection;
namespace shopsales_tools
{
    public class MonthLOV
    {
        //<summary>
        //class for display month in any language
        //</summary>
        public string MonthID { get; set; } //id for month
        public string MonthNameEN { get; set; } //name Eng
        public string MonthNameTH { get; set; } // name th
    }
    public class ClsConnectSql : IDisposable
    {
        //<summary>
        //class for connect with databases
        //</summary>
        private string server = "";
        private string user = "";
        private string password = "";
        private string db = "";
        private string err = "";
        private string path = Program.StartupPath+@"\\";
        private SqlConnection conn;
        public ClsConnectSql(int dbIndex=0)
        {
            try
            {
                DataSet ds = new DataSet();
                
                ds.ReadXml(path + "Connect.xml");
                server = ds.Tables[0].Rows[dbIndex]["Server"].ToString();
                user = ds.Tables[0].Rows[dbIndex]["User"].ToString();
                password = ds.Tables[0].Rows[dbIndex]["Password"].ToString();
                db = ds.Tables[0].Rows[dbIndex]["Database"].ToString();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

        }
        public string ErrorMessage()
        {
            return err;
        }
        public string ConnectionString()
        {
            string strconn = @"server=" + server + @";user id=" + user + @";password=" + password + @";database=" + db;
            return strconn;
        }
        public SqlConnection Connection()
        {
            err = "";
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }            
            conn= new SqlConnection(ConnectionString());
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return conn;
        }
        public void CloseConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            catch
            {

            }
        }
        public bool isReady()
        {
            if (Connection().State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public SqlDataReader DataReader(string sqlcmd)
        {
            SqlConnection con = Connection();
            string sql = sqlcmd;
            SqlDataReader dr = new SqlCommand(sql, con).ExecuteReader();
            dr.Read();
            return dr;
        }

        public SqlDataAdapter DataAdapter(string sqlcmd,bool Forupdate =false)
        {
            SqlConnection con = Connection();
            string sql = sqlcmd;
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            if (Forupdate == true)
            {
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
            }
            return da;
        }

        public DataTable RecordSet(string sqlcmd)
        {
            SqlDataAdapter da = DataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable RecordSet(SqlDataAdapter da)
        {
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string FindValue(string sqlcmd,string defaultval="")
        {
            SqlDataReader rd = DataReader(sqlcmd);
            if(rd.HasRows ==true)
            {
                defaultval= rd[0].ToString();
            }
            rd.Close();
            return defaultval;
        }
        public DataRow FindRow(DataTable dt,string sqlCliteria)
        {
            DataRow r = dt.NewRow();
            DataRow[] dr = dt.Select(sqlCliteria);
            foreach (DataRow row in dr)
            {
                r = row;
            }
            return r;
        }
        public bool CompareRow(DataRow row1, DataRow row2, DataTable tb)
        {
            bool pass = true;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                string colname = tb.Columns[i].ColumnName;
                try
                {
                    bool match = row1[colname].ToString().Trim() == row2[colname].ToString().Trim();
                    if (match == false)
                    {
                        pass = false;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
            }
            return pass;
        }
        public DataTable CompareTable(DataTable tb1, DataTable tb2, bool useSource = true)
        {
            DataTable dt = tb2.Clone();
            if (useSource == true)
            {
                dt = tb1.Clone();
            }
            foreach (DataRow row1 in tb1.Rows)
            {
                string keycol = tb1.Columns[0].ColumnName;
                string keyval = row1[keycol].ToString();
                bool found = false;
                try
                {
                    DataRow[] rows = tb2.Select(keycol + "='" + keyval + "'");
                    bool match = false;
                    foreach (DataRow row2 in rows)
                    {
                        found = true;
                        if (useSource == true)
                        {
                            match = CompareRow(row1, row2, tb1);
                            if (match == false)
                            {
                                dt.ImportRow(row1);
                                break;
                            }
                        }
                        else
                        {
                            match = CompareRow(row2, row1, tb2);
                            if (match == false)
                            {
                                dt.ImportRow(row2);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
                if (found == false)
                {
                    dt.ImportRow(row1);
                }
            }
            return dt;
        }
        public bool UpdateRow(string sqlcmd, DataRow rowdata)
        {
            bool success = false;
            try
            {
                SqlDataAdapter da = DataAdapter(sqlcmd, true);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow rowsrc = dt.NewRow();
                if (dt.Rows.Count > 0)
                {
                    rowsrc = dt.Rows[0];
                }
                else
                {
                    rowsrc[0] = rowdata[0];
                }    
                if(CompareRow(rowsrc,rowdata,dt)==false)
                {
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        try
                        {
                            rowsrc[i] = rowdata[i];
                        }
                        catch
                        {

                        }
                    }
                    if (rowsrc.RowState == DataRowState.Detached) dt.Rows.Add(rowsrc);
                    da.Update(dt);
                }
                success = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return success; 
        }
        public bool Execute(string sqlcommand)
        {
            try
            {
                err = "";
                string sql = sqlcommand;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
        public DataTable LOV_Month()
        {
            List<MonthLOV> Months = new List<MonthLOV>();
            Months.Add(new MonthLOV() { MonthID = "", MonthNameEN = "All", MonthNameTH = "{ทั้งหมด}" });
            Months.Add(new MonthLOV() { MonthID = "01", MonthNameEN = "January", MonthNameTH = "มกราคม" });
            Months.Add(new MonthLOV() { MonthID = "02", MonthNameEN = "Febuary", MonthNameTH = "กุมภาพันธ์" });
            Months.Add(new MonthLOV() { MonthID = "03", MonthNameEN = "March", MonthNameTH = "มีนาคม" });
            Months.Add(new MonthLOV() { MonthID = "04", MonthNameEN = "April", MonthNameTH = "เมษายน" });
            Months.Add(new MonthLOV() { MonthID = "05", MonthNameEN = "May", MonthNameTH = "พฤษภาคม" });
            Months.Add(new MonthLOV() { MonthID = "06", MonthNameEN = "June", MonthNameTH = "มิถุนายน" });
            Months.Add(new MonthLOV() { MonthID = "07", MonthNameEN = "July", MonthNameTH = "กรกฏาคม" });
            Months.Add(new MonthLOV() { MonthID = "08", MonthNameEN = "August", MonthNameTH = "สิงหาคม" });
            Months.Add(new MonthLOV() { MonthID = "09", MonthNameEN = "September", MonthNameTH = "กันยายน" });
            Months.Add(new MonthLOV() { MonthID = "10", MonthNameEN = "October", MonthNameTH = "ตุลาคม" });
            Months.Add(new MonthLOV() { MonthID = "11", MonthNameEN = "November", MonthNameTH = "พฤษจิกายน" });
            Months.Add(new MonthLOV() { MonthID = "12", MonthNameEN = "December", MonthNameTH = "ธันวาคม" });
            return ToDataTable<MonthLOV>(Months);
        }
        public DataTable LOV_Customer(bool AddallOption = false)
        {
            DataTable dt = RecordSet(@"select [Name] as CustName,[OID] from xCustomer order by [Name]").Copy();
            if(AddallOption==true)
            {
                DataRow dr = dt.NewRow();
                dr[0]="ทั้งหมด";
                dr[1]= "0";
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }
        public DataTable LOV_ShoeType()
        {
            return RecordSet(@"select STId,STName from ShoeType order by STName");
        }
        public DataTable LOV_Model()
        {
            return RecordSet(@"select SMId,[Name] as SMName,SSid,STid,ProdCatId from ShoeModel order by [Name]");
        }
        public DataTable LOV_Color()
        {
            return RecordSet(@"select ColId,ColNameInit,ColNameTh,ColNameEng from ShoeColor order by [ColNameTh]");
        }
        public DataTable LOV_Size()
        {
            return RecordSet(@"select SSId,SSName,Minsize,MaxSize,SSName+' ('+Convert(varchar,MinSize)+'-'+Convert(varchar,MaxSize)+')' as SizeGroup from ShoeSize order by SSName");
        }
        public DataTable LOV_ProductKind()
        {
            return RecordSet(@"select ProdKindId,ProdKindName from ProductKind order by ProdKindName");
        }
        public DataTable LOV_ProductCategory()
        {
            return RecordSet(@"select ProdCatId,ProdCatName from ProductCategory order by ProdCatName");
        }
        public DataTable LOV_ProductGroup()
        {
            return RecordSet(@"select ProdGroupId,ProdGroupName from ProductGroup order by ProdGroupName");
        }
        public DataTable LOV_Mold()
        {
            return RecordSet(@"select Moldid,MoldName from ShoeMold");
        }
        public DataTable LOV_SaleType()
        {
            return RecordSet(@"select [Name] as SaleType,[OID] from SaleType");
        }
        public string SQLVal(string strin)
        {
            return strin.Replace("'", "''");
        }
        private DataTable ToDataTable<TSource>(IList<TSource> data)
        {
            DataTable dataTable = new DataTable(typeof(TSource).Name);
            PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (conn.State == ConnectionState.Open) conn.Close();
                    conn.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ClsConnectSql() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
