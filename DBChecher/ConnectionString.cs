using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DBChecher
{
    public class ConnectionString
    {
        public string Server ="";
        public string Port= "";
        public string UserID = "";
        public string Password = "";
        public string DataBase = "";
        public ConnectionString(){}
        public ConnectionString(string server, string port, string userID, string password, string dataBase) 
        {
            Server = server;
            Port = port;
            UserID = userID;
            Password = password;
            DataBase = dataBase;
        }
        public string GetConnectionString()
        {
            return "Server=" + Server +
                "; port=" + Port +
                "; user id=" + UserID +
                "; password=" + Password +
                "; database=" + DataBase + ";";
        }
    }
}




