namespace Annex.Events
{
    public abstract class CustomEvent : BaseEvent
    {
        public CustomEvent(string eventID, int interval_ms, int delay_ms) : base(eventID, interval_ms, delay_ms) {
        }

        public override EventArgs Probe(long timeDifference_ms) {
            return Probe(timeDifference_ms, Run);
        }

        protected abstract void Run(EventArgs args);
    }
}
