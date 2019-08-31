using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleLogger.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Xml.Linq;

namespace ConsoleLogger.Parsers.Tests
{
    [TestClass()]
    public class LogParserXmlTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            string xmlstring = @"
<actions>
    <action>
        <id>1234</id>
        <name>Edited: CSV document 1</name>
        <description>Cell A:5 modified successfully</description>
        <level>INFO</level>
        <timestamp/>
    </action>
</actions>
";
            
            string location = "testlocation";
            string logXPath = "action";
            string levelXPath = "level";
            string dateXPath = "timestamp";
            string messageXPath = "name";
            List<LogEntry> logEntries = new List<LogEntry>();
            LogEntry expected = new LogEntry(DateTime.MinValue, location, "INFO", "Edited: CSV document 1");
            logEntries.Add(expected);

            LogParserXml parser = new LogParserXml(location, logXPath, levelXPath, dateXPath, messageXPath);
            List<LogEntry> result = parser.Parse(xmlstring);
            result.Should().BeEquivalentTo(logEntries);
        }
    }
}