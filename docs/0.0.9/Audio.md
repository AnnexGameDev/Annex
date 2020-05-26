---
layout: default
title: Audio
nav_order: 0
parent: v0.0.9
# search_exclude: true
---
# Audio
The audio service is made available through the service proider.

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

* Set BufferMode to None when the audio file is small in size, and is played very briefly.
* Set BufferMode to Buffered when the audio file is large in size, and is played for long periods of time.