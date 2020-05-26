---
layout: default
title: Service Provider
nav_order: 0
parent: v0.0.9
# search_exclude: true
---

The Annex framework operates largely on the singleton pattern.

```cs
// Retrieve a singleton
var events = GameEvents.Singleton;
```

The singletons already set up by Annex are
* Log
* EventManager
* SceneManager
* GameWindow
* AudioManager

If you are defining your own singletons, it's recommended to follow the convention that Annex uses which guarantees that a singleton will exist whenever you need it.

```cs
using Annex;

public class Foo : Singleton
{
    static Foo() {
        Create<Foo>();
    }
    public static Foo Singleton => Get<Foo>();
}
```

Please note that singleton classes must have a default constructor.