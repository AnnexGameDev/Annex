using Annex;
using Annex.Services;
using NUnit.Framework;
using System;

namespace Tests.Services
{
    public class ServiceContainerSingletonTests
    {
        [TearDown]
        public void TearDown() {
            try {
                ServiceContainerSingleton.Destroy();
            } catch {

            }
        }

        [Test]
        public void GetSingleton_SingletonUnset_ThrowsException() {
            Assert.Throws<NullReferenceException>(() => {
                var instance = ServiceContainerSingleton.Instance;

                if (instance == null) {
                    throw new NullReferenceException();
                }
            });
        }

        [Test]
        public void GetSingleton_SingletonSet_ReturnsSingleton() {
            var instance = ServiceContainerSingleton.Create();
            Assert.AreEqual(instance, ServiceContainerSingleton.Instance);
        }

        [Test]
        public void CreateSingleton_SingletonSet_ThrowsException() {
            ServiceContainerSingleton.Create();
            Assert.Throws<AssertionFailedException>(() => {
                ServiceContainerSingleton.Create();
            });
        }

        [Test]
        public void DestroySingleton_SingletonUnset_ThrowsException() {
            Assert.Throws<AssertionFailedException>(() => {
                ServiceContainerSingleton.Destroy();
            });
        }

        [Test]
        public void DestroySingleton_SingletonSet_DestroysSingleton() {
            var i1 = ServiceContainerSingleton.Create();
            Assert.AreEqual(i1, ServiceContainerSingleton.Instance);
            ServiceContainerSingleton.Destroy();
            Assert.Throws<NullReferenceException>(() => {
                var i2 = ServiceContainerSingleton.Instance;

                if (i2 == null) {
                    throw new NullReferenceException();
                }
            });
        }
    }
}
