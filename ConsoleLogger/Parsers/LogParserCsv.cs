using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLogger.Utilities;
using System.Globalization;

namespace ConsoleLogger.Parsers
{
    /// <summary>A log parser for CSV files</summary>
    /// <seealso cref="LogParser" />
    public class LogParserCsv:LogParser
    {
        public CsvConfig CsvConfig { get; private set; }
        public int TimeIndex { get; private set; }
        public int LevelIndex { get; private set; }
        public int OutputIndex { get; private set; }
        public int LocationIndex { get; private set; }
        public string Location { get; private set; }
        public string TimeFormat { get; private set; }

        public LogParserCsv(
            int TimeIndex,
            string Format,
            int LevelIndex,
            int OutputIndex,
            string Location, 
            string Newline,
            int IgnoreFirstNLines = 1,
            char Delimiter=',',  
            char Quotemark = '"'
        )
        {
            this.TimeIndex = TimeIndex;
            TimeFormat = Format;
            this.LevelIndex = LevelIndex;
            this.OutputIndex = OutputIndex;
            this.Location = Location;
            this.LocationIndex = -1;
            CsvConfig = new CsvConfig(Delimiter, Newline, Quotemark, IgnoreFirstNLines);
        }

        public LogParserCsv(
            int TimeIndex,
            string Format,
            int LevelIndex,
            int OutputIndex,
            int LocationIndex,
            string Newline,
            int IgnoreFirstNLines = 1,
            char Delimiter = ',',
            char Quotemark = '"'
        )
        {
            this.TimeIndex = TimeIndex;
            TimeFormat = Format;
            this.LevelIndex = LevelIndex;
            this.OutputIndex = OutputIndex;
            this.LocationIndex = LocationIndex;
            this.Location = "";
            CsvConfig = new CsvConfig(Delimiter, Newline, Quotemark, IgnoreFirstNLines);
        }

        /// <summary>  Converts a CSV string to a list of log entries.</summary>
        /// <param name="s">The string.</param>
        /// <returns>a List of LogEntries</returns>
        public override List<LogEntry> Parse(string s)
        {
            List<string[]> parsedText = CsvReader.Read(s, CsvConfig);
            List<LogEntry> result = new List<LogEntry>();
            foreach (string[] line in parsedText)
            {
                DateTime time;
                DateTime.TryParseExact(line[TimeIndex], TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out time);
                if (time == DateTime.MinValue)
                {
                    //throw new FormatException("Error parsing time");
                }

                string location = Location;
                if (LocationIndex >= 0)
                {
                    //read location from log instead 
                    location = line[LocationIndex];
                }
                result.Add(new LogEntry(time, location, line[LevelIndex], line[OutputIndex]));
            }
            return result;
        }
    }
}
