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

    let ciffers2Multiplier (ciffers: int) =
        System.Math.Pow(10.0, float ciffers - 1.0)
    let rec toVal (list: iterItem array) (ciffers: int): int =
        let noTailList = list.[0..list.Length-ciffers]
        let tempMax = noTailList |>  Array.fold(fun prev next -> maxWithIndex prev next) emptyIterItem
        let tail = list.[list.Length-1]
        if tempMax.value = tail.value then
            tempMax.value * 10 + tail.value // Return fast!
        else
        let firstCiffer = tempMax
        let rest = list.[firstCiffer.index + 1 .. list.Length]
        let lastCiffer = rest |> Array.fold(fun prev next -> maxWithIndex prev next) emptyIterItem
        firstCiffer.value * 10 + lastCiffer.value

    let maxJoltageUsingNBatteries filePath n=
        let list = filePath
                |> loadLinesFromFile
                |> List.toArray
                |> Array.map(fun x -> strArr2NumArr x)

        let toIterItems (l: int array) : iterItem array =
            l |> Array.mapi(fun i y ->  { index = i; value = y })

        let what =list |> Array.map(fun x -> x |> toIterItems )
        let a = Array.map(fun x ->  toVal x 2 ) what
        a |> Array.sum


