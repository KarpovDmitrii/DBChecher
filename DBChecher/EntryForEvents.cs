using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBChecher
{
    public class EntryForEvents
    {
        public int Key = 0;
        public string Value = "";
        public EntryForEvents()
        {
        }
        public EntryForEvents(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}