open AdventOfCode.DeScrambler
// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

let moves = "./d01-input.txt" |> loadLinesFromFile |> loadMoves

printfn "Moves: %d" moves.Length

let result = deScrambleMessages  50 moves 0 0

printfn "Result: %d " result.Count
printfn "Result: %d " result.Rounds
printfn "Result: %d " (result.Count + result.Rounds)

