namespace AdventOfCode

module DeScrambler =
    let dialSize = 100

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
            let correctedStart = if start = 0 then dialSize else start
            correctedStart - moves < 0
    let toDialPosition n =
            // Ensure result stays within the dial size eg. dial size 100 values [-R;R] will remain in bounds [0..99]
            // dialSize=100, n=321 -> 21
            // dialSize=100, n=1 -> 1
            // dialSize=100, n= -1004 -> 96
            (n % dialSize + dialSize) % dialSize

    let calculateNewDialPositionAfterMove (start: int) (direction: Direction) (crossedZeroCount: int) : MoveResult =

        let dialRounds, moves = decomposeMove direction
        let multiplier dir : int =
            match dir with
            | Right _ -> 1
            | Left _ -> -1

        let createResult (start: int) (direction: Direction) (rounds: int) (moves: int) =
            let passed0 = crossingZero start direction
            {
                Rounds = if passed0 then crossedZeroCount + 1 + rounds else crossedZeroCount + rounds
                Position = multiplier direction * moves + start |> toDialPosition
            }
        createResult start direction dialRounds moves

    let rec deScrambleMessages (startPosition: int) (moves: Direction list) (stoppedOnZeroCount: int) (parsedZeroCount: int) : DialResult =
        match moves with
        | head :: tail ->
            let positionAfterMove = (startPosition, head, parsedZeroCount) |||> calculateNewDialPositionAfterMove

            let updatedCount =
                if positionAfterMove.Position = 0 then stoppedOnZeroCount + 1 else stoppedOnZeroCount

            deScrambleMessages positionAfterMove.Position tail updatedCount positionAfterMove.Rounds

        | [] ->
            { Count = stoppedOnZeroCount
              Position = startPosition
              Rounds = parsedZeroCount }

