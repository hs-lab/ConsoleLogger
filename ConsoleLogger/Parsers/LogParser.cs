using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger.Parsers
{
    /// <summary>Parent class for all log parsers</summary>
    public abstract class LogParser
    {
        /// <summary>Parses the specified string into a list of logentries.</summary>
        /// <param name="s">The string.</param>
        /// <returns>List of LogEntry</returns>
        public abstract List<LogEntry> Parse(string s);
    }
}
