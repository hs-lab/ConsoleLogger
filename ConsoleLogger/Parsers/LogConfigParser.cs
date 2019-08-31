using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using ConsoleLogger.LogReaders;
using ConsoleLogger.DataSources;

namespace ConsoleLogger.Parsers
{
    /// <summary>Utility class for reading and writing logger config file.</summary>
    public class ConfigUtility
    {
        public static void WriteConfig(string location, List<LogConfig> configs)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<LogConfig>));
            using (StreamWriter writer = new StreamWriter(location))
            {
                serializer.Serialize(writer, configs);
            }
        }

        public static List<LogConfig> ReadConfig(string location)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<LogConfig>));
            using (StreamReader reader = new StreamReader(location))
            {
                return (List<LogConfig>)serializer.Deserialize(reader);
            }
        }

        public static LogReader ConfigToLogReader(LogConfig config)
        {
            LogParser parser;
            DataSource source;
            switch (config.type)
            {
                case LogType.CSV:
                    parser = new LogParserCsv(int.Parse(config.dateField),
                        config.dateFormat,
                        int.Parse(config.levelField),
                        int.Parse(config.messageField),
                        config.location,
                        config.additionalSettings[0],int.Parse(config.additionalSettings[1]),char.Parse(config.additionalSettings[2]),char.Parse(config.additionalSettings[3]));
                    source = new DataSourceFile(config.location);
                    break;
                case LogType.XML:
                    parser = new LogParserXml(config.location, 
                        config.additionalSettings[0],
                        config.levelField,
                        config.dateField,
                        config.messageField
                        );
                    source = new DataSourceFile(config.location);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("config.type");
            }
            return new LogReader(parser, source);

        }
    }
}
