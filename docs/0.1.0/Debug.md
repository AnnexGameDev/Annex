---
layout: default
title: Debug
nav_order: 0
parent: v0.1.0
# search_exclude: true
---

# Debug
Sometimes, for the purposes of debugging, it's useful to have certain functionality available in debug builds, but not in release builds like running administrative commands, or displaying diagnostic information. The Debug class contains functionality only present in Debug builds.

# Sanity Checks
To verify that your code is behaving as expected, without affecting performance on release builds, usage of ```Assert```, ```ErrorIf```, and ```Fail``` is recommended.

| Name | Parameters | Behaviour |
|:------------|:-----------|:--------|
| Debug.Assert | (bool, string) | If the condition is false, an AssertionFailedException is thrown with the given message |
| Debug.ErrorIf | (bool, string) | If the condition is true, an AssertionFailedException is thrown with the given message |
| Debug.Fail | (string) | An AssertionFailedException is thrown with the given message |

``` cs
Debug.Assert(condition, "Condition is not met");
Debug.ErrorIf(!condition, "Condition is not met");
Debug.Fail("Condition is not met");
```

# Debug Overlay
The debug overlay is a developer console which can display information, and receive input. The overlay visibility can be toggled by calling ```ToggleDebugOverlay```.

``` cs
Debug.ToggleDebugOverlay();
```

**Note**: In order to display debug information, or use debug commands, your canvas font manager must have a font named **default.ttf**
{: .note }


## Debug Overlay Information
The debug overlay can display textual content. A common usecase would be to display the game's FPS. Information can be added to the overlay by calling ```AddDebugOverlayInformation```.

``` cs
// Attach a tracker that resets every second to the game event responsible for drawing the game
var tracker = new EventTracker(1000);
var drawEvent = ServiceProvider.EventManager.GetEvent(Annex.Graphics.EventIDs.DrawGameEventID);
drawEvent.AttachTracker(tracker);

// Display the last count of the event tracker
Debug.AddDebugOverlayInformation(() => $"FPS: {tracker.LastCount}");
```

## Debug Overlay Command
The debug overlay has a simple CLI built in. A command can be added by using ```AddDebugOverlayCommand```.

A command is the first word in the user-input (case-insensitive). The arguments are the text trailing the command, which is split by whitespace.

``` cs
Debug.AddDebugOverlayCommand("AddMoney", (args) => {
    int quantity = int.Parse(args[0]);
    player.Money += quantity;
});
```

The up and down arrow keys can be used to scroll through the previously executed command history.


# Asset Synchronization
Game music and textures are usually not embedded into your application directly but are files stored somewhere (usually) in the same folder as the game application. Since Bin folders should not be subject to version control, assets should be contained externally. To migrate assets from this external assets folder to the binary, use the function ```PackageAssetsToBinaryFrom```.

``` cs
Debug.PackageAssetsToBinaryFrom(assetType, sourcePath);
```