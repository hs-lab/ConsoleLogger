using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLogger.DataSources;
using ConsoleLogger.Parsers;
using ConsoleLogger.LogReaders;
using System.Globalization;
using ConsoleLogger.Utilities;
using System.IO;

namespace ConsoleLogger
{
    class MasterLogger
    {
        static void Main(string[] args)
        {
            LogConfig config1 = new LogConfig(LogType.CSV, @"C:\temp\csv\payments.csv", "4", "dd/MM/yyyy", "5", "6");
            LogConfig config2 = new LogConfig(LogType.CSV, @"c:\temp\logs\app.log", "0", "yyyy-MM-dd HH:mm:ss", "2", "3");
            LogReader reader1 = ConfigUtility.ConfigToLogReader(config1);
            LogReader reader2 = ConfigUtility.ConfigToLogReader(config2);
            List<LogEntry> entries1 = reader1.GetLogEntries();
            List<LogEntry> entries2 = reader2.GetLogEntries();
            List<LogEntry> entries = entries1;
            entries.AddRange(entries2);
            entries.Sort();

            using (StreamWriter writer = File.AppendText(@"C:\temp\masterlog.log"))
            {
                foreach (LogEntry entry in entries)
                {
                    Console.WriteLine(entry.ToString(CultureInfo.GetCultureInfo("En-AU").DateTimeFormat));
                    writer.WriteLine(entry.ToString(CultureInfo.GetCultureInfo("En-AU").DateTimeFormat));
                }
            }

            Console.ReadKey();
        }
    }
}
