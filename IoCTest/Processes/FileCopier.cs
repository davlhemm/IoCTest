

using System.Collections;

namespace IoCTest.Processes
{

    public class FileCopyService
    {
        private readonly IFileCopier _copier;
        
        public FileCopyService(IFileCopier copier)
        {
            _copier = copier;
        }
    }

    public interface IFileCopier
    {
        string OriginalFilename { get; }
        string NewFilename { get; }
        string OriginFolder { get; }
        string DestinationFolder { get; }
        bool OverridePreviousFile();
    }
}