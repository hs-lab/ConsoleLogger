using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger
{
    public enum LogType
    {
        CSV,
        XML
    }

    [Serializable()]
    public class LogConfig
    {
        public LogType type;
        public string location;
        public string dateField;
        public string dateFormat;
        public string levelField;
        public string messageField;

        public LogConfig()
        {

        }

        public LogConfig(LogType Type, string Location, string DateField, string DateFormat, string LevelField, string MessageField)
        {
            type = Type;
            location = Location;
            dateField = DateField;
            dateFormat = DateFormat;
            levelField = LevelField;
            messageField = MessageField;
        }

    }
}
