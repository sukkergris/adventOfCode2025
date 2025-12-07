open AdventOfCode.DeScrambler
// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

let moves = "./d01-input.txt" |> loadLines2Array |> loadMoves

printfn "Moves: %d" moves.Length

let result = (50, moves, 0) |||> countDialsStoppedOnPositionZero

printfn "Result: %d " result.Count

