open AdventOfCode.DeScrambler
// For more information see https://aka.ms/fsharp-console-apps

let inputFile = "./x.txt" //"./d01-input.txt"
printfn "De scrambling %s" inputFile

let moves = inputFile |> loadLinesFromFile |> loadMoves

printfn "Moves: %d" moves.Length

let result = deScrambleMessages  50 moves 0 0

printfn "Result: %d " result.Count
printfn "Result: %d " result.Rounds
printfn "Result: %d " (result.Count + result.Rounds)

