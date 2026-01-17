using System;
using System.Data;
using System.Data.SqlClient;
namespace Kztek.Databases
{
    public class SqlServerConnection : IConnection
    {
        private SqlConnection sqlConnection;
        private string serverName = "";
        private string serverPort = "";
        private string databaseName = "";
        private int authentication;
        private string userName = "sa";
        private string password = "";
        private bool enableMARS;
        private int commandTimeout = 0; // second - default
        public event DatabaseConnectionErrorEventHandler DatabaseConnectionErrorEvent;
        public string ServerName
        {
            get
            {
                return this.serverName;
            }
            set
            {
                this.serverName = value;
            }
        }

        public string ServerPort
        {
            get
            {
                return this.serverPort;
            }
            set
            {
                this.serverPort = value;
            }
        }

        public string DatabaseName
        {
            get
            {
                return this.databaseName;
            }
            set
            {
                this.databaseName = value;
            }
        }
        public int Authentication
        {
            get
            {
                return this.authentication;
            }
            set
            {
                this.authentication = value;
            }
        }
        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }
        public bool EnableMARS
        {
            get
            {
                return this.enableMARS;
            }
            set
            {
                this.enableMARS = value;
            }
        }
        public int CommandTimeOut
        {
            get { return this.commandTimeout; }
            set { this.commandTimeout = value; }
        }
        private bool keepConnection = false; // 0 - close connection after execute    1 - keep connection continuous
        public bool KeepConnection
        {
            get { return keepConnection; }
            set { keepConnection = value; }
        }

