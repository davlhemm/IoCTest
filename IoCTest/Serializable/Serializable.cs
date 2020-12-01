
using System;
using System.Collections.Generic;

namespace IoCTest
{
    [Serializable]
    public class SerialItem
    {
        public int Num { get; set; }
        protected Dictionary<int, TValue> NumValueDict { get; set; }

    }

    public sealed class TValue
    {
        public int Value { get; set; }
    }
}