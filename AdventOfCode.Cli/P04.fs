namespace AdventOfCodeRunner

open AdventOfCode.PrintingDepartment

module PrintingDepartment =
    let run () =
        let b = "./AdventOfCode.Test/TestData/day04.01.txt"
                |> getInput
                |> toBoard

        let final = b |> runGame
        let countOnboard board c =
            printfn "%A" (board |> List.concat |> List.countBy(fun x -> x.c = c) )

        countOnboard b '@'
        countOnboard final '@'
