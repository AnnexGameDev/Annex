using System;

namespace Annex.Events
{
    public class ActionEvent : BaseEvent
    {
        private readonly Action<EventArgs> _action;

        public ActionEvent(string eventID, Action<EventArgs> action, int interval_ms, int delay_ms) : base(eventID, interval_ms, delay_ms) {
            this._action = action;
        }

        public override EventArgs Probe(long timeDifference_ms) {
            return Probe(timeDifference_ms, this._action);
        }
    }
}
