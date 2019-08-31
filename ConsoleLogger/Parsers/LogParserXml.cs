using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleLogger.Parsers
{
    public class LogParserXml:LogParser
    {
        string location;
        string logEntryXpath;
        string levelXPath;
        string dateXPath;
        string messageXPath;

        public LogParserXml(string Location, string LogEntryXpath, string LevelXpath, string DateXpath, string MessageXpath)
        {
            location = Location;
            logEntryXpath = LogEntryXpath;
            levelXPath = LevelXpath;
            dateXPath = DateXpath;
            messageXPath = MessageXpath;
        }
            
        public override List<LogEntry> Parse(string s)
        {
            List<LogEntry> result = new List<LogEntry>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(s);
            XmlNodeList logEntryNodes = xDoc.DocumentElement.SelectNodes(logEntryXpath);
            foreach (XmlNode logEntryNode in logEntryNodes)
            {
                string level = logEntryNode.SelectSingleNode(levelXPath).InnerText;
                string date = logEntryNode.SelectSingleNode(dateXPath).InnerText;
                DateTime time;
                DateTime.TryParse(date, out time);
                string message = logEntryNode.SelectSingleNode(messageXPath).InnerText;
                result.Add(new LogEntry(time, location, level, message));
            }
            return result;
        }
    }
}
