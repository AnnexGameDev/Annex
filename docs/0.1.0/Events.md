---
layout: default
title: Events
nav_order: 0
parent: v0.1.0
# search_exclude: true
---

# Events

The backbone of any game is the gameloop; a while-loop in charge of the flow of game-logic. 

```cs
// pseudo-code
while game.IsOpen:
    userInput.Process()
    gameLogic.Update()
    graphics.Draw()
```

An event is a unit of logic within the game-loop that needs to be executed repeatedly at regular intervals.

The gameloop can be accessed using the ```EventService``` in the ```ServiceProvider```.


```cs
var eventService = ServiceProvider.EventService;
```

# Priorities
In Annex, the gameloop is implemented as a priority queue where each level in the priority queue contains a collection of events relating to that priority. 

The queue cycles through all the priority levels from ```START``` to ```END``` and sequentially executes the game events stored at each priority level.

![Event Queue](https://i.imgur.com/LrPPPZa.png)

# Creating Game Events
Creating a game event can be done using the ```AddEvent``` method in the ```EventService```.


```cs
var priority = PriorityType.Input;
int interval = 1000;

eventService.AddEvent(priority, (e) => {
    // TODO: Logic
}, interval);
```

Interval denotes (in milliseconds) how long to wait before running an event again. 

Delay denotes (in milliseconds) how long to wait before running an event for the first time. By default, delay is set to 0.


## Game Event Args
The action of a game event takes in a GameEventArgs parameter which contains properties regarding the context and management of the game event.

```cs
void Handler(GameEventArgs args) {
    // Remove the event from the queue once it's done running by setting RemoveFromQueue to true 
    args.RemoveFromQueue = true;
}
```

# Event Trackers
Event trackers are objects that can be attached to game events to be notified when they're probed. A common usecase for event trackers is to be used to collect performance information like FPS.

```cs
long interval = 1000;
var tracker = new InvocationCounterTracker(interval);

tracker.CurrentCount; // How many times the event ran in this interval.
tracker.LastCount; // How many times the event ran last interval.
tracker.CurrentInterval; // How long the current interval has been running for.

string eventID;
var gameEvent = EventManager.Singleton.GetEvent(eventID);
gameEvent.AttachTracker(tracker);

Debug.AddDebugOverlayInformation(() => $"FPS: {tracker.LastCount}");
```

You can write your own custom event trackers by implementing the ```IEventTracker``` interface.