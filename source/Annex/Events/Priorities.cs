using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Events
{
    public static class Priorities
    {
        public static readonly IEnumerable<int> All;
        public static readonly int Count;

        static Priorities() {
            All = ((PriorityType[])Enum.GetValues(typeof(PriorityType))).Select(val => (int)val);
            Count = All.Count();
        }
    }
}
