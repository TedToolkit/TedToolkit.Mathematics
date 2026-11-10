---
name: model-interval-boundaries
description: >-
  Use TedToolkit.Mathematics.Interval when range membership or interval sets depend on explicit open, closed, empty, or unbounded endpoints.
---

# Model Interval Boundaries

State whether each endpoint belongs to the range. Preserve that choice when converting from a predicate, sorting ranges, or combining interval sets.

## Workflow

- Represent each endpoint with the correct boundary kind and use the interval API for membership and set operations.
- Use a normalized interval set when composing multiple ranges. Preserve domain semantics when two ranges touch at an excluded endpoint.
- Test values exactly at each endpoint as well as empty, touching, or unbounded cases relevant to the caller.

## Example

```csharp
using TedToolkit.Mathematics.Interval;

var closed = Interval<int>.Create(1, 5, out _);
var openLeft = Interval<int>.Create(
    new IntervalEnd<int>(1, BoundaryType.OPEN),
    new IntervalEnd<int>(5),
    out _);

Console.WriteLine(closed.IsInside(1));             // True
Console.WriteLine(openLeft.IsInside(1));           // False
Console.WriteLine(closed.GetRelationTo(openLeft)); // ProperSuperset
```

## Check the result

Check intersection and union with intervals that touch at one excluded endpoint, in addition to ordinary overlap. Keep the numeric domain and open/closed semantics explicit in the test.
