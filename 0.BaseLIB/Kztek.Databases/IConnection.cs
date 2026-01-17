using System;
using System.Data;

namespace Kztek.Databases
{
    public interface IConnection
    {
        event DatabaseConnectionErrorEventHandler DatabaseConnectionErrorEvent;
        //event DatabaseConnectionUpdateEventHandler DatabaseConnectionUpdateEvent;

        string ServerName
        {
            get;
            set;
        }

        string ServerPort
        {
            get;
            set;
        }

        string DatabaseName
        {
            get;
            set;
        }
        int Authentication
        {
            get;
            set;
        }
        string UserName
        {
            get;
            set;
        }
        string Password
        {
            get;
            set;
        }
        bool EnableMARS
        {
            get;
            set;
        }
        int CommandTimeOut
        {
            get;
            set;
        }
        ConnectionState State
        {
            get;
        }
        string GetConnectionString();
        bool Open();
        void Close();
        bool CheckConnectionState();
        DataTable GetTable(string commandString);
        DataTable GetTable(string storedProcedureName, string[] parameters, string[] values, SqlDbType[] sqlDbType);
        DataSet GetDataSet(string commandString);
        bool ExecuteCommand(string commandString);
        bool ExecuteCommand(string commandString, string parameters, byte[] values);
        bool ExecuteCommand(string commandString, string parameter1, byte[] value1, string parameter2, byte[] value2);
        bool ExecuteCommand(string commandString, string[] parameters, string[] values, SqlDbType[] sqlDbType);
        bool ExecuteStoredProcedure(string storedProcedureName, string[] parameters, string[] values, SqlDbType[] sqlDbType);
        int GetNumberOfRecords(string tableName);
        int GetNumberOfRecords(string tableName, string conditionString);
        DateTime GetServerDateTime();
        bool BuckCopy(DataTable tableSource, string tableName_Destination);
        object GetValue(string commandString);

    }
}