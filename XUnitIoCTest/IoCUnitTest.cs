using System;
using Xunit;
using IoCTest;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace XUnitIoCTest
{
    public class IoCUnitTest
    {
        [Fact]
        public void Test1()
        {
            IBase aBase = new Derived();
            DerivedModel aModel = new DerivedModel(aBase);
            string modelString = aModel.Do("TEST");

            Derived thing1 = new Derived();
            List<Type> nest1Types = thing1.GetType().GetNestedTypes().ToList();
            FurtherDerived thing2 = new FurtherDerived();
            List<Type> nest2Types = thing2.GetType().GetNestedTypes().ToList();
            string theString = thing1.BaseDo("Test1 test");
        }

        [Fact]
        public void ExplicitDependencyTest()
        {
            //Logger log = new Logger(new StreamWriter(new FileStream());
            Logger log = new FileLogger("LogFile.txt", Environment.SpecialFolder.Desktop);
            log.Log("LOG DATA");
        }

        public class FileLogger : Logger
        {
            public readonly string _logFile;

            protected FileLogger() { }

            public FileLogger(string logFile)
                : base(new StreamWriter
                      (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), logFile))) 
            {
                _logFile = logFile;
            }

            public FileLogger(string logFile, Environment.SpecialFolder specialFolder)
                : base(new StreamWriter(Path.Combine(Environment.GetFolderPath(specialFolder), logFile)))
            {
                _logFile = logFile;
            }
        }

        public interface ILogger 
        {
            void Log(string message); 
        }

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

            public void Log(string message)
            {
                //Don't handle writer disposal here...
                _writer.WriteLine(message);
                _writer.Flush();
                _writer.Close();
            }
        }
    }


    public class StratTester
    {
        [Fact]
        public void StratTest()
        {
            StratContext aStrat = new StratContext(new ConcreteStrategyA());
            StratContext bStrat = new StratContext(new ConcreteStrategyB());
            
            aStrat.DoStrat();
            bStrat.DoStrat();
        }
    }


    public class DynamicTester
    {
        [Fact]
        public void DynamicTest()
        {
            DelegateInvoker<int> invoker = new DelegateInvoker<int>();
            invoker.BuildDynamic();
        }
    }
}
