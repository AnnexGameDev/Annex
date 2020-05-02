---
layout: default
title: Audio
nav_order: 0
parent: v0.0.1
grand_parent: Annex Home
# search_exclude: true
---
# Audio

You can interact with Annex's audio module by using the Audio Manager in the service proider.

```cs
var audio = ServiceProvider.AudioManager;
```
# Playing and Stopping Audio

```cs
// Play audio
var context = new AudioContext() {
    BufferMode = BufferMode.Buffered,
    ID = "music", // some identifier to assign to the audio. Default: null
    Loop = true, // whether or not the audio loops. Default: false
    Volume = 100 // how loud the audio is ranging from 0 (no volume) to 100 (max volume). Default: 100
};
var playingAudio = audio.PlayAudio("sound.flac", context);

// Stop audio - optionally takes in an identifier
audio.StopPlayingAudio();
```

# Buffered vs NonBuffered

* Set BufferMode to None when your audio file is small in size, and is played very briefly. E.g. Sound effects
* Set BufferMode to Buffered when your audio file is large in size, and is played for long periods of time. E.g. background music.