using Annex;
using Annex.Assets;
using Annex.Logging;
using Annex.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    public class ServiceContainerTests : TestWithServiceContainerSingleton
    {
        [OneTimeSetUp]
        public new void OneTimeSetUp() {
            ServiceContainer.Provide<ILogService>(new LogService());
        }

        [Test]
        public void Resolve_WithDifferentService_ThrowsException() {
            Assert.Throws<AssertionFailedException>(() => {
                ServiceContainer.Resolve<AService>();
            });
        }

        [Test]
        public void Resolve_WithIntendedService_ReturnsService() {
            var instance = ServiceContainer.Provide<AService>(new AService());
            Assert.AreEqual(instance, ServiceContainer.Resolve<AService>());
            Assert.IsTrue(ServiceContainer.Exists<AService>());
        }

        [Test]
        public void Remove_WithService_RemovesService() {
            ServiceContainer.Provide<AService>(new AService());
            ServiceContainer.Remove<AService>();

            Assert.IsFalse(ServiceContainer.Exists<AService>());
        }


        [Test]
        public void Remove_WithoutService_ThrowsException() {
            Assert.Throws<AssertionFailedException>(() => {
                ServiceContainer.Remove<AService>();
            });
        }

        private class AService : IService
        {
            public void Destroy() {

            }

            public IEnumerable<IAssetManager> GetAssetManagers() {
                return Enumerable.Empty<IAssetManager>();
            }
        }
    }
}
