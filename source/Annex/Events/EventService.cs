using Annex.Events.Execution;
using Annex.Events.Scheduling;
using Annex.Services;
using System;
using System.Threading;

namespace Annex.Events
{
    public class EventService : IService
    {
        private readonly IExecutionEngine _executionEngine;
        private readonly ISchedulingEngine _schedulingEngine;

        public EventService(IExecutionEngine executionEngine, ISchedulingEngine schedulingEngine) {
            this._executionEngine = executionEngine;
            this._schedulingEngine = schedulingEngine;
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }

        public void Register(IEvent e) {
            this._schedulingEngine.Schedule(e);
        }

        public void Run(ITerminatable terminatable) {
            long tick;
            long lastTick = TickCount.Current;
            long timeDelta;

            while (!terminatable.ShouldTerminate()) {
                tick = TickCount.Current;
                timeDelta = tick - lastTick;
                lastTick = tick;

                if (timeDelta == 0) {
                    Thread.Yield();
                    continue;
                }

                var schedule = this._schedulingEngine.GetEventSchedule();
                while (schedule.HasNext) {
                    this._executionEngine.Execute(schedule.Next);
                }
            }
        }
    }
}
