namespace AdventOfCodeRunner

open AdventOfCode.GiftShop

module GiftShop =
    let run () =
        printfn "At the Gift shop!"
        "./AdventOfCode.Test/TestData/day02.test.csv" |> productIds |> writeToCli

