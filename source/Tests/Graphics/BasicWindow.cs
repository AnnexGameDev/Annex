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

        [Test]
        public void Test() {
            StartTest<TestCaseScene>();
            Wait(5000);
            EndTest();
        }
    }
}
