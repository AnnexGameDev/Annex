using Annex.Logging;
using Annex.Scenes;
using Annex.Scenes.Components;
using NUnit.Framework;

namespace Tests.Scenes
{
    public class SceneServiceTests : TestWithServiceContainerSingleton
    {
        private ISceneService _sceneService => this.ServiceContainer.Resolve<ISceneService>();

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            this.ServiceContainer.Provide<ILogService>(new LogService());
        }

        [SetUp]
        public void SetUp() {
            this.ServiceContainer.Provide<ISceneService>(new SceneService());
        }

        [TearDown]
        public void TearDown() {
            this.ServiceContainer.Remove<ISceneService>();
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
        public void LoadSceneCreateNew_SameScene_CreatesNewInstance() {
            var ascene1 = this._sceneService.LoadScene<AScene>();
            var ascene2 = this._sceneService.LoadScene<AScene>(true);
            Assert.AreNotSame(ascene1, ascene2);
        }

        [Test]
        public void LoadSceneCreateNew_WithScene_CreatesNewInstance() {
            var ascene = this._sceneService.LoadScene<AScene>();
            var bscene = this._sceneService.LoadScene<BScene>();

            Assert.AreNotEqual(ascene, this._sceneService.LoadScene<AScene>(true));
        }

        [Test]
        public void UnloadScene_WhileInCurrentScene_Allowed() {
            this._sceneService.LoadScene<AScene>();
            this._sceneService.UnloadScene<AScene>();
        }

        [Test]
        public void UnloadScene_NotInCurrentScene_UnloadsScene() {
            var ascene = this._sceneService.LoadScene<AScene>();
            this._sceneService.LoadScene<BScene>();

            this._sceneService.UnloadScene<AScene>();

            Assert.AreNotEqual(ascene, this._sceneService.LoadScene<AScene>());
        }

        [Test]
        public void LoadScene_CallsOnEnterAndOnLeave() {
            var ascene = this._sceneService.LoadScene<AScene>();

            Assert.AreEqual(1, ascene.EnterCount);
            Assert.AreEqual(0, ascene.LeaveCount);

            this._sceneService.LoadScene<BScene>();

            Assert.AreEqual(1, ascene.EnterCount);
            Assert.AreEqual(1, ascene.LeaveCount);
        }

        [Test]
        public void LoadScene_SameScene_CallsOnEnterAndOnLeaveForTheSameScene() {
            var ascene = this._sceneService.LoadScene<AScene>();
            this._sceneService.LoadScene<AScene>();

            Assert.AreEqual(2, ascene.EnterCount);
            Assert.AreEqual(1, ascene.LeaveCount);
        }

        [Test]
        public void LeadScene_NewInstance_CallsDifferentOnEnterAndOnLeave() {
            var ascene1 = this._sceneService.LoadScene<AScene>();
            var ascene2 = this._sceneService.LoadScene<AScene>(true);

            Assert.AreEqual(1, ascene1.EnterCount);
            Assert.AreEqual(1, ascene1.LeaveCount);

            Assert.AreEqual(1, ascene2.EnterCount);
            Assert.AreEqual(0, ascene2.LeaveCount);
        }


        private class AScene : Scene
        {
            public int EnterCount { get; private set; }
            public int LeaveCount { get; private set; }

            public AScene() : base(0, 0) {
            }

            public override void OnEnter(OnSceneEnterEvent e) {
                this.EnterCount++;
            }

            public override void OnLeave(OnSceneLeaveEvent e) {
                this.LeaveCount++;
            }
        }

        private class BScene : Scene
        {
            public BScene() : base(0, 0) {
            }
        }
    }
}
