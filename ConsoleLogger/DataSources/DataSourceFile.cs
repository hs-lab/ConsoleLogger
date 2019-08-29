using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleLogger.DataSources
{
    public class DataSourceFile:DataSource
    {
        
        public DataSourceFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"File path does not exist: {filePath}");
            }
            location = filePath;
        }

        public override string Read()
        {
            using (StreamReader r = new StreamReader(location))
            {
                return r.ReadToEnd();
            }


        }
    }
}
