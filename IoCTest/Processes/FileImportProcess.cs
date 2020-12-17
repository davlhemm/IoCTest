using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace IoCTest.Processes
{
    public class FileImportProcess
    {
        private readonly string _baseImportPath;
        private readonly string _extension;
        private readonly SearchOption _searchOption;
        
        //private FileImporter Importer { get; set; }

        private FileImportProcess() { }
        
        //TODO: Configurable base path
        public FileImportProcess(string baseImportPath, string extension)
        {
            _baseImportPath = baseImportPath;
            _extension = extension;
            _searchOption = SearchOption.TopDirectoryOnly;
        }

        public FileImportProcess(string baseImportPath, string extension, SearchOption searchOption)
        {
            _baseImportPath = baseImportPath;
            _extension = extension;
            _searchOption = searchOption;
        }

        /// <summary>
        /// Import files from base path with wildcard extension
        /// </summary>
        /// <returns></returns>
        public IList<string> GetImportFiles()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_baseImportPath, "*"+_extension, _searchOption);
            return files.ToList();
        }
    }

    
    public class FileImporter : IFileImporter
    {       
        public void Import()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IFileImporter
    {
        public void Import();
    }
}