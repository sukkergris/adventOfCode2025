namespace AdventOfCode

open FileLoader
module GiftShop =
    type ProductId = { FirstId: string; LastId: string }

    let productIds path =
        let lines = loadCSVFile path
        let mapped =
            lines
            |> List.filter( fun x -> x <> "" )
            |> List.map (fun x ->
                printfn "%A" x
                let parts = x.Split('-')
                { FirstId = parts.[0]; LastId = parts.[1] })
        mapped

    let writeToCli products =
        products |> List.iter (fun id ->  printfn "%A" id )
