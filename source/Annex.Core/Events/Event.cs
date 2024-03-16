namespace Annex.Core.Events
{
    public abstract class Event : IEvent
    {
        private long _nextEventInvocation;
        private readonly long _interval;

        public Guid Id { get; } = Guid.NewGuid();

        public Event(long interval, long initialDelay, Guid? id = null) {
            if (id != null)
            {
                this.Id = Id;
            }
            this._nextEventInvocation = initialDelay;
            this._interval = interval;
        }

        public Task TimeElapsedAsync(long timeDifference_ms) {
            this._nextEventInvocation -= timeDifference_ms;

            if (this._nextEventInvocation <= 0)
            {
                this._nextEventInvocation += this._interval;
                return this.RunAsync();
            }

            return Task.CompletedTask;
        }

        protected abstract Task RunAsync();
    }
}