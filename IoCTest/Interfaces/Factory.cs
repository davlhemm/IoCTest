
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace IoCTest
{
    public interface IFactoryItem<T>
    {
        T GetFactoryItem();
        void DoThing();
    }

    public class FactoryItem<T> : IFactoryItem<T>
        where T : new()
    {
        public void DoThing()
        {
            Debug.WriteLine(ToString());
        }

        public T GetFactoryItem()
        {
            return new T();
        }
    }


}