using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Events
{
    internal static class Priorities
    {
        internal static readonly IEnumerable<int> All;
        internal static readonly int Count;

        static Priorities() {
            All = ((PriorityType[])Enum.GetValues(typeof(PriorityType))).Select(val => (int)val);
            Count = All.Count();
        }
    }
}
