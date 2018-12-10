using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JBATaskCode
{
    public class DBConnection
    {
        private string ConnectionString;
        public SqlDataReader reader = null;

        public SqlConnection Connection { get; private set; }

        public DBConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void OpenConnection()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

        public void CloseConnection()
        {
            Connection.Dispose();
        }
    }
}
