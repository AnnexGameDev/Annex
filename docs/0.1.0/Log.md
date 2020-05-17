---
layout: default
title: Log
nav_order: 0
parent: v0.1.0
# search_exclude: true
---

# Logging
You can use `Debug.Log(message)` to add an entry into the current session's log, found in the bin/logs/ folder. Each log file is generated at the start of each session with the DateTimeStamp of the start of the session as the name.

```cs
Console.Log("We got to this point!");
```