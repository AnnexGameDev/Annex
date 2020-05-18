using System;

namespace Annex.Events
{
    public class GameEvent
    {
        public readonly string EventID;
        private readonly Action<GameEventArgs> _event;
        private long _interval;
        private long _nextEventInvocation;
        private EventTracker? _tracker;

        private GameEventArgs _gameEventArgs;

        public GameEvent(string eventID, Action<GameEventArgs> @event, int interval_ms, int delay_ms) {
            this.EventID = eventID;
            this._event = @event;
            this._interval = interval_ms;
            this._nextEventInvocation = delay_ms;
            this._gameEventArgs = new GameEventArgs();
        }

        public GameEventArgs Probe(long timeDifference_ms) {
            this._nextEventInvocation -= timeDifference_ms;

            if (this._nextEventInvocation <= 0) {
                this._tracker?.Probe(timeDifference_ms, true);
                this._nextEventInvocation += this._interval;
                this._event.Invoke(_gameEventArgs);
            }

            this._tracker?.Probe(timeDifference_ms, false);
            return _gameEventArgs;
        }

        public void AttachTracker(EventTracker tracker) {
            this._tracker = tracker;
        }

        public void SetInterval(long interval) {
            this._nextEventInvocation -= this._interval;
            this._nextEventInvocation += interval;
            this._interval = interval;
        }

        public long GetInterval() {
            return this._interval;
        }
    }

    public class GameEventArgs
    {
        public ControlEvent ControlEvent { get; set; }
    }
}
