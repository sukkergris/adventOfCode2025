namespace AdventOfCode

open System.Collections.Generic
open AdventOfCode.FileHandler
module Lobby =
    let strArr2NumArr (strArr: string) : int64 array = strArr |> Array.ofSeq |> Array.map(fun x -> int64 x - int64 '0')

    type iterItem = { index: int; value: int64 }
    let maxWithIndex (prev: iterItem ) (next: iterItem) =
        if prev.value >= next.value then
            prev
        else
            next
    let emptyIterItem = { index = -1; value = -1 }
    let toIterItems (l: int64 array) : iterItem array =
            l |> Array.mapi(fun i y ->  { index = i; value = y })
    let toIntArr (l: string array) = l |> Array.map(fun x -> strArr2NumArr x)
    let ciffers2Multiplier (ciffers: int) : int64=
        System.Math.Pow(10.0, float ciffers - 1.0) |> int64
    let rec getVal (ciffers: int) (l: int64 array): int64 =
        if ciffers = 0 then
            0
        else if ciffers = 1 then
            l |> Array.max
        else
        let list = l |> toIterItems

        let m = ciffers |> ciffers2Multiplier

        let noTailList = list.[0..list.Length-ciffers]

        let firstCiffer = noTailList
                        |>  Array.fold(fun prev next -> maxWithIndex prev next) emptyIterItem

        let rest = l.[firstCiffer.index + 1 .. list.Length]

        firstCiffer.value * (m |>int64) + getVal (ciffers-1) rest
    let maxJoltageUsingNBatteries filePath n=
        let list = filePath
                |> loadLinesFromFile
                |> List.toArray
                |> toIntArr

        list |> Array.map(fun (x:int64 array)-> getVal n x ) |> Array.sum

// Q2: Answers
// 7839340789394L - remember to use int64
// 173416889848394L


