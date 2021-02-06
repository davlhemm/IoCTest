using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Compression
{
    public static class CompressionStrengthEnum
    {
        public const string None     = nameof(None);
        public const string Weak     = nameof(Weak);
        public const string Moderate = nameof(Moderate);
        public const string Strong   = nameof(Strong);
        public const string Intense  = nameof(Intense);
    }
    /// <summary>
    /// One side of contract for strength of compression vs myriad of compression strategies
    /// </summary>
    public enum CompressionStrength
    {
        None,
        Weak,
        Moderate,
        Strong,
        Intense
    }

    public class CompressInfo : ICompressInfo
    {
        private CompressionStrength _strength;
        public CompressionStrength Strength
        {
            get => _strength;
            set => _strength = value;
        }
    }

    public interface ICompressInfo
    {
        CompressionStrength Strength { get; }
    }


    /// <summary>
    /// Class that will handle all things simple compression
    /// <para>Different strategies should be supported for disparate libraries</para>
    /// </summary>
    public static class Compression
    {
        public static CompressionStrength CompressStrengthContract(this int compressionLevel)
        {
            switch (compressionLevel)
            {
                case 0:
                    return CompressionStrength.None;
                case 1:
                case 2:
                    return CompressionStrength.Weak;
                case 3:
                case 4:
                case 5:
                    return CompressionStrength.Moderate;
                case 6:
                case 7:
                case 8:
                    return CompressionStrength.Strong;
                case 9:
                    return CompressionStrength.Intense;
                default:
                    return CompressionStrength.Moderate;
            }
        }

        /// <summary>
        /// Represents homogenized values for this Compression strategy
        /// </summary>
        /// <param name="compressionLevel"></param>
        /// <returns>Integer representation for SharpZipLib</returns>
        public static int CompressStrengthContract(this CompressionStrength compressionLevel)
        {
            switch (compressionLevel)
            {
                case CompressionStrength.None:
                    return 0;
                case CompressionStrength.Weak:
                    return 2;
                case CompressionStrength.Moderate:
                    return 4;
                case CompressionStrength.Strong:
                    return 7;
                case CompressionStrength.Intense:
                    return 9;
                default:
                    return 5;
            }
        }


        public static void CompressDirectory(IList<string> files, string outDirectory, string outFileName,
                                                CompressionStrength compressionLevel = CompressionStrength.Intense)
        {
            CompressDirectory(files, outDirectory, outFileName, compressionLevel.CompressStrengthContract());
        }
        
        /// <summary>
        /// Method that compress all the files inside a folder (non-recursive) into a zip file.
        /// </summary>
        /// <param name="files"></param>
        /// <param name="outDirectory"></param>
        /// <param name="outFileName">Used as explicit name for strategies that use single file and an extension for those that do per-file.</param>
        /// <param name="compressionLevel"></param>
        public static void CompressDirectory(IList<string> files, string outDirectory, string outFileName, int compressionLevel)
        {
            try
            {
                // 'using' statements guarantee the stream is closed properly which is a big source
                // of problems otherwise. Exception safe.

                //Define output stream based on full-path filename
                using (ZipOutputStream outputStream = new ZipOutputStream(File.Create(outDirectory + "\\" + outFileName))) //zipName)))
                {
                    // Define the compression level
                    // 0 - store only to 9 - means best compression
                    //Done: homogenized enum for compression strength
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
#if DEBUG
                    Debug.WriteLine("Files successfully compressed");
#endif
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                // No need to rethrow the exception as for our purposes its handled.
                Debug.WriteLine("Exception during processing {0}", ex);
#endif
            }
        }
    }
}
