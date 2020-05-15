using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ShuklaJi.DataManager
{
    /// <summary>
    /// Contains Database access Methods
    /// </summary>
    public static partial class DataExecutor
    {
        #region Private Variable
        #endregion

        #region Ctor

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute Store Procedure and get Datatable
        /// </summary>
        /// <param name="ProcedureName">Store Procedure Name</param>
        /// <param name="param">List of Parameters</param>
        /// <returns>Datatable</returns>
        public static DataTable ExecuteDataTable(string ProcedureName, IEnumerable<KeyValuePair<string, object>> param = default(IEnumerable<KeyValuePair<string, object>>))
        {
            if(string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception(DataManagerConstant.Message.NoConnectionString);
            }

            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                GetAdapter(ProcedureName, sqlConnection, param).Fill(dataTable);
            }

            return dataTable;
        }

        /// <summary>
        /// Execute Store Procedure and get DataSet
        /// </summary>
        /// <param name="ProcedureName">Store Procedure Name</param>
        /// <param name="param">List of Parameters</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string ProcedureName, IEnumerable<KeyValuePair<string, object>> param = default(IEnumerable<KeyValuePair<string, object>>))
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception(DataManagerConstant.Message.NoConnectionString);
            }
            DataSet dataSet = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                GetAdapter(ProcedureName, sqlConnection, param).Fill(dataSet);
            }
            return dataSet;
        }

        /// <summary>  
        /// Execute stored procedure with optional paramets list  
        /// </summary>  
        /// <param name="procedureName">stored procedure name</param>  
        /// <param name="parameters">List of Parameters</param>  
        /// <returns>No. of effected rows</returns>  
        public static int ExecuteNonQuery(string procedureName, IEnumerable<KeyValuePair<string, object>> param = default(IEnumerable<KeyValuePair<string, object>>))
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception(DataManagerConstant.Message.NoConnectionString);
            }
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                return GetCommand(procedureName, sqlConnection, CommandType.StoredProcedure, param).ExecuteNonQuery();
            }
        }

        /// <summary>  
        /// Execute stored procedure with optional paramets list  
        /// </summary>  
        /// <param name="procedureName">stored procedure name</param>  
        /// <param name="parameters">List of Parameters</param>  
        /// <returns>recent updated identity column</returns>  
        public static string ExecuteScalar(string procedureName, IEnumerable<KeyValuePair<string, object>> param = default(IEnumerable<KeyValuePair<string, object>>))
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception(DataManagerConstant.Message.NoConnectionString);
            }
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                var oResult = GetCommand(procedureName, sqlConnection, CommandType.StoredProcedure, param).ExecuteScalar();
                return oResult == null ? string.Empty : oResult.ToString();
            }
        }
        #endregion

        #region Property

        /// <summary>
        /// Set Conection string
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Set Connection time out 
        /// Default is 300 Seconds
        /// </summary>
        public static int ConnectionTimeout { get; set; } = 300;
        #endregion
    }
}
