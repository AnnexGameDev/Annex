#define DEBUG
using Annex.Scenes.Components;
using NUnit.Framework;

namespace Tests.Graphics
{
    public class BasicWindow : SfmlCanvasTestCase
    {
        public class TestCaseScene : Scene
        {
        }

        public BasicWindow() {
        }

        // TODO: Broken test case because of removal of AnnexGame
        [Test]
        public void Test() {
            StartTest<TestCaseScene>();
            Wait(5000);
            EndTest();
        }
    }
}
