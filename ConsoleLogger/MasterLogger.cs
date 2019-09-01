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
using System.Configuration;

namespace ConsoleLogger
{
    class MasterLogger
    {
        /// <summary>Entry point to the application. The MasterLogger reads multiple log files, and aggregates them into a single log file.</summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //TODO: save configs to a file and load from the file
            List<LogConfig> configs = new List<LogConfig>();
            configs.Add(new LogConfig(LogType.CSV, @"C:\temp\csv\payments.csv", "4", "dd/MM/yyyy", "5", "6", new string[] { Environment.NewLine, "1", ",", "\"" }));
            configs.Add(new LogConfig(LogType.CSV, @"c:\temp\logs\app.log", "0", "yyyy-MM-dd HH:mm:ss", "2", "3", new string[] { Environment.NewLine, "0", ",", "\0" }));
            configs.Add(new LogConfig(LogType.XML, @"c:\temp\xml\log.xml", "timestamp", "yyyy-MM-dd HH:mm:ss", "level", "name", new string[] { "action" }));
            configs.Add(new LogConfig(LogType.CSV, @"C:\temp\csv\NoPayments.csv", "4", "dd/MM/yyyy", "5", "6", new string[] { Environment.NewLine, "1", ",", "\"" }));


            List<LogEntry> logEntries = new List<LogEntry>();
            foreach (LogConfig logConfig in configs)
            {
                try
                {
                    LogReader logReader = ConfigUtility.ConfigToLogReader(logConfig);
                    logEntries.AddRange(logReader.GetLogEntries());
                    logReader.WatchSource();
                }
                catch (Exception ex)
                {
                    //Want system to continue to run if an error is encountered with a single log.
                    //Record error as another log entry so it can be reviewed
                    logEntries.Add(new LogEntry(DateTime.Now, "Master logger", "Error", ex.Message));
                }
            }
            logEntries.Sort();
            writeLog(logEntries);
            Console.ReadKey();
        }

        public static void writeLog(List<LogEntry> logEntries)
        {
            using (StreamWriter writer = File.AppendText(System.Configuration.ConfigurationManager.AppSettings["masterLogLocation"]))
            {
                foreach (LogEntry entry in logEntries)
                {
                    Console.WriteLine(entry.ToString("dd-MM-yyyy HH:mm"));
                    writer.WriteLine(entry.ToString("dd-MM-yyyy HH:mm"));
                }
            }
        }
    }
}
