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

    /// <summary>Provodes a generalised structure for configuring a LogReader.
    /// TODO: set up subclasses to make handling of different log types more robust.
    /// </summary>
    [Serializable()]
    public class LogConfig
    {
        public LogType type;
        public string location;
        public string path;
        public string dateField;
        public string dateFormat;
        public string levelField;
        public string messageField;
        public string[] additionalSettings;

        /// <summary>Initializes a new instance of the <see cref="LogConfig"/> class.
        /// Required for serializability.</summary>
        public LogConfig()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="LogConfig"/> class. 
        /// Translation between the strings provided and the format required for the specified parser type is handled by
        /// the LogConfigParser class.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Location">The location.</param>
        /// <param name="DateField">The date field.</param>
        /// <param name="DateFormat">The date format.</param>
        /// <param name="LevelField">The level field.</param>
        /// <param name="MessageField">The message field.</param>
        /// <param name="AdditionalSettings">The additional settings.</param>
        public LogConfig(LogType Type, string Location, string DateField, string DateFormat, string LevelField, string MessageField, string[] AdditionalSettings)
        {
            type = Type;
            location = Location;
            path = Location;
            dateField = DateField;
            dateFormat = DateFormat;
            levelField = LevelField;
            messageField = MessageField;
            additionalSettings = AdditionalSettings;
        }

        public LogConfig(LogType Type, string Path, string Location, string DateField, string DateFormat, string LevelField, string MessageField, string[] AdditionalSettings)
        {
            type = Type;
            location = Location;
            path = Path;
            dateField = DateField;
            dateFormat = DateFormat;
            levelField = LevelField;
            messageField = MessageField;
            additionalSettings = AdditionalSettings;
        }

    }
}
