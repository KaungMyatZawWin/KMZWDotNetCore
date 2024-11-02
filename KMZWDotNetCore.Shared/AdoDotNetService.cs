using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable Query(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Key, sqlParameter.Value);
                };
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            connection.Close();

            return dt;
        }

        public int Execute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Key, sqlParameter.Value);
                };
            }

            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();

            //adapter.Fill(dt);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return result;
        }

        public class SqlParameterModel
        {
            public string Key { get; set; }

            public object Value { get; set; }

            public SqlParameterModel(string name, object value)
            {
                Key = name;
                Value = value;
            }

        }


    }
}
