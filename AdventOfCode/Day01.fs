namespace AdventOfCode

module DeScrambler =
    let start = 50
    let dialSize = 100

    type Direction =
        | Left of int
        | Right of int

    let loadLines2List (path: string) : string list =
        System.IO.File.ReadAllLines path |> Array.toList

    let string2int (chars: char list) =
        chars |> System.String.Concat |> int

    let string2Direction (input: string) =
        match input |> List.ofSeq with
        | 'R' :: tail -> tail |> string2int |> Right
        | 'L' :: tail -> tail |> string2int |> Left
        | _ -> "Epic failure" |> System.Exception |> raise

    let unwrapDirection direction =
        match direction with
        | Right r -> r
        | Left l -> l

    let loadMoves (list: string list) : Direction list =
        list |> List.map string2Direction

    type NewResult = { Rounds: int; Position: int }
    type Result = { Count: int; Position: int; Rounds: int }

    let parsingZeroDuringMove start direction : bool =
        let unwrapped = unwrapDirection direction
        let rounds = unwrapped / dialSize
        let moves = unwrapped - (rounds * dialSize)

        match direction with
        | Right _ -> start + moves > dialSize
        | Left _ ->
            let correctedStart = if start = 0 then dialSize else start
            correctedStart - moves < 0

    let calculateNewDialPositionAfterMove (start: int) (direction: Direction) (completedRounds: int) : NewResult =
        let normalize n =
            // Ensure result stays in [0..99]
            ((n % 100) + 100) % 100

        let unwrapped = unwrapDirection direction
        let rounds = unwrapped / dialSize
        let moves = unwrapped - (rounds * dialSize)
        let dir direction : int =
            match direction with
            | Right _ -> 1
            | Left _ -> -1

        let createResult (start: int) (direction: Direction) (rounds: int) (moves: int) =
            let passed0 = parsingZeroDuringMove start direction

            {
                Rounds = if passed0 then completedRounds + 1 + rounds else completedRounds + rounds
                Position = dir direction * moves + start |> normalize
            }

        match direction with
        | Right _ -> createResult start direction rounds moves
        | Left _ -> createResult start direction rounds moves

    let rec countDialsStoppedOnPositionZero (startPosition: int) (moves: Direction list) (numberOfIterationsAggregated: int) (numberOfTimesPassedZero: int) : Result =
        // printfn "Passed zero count: %d" numberOfTimesPassedZero
        match moves with
        | head :: tail ->
            let positionAfterMove = (startPosition, head, numberOfTimesPassedZero) |||> calculateNewDialPositionAfterMove

            let updatedCount =
                if positionAfterMove.Position = 0 then numberOfIterationsAggregated + 1 else numberOfIterationsAggregated

            countDialsStoppedOnPositionZero positionAfterMove.Position tail updatedCount positionAfterMove.Rounds

        | [] ->
            { Count = numberOfIterationsAggregated
              Position = startPosition
              Rounds = numberOfTimesPassedZero }

