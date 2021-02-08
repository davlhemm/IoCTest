using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compression.Interfaces;

namespace IoCTest.Interfaces
{
    public class BackupService : IDisposable
    {
        private readonly IBackup _backupStrategy;
        //TODO: See if we want exposed strat here or just hide behind indifferent adapter (DI)
        public IBackup BackupStrategy => _backupStrategy;

        private BackupService()
        {
            throw new MethodAccessException("Can't make backup service w/no backup strategy.");
        }

        public BackupService(IBackup backupStrategy)
        {
            _backupStrategy = backupStrategy;
        }
        
        public void Dispose() { }
    }

    public class ZipBackup : IBackup
    {
        public void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName)
        {
            //Journey into dependent code...
            ICompress compressionItem = new Compression.Interfaces.Compression(new CompressInfo());
            
            //TODO: Pull compression information up where dependency not embedded so deeply
            //TODO: Decouple compression from backup strategy, inject Compression info and default to particular compression on all backups
            compressionItem.CompressDirectory((IList)listOfFiles, outDirectory, outFileName);
        }

#if !NET5_0
        public void MakeBackup(string inDirectory, string outDirectory, string outFileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(inDirectory);
            MakeBackup(dirInfo.GetFiles().Select(x => x.FullName).ToList(), outDirectory, outFileName);
        }
#endif
    }

    /// <summary>
    /// Per-file backup
    /// TODO: Possibly make this base for any per-file strategy...probably should be distinct
    /// </summary>
    public class BasicBackup : IBackup
    {
        public void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName)
        {
            foreach (string file in listOfFiles)
            {
                Console.WriteLine(file);
                //In Basic backup, append outFileName as extension for creating per-file backups
                //TODO: Delineate backup strats that require/support compression per-file and per-N.
                //      Shouldn't need alternative client work just a sent Compression scheme...
                string theFileXpCompat = Path.GetFileName(file);
                File.Copy(file, outDirectory + "\\" + theFileXpCompat + outFileName, true);
            }
        }

#if !NET5_0
        public void MakeBackup(string inDirectory, string outDirectory, string outFileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(inDirectory);
            MakeBackup(dirInfo.GetFiles().Select(x => x.FullName).ToList(), outDirectory, outFileName);
        }
#endif
    }

    public interface IBackup
    {
        void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName);

        //TODO: Explicit interface implementation (C# 8)
#if NET5_0     //TODO: Use Default if we can but likely won't support
        void MakeBackup(string inDirectory, string outDirectory, string outFileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(inDirectory);
            MakeBackup(dirInfo.GetFiles().Select(x => x.FullName).ToList(), outDirectory, outFileName);
        }
#else
        void MakeBackup(string inDirectory, string outDirectory, string outFileName);
#endif
    }
}