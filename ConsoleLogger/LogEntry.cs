using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            _level = Regex.Replace(level, @"^[\W]+|[\W]+$",""); //trim level of leading and trailing non-word characters.
            _level = _level.ToUpper(); //to uppercase for consistency; could use an Enum, but limits flexibility for new logs.
            _output = output;
        }

        public int CompareTo(object obj)
        {
            return _time.CompareTo(((LogEntry)obj)._time);
        }

        public override string ToString()
        {
            return $"{_time},{_location.Replace("\"", "\"\"\"")},{_level},{_output.Replace("\"", "\"\"\"")}";
        }

        public string ToString(IFormatProvider dateFormat)
        {
            return $"{_time.ToString(dateFormat)},{_location.Replace("\"", "\"\"\"")},{_level},{_output.Replace("\"", "\"\"\"")}";
        }
    }


}
