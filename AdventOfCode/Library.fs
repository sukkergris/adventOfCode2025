namespace AdventOfCode

module DeScrambler =
    let start = 50;
    let dialSize = 100;


    type Direction = Left of int | Right of int
    let loadLines2Array (path : string) : string list = System.IO.File.ReadAllLines(path) |> Array.toList

    let unwrapIntFromDialPosition (dial : Direction) : int=
        match dial with
            | Right value -> value
            | Left value -> value

    let string2int ( chars: char list) = chars|> System.String.Concat |> int
    let string2Direction (input: string) =
        match input |> List.ofSeq with
            | 'R' :: tail -> tail |> string2int |> Right
            | 'L' :: tail -> tail |> string2int |> Left
            | _ -> "Epic failure" |> System.Exception |> raise

    let loadMoves (list: string list) : list<Direction> =
        List.map (fun x-> x |> string2Direction ) list


    let calculateNewDialPositionAfterMove (start: int) (direction: Direction) : int =
        let normalize n =
            // Ensure result stays in [0..99]
            ((n % 100) + 100) % 100
        match direction with
            | Right r ->
                let rounds = r / 100
                let moves = r - (rounds * 100)
                moves + start |> normalize
            | Left l ->
                let rounds = l / 100
                let moves = l - (rounds * 100)
                start - moves |> normalize

    type Result = { Count: int; Position: int }
    let rec countDialsStoppedOnPositionZero (startPosition: int) ( moves: list<Direction>) (numberOfIterationsAggregated: int) : Result =
        match moves with
            | head :: tail ->
                let positionAfterMove = (startPosition, head)||> calculateNewDialPositionAfterMove
                printfn "Position after move: %d" positionAfterMove
                let updatedCount =
                    if positionAfterMove = 0 then numberOfIterationsAggregated + 1
                    else numberOfIterationsAggregated
                (positionAfterMove ,tail, updatedCount)|||>countDialsStoppedOnPositionZero
            | [] -> { Count = numberOfIterationsAggregated; Position = startPosition }

