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
            LogParser parser1 = new LogParserCsv(4, DateTimeFormatInfo.CurrentInfo, 5, 6, "CSV Payments", Environment.NewLine);
            DataSource csv1 = new DataSourceFile("c:\\temp\\csv\\payments.csv");
            LogReader reader1 = new LogReader(parser1, csv1);
            List<LogEntry> entries1 = reader1.GetLogEntries();
            Console.WriteLine(entries1.Count);
            Console.ReadKey();
        }
    }
}
