namespace AdventOfCodeRunner

open AdventOfCode.GiftShop

module GiftShop =
    let run () =
        let testfile = "./AdventOfCode.Test/TestData/day02.02.csv"
        printfn "At the Gift shop!"
        // "./AdventOfCode.Test/TestData/day02.test.csv" |> productIds |> writeToCli

        let products = productIds testfile
        // saveToFile "xyz.txt" products
        let reg1 = @"^((?!0)\d+)\1$"
        let reg2 = @"(?<=,|^)(((\d)\4+)|((.+)\5+))(?=,|$)"
        let filtered = filter testfile reg2
        let sum = filtered |> List.map (fun x-> x |> int64) |> List.sum
        printfn "%A" sum
        // System.IO.File.WriteAllLines ("output.txt", filtered)
