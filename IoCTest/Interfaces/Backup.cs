using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace NewLLDP.Abstracts
{
    public class BackupService : IDisposable
    {
        private readonly IBackup _backupTool;

        public BackupService(IBackup backupTool)
        {
            _backupTool = backupTool;
        }
        
        public void Dispose()
        {
        }
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
        /// <param name="outputFilePath"></param>
        /// <param name="outFileName"></param>
        /// <param name="compressionLevel"></param>
        private static void CompressDirectory(IList<string> files, string outputFilePath, string outFileName, int compressionLevel = 9)
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
                using (ZipOutputStream outputStream = new ZipOutputStream(File.Create(outputFilePath + "\\" + outFileName))) //zipName)))
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
            foreach (string item in listOfFiles)
            {
                Console.WriteLine(item);
            }
        }
    }

    public interface IBackup
    {
        void MakeBackup(IList<string> listOfFiles, string outDirectory, string outFileName);
    }


}