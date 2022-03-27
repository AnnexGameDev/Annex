using Annex.Core.Time;
using Scaffold.Collections.Generic;

namespace Annex.Core.Events
{
    public class EventQueue : IEventQueue
    {
        private readonly IConcurrentList<IEvent> _queueItems = new ConcurrentList<IEvent>();
        private readonly ITimeService _timeService;

        private long? _lastStep = null;

        public EventQueue() {
            this._timeService = new StopwatchTimeService();
        }

        public EventQueue(ITimeService timeService) {
            this._timeService = timeService;
        }

        public void Remove(Guid itemId) {
            this._queueItems.Remove(groupItem => groupItem.Id == itemId);
        }

        public void Add(params IEvent[] items) {
            this._queueItems.AddRange(items);
        }

        public void Step() {
            long lastStep = this._lastStep ?? this._timeService.Now;
            long step = this._timeService.ElapsedTimeSince(lastStep);
            foreach (var item in this._queueItems) {
                item.TimeElapsed(step);
            }
            this._lastStep = lastStep + step;
        }
    }
}