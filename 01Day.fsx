// Hello.fsx
printfn "Hello from F# Script!"

// You can still use the F# entry point if you prefer, but it's optional
let args = fsi.CommandLineArgs |> Array.tail
if args.Length > 0 then
    printfn "Argument received: %s" args.[0]

type Direction = Left of int | Right of int | Failure

type DialPosition = DialPosition of int
let mutable counter = 0
let mutable roundsC = 0
let mutable dialPosition : DialPosition = DialPosition 50

let int2DialPositionNumber (value : int): int =
        printfn "Input: %d" value
        let rounds = value / 100
        printfn "Rounds: %d" rounds
        roundsC <- abs( rounds ) + roundsC
        let moves = value-rounds * 100
        if value < 0 then
            let unwrapIntFromDialPosition dialPosition
            let newDialPosition = 100 + moves
            100 + moves
        else
            moves

let unwrapIntFromDialPosition dial : int =
    match dial with
        | DialPosition value -> value
let tailString2DialPositionNumber (chars: char list) : int =
    chars |> System.String.Concat |> int |> int2DialPositionNumber
let rec plotZeroMatched line (start :DialPosition) count: int * int =
    let direction : Direction = match line |> List.ofSeq with
                                        | 'R' :: tail -> Right (tailString2DialPositionNumber tail)
                                        | 'L' :: tail -> Left (
                                            line |>
                                            Seq.tail |>
                                            System.String.Concat |> int |> int2DialPositionNumber)
                                        | _ -> Failure

    printfn "Dial position: %d" (start |> unwrapIntFromDialPosition)

    let x : int = match direction with
                        | Left moves ->
                            let m = (start |> unwrapIntFromDialPosition) - moves
                            printfn "Left %d" m
                            m
                        | Right moves->
                            let m = (start |> unwrapIntFromDialPosition)  + moves
                            printfn "Right %d" m
                            m
                        | Failure ->
                             printf  "Epic fail"
                             raise (System.Exception("Epic fail"))

    let correctedPosition : int = int2DialPositionNumber x
    printfn "Corrected position: %d" correctedPosition

    if correctedPosition = 0 then
        printfn "Matched position zero: %d" correctedPosition
        printfn "Moves %d" correctedPosition
        1, correctedPosition
    else
        printfn "Moves %d" correctedPosition
        0, correctedPosition

let lines = System.IO.File.ReadAllLines( args.[0])
for line in lines do
    let count, dial = plotZeroMatched line dialPosition counter
    counter <- count + counter
    dialPosition <- DialPosition dial

printfn "%d" (counter + roundsC)
