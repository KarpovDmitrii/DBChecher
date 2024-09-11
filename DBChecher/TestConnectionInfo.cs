using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBChecher
{
    public class TestConnectionInfo
    {
        public string FullDBSize = "";
        public ConnectionMenager Manager = new ConnectionMenager();
        public DataTable AllTables = new DataTable();
        public DataTable MeasuresTable = new DataTable();
        public DataTable RecordsTable = new DataTable();
       

        public TestConnectionInfo() { }

        public TestConnectionInfo(string fullDBSize, ConnectionMenager manager, DataTable allTables, DataTable measuresTable, DataTable recordsTable)
        {  
            FullDBSize = fullDBSize; 
            Manager = manager; 
            AllTables = allTables;
            MeasuresTable = measuresTable;
            RecordsTable = recordsTable;
        }

        public ConnectionInfo convertToConnectionInfo()
        {
            return new ConnectionInfo(Manager, AllTables, MeasuresTable, RecordsTable, FullDBSize); 
        }
    }
}
