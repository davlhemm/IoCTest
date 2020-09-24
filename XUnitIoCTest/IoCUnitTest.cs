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
            IBase aBase = new Derived();
            DerivedModel aModel = new DerivedModel(aBase);
            string modelString = aModel.Do("TEST");

            Derived thing1 = new Derived();
            List<Type> nest1Types = thing1.GetType().GetNestedTypes().ToList();
            FurtherDerived thing2 = new FurtherDerived();
            List<Type> nest2Types = thing2.GetType().GetNestedTypes().ToList();
            string theString = thing1.Do("Test1 test");
        }
    }
}
