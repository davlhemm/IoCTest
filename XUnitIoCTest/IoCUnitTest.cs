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
        public void TestExpressionBasics()
        {
            string paddedEx = "TEST".PadLeft(30,'~');

        }


        [Fact]
        public void TestEnumFactoryDelegated()
        {
            //Create delegate for creating Cat...
            AnimalDescriptor catDescriptor    = new AnimalDescriptor { Type = Animal.Cat,    Creator = () => new Cat()};
            AnimalDescriptor dogDescriptor    = new AnimalDescriptor { Type = Animal.Dog,    Creator = () => new Dog() };
            AnimalDescriptor horseDescriptor  = new AnimalDescriptor { Type = Animal.Horse,  Creator = () => new Horse() };
            AnimalDescriptor personDescriptor = new AnimalDescriptor { Type = Animal.Person, Creator = () => new Person() };

            IList<AnimalDescriptor> descriptors = new List<AnimalDescriptor>();
            descriptors.Add(catDescriptor);
            descriptors.Add(dogDescriptor);
            descriptors.Add(horseDescriptor);
            descriptors.Add(personDescriptor);

            AnimalFactory factory = new AnimalFactory(descriptors);

            IAnimal dogItem = factory.Create(Animal.Dog);
            //Dog has a primitive that should default to 4
            Assert.True(dogItem.Legs == 4);

            IAnimal personItem = factory.Create(Animal.Person);
            Assert.True(personItem.Legs == 2);
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
            #region FileImport
            
            //TODO: Classify required info for this backup process, inject
            string basePath = Environment.GetEnvironmentVariable("userprofile") + @"\Documents\LLDataPrcessor\Test\";
            string searchPattern = @"*.DWG";

            IFileImportInfo importInfo = new FileImportInfo(basePath, searchPattern);
            IFileImporter importer = new FileImporter(importInfo);

            IList<string> files = importer.GetImportFiles();

            #endregion


            //TODO: Use expression evaluation at runtime to allow this config...
            string dateStringFormat = $"{DateTime.Now:yyyyMMdd-HHmmss}";
            string backupExt = dateStringFormat + ".bak";
            string zipName = $"{"LLDPDwgBackup"}{dateStringFormat}{".zip"}";


            // DI compatibility tested in MEDI
            // Ex: services.AddSingleton<IBackup>(x => new ZipBackup());
            IBackup dumbBackup = new BasicBackup();
            IBackup zipBackup  = new ZipBackup();

            using (BackupService backup = new BackupService(dumbBackup))
            {
                backup.BackupStrategy.MakeBackup(basePath, basePath, backupExt);
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
            FileImporter importerProcess = new FileImporter(new FileImportInfo("C:\\RDS", ".xlsx"));
            IList<string> testFiles = importerProcess.GetImportFiles();

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
                IStrategyBase stratA = new ConcreteStrategyA();
                IStrategyBase stratB = new ConcreteStrategyB();
                StratContext stratAContext = new StratContext(stratA);
                StratContext stratBContext = new StratContext(stratB);

                stratAContext.DoStrat();
                stratBContext.DoStrat();
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
