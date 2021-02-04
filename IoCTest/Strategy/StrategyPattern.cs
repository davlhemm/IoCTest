
using System;
using System.Diagnostics;

namespace IoCTest
{
    public class StratContext
    {
        private readonly IStrategyBase _theStrat;

        public StratContext(IStrategyBase strategy)
        {
            _theStrat = strategy;
        }

        public void DoStrat()
        {
            _theStrat.StratAlgorithm();
        }
    }


    public abstract class StrategyBase : IStrategyBase
    {
        public abstract void StratAlgorithm();
    }

    public interface IStrategyBase
    {
        public void StratAlgorithm();
    }

    public class ConcreteStrategyA : StrategyBase
    {
        public override void StratAlgorithm()
        {
            Debug.WriteLine("A: "+ToString());
        }
    }

    public class ConcreteStrategyB : StrategyBase
    {
        public override void StratAlgorithm()
        {
            Debug.WriteLine("B: "+ToString());
        }
    }
}