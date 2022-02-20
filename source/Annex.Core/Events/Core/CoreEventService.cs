using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Time;

namespace Annex.Core.Events.Core
{
    internal class CoreEventService : ICoreEventService, IDisposable
    {
        private readonly IBroadcast<RequestStopAppMessage> _stopAppMessage;
        private readonly IEventQueue _eventQueue;

        public CoreEventService(IBroadcast<RequestStopAppMessage> stopAppMessage, ITimeService timeService) {
            this._stopAppMessage = stopAppMessage;
            this._stopAppMessage.OnBroadcastPublished += StopAppMessage_OnBroadcastPublished;

            this._eventQueue = new EventQueue(timeService);
        }

        public void Add(CoreEventType type, IEvent coreEvent) {
            this._eventQueue.Add(coreEvent);
        }

        public void Dispose() {
            this._stopAppMessage.OnBroadcastPublished -= StopAppMessage_OnBroadcastPublished;
        }

        public void Run() {
            this._eventQueue.Run();
        }

        private void StopAppMessage_OnBroadcastPublished(object? sender, RequestStopAppMessage e) {
            this._eventQueue.Stop();
        }
    }
}