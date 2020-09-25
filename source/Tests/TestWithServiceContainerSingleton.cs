using Annex.Services;
using NUnit.Framework;

namespace Tests
{
    public class TestWithServiceContainerSingleton
    {
        protected ServiceContainer ServiceContainer => ServiceContainerSingleton.Instance;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            ServiceContainerSingleton.Create();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            ServiceContainerSingleton.Destroy();
        }
    }
}
