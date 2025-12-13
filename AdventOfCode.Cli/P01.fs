namespace AdventOfCodeRunner

open AdventOfCode.DeScrambler

module SecretEntrance =
    let run () =
        let inputFile = "./AdventOfCode.Test/TestData/day01.test.txt"
        printfn "De scrambling %s" inputFile

        let moves = inputFile |> loadLinesFromFile |> loadMoves

        printfn "Moves: %d" moves.Length

        let result = deScrambleMessages  50 moves 0 0

        printfn "Result: %d " result.Count
        printfn "Result: %d " result.Rounds
        printfn "Result: %d " (result.Count + result.Rounds)

