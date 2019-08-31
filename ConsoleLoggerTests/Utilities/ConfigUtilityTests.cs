using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleLogger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleLogger;
using FluentAssertions;
using System.Xml.Linq;

namespace ConsoleLogger.Utilities.Tests
{
    [TestClass()]
    public class ConfigUtilityTests
    {
        [TestMethod()]
        public void serializeAndDeserializeConfigTest()
        {
            LogConfig config = new LogConfig(ConsoleLogger.LogType.CSV, "location", "datefield", "dd-MM-YYYY HH:mm", "levelfield", "messagefield");
            List<LogConfig> configs = new List<LogConfig>();
            configs.Add(config);
            string path = @"C:\temp\logconfix.xml";

            ConfigUtility.WriteConfig(path, configs);
            List<LogConfig> readback = ConfigUtility.ReadConfig(path);

            configs[0].Should().BeEquivalentTo(readback[0]);
        }
    }
}