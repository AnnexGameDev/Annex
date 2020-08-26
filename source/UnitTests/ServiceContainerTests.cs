using Annex.Services;
using NUnit.Framework;

namespace UnitTests
{
    public class ServiceContainerTests
    {
        [Test]
        public void EmptyServiceContainer_GetService_ReturnsNull() {
            using var container = new ServiceContainer();
            Assert.AreEqual(null, container.GetService(typeof(object)));
        }

        [Test]
        public void ServiceContainerWithService_GetService_ReturnsService() {
            using var container = new ServiceContainer();
            object? service = new object();
            container.SetService(typeof(object), service);

            Assert.AreEqual(service, container.GetService(typeof(object)));
        }

        [Test]
        public void ServiceContainerWithService_GetOtherService_ReturnsNull() {
            using var container = new ServiceContainer();
            container.SetService(typeof(object), new object());

            Assert.AreEqual(null, container.GetService(typeof(string)));
        }
    }
}
