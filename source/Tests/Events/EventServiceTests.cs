﻿using Annex;
using Annex.Events;
using Annex.Logging;
using Annex.Scenes;
using Annex.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

namespace Tests.Events
{
    public class EventServiceTests :  ITerminationCondition
    {
        public bool _shouldTerminate;
        private IEventService _eventService => this.ServiceContainer.Resolve<IEventService>()!;

        private ServiceContainer ServiceContainer => ServiceContainerSingleton.Instance!;

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            ServiceContainerSingleton.Destroy();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            ServiceContainerSingleton.Create();
            this.ServiceContainer.Provide<ILogService>(new LogService());
            this.ServiceContainer.Provide<ISceneService>(new SceneService());
        }

        [SetUp]
        public void SetUp() {
            this._shouldTerminate = false;
            this.ServiceContainer.Provide<IEventService>(new EventService());
        }

        [TearDown]
        public void TearDown() {
            this.ServiceContainer.Remove<IEventService>();
        }

        public bool ShouldTerminate() {
            return _shouldTerminate;
        }

        private void TerminateIn(int seconds) {
            new Thread(() => {
                Thread.Sleep(seconds * 1000);
                this._shouldTerminate = true;
            }).Start();
        }

        [Test]
        public void Run_Empty_Runs() {
            TerminateIn(1);
            this._eventService.Run(this);
            Assert.Pass();
        }

        [Test]
        public void Run_WithEvents_Runs() {
            var events = new List<CounterEvent>();
            int duration = 5;
            int interval = 100;
            int expected = duration * 1000 / interval;


            TerminateIn(duration);
            foreach (var priority in Priorities.All) {
                var e = new CounterEvent(interval);
                this._eventService.AddEvent((PriorityType)priority, e);
                events.Add(e);
            }
            this._eventService.Run(this);

            foreach (var e in events) {
                Assert.AreEqual(expected, e.Counter, 1);
            }
        }

        [Test]
        public void Run_WithNoIntervalEvent_RunsMoreThan1000TimesPerSecond() {
            var e = new CounterEvent(0);
            this._eventService.AddEvent(PriorityType.LOGIC, e);
            TerminateIn(1);
            this._eventService.Run(this);

            Assert.Greater(e.Counter, 1000);
        }

        private class CounterEvent : GameEvent
        {
            public int Counter { get; private set; }

            public CounterEvent(int interval, int delay = 0) : base(interval, delay) {
            }

            protected override void Run(EventArgs gameEventArgs) {
                this.Counter++;
            }
        }
    }
}
