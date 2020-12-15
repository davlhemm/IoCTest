using IoCTest.Interfaces;
using System;
using System.IO;

namespace IoCTest.Model 
{ 
/// <summary>
/// Requires a writer, decoupled but not required by interface...
/// </summary>
public abstract class Logger : ILogger
{
    private readonly TextWriter _writer;

    protected Logger() { throw new NotImplementedException("Can't create logger here..."); }

    protected Logger(TextWriter writer)
    {
        _writer = writer;
    }

    public virtual void Log(string message)
    {
        //Don't handle writer disposal here...
        _writer.WriteLine(message);
        _writer.Flush();
        //_writer.Close();
    }
}
}