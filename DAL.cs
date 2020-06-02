using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL
    {
        public class DatabaseHelper : IDisposable
        {
            private string strConnectionString;
            private DbConnection objConnection;
            private DbCommand objCommand;
            private DbProviderFactory objFactory = null;
            private bool boolHandleErrors;
            private string strLastError;
            private bool boolLogError;
            private string strLogFile;

            public DatabaseHelper()
            {
                //object ConfigurationManager = null;
                strConnectionString = ConfigurationManager.ConnectionStrings["xCarrierConnectionString"].ConnectionString;
                objFactory = System.Data.SqlClient.SqlClientFactory.Instance;
                objConnection = objFactory.CreateConnection();
                objCommand = objFactory.CreateCommand();

                objConnection.ConnectionString = strConnectionString;
                objCommand.Connection = objConnection;
            }
            public DatabaseHelper(bool status)
            {
                if (status)
                    strConnectionString = ConfigurationManager.ConnectionStrings["xCarrierConnectionString"].ConnectionString;
                else
                    strConnectionString = ConfigurationManager.AppSettings["companyConnectionString"];
                objFactory = System.Data.SqlClient.SqlClientFactory.Instance;
                objConnection = objFactory.CreateConnection();
                objCommand = objFactory.CreateCommand();

                objConnection.ConnectionString = strConnectionString;
                objCommand.Connection = objConnection;
            }

            public DatabaseHelper(String ERPName, String ConnectionString)
            {
                strConnectionString = ConnectionString;
                if (ERPName == "SAPB1")
                {
                    objFactory = System.Data.SqlClient.SqlClientFactory.Instance;

                }
                else
                {
                    objFactory = System.Data.Odbc.OdbcFactory.Instance;

                }
                objConnection = objFactory.CreateConnection();
                objCommand = objFactory.CreateCommand();

                objConnection.ConnectionString = strConnectionString;
                objCommand.Connection = objConnection;
            }

            //public void LoginDB(string server, string dbName, string userName, string Password)
            //{
            //    Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            //    SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder(objConnection.ConnectionString);
            //    conStringBuilder.InitialCatalog = dbName;
            //    conStringBuilder.DataSource = server;
            //    conStringBuilder.IntegratedSecurity = false;
            //    conStringBuilder.UserID = userName;
            //    conStringBuilder.Password = Password;
            //    webConfigApp.AppSettings.Settings.Add("companyConnectionString", conStringBuilder.ConnectionString);

            //    //webConfigApp.AppSettings("xCarrierConnectionString") = "MultipleActiveResultSets=True;Data Source=" + server + ";Initial Catalog=" + dbName + ";User ID=" + userName + ";Password=" + Password + ";pooling=true;Max Pool Size=1500;Min Pool Size=20;timeout=2880";
            //    webConfigApp.Save();
            //}

            public void SignUpDB()
            {
                SqlConnectionStringBuilder strConn = new SqlConnectionStringBuilder(objConnection.ConnectionString);
                strConn.InitialCatalog = "xCarrierSaS";
                objConnection.ConnectionString = strConn.ConnectionString;
            }

            public bool HandleErrors
            {
                get
                {
                    return boolHandleErrors;
                }
                set
                {
                    boolHandleErrors = value;
                }
            }

            public string LastError
            {
                get
                {
                    return strLastError;
                }
            }

            public bool LogErrors
            {
                get
                {
                    return boolLogError;
                }
                set
                {
                    boolLogError = value;
                }
            }

            public string LogFile
            {
                get
                {
                    return strLogFile;
                }
                set
                {
                    strLogFile = value;
                }
            }

            public int AddParameter(string name, object value)
            {
                DbParameter p = objFactory.CreateParameter();
                p.ParameterName = name;
                p.Value = value;
                return objCommand.Parameters.Add(p);
            }

            public int AddParameter(string name, object value, ParameterDirection direction)
            {
                DbParameter p = objFactory.CreateParameter();
                p.ParameterName = name;
                p.Value = value;
                p.Direction = direction;
                return objCommand.Parameters.Add(p);
            }

            public int AddParameter(string name, object value, ParameterDirection direction, DbType dbType, int intSize)
            {
                DbParameter p = objFactory.CreateParameter();
                p.ParameterName = name;
                p.Value = value;
                p.Direction = direction;
                p.Size = intSize;
                p.DbType = dbType;
                return objCommand.Parameters.Add(p);
            }

            public int AddParameter(DbParameter parameter)
            {
                return objCommand.Parameters.Add(parameter);
            }

            public DbCommand Command
            {
                get
                {
                    return objCommand;
                }
            }

            public void BeginTransaction()
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                objCommand.Transaction = objConnection.BeginTransaction();
            }

            public void CommitTransaction()
            {
                objCommand.Transaction.Commit();
                objConnection.Close();
            }

            public void RollbackTransaction()
            {
                objCommand.Transaction.Rollback();
                objConnection.Close();
            }

            public DataTable ExecuteDataTable(string query, CommandType commandtype)
            {
                return ExecuteDataTable(query, commandtype, ConnectionState.CloseOnExit);
            }
            public DataTable ExecuteDataTable(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                DbDataAdapter adapter = objFactory.CreateDataAdapter();
                objCommand.CommandText = query;
                objCommand.CommandType = commandtype;
                adapter.SelectCommand = objCommand;
                DataTable dt = new DataTable();
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    HandleExceptions(ex);
                }
                finally
                {
                    objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objConnection.State == System.Data.ConnectionState.Open)
                        {
                            objConnection.Close();
                        }
                    }
                }
                return dt;
            }

            public int ExecuteNonQuery(string query)
            {
                return ExecuteNonQuery(query, CommandType.Text, ConnectionState.CloseOnExit);
            }

            public int ExecuteNonQuery(string query, CommandType commandtype)
            {
                return ExecuteNonQuery(query, commandtype, ConnectionState.CloseOnExit);
            }

            public int ExecuteNonQuery(string query, ConnectionState connectionstate)
            {
                return ExecuteNonQuery(query, CommandType.Text, connectionstate);
            }

            public int ExecuteNonQuery(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                objCommand.CommandText = query;
                objCommand.CommandType = commandtype;
                int i = -1;
                try
                {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                    if (objConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    i = objCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    HandleExceptions(ex);
                }
                finally
                {
                    //objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        objConnection.Close();
                    }
                }

                return i;
            }

            public object ExecuteScalar(string query)
            {
                return ExecuteScalar(query, CommandType.Text, ConnectionState.CloseOnExit);
            }

            public object ExecuteScalar(string query, CommandType commandtype)
            {
                return ExecuteScalar(query, commandtype, ConnectionState.CloseOnExit);
            }

            public object ExecuteScalar(string query, ConnectionState connectionstate)
            {
                return ExecuteScalar(query, CommandType.Text, connectionstate);
            }

            public object ExecuteScalar(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                objCommand.CommandText = query;
                objCommand.CommandType = commandtype;
                object o = null;
                try
                {
                    if (objConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    o = objCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    HandleExceptions(ex);
                }
                finally
                {
                    objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        objConnection.Close();
                    }
                }

                return o;
            }

            public DbDataReader ExecuteReader(string query)
            {
                return ExecuteReader(query, CommandType.Text, ConnectionState.CloseOnExit);
            }

            public DbDataReader ExecuteReader(string query, CommandType commandtype)
            {
                return ExecuteReader(query, commandtype, ConnectionState.CloseOnExit);
            }

            public DbDataReader ExecuteReader(string query, ConnectionState connectionstate)
            {
                return ExecuteReader(query, CommandType.Text, connectionstate);
            }

            public DbDataReader ExecuteReader(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                objCommand.CommandText = query;
                objCommand.CommandType = commandtype;
                DbDataReader reader = null;
                try
                {
                    if (objConnection.State == System.Data.ConnectionState.Closed)
                    {
                        objConnection.Open();
                    }
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    else
                    {
                        reader = objCommand.ExecuteReader();
                    }

                }
                catch (Exception ex)
                {
                    HandleExceptions(ex);
                }
                finally
                {
                    objCommand.Parameters.Clear();
                }

                return reader;
            }

            public DataSet ExecuteDataSet(string query)
            {
                return ExecuteDataSet(query, CommandType.Text, ConnectionState.CloseOnExit);
            }

            public DataSet ExecuteDataSet(string query, CommandType commandtype)
            {
                return ExecuteDataSet(query, commandtype, ConnectionState.CloseOnExit);
            }

            public DataSet ExecuteDataSet(string query, ConnectionState connectionstate)
            {
                return ExecuteDataSet(query, CommandType.Text, connectionstate);
            }

            public DataSet ExecuteDataSet(string query, CommandType commandtype, ConnectionState connectionstate)
            {
                DbDataAdapter adapter = objFactory.CreateDataAdapter();
                objCommand.CommandText = query;
                objCommand.CommandType = commandtype;
                adapter.SelectCommand = objCommand;
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    HandleExceptions(ex);
                }
                finally
                {
                    objCommand.Parameters.Clear();
                    if (connectionstate == ConnectionState.CloseOnExit)
                    {
                        if (objConnection.State == System.Data.ConnectionState.Open)
                        {
                            objConnection.Close();
                        }
                    }
                }
                return ds;
            }

            private void HandleExceptions(Exception ex)
            {
                if (LogErrors)
                {
                    WriteToLog(ex.Message);
                }
                if (HandleErrors)
                {
                    strLastError = ex.Message;
                }
                else
                {
                    throw ex;
                }
            }

            private void WriteToLog(string msg)
            {
                StreamWriter writer = File.AppendText(LogFile);
                writer.WriteLine(DateTime.Now.ToString() + " - " + msg);
                writer.Close();
            }

            public void Dispose()
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
            }

            //~DatabaseHelper()
            //{
            //    this.Dispose();
            //}

        }

        public enum ConnectionState
        {
            KeepOpen, CloseOnExit
        }
    }
}
