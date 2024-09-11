using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DBChecher
{
    public class ConnectionInfo
    {
        public string FullDBSize = "";
        public ConnectionMenager Manager = new ConnectionMenager();
        public DataTable AllTables = new DataTable();
        public DataTable MeasuresTable = new DataTable();
        public DataTable RecordsTable = new DataTable();
        public ConnectionInfo(ConnectionMenager manager, DataTable allTables, DataTable measuresTable, DataTable recordsTable, String fullDBSize) 
        {
            FullDBSize = fullDBSize;
            Manager = manager;
            AllTables = allTables;
            MeasuresTable = measuresTable;
            RecordsTable = recordsTable;
        }
        public ConnectionInfo() { }
    }
}
