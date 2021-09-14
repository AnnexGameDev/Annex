using System;

namespace Annex.Graphics.Events
{
    public class MouseWheelMovedEvent : HardwareEvent
    {
        public double Delta { get; set; }
    }
}
