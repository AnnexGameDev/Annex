using Annex.Core.Services;
using FluentAssertions;
using System;
using Xunit;

namespace Annex.Core.Tests.Services
{
    public class ContainerTests
    {
        private readonly IContainer _container;
        private readonly RegistrationOptions _singletonOptions;

        #region Helpers
        private interface IServiceInterface { }
        private class ServiceImplementation : IServiceInterface { }
        private record ServiceWithDependency(IServiceInterface DependencyInstance) { }
        private class ServiceWithMultipleConstructors
        {
            public ServiceWithMultipleConstructors() { }
            public ServiceWithMultipleConstructors(object _) { }
        }
        #endregion

        public ContainerTests() {
            this._container = new Container();

            this._singletonOptions = new RegistrationOptions() {
                Singleton = true
            };
        }

        [Fact]
        public void GivenAContainer_WhenCheckingIfIContainerIsRegistered_ThenReturnsTrue() {
            // Arrange

            // Act
            bool icontainerIsRegistered = this._container.IsRegistered<IContainer>();

            // Assert
            icontainerIsRegistered.Should().BeTrue();
        }

        [Fact]
        public void GivenAServiceIsRegistered_WhenCheckingIfServiceIsRegistered_ThenReturnsTrue() {
            // Arrange
            this._container.Register<IServiceInterface, ServiceImplementation>();

            // Act
            bool serviceIsRegistered = this._container.IsRegistered(typeof(IServiceInterface));

            // Assert
            serviceIsRegistered.Should().BeTrue();
        }

        [Fact]
        public void GivenAServiceIsRegistered_WhenCheckingIfServiceIsRegisteredTemplated_ThenReturnsTrue() {
            // Arrange
            this._container.Register<IServiceInterface, ServiceImplementation>();

            // Act
            bool serviceIsRegistered = this._container.IsRegistered<IServiceInterface>();

            // Assert
            serviceIsRegistered.Should().BeTrue();
        }

        [Fact]
        public void GivenNoServiceIsRegistered_WhenTryingToResolveTheInterface_ThenThrowsNullReferenceException() {
            // Arrange / Act / Assert
            Assert.Throws<NullReferenceException>(() => {
                this._container.Resolve(typeof(IServiceInterface));
            });
        }

        [Fact]
        public void GivenNoServiceIsRegistered_WhenTryingToResolveTheInterfaceTemplated_ThenReturnsNull() {
            // Arrange / Act / Assert
            Assert.Throws<NullReferenceException>(() => {
                this._container.Resolve<IServiceInterface>();
            });
        }

        [Fact]
        public void GivenAServiceIsRegisteredToAType_WhenTryingToResolveTheType_ThenReturnsTheServiceInstance() {
            // Arrange
            this._container.Register<IServiceInterface, ServiceImplementation>();

            // Act
            var theActualService = this._container.Resolve(typeof(IServiceInterface));

            // Assert
            theActualService.Should().NotBeNull();
            theActualService.Should().BeOfType<ServiceImplementation>();
        }

        [Fact]
        public void GivenAServiceIsRegisteredToAType_WhenTryingToResolveTheTypeTemplated_ThenReturnsTheServiceInstance() {
            // Arrange
            this._container.Register<IServiceInterface, ServiceImplementation>();

            // Act
            var theActualService = this._container.Resolve<IServiceInterface>();

            // Assert
            theActualService.Should().NotBeNull();
            theActualService.Should().BeOfType<ServiceImplementation>();
        }

        [Fact]
        public void GivenAServiceIsRegistered_WhenTryingToOverwrite_ThenThrows() {
            // Arrange
            this._container.Register<IServiceInterface, ServiceImplementation>();

            // Act / assert
            Assert.Throws<ArgumentException>(() => {
                this._container.Register<IServiceInterface, ServiceImplementation>();
            });
        }

        [Fact]
        public void GivenAServiceIsRegisteredAsSingleton_WhenResolving_ThenReturnsTheSameInstance() {
            // Arrange
            this._container.Register<IServiceInterface, ServiceImplementation>(this._singletonOptions);
            var theExpectedInstance = this._container.Resolve<IServiceInterface>();

            // Act
            var theActualInstance = this._container.Resolve<IServiceInterface>();

            // Assert
            theActualInstance.Should().Be(theExpectedInstance);
        }

        [Fact]
        public void GivenServicesAreRegistered_WhenResolvingServiceWithDependency_ThenServiceIsResolvedWithAnInjectedDependency() {
            // Arrange
            this._container.Register<IServiceInterface, ServiceImplementation>();
            this._container.Register<ServiceWithDependency, ServiceWithDependency>();

            // Act
            var theServiceWithDependency = this._container.Resolve<ServiceWithDependency>();

            // Assert
            theServiceWithDependency.DependencyInstance.Should().NotBeNull();
        }

        [Fact]
        public void GivenAServiceIsRegisteredWithMultipleConstructors_WhenResolvingTheService_ThenThrowsInvalidOperationException() {
            // Arrange
            this._container.Register<ServiceWithMultipleConstructors, ServiceWithMultipleConstructors>();

            // Act // Assert
            Assert.Throws<InvalidOperationException>(() => {
                this._container.Resolve<ServiceWithMultipleConstructors>();
            });
        }
    }
}