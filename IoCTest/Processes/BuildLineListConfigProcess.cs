
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace  IoCTest.Processes
{
    public class BuildLineListConfig
    {

    }

    public class LineListConfig<T>: ILineListConfig<T>
    {

    }

    public interface ILineListConfig<T>
    {
    }

    //TODO: Support composites
    //TODO: Support regex transformations
    public class LineListWriterInfo
    {
        #region Constructors

        public LineListWriterInfo()
        {
            Index = new List<string>();
        }

        //Module: Values too primitive here and/or need regex/writers
        public LineListWriterInfo(string name, string value = "", string format = "{0}") : this()
        {
            Name = name;
            Format = format;
            Value = value;
        }

        #endregion Constructors


        #region Properties


        public string Format { get; set; }

        //TODO: Utilize for variable write, or move up to alternative config item.
        public Regex Modifier { get; set; }

        public string Value { get; set; }

        public List<string> Index { get; set; }

        public string Name { get; set; }

        #endregion Properties


        public override string ToString()
        {
            string formatIndeces = "";
            foreach (string formIndex in this.Index)
                formatIndeces += formIndex + "-";
            formatIndeces = formatIndeces.Trim(' ', '-');

            return "Name: " + this.Name + " " +
                   "Excel Columns: " + formatIndeces + " " +
                   "Format: " + this.Format + " " +
                   "Value: " + this.Value;
        }
    }
}