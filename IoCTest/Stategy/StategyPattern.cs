
using System;
using System.Diagnostics;

namespace IoCTest
{
    public class StratContext
    {
        private StrategyBase _theStrat;

        public StratContext(StrategyBase strategy)
        {
            _theStrat = strategy;
        }

        public void DoStrat()
        {
            _theStrat.StratAlgorithm();
        }
    }


    public abstract class StrategyBase
    {
        public abstract void StratAlgorithm();
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