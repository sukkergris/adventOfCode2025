namespace AdventOfCodeRunner

open AdventOfCode.PrintingDepartment

module PrintingDepartment =
    let run () =
        let b = "./AdventOfCode.Test/TestData/day04.01.txt"
                |> getInput
                |> toBoard

        let removeX (board: point list list) =
            board |> List.map(fun x -> x |> List.map(fun p -> if p.c = 'x' then { p with c = '.'} else p))

        let rec runGame board =
            let nextMove = board |> getUpdatedBoard

            // view board nextMove

            let flatNextMove = nextMove |> List.concat
            let anyMovableInNextMove = flatNextMove |> List.exists(fun x -> x.c ='x')
            if anyMovableInNextMove then
                nextMove  |> removeX |> runGame
            else
                board

        let final = b |> runGame
        let countOnboard board c =
            printfn "%A" (board |> List.concat |> List.countBy(fun x -> x.c = c) )

        countOnboard b '@'
        countOnboard final '@'
