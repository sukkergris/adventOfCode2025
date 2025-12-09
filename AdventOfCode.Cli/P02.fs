namespace AdventOfCodeRunner

open AdventOfCode.GiftShop

module GiftShop =
    let run () =
        let testfile = "./AdventOfCode.Test/TestData/day02.test.csv"
        printfn "At the Gift shop!"
        // "./AdventOfCode.Test/TestData/day02.test.csv" |> productIds |> writeToCli

        // let products = productIds testfile
        // saveToFile "xyz.txt" products
        let filtered = filter testfile
        let sum = filtered |> List.map (fun x-> x |> int) |> List.sum
        printfn "%A" sum


