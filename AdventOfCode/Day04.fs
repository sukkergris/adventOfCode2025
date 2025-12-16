namespace AdventOfCode

open AdventOfCode.FileHandler
module PrintingDepartment =
    open System
    let getInput path =
        let lines = path |> loadLinesFromFile
        lines

    type point = { x: int; y: int; c: char }
    type coord = { x: int; y: int }
    let toCoord x y = {x=x;y=y}

    let toBoard (lines: string list) =
        lines |> List.mapi(fun y l -> l |> Seq.mapi(fun x c -> { x = x; y = y; c = c } ) |> Seq.toList)

    let getAdjacentCoords (p: point) (abscissa: int) (ordinate:int) =
        [   toCoord (p.x-1) (p.y-1); toCoord p.x    (p.y-1); toCoord (p.x+1) (p.y-1)
            toCoord (p.x-1)  p.y   ; toCoord (p.x+1) p.y ;
            toCoord (p.x-1) (p.y+1); toCoord p.x    (p.y+1); toCoord (p.x+1) (p.y+1) ]
             |> List.filter(fun x -> x.x > -1 && x.x < abscissa && x.y > - 1 && x.y < ordinate)

    let tryFindPointsWithSupport (flattenedBoard: point list) (coord:coord) =
        flattenedBoard |> List.tryFind(fun p -> p.x =coord.x && p.y = coord.y && p.c = '@') // @ = support
    let board path = path |> getInput |> toBoard

    let getLiftAbility (p: point) (flattenedBoard: point list) (abscissa: int) (ordinate: int) =
        if p.c = '.' then p else // Ignore '.'
        // (abscissa: int) (ordinate:int)
        let a = getAdjacentCoords p abscissa ordinate
                    |> List.map(fun c-> tryFindPointsWithSupport flattenedBoard c )
                    |> List.filter(fun x -> x.IsSome )
                    |> List.length
        if a >= 4 then
            p
        else
        { p with c = 'x' }

    let getUpdatedBoard (board: point list) (abscissa: int) (ordinate: int)=
        board |> List.map(fun p -> getLiftAbility p board abscissa ordinate)

    let view right left =
        Console.SetCursorPosition(0, 0)
            // Print side-by-side comparison
        printfn "\n%s | %s" "Expected" "Actual"
        printfn "%s-+-%s" (String.replicate 10 "-") (String.replicate 10 "-")
        List.zip right left
                    |> List.iter (fun (refRow, updRow) ->
        let refChars = refRow |> List.map _.c |> List.toArray |> System.String
        let updChars = updRow |> List.map (fun p -> p.c) |> List.toArray |> System.String
        let marker = if refChars = updChars then " " else "*"
        printfn "%s | %s %s" refChars updChars marker)

    let removeX (board: point list) =
            board |> List.map(fun p -> if p.c = 'x' then { p with c = '.'} else p)

    let print (board: point list) : unit =
        Console.SetCursorPosition(0, 1)
        board
        |> List.groupBy (fun i -> i.y)
        |> List.sortBy (fun (y, _) -> y)
        |> List.iter (fun (_, v) ->
            v |> List.map (fun p -> p.c)
              |> Array.ofList
              |> System.String
              |> printfn "%s"
        )




    let rec runGame (board: point list) (abscissa: int) (ordinate: int)=
        let nextMove = getUpdatedBoard board abscissa ordinate

        print board

        let anyMovableInNextMove = nextMove |> List.exists(fun x -> x.c ='x')
        if anyMovableInNextMove then
            runGame (removeX nextMove) abscissa ordinate
        else
            board