        public ConnectionState State
        {
            get
            {
                if (keepConnection)
                    return this.sqlConnection.State;

                return ConnectionState.Closed;

            }
        }
        public SqlServerConnection()
        {
        }
        public SqlServerConnection(string serverName, string databaseName)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
        }
        public SqlServerConnection(string serverName, string databaseName, string userName, string password)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            this.userName = userName;
            this.password = password;
        }
        public SqlServerConnection(string serverName, string databaseName, string userName, string password, bool enableMARS)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            this.userName = userName;
            this.password = password;
            this.enableMARS = enableMARS;
        }
        public string GetConnectionString()
        {
            string text;
            if (this.authentication == 0)
            {
                text = string.Concat(new string[]
                {
                    "server=",
                    this.serverName,
                    ";database=",
                    this.databaseName,
                    ";Integrated Security=true"
                });
            }
            else
            {
                text = string.Concat(new string[]
                {
                    "server=",
                    this.serverName,
                    ";database=",
                    this.databaseName,
                    ";uid=",
                    this.userName,
                    ";pwd=",
                    this.password
                });
            }
            if (this.enableMARS)
            {
                text += ";MultipleActiveResultSets=true";
            }
            return text;
        }
        public bool Open()
        {
            if (keepConnection)
            {
                bool result;
                try
                {
                    if (this.sqlConnection == null)
                    {
                        this.sqlConnection = new SqlConnection();
                    }
                    if (this.sqlConnection.State == ConnectionState.Open)
                    {
                        this.sqlConnection.Close();
                    }
                    this.sqlConnection.ConnectionString = this.GetConnectionString();
                    this.sqlConnection.Open();

                    result = true;
                }
                catch (Exception ex)
                {
                    if (this.DatabaseConnectionErrorEvent != null)
                    {
                        this.DatabaseConnectionErrorEvent(ex.ToString());
                    }
                    result = false;
                }
                return result;
            }
            else
                return true;
        }
        public void Close()
        {
            if (keepConnection)
            {
                if (this.sqlConnection != null && this.sqlConnection.State != ConnectionState.Closed)
                {
                    this.sqlConnection.Close();
                }
            }
            GC.Collect();
        }
        public void Dispose()
        {
            if (keepConnection)
            {
                if (this.sqlConnection.State != ConnectionState.Closed)
                {
                    this.sqlConnection.Close();
                    this.sqlConnection.Dispose();
                    this.sqlConnection = null;
                }
            }
            GC.Collect();
        }

        private bool Open_Connection(ref SqlConnection conn)
        {
            bool result;
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = this.GetConnectionString();
                conn.Open();
                result = true;
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent(ex.ToString());
                }
                result = false;
            }
            return result;
        }
        private void Close_Connection(SqlConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        private void Dispose_Connection(SqlConnection conn)
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
                GC.Collect();
            }
        }

        public bool CheckConnectionState()
        {
            SqlConnection conn = new SqlConnection();
            bool result;
            try
            {
                conn.ConnectionString = this.GetConnectionString();
                conn.Open();
                result = true;
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent(ex.ToString());
                }
                result = false;
            }
            finally
            {
                conn.Close();
                conn = null;
                GC.Collect();
            }
            return result;
        }
        public DataTable GetTable(string commandString)
        {
            SqlConnection conn = null;
            DataTable ret = null;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    // Execute
                    if (conn != null)
                    {
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandString, conn);
                        DataSet dataSet = new DataSet();
                        if (sqlDataAdapter != null)
                        {
                            sqlDataAdapter.Fill(dataSet);
                            sqlDataAdapter.Dispose();
                            if (dataSet != null && dataSet.Tables.Count > 0)
                                ret = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get data from table: " + ex.Message + "\n" + commandString);
                }
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return ret;
        }

        public DataTable GetTable(string storedProcedureName, string[] parameters, string[] values, SqlDbType[] sqlDbType)
        {
            SqlConnection conn = null;
            DataTable ret = null;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    // Execute
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = storedProcedureName;
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = parameters[i];
                        sqlParameter.SqlValue = values[i];
                        sqlParameter.SqlDbType = sqlDbType[i];
                        sqlCommand.Parameters.Add(sqlParameter);
                    }

                    if (conn != null)
                    {
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        DataSet dataSet = new DataSet();

                        if (sqlDataAdapter != null)
                        {
                            sqlDataAdapter.Fill(dataSet);
                            sqlDataAdapter.Dispose();
                            if (dataSet != null && dataSet.Tables.Count > 0)
                                ret = dataSet.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get data from procedure: " + ex.Message + "\n" + storedProcedureName);
                }
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return ret;
        }

        public DataSet GetDataSet(string commandString)
        {
            SqlConnection conn = null;
            DataSet ret = null;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    if (conn != null)
                    {
                        // Execute
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandString, conn);
                        DataSet dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        sqlDataAdapter.Dispose();
                        ret = dataSet;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get dataSet fom table: " + ex.Message + "\n" + commandString);
                }
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return ret;
        }
        public SqlDataReader GetReader(string commandString)
        {
            SqlConnection conn = null;
            SqlDataReader ret = null;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    if (conn != null)
                    {
                        // Execute
                        SqlCommand sqlCommand = new SqlCommand(commandString, conn);
                        if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                        ret = sqlCommand.ExecuteReader();
                    }
                }
            }
            catch
            { }
            finally
            {
                Dispose_Connection(conn);
            }
            return ret;
        }
        public SqlDataReader GetSqlDataReaderBySP(string storedProcedureName)
        {
            SqlConnection conn = null;
            SqlDataReader ret = null;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    if (conn != null)
                    {
                        // Execute
                        SqlCommand sqlCommand = new SqlCommand
                        {
                            CommandText = storedProcedureName,
                            Connection = conn,
                            CommandType = CommandType.StoredProcedure
                        };
                        if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                        ret = sqlCommand.ExecuteReader();
                    }
                }
            }
            catch
            { }
            finally
            {
                Dispose_Connection(conn);
            }
            return ret;
        }

        public object GetValue(string commandString)
        {
            SqlConnection conn = null;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    // Execute
                    if (conn != null)
                    {
                        object obj = null;
                        obj = new SqlCommand(commandString, conn).ExecuteScalar();
                        if (obj == DBNull.Value)
                        {
                            obj = null;
                        }
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get value by command. \n" + ex.Message);
                }
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return null;
        }

        public bool ExecuteCommand(string commandString)
        {
            SqlConnection conn = null;
            bool result = false;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    if (conn != null)
                    {
                        // Execute
                        SqlCommand sqlCommand = new SqlCommand(commandString, conn);
                        if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                        sqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute command: " + ex.Message + "\n" + commandString);
                }
                result = false;
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return result;
        }
        public bool ExecuteCommand(string commandString, string parameters, byte[] values)
        {
            SqlConnection conn = null;
            bool result = false;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    if (conn != null)
                    {
                        // Execute
                        SqlCommand sqlCommand = new SqlCommand(commandString, conn);
                        if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                        sqlCommand.Parameters.Add(parameters, SqlDbType.Image);
                        if (values != null)
                        {
                            sqlCommand.Parameters[parameters].Value = values;
                        }
                        else
                        {
                            sqlCommand.Parameters[parameters].Value = DBNull.Value;
                        }
                        sqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute command: " + ex.Message + "\n" + commandString);
                }
                result = false;
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return result;
        }
        public bool ExecuteCommand(string commandString, string parameter1, byte[] value1, string parameter2, byte[] value2)
        {
            SqlConnection conn = null;
            bool result = false;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    if (conn != null)
                    {
                        // Execute
                        SqlCommand sqlCommand = new SqlCommand(commandString, conn);
                        if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                        sqlCommand.Parameters.Add(parameter1, SqlDbType.Image);
                        if (value1 != null)
                        {
                            sqlCommand.Parameters[parameter1].Value = value1;
                        }
                        else
                        {
                            sqlCommand.Parameters[parameter1].Value = DBNull.Value;
                        }
                        sqlCommand.Parameters.Add(parameter2, SqlDbType.Image);
                        if (value2 != null)
                        {
                            sqlCommand.Parameters[parameter2].Value = value2;
                        }
                        else
                        {
                            sqlCommand.Parameters[parameter2].Value = DBNull.Value;
                        }
                        sqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute command: " + ex.Message + "\n" + commandString);
                }
                result = false;
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return result;
        }
        public bool ExecuteCommand(string commandString, string[] parameters, string[] values, SqlDbType[] sqlDbType)
        {
            SqlConnection conn = null;
            bool result = false;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    // Execute
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = commandString;
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = parameters[i];
                        sqlParameter.SqlValue = values[i];
                        sqlParameter.SqlDbType = sqlDbType[i];
                        sqlCommand.Parameters.Add(sqlParameter);
                    }
                    sqlCommand.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute command: " + ex.Message + "\n" + commandString);
                }
                result = false;
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return result;
        }
        public bool ExecuteStoredProcedure(string storedProcedureName, string[] parameters, string[] values, SqlDbType[] sqlDbType)
        {
            SqlConnection conn = null;
            bool result = false;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    // Execute
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = storedProcedureName;
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = parameters[i];
                        sqlParameter.SqlValue = values[i];
                        sqlParameter.SqlDbType = sqlDbType[i];
                        sqlCommand.Parameters.Add(sqlParameter);
                    }
                    sqlCommand.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute store procedure: " + ex.Message + "\n" + storedProcedureName);
                }
                result = false;
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return result;
        }
        public int GetNumberOfRecords(string tableName)
        {
            SqlConnection conn = null;
            int result = 0;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    // Execute
                    SqlCommand sqlCommand = new SqlCommand("select count(*) from " + tableName, conn);
                    if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                    result = (int)sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get number of records from table : " + tableName + "\n" + ex.Message);
                }
            }
            finally
            {
                Dispose();
            }
            return result;
        }
        public int GetNumberOfRecords(string tableName, string conditionString)
        {
            if (conditionString == null || conditionString == "" || conditionString.Trim().ToLower() == "where")
            {
                return this.GetNumberOfRecords(tableName);
            }

            SqlConnection conn = null;
            int result = 0;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    SqlCommand sqlCommand = new SqlCommand("select count(*) from " + tableName + " where " + conditionString, conn);
                    if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                    result = (int)sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get number of records from table : " + tableName + "\n" + ex.Message);
                }
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return result;
        }
        public DateTime GetServerDateTime()
        {
            SqlConnection conn = null;
            DateTime result = DateTime.Now;
            try
            {
                // Open Connection
                if (Open_Connection(ref conn))
                {
                    SqlCommand sqlCommand = new SqlCommand("select getdate()", conn);
                    if (commandTimeout > 0) sqlCommand.CommandTimeout = commandTimeout;
                    result = (DateTime)sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get current DateTime from Server\n" + ex.Message);
                }
            }
            finally
            {
                Dispose_Connection(conn);
            }
            return result;
        }
        public bool BuckCopy(DataTable tableSource, string tableName_Destination)
        {
            bool ret = false;
            try
            {
                DataTable dt = GetTable("Select top 1 * from " + tableName_Destination);

                if (dt != null && dt.Rows.Count > 0)
                {

                    using (System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString()))
                    {
                        bulkCopy.DestinationTableName = tableName_Destination;

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {    //check if destination Column Exists in Source table
                            if (tableSource.Columns.Contains(dt.Columns[i].ToString()))//contain method is not case sensitive
                            {
                                int sourceColumnIndex = tableSource.Columns.IndexOf(dt.Columns[i].ToString());//Once column matched get its index
                                bulkCopy.ColumnMappings.Add(tableSource.Columns[sourceColumnIndex].ToString(), tableSource.Columns[sourceColumnIndex].ToString());//give coluns name of source table rather then destination table so that it would avoid case sensitivity
                            }

                        }
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(tableSource);
                        System.Threading.Thread.Sleep(1);
                        bulkCopy.Close();
                        ret = true;
                    }
                    dt.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while bulkCopy to " + tableName_Destination + ": " + ex.Message);
                }
            }
            return ret;
        }
    }
}
