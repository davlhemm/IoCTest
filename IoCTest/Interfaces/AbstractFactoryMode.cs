
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace IoCTest
{
    public enum FactoryMode
    {
        First,
        Second,
        Third
    }
    
    public class OneMode: Mode { }
    public class TwoMode: Mode { }
    public class ThreeMode: Mode { }
    public class Default : Mode { }

    public class Mode: IMode { }
    public interface IMode { }
    
    public interface IAbstractFactory<out TFact>
    {
        public TFact Create(FactoryMode theMode);
    }


    public abstract class AbstractFactory<TFact> : IAbstractFactory<TFact>
    where TFact : IMode, new()
    {
        protected AbstractFactory(){}

        public TFact Create(FactoryMode theMode)
        {
            return (TFact)ModeFactoryService.CreateByMode(theMode);
        }
    }

    public static class ModeFactoryService
    {
        public static IMode CreateByMode(FactoryMode mode)
        {
            switch (mode)
            {
                case FactoryMode.First:
                    return new OneMode();
                case FactoryMode.Second:
                    return new TwoMode();
                case FactoryMode.Third:
                    return new ThreeMode();
                default:
                    return new Default();
            }
        }
    }
}