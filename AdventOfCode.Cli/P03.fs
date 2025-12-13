namespace AdventOfCodeRunner

open AdventOfCode.Lobby

module Lobby =
    let run () =
        printfn "%A" (("./AdventOfCode.Test/TestData/day03.01.txt", 12) ||> maxJoltageUsingNBatteriesTheodor  )
