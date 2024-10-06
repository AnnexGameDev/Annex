﻿using System.Diagnostics;

namespace Annex.Core;

public static class Debug
{
    [Conditional("DEBUG")]
    public static void Assert(bool condition, string reason) {
        System.Diagnostics.Debug.Assert(condition, reason);
    }
}