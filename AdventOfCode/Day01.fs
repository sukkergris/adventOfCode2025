namespace AdventOfCode

module DeScrambler =
    let dialSize = 100
    let dialStart = dialSize - dialSize

    type Direction =
        | Left of int
        | Right of int

    let loadLinesFromFile (path: string) : string list =
        System.IO.File.ReadAllLines path |> Array.toList

    let parseInt (chars: char list) =
        chars |> System.String.Concat |> int

    let parseDirection (input: string) =
        match input |> List.ofSeq with
        | 'R' :: tail -> tail |> parseInt |> Right
        | 'L' :: tail -> tail |> parseInt |> Left
        | _ -> "Epic failure" |> System.Exception |> raise

    let unwrapDirection direction =
        match direction with
        | Right r -> r
        | Left l -> l


    let loadMoves (list: string list) : Direction list =
        list |> List.map parseDirection

    type MoveResult = { Rounds: int; Position: int }
    type DialResult = { Count: int; Position: int; Rounds: int }
    type DialPosition = | DialPosition of int


    let decompose direction =
        match direction with
        | Right r -> r
        | Left l -> l

    let decomposeMove direction =
        let magnitude = unwrapDirection direction
        let dialRounds = magnitude / dialSize
        let efficientDialMove = magnitude - dialRounds * dialSize
        dialRounds, efficientDialMove

    let crossingZero start direction : bool =
        let _, moves = decomposeMove direction

        match direction with
        | Right _ -> start + moves > dialSize
        | Left _ ->
            let correctedStart = if start = dialStart then dialSize else start
            correctedStart - moves < 0
    let toDialPosition n =
            // Ensure result stays within the dial size eg. dial size 100 values [-R;R] will remain in bounds [0..99]
            // dialSize=100, n=321 -> 21
            // dialSize=100, n=1 -> 1
            // dialSize=100, n= -1004 -> 96
            (n % dialSize + dialSize) % dialSize

    // let (+) (DialPosition pos) direction =
    //     let multiplier =
    //         match direction with
    //         | Right _ -> 1
    //         | Left _ -> -1
    //     let magnitude = unwrapDirection direction
    //     let moves = magnitude % dialSize
    //     DialPosition (toDialPosition (pos + multiplier * moves))
    let getNextDialPosition (start: int) (direction: Direction) (crossedZeroCount: int) : MoveResult =

        let x  =
            match direction with
            | Right value ->
                let endPosition = value |> toDialPosition
                let rounds = value / dialSize
                let crossedZero = start + value > dialSize
                endPosition, rounds + crossedZeroCount, crossedZero

            | Left value ->
                let endPosition = - value |> toDialPosition
                let rounds = value / dialSize
                let correctedStart = if start = dialStart then dialSize else start
                let crossedZero = correctedStart - value < 0

                endPosition, rounds + crossedZeroCount, crossedZero
        let endPosition, dialRounds,crossedZero = x

        {
            Rounds = if crossedZero then  1 + dialRounds else dialRounds
            Position = endPosition
        }
    let calculateNewDialPositionAfterMove (start: int) (direction: Direction) (crossedZeroCount: int) : MoveResult =

        let dialRounds, moves = decomposeMove direction
        let multiplier dir : int =
            match dir with
            | Right _ -> 1
            | Left _ -> -1

        let passed0 = crossingZero start direction
        {
            Rounds = if passed0 then crossedZeroCount + 1 + dialRounds else crossedZeroCount + dialRounds
            Position = multiplier direction * moves + start |> toDialPosition
        }

    let rec deScrambleMessages (startPosition: int) (moves: Direction list) (stoppedOnZeroCount: int) (parsedZeroCount: int) : DialResult =
        match moves with
        | head :: tail ->
            let positionAfterMove = (startPosition, head, parsedZeroCount) |||> calculateNewDialPositionAfterMove

            // let positionAfterMove = getNextDialPosition startPosition head parsedZeroCount

            let updatedCount =
                if positionAfterMove.Position = dialStart then stoppedOnZeroCount + 1 else stoppedOnZeroCount

            deScrambleMessages positionAfterMove.Position tail updatedCount positionAfterMove.Rounds

        | [] ->
            { Count = stoppedOnZeroCount
              Position = startPosition
              Rounds = parsedZeroCount }



