using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBATaskCode
{
    public class CreateTableInDB
    {
        DBConnection connection;

        public CreateTableInDB(string connectionString)
        {
            connection = new DBConnection(connectionString);
            connection.OpenConnection();
        }

        public async Task CreateTable(string command)
        {
            SqlCommand cmd = new SqlCommand(command, connection.Connection);
            await Task.Run(() => cmd.ExecuteNonQuery());
            CloseConnection();
        }

        private void CloseConnection()
        {
            connection.CloseConnection();
        }

    }
}
