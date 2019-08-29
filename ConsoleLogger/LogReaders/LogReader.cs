using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLogger.Parsers;
using ConsoleLogger.DataSources;

namespace ConsoleLogger.LogReaders
{
    public class LogReader
    {
        LogParser _parser;
        DataSource _source;

        public LogReader(LogParser parser, DataSource source)
        {
            _parser = parser;
            _source = source;
        }

        public List<LogEntry> GetLogEntries()
        {
            return _parser.Parse(_source.Read());
        }
    }
}
