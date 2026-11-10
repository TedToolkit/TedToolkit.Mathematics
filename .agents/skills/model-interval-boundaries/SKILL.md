---
name: model-interval-boundaries
description: >-
  Use TedToolkit.Mathematics.Interval when range membership or interval sets depend on explicit open, closed, empty, or unbounded endpoints.
---

# Model Interval Boundaries

- Represent each endpoint with the correct boundary kind and use the interval API for membership and set operations.
- Use a normalized interval set when composing multiple ranges. Preserve domain semantics when two ranges touch at an excluded endpoint.
- Test values exactly at each endpoint as well as empty, touching, or unbounded cases relevant to the caller.
