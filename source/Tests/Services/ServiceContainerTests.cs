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
            ServiceContainer.Provide<Log>();
        }

        [Test]
        public void Resolve_WithDifferentService_ThrowsException() {
            Assert.Throws<AssertionFailedException>(() => {
                ServiceContainer.Resolve<AService>();
            });
        }

        [Test]
        public void Resolve_WithIntendedService_ReturnsService() {
            var instance = ServiceContainer.Provide<AService>();
            Assert.AreEqual(instance, ServiceContainer.Resolve<AService>());
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
