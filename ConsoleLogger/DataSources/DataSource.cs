using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger.DataSources
{
    public abstract class DataSource
    {
        protected string location;

        public abstract string Read();

        public abstract void Watch();
    }
}
