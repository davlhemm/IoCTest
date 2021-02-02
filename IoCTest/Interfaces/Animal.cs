using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace IoCTest.Interfaces
{
    public enum MyItemType
    {
        Person = 0,
        Dog = 1,
        Cat = 2,
        Horse = 3
    }

    public delegate IMyItem MyItemCreationDelegate();

    public interface IMyItem
    {
        int Legs { get; }
    }

    public class MyItemDescriptor
    {
        public MyItemType Type;

        // This ends up being a delegate
        public MyItemCreationDelegate Creator;
    }

    public class MyItemFactory
    {
        private readonly IList<MyItemDescriptor> _creatorList;

        public MyItemFactory(IList<MyItemDescriptor> creators)
        {
            _creatorList = creators;
        }

        public IMyItem Create(MyItemType type)
        {
            MyItemCreationDelegate creator = _creatorList.FirstOrDefault(x => x.Type == type)?.Creator;
            if (creator != null) return creator();
            else
            {
                throw new NullReferenceException("Can't create delegate due to lack of creator type in Create(...)");
            }
        }
    }
}