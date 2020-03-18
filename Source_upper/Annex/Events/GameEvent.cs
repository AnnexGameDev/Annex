using System;

namespace Annex.Events
{
    public class GameEvent
    {
        private readonly Func<ControlEvent> _event;
        private readonly int _interval;
        private int _nextEventInvocation;

        public GameEvent(Func<ControlEvent> @event, int interval_ms, int delay_ms) {
            this._event = @event;
            this._interval = interval_ms;
            this._nextEventInvocation = delay_ms;
        }

        public ControlEvent Probe(int timeDifference_ms) {
            this._nextEventInvocation -= timeDifference_ms;

            if (this._nextEventInvocation <= 0) {
                this._nextEventInvocation += this._interval;
                return this._event.Invoke();
            }

            return ControlEvent.NONE;
        }
    }
}
