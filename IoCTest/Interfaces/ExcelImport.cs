
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace IoCTest
{
    public class ExcelImport : IExcelImport
    {
        public string FullExcelFilePath { get; }


    }

    /// <summary>
    /// Define what's needed to yield a generic input/item from an excel file
    /// </summary>
    public interface IExcelImport
    {
        string FullExcelFilePath { get; }

    }
}