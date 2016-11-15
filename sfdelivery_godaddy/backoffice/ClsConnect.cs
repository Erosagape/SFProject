using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;

namespace SfDeliverTracking
{
    public class ClsConnectSql : IDisposable
    {
        private string server = "";
        private string user = "";
        private string password = "";
        private string db = "";
        private string err = "";
        private SqlConnection conn;
        public ClsConnectSql(int dbIndex=0)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml("Connect.xml");
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
            SqlDataReader dr = new SqlCommand(sqlcmd, con).ExecuteReader();
            dr.Read();
            return dr;
        }

        public SqlDataAdapter DataAdapter(string sqlcmd,bool Forupdate =false)
        {
            SqlConnection con = Connection();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd, con);
            if (Forupdate == true)
            {
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
            }
            return da;
        }

        public DataTable RecordSet(string sqlcmd)
        {
            sqlcmd = sqlcmd.Replace(@"\r\n", Environment.NewLine);
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
        public bool CompareRow(DataRow row1,DataRow row2,DataTable tb,string skipField="")
        {
            bool pass = true;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                string colname = tb.Columns[i].ColumnName;
                if(skipField.IndexOf(colname)<0)
                {
                    try
                    {
                        bool match = (row1[colname].ToString().Trim() == row2[colname].ToString().Trim());
                        if (match == false)
                        {
                            //พยายามตรวจสอบข้อมูลอีกครั้ง
                            if (CFunction.IsNumeric(row1[colname].ToString().Trim()) && CFunction.IsNumeric(row2[colname].ToString().Trim()))
                            {
                                match = (CFunction.CDouble(row1[colname].ToString().Trim()) == CFunction.CDouble(row2[colname].ToString().Trim()));
                            }
                            else
                            {
                                if (CFunction.IsDate(row1[colname].ToString().Trim()) && CFunction.IsDate(row2[colname].ToString().Trim()))
                                {
                                    match = (CFunction.CDateLong(row1[colname].ToString().Trim()) == CFunction.CDateLong(row2[colname].ToString().Trim()));
                                }

                            }
                        }
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
            }
            return pass;
        }
        public DataTable CompareTable(DataTable tb1, DataTable tb2,bool useSource=true,bool checkNewOnly=false,string fieldlist="")
        {
            DataTable dt = tb2.Clone();
            if(useSource==true)
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
                        if (checkNewOnly == false)
                        {
                            match = CompareRow(row1, row2, tb2,fieldlist);
                            if (match == false)
                            {
                                if (useSource == true)
                                {
                                    dt.ImportRow(row1);
                                }
                                else
                                {
                                    dt.ImportRow(row2);
                                }
                                //if has some field to skip
                                if (fieldlist != "")
                                {
                                    foreach(string str in (fieldlist + ",").Split(','))
                                    {
                                        if(str!="")
                                        {
                                            dt.Rows[dt.Rows.Count - 1][str] = row2[str];
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
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
                SqlCommand cmd = new SqlCommand(sqlcommand, conn);
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
        public string SQLVal(string strin)
        {
            return strin.Replace("'", "''");
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
                    CloseConnection();
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
