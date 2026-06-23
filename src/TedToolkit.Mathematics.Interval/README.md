# TedToolkit.Mathematics.Interval

`TedToolkit.Mathematics.Interval` provides explicit interval types and normalized interval sets for .NET.

Use it when your code needs correct boundary behavior instead of manually coordinating `min`, `max`, and inclusive/exclusive flags.

## Installation

```shell
dotnet add package TedToolkit.Mathematics.Interval
```

## Core Types

### `IntervalEnd<TData>`

Represents one endpoint value together with its boundary type:

- `BoundaryType.CLOSED`
- `BoundaryType.OPEN`

### `Interval<TData>`

Represents a single interval.

- endpoints are exposed as `Min` and `Max`
- `Create(start, end, out isReversed)` orders the endpoints for you
- `IsEmpty` reports whether the interval contains no values
- `IsInside`, `Clamp`, `GetRelationTo`, `IsAdjacentTo`, and `ToIntervalSet` expose the main operations

`TData` must implement `IComparable<TData>`.

### `IntervalSet<TData>`

Represents a normalized set of intervals.

When you construct it from multiple intervals:

- empty intervals are removed
- intervals are sorted
- overlapping or adjacent intervals are merged

It also provides `Union`, `Intersect`, and `Difference`.

### `RelationType`

Represents the relationship between two operands:

- `Disjoint`
- `Overlapping`
- `ProperSuperset`
- `ProperSubset`
- `Equal`

## Basic Usage

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

String formatting follows mathematical interval notation:

```csharp
Console.WriteLine(closed);   // [1, 5]
Console.WriteLine(openLeft); // (1, 5]
```

Special cases:

- a singleton interval prints as the value itself, for example `1`
- an empty interval prints as `Ø`

## Interval Sets

`IntervalSet<TData>` normalizes overlapping or adjacent intervals automatically:

```csharp
var set = new IntervalSet<int>(
    Interval<int>.Create(1, 2, out _),
    Interval<int>.Create(2, 4, out _),
    Interval<int>.Create(6, 8, out _));

Console.WriteLine(set); // [1, 4] ∪ [6, 8]
```

Set operations are available directly:

```csharp
var left = new IntervalSet<int>(Interval<int>.Create(1, 4, out _), Interval<int>.Create(6, 8, out _));
var right = new IntervalSet<int>(Interval<int>.Create(3, 7, out _));

Console.WriteLine(left.Intersect(right)); // [3, 4] ∪ [6, 7]
Console.WriteLine(left.Union(right));     // [1, 8]
```

Difference preserves boundary correctness:

```csharp
var current = new IntervalSet<int>(Interval<int>.Create(1, 6, out _));
var removed = new IntervalSet<int>(Interval<int>.Create(3, 4, out _));

Console.WriteLine(current.Difference(removed)); // [1, 3) ∪ (4, 6]
```

## Notes

- Empty intervals are represented explicitly and participate in comparisons.
- `Clamp` throws `InvalidOperationException` for an empty interval.
- Adjacency is value-based and takes boundary openness into account.

## Target Frameworks

- `net6.0`
- `net7.0`
- `net8.0`
- `net9.0`
- `net10.0`
- `net472`
- `net48`
- `netstandard2.0`
- `netstandard2.1`
