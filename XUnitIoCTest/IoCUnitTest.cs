using System;
using Xunit;
using IoCTest;
using System.Linq;
using System.Collections.Generic;

namespace XUnitIoCTest
{
    public class IoCUnitTest
    {
        [Fact]
        public void Test1()
        {
            IBase aBase = new MessageDumper();
            DerivedModel aModel = new DerivedModel(aBase);
            string modelString = aModel.Do("TEST");

            MessageDumper thing1 = new MessageDumper();
            List<Type> nest1Types = thing1.GetType().GetNestedTypes().ToList();
            OtherMessageDumper thing2 = new OtherMessageDumper();
            List<Type> nest2Types = thing2.GetType().GetNestedTypes().ToList();
            string theString = thing1.Do("Test1 test");
        }
    }
}
