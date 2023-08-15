using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Scenes;

namespace Annex.Core.Events.Core
{
    internal class CoreEventService : ICoreEventService, IDisposable
    {
        private readonly IBroadcast<RequestStopAppMessage> _stopAppMessage;
        private bool _keepRunning = true;
        private readonly IPriorityEventQueue _globalEvents;
        private readonly ISceneService _sceneService;

        public CoreEventService(IBroadcast<RequestStopAppMessage> stopAppMessage, IPriorityEventQueue priorityEventQueue, ISceneService sceneService) {
            this._stopAppMessage = stopAppMessage;
            this._stopAppMessage.OnBroadcastPublished += StopAppMessage_OnBroadcastPublished;
            this._globalEvents = priorityEventQueue;
            this._sceneService = sceneService;
        }

        public void Add(long priority, IEvent coreEvent) {
            this._globalEvents.Add(priority, coreEvent);
        }

        public void Dispose() {
            this._stopAppMessage.OnBroadcastPublished -= StopAppMessage_OnBroadcastPublished;
        }

        public void Remove(Guid eventId) {
            this._globalEvents.Remove(eventId);
        }

        public void Run() {
            while (this._keepRunning) {
                var sceneEvents = this._sceneService.CurrentScene.Events;
                var corePriorities = Enum.GetValues<CoreEventPriority>().Cast<long>();
                var globalPriorities = this._globalEvents.Priorities;
                var scenePriorities = sceneEvents.Priorities;
                var allPriorities = scenePriorities.Union(globalPriorities).Union(corePriorities).OrderBy(val => val);

                foreach (var priority in allPriorities) {
                    this._globalEvents.StepPriority(priority);
                    sceneEvents.StepPriority(priority);
                }
            }
        }

        private void StopAppMessage_OnBroadcastPublished(object? sender, RequestStopAppMessage e) {
            this._keepRunning = false;
        }
    }
}