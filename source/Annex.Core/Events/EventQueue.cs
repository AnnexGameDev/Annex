using Annex.Core.Time;
using Scaffold.Collections.Generic;

namespace Annex.Core.Events
{
    public class EventQueue : IEventQueue
    {
        private readonly IConcurrentList<IEvent> _queueItems = new ConcurrentList<IEvent>();
        private readonly ITimeService _timeService;
        private bool _keepRunning = true;

        public EventQueue(ITimeService timeService) {
            this._timeService = timeService;
        }

        public void Remove(Guid itemId) {
            this._queueItems.Remove(groupItem => groupItem.Id == itemId);
        }

        public void Add(params IEvent[] items) {
            this._queueItems.AddRange(items);
        }

        public void Stop() {
            this._keepRunning = false;
        }

        public void Run() {
            long lastRun = this._timeService.Now;
            while (this._keepRunning) {
                long step = this._timeService.ElapsedTimeSince(lastRun);
                foreach (var item in this._queueItems) {
                    item.TimeElapsed(step);
                }
                lastRun = this._timeService.Now;
            }
        }
    }
}