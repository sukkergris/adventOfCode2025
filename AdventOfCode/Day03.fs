namespace AdventOfCode

open AdventOfCode.FileHandler
module Lobby =
    let strArr2NumArr (strArr: string) : int array = strArr |> Array.ofSeq |> Array.map(fun x -> int x - int '0')

    type iterItem = { index: int; value: int }
    let maxWithIndex (prev: iterItem ) (next: iterItem) =
        if prev.value >= next.value then
            prev
        else
            next
    let emptyIterItem = { index = -1; value = -1 }

    let answer_ filePath =
        let list = filePath
                |> loadLinesFromFile
                |> List.toArray
                |> Array.map(fun x -> strArr2NumArr x)

        let toIterItems (l: int array) : iterItem array =
            l |> Array.mapi(fun i y ->  { index = i; value = y })

        let toVal (l: iterItem array) : int =
            let lnoTail = l.[0..l.Length-2]
            let tempMax = lnoTail |>  Array.fold(fun prev next -> maxWithIndex prev next) emptyIterItem
            let tail = l.[l.Length-1]
            if tempMax.value = tail.value then
                tempMax.value * 10 + tail.value // Return fast!
            else
            let firstCiffer = tempMax
            let rest = l.[firstCiffer.index + 1 .. l.Length]
            let lastCiffer = rest |> Array.fold(fun prev next -> maxWithIndex prev next) emptyIterItem
            firstCiffer.value * 10 + lastCiffer.value

        let what =list |> Array.map(fun x -> x |> toIterItems )
        let a = Array.map(fun x -> x |> toVal ) what
        a |> Array.sum
