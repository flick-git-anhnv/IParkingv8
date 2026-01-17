using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Futech.Tools
{
    public class MDBExcel
    {
        private string FilePath = "";
        //private OleDbConnection ExcelConnection = null;
        private OleDbConnection excelConnection = null; 
       
        //contructor
        public MDBExcel()
        {
 
        }
        public MDBExcel(string filepath)
        {
            FilePath = filepath;
        }

        //Mo ket noi
        public bool Open()
        {
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;
                    Data Source=" + FilePath + @";Extended Properties=""Excel 12.0;HDR=YES;""";

//                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;
//                    Data Source=" + FilePath + @";Extended Properties=""Excel 12.0;HDR=NO;""";

                // if you don't want to show the header row (first row)
                // use 'HDR=NO' in the string
                excelConnection = new OleDbConnection(connectionString);
                excelConnection.Open(); // This code will open excel file.
                if (excelConnection.State == ConnectionState.Open)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return false;
        }

        //Kiem tra trang thai ket noi
        public bool State()
        {
            try
            {
                if (excelConnection != null)
                {
                    if (excelConnection.State == ConnectionState.Open)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //FillData
        public DataTable FillData(string commandString)
        {
            if (Open())
            {
                try
                {
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(commandString, excelConnection);
                    DataSet dtset = new DataSet();
                    dataAdapter.Fill(dtset);
                    dataAdapter.Dispose();
                    excelConnection.Close();
                    excelConnection.Dispose();
                    return dtset.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            return null;

        }

        public bool ExcuteCommand(string commandString)
        {
            if (Open())
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(commandString, excelConnection);
                    command.ExecuteNonQuery();
                    //excelConnection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
               
            }
            return false;
        }

        public void Close()
        {
            excelConnection.Close();
            excelConnection.Dispose();
        }

        //Get ExcelsheetName
        public String[] GetExcelSheetNames()
        {

            System.Data.DataTable dt = null;

            try
            {
                if (excelConnection == null) Open();

                // Get the data table containing the schema
                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString().Trim();
                    excelSheets[i] = strSheetTableName.Substring(0, strSheetTableName.Length - 1);
                    i++;
                }


                return excelSheets;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                //// Clean up.
                //if (this.KeepConnectionOpen == false)
                //{
                //    this.Close();
                //}
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        public string ColName(int intCol)
        {
            string sColName = "";
            if (intCol < 26)
                sColName = Convert.ToString(Convert.ToChar((Convert.ToByte((char)'A') + intCol)));
            else
            {
                int intFirst = ((int)intCol / 26);
                int intSecond = ((int)intCol % 26);
                sColName = Convert.ToString(Convert.ToByte((char)'A') + intFirst);
                sColName += Convert.ToString(Convert.ToByte((char)'A') + intSecond);
            }
            return sColName;
        }

        public int ColNumber(string strCol)
        {
            strCol = strCol.ToUpper();
            int intColNumber = 0;
            if (strCol.Length > 1)
            {
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[1]) - 65);
                intColNumber += Convert.ToInt16(Convert.ToByte(strCol[1]) - 64) * 26;
            }
            else
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[0]) - 65);
            return intColNumber;
        }

       
    }
}
