﻿namespace Annex.Core.Input.InputEvents
{
    public abstract class HardwareEvent
    {
        public bool Handled { get; set; } = false;
    }
}