using System;

namespace IoCTest
{
    public interface IBase 
    {
        string Do(string txt);
    }

    public abstract class Base : IBase
    {
        public abstract string Do(string txt);
    }

    public class Derived : Base
    {
        public override string Do(string txt)
        {
            return ToString() + ": " + txt;
        }


        public virtual void ParentClassDoesKnow() { }
    }

    public class FurtherDerived : Derived 
    {
        public virtual void ParentClassDontKnow() { }
    }
}
