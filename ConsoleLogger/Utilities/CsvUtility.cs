using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleLogger.Utilities
{

    //CSV parser from https://growlofowl.com/2016/11/18/simple-csv-parser-in-c-with-comma-in-cell-support/, with minor modification
    public class CsvConfig
    {
        public char Delimiter { get; private set; }
        public string NewLineMark { get; private set; }
        public char QuotationMark { get; private set; }

        public CsvConfig(char delimiter, string newLineMark, char quotationMark)
        {
            Delimiter = delimiter;
            NewLineMark = newLineMark;
            QuotationMark = quotationMark;
        }

        // useful configs

        public static CsvConfig Default
        {
            get { return new CsvConfig(',', "\r\n", '\"'); }
        }

        // etc.
    }

    public static class CsvReader
    {
        
        public static List<string[]> Read(string csvFileContents, CsvConfig m_config)
        {
            List<string[]> result = new List<string[]>();
            using (StringReader reader = new StringReader(csvFileContents))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    result.Add(ParseLine(line, m_config));
                }
            }
            return result;
        }

        private static string[] ParseLine(string line, CsvConfig m_config)
        {
            Stack<string> result = new Stack<string>();

            int i = 0;
            while (true)
            {
                string cell = ParseNextCell(line, ref i, m_config);
                if (cell == null)
                    break;
                result.Push(cell);
            }

            // remove last elements if they're empty
            while (string.IsNullOrEmpty(result.Peek()))
            {
                result.Pop();
            }

            var resultAsArray = result.ToArray();
            Array.Reverse(resultAsArray);
            return resultAsArray;
        }

        // returns iterator after delimiter or after end of string
        private static string ParseNextCell(string line, ref int i, CsvConfig m_config)
        {
            if (i >= line.Length)
                return null;

            if (line[i] != m_config.QuotationMark)
                return ParseNotEscapedCell(line, ref i, m_config);
            else
                return ParseEscapedCell(line, ref i, m_config);
        }

        // returns iterator after delimiter or after end of string
        private static string ParseNotEscapedCell(string line, ref int i, CsvConfig m_config)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (i >= line.Length) // return iterator after end of string
                    break;
                if (line[i] == m_config.Delimiter)
                {
                    i++; // return iterator after delimiter
                    break;
                }
                sb.Append(line[i]);
                i++;
            }
            return sb.ToString();
        }

        // returns iterator after delimiter or after end of string
        private static string ParseEscapedCell(string line, ref int i, CsvConfig m_config)
        {
            i++; // omit first character (quotation mark)
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (i >= line.Length)
                    break;
                if (line[i] == m_config.QuotationMark)
                {
                    i++; // we're more interested in the next character
                    if (i >= line.Length)
                    {
                        // quotation mark was closing cell;
                        // return iterator after end of string
                        break;
                    }
                    if (line[i] == m_config.Delimiter)
                    {
                        // quotation mark was closing cell;
                        // return iterator after delimiter
                        i++;
                        break;
                    }
                    if (line[i] == m_config.QuotationMark)
                    {
                        // it was doubled (escaped) quotation mark;
                        // do nothing -- we've already skipped first quotation mark
                    }

                }
                sb.Append(line[i]);
                i++;
            }

            return sb.ToString();
        }
    }
}
