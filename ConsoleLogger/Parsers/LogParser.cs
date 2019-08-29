using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger.Parsers
{
    public abstract class LogParser
    {
        public abstract List<LogEntry> Parse(string s);
    }
}
