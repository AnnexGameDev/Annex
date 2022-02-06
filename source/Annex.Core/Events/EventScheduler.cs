using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Time;

namespace Annex.Core.Events
{
    internal class EventScheduler : IEventScheduler
    {
        private readonly IEnumerable<IEventGroup> _groups;
        private readonly IBroadcast<RequestStopAppMessage> _requestStopAppMessage;
        private readonly ITimeService _timeService;
        private bool _keepRunning = true;

        public EventScheduler(IEnumerable<IEventGroup> groups, IBroadcast<RequestStopAppMessage> appLifeCycleMessage, ITimeService timeService) {
            this._groups = groups.OrderBy(group => group.Priority);
            this._requestStopAppMessage = appLifeCycleMessage;
            this._requestStopAppMessage.OnBroadcastPublished += _requestStopAppMessage_OnBroadcastPublished;
            this._timeService = timeService;
        }

        public void Run() {
            long lastRun = this._timeService.Now;
            while (this._keepRunning) {
                long step = this._timeService.ElapsedTimeSince(lastRun);
                foreach (var group in this._groups) {
                    group.Run(step);
                }
                lastRun = this._timeService.Now;
            }
        }

        private void _requestStopAppMessage_OnBroadcastPublished(object? sender, RequestStopAppMessage e) {
            this._keepRunning = false;
        }

        public void Dispose() {
            this._requestStopAppMessage.OnBroadcastPublished -= _requestStopAppMessage_OnBroadcastPublished;
        }
    }
}