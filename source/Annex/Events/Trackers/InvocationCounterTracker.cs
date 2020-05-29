namespace Annex.Events.Trackers
{
    public class InvocationCounterTracker : IEventTracker
    {
        public long LastCount { get; set; }
        public long CurrentCount { get; set; }
        public long CurrentInterval { get; set; }
        public long Interval { get; set; }

        public InvocationCounterTracker(long interval) {
            this.Interval = interval;
            this.LastCount = 0;
            this.CurrentCount = 0;
            this.CurrentInterval = 0;
        }

        public void NotifyProbe(GameEvent gameEvent, long timeDiff, bool invoked) {
            if (invoked) {
                this.CurrentCount++;
            }
            this.CurrentInterval += timeDiff;

            if (this.CurrentInterval >= this.Interval) {
                LastCount = CurrentCount;
                CurrentCount = 0;
                this.CurrentInterval = 0;
            }
        }
    }
}
