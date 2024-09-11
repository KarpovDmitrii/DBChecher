using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBChecher
{
    public class Entry
    {
        public string Key = "";
        public ConnectionInfo Value = new ConnectionInfo(); //"opo-postgresql.zav.mir", "5432", "postgres", "mirpass", "utek_lnx"
        public Entry()
        {
        }
        public Entry(string key, ConnectionInfo value)
        {
            Key = key;
            Value = value;
        }
    }
}
