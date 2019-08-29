using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleLogger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger.Utilities.Tests
{
    [TestClass()]
    public class CsvReaderTests
    {
        [TestMethod()]
        public void SingleLineReadTest()
        {
            //Arrange
            CsvConfig config = new CsvConfig(',', Environment.NewLine, '"');
            string csvString = "a,b,c";
            List<string[]> expected = new List<string[]>();
            string[] line1 = { "a", "b", "c" };
            expected.Add(line1);

            //Act
            List<string[]> result = CsvReader.Read(csvString, config);

            //Expects
            CollectionAssert.AreEqual(result[0], expected[0]);
        }

        [TestMethod()]
        public void SingleLineQuotedReadTest()
        {
            //Arrange
            CsvConfig config = new CsvConfig(',', Environment.NewLine, '"');
            string csvString = "a,\",b,c,\",d";
            List<string[]> expected = new List<string[]>();
            string[] line1 = { "a", ",b,c,", "d" };
            expected.Add(line1);

            //Act
            List<string[]> result = CsvReader.Read(csvString, config);

            //Expects
            CollectionAssert.AreEqual(result[0], expected[0]);
        }

        [TestMethod()]
        public void EmptyStringReadTest()
        {
            //Arrange
            CsvConfig config = new CsvConfig(',', Environment.NewLine, '"');
            string csvString = "";
            List<string[]> expected = new List<string[]>(); 

            //Act
            List<string[]> result = CsvReader.Read(csvString, config);

            //Expects
            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void MultiLineReadTest()
        {
            //Arrange
            CsvConfig config = new CsvConfig(',', Environment.NewLine, '"');
            string csvString = "a,\",b,c,\",d\na,\",b,c,\",d";
            List<string[]> expected = new List<string[]>();
            string[] line1 = { "a", ",b,c,", "d" };
            string[] line2 = { "a", ",b,c,", "d" };
            expected.Add(line1);
            expected.Add(line2);

            //Act
            List<string[]> result = CsvReader.Read(csvString, config);

            //Expects
            CollectionAssert.AreEqual(result[0], expected[0]);
            CollectionAssert.AreEqual(result[1], expected[1]);
        }
    }
}