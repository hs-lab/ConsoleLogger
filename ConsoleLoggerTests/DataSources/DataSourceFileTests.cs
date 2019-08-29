using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleLogger.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger.DataSources.Tests
{
    [TestClass()]
    public class DataSourceFileTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DataSourceFileTest()
        {
            //Arrage 

            //Act
            DataSourceFile dsf = new DataSourceFile("c:\\temp\\nonexistentfile.txt");

            //Expects exception
        }

        [TestMethod()]
        public void readTest()
        {
            Assert.Fail();
        }
    }
}