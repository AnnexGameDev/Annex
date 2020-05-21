---
layout: default
title: Data
nav_order: 0
parent: v0.1.0
# search_exclude: true
---

# Shared Data

Shared data is a mechanism to maintain references to the same data (primarily primitive data) across different objects. This behaviour is achieved through the use of the generic wrapper class ```Annex.Data.Shared```.

## String ```class String : Shared<string>```

A wrapper class for a string.

## Int
A wrapper class for a 32 bit integer.

## ScalingInt
A type of shared Int that applies a scaling integer to a base value.

Setting the internal property *Value* will set the scale component.
Getting the internal property *Value* will return the base component times the scale component.
```cs
var shared = new SharedInt(5, 10);
int product;

shared.Value = 20; // These two lines are synonymous.
shared.Scale.Value = 20;

product = shared.Value; // These two lines are synonymous.
product = shared.Base.Value * shared.Scale.Value;
```

## IntRect
Defines an integer rectangle structure that can be shared across objects.

## Float
Defines a 32 bit floating point number that can be shared across objects.

## Vector
Defines a two-dimentional vector that can be shared across objects. Vectors must be created using the static Create method.

```cs
var pos = Vector.Create(5, 10);
```

## OffsetVector
A type of shared vector which applies an offset to the base vector.

Setting the internal X/Y component of an offset vector will set the offset X/Y component.
Getting the internal X/Y component of an offset vector returns the base X/Y component plus the offset X/Y component.

```cs
var v = new OffsetVector(5, 10);
float x;
float y;

v.X = 20; // These two lines are synonymous.
v.Offset.X = 20;

v.Y = 20; // These two lines are synonymous.
v.Offset.Y = 20;

x = v.X; // These two lines are synonymous.
x = v.Base.X + v.Offset.X;

y = v.Y; // These two lines are synonymous.
y = v.Base.Y + v.Offset.Y;
```

## ScalingVector
A type of shared vector which applies a scale to the base vector.

Setting the internal X/Y component of a scaling vector will set the scale X/Y component.
Getting the internal X/Y component of a scaling vector returns the base X/Y component times the scale X/Y component.

```cs
var v = new ScalingVector(5, 10);
float x;
float y;

v.X = 20; // These two lines are synonymous.
v.Scale.X = 20;

v.Y = 20; // These two lines are synonymous.
v.Scale.Y = 20;

x = v.X; // These two lines are synonymous.
x = v.Base.X * v.Scale.X;

y = v.Y; // These two lines are synonymous.
y = v.Base.Y * v.Scale.Y;
```