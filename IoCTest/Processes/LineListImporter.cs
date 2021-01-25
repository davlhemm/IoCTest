using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IoCTest.Helpers;
using IoCTest.Processes;
using OfficeOpenXml;


namespace IoCTest.Processes
{
    /// <summary>
    /// We need a class to handle (in a homogenized, configurable fashion)
    ///  the import of a line-list from an amorphous service and
    ///  make coherent contiguous elements of said line-list "items"
    /// </summary>
    public class LineListImporter<T,TR> : ILineListImporter<T,TR>
    {

        ILineListProvider<T> ILineListImporter<T, TR>.Provider { get; } = default;
        IList<ILineListConfig<TR>> ILineListImporter<T, TR>.ConfigItems { get; } = default;
    }

    /// <summary>
    /// Takes a line-list source (database, excel, or other provider) and derives elements
    /// </summary>
    public interface ILineListImporter<out T, TR>
    {
        ILineListProvider<T> Provider { get; }
        IList<ILineListConfig<TR>> ConfigItems { get; }
    }

    public interface ILineListProvider<out T>
    {
        T GetLineListItem();
    }

    public interface ILineListItem
    {
        string Key { get; }
        string RawValue { get; }
    }
}