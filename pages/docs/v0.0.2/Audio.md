You can interact with Annex's audio module by using the Audio Manager singleton. 

``` CSharp
var audio = AudioManager.Singleton;
```
# Playing and Stopping Audio

``` CSharp
// Play audio
audio.PlayBufferedAudio("music.flac");
audio.PlayAudio("sfx.flac");

// Stop audio
audio.StopAllAudio();
```
You can specify three other optional parameters when playing audio.

``` CSharp
// whether or not the audio loops. 
// By default, loop is set to false.
bool loop = true; 

// how loud the audio is ranging from 0 (no volume) to 100 (max volume).
// By default, volume is set to 100.
float volume = 100.0f; 

// some identifier to assign to the audio
string id = "background music";

audio.PlayBufferedAudio("music.flac", id, loop, volume);

// you can use id to stop all audio with that id
audio.StopAllAudio(id);
```

# Buffered vs NonBuffered

* use PlayAudio when your audio file is small in size, and is played very briefly. E.g. Sound effects
* use PlayAudioBuffered when your audio file is large in size, and is played for long periods of time. E.g. background music.