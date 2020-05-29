---
layout: default
title: Events
nav_order: 2
parent: v0.0.2
search_exclude: true
---

**Careful:** This page was for meant for an older version of Annex
{: .deprecated }

The backbone of any game is the gameloop, and all the game events that the gameloop kicks off.
A gameloop is a while-loop which is in charge of the flow of game-logic.

```cs
// pseudo-code
while game.IsOpen:
    userInput.Process()
    gameLogic.Update()
    graphics.Draw()
```

In Annex, the **EventManager** singleton represents the gameloop as a priority queue. Each level in the priority queue contains a collection of events relating to that priority.

# Priorities
Each game event is given a priority, which tells Annex at which point in the queue that event should be run.

The queue cycles through all the priority levels, starting with START and finishing with END, and executes the game events stored at that priority level.

![Event Queue](https://i.imgur.com/LrPPPZa.png)

You can see all the priority types in Annex.Events.PriorityTypes.cs

```cs
    public enum PriorityType
    {
        START,
        LOGIC,
        ANIMATION,
        INPUT,
        CAMERA,
        GRAPHICS,
        SOUNDS,
        END
    }
```

Note:
If you're building Annex yourself, you can even add your own priority types or re-order the existing ones to have more intricate control over your gameloop flow.

# Intervals

Each game event is given an interval, which tells Annex how often the event should be run. The queue is able to cycle through its events hundreds of times per second, but you usually don't want your game events to run hundreds of times per second.

Let's say you want to implement a passive healing system in your game where your player gains 10hp every five seconds. The game event function might look like this.

```cs
public ControlEvent PassiveHeal() {
    player.HP += 10;
    return ControlEvent.None;
}
```

In this case, you should set the interval value to 5000 (5000 milliseconds == 5 seconds). 

```cs
var gameEvent = PassiveHeal;
int interval = 5000;
int delay = 0;
var priority = PriorityType.LOGIC;
events.AddEvent(priority, PassiveHeal, interval, delay);
```

This way, even though the event queue is probing the event hundreds of times per second to see if it should run, the event will only run once every five seconds.

# Delay
Sometimes when you create an event, you don't want it to run right away. The delay value specifies how long you want to wait before the event queue will start running your game event.

For example, if you want your passive healing function to run but only 10 seconds from now, specify a delay of 10000 (10000 milliseconds == 10 seconds).

```cs
var gameEvent = PassiveHeal;
int interval = 5000;
int delay = 0;
var priority = PriorityType.LOGIC;
events.AddEvent(priority, gameEvent, interval, delay);
```

## Control Events
The return value for game events is a ControlEvent enumeration value. This value tells the queue what to do with the game event when it's done running. 

```cs
public enum ControlEvent
{
    NONE,
    REMOVE
}
```

**NONE** tells the event queue to do nothing.

**REMOVE** tells the event queue to remove the event from the queue.

# Creating game events

Adding game events is simple.

```cs
    ...
    var events = EventManager.Singleton;
    var priority = PriorityType.LOGIC;
    int interval = 16;
    int delay = 0;
    var eventFunction = GameEvent;

    events.AddEvent(priority, eventFunction, interval, delay);
}
public ControlEvent GameEvent() {
    return ControlEvent.NONE;
}
```
but this form of event creation can be quite verbose. While not required, it would be more clean to not use neatly written variables, and use lambdas instead of writing member functions.

```cs
var events = EventManager.Singleton;
events.AddEvent(PriorityType.LOGIC, () => { 
    return ControlEvent.NONE;
}, 16, 0);
```