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
        public void TestEnumFactoryDelegated()
        {
            //Create delegate for creating Cat...
            MyItemDescriptor itemDescriptor = new MyItemDescriptor();
            itemDescriptor.Type = MyItemType.Cat;

            //TODO: Create cat creator delegate
            //itemDescriptor.Creator = new MyItemCreationDelegate();

            MyItemFactory factory = new MyItemFactory(new List<MyItemDescriptor>());
            IMyItem item = factory.Create(MyItemType.Cat);
        }

        [Fact]
        public void TestExcelImportBuilder()
        {
            var itemList = new List<ITestItem>()
            { 
                new TestItem(){ ProposedKey = "Key1", RawValue = "Value1", StoredType = typeof(string)},
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
            //TODO: Classify required info for this backup process, inject
            string basePath = Environment.GetEnvironmentVariable("userprofile") + @"\Documents\LLDataPrcessor\Test\";
            string searchPattern = @"*.DWG";
            string dateStringFormat = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string backExt = dateStringFormat + ".bak";
            string zipName = $"{"LLDPDwgBackup"}" +
                             dateStringFormat +
                             $"{".zip"}";
            
            IList<string> files = Directory.GetFiles(
                basePath,
                searchPattern,
                SearchOption.AllDirectories);

            // DI compatibility tested in MEDI
            // Ex: services.AddSingleton<IBackup>(x => new ZipBackup());
            IBackup dumbBackup = new BasicBackup();
            IBackup zipBackup = new ZipBackup();

            using (BackupService backup = new BackupService(dumbBackup))
            {
                backup.BackupStrategy.MakeBackup(basePath, basePath, backExt);
            }
            using (BackupService backup = new BackupService(zipBackup))
            {
                backup.BackupStrategy.MakeBackup(files, basePath, zipName);
            }

            Assert.True(dumbBackup.GetType() == typeof(BasicBackup) &&
                         zipBackup.GetType() == typeof(ZipBackup));
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
            FileImport importProcess = new FileImport(new FileImportInfo("C:\\RDS", ".xlsx"));
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
