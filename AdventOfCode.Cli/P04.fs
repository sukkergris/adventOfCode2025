namespace AdventOfCodeRunner

open System
open System.IO
open AdventOfCode.PrintingDepartment

module PrintingDepartment =
    let run () =
        let path =
            Path.Combine(
            "/Users/sukkerfrit/private/fun/adventofcode2025/",
            "AdventOfCode.Test/bin/Debug/net10.0",
            "TestData",
            "day04.01.txt")

        let b = path//"./AdventOfCode.Test/TestData/day04.01.txt"
                |> getInput
                |> toBoard

        let final = b |> runGame
        let countOnboard board c =
            printfn "%A" (board |> List.concat |> List.countBy(fun x -> x.c = c) )

        countOnboard b '@'
        countOnboard final '@'
