using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Npgsql;


namespace DBChecher
{
    public class ConnectionMenager
    {
        private NpgsqlConnection _connection = new NpgsqlConnection();
        public ConnectionString connectionString = new ConnectionString();

        public ConnectionMenager(){}
        public ConnectionMenager(ConnectionString connect) 
        {
            connectionString = connect;
            _connection = new NpgsqlConnection(connectionString.GetConnectionString());
        }
        public void CreateConnection() 
        { 
            _connection = new NpgsqlConnection(connectionString.GetConnectionString());
        }

        public void OpenConnection()
        {
            _connection.Open();
        }
        public void CloseConnection()
        {
            _connection.Close();
        }
        public DataTable sendRequest (string sql, int timeOut)
        {
            DataTable dataTable = new DataTable();
            var cmd = new NpgsqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = sql;
            cmd.CommandTimeout = timeOut;
            NpgsqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);

            return dataTable;
        }
    }
}
