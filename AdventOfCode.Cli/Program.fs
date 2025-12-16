namespace AdventOfCode

open AdventOfCodeRunner

module Program =
    open System
    [<EntryPoint>]
    let main argv =
        Console.ReadKey() |> ignore
        PrintingDepartment.run()
        0
