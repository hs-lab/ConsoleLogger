using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLogger.DataSources;
using ConsoleLogger.Parsers;
using ConsoleLogger.LogReaders;
using System.Globalization;

namespace ConsoleLogger
{
    class MasterLogger
    {
        static void Main(string[] args)
        {
        
            LogReader reader1 = new LogReader(new LogParserCsv(4, CultureInfo.GetCultureInfo("En-AU").DateTimeFormat, 5, 6, "CSV Payments", Environment.NewLine),
                new DataSourceFile("c:\\temp\\csv\\payments.csv"));
            LogReader reader2 = new LogReader(new LogParserCsv(0, DateTimeFormatInfo.InvariantInfo, 2, 3, "App log", Environment.NewLine, quotemark:'\0'),
                new DataSourceFile("c:\\temp\\logs\\app.log"));
            List<LogEntry> entries1 = reader1.GetLogEntries();
            List<LogEntry> entries2 = reader2.GetLogEntries();
            List<LogEntry> entries = entries1;
            entries.AddRange(entries2);
            entries.Sort();
            foreach (LogEntry entry in entries)
            {
                Console.WriteLine(entry.ToString(CultureInfo.GetCultureInfo("En-AU").DateTimeFormat));
            }

            Console.ReadKey();
        }
    }
}
