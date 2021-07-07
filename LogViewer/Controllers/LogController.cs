using ConsoleLogger;
using ConsoleLogger.LogReaders;
using ConsoleLogger.ConfigHandler;
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
        /// <summary>
        ///   <para>
        ///  Provides a RESTAPI to read the master log.</para>
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult getLogs()
        {
            LogConfig logConfig = new LogConfig(LogType.CSV, @"C:\temp\masterlog.log","1", "0", "dd-MM-yyyy HH:mm", "2", "3", new string[] { Environment.NewLine, "1", ",", "\"" });
            LogReader reader = ConfigUtility.ConfigToLogReader(logConfig);
            List<LogEntry> entries = reader.GetLogEntries();
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
