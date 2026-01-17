using System;
using System.Data;
using System.Data.OleDb;

namespace Kztek.Databases
{
    public class AccessConnection : IConnection
    {
        public OleDbConnection conn;
        private string serverName = "Microsoft.Jet.OLEDB.4.0";
        private string serverPort = "";
        private string databaseName = "";
        private int authentication;
        private string userName = "sa";
        private string password = "";
        private bool enableMARS;
        private int commandTimeout = 0;
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
        public ConnectionState State
        {
            get
            {
                return this.conn.State;
            }
        }
        public AccessConnection()
        {
            this.conn = new OleDbConnection();
        }
        public AccessConnection(string databaseName)
        {
            this.conn = new OleDbConnection();
            this.databaseName = databaseName;
        }
        public AccessConnection(string databaseName, string password)
        {
            this.conn = new OleDbConnection();
            this.databaseName = databaseName;
            this.password = password;
        }
        public string GetConnectionString()
        {
            string result;
            if (this.authentication == 0)
            {
                result = "Provider=" + this.serverName + ";Data Source='" + this.databaseName;
            }
            else
            {
                result = string.Concat(new string[]
                {
                    "Provider=",
                    this.serverName,
                    ";Data Source='",
                    this.databaseName,
                    "';Persist Security Info=True;Jet OLEDB:Database Password=",
                    this.password
                });
            }
            return result;
        }
        public bool Open()
        {
            bool result;
            try
            {
                if (this.conn.State == ConnectionState.Open)
                {
                    this.conn.Close();
                }
                this.conn.ConnectionString = this.GetConnectionString();
                Console.WriteLine(this.conn.ConnectionString);
                this.conn.Open();
                result = true;
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent(ex.Message);
                }
                result = false;
            }
            return result;
        }
        public void Close()
        {
            if (this.conn.State == ConnectionState.Open)
            {
                this.conn.Close();
                this.conn.Dispose();
            }
        }
        public bool CheckConnectionState()
        {
            bool ret = false;
            if (this.conn.State == ConnectionState.Open)
                return true;

            if (Open())
            {
                ret = true;
                Close();
            }
            return ret;
        }
        public DataTable GetTable(string commandString)
        {
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(commandString, this.conn);
            DataSet dataSet = new DataSet();
            try
            {
                oleDbDataAdapter.Fill(dataSet);
                oleDbDataAdapter.Dispose();
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get data fom table: " + ex.Message + "\n" + commandString);
                }
            }
            return null;
        }

        public DataTable GetTable(string storedProcedureName, string[] parameters, string[] values, SqlDbType[] sqlDbType)
        {
            return null;
        }

        public DataSet GetDataSet(string commandString)
        {
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(commandString, this.conn);
            DataSet dataSet = new DataSet();
            try
            {
                oleDbDataAdapter.Fill(dataSet);
                oleDbDataAdapter.Dispose();
                return dataSet;
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while get dataSet fom table: " + ex.Message + "\n" + commandString);
                }
            }
            return null;
        }
        public OleDbDataReader GetReader(string commandString)
        {
            OleDbCommand oleDbCommand = new OleDbCommand(commandString, this.conn);
            return oleDbCommand.ExecuteReader();
        }
        public bool ExecuteCommand(string commandString)
        {
            OleDbCommand oleDbCommand = new OleDbCommand(commandString, this.conn);
            bool result;
            try
            {
                oleDbCommand.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute command: " + ex.Message + "\n" + commandString);
                }
                result = false;
            }
            return result;
        }
        public bool ExecuteCommand(string commandString, string parameters, byte[] values)
        {
            OleDbCommand oleDbCommand = new(commandString, this.conn);
            oleDbCommand.Parameters.Add(parameters, OleDbType.Binary);
            if (values != null)
            {
                oleDbCommand.Parameters[parameters].Value = values;
            }
            else
            {
                oleDbCommand.Parameters[parameters].Value = DBNull.Value;
            }
            bool result;
            try
            {
                oleDbCommand.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute command: " + ex.Message + "\n" + commandString);
                }
                result = false;
            }
            return result;
        }
        public bool ExecuteCommand(string commandString, string parameter1, byte[] value1, string parameter2, byte[] value2)
        {
            OleDbCommand oleDbCommand = new OleDbCommand(commandString, this.conn);
            oleDbCommand.Parameters.Add(parameter1, OleDbType.Binary);
            if (value1 != null)
            {
                oleDbCommand.Parameters[parameter1].Value = value1;
            }
            else
            {
                oleDbCommand.Parameters[parameter1].Value = DBNull.Value;
            }
            oleDbCommand.Parameters.Add(parameter2, OleDbType.Binary);
            if (value2 != null)
            {
                oleDbCommand.Parameters[parameter2].Value = value2;
            }
            else
            {
                oleDbCommand.Parameters[parameter2].Value = DBNull.Value;
            }
            bool result;
            try
            {
                oleDbCommand.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                if (this.DatabaseConnectionErrorEvent != null)
                {
                    this.DatabaseConnectionErrorEvent("Error while excute command: " + ex.Message + "\n" + commandString);
                }
                result = false;
            }
            return result;
        }
        public bool ExecuteCommand(string commandString, string[] parameters, string[] values, SqlDbType[] sqlDbType)
        {
            bool result = false;
            return result;
        }

        public bool ExecuteStoredProcedure(string storedProcedureName, string[] parameters, string[] values, SqlDbType[] sqlDbType)
        {
            bool result = false; ;
            return result;
        }

        public int GetNumberOfRecords(string tableName)
        {
            return 0;
        }
        public int GetNumberOfRecords(string tableName, string conditionString)
        {
            return 0;
        }
        public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }

        public bool BuckCopy(DataTable tableSource, string tableName_Destination)
        {
            return false;
        }

        public object GetValue(string commandString)
        {
            return null;
        }
    }
}
