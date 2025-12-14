namespace AdventOfCodeRunner

open AdventOfCode.PrintingDepartment

module PrintingDepartment =
    let run () =
        let arr = "./AdventOfCode.Test/TestData/day04.test.txt" |> getInput |> toBoard
        arr |> Seq.iter(fun x -> printfn "%A" x)
