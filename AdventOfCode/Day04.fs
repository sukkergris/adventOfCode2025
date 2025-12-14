namespace AdventOfCode

open AdventOfCode.FileHandler
module PrintingDepartment =
    let getInput path =
        let lines = path |> loadLinesFromFile
        lines

    type point = { x: int; y: int; c: char }
    type coord = { x: int; y: int }
    let toCoord x y = {x=x;y=y}

    let toBoard (lines: string list) =
        lines |> List.mapi(fun y l -> l |> Seq.mapi(fun x c -> { x = x; y = y; c = c } ) |> Seq.toList)

    let getAdjacentCoords (p: point) (abscissa: int) (ordinate:int) =
        [   toCoord p.x (p.y-1);
            toCoord (p.x-1) p.y; toCoord (p.x+1) p.y ;
            toCoord p.x (p.y+1)] |> List.filter(fun x -> x.x > -1 && x.x < abscissa && x.y > - 1 && x.y < ordinate)

    let tryFind (board: point list list) (coord:coord) =
        board |> List.map(fun x -> x |> List.try(fun p -> p.x =coord.x && p.y = coord.y )) |>

    let canLiftStatus (p: point) (abscissa: int) (ordinate:int) =
        let a = getAdjacentCoords p abscissa ordinate
                    // |> List.filter (fun x -> x.)
                    |> List.length
        if a >= 4 then
            p
        else
        { p with c = 'x' }

    let getAdjacent (board: point list list ) p =
        let adjacentCoords = board |> List.map( fun x -> x |> List.map(fun p ->  getAdjacentCoords p x.Length board.Length  ))
        0


    let run = 0
