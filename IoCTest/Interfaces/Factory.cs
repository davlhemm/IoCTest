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
    

    public enum DataItemVariation
    {
        First,
        Second,
        Third,
        Default
    }


    public class DataItemFactory<T> 
        where T : new()
    {
        /// <summary>
        /// Generate a data item based on declarative enum
        /// </summary>
        /// <param name="enumQualifier"></param>
        /// <returns></returns>
        public static IDataItem<T> CreateDataItem(DataItemVariation enumQualifier)
        {
            switch (enumQualifier)
            {
                case DataItemVariation.First:
                    return new DataItem<T>();
                default:
                    return new DataItem<T>();
            }
        }
    }

    public class DataItem<T> : IDataItem<T>
    {
        public T Item => throw new NotImplementedException();

        public int EnumAssociatedWithItemCreation => throw new NotImplementedException();
    }

    public interface IDataItem<out T>
    {
        T Item { get; }
        int EnumAssociatedWithItemCreation { get; }
    }

    public interface IEnumFactory
    {
        
    }
}