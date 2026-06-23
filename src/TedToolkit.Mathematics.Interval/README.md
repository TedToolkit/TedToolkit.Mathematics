# TedToolkit.Mathematics.Interval

`TedToolkit.Mathematics.Interval` is a small .NET library for working with mathematical intervals and normalized sets of intervals.

It is meant for code that needs interval semantics to be explicit and reliable instead of treating ranges as two loosely related values.

## What This Library Is For

This library helps you model:

- open and closed boundaries, such as `(1, 3]`
- singleton intervals, such as `[5, 5]`
- empty intervals
- relationships between intervals, such as disjoint, overlapping, subset, and superset
- sets of intervals that can be normalized and merged automatically

It is useful when your domain needs to answer questions like:

- Does this value belong to the range?
- Are these two ranges touching or overlapping?
- What is the union, intersection, or difference of these ranges?
- How should several ranges be stored without duplicates or redundant overlaps?

## Core Concepts

### `Interval<TData>`

Represents a single interval with:

- a minimum endpoint
- a maximum endpoint
- an open or closed boundary on each side

The type parameter must be comparable, so the library can order and compare endpoints safely.

### `IntervalSet<TData>`

Represents a normalized set of intervals.

When you create a set from multiple intervals, overlapping or adjacent intervals are merged into a cleaner representation. This makes it a good fit for domains that treat many ranges as one logical set.

## Typical Use Cases

- scheduling windows and availability ranges
- numeric constraints and validation rules
- geometry or coordinate segments
- version, score, or threshold ranges
- any business rule that depends on precise boundary behavior

## Why Use It

The main value of the library is correctness around boundary behavior.

Instead of re-implementing interval logic in each project, you get a reusable model for:

- checking membership
- clamping values into a valid range
- comparing interval relationships
- composing interval sets with union, intersection, and difference

This reduces edge-case bugs around touching boundaries, empty ranges, and partial overlap.
