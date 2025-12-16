namespace AdventOfCode

open AdventOfCodeRunner

module Program =
    open System
    open System.Diagnostics
    open System.Timers
    [<EntryPoint>]
    let main argv =
        printfn "press any key to start"
        Console.ReadKey() |> ignore
        let stopwatch = Stopwatch.StartNew()
        use timer = new Timer(100.0) // 100 ms interval
        timer.Elapsed.Add(fun _ ->
            Console.Write($"\rElapsed time: {stopwatch.ElapsedMilliseconds} ms   ")
        )
        timer.Start()

        PrintingDepartment.run()

        stopwatch.Stop()
        timer.Stop()
        Console.WriteLine($"\rElapsed time: {stopwatch.ElapsedMilliseconds} ms   ")
        0
