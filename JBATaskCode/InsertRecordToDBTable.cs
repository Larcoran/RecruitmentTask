using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBATaskCode
{
    public class InsertRecordToDBTable
    {
        DBConnection connection;
        public InsertRecordToDBTable(string connectionString)
        {
            connection = new DBConnection(connectionString);
            connection.OpenConnection();
        }

        public async Task InsertRecordToTable(Record record)
        {
            string query = "INSERT INTO Precipitation (Xref,Yref,Date,Value)";
            query += " VALUES (@Xref, @Yref ,@Date ,@Value)";
            SqlCommand cmd = new SqlCommand(query, connection.Connection);
            cmd.Parameters.AddWithValue("@Xref", record.Xref);
            cmd.Parameters.AddWithValue("@Yref", record.Yref);
            cmd.Parameters.AddWithValue("@Date", record.Date);
            cmd.Parameters.AddWithValue("@Value", record.Value);
            await Task.Run(() => cmd.ExecuteNonQuery());
        }

        public void CloseConnection()
        {
            connection.CloseConnection();
        }
    }
}
