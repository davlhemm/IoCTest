using IoCTest.Interfaces;
using System;
using System.IO;

namespace IoCTest.Model 
{ 
    /// <summary>
    /// Requires a writer, decoupled but not required by interface...
    /// </summary>
    public abstract class Logger : ILogger
    {
        private readonly TextWriter _writer;

        protected Logger() { throw new NotImplementedException("Can't create logger here..."); }

        protected Logger(TextWriter writer)
        {
            _writer = writer;
        }

        public virtual void Log(string message)
        {
            //Don't handle writer disposal here...
            _writer.WriteLine(message);
            _writer.Flush();
            //_writer.Close();
        }
    }

    /// <summary>
    /// Concrete logger implementation for testing
    /// </summary>
    public class FileLogger : Logger
    {
        private readonly string _logFile;

        protected FileLogger()
        {
            throw new NotImplementedException("Can't create logger here...");
        }

        /// <summary>
        /// My documents default location for file.
        /// </summary>
        /// <param name="logFile"></param>
        public FileLogger(string logFile)
            : base(new StreamWriter
            (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                logFile)))
        {
            _logFile = logFile;
        }

        public FileLogger(string logFile, Environment.SpecialFolder specialFolder)
            : base(new StreamWriter
            (Path.Combine(Environment.GetFolderPath(specialFolder),
                logFile)))
        {
            _logFile = logFile;
        }

        public FileLogger(string logFile, TextWriter writer)
            : base(writer)
        {
            _logFile = logFile;
        }
    }

}