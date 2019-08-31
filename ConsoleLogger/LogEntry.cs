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
        public DateTime Time { get; private set; }
        public string Location { get; private set; }
        public string Level { get; private set; }
        public string Output { get; private set; }


        public LogEntry(DateTime time, string location, string level, string output)
        {
            this.Time = time;
            this.Location = location;
            this.Level = Regex.Replace(level, @"^[\W]+|[\W]+$",""); //trim level of leading and trailing non-word characters.
            this.Level = this.Level.ToUpper(); //to uppercase for consistency; could use an Enum, but limits flexibility for new logs.
            this.Output = output;
        }

        public int CompareTo(object obj)
        {
            return Time.CompareTo(((LogEntry)obj).Time);
        }

        public override string ToString()
        {
            return $"{Time},{Location.Replace("\"", "\"\"\"")},{Level},{Output.Replace("\"", "\"\"\"")}";
        }

        public string ToString(string dateFormat)
        {
            return $"{Time.ToString(dateFormat)},{Location.Replace("\"", "\"\"\"")},{Level},{Output.Replace("\"", "\"\"\"")}";
        }
    }


}
