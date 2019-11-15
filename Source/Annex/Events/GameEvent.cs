#nullable enable
using System;

namespace Annex.Events
{
    public class GameEvent
    {
        public readonly string EventID;
        private readonly Func<ControlEvent> _event;
        private long _interval;
        private long _nextEventInvocation;
        private EventTracker? _tracker;

        public GameEvent(string eventID, Func<ControlEvent> @event, int interval_ms, int delay_ms) {
            this.EventID = eventID;
            this._event = @event;
            this._interval = interval_ms;
            this._nextEventInvocation = delay_ms;
        }

        public ControlEvent Probe(long timeDifference_ms) {
            this._nextEventInvocation -= timeDifference_ms;

            if (this._nextEventInvocation <= 0) {
                this._tracker?.Probe(timeDifference_ms, true);
                this._nextEventInvocation += this._interval;
                return this._event.Invoke();
            }

            this._tracker?.Probe(timeDifference_ms, false);
            return ControlEvent.NONE;
        }

        public void AttachTracker(EventTracker tracker) {
            this._tracker = tracker;
        }

        public void SetInterval(long interval) {
            this._nextEventInvocation -= this._interval;
            this._nextEventInvocation += interval;
            this._interval = interval;
        }
    }
}
