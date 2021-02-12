
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace IoCTest
{
    /// <summary>
    /// <para>May become monolithic configurable for Excel items related to line list import...</para>
    /// <para>But needs support for alternative items such as item->conjugate dicts</para>
    /// </summary>
    public class ExcelImport<TExcelProvider> : IExcelImport<TExcelProvider>
    {
        private readonly string _fullExcelFilePath;
        private readonly IExcelProvider _provider;

        public string FullExcelFilePath => _fullExcelFilePath;

        public IExcelProvider Provider => _provider;

        private ExcelImport() { }

        /// <summary>
        /// IoC compatibility
        /// </summary>
        /// <param name="fullExcelFilePath"></param>
        /// <param name="provider"></param>
        public ExcelImport(string fullExcelFilePath, IExcelProvider provider)
        {
            _fullExcelFilePath = fullExcelFilePath;
            _provider = provider;
        }

        public bool IsEndOfFile()
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Define what's needed to yield a generic input/item from an excel file
    /// </summary>
    ///
    
    
    public interface IExcelImport<TExcelProvider>
    {
        string FullExcelFilePath { get; }
        IExcelProvider Provider { get; }
        bool IsEndOfFile();
    }


    /// <summary>
    /// Serves as an adapter to whatever desired excel impl is needed for homogenized access
    /// </summary>
    public interface IExcelProvider
    {
        IExcelWorkbook GetWorkbook(string excelFilePath, FileMode mode, FileAccess access, FileShare share);
    }

    public interface IExcelWorkbook
    {

    }
}