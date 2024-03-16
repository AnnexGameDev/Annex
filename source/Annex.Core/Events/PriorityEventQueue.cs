using Scaffold.Collections.Generic;

namespace Annex.Core.Events
{
    internal class PriorityEventQueue : IPriorityEventQueue
    {
        private readonly ISet<long> _priorities = new SortedSet<long>();
        private readonly IDictionary<long, EventQueue> _eventQueues = new Dictionary<long, EventQueue>();
        private readonly IList<(long, IEvent)> _delayInsert = new ConcurrentList<(long, IEvent)>();

        public IEnumerable<long> Priorities => this._priorities;

        private readonly SemaphoreSlim _modifyDictionaryCollectionWhileIteratingSempahore = new(1);

        public void Add(long priority, IEvent @event) {
            Atomic(() => {
                _delayInsert.Add((priority, @event));
            });
        }

        public void Add(long priority, long interval, long delay, Action timeElapsedDelegate) {
            Atomic(() => {
                _delayInsert.Add((priority, new LambdaEvent(interval, delay, timeElapsedDelegate)));
            });
        }

        private EventQueue GetOrCreateQueue(long priority) {
            if (!this._eventQueues.ContainsKey(priority))
            {
                this._priorities.Add(priority);
                this._eventQueues.Add(priority, new EventQueue());
            }
            return this._eventQueues[priority];
        }

        public void Remove(Guid eventId) {
            Atomic(() => {
                foreach (var priority in this.Priorities)
                {
                    this._eventQueues[priority].Remove(eventId);
                }
            });
        }

        private void InsertQueuedItems() {
            foreach ((long priority, IEvent @event) in _delayInsert)
            {
                var queue = this.GetOrCreateQueue(priority);
                queue.Add(@event);
            }
            this._delayInsert.Clear();
        }

        public Task StepPriorityAsync(long priority) {
            return AtomicAsync(async () => {
                InsertQueuedItems();
                await (this.GetOrCreateQueue(priority)).StepAsync();
            });
        }

        private void Atomic(Action action) {
            this._modifyDictionaryCollectionWhileIteratingSempahore.Wait();
            try
            {
                action();
            }
            finally
            {
                this._modifyDictionaryCollectionWhileIteratingSempahore.Release();
            }
        }

        private async Task AtomicAsync(Func<Task> action) {
            this._modifyDictionaryCollectionWhileIteratingSempahore.Wait();
            try
            {
                await action();
            }
            finally
            {
                this._modifyDictionaryCollectionWhileIteratingSempahore.Release();
            }
        }

        public void Dispose() {
            foreach (var eventQueue in this._eventQueues.Values)
            {
                eventQueue.Dispose();
            }
        }

        private class LambdaEvent : Event
        {
            private readonly Func<Task> _timeElapsedDelegate;

            public LambdaEvent(long interval, long delay, Action timeElapsedDelegate) : this(interval, delay, () => {
                timeElapsedDelegate();
                return Task.CompletedTask;
            }) {
            }

            public LambdaEvent(long interval, long delay, Func<Task> timeElapsedDelegate) : base(interval, delay, Guid.NewGuid()) {
                _timeElapsedDelegate = timeElapsedDelegate;
            }

            protected override Task RunAsync() {
                return _timeElapsedDelegate();
            }
        }
    }
}
