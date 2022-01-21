using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class SqlHelper
    {
        public static string ConnString = Settings.GetConnectionString("MainConnection").ToString();

        public static bool IsConnect(string myconn)
        {
            bool result = false;
            try
            {
                SqlConnection myConnection = new SqlConnection(myconn);
                myConnection.Open();
                myConnection.Close();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static void ConvertEmptyValuesToDBNull(Hashtable values)
        {
            List<object> keysToDbNull = new List<object>();

            foreach (DictionaryEntry entry in values)
            {
                if (entry.Value == null || (entry.Value is String && String.IsNullOrEmpty((String)entry.Value)))
                {
                    keysToDbNull.Add(entry.Key);
                }
            }

            foreach (object key in keysToDbNull)
            {
                values[key] = DBNull.Value;
            }
        }
        public static void ConvertEmptyValuesToDBNull(OrderedDictionary values)
        {
            List<object> keysToDbNull = new List<object>();

            foreach (DictionaryEntry entry in values)
            {
                if (entry.Value == null || (entry.Value is String && String.IsNullOrEmpty((String)entry.Value)))
                {
                    keysToDbNull.Add(entry.Key);
                }
            }

            foreach (object key in keysToDbNull)
            {
                values[key] = DBNull.Value;
            }
        }

        public static void RunSqlTransaction(string commandText)
        {
            RunSqlTransaction(commandText, ConnString);
        }
        public static void RunSqlTransaction(string commandText, string myconn)
        {
            SqlConnection myConnection = new SqlConnection(myconn);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = commandText;
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        throw new Exception("An exception of type " + ex.GetType() + " was encountered while attempting to roll back the transaction.", ex);
                    }
                }
                throw new Exception("An exception of type " + e.GetType() + " was encountered while transaction.", e);
            }
            finally
            {
                myConnection.Close();
            }
        }
        public static void ExecuteNonQuery(string commandText)
        {
            ExecuteNonQuery(commandText, ConnString);
        }
        public static void ExecuteNonQuery(string commandText, string myconn)
        {
            SqlConnection myConnection = new SqlConnection(myconn);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = commandText;
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        throw new Exception("An exception of type " + ex.GetType() + " was encountered while attempting to roll back the transaction.", ex);
                    }
                }
                throw new Exception("An exception of type " + e.GetType() + " was encountered while transaction.", e);
            }
            finally
            {
                myConnection.Close();
            }
        }
        public static void ExecuteNonQuery(string commandText, Hashtable parameters)
        {
            ExecuteNonQuery(commandText, parameters, ConnString);
        }
        public static void ExecuteNonQuery(string commandText, Hashtable parameters, string myconn)
        {
            SqlConnection myConnection = new SqlConnection(myconn);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = commandText;
                if (parameters != null)
                {
                    foreach (DictionaryEntry entry in parameters)
                    {
                        myCommand.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                    }
                }
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        throw new Exception("An exception of type " + ex.GetType() + " was encountered while attempting to roll back the transaction.", ex);
                    }
                }
                throw new Exception("An exception of type " + e.GetType() + " was encountered while transaction.", e);
            }
            finally
            {
                myConnection.Close();
            }
        }

        public static void ExecuteNonQuery(string commandText, OrderedDictionary parameters)
        {
            ExecuteNonQuery(commandText, parameters, ConnString);
        }
        public static void ExecuteNonQuery(string commandText, OrderedDictionary parameters, string myconn)
        {
            SqlConnection myConnection = new SqlConnection(myconn);
            myConnection.Open();

            SqlCommand myCommand = myConnection.CreateCommand();
            SqlTransaction myTrans;

            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = commandText;
                if (parameters != null)
                {
                    foreach (DictionaryEntry entry in parameters)
                    {
                        myCommand.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                    }
                }
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        throw new Exception("An exception of type " + ex.GetType() + " was encountered while attempting to roll back the transaction.", ex);
                    }
                }
                throw new Exception("An exception of type " + e.GetType() + " was encountered while transaction.", e);
            }
            finally
            {
                myConnection.Close();
            }
        }

        public static DataTable GetDataTable(string query)
        {
            return GetDataTable(query, ConnString);
        }
        public static DataTable GetDataTable(string query, string myconn)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return GetDataTable(query, parameters, myconn);
        }
        public static DataTable GetDataTable(string query, OrderedDictionary parameters)
        {
            return GetDataTable(query, parameters, ConnString);
        }
        public static DataTable GetDataTable(string query, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    adapter.SelectCommand.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }

            DataTable myDataTable = new DataTable();

            conn.Open();
            try
            {
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }

            return myDataTable;
        }

        public static int ExecuteScalarInt(string commandText)
        {
            OrderedDictionary parameter = new OrderedDictionary();
            return ExecuteScalarInt(commandText, parameter, ConnString);
        }
        public static int ExecuteScalarInt(string commandText, string myconn)
        {
            OrderedDictionary parameter = new OrderedDictionary();
            return ExecuteScalarInt(commandText, parameter, myconn);
        }
        public static int ExecuteScalarInt(string commandText, OrderedDictionary parameter)
        {
            return ExecuteScalarInt(commandText, parameter, ConnString);
        }
        public static int ExecuteScalarInt(string commandText, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlCommand command = new SqlCommand(commandText, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }

            int result;
            conn.Open();
            try
            {
                result = int.Parse(command.ExecuteScalar().ToString());
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static Int64 ExecuteScalarInt64(string commandText)
        {
            OrderedDictionary parameter = new OrderedDictionary();
            return ExecuteScalarInt64(commandText, parameter, ConnString);
        }
        public static Int64 ExecuteScalarInt64(string commandText, string myconn)
        {
            OrderedDictionary parameter = new OrderedDictionary();
            return ExecuteScalarInt64(commandText, parameter, myconn);
        }
        public static Int64 ExecuteScalarInt64(string commandText, OrderedDictionary parameter)
        {
            return ExecuteScalarInt64(commandText, parameter, ConnString);
        }
        public static Int64 ExecuteScalarInt64(string commandText, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlCommand command = new SqlCommand(commandText, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }

            Int64 result ;
            conn.Open();
            try
            {
                var obj = command.ExecuteScalar();
                result = Int64.Parse(obj.ToString());
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        public static string ExecuteScalarString(string commandText)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalarString(commandText, parameters, ConnString);
        }
        public static string ExecuteScalarString(string commandText, string myconn)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalarString(commandText, parameters, myconn);
        }
        public static string ExecuteScalarString(string commandText, OrderedDictionary parameters)
        {
            return ExecuteScalarString(commandText, parameters, ConnString);
        }
        public static string ExecuteScalarString(string commandText, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlCommand command = new SqlCommand(commandText, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }
            string result="";
            conn.Open();
            try
            {
                var obj = command.ExecuteScalar();
                if (obj != null) {
                    result = obj.ToString();
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static DateTime ExecuteScalarDateTime(string commandText)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalarDateTime(commandText, parameters, ConnString);
        }
        public static DateTime ExecuteScalarDateTime(string commandText, string myconn)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalarDateTime(commandText, parameters, myconn);
        }
        public static DateTime ExecuteScalarDateTime(string commandText, OrderedDictionary parameters)
        {
            return ExecuteScalarDateTime(commandText, parameters, ConnString);
        }
        public static DateTime ExecuteScalarDateTime(string commandText, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlCommand command = new SqlCommand(commandText, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }
            DateTime result;
            conn.Open();
            try
            {
                result = (DateTime)command.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static decimal ExecuteScalarDecimal(string commandText)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalarDecimal(commandText, parameters, ConnString);
        }
        public static decimal ExecuteScalarDecimal(string commandText, string myconn)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalarDecimal(commandText, parameters, myconn);
        }
        public static decimal ExecuteScalarDecimal(string commandText, OrderedDictionary parameters)
        {
            return ExecuteScalarDecimal(commandText, parameters, ConnString);
        }
        public static decimal ExecuteScalarDecimal(string commandText, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlCommand command = new SqlCommand(commandText, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }
            decimal result;
            conn.Open();
            try
            {
                result = (decimal)command.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }



        public static object ExecuteScalar(string commandText)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalar(commandText, parameters, ConnString);
        }
        public static object ExecuteScalar(string commandText, string myconn)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return ExecuteScalar(commandText, parameters, myconn);
        }
        public static object ExecuteScalar(string commandText, OrderedDictionary parameters)
        {
            return ExecuteScalar(commandText, parameters, ConnString);
        }
        public static object ExecuteScalar(string commandText, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlCommand command = new SqlCommand(commandText, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }
            object result;
            conn.Open();
            try
            {
                result = command.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static string GetObjectId(string tableName, string pkColumnName, string uniqueColumnName, string listColumnName, string listColumnValue)
        {
            string[] pkColumn = pkColumnName.Split(',');
            string idColumn = pkColumn[pkColumn.Length - 1];

            string[] uniqueColumn = uniqueColumnName.Split(',');

            string[] listColumn = listColumnName.Split(',');
            string[] listVaues = listColumnValue.Split(',');
            OrderedDictionary newData = new OrderedDictionary();
            for (int i = 0; i < listColumn.Length; i++)
            {
                if (listColumn[i] != pkColumnName)
                {
                    newData[listColumn[i]] = listVaues[i];
                }
            }

            string whereUnique = "";
            for (int i = 0; i < uniqueColumn.Length; i++)
            {
                whereUnique += whereUnique != "" ? " AND " : "";
                whereUnique += " " + uniqueColumn[i] + "=@" + uniqueColumn[i] + " ";
            }
            if (whereUnique != "")
            {
                whereUnique = " WHERE " + whereUnique;
            }
            //================
            string whereFk = "";
            if (pkColumn.Length > 1)
            {
                for (int i = 0; i < pkColumn.Length - 1; i++)
                {
                    whereFk += whereFk != "" ? " AND " : "";
                    whereFk += " " + pkColumn[i] + "=@" + pkColumn[i] + " ";
                }
            }
            if (whereFk != "")
            {
                whereFk = " WHERE " + whereFk;
            }
            string sqlInsert = "DECLARE @" + idColumn + " int;";
            sqlInsert += "IF EXISTS(select " + idColumn + " from " + tableName + " " + whereUnique;
            sqlInsert += ") ";
            sqlInsert += " BEGIN ";
            sqlInsert += "      SELECT  @" + idColumn + "=" + idColumn + " from " + tableName + " " + whereUnique + ";";
            sqlInsert += " END ";
            sqlInsert += " ELSE ";
            sqlInsert += " BEGIN ";
            sqlInsert += "      SELECT @" + idColumn + "=ISNULL(max(" + idColumn + "),0) +1 FROM " + tableName + " " + whereFk;
            sqlInsert += "      INSERT INTO " + tableName + "(" + listColumnName + ") VALUES (@" + listColumnName.Replace(",", ",@") + ")";
            sqlInsert += " ";
            sqlInsert += " ";
            sqlInsert += " END ";
            sqlInsert += " SELECT  @" + idColumn + ";";
            int idHasil = ExecuteScalarInt(sqlInsert, newData);
            return idHasil.ToString();
        }


        public static DataSet GetDataSetFromSP(string commandText)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return GetDataSetFromSP(commandText, parameters, ConnString);
        }
        public static DataSet GetDataSetFromSP(string commandText, string myconn)
        {
            OrderedDictionary parameters = new OrderedDictionary();
            return GetDataSetFromSP(commandText, parameters, myconn);
        }
        public static DataSet GetDataSetFromSP(string commandText, OrderedDictionary parameters)
        {
            return GetDataSetFromSP(commandText, parameters, ConnString);
        }

        public static DataSet GetDataSetFromSP(string query, OrderedDictionary parameters, string myconn)
        {
            SqlConnection conn = new SqlConnection(myconn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    adapter.SelectCommand.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }

            DataSet myDataSet = new DataSet();

            conn.Open();
            try
            {
                adapter.Fill(myDataSet);
            }
            finally
            {
                conn.Close();
            }

            return myDataSet;
        }
    }
}
