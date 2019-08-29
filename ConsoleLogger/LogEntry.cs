using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger
{
    public class LogEntry:IComparable
    {
        DateTime _time;
        string _location;
        string _level;
        string _output;

        public LogEntry(DateTime time, string location, string level, string output)
        {
            _time = time;
            _location = location;
            _level = level;
            _output = output;
        }

        public int CompareTo(object obj)
        {
            return _time.CompareTo(obj);
        }
    }


}
