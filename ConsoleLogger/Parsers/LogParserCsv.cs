using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLogger.Utilities;
using System.Globalization;

namespace ConsoleLogger.Parsers
{
    public class LogParserCsv:LogParser
    {
        CsvConfig _csvConfig;
        int _timeIndex;
        int _levelIndex;
        int _outputIndex;
        string _location;
        DateTimeFormatInfo _format;

        public LogParserCsv(int timeIndex, DateTimeFormatInfo format, int levelIndex, int outputIndex, string location, string newline, char delimiter=',',  char quotemark = '"')
        {
            _timeIndex = timeIndex;
            _format = format;
            _levelIndex = levelIndex;
            _outputIndex = outputIndex;
            _location = location;
            _csvConfig = new CsvConfig(delimiter, newline, quotemark);
        }

        public override List<LogEntry> Parse(string s)
        {
            List<string[]> parsedText = CsvReader.Read(s, _csvConfig);
            List<LogEntry> result = new List<LogEntry>();
            foreach (string[] line in parsedText)
            {
                DateTime time;
                DateTime.TryParse(line[_timeIndex], _format, DateTimeStyles.AdjustToUniversal, out time);
                if (time == null)
                {
                    throw new FormatException("Error parsing time");
                }
                result.Add(new LogEntry(time, _location, line[_levelIndex], line[_outputIndex]));
            }
            return result;
        }
    }
}
