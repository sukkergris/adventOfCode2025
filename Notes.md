# Notes

```sh
> dotnet add ~/private/fun/adventofcode2025/AdventOfCode.Cli reference ~/private/fun/adventofcode2025/AdventOfCode
Reference `..\AdventOfCode\AdventOfCode.fsproj` added to the project.
```

```sh
> dotnet new console -lang f# -n AdventOfCode.Cli
> dotnet new classlib -lang f# -n AdventOfCode.Benchmarks

dotnet add package BenchmarkDotNet
❯ dotnet dotnet add reference ~/private/fun/adventofcode2025/AdventOfCode
Reference `..\AdventOfCode\AdventOfCode.fsproj` added to the project.

```

## Day 3 benchmarks

| Method        | Mean     | Error    | StdDev  | Gen0     | Gen1     | Allocated  |
|-------------- |---------:|---------:|--------:|---------:|---------:|-----------:|
| GeminiSolver  | 115.9 us |  1.63 us | 1.45 us |  48.8281 |  19.0430 |  399.42 KB |
| TheodorSolver | 636.4 us | 11.17 us | 9.90 us | 529.2969 | 170.8984 | 4329.04 KB |
