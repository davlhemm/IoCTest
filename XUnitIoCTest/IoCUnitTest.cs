using System;
using Xunit;
using IoCTest;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using IoCTest.Model;
using IoCTest.Interfaces;
using IoCTest.Processes;

namespace XUnitIoCTest
{
    public class IoCUnitTest
    {
        [Fact]
        public void TestModeCreate()
        {
            IMode aMode = ModeFactoryService.CreateByMode(FactoryMode.First);
            string theType = aMode.GetType().AssemblyQualifiedName;
        }

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
        public void LoggerDependencyTest()
        {
            ILogger log = new FileLogger("LogFile.txt");
            log.Log("First line");
            log.Log("Second line");

            ILogger logTwo = new FileLogger("AnotherLog.txt");
            logTwo.Log("Line");
        }

        [Fact]
        public void TestFileImport()
        {
            FileImportProcess importProcess = new FileImportProcess("C:\\RDS", ".xlsx");
            IList<string> testFiles = importProcess.GetImportFiles();

            foreach (var file in testFiles)
            {
                List<LineListWriterInfo> linelistWrites = new List<LineListWriterInfo>
                {
                    new LineListWriterInfo()
                    {
                        Name = "Name",
                        Format = "{0}",
                        Index = new List<string>() { "1" }
                    }
                };
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

            [Fact]
            public void DynDictTest()
            {
                Dictionary<string, object> testDict = new Dictionary<string, object>();
                testDict.Add("1", 1);
                object test;
                testDict.TryGetValue("1", out test);
            }
        }

        public class FactoryTest
        {
            [Fact]
            public void TestModeCreate()
            {
                IMode aMode = ModeFactoryService.CreateByMode(FactoryMode.First);
                string theType = aMode.GetType().AssemblyQualifiedName;
            }
        }
    }
}
