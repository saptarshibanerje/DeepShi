using DeepShiShared.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeepShiEntityContext.Helper
{
    public sealed class DBHelper
    {
        private static DBHelper helperObject = null;
        private static Mutex mutaxObject = new Mutex();
        private string strconnectionString = string.Empty;
        private SqlConnection sqlCon;

        public DBHelper(string strconn)
        {
            strconnectionString = strconn;
        }

        public static DBHelper GetInstance()
        {
            return GetInstance(string.Empty);
        }

        public static DBHelper GetInstance(string strconn)
        {
            try
            {
                mutaxObject.WaitOne();
                if (helperObject == null || !helperObject.strconnectionString.Equals(strconn))
                {
                    if (String.IsNullOrEmpty(strconn))
                        helperObject = new DBHelper("");
                    else
                        helperObject = new DBHelper(strconn);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                mutaxObject.ReleaseMutex();
            }

            return helperObject;
        }
        public void OpenConnection()
        {
            if (sqlCon == null)
                sqlCon = new SqlConnection(strconnectionString);
            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();
        }

        /// <summary>
        /// Closes the connection and release the connection object.
        /// </summary>
        public void CloseConnection()
        {
            if (sqlCon != null && sqlCon.State != ConnectionState.Closed)
                sqlCon.Close();
            sqlCon = null;
        }

        private string Decode(string cipherText)
        {
            return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(cipherText));
        }

        /// <summary>
        /// Method used to execute a raw sql dml query statement.
        /// </summary>
        /// <param name="dmlStatement">The dml query statement string.</param>
        /// <returns>Returns the number of rows affected by the executed query.</returns>
        /// <exception cref="System.Exception">
        /// Thrown when there is any error in a database related operation.
        /// </exception>
        public int ExecuteNonQuery(string dmlStatement)
        {
            int rowsAffected = -1;
            SqlConnection connectionObject = null;

            try
            {
                connectionObject = new SqlConnection(strconnectionString);
                SqlCommand commandObject = new SqlCommand(
                    dmlStatement, connectionObject);

                connectionObject.Open();
                rowsAffected = commandObject.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log Error

                throw ex;
            }
            finally
            {
                if (connectionObject != null)
                    connectionObject.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Method used to execute a stored procedure.
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>
        /// <returns>
        /// Returns the number of rows affected by the executed stored procedure.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown when there is any error in a database related operation.
        /// </exception>
        public int ExecuteSP(string spName)
        {
            int rowsAffected = -1;
            SqlConnection connectionObject = null;

            try
            {
                connectionObject = new SqlConnection(strconnectionString);
                SqlCommand commandObject = new SqlCommand(
                    spName, connectionObject);

                commandObject.CommandType = CommandType.StoredProcedure;

                connectionObject.Open();
                rowsAffected = commandObject.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Log Error

                throw ex;
            }
            finally
            {
                if (connectionObject != null)
                    connectionObject.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Method used to execute a stored procedure.
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="parameters">Any parameters for the stored procedure.</param>
        /// <returns>
        /// Returns the number of rows affected by the executed stored procedure.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown when there is any error in a database related operation.
        /// </exception>
        public int ExecuteSP(string spName, IDbDataParameter[] parameters)
        {
            int rowsAffected = -1;
            SqlConnection connectionObject = null;

            try
            {
                if (this.sqlCon == null)
                {
                    connectionObject = new SqlConnection(strconnectionString);
                    connectionObject.Open();
                }
                else
                    connectionObject = this.sqlCon;

                SqlCommand commandObject = new SqlCommand(
                    spName, connectionObject);

                commandObject.CommandType = CommandType.StoredProcedure;
                commandObject.Parameters.AddRange(parameters);

                rowsAffected = commandObject.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Log Error

                throw ex;
            }
            finally
            {
                if (this.sqlCon == null && connectionObject != null)
                    connectionObject.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Method used to execute a stored procedure.
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>        
        /// <returns>
        /// Returns a dataset filled with the values returned by the 
        /// executed stored procedure.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown when there is any error in a database related operation.
        /// </exception>
        public DataSet ExecuteDataSetSP(string spName)
        {
            SqlConnection connectionObject = null;
            DataSet dsObject = new DataSet();

            try
            {
                connectionObject = new SqlConnection(strconnectionString);

                SqlCommand commandObject = new SqlCommand(spName, connectionObject);
                commandObject.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapterObject = new SqlDataAdapter(commandObject);
                adapterObject.Fill(dsObject);
            }
            catch (Exception ex)
            {
                //Log Error
                throw ex;
            }
            finally
            {
                if (connectionObject != null)
                    connectionObject.Close();
            }

            return dsObject;
        }

        public DataSet ExecuteDataSet(string dmlStatement)
        {
            SqlConnection connectionObject = null;
            DataSet dsObject = new DataSet();

            try
            {
                connectionObject = new SqlConnection(strconnectionString);

                SqlCommand commandObject = new SqlCommand(dmlStatement, connectionObject);
                commandObject.CommandType = CommandType.Text;

                SqlDataAdapter adapterObject = new SqlDataAdapter(commandObject);
                adapterObject.Fill(dsObject);
            }
            catch (Exception ex)
            {
                //Log Error
                throw ex;
            }
            finally
            {
                if (connectionObject != null)
                    connectionObject.Close();
            }

            return dsObject;
        }

        /// <summary>
        /// Method used to execute a stored procedure.
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="parameters">Any parameters for the stored procedure.</param>
        /// <returns>
        /// Returns a dataset filled with the values returned by the 
        /// executed stored procedure.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown when there is any error in a database related operation.
        /// </exception>
        public DataSet ExecuteDataSetSP(string spName, IDbDataParameter[] parameters)
        {
            SqlConnection connectionObject = null;
            DataSet dsObject = new DataSet();

            try
            {
                connectionObject = new SqlConnection(strconnectionString);

                SqlCommand commandObject = new SqlCommand(spName, connectionObject);
                commandObject.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    commandObject.Parameters.AddRange(parameters);

                SqlDataAdapter adapterObject = new SqlDataAdapter(commandObject);
                adapterObject.Fill(dsObject);
            }
            catch (Exception ex)
            {
                //Log Error
                throw ex;
            }
            finally
            {
                if (connectionObject != null)
                    connectionObject.Close();
            }

            return dsObject;
        }

        /// <summary>
        /// Overloaded method used to generate a proper sql parameter object.
        /// </summary>
        /// <typeparam name="T">
        /// Generic type, to indicate the type of the parameter value.
        /// </typeparam>
        /// <param name="dataType">SQL data-type of the parameter.</param>
        /// <param name="parameterName">Name of the parameter in the database.</param>
        /// <param name="value">Value of the parameter.</param>
        /// <returns>
        /// Returns SqlParameter object filled with parameter 
        /// type, name, and value information.
        /// </returns>
        public IDbDataParameter CreateParameter<T>(SqlDbType dataType, string parameterName, T value)
        {
            SqlParameter parameterObject = new SqlParameter(parameterName, dataType);
            parameterObject.Value = value;

            return parameterObject;
        }

        /// <summary>
        /// Method used to generate a proper sql parameter object.
        /// </summary>
        /// <typeparam name="T">
        /// Generic type, to indicate the type of the parameter value.
        /// </typeparam>
        /// <param name="dataType">SQL data-type of the parameter.</param>
        /// <param name="parameterName">Name of the parameter in the database.</param>
        /// <param name="value">Value of the parameter.</param>
        /// <param name="length">Length of the parameter.</param>
        /// <returns>
        /// Returns SqlParameter object filled with parameter 
        /// type, name, and value information.
        /// </returns>
        public IDbDataParameter CreateParameterWithSize<T>(SqlDbType dataType, int length, string parameterName, T value)
        {
            SqlParameter parameterObject = new SqlParameter(
                parameterName, dataType, length);

            parameterObject.Value = value;

            return parameterObject;
        }

        /// <summary>
        /// Method used to execute a stored procedure.
        /// </summary>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="parameters">Any parameters for the stored procedure.</param>
        /// <returns>
        /// Integer value selected from the database.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown when there is any error in a database related operation.
        /// </exception>
        public int ExecuteScalar(string spName, IDbDataParameter[] parameters)
        {
            SqlConnection connectionObject = null;

            try
            {
                connectionObject = new SqlConnection(strconnectionString);
                SqlCommand commandObject = new SqlCommand(
                    spName, connectionObject);

                commandObject.CommandType = CommandType.StoredProcedure;
                commandObject.Parameters.AddRange(parameters);

                connectionObject.Open();
                return (int)commandObject.ExecuteScalar();
            }
            catch (Exception)
            {
                //Log Error
                throw;
            }
            finally
            {
                if (connectionObject != null)
                    connectionObject.Close();
            }

        }

        //public SqlResponseBaseModel ExecuteModelSP(string spName)
        //{
        //    SqlConnection connectionObject = null;
        //    DataSet dsObject = new DataSet();

        //    try
        //    {
        //        connectionObject = new SqlConnection(strconnectionString);

        //        SqlCommand commandObject = new SqlCommand(spName, connectionObject);
        //        commandObject.CommandType = CommandType.StoredProcedure;

        //        SqlDataAdapter adapterObject = new SqlDataAdapter(commandObject);
        //        adapterObject.Fill(dsObject);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Error
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (connectionObject != null)
        //            connectionObject.Close();
        //    }

        //    return dsObject;
        //}

        //public SqlResponseBaseModel ExecuteModelSP(string spName, IDbDataParameter[] parameters)
        //{
        //    SqlConnection connectionObject = null;
        //    DataSet dsObject = new DataSet();

        //    try
        //    {
        //        connectionObject = new SqlConnection(strconnectionString);

        //        SqlCommand commandObject = new SqlCommand(spName, connectionObject);
        //        commandObject.CommandType = CommandType.StoredProcedure;

        //        if (parameters != null)
        //            commandObject.Parameters.AddRange(parameters);

        //        SqlDataAdapter adapterObject = new SqlDataAdapter(commandObject);
        //        adapterObject.Fill(dsObject);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Error
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (connectionObject != null)
        //            connectionObject.Close();
        //    }

        //    return dsObject;
        //}
    }
}
