using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBATaskCode
{
    public class GetDataFromTable
    {
        DBConnection connection;
        public SqlDataReader reader;

        public GetDataFromTable(string connectionString)
        {
            connection = new DBConnection(connectionString);
            connection.OpenConnection();
        }

        public async Task GetData(string command)
        {
            SqlCommand cmd = new SqlCommand(command, connection.Connection);
            await Task.Run(() => reader = cmd.ExecuteReader());
        }

        public void CloseConnection()
        {
            reader.Close();
            connection.CloseConnection();
        }
    }
}
