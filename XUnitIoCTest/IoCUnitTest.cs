using System;
using Xunit;
using IoCTest;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IoCTest.Model;
using IoCTest.Interfaces;
using IoCTest.Processes;

using System.Windows;

namespace XUnitIoCTest
{
    public class IoCUnitTest
    {
        [Fact]
        public void TestExcelImportBuilder()
        {
            var itemList = new List<ITestItem>()
            { 
                new TestItem(){ ProposedKey = "Key1", RawValue = "Value", StoredType = typeof(string)},
                new TestItem(){ ProposedKey = "Key1", RawValue = "Value2", StoredType = typeof(string)},
                new TestItem(){ ProposedKey = "Key2", RawValue = "Value3", StoredType = typeof(string)},
                new TestItem(){ ProposedKey = "Key3", RawValue = "Value4", StoredType = typeof(string)}
            };

            //Test LINQ distinctness
            var itemListSifted = itemList.GroupBy(x => x.ProposedKey,x=>x).Select(s=>s.First()).ToList();

            //Create a dictionary out of obvious key, keep item intact as value
            var itemDict = itemListSifted.Distinct().ToDictionary(x => x.ProposedKey, item => item);
        }

        /// <summary>
        /// Stupid POCO for dict testing
        /// </summary>
        internal class TestItem : ITestItem
        {
            public string ProposedKey { get; set; }
            public string RawValue { get; set; }
            public Type StoredType { get; set; }
        }

        internal interface ITestItem
        {
            string ProposedKey { get; }
            string RawValue { get; }
            Type StoredType { get; }
        }

        [Fact]
        public void TestBackup()
        {
            string basePath = @"C:\Users\dhemmenway\Documents\LLDataPrcessor\Test\";
            string searchPattern = @"*.DWG";
            IList<string> files = Directory.GetFiles(
                basePath,
                searchPattern,
                SearchOption.AllDirectories);
            string dateStringFormat = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string zipName = $"{"LLDPDwgBackup"}" +
                             dateStringFormat +
                             $"{".zip"}";
            string backExt = dateStringFormat + ".bak";

            using (BackupService backup = new BackupService(new BasicBackup()))
            {
                backup.MakeBackup(basePath, basePath, backExt);
            }
            using (BackupService backup = new BackupService(new ZipBackup()))
            {
                backup.MakeBackup(files, basePath, zipName);
            }
        }

        [Fact]
        public void TestModeCreate()
        {
            IMode aMode = ModeFactoryService.CreateByMode(FactoryMode.First);
            string theType = aMode.GetType().AssemblyQualifiedName;
        }

        [Fact]
        public void TestDerived()
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
    }
}
