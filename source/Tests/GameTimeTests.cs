using Annex;
using NUnit.Framework;
using System;
using System.Threading;

namespace Tests
{
    public class GameTimeTests
    {
        [Test]
        public void Now_TimeElapsed_DifferentTime() {
            var rng = new Random();
            var wait = rng.Next(100, 1000);
            var before = GameTime.Now;
            Thread.Sleep(wait);
            var after = GameTime.Now;

            Assert.AreNotEqual(before, after);
            Assert.AreEqual(before, after, wait * 1.01f);
        }
    }
}
