---
layout: default
title: Log
nav_order: 0
parent: v0.0.9
# search_exclude: true
---

# Logging
The log is available as a service through the service provider.


```cs
var log = ServiceProvider.Log;

// Non-formatting logging
log.WriteClean("No formatting done. No new line");
log.WriteLineClean("No formatting done");

// Channels
log.WriteVerbose("message");
log.WriteWarning("message");
log.WriteError("message");

// Tracing - appends module/PID/TID information to each statement
log.WriteTrace(this, "trace");
log.WriteTrace_Module("Module", "trace");
```


Only the ```Error``` channel is enabled by default.

```cs
log.EnableChannel(OutputChannel.Verbose);
log.EnableChannel(OutputChannel.Warning);
```