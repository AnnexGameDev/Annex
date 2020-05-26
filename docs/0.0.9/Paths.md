---
layout: default
title: Paths
nav_order: 0
parent: v0.0.9
# search_exclude: true
---

# Paths

**Note**: ```SourceFolder``` and ```SolutionFolder``` should be avoided as much as possible, even if you can guarentee you are on a DEBUG build. Existance of the .sln file is **never guaranteed**. 
{: .note }

| Path | Description | Comment | 
|:-----|:------------|:--------|
| Paths.ApplicationPath | Location of the binary | |
| Paths.SourceFolder | Location of the source folder| Defaults to C:\Source if the .sln cannot be found |
| Paths.SolutionFolder | Location of the .sln file | Defaults to C:\ if the .sln cannot be found |
