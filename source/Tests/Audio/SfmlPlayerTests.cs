using Annex;
using Annex.Assets;
using Annex.Assets.Streams;
using Annex.Assets.Services;
using Annex.Audio;
using Annex.Audio.Sfml;
using Annex.Events;
using Annex.Logging;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Threading;
using static Annex.Paths;

namespace Tests.Audio
{
    [TestFixture]
    public class SfmlPlayerTests : TestWithServiceContainerSingleton
    {
        private IAudioService _audio;
        private const string Cold = "test/Cold2.wav";
        private const string Holy = "test/Holy1.wav";

        private static Mutex _mutex = new Mutex();

        private void Wait(int ms) {
            System.Threading.Thread.Sleep(ms);
        }

        private class DefaultAudioManager : AssetManager, IAudioManager
        {
            public DefaultAudioManager() : base(new FileSystemStreamer("audio", ".wav")) {
            }
        }

        [OneTimeSetUp]
        public void SuiteSetUp() {
            ServiceContainer.Provide<ILogService>(new LogService());
            ServiceContainer.Provide<IEventService>(new EventService());
            ServiceContainer.Provide<IAudioManager>(new DefaultAudioManager());
            this._audio = ServiceContainer.Provide<IAudioService>(new SfmlPlayer());

            Debug.PackageAssetsToBinary(ServiceContainer.Resolve<IAudioManager>(), Path.Combine(SolutionFolder, "assets/audio"));
        }

        [OneTimeTearDown]
        public void SuiteCleanUp() {
            this._audio.Destroy();
        }

        [SetUp]
        public void TestSetUp() {
            _mutex.WaitOne();
        }

        [TearDown]
        public void TestCleanUp() {
            this._audio.StopPlayingAudio();
            _mutex.ReleaseMutex();
        }

        [Test]
        public void PlayAudio() {
            var audio = this._audio.PlayAudio(Holy);
            Assert.IsTrue(audio.IsPlaying);
        }

        [Test]
        public void PlayAudio_ThenStop() {
            this._audio.PlayAudio(Cold);
            this._audio.StopPlayingAudio();
            var playingAudio = this._audio.GetPlayingAudio();
            Assert.AreEqual(0, playingAudio.Count());
        }

        [Test]
        public void GetPlayingAudio_WithNullID_ReturnsAllPlayingAudio() {
            this._audio.PlayAudio(Cold, new AudioContext() {
                ID = "cold",
                Loop = true
            });
            this._audio.PlayAudio(Holy, new AudioContext() {
                ID = "holy",
                Loop = true
            });
            var playingAudio = this._audio.GetPlayingAudio();
            Assert.AreEqual(2, playingAudio.Count());
        }

        [Test]
        public void GetPlayingAudio_WithID_ReturnsPlayingAudioWithThatID() {
            this._audio.PlayAudio(Cold, new AudioContext() {
                ID = "cold",
                Loop = true
            });
            this._audio.PlayAudio(Holy, new AudioContext() {
                Loop = true
            });

            var playingAudio = this._audio.GetPlayingAudio("cold");
            Assert.AreEqual(1, playingAudio.Count());

            var audio = playingAudio.First();
            Assert.AreEqual("cold", audio.ID);
        }

        [Test]
        public void AdjustVolume_Invalid() {
            var audio = this._audio.PlayAudio(Cold, new AudioContext() {
                Loop = true
            });
            try {
                audio.Volume = -0.1f;
                Assert.Fail();
            } catch (AssertionFailedException) { }
            try {
                audio.Volume = 100.1f;
                Assert.Fail();
            } catch (AssertionFailedException) { }
        }

        [Test]
        public void AdjustVolume() {
            var audio = this._audio.PlayAudio(Cold, new AudioContext() {
                Loop = true
            });

            for (float volume = 100; volume >= 0; volume -= 0.1f) {
                audio.Volume = volume;
            }
        }

        [Test] 
        public void PlayTwoSounds() {
            for (int i = 0; i < 2; i++) {
                var audio = this._audio.PlayAudio(Cold, new AudioContext());
            }
        }

        [Test]
        public void Loop() {
            var audio = this._audio.PlayAudio(Cold, new AudioContext() {
                Loop = true
            });

            Wait(2000);
            Assert.IsTrue(audio.IsPlaying);
        }

        [Test]
        public void ModifyAfterDispose() {
            var audio = this._audio.PlayAudio(Cold);
            this._audio.StopPlayingAudio();
            audio.Volume = 20;
        }
    }
}
