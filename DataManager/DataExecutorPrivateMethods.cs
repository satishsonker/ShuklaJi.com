using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ShuklaJi.DataManager
{
    public static partial class DataExecutor
    {
        #region Private Methods

        /// <summary>  
        /// Prepares SqlCommand for stored procedure with oprional parameters list  
        /// </summary>  
        /// <param name="procedureName">stored procedure Name</param>  
        /// <param name="parameters">List of parameters</param>  
        /// <returns></returns>  
        private static SqlCommand GetCommand(string procedureName, SqlConnection sqlConnection, CommandType commandType, IEnumerable<KeyValuePair<string, object>> parameters = default(List<KeyValuePair<string, object>>))
        {
            SqlCommand command = new SqlCommand(procedureName);
            if (sqlConnection.State != ConnectionState.Open)
            {
                if (string.IsNullOrEmpty(sqlConnection.ConnectionString))
                {
                    sqlConnection.ConnectionString = ConnectionString;
                }
                sqlConnection.Open();
            }
            if (parameters != default(List<KeyValuePair<string, object>>) && parameters.Count() > 0)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            command.Connection = sqlConnection;
            command.CommandTimeout = ConnectionTimeout;
            command.CommandType = commandType;
            return command;
        }

        /// <summary>  
        /// Creates SqlAdapter instance for the stored procedure with optional paramets  
        /// </summary>  
        /// <param name="procedureName"></param>  
        /// <param name="parameters"></param>  
        /// <returns></returns>  
        private static SqlDataAdapter GetAdapter(string procedureName, SqlConnection sqlConnection, IEnumerable<KeyValuePair<string, object>> parameters = default(List<KeyValuePair<string, object>>), CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return new SqlDataAdapter(GetCommand(procedureName, sqlConnection, commandType, parameters));
            }
            catch (SqlException se)
            {
                throw se;
            }
        }


        #endregion
    }
}
