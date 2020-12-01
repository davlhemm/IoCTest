
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
        private delegate TR ReturnDoubled<TR,TP>(TP input);

        Type[] methodArgs = default;

        public DelegateInvoker()
        {
            var a = new T();
            methodArgs = new Type[]{a.GetType()};
        }

        public void BuildDynamic()
        {
            DynamicMethod doubleIt = new DynamicMethod(
                "DoubleIt", 
                typeof(T), 
                methodArgs, 
                typeof(DelegateInvoker<T>).Module);

            ILGenerator iLGen = doubleIt.GetILGenerator();
            iLGen.Emit(OpCodes.Ldarg_0);
            iLGen.Emit(OpCodes.Conv_I8);
            iLGen.Emit(OpCodes.Dup);
            iLGen.Emit(OpCodes.Add); //See wtf this is even doing...straight opcodes lol
            iLGen.Emit(OpCodes.Ret);


        }
    }
}