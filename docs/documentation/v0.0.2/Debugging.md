Annex offers several debugging tools that you can leverage to your discretion. All of these tools apply only on DEBUG builds and will not affect performance on RELEASE builds.

# Event Trackers
In order to make sure certain critical game-events are meeting your expectations, you can attach performance trackers to them. Trackers keep track of how many times an event gets executed in a certain interval. 

```CSharp
long interval = 1000; // How often the interval resets.
var tracker = new EventTracker(interval);

tracker.CurrentCount; // How many times the event ran in this interval.
tracker.LastCount; // How many times the event ran last interval.
tracker.Interval; // Corresponds to interval.
tracker.CurrentInterval; // How long the current interval has been running for.

string eventID;
var gameEvent = EventManager.Singleton.GetEvent(eventID);
gameEvent.AttachTracker(tracker);
```

# Logging
You can use `Debug.Log(message)` to add an entry into the current session's log, found in the bin/logs/ folder. Each log file is generated at the start of each session with the DateTimeStamp of the start of the session as the name.

```CSharp
Console.Log("We got to this point!");
```

# Assertions 
You can use `Debug.Assert(bool)` in code that makes assumptions about the state of your game. If the assumption is invalid, an exception will be thrown and a log entry will be made noting the details of the failed assertion.

```CSharp
float Divide(float numerator, float denominator) {
    Debug.Assert(denominator != 0);
    return numerator / denominator;
}
```

# Overlay
You can gain access to a game-overlay, similar to a CLI, which you can use to display information, or execute commands. You can toggle this overlay for your current scene by calling its corresponding function.


***
**PLEASE NOTE THAT DUE TO THE LACK OF DEFAULT FONTS IN ANNEX, YOU CURRENTLY NEED A FONT FILE CALLED `default.ttf` IN ORDER FOR THE OVERLAY TO WORK**
***

```CSharp
Debug.ToggleDebugOverlay();
```

Without overlay
![](https://i.imgur.com/xD23ZpR.png)

With overlay
![](https://i.imgur.com/8pdNerH.png)

## Commands
The top of this overlay is a rudimentary command-line-interface. While the overlay is visible, you can type into the CLI and hit enter to execute that command. 

A command is the first word in the user-input (case-insensitive) and needs its corresponding data.
The data is the text trailing the command, which is split by whitespace.

## Example
A debug command that sets the player's position might look like this.

```
SetPlayerPosition 200 300
```

You can implement this debug command by using the `Debug.AddDebugCommand` function.

```CSharp
Debug.AddDebugCommand("setplayerposition", (data) => {
    float x = float.Parse(data[0]);
    float y = float.Parse(data[1]);
    player.SetPosition(x, y);
});
```

Every command you enter gets stored, and can be flipped through using the up and down arrow-keys - similar to a regular CLI. You can remove all this history by executing the "clear" command.

## Information
To display information on the overlay, you can use the `Debug.AddDebugInformation` function.
For example, you can display your game's FPS by using Debug Information along with event trackers.

```CSharp
 var drawEvent = EventManager.Singleton.GetEvent(GameWindow.DrawGameEventID);
var fpsTracker = new EventTracker(1000);
drawEvent.AttachTracker(fpsTracker);

Debug.AddDebugInformation(() => $"FPS: {fpsTracker.LastCount}");
```