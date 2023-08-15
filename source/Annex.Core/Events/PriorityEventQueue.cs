using Scaffold.Collections.Generic;

namespace Annex.Core.Events
{
    internal class PriorityEventQueue : IPriorityEventQueue
    {
        private readonly ISet<long> _priorities = new SortedSet<long>();
        private readonly IDictionary<long, EventQueue> _eventQueues = new Dictionary<long, EventQueue>();
        private readonly IList<(long, IEvent)> _delayInsert = new ConcurrentList<(long, IEvent)>();

        public IEnumerable<long> Priorities => this._priorities;

        private object _modifyDictionaryCollectionWhileIteratingLock = new();

        public void Add(long priority, IEvent @event) {
            lock (this._modifyDictionaryCollectionWhileIteratingLock)
            {
                _delayInsert.Add((priority, @event));
            }
        }

        public void Add(long priority, long interval, long delay, Action timeElapsedDelegate) {
            lock (this._modifyDictionaryCollectionWhileIteratingLock)
            {
                _delayInsert.Add((priority, new LambdaEvent(interval, delay, timeElapsedDelegate)));
            }
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
            lock (this._modifyDictionaryCollectionWhileIteratingLock)
            {
                foreach (var priority in this.Priorities)
                {
                    this._eventQueues[priority].Remove(eventId);
                }
            }
        }

        private void InsertQueuedItems() {
            lock (this._modifyDictionaryCollectionWhileIteratingLock)
            {
                foreach ((long priority, IEvent @event) in _delayInsert)
                {
                    var queue = this.GetOrCreateQueue(priority);
                    queue.Add(@event);
                }
                this._delayInsert.Clear();
            }
        }

        public void StepPriority(long priority) {
            lock (this._modifyDictionaryCollectionWhileIteratingLock)
            {
                InsertQueuedItems();
                this.GetOrCreateQueue(priority).Step();
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
            private readonly Action _timeElapsedDelegate;

            public LambdaEvent(long interval, long delay, Action timeElapsedDelegate) : base(interval, delay, Guid.NewGuid()) {
                _timeElapsedDelegate = timeElapsedDelegate;
            }

            protected override void Run() {
                _timeElapsedDelegate();
            }
        }
    }
}
