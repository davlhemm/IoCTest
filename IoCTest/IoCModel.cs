using System;

namespace IoCTest
{
    public abstract class BaseModel { }

    public class DerivedModel : BaseModel
    {
        private readonly IBase messageDumper;

        public DerivedModel(IBase dumper)
        {
            messageDumper = dumper;
        }

        public virtual string Do(string txt) 
        { 
            return messageDumper.Do(ToString() + " called: " + txt); 
        }
    }
}
