using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IoCTest.Interfaces
{
    public interface IFileImporter
    {
        IFileImportInfo ImportInfo { get; }
        IList<string> GetImportFiles();
    }

    /// <summary>
    /// <para>File Import Process should be what's related to individual file imports...</para>
    /// <remarks>NOTE: Replace with a builder/factory for file importers</remarks>
    /// </summary>
    public class FileImporter : IFileImporter
    {
        private readonly IFileImportInfo _importInfo;
        //TODO: Default ImportInfo settings here or within...
        public IFileImportInfo ImportInfo => _importInfo;


        private FileImporter() { }
        
        //TODO: Configurable base path
        public FileImporter(IFileImportInfo importInfo)
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

    public interface IFileImportInfo
    {
        string BaseImportPath { get; }
        string SearchPattern { get; }
        SearchOption SearchOption { get; }
    }

    public class FileImportInfo : IFileImportInfo
    {
        public string BaseImportPath { get; }
        public string SearchPattern { get; }
        public SearchOption SearchOption { get; } = SearchOption.TopDirectoryOnly;

        /// <summary>
        /// Default location and search pattern use C drive and all files
        /// </summary>
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

        public FileImportInfo(string baseImportPath, string searchPattern, SearchOption recurse)
        :this(baseImportPath,searchPattern)
        {
            SearchOption = recurse;
        }
    }
}