namespace AdventOfCode

module DeScrambler =
    let start = 50;
    let dialSize = 100;


    type Direction = Left of int | Right of int
    let loadLines2Array (path : string) : string list = System.IO.File.ReadAllLines(path) |> Array.toList

    let unwrapIntFromDialPosition (dial : Direction) : string=
        match dial with
            | Right value -> sprintf "Move %d right" value
            | Left value -> sprintf "Move %d left" value

    let string2int ( chars: char list) = chars|> System.String.Concat |> int
    let string2Direction (input: string) =
        match input |> List.ofSeq with
            | 'R' :: tail -> tail |> string2int |> Right
            | 'L' :: tail -> tail |> string2int |> Left
            | _ -> "Epic failure" |> System.Exception |> raise

    let unwrapDirection direction =
        match direction with
            | Right r -> r
            | Left l -> l

    let loadMoves (list: string list) : list<Direction> =
        List.map (fun x-> x |> string2Direction ) list
    type NewResult = {Rounds: int; Position: int }
    type Result = { Count: int; Position: int; Rounds: int }

    let parsingZeroDuringMove start direction : bool =
        let unwrapped = unwrapDirection direction
        let rounds = unwrapped / 100
        let moves = unwrapped - (rounds * 100)
        match direction with
            | Right r ->
                start + moves > 100
            | Left l ->
                let correctedStart = if start = 0 then 100 else start
                correctedStart - moves < 0
    let calculateNewDialPositionAfterMove (start: int) (direction: Direction) (completedRounds: int): NewResult =
        let normalize n =
            // Ensure result stays in [0..99]
            ((n % 100) + 100) % 100
        // printfn $"Start: {start}, Direction: {unwrapIntFromDialPosition direction}, Completed rounds: {completedRounds}"
        let unwrapped = unwrapDirection direction
        let rounds = unwrapped / 100
        let moves = unwrapped - (rounds * 100)
        match direction with
            | Right r ->
                let passed0 = parsingZeroDuringMove start direction
                {
                    Rounds = if passed0 then completedRounds + 1 + rounds else completedRounds + rounds;
                    Position = moves + start |> normalize
                }
            | Left l ->
                let passed0 = parsingZeroDuringMove start direction
                {
                    Rounds = if passed0 then completedRounds + 1 + rounds else completedRounds + rounds;
                    Position = start - moves |> normalize
                }
    let rec countDialsStoppedOnPositionZero (startPosition: int) ( moves: list<Direction>) (numberOfIterationsAggregated: int) (numberOfTimesPassedZero: int): Result =
        // printfn "Passed zero count: %d" numberOfTimesPassedZero
        match moves with
            | head :: tail ->
                let positionAfterMove = (startPosition, head, numberOfTimesPassedZero)|||> calculateNewDialPositionAfterMove
                let updatedCount =
                    if positionAfterMove.Position = 0 then numberOfIterationsAggregated + 1
                    else numberOfIterationsAggregated
                countDialsStoppedOnPositionZero positionAfterMove.Position tail updatedCount positionAfterMove.Rounds
            | [] -> { Count = numberOfIterationsAggregated; Position = startPosition; Rounds = numberOfTimesPassedZero }

