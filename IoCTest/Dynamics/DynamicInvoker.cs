
using System;
using System.Reflection.Emit;

namespace IoCTest
{
    /// <summary>
    /// Testing Delegates for Dynamic Methods
    /// <para>Some are generic some aren't.</para>
    /// </summary>
    public class DelegateInvoker<T>
        where T: new()
    {
        private delegate int DoubledInvoker(int input);
        private delegate TR ReturnDoubled<TR>(T input);

        Type[] methodArgs;

        DelegateInvoker()
        {
            var a = new T();
            methodArgs = new Type[]{a.GetType()};
        }

        public void BuildDynamic()
        {
            DynamicMethod dynamic = new DynamicMethod(
                "Doubled", 
                typeof(int), 
                methodArgs, 
                typeof(ConcreteStrategyA).Module);
        }
    }
}