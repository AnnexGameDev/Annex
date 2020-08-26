#define DEBUG
using Annex;
using NUnit.Framework;

namespace UnitTests
{
    public class DebugTests
    {
        [Test]
        public void Debug_AssertTrue_NoException() {
            Debug.Assert(true, nameof(Debug_AssertTrue_NoException));
        }

        [Test]
        public void Debug_AssertFalse_ThrowsException() {
            var e = Assert.Throws<AssertionFailedException>(() => {
                Debug.Assert(false, nameof(Debug_AssertFalse_ThrowsException));
            });
            Assert.AreEqual(e.Message, nameof(Debug_AssertFalse_ThrowsException));
        }

        [Test]
        public void Debug_ErrorIfFalse_NoException() {
            Debug.ErrorIf(false, nameof(Debug_ErrorIfFalse_NoException));
        }

        [Test]
        public void Debug_ErrorIfTrue_ThrowsException() {
            var e = Assert.Throws<AssertionFailedException>(() => {
                Debug.ErrorIf(true, nameof(Debug_ErrorIfTrue_ThrowsException));
            });
            Assert.AreEqual(e.Message, nameof(Debug_ErrorIfTrue_ThrowsException));
        }
    }
}
