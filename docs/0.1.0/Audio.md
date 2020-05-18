---
layout: default
title: Audio
nav_order: 0
parent: v0.1.0
# search_exclude: true
---
# Audio
You can access the audio service using the service proider.

```cs
var audioService = ServiceProvider.AudioService;
```
# Playing and Stopping Audio

```cs
var context = new AudioContext() {
    BufferMode = BufferMode.Buffered,
    ID = "music",
    Loop = true,
    Volume = 100
};
var playingAudio = audioService.PlayAudio("sound.flac", context);

// Stops all playing audio
audioService.StopPlayingAudio();

// Stops all playing audio with the given id
audioService.StopPlayingAudio("music");
```

# Buffered vs NonBuffered

* Set BufferMode to None when your audio file is small in size, and is played very briefly. E.g. Sound effects
* Set BufferMode to Buffered when your audio file is large in size, and is played for long periods of time. E.g. background music.