﻿using Annex.Events.Trackers;
using System;
using System.Collections.Generic;

namespace Annex.Events
{
    public abstract class BaseEvent : IEvent
    {
        public string EventID { get; }
        private long _interval;
        private long _nextEventInvocation;
        private List<IEventTracker>? _trackers;
        private EventArgs _gameEventArgs;

        public BaseEvent(string eventID, int interval_ms, int delay_ms) {
            this.EventID = eventID;
            this._interval = interval_ms;
            this._nextEventInvocation = delay_ms;
            this._gameEventArgs = new EventArgs();
        }

        public EventArgs Probe(long timeDifference_ms, Action<EventArgs> action) {
            bool wasInvoked = false;
            this._nextEventInvocation -= timeDifference_ms;

            if (this._nextEventInvocation <= 0) {
                this._nextEventInvocation += this._interval;
                action.Invoke(_gameEventArgs);
                wasInvoked = true;
            }

            NotifyAllProbe(timeDifference_ms, wasInvoked);
            return _gameEventArgs;
        }

        private void NotifyAllProbe(long diff, bool invoked) {
            if (this._trackers == null) {
                return;
            }
            foreach (var tracker in this._trackers) {
                tracker.NotifyProbe(this, diff, invoked);
            }
        }

        public void AttachTracker(IEventTracker tracker) {
            if (this._trackers == null) {
                this._trackers = new List<IEventTracker>();
            }
            Debug.ErrorIf(this._trackers.Contains(tracker), "Tried to attach a tracker to a GameEvent that is already attached to it");
            this._trackers.Add(tracker);
        }

        public void SetInterval(long interval) {
            this._nextEventInvocation -= this._interval;
            this._nextEventInvocation += interval;
            this._interval = interval;
        }

        public long GetInterval() {
            return this._interval;
        }

        public abstract EventArgs Probe(long timeDifference_ms);
    }
}