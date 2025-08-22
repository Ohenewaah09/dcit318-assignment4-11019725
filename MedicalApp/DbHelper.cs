using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MedicalApp
{
    public static class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MedicalDB"].ConnectionString;
            return new SqlConnection(connectionString);
        }

        public static SqlCommand CreateCommand(SqlConnection conn, string sql)
        {
            return new SqlCommand(sql, conn);
        }

        public static void AddParam(SqlCommand cmd, string paramName, object value, SqlDbType dbType)
        {
            var param = cmd.Parameters.Add(paramName, dbType);
            param.Value = value ?? DBNull.Value;
        }
    }
}