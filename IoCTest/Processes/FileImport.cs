using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using IoCTest.Interfaces;

namespace IoCTest.Processes
{
    /// <summary>
    /// <para>File Import Process should be what's related to individual file imports...</para>
    /// <remarks>NOTE: Replace with a builder/factory for file importers</remarks>
    /// </summary>
    public class FileImport : IFileImport
    {
        private readonly IFileImportInfo _importInfo;
        public IFileImportInfo ImportInfo => _importInfo;


        private FileImport() { }
        
        //TODO: Configurable base path
        public FileImport(IFileImportInfo importInfo)
        {
            _importInfo = importInfo;
        }
        

        /// <summary>
        /// Import files from base path with wildcard extension
        /// </summary>
        /// <returns></returns>
        public IList<string> GetImportFiles()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_importInfo.BaseImportPath, _importInfo.SearchPattern, _importInfo.SearchOption);
            return files.ToList();
        }
    }

    public interface IFileImport
    {
        IFileImportInfo ImportInfo { get; }
        IList<string> GetImportFiles();
    }

    public class FileImportInfo : IFileImportInfo
    {
        public string BaseImportPath { get; }
        public string SearchPattern { get; }
        public SearchOption SearchOption { get; } = SearchOption.TopDirectoryOnly;

        public FileImportInfo()
        {
            BaseImportPath = "C:\\";
            SearchPattern = "*";
        }

        public FileImportInfo(string baseImportPath, string searchPattern)
        {
            BaseImportPath = baseImportPath;
            SearchPattern = searchPattern;
        }
    }

    public interface IFileImportInfo
    {
        string BaseImportPath { get; }
        string SearchPattern { get; }
        SearchOption SearchOption { get; }
    }
}