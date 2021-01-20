using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace IoCTest.Interfaces
{
    public class BackupService : IBackup, IDisposable
    {
        private readonly IBackup _backupStrategy;

        public BackupService(IBackup backupStrategy)
        {
            _backupStrategy = backupStrategy;
        }

        //public void SetBackupStrat(IBackup backupStrategy)
        //{
        //    _backupStrategy = backupStrategy;
        //}
        
        public void Dispose() { }

        public void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName)
        {
            _backupStrategy.MakeBackup(listOfFiles, outDirectory, outFileName);
        }

        public void MakeBackup(string inDirectory, string outDirectory, string outFileName)
        {
            _backupStrategy.MakeBackup(inDirectory, outDirectory, outFileName);
        }

        //public string GetDirectory(string whereTo)
        //{
        //    FolderBrowserDialog dialog = new FolderBrowserDialog { Description = whereTo };
        //    DialogResult result = dialog.ShowDialog();

        //    return GetDirectory(dialog.SelectedPath, result);
        //}

        //public string GetDirectory(string selectedPath, DialogResult result)
        //{
        //    return result == DialogResult.OK ? selectedPath : string.Empty;
        //}
    }

    public class ZipBackup : IBackup
    {
        public void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName)
        {
            //Journey into dependent code...
            CompressDirectory(listOfFiles, outDirectory, outFileName);
        }

        /// <summary>
        /// Method that compress all the files inside a folder (non-recursive) into a zip file.
        /// </summary>
        /// <param name="files"></param>
        /// <param name="outDirectory"></param>
        /// <param name="outFileName">Used as explicit name for strategies that use single file and an extension for those that do per-file.</param>
        /// <param name="compressionLevel"></param>
        private static void CompressDirectory(IList<string> files, string outDirectory, string outFileName, int compressionLevel = 9)
        {
            try
            {
                // 'using' statements guarantee the stream is closed properly which is a big source
                // of problems otherwise. Exception safe.

                //Zip is a single location/file so append default naming scheme
                //Done: TODO: Define naming schema elsewhere this is big dumb and decoupled from helper lib
                //string zipName = $"{"LLDPDwgBackup"}" +
                //                 DateTime.Now.ToString("yyyyMMdd-HHmmss") +
                //                 $"{".zip"}";

                //Define output stream based on full-path filename
                using (ZipOutputStream outputStream = new ZipOutputStream(File.Create(outDirectory + "\\" + outFileName))) //zipName)))
                {
                    // Define the compression level
                    // 0 - store only to 9 - means best compression
                    outputStream.SetLevel(compressionLevel);

                    byte[] buffer = new byte[4096];

                    foreach (var file in files)
                    {

                        // Using GetFileName makes the result compatible with XP
                        // as the resulting path is not absolute.
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        // Setup the entry data as required.

                        // Crc and size are handled by the library for seekable streams
                        // so no need to do them here.

                        // Could also use the last write time or similar for the file.
                        entry.DateTime = DateTime.Now;
                        outputStream.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            // Using a fixed size buffer here makes no noticeable difference for output
                            // but keeps a lid on memory usage.
                            int sourceBytes;

                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                outputStream.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }

                    // Finish/Close aren't needed strictly as the using statement does this automatically

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    outputStream.Finish();

                    // Close is important to wrap things up and unlock the file.
                    outputStream.Close();

                    Console.WriteLine("Files successfully compressed");
                }
            }
            catch (Exception ex)
            {
                // No need to rethrow the exception as for our purposes its handled.
                Console.WriteLine("Exception during processing {0}", ex);
            }
        }
    }

    public class BasicBackup : IBackup
    {
        public void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName)
        {
            foreach (string file in listOfFiles)
            {
                Console.WriteLine(file);
                //In Basic backup, append outFileName as extension for creating per-file backups
                string theFileXpCompat = Path.GetFileName(file);
                File.Copy(file, outDirectory + "\\" + theFileXpCompat + outFileName, true);
            }
        }
    }

    public interface IBackup
    {
        void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName);

        //TODO: Explicit interface implementation (C# 8)
        void MakeBackup(string inDirectory, string outDirectory, string outFileName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(inDirectory);
            MakeBackup(dirInfo.GetFiles().Select(x => x.FullName).ToList(),outDirectory, outFileName);
        }
    }


}