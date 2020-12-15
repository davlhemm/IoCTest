namespace IoCTest.Model
{
    public abstract class Base : IBase
    {
        public abstract string BaseDo(string txt);
    }

    public class Derived : Base
    {
        public override string BaseDo(string txt)
        {
            return ToString() + ": " + txt;
        }


        public virtual void ParentClassDoesKnow() { }
    }

    public class FurtherDerived : Derived 
    {
        public virtual void ParentClassDontKnow() { }

        public override string BaseDo(string txt)
        {
            return ToString() + ": " + txt + "\nFurther Derived Decoration";
        }
    }
}
