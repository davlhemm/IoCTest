using System;
using System.Diagnostics;

namespace IoCTest.Interfaces
{
    public interface IFactoryItem<out T>
    {
        T GetFactoryItem();
        void DoThing();
    }

    public class FactoryItem<T> : IFactoryItem<T>
        where T : new()
    {
        public T GetFactoryItem()
        {
            return new T();
        }

        public void DoThing()
        {
            Debug.WriteLine(ToString());
        }
    }


    /*
     *Need import to be trivialized for user
     * Support Regex to alter item
     * Mutator delegates stored for runtime use
     * Backing item as raw string
     * Type-sensitive at runtime for interpreting
     */
    
    /// <summary>
    /// This the progenitor of different typed items...
    /// <para>See what options exist for type-ambiguity instead of declarative</para>
    /// </summary>
    public enum DataItemVariation
    {
        Default,
        First,
        Second,
        Third
    }


    public class DataItemFactory
    {
        /// <summary>
        /// Generate a data item based on declarative enum
        /// </summary>
        /// <param name="enumQualifier"></param>
        /// <returns></returns>
        public static IDataItem<T> CreateDataItem<T>(DataItemVariation enumQualifier)
        where T: new()
        {
            T tItem = new T();
            return new DataItem<T>(tItem, enumQualifier);
        }
    }

    public class DataItem<T> : IDataItem<T>
    {
        public DataItem(T item, DataItemVariation dataEnum)
        {
            Item = item;
            DataItemEnum = dataEnum;
        }

        public T Item { get; set; }
        
        public DataItemVariation DataItemEnum { get; set; }
    }

    public interface IDataItem<out T>
    {
        T Item { get; }
        DataItemVariation DataItemEnum { get; }
    }

    public interface IEnumFactory
    {
        
    }
}