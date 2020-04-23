namespace Annex.Events
{
    public class EventTracker
    {
        public long LastCount { get; set; }
        public long CurrentCount { get; set; }
        public long CurrentInterval { get; set; }
        public long Interval { get; set; }

        public EventTracker(long interval) {
            this.Interval = interval;
            this.LastCount = 0;
            this.CurrentCount = 0;
            this.CurrentInterval = 0;
        }

        public void Probe(long diff, bool invoked) {
            if (invoked) {
                this.CurrentCount++;
            }
            this.CurrentInterval += diff;

            if (this.CurrentInterval >= this.Interval) {
                LastCount = CurrentCount;
                CurrentCount = 0;
                this.CurrentInterval = 0;
            }
        }
    }
}
