namespace AdventOfCode.Benchmarks

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open AdventOfCode.Lobby
open System.IO

// dotnet run -c Release --project AdventOfCode.Benchmarks
module Benchmark =
    [<MemoryDiagnoser>]
    type Day03Benchmarks() =
        let testDataPath =
            Path.Combine(__SOURCE_DIRECTORY__, "..", "AdventOfCode.Test", "TestData", "day03.01.txt")

        [<Benchmark>]
        member _.GeminiSolver() =
            maxJoltageUsingNBatteriesGemini testDataPath 12

        [<Benchmark>]
        member _.TheodorSolver() =
            maxJoltageUsingNBatteriesTheodor testDataPath 12

    [<EntryPoint>]
    let main args =
        BenchmarkRunner.Run<Day03Benchmarks>() |> ignore
        0
