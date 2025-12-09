namespace AdventOfCode

open FileHandler
open System.Text.RegularExpressions
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

    let writeToCli (products: string list) =
        printfn "%A" products.Length
        products |> List.iter (fun id ->  printfn "%A" id )

    let toString (product: ProductId) : string =
        let sequence = seq { product.FirstId |> int64 .. int64 product.LastId }
        let unFoldedList = sequence |> Seq.map string |> String.concat ","
        unFoldedList
    let toListOfStrings (product: ProductId) : string list=
        let sequence = seq { product.FirstId |> int64 .. int64 product.LastId }
        let unFoldedList = sequence |> Seq.map string |> Seq.toList
        unFoldedList

    let saveToFile (path: string) (products: ProductId list) =
        let lines =
                            products
                                |> List.toSeq
                                |> Seq.map toString
        System.IO.File.WriteAllLines (path, lines)


    let filter (path: string) =
        let products = productIds path

        let pattern = @"^((?!0)\d+)\1$"
        let regex = Regex(pattern)

        let filtered = products |> List.collect toListOfStrings
                    |> List.filter (fun x ->
                       (regex.IsMatch x))
                    |> List.filter(fun x-> x <> "101")
        filtered
// Guesses:
// 1846542800809725
// 1846542800809725L (12850231731L)
// 12850231731L
