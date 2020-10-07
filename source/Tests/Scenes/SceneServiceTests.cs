using Annex;
using Annex.Logging;
using Annex.Scenes;
using Annex.Scenes.Components;
using NUnit.Framework;

namespace Tests.Scenes
{
    public class SceneServiceTests : TestWithServiceContainerSingleton
    {
        private SceneService _sceneService => this.ServiceContainer.Resolve<SceneService>();

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            this.ServiceContainer.Provide<Log>();
            this.ServiceContainer.Provide<SceneService>();
        }

        [Test]
        public void LoadScene_WithNoScene_CreatesScene() {
            var scene = this._sceneService.LoadScene<AScene>();
            Assert.AreSame(scene, this._sceneService.CurrentScene);
        }

        [Test]
        public void LoadScene_WithScene_UsesCachedInstance() {
            var ascene = this._sceneService.LoadScene<AScene>();
            var bscene = this._sceneService.LoadScene<BScene>();

            Assert.AreSame(ascene, this._sceneService.LoadScene<AScene>());
        }

        [Test]
        public void LoadSceneCreateNew_WithScene_CreatesNewInstance() {
            var ascene = this._sceneService.LoadScene<AScene>();
            var bscene = this._sceneService.LoadScene<BScene>();

            Assert.AreNotEqual(ascene, this._sceneService.LoadScene<AScene>(true));
        }

        [Test]
        public void UnloadScene_WhileInCurrentScene_ThrowsException() {
            this._sceneService.LoadScene<AScene>();
            Assert.Throws<AssertionFailedException>(() => {
                this._sceneService.UnloadScene<AScene>();
            });
        }

        [Test]
        public void UnloadScene_NotInCurrentScene_UnloadsScene() {
            var ascene = this._sceneService.LoadScene<AScene>();
            this._sceneService.LoadScene<BScene>();

            this._sceneService.UnloadScene<AScene>();

            Assert.AreNotEqual(ascene, this._sceneService.LoadScene<AScene>());
        }

        private class AScene : Scene
        {
            public AScene() : base(0, 0) {
            }
        }

        private class BScene : Scene
        {
            public BScene() : base(0, 0) {
            }
        }
    }
}
