using Annex.Services;
using NUnit.Framework;
using System;

namespace IntegrationTests
{
    public class ServiceProviderTests
    {
        public interface ITestService : IService
        {
        }

        public class TestService : ITestService
        {
            public void Dispose() {
                GC.SuppressFinalize(this);
            }
        }

        [SetUp]
        public void Setup() {
            var container = new ServiceContainer();
            var provider = new ServiceProvider(container);
            ServiceProvider.CreateSingleton(provider);
        }

        [TearDown]
        public void Teardown() {
            ServiceProvider.DestroySingleton();
        }

        [Test]
        public void EmptyServiceProvider_Provide_ReturnsService() {
            var service = new TestService();
            Assert.AreEqual(service, ServiceProvider.Provide(service));
        }

        [Test]
        public void EmptyServiceProvider_ProvideInterface_ReturnsService() {
            var service = new TestService();
            Assert.AreEqual(service, ServiceProvider.Provide<ITestService>(service));
        }

        [Test]
        public void ServiceProviderWithService_Resolve_ReturnsService() {
            var service = ServiceProvider.Provide(new TestService());
            Assert.AreEqual(service, ServiceProvider.Resolve<TestService>());
        }

        [Test]
        public void ServiceProviderWithService_ResolveInterface_ReturnsNull() {
            ServiceProvider.Provide(new TestService());
            Assert.AreEqual(null, ServiceProvider.Resolve<ITestService>());
        }

        [Test]
        public void ServiceProviderWithInterfaceService_ResolveInterface_ReturnsService() {
            var service = ServiceProvider.Provide<ITestService>(new TestService());
            Assert.AreEqual(service, ServiceProvider.Resolve<ITestService>());
        }

        [Test]
        public void ServiceProviderWithInterfaceService_Resolve_ReturnsNull() {
            ServiceProvider.Provide<ITestService>(new TestService());
            Assert.AreEqual(null, ServiceProvider.Resolve<TestService>());
        }
    }
}
