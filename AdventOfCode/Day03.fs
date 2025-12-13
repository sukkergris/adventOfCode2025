namespace AdventOfCode

open AdventOfCode.FileHandler
module Lobby =
    let strArr2NumArr (strArr: string) : int array = strArr |> Array.ofSeq |> Array.map(fun x -> int x - int '0')
    let maxOf current next =
        if current > next then
            current
        else
            next
    let maxOfExcept (current:int) (next: int) (except:int) : int =
        if current > next && current <> except then
            current
        else
            next
    let selectMax arrayMax headMax =
        if arrayMax > headMax  then headMax else arrayMax
    type iterItem = { index: int; value: int }
    let maxWithIndex (prev: iterItem ) (next: iterItem) =
        if prev.value < next.value then
            prev
        else
            next
    let emptyIterItem = { index = -1; value = -1 }
    let max_ (arr: int array) =
            arr |> Array.mapi (fun i x-> { index=i; value= x } )
                |> Array.fold (fun acc x -> maxWithIndex acc x ) emptyIterItem

    let max (arr: int array) =
        let arrayMax : int = arr |> Array.max
        let tailValue : int = arr[arr.Length-1]
        let headMax : int = arr[.. arr.Length - 2] |> Array.max
        if tailValue = headMax then
            (headMax * 10) + tailValue
        else

        let firstCiffer = selectMax arrayMax headMax

        let lastCiffer : int = arr |> Array.fold (fun acc x-> maxOfExcept acc x firstCiffer) 0
        firstCiffer * 10 + lastCiffer
    let answer filePath =
        filePath |> loadLinesFromFile
        |> List.toArray
        |> Array.map (fun x -> strArr2NumArr x)
        |> Array.map (fun x -> max x)
