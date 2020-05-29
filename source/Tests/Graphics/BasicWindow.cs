#define DEBUG
using Annex;
using Annex.Logging;
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
            ServiceProvider.Log.EnableChannel(OutputChannel.Verbose);
        }

        [Test]
        public void Test() {
            StartTest<TestCaseScene>();
            Wait(5000);
            EndTest();
        }
    }
}
