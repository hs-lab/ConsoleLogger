using ConsoleLogger;
using ConsoleLogger.DataSources;
using ConsoleLogger.LogReaders;
using ConsoleLogger.Parsers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace LogViewer.Controllers
{
    public class LogsController : ApiController
    {
        public IHttpActionResult getLogs()
        {
            LogReader reader1 = new LogReader(new LogParserCsv(0, "dd-MM-yyyy HH:mm", 2, 3, 1, Environment.NewLine),
                new DataSourceFile(@"c:\temp\masterlog.log"));
            List<LogEntry> entries1 = reader1.GetLogEntries();
            List<LogEntry> entries = entries1;
            entries.Sort();
            StringBuilder s = new StringBuilder();
            foreach (LogEntry entry in entries)
            {
                s.Append(entry.ToString());
            }
            return Ok(new JavaScriptSerializer().Serialize(entries));
        }
    }
}
