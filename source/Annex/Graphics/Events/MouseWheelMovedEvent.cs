using System;

namespace Annex_Old.Graphics.Events
{
    public class MouseWheelMovedEvent : HardwareEvent
    {
        public double Delta { get; set; }
    }
}
