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
            return _time.CompareTo(((LogEntry)obj)._time);
        }

        public override string ToString()
        {
            return $"{_time.ToString()} | {_location} | {_level} | {_output}";
        }

        public string ToString(IFormatProvider dateFormat)
        {
            return $"{_time.ToString(dateFormat)} | {_location} | {_level} | {_output}";
        }
    }


}
